using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Naninovel.ArabicSupport;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Naninovel.UI
{
    /// <summary>
    /// Wrapper over TMPro text with Naninovel-specific tags and arabic text support.
    /// </summary>
    public class NaninovelTMProText : TextMeshProUGUI, IPointerClickHandler, IInputTrigger
    {
        [Serializable]
        public class LinkClickedEvent : UnityEvent<TMP_LinkInfo> { }

        public override string text { get => unprocessedText; set => base.text = ProcessText(unprocessedText = value); }

        protected virtual string RubyVerticalOffset => rubyVerticalOffset;
        protected virtual float RubySizeScale => rubySizeScale;
        protected virtual bool AddRubyLineHeight => addRubyLineHeight;
        protected virtual bool UnlockTipsOnPrint => unlockTipsOnPrint;
        protected virtual bool FixArabicText => fixArabicText;
        protected virtual string TipTemplate => tipTemplate;
        protected virtual Canvas TopmostCanvas => topmostCanvasCache ? topmostCanvasCache : topmostCanvasCache = gameObject.FindTopmostComponent<Canvas>();
        protected virtual bool Edited => !Application.isPlaying || ObjectUtils.IsEditedInPrefabMode(gameObject);

        [Tooltip("Vertical line offset to use for the ruby (furigana) text; supported units: em, px, %.")]
        [SerializeField] private string rubyVerticalOffset = "1em";
        [Tooltip("Font size scale (relative to the main text font size) to apply for the ruby (furigana) text.")]
        [SerializeField] private float rubySizeScale = .5f;
        [Tooltip("Whether to compensate (add) line height for the lines that contain ruby tags.")]
        [SerializeField] private bool addRubyLineHeight = true;
        [Tooltip("Whether to automatically unlock associated tip records when text wrapped in <tip> tags is printed.")]
        [SerializeField] private bool unlockTipsOnPrint = true;
        [Tooltip("Template to use when processing text wrapped in <tip> tags. " + tipTemplateLiteral + " will be replaced with the actual tip content.")]
        [SerializeField] private string tipTemplate = $"<u>{tipTemplateLiteral}</u>";
        [Tooltip("Invoked when a text wrapped in <tip> tags is clicked; returned string argument is the ID of the clicked tip. Be aware, that the default behaviour (showing `ITipsUI` when a tip is clicked) won't be invoked when a custom handler is assigned.")]
        [SerializeField] private StringUnityEvent onTipClicked;
        [Tooltip("Whether to modify the text to support arabic languages (fix letters connectivity issues).")]
        [SerializeField] private bool fixArabicText;
        [Tooltip("When `Fix Arabic Text` is enabled, controls to whether also fix Farsi characters.")]
        [SerializeField] private bool fixArabicFarsi = true;
        [Tooltip("When `Fix Arabic Text` is enabled, controls to whether also fix rich text tags.")]
        [SerializeField] private bool fixArabicTextTags = true;
        [Tooltip("When `Fix Arabic Text` is enabled, controls to whether preserve numbers.")]
        [SerializeField] private bool fixArabicPreserveNumbers;
        [Tooltip("Template to use when processing text wrapped in <link> tags. " + linkTemplateLiteral + " will be replaced with the actual tip content. When nothing is specified, the link tags won't be modified.")]
        [SerializeField] private string linkTemplate = $"<u>{linkTemplateLiteral}</u>";
        [Tooltip("Invoked when a text wrapped in <link> tags is clicked.")]
        [SerializeField] private LinkClickedEvent onLinkClicked;

        private const int rubyLinkHash = -153532380;
        private const string rubyLinkId = "NANINOVEL.RUBY";
        private const string tipIdPrefix = "NANINOVEL.TIP.";
        private const string tipTemplateLiteral = "%TIP%";
        private const string linkTemplateLiteral = "%LINK%";
        private static readonly Regex captureRubyRegex = new(@"<ruby=""([\s\S]*?)"">([\s\S]*?)</ruby>", RegexOptions.Compiled);
        private static readonly Regex captureTipRegex = new(@"<tip=""(\w*?)"">([\s\S]*?)</tip>", RegexOptions.Compiled);
        private static readonly Regex captureLinkRegex = new(@"<link=""(\w*?)"">([\s\S]*?)</link>", RegexOptions.Compiled);
        private static readonly Regex captureEventRegex = new("<@(.*?)>", RegexOptions.Compiled);
        private static readonly Regex captureExpressionRegex = new("<:(.*?)>", RegexOptions.Compiled);

        private readonly FastStringBuilder arabicBuilder = new(RTLSupport.DefaultBufferSize);
        private readonly Dictionary<int, string> charToEvent = new();
        private string unprocessedText = "";
        private Canvas topmostCanvasCache;

        public virtual void OnPointerClick (PointerEventData eventData)
        {
            var renderCamera = TopmostCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : TopmostCanvas.worldCamera;
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(this, eventData.position, renderCamera);
            if (linkIndex >= 0) OnLinkClicked(textInfo.linkInfo[linkIndex]);
        }

        public virtual bool CanTriggerInput ()
        {
            if (!TryGetMousePosition(out var position)) return true;
            var renderCamera = TopmostCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : TopmostCanvas.worldCamera;
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(this, position, renderCamera);
            return linkIndex == -1;
        }

        /// <summary>
        /// Returns bodies of the event tags placed at or before specified character index.
        /// Returns null when no events are found. Returned bodies are removed and won't be returned again.
        /// </summary>
        public virtual IReadOnlyCollection<string> PopEventsAtChar (int index)
        {
            var found = default(Dictionary<int, string>);
            foreach (var kv in charToEvent)
                if (kv.Key <= index)
                    if (found == null) found = new() { [kv.Key] = kv.Value };
                    else found[kv.Key] = kv.Value;
            if (found != null)
                foreach (var key in found.Keys)
                    charToEvent.Remove(key);
            return found?.Values;
        }

        protected override void Awake ()
        {
            base.Awake();
            if (!Edited) text = base.text ?? "";
        }

        protected virtual void OnLinkClicked (TMP_LinkInfo linkInfo)
        {
            if (onLinkClicked?.GetPersistentEventCount() > 0)
                onLinkClicked.Invoke(linkInfo);

            var linkId = linkInfo.GetLinkID();
            if (linkId.StartsWithFast(tipIdPrefix))
                OnTipClicked(linkId.GetAfter(tipIdPrefix));
        }

        protected virtual void OnTipClicked (string tipId)
        {
            if (onTipClicked?.GetPersistentEventCount() > 0)
            {
                onTipClicked.Invoke(tipId);
                return;
            }

            var tipsUI = Engine.GetService<IUIManager>()?.GetUI<ITipsUI>();
            tipsUI?.SelectTipRecord(tipId);
            tipsUI?.Show();
        }

        /// <summary>
        /// Applies various pre-processing (ruby, tips, arabic, etc) before assigning TMPro <see cref="TextMeshProUGUI.text"/>.
        /// </summary>
        protected virtual string ProcessText (string content)
        {
            if (string.IsNullOrEmpty(content)) return content;
            return FixArabic(ProcessEventTags(ProcessRubyTags(ProcessTipTags(ProcessLinkTags(ProcessExpressionTags(content))))));
        }

        /// <summary>
        /// When 'Link Template' is assigned, will modify the link content in accordance with the template.
        /// </summary>
        protected virtual string ProcessLinkTags (string content)
        {
            if (string.IsNullOrEmpty(linkTemplate) || !linkTemplate.Contains(linkTemplateLiteral)) return content;

            var matches = captureLinkRegex.Matches(content);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 3) continue;
                var fullMatch = match.Groups[0].ToString();
                var linkId = match.Groups[1].ToString();
                var linkContent = match.Groups[2].ToString();

                var replace = $"<link={linkId}>{linkTemplate.Replace(linkTemplateLiteral, linkContent)}</link>";
                content = content.Replace(fullMatch, replace);
            }

            return content;
        }

        /// <summary>
        /// Given the input text, extracts text wrapped in ruby tags and replace it with expression natively supported by TMPro.
        /// </summary>
        protected virtual string ProcessRubyTags (string content)
        {
            var matches = captureRubyRegex.Matches(content);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 3) continue;
                var fullMatch = match.Groups[0].ToString();
                var rubyValue = match.Groups[1].ToString();
                var baseText = match.Groups[2].ToString();

                var margin = ((m_margin.x > 0 ? m_margin.x : 0) + (m_margin.z > 0 ? m_margin.z : 0)) / 2f;
                var baseTextWidth = GetPreferredValues(baseText).x - margin;
                var rubyTextWidth = GetPreferredValues(rubyValue).x * RubySizeScale - margin;
                var rubyTextOffset = (baseTextWidth + rubyTextWidth - margin) / 2f;
                var compensationOffset = (baseTextWidth - rubyTextWidth - margin) / 2f;
                var replace = $"{baseText}<space=-{rubyTextOffset}><voffset={RubyVerticalOffset}><size={RubySizeScale * 100f}%><link=\"{rubyLinkId}\">{rubyValue}</link></size></voffset><space={compensationOffset}>";
                if (!AddRubyLineHeight) replace = "<line-height=100%>" + replace;
                content = content.Replace(fullMatch, replace);
            }

            return content;
        }

        /// <summary>
        /// Registers event tags (<@...>) and removes them from specified content.
        /// </summary>
        protected virtual string ProcessEventTags (string content)
        {
            charToEvent.Clear();
            var matches = captureEventRegex.Matches(content);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 2) continue;
                var fullMatch = match.Groups[0].ToString();
                var eventBody = match.Groups[1].ToString();
                charToEvent[match.Index] = eventBody;
                content = content.Remove(fullMatch);
            }
            return content;
        }

        /// <summary>
        /// Replaces expression tags (<:...>) with evaluated expression result.
        /// </summary>
        protected virtual string ProcessExpressionTags (string content)
        {
            var matches = captureExpressionRegex.Matches(content);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 2) continue;
                var fullMatch = match.Groups[0].ToString();
                var expression = match.Groups[1].ToString();
                var result = ExpressionEvaluator.Evaluate<string>(expression, e => Engine.Err(e));
                content = content.Replace(fullMatch, result);
            }
            return content;
        }

        /// <summary>
        /// Given the input text, extracts text wrapped in tip tags and replace it with expression natively supported by TMPro.
        /// </summary>
        protected virtual string ProcessTipTags (string content)
        {
            var matches = captureTipRegex.Matches(content);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 3) continue;
                var fullMatch = match.Groups[0].ToString();
                var tipID = match.Groups[1].ToString();
                var tipContent = match.Groups[2].ToString();

                if (UnlockTipsOnPrint)
                    Engine.GetService<IUnlockableManager>()?.UnlockItem($"Tips/{tipID}");

                var replace = $"<link={tipIdPrefix + tipID}>{TipTemplate.Replace(tipTemplateLiteral, tipContent)}</link>";
                content = content.Replace(fullMatch, replace);
            }

            return content;
        }

        /// <summary>
        /// Checks whether character with the specified index is inside ruby tag.
        /// </summary>
        protected virtual bool IsRuby (int charIndex)
        {
            for (int i = 0; i < textInfo.linkCount; i++)
                if (IsRubyLinkAndContainsChar(textInfo.linkInfo[i]))
                    return true;
            return false;

            bool IsRubyLinkAndContainsChar (TMP_LinkInfo link) =>
                link.hashCode == rubyLinkHash &&
                charIndex >= link.linkTextfirstCharacterIndex &&
                charIndex < link.linkTextfirstCharacterIndex + link.linkTextLength;
        }

        protected virtual string FixArabic (string value)
        {
            if (!FixArabicText || string.IsNullOrWhiteSpace(value)) return value;
            arabicBuilder.Clear();
            RTLSupport.FixRTL(value, arabicBuilder, fixArabicFarsi, fixArabicTextTags, fixArabicPreserveNumbers);
            arabicBuilder.Reverse();
            return arabicBuilder.ToString();
        }

        protected virtual bool TryGetMousePosition (out Vector2 position)
        {
            position = default;
            #if ENABLE_LEGACY_INPUT_MANAGER
            position = Input.mousePosition;
            return true;
            #elif ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
            if (UnityEngine.InputSystem.Mouse.current == null) return false;
            position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
            return true;
            #else
            return false;
            #endif
        }
    }
}
