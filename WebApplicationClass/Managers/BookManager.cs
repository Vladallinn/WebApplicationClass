using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibraryNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationClass.Managers
{
    public class BookManager
    {
        //private static List<Book> _books = new List<Book>()
        //{
        //        new Book("Antic Hay", "Aldous Huxley", 300, 0),
        //        new Book("As I Lay Dying", "William Faulkner", 350, 1),
        //        new Book("Death Be Not Proud", "John Gunther", 400, 2),
        //        new Book("If Not Now, When?", "Primo Levi", 450,3)
        //};
        private readonly AppDbContext appDbContext;

        public BookManager(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await appDbContext.Books.ToListAsync();
        }

        public async Task<Book> GetById(int ibs)
        {
            return await appDbContext.Books.FirstOrDefaultAsync(s => s.ISBN13 == ibs);
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var result = await appDbContext.Books.FirstOrDefaultAsync(s => s.ISBN13 == book.ISBN13);

            if (result is not null)
            {
                result.Title = book.Title;
                result.Author = book.Author;
                result.PageNumber = book.PageNumber;

                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        public void AddBook(Book book)
        { 
            appDbContext.Books.Add(book);
            appDbContext.SaveChanges();
        }

        public async Task<Book> DeleteBook(int ibs)
        {
            var result = await appDbContext.Books.FirstOrDefaultAsync(s => s.ISBN13 == ibs);
            if (result is not null)
            {
                appDbContext.Books.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
            return null;
        }


       


    }
}
