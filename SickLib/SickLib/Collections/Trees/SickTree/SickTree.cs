using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SickLib.Collections.Trees.SickTree
{
    public enum Traversion
    {
        PreOrder,
        PostOrder
    }


    public class SickTree<TItem, TDecision> : IEnumerable<TItem>, IEnumerable
    {
        public SickTreeNode<TItem, TDecision> Root { get; private set; }
        public Traversion CurrTraversion { get; set; }

        public SickTree()
        {
            Root = new SickTreeNode<TItem, TDecision>();
            CurrTraversion = Traversion.PreOrder;
        }

        public void AddPath(TItem item, TDecision[] path, bool overrideExisting = false)
        {
            if (path.Length == 0)
            {
                Root.Item = item;
                return;
            }

            var curr = Root;

            int i;
            for (i = 0; i < path.Length - 1; i++)
            {
                if (curr.Children.ContainsKey(path[i]))
                    curr = curr.Children[path[i]];
                else
                {
                    var temp = new SickTreeNode<TItem, TDecision>();
                    curr.Children.Add(path[i], temp);
                    curr = temp;
                }
            }

            if (curr.Children.ContainsKey(path[i]))
                if (overrideExisting)
                    curr.Children[path[i]].Item = item;
                else
                    throw new ArgumentException("path already exists");
            else
                curr.Children.Add(path[i], new SickTreeNode<TItem, TDecision>(item));
            
        }

        #region Traversion

        public List<TItem> Traverse(Traversion traversion)
        {
            List<TItem> result = new List<TItem>();

            switch (traversion)
            {
                case Traversion.PreOrder:
                    PreOrder(Root, ref result);
                    break;
                case Traversion.PostOrder:
                    PostOrder(Root, ref result);
                    break;
                default:
                    throw new ArgumentException("Traversion not supported (did the enum change?)");
            }

            return result;
        }

        private void PreOrder(SickTreeNode<TItem, TDecision> node, ref List<TItem> result)
        {
            if (node.HasValue) result.Add(node.Item);
            if (node.Children.Count != 0)
            {
                foreach (var v in node.Children.Values)
                    PreOrder(v, ref result);
            }
        }

        private void PostOrder(SickTreeNode<TItem, TDecision> node, ref List<TItem> result)
        {
            if (node.Children.Count != 0)
            {
                foreach (var v in node.Children.Values)
                    PreOrder(v, ref result);
            }
            if (node.HasValue) result.Add(node.Item);
        }

        #endregion

        #region IEnumerable<T> implementation

        public IEnumerator<TItem> GetEnumerator()
        {
            return new SickTreeEnumerator<TItem, TDecision>(this, CurrTraversion);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SickTreeEnumerator<TItem, TDecision>(this, CurrTraversion);
        }

        #endregion
    }
}
