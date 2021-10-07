using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibraryNET;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationClass.Managers
{
    public class BookManager
    {
        private static List<Book> _books = new List<Book>()
        {
                new Book("Antic Hay", "Aldous Huxley", 300, 0),
                new Book("As I Lay Dying", "William Faulkner", 350, 1),
                new Book("Death Be Not Proud", "John Gunther", 400, 2),
                new Book("If Not Now, When?", "Primo Levi", 450,3)
        };

        public List<Book> GetAll()
        {
            return new List<Book>(_books);
        }

        public ActionResult<Book> GetById(int ibs)
        {
            Book book = _books.FirstOrDefault(s => s.ISBN13 == ibs);
            return book;
        }

        public Book UpdateBook(Book book)
        {
            var result = _books.FirstOrDefault(s => s.ISBN13 == book.ISBN13);

            if (result is not null)
            {
                result.Title = book.Title;
                result.Author = book.Author;
                result.PageNumber = book.PageNumber;

                return new Book();
            }
            return null;
        }

        public Book AddBook(Book boook)
        {
            bool valid = true;
            foreach (Book item in _books)
            {
                if (item.ISBN13 == boook.ISBN13)
                { 
                    valid = false;
                    return null;
                }
            }
            if (valid)
            {
                Book book = new Book();
                _books.Add(book);
                return book;
            }

            return null;

        }

        public Book DeleteBook(int ibs)
        {
            var result = _books.FirstOrDefault(s => s.ISBN13 == ibs);
            if (result is not null)
            {
                _books.Remove(result);
                return result;
            }

            return null;
        }


       


    }
}
