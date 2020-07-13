namespace CustomList
{
    public class Item<T>
    {
        public T Data { get; set; }

        public Item<T> Next { get; set; }

        public Item(T data)
        {
            Data = data;
            Next = null;
        }
    }
}