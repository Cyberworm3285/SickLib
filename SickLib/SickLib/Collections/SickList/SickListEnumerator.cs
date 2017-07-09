using System.Collections;
using System.Collections.Generic;

namespace SickLib.Collections.SickList
{
    class SickListEnumerator<T> : IEnumerator<T>
    {
        private readonly SickList<T> _list;
        private bool _flag = true;

        public SickListEnumerator(SickList<T> list)
        {
            _list = list;
            _list.MoveToHead();
        }

        public T Current => _flag?default(T):_list.Current.Item;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (_flag)
            {
                _flag = false;
                return true;
            }
            return _list.MoveNext();
        }
            

        public void Reset()
        {
            _list.MoveToHead();
            _flag = true;
        }
    }
}
