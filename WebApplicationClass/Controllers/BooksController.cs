using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibraryNET;
using Microsoft.AspNetCore.Http;
using WebApplicationClass.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookManager _manager = new BookManager();
        // GET: api/Books
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _manager.GetAll();
        }

        // GET api/Books/0 or 1 or 2 or 3
        [HttpGet("{ibs:int}")]
        public async Task<ActionResult<Book>> GetBook(int ibs)
        {
            try
            {
                var result = _manager.GetById(ibs);

                if (result is null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<Book>> CreateBook(Book book)
        //{
        //    try
        //    {
        //        if (book is null)
        //        {
        //            return BadRequest();
        //        }
        //        return new ActionResult<Book>(book);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database");
        //    }
        //}

        [HttpPut("{ibs:int}")]
        public async Task<ActionResult<Book>> UpdateBook(int ibs, Book book)
        {
            try
            {
                if (ibs != book.ISBN13)
                {
                    return BadRequest("Book ID mismatch");
                }

                var bookUpdate = _manager.GetById(ibs);
                if (bookUpdate is null)
                {
                    return NotFound($"Book with Id = {ibs} not found");
                }

                return _manager.UpdateBook(book);
                return null;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] Book book)
        {
            

        }


        // DELETE api/<BooksController>/5
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest();
                } 
                return _manager.AddBook(book);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        // DELETE api/<BooksController>/5
        [HttpDelete("{ibs:int}")]
        public async Task<ActionResult<Book>> DeleteBook(int ibs)
        {
            try
            {
                var deleteBook = _manager.GetById(ibs);
                if (deleteBook is null)
                {
                    return NotFound($"Book with Id = {ibs} not found");
                }

                return _manager.DeleteBook(ibs);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
