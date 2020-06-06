using System;

namespace ListLibrary
{
    public class MyLinkedList
    {
        // Поле для ссылки на первый и последний элементы
        MyLinkedListNode beg = null;
        MyLinkedListNode end = null;
        // Кол-во элементов в списке
        int count = 0;

        // Конструктор без параметров (создаёт пустой список) 
        public MyLinkedList() { }

        // Конструктор с заданным начальным размером списка (создаёт список со стандартными значениями эелемнтов)
        public MyLinkedList(int size)
        {
            if (size < 0) throw new Exception("Invalid value!");
            for (int i = 0; i < size; i++) AddLast(new MyLinkedListNode());
        }

        // Кол-во элементов в списке
        public int Count
        {
            get { return count; }
        }

        // Ссылка на первый элемент
        public MyLinkedListNode First
        {
            get { return beg; }
        }

        // Ссылка на последний элемент
        public MyLinkedListNode Last
        {
            get { return end; }
        }

        // Проверка на пустоту списка
        public bool IsEmpty
        {
            get { return beg == null; }
        }

        public double Calculate(double x)
        {
            if (IsEmpty) throw new Exception("Empty list!");
            double result = 0;
            MyLinkedListNode tmp = First;
            while(tmp != null)
            {
                result += tmp.A * Math.Pow(x, tmp.I);
                tmp = tmp.next;
            }
            return result;
        }

        // Удаление элемента
        public void Remove(MyLinkedListNode existingNode)
        {
            if (existingNode == null) throw new Exception("Null argument!");
            if (existingNode.owner != this) throw new Exception("The node does not belong to this list!");
            existingNode.owner = null;
            if (existingNode.previous == null)
            {
                beg = beg.next;
                existingNode.next.previous = existingNode.previous;
            }
            else
            {
                if (existingNode.next == null) 
                {
                    end = end.previous;
                    existingNode.previous.next = existingNode.next;
                }
                else
                {
                    existingNode.next.previous = existingNode.previous;
                    existingNode.previous.next = existingNode.next;
                }
            }
            count--;
        }

        // Удаление элемента с соответствующим значением
        public bool Remove(int i, int a)
        {
            MyLinkedListNode tmp = First;
            while (tmp != null)
            {
                if (tmp.A.Equals(a) && tmp.I.Equals(i))
                {
                    Remove(tmp);
                    return true;
                }
                tmp = tmp.next;
            }
            return false;
        }

        // Добавление нового значения после какого-либо элемента
        public void AddAfter(MyLinkedListNode existingNode, int i, int a)
        {
            AddAfter(existingNode, new MyLinkedListNode(i, a));
        }

        // Добавление нового элемента в список после какого-либо элемента
        public void AddAfter(MyLinkedListNode existingNode, MyLinkedListNode newNode)
        {
            if (existingNode == null || newNode == null) throw new Exception("Null argument!");
            if (existingNode.owner != this) throw new Exception("The node does not belong to this list!");
            if (newNode.owner != null) throw new Exception("Adding node already belongs to some list!");
            newNode.owner = this;
            newNode.next = existingNode.next;
            newNode.previous = existingNode;
            existingNode.next = newNode;
            if (existingNode == end) end = newNode;
            else existingNode.next.previous = newNode;
            count++;
        }

        // Добавление нового значения перед каким-либо элементом
        public void AddBefore(MyLinkedListNode existingNode, int i, int a)
        {
            AddBefore(existingNode, new MyLinkedListNode(i, a));
        }

        // Добавление нового элемента в список перед каким-либо элементом
        public void AddBefore(MyLinkedListNode existingNode, MyLinkedListNode newNode)
        {
            if (existingNode == null || newNode == null) throw new Exception("Null argument!");
            if (existingNode.owner != this) throw new Exception("The node does not belong to this list!");
            if (newNode.owner != null) throw new Exception("Adding node already belongs to some list!");
            newNode.owner = this;
            newNode.next = existingNode;
            newNode.previous = existingNode.previous;
            if (existingNode == beg) beg = newNode;
            else existingNode.previous.next = newNode;
            existingNode.previous = newNode;
            count++;
        }

        // Удаление последнего элемента
        public void RemoveLast()
        {
            Remove(Last);
        }

        // Добавление значения в конец списка
        public void AddLast(int i, int a)
        {
            AddLast(new MyLinkedListNode(i, a));
        }

        // Добавление элемента в конец списка
        public void AddLast(MyLinkedListNode newNode)
        {
            if (IsEmpty) AddStartNode(newNode);
            else AddAfter(Last, newNode);
        }

        // Удаление первого элемента
        public void RemoveFirst()
        {
            Remove(First);
        }

        // Добавление значения в начало списка
        public void AddFirst(int i, int a)
        {
            AddFirst(new MyLinkedListNode(i, a));
        }

        // Добавление элемента в начало списка
        public void AddFirst(MyLinkedListNode newNode)
        {
            if (IsEmpty) AddStartNode(newNode);
            else AddBefore(First, newNode);
        }

        // Проверка содержит ли список заданное значение
        public bool Contains(int i, int a)
        {
            MyLinkedListNode tmp = First;
            while (tmp != null)
            {
                if (tmp.A.Equals(a) && tmp.I.Equals(i)) return true;
                tmp = tmp.next;
            }
            return false;
        }

        // Поиск первого элемента с заданным значением
        public MyLinkedListNode Find(int i, int a)
        {
            MyLinkedListNode tmp = First;
            while (tmp != null)
            {
                if (tmp.A.Equals(a) && tmp.I.Equals(i)) return tmp;
                tmp = tmp.next;
            }
            return null;
        }

        // Поиск последнего элемента с заданным значением
        public MyLinkedListNode FindLast(int i, int a)
        {
            MyLinkedListNode tmp = Last;
            while (tmp != null)
            {
                if (tmp.A.Equals(a) && tmp.I.Equals(i)) return tmp;
                tmp = tmp.previous;
            }
            return null;
        }

        // Добавление элемента в пустой список
        private void AddStartNode(MyLinkedListNode node)
        {
            if (node == null) throw new Exception("Null argument!");
            if (node.owner != null) throw new Exception("Adding node already belongs to some list!");
            node.owner = this;
            beg = end = node;
            count++;
        }

        // Печать списка
        public void Print(string sep = " ", string end = "\n")
        {
            if (IsEmpty)
            {
                Console.WriteLine("This list is empty.");
                return;
            }
            MyLinkedListNode tmp = beg;
            while (tmp != this.end)
            {
                Console.Write(tmp.ToString() + sep);
                tmp = tmp.next;
            }
            Console.Write(tmp.ToString() + end);
        }
    }
}