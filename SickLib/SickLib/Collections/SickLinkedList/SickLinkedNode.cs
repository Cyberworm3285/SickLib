namespace SickLib.Collections.SickLinkedList
{
    public class SickLinkedNode<T>
    {
        public SickLinkedNode<T> Prev { get; set; }
        public SickLinkedNode<T> Next { get; set; }

        public T Item { get; set; }

        public SickLinkedNode(T item, SickLinkedNode<T> p = null, SickLinkedNode<T> n = null)
        {
            Item = item;
            Prev = p;
            Next = n;
        }
    }
}
