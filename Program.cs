using System;
using Task.services;

namespace Task
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.Write("Library Name: ");
            string name = Console.ReadLine();

            Console.Write("Book capacity: ");
            int cap = int.Parse(Console.ReadLine());

            Library Lib = new Library(name, cap);

            Lib.run();
        }
    }
}