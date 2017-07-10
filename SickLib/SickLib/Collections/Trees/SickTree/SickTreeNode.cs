using System;
using System.Collections.Generic;
using System.Text;

namespace SickLib.Collections.Trees.SickTree
{
    public class SickTreeNode<TItem, TDecision>
    {
        public Dictionary<TDecision, SickTreeNode<TItem, TDecision>> Children { get; private set; }

        private TItem item;
        public TItem Item
        {
            get => item;
            set
            {
                item = value;
                HasValue = true;
            }
        }

        public bool HasValue { get; private set; }

        public SickTreeNode()
        {
            Children = new Dictionary<TDecision, SickTreeNode<TItem, TDecision>>();
            HasValue = false;
        }

        public SickTreeNode(TItem item)
        {
            Item = item;
            Children = new Dictionary<TDecision, SickTreeNode<TItem, TDecision>>();
            HasValue = true;
        }

        public void Invalidate()
        {
            Item = default(TItem);
            HasValue = false;
        }
    }
}
