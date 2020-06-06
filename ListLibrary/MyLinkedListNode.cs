namespace ListLibrary
{
    public class MyLinkedListNode
    {
        // Поле для хранения коэффициента
        internal int a;

        // Поле для хранения показателя степени
        internal int i;

        // Ссылка на следующий элемент
        internal MyLinkedListNode next = null;

        // Ссылка на предыдущий эжлемент
        internal MyLinkedListNode previous = null;

        // Список, к. принадлежит элемент
        internal MyLinkedList owner = null;

        // Конструктор с заданным значением
        public MyLinkedListNode(int i = default, int a = default)
        {
            I = i;
            A = a;
        }

        // Ссылка на следующий элемент
        public MyLinkedListNode Next
        {
            get { return next; }
        }

        // Ссылка на предыдущий эжлемент
        public MyLinkedListNode Previous
        {
            get { return previous; }
        }

        // Получение/изменение данных
        public int I
        {
            get { return i; }
            set { i = value; }
        }

        // Получение/изменение данных
        public int A
        {
            get { return a; }
            set { a = value; }
        }

        /*public override bool Equals(object obj)
        {
            return obj is MyLinkedListNode<T> node && node.owner == owner && node.next == next && node.previous == previous && node.Value.Equals(Value);
        }*/

        // Преобразование в строку
        public override string ToString()
        {
            return i.ToString() + " " + a.ToString();
        }
    }
}