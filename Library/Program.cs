using Library.Models;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        LibraryRepository repository = new LibraryRepository();
        bool exitProgram = false;

        do
        {
            Console.WriteLine("\nLibrary Management System");
            Console.WriteLine("\nPlease select an option from the list below");
            Console.WriteLine("\n");
            Console.WriteLine("1. Books");
            Console.WriteLine("2. Members");
            Console.WriteLine("3. Exit");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        ManageBooks(repository);
                        break;
                    case 2:
                        ManageMembers(repository);
                        break;
                    case 3:
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (!exitProgram);
    }

    static void ManageBooks(LibraryRepository repository)
    {
        bool exitBooksMenu = false;

        do
        {
            Console.WriteLine("\nBooks Menu");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View Books");
            Console.WriteLine("3. Loan Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Search Books by Genre");
            Console.WriteLine("6. Back to Main Menu");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddBook(repository);
                        break;
                    case 2:
                        ViewBooks(repository);
                        break;
                    case 3:
                        LoanBook(repository);
                        break;
                    case 4:
                        ReturnBook(repository);
                        break;
                    case 5:
                        SearchBooksByGenre(repository);
                        break;
                    case 6:
                        exitBooksMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (!exitBooksMenu);
    }

    static void ManageMembers(LibraryRepository repository)
    {
        bool exitMembersMenu = false;

        do
        {
            Console.WriteLine("\nMembers Menu");
            Console.WriteLine("1. Add Member");
            Console.WriteLine("2. View All Members");
            Console.WriteLine("3. Remove Member");
            Console.WriteLine("4. Back to Main Menu");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddMember(repository);
                        break;
                    case 2:
                        ViewAllMembers(repository);
                        break;
                    case 3:
                        RemoveMember(repository);
                        break;
                    case 4:
                        exitMembersMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (!exitMembersMenu);
    }

    static void AddBook(LibraryRepository repository)
    {
        Console.WriteLine("\nEnter Book Title:");
        string title = Console.ReadLine();
        Console.WriteLine("Enter Book Author:");
        string author = Console.ReadLine();
        Console.WriteLine("Enter Book ISBN:");
        string isbn = Console.ReadLine();

        Book book = new Book
        {
            Title = title,
            Author = author,
            ISBN = isbn,
            IsAvailable = true
        };

        repository.AddBook(book);
        Console.WriteLine("Book added successfully!");
    }

    static void ViewBooks(LibraryRepository repository)
    {
        var books = repository.GetBooks().ToList();

        if (!books.Any())
        {
            Console.WriteLine("\nNo books available.");
        }
        else
        {
            Console.WriteLine("\nList of Books:");
            foreach (var book in books)
            {
                string availableStatus = book.IsAvailable.HasValue ? (book.IsAvailable.Value ? "Available" : "Not Available") : "Unknown";
                Console.WriteLine($"ID: {book.BookID}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Available: {availableStatus}");
            }
        }
    }

    static void LoanBook(LibraryRepository repository)
    {
        var availableBooks = repository.GetBooks().Where(b => b.IsAvailable == true || b.IsAvailable == null).ToList();

        if (availableBooks.Count == 0)
        {
            Console.WriteLine("No books available to loan.");
            return;
        }

        Console.WriteLine("\nAvailable Books:");
        for (int i = 0; i < availableBooks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableBooks[i].Title} by {availableBooks[i].Author} (ID: {availableBooks[i].BookID})");
        }

        Console.WriteLine("Enter the number of the book to loan:");
        if (int.TryParse(Console.ReadLine(), out int bookChoice))
        {
            if (bookChoice >= 1 && bookChoice <= availableBooks.Count)
            {
                int bookId = availableBooks[bookChoice - 1].BookID;

                Console.WriteLine("Enter Member ID who is borrowing:");
                if (int.TryParse(Console.ReadLine(), out int memberId))
                {
                    repository.LoanBook(bookId, memberId);
                    Console.WriteLine("Loan granted!");
                }
                else
                {
                    Console.WriteLine("Invalid Member ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid book number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void ReturnBook(LibraryRepository repository)
    {
        Console.WriteLine("\nEnter Member ID:");
        if (int.TryParse(Console.ReadLine(), out int memberId))
        {
            repository.ReturnBook(memberId); // Call with memberId instead of bookId
        }
        else
        {
            Console.WriteLine("Invalid Member ID.");
        }
    }

    static void AddMember(LibraryRepository repository)
    {
        Console.WriteLine("\nEnter Member Name:");
        string name = Console.ReadLine();
        Console.WriteLine("Enter Member Email:");
        string email = Console.ReadLine();

        repository.AddMember(name, email);
    }

    static void ViewAllMembers(LibraryRepository repository)
    {
        var members = repository.GetAllMembers();

        if (!members.Any())
        {
            Console.WriteLine("\nNo members found.");
        }
        else
        {
            Console.WriteLine("\nList of Members:");
            foreach (var member in members)
            {
                Console.WriteLine($"ID: {member.MemberID}, Name: {member.Name}, Email: {member.Email}");
            }
        }
    }

    static void RemoveMember(LibraryRepository repository)
    {
        var members = repository.GetAllMembers();

        if (!members.Any())
        {
            Console.WriteLine("\nNo members found.");
            return;
        }

        Console.WriteLine("\nSelect a member to remove:");

        foreach (var member in members)
        {
            Console.WriteLine($"ID: {member.MemberID}, Name: {member.Name}, Email: {member.Email}");
        }

        Console.WriteLine("\nEnter Member ID to remove:");
        if (int.TryParse(Console.ReadLine(), out int memberId))
        {
            repository.RemoveMember(memberId);
        }
        else
        {
            Console.WriteLine("Invalid Member ID.");
        }
    }

    static void SearchBooksByGenre(LibraryRepository repository)
    {
        Console.WriteLine("\nSelect a genre to search books:");
        Console.WriteLine("1. Fantasy");
        Console.WriteLine("2. Science Fiction");
        Console.WriteLine("3. Mystery");
        Console.WriteLine("4. Romance");
        Console.WriteLine("5. Thriller");

        int genreChoice;
        if (int.TryParse(Console.ReadLine(), out genreChoice))
        {
            string genreName = GetGenreName(genreChoice);

            if (genreName != null)
            {
                var books = repository.GetBooksByGenre(genreName).ToList();

                if (books == null || !books.Any())
                {
                    Console.WriteLine($"\nNo books found in the {genreName} genre.");
                }
                else
                {
                    Console.WriteLine($"\nBooks in the {genreName} genre:");
                    foreach (var book in books)
                    {
                        string availableStatus = book.IsAvailable.HasValue ? (book.IsAvailable.Value ? "Available" : "Not Available") : "Unknown";
                        Console.WriteLine($"ID: {book.BookID}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Available: {availableStatus}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid genre choice.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static string GetGenreName(int genreChoice)
    {
        switch (genreChoice)
        {
            case 1:
                return "Fantasy";
            case 2:
                return "Science Fiction";
            case 3:
                return "Mystery";
            case 4:
                return "Romance";
            case 5:
                return "Thriller";
            default:
                return null;
        }
    }
}
