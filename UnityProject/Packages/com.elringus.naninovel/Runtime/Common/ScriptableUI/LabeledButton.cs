using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel
{
    public class LabeledButton : Button
    {
        public virtual TMP_Text Label => labelText ? labelText : labelText = GetComponentInChildren<TMP_Text>();
        public virtual ColorBlock LabelColorBlock => labelColors;
        public virtual Color LabelColorMultiplier
        {
            get => labelColorMultiplier;
            set
            {
                labelColorMultiplier = value;
                DoStateTransition(currentSelectionState, false);
            }
        }

        [SerializeField] private TMP_Text labelText;
        [SerializeField] private ColorBlock labelColors = ColorBlock.defaultColorBlock;

        private Color labelColorMultiplier = Color.white;
        private Tweener<ColorTween> tintTweener;

        protected override void Awake ()
        {
            base.Awake();

            tintTweener = new();
        }

        protected override void DoStateTransition (SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            if (!Label) return;

            var tintColor = state switch {
                SelectionState.Normal => LabelColorBlock.normalColor,
                SelectionState.Highlighted => LabelColorBlock.highlightedColor,
                SelectionState.Pressed => LabelColorBlock.pressedColor,
                SelectionState.Selected => LabelColorBlock.selectedColor,
                SelectionState.Disabled => LabelColorBlock.disabledColor,
                _ => Color.white
            };

            if (instant)
            {
                if (tintTweener != null && tintTweener.Running) tintTweener.CompleteInstantly();
                Label.color = tintColor * LabelColorBlock.colorMultiplier * LabelColorMultiplier;
            }
            else if (tintTweener != null)
            {
                var tween = new ColorTween(Label.color, tintColor * LabelColorBlock.colorMultiplier * LabelColorMultiplier,
                    new(LabelColorBlock.fadeDuration, scale: false), ColorTweenMode.All, c => Label.color = c);
                tintTweener.Run(tween, target: Label);
            }
        }
    }
}
