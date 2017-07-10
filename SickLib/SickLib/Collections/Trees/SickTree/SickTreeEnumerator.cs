using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SickLib.Collections.Trees.SickTree
{
    class SickTreeEnumerator<TItem, TDecision> : IEnumerator<TItem>, IEnumerator
    {
        private readonly List<TItem> list;
        private int index = -1;

        public SickTreeEnumerator(SickTree<TItem, TDecision> tree, Traversion traversion)
        {
            list = tree.Traverse(traversion);
        }


        public object Current => list[index];

        TItem IEnumerator<TItem>.Current => list[index];

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (++index >= list.Count)
                return false;
            return
                true;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
