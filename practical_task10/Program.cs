using System;
using System.IO;
using ListLibrary;

namespace practical_task10
{
    public class Program
    {
        // Вывод меню
        static void PrintMenu(string[] menuItems, int choice, string info)
        {
            Console.Clear();
            Console.WriteLine(info);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == choice) Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{i + 1}. {menuItems[i]}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        // Выбор пункта из меню
        static int MenuChoice(string[] menuItems, string info = "")
        {
            Console.CursorVisible = false;
            int choice = 0;
            while (true)
            {
                PrintMenu(menuItems, choice, info);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (choice == 0) choice = menuItems.Length;
                        choice--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (choice == menuItems.Length - 1) choice = -1;
                        choice++;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return choice;
                }
            }
        }

        // Ввод действительного числа с клавиатуры
        public static double DoubleInput(double lBound = double.MinValue, double uBound = double.MaxValue, string info = "")
        {
            bool exit;
            double result;
            Console.Write(info);
            do
            {
                exit = double.TryParse(Console.ReadLine(), out result);
                if (!exit) Console.Write("Введено не число! Повторите ввод: ");
                else if (result <= lBound || result >= uBound)
                {
                    Console.Write("Введено недопустимое значение! Повторите ввод: ");
                    exit = false;
                }
            } while (!exit);
            return result;
        }

        // Получение списка показателей степеней и коэфициентов из файла
        public static bool GetValues(string path, out MyLinkedList result)
        {
            result = new MyLinkedList();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] input = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(input[0], out int i) && int.TryParse(input[1], out int a))
                    {
                        if (a != 0) result.AddLast(i, a);
                    }
                    else return false;
                }
            }
            return true;
        }

        // Ввод значений х и подсчёт значения полинома
        public static void GetArgs(MyLinkedList polynomial, string info = "")
        {
            Console.WriteLine("Введите значения х, для которых нужно найти значение полинома.");
            Console.WriteLine("Чтобы завершить ввод, введите 0.");
            double x = DoubleInput(info: info);
            while(x != 0)
            {
                Console.WriteLine($"P({x}) = {polynomial.Calculate(x)}");
                x = DoubleInput(info: info);
            }
        }

        static void Main(string[] args)
        {
            // Пункты меню
            string[] MENU_ITEMS = { "Вычислить значение полинома", "Выйти из программы" };

            // Индекс пункта - выход из программы
            const int EXIT_CHOICE = 1;

            // Индекс пункта меню, который выбрал пользователь
            int userChoice;

            while (true)
            {
                MyLinkedList polinomial;

                // Пользователь выбирает действие (выйти или создать полином)
                userChoice = MenuChoice(MENU_ITEMS, "Программа для вычисления значения полинома Р(х) с целочисленными коэффициентами\nВыберите действие:");
                if (userChoice == EXIT_CHOICE) break;
                Console.Clear();

                // Ввод имени файла с коэфицентами и степенями                
                Console.Write("Введите имя текстового файла с показателями степеней и коэффициентами:");
                string fileName = Console.ReadLine();
                if (File.Exists(fileName))
                {
                    if (GetValues(fileName, out polinomial))
                    {
                        if (polinomial.IsEmpty) Console.WriteLine("Полином не содержит ни одного члена.");
                        else GetArgs(polinomial, "Введите значение х: ");
                    }
                    else Console.WriteLine("Ошибка при чтении файла.");
                }
                else Console.WriteLine("Файл с заданным именем не существует.");

                Console.WriteLine("Нажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
            }
        }
    }
}
