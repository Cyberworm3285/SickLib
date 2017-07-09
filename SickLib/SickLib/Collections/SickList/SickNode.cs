namespace SickLib.Collections.SickList
{
    public class SickNode<T>
    {
        public SickNode<T> Prev { get; set; }
        public SickNode<T> Next { get; set; }

        public T Item { get; set; }

        public SickNode(T item, SickNode<T> p = null, SickNode<T> n = null)
        {
            Item = item;
            Prev = p;
            Next = n;
        }
    }
}
