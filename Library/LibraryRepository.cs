using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class LibraryRepository
{
    private readonly LibraryContext _context;

    public LibraryRepository()
    {
        _context = new LibraryContext();
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void AddMember(string name, string email)
    {
        Member member = new Member { Name = name, Email = email };
        _context.Members.Add(member);
        _context.SaveChanges();
    }

    public IQueryable<Book> GetBooks()
    {
        return _context.Books;
    }

    public IQueryable<Book> GetBooksByGenre(string genreName)
    {
        return _context.Books
                       .Include(b => b.Genre) // Ensure Genre is included
                       .Where(b => b.Genre.Name == genreName);
    }

    public void LoanBook(int bookId, int memberId)
    {
        Loan loan = new Loan
        {
            BookID = bookId,
            MemberID = memberId,
            LoanDate = DateTime.Today,
            ReturnDate = null
        };

        _context.Loans.Add(loan);

        var book = _context.Books.FirstOrDefault(b => b.BookID == bookId);
        if (book != null)
        {
            book.IsAvailable = false;
        }

        _context.SaveChanges();
    }

    public void ReturnBook(int memberId)
    {
        var member = _context.Members.FirstOrDefault(m => m.MemberID == memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        var loans = _context.Loans
                            .Where(l => l.MemberID == memberId && l.ReturnDate == null)
                            .ToList();

        if (loans.Count == 0)
        {
            Console.WriteLine("No active loans found for this member.");
            return;
        }

        Console.WriteLine($"\nBooks for Member {member.Name} (ID: {memberId}):");
        foreach (var loan in loans)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookID == loan.BookID);
            if (book != null)
            {
                Console.WriteLine($"ID: {book.BookID}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}");
            }
        }

        Console.WriteLine("\nEnter Book ID to return:");
        if (int.TryParse(Console.ReadLine(), out int bookId))
        {
            var loan = loans.FirstOrDefault(l => l.BookID == bookId);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Today;

                var book = _context.Books.FirstOrDefault(b => b.BookID == bookId);
                if (book != null)
                {
                    book.IsAvailable = true;
                }

                _context.SaveChanges();
                Console.WriteLine("Book returned successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Book ID or the book is not loaned by this member.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    public List<Member> GetAllMembers()
    {
        return _context.Members.ToList();
    }

    // Method to remove a member by ID
    public void RemoveMember(int memberId)
    {
        var member = _context.Members.FirstOrDefault(m => m.MemberID == memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        _context.Members.Remove(member);
        _context.SaveChanges();
        Console.WriteLine($"Member {member.Name} (ID: {memberId}) has been removed.");
    }
}
