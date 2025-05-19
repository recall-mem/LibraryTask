using System;
using System.Data.Common;
using System.Dynamic;
using Task.models;

namespace Task.services
{
    public class Library
    {
        private int _id_counter = 0;
        public string Name { get; set; }
        private Book[] _books { get; set; }
        public int Capacity { get; }
        private int size { get; set; }

        public Library(string name, int Capacity)
        {
            size = 0;
            this.Name = name;
            this.Capacity = Capacity;
            _books = new Book[Capacity];
        }


        public void AddBook()
        {
            if (size == Capacity)
            {
                throw new Exception("Library is full, Remove to add new books");
            }

            string Name, AuthorName;
            double Price;

            Console.Write("AuthorName: ");
            AuthorName = Console.ReadLine();
            if (AuthorName == string.Empty)
            {
                throw new Exception("AuthorName Required!");
            }

            Console.Write("Book Name: ");
            Name = Console.ReadLine();
            if (Name == string.Empty)
            {
                throw new Exception("Name Required!");
            }

            Console.Write("Price: ");
            Price = double.Parse(Console.ReadLine());
            if (Price < 0)
            {
                throw new Exception("Price must be positive");
            }

            InsetBook(
                new Book()
                {
                    Name = Name,
                    AuthorName = AuthorName,
                    Price = Price
                }
            );
        }

        public Book GetBookById()
        {
            if (size == 0)
            {
                throw new Exception("There are no books in the library");
            }

            Console.Write("Book ID: ");
            int id = int.Parse(Console.ReadLine());

            foreach (var book in _books)
            {
                if (book is null)
                {
                    continue;
                }
                else if (book.Id == id)
                {
                    return book;
                }
            }

            return null;
        }

        public void RemoveBook()
        {
            if (size == 0)
            {
                throw new Exception("There are no books in the library");
            }
            else
            {
                Console.Write("Book ID: ");
                int BookId = int.Parse(Console.ReadLine());

                if (BookId >= _id_counter)
                {
                    throw new Exception($"Book {BookId}: Not found");
                }
                else if (BookId < 0)
                {
                    throw new Exception($"ID can't be negative");
                }

                for (int n = 0; n < Capacity; n++)
                {
                    if (_books[n].Id == BookId)
                    {
                        _books[n] = null;
                        size--;
                        Console.WriteLine($"Book {BookId} Removed!");
                        return;
                    }
                }
                throw new Exception($"Book {BookId}: Not found");
            }

        }// DeleteBook

        public Book[] GetBook()
        {
            if (size == 0)
            {
                throw new Exception("There are no books in the library");
            }
            Console.Write("Book Name: ");
            string Name = Console.ReadLine();

            Book[] Books = new Book[0];

            foreach (var book in _books)
            {
                if (book is null)
                {
                    continue;
                }
                else if (book.Name == Name)
                {
                    Array.Resize(ref Books, Books.Length + 1);
                    Books[^1] = book;
                }
            }
            return Books;
        }

        public Book[] GetAllBooks()
        {
            if (size == 0)
            {
                throw new Exception("There are no books in the library");
            }
            return _books;
        }

        public void PrintBooks(Book[] books)
        {
            if (books.Length == 0)
            {
                Console.WriteLine("No Books");
                return;
            }

            foreach (var book in books)
            {
                book.ShowInfo();
            }
        }

        public void UpdateBook()
        {
            if (size == 0)
            {
                throw new Exception("There are no books in the library");
            }

            Console.Write("Book ID to update: ");
            int id = int.Parse(Console.ReadLine());

            Book bookToUpdate = GetBookByIdById(id);
            if (bookToUpdate == null)
            {
                throw new Exception($"Book {id} was not found");
            }

            Console.WriteLine("Leave empty to save current value");

            Console.Write($"Current Name ({bookToUpdate.Name}): ");
            string newName = Console.ReadLine();
            if (newName != string.Empty)
            {
                bookToUpdate.Name = newName;
            }

            Console.Write($"Current AuthorName ({bookToUpdate.AuthorName}): ");
            string newAuthor = Console.ReadLine();
            if (newAuthor != string.Empty)
            {
                bookToUpdate.AuthorName = newAuthor;
            }

            Console.Write($"Current Price ({bookToUpdate.Price}): ");
            string newPrice= Console.ReadLine();
            if (newPrice != string.Empty)
            {
                bookToUpdate.Price = double.Parse(newPrice);           
            }

            Console.WriteLine($"Book {id} updated!");
        }

        // Yardımcı metod (ben ekledim) — id ile kitap buluyor:
        private Book GetBookByIdById(int id)
        {
            foreach (var book in _books)
            {
                if (book != null && book.Id == id)
                {
                    return book;
                }
            }
            return null;
        }


        private void InsetBook(Book book)
        {
            for (int n = 0; n < Capacity; n++)
            {
                if (_books[n] == null)
                {
                    _books[n] = book;
                    book.Id = _id_counter++;
                    size++;
                    Console.WriteLine("Added");
                    return;
                }
            }
        }// InsetBook

        public static void PrintMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Get book by id");
            Console.WriteLine("3. Remove book");
            Console.WriteLine("4. Update book");
            Console.WriteLine("5. Get all books");
            Console.WriteLine("0. Quit");
        }// PrintMenu

        public void run()
        {

            char opt;

            while (true)
            {
                PrintMenu();

                Console.Write("Option: ");
                opt = Console.ReadLine()[0];

                switch (opt)
                {
                    case '1':
                        Console.WriteLine("Adding book...");
                        try
                        {
                            AddBook();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case '2':
                        Console.WriteLine("Getting book by ID...");
                        try
                        {
                            GetBookById().ShowInfo();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case '3':
                        Console.WriteLine("Removing book...");
                        try
                        {
                            RemoveBook();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case '4':
                        Console.WriteLine("Updating book...");
                        UpdateBook();
                        break;
                    case '5':
                        Console.WriteLine("Getting all books...");
                        try
                        {
                            PrintBooks(GetAllBooks());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case '0':
                        Console.WriteLine("Quitting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

        }// RUN 



    }// Class

}// Namespace