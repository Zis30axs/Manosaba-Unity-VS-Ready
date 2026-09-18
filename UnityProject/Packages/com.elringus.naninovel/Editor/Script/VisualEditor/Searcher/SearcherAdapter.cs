using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace Naninovel.Searcher
{
    public enum ItemExpanderState
    {
        Hidden,
        Collapsed,
        Expanded
    }

    [PublicAPI]
    public interface ISearcherAdapter
    {
        VisualElement MakeItem();
        VisualElement Bind(VisualElement target, SearcherItem item, ItemExpanderState expanderState, string text);

        string Title { get; }
        bool HasDetailsPanel { get; }
        void OnSelectionChanged(IEnumerable<SearcherItem> items);
        void InitDetailsPanel(VisualElement detailsPanel);
    }

    [PublicAPI]
    public class SearcherAdapter : ISearcherAdapter
    {
        public virtual string Title { get; }
        public virtual bool HasDetailsPanel => true;

        private const string entryName = "smartSearchItem";
        private const int indentDepthFactor = 15;
        private readonly VisualTreeAsset defaultItemTemplate;
        private Label detailsLabel;

        public SearcherAdapter(string title)
        {
            Title = title;
            defaultItemTemplate = Resources.Load<VisualTreeAsset>("Searcher/SearcherItem");
        }

        public virtual VisualElement MakeItem()
        {
            // Create a visual element hierarchy for this search result.
            var item = defaultItemTemplate.CloneTree();
            return item;
        }

        public virtual VisualElement Bind(VisualElement element, SearcherItem item, ItemExpanderState expanderState, string query)
        {
            var indent = element.Q<VisualElement>("itemIndent");
            indent.style.width = item.Depth * indentDepthFactor;

            var expander = element.Q<VisualElement>("itemChildExpander");

            var icon = expander.Query("expanderIcon").First();
            icon.ClearClassList();

            switch (expanderState)
            {
                case ItemExpanderState.Expanded:
                    icon.AddToClassList("Expanded");
                    break;

                case ItemExpanderState.Collapsed:
                    icon.AddToClassList("Collapsed");
                    break;
            }

            var nameLabelsContainer = element.Q<VisualElement>("labelsContainer");
            nameLabelsContainer.Clear();

            if (string.IsNullOrWhiteSpace(query))
            {
                if (string.IsNullOrEmpty(item.Help))
                    nameLabelsContainer.Add(new Label(item.Name));
                else
                {
                    var name = new Label(item.Name);
                    var help = new Label("  (" + item.Help + ")");
                    help.style.opacity = .5f;
                    nameLabelsContainer.Add(name);
                    nameLabelsContainer.Add(help);
                }
            }
            else
                SearcherHighlighter.HighlightTextBasedOnQuery(nameLabelsContainer, item.Name, query);

            element.userData = item;
            element.name = entryName;

            return expander;
        }

        public virtual void InitDetailsPanel(VisualElement detailsPanel)
        {
            detailsLabel = new();
            detailsPanel.Add(detailsLabel);
        }

        public virtual void OnSelectionChanged(IEnumerable<SearcherItem> items)
        {
            if (detailsLabel != null)
            {
                var itemsList = items.ToList();
                detailsLabel.text = itemsList.Any() ? itemsList[0].Help : "No results";
            }
        }
    }
}
