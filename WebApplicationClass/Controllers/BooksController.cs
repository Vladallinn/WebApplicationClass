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
        private readonly BookManager _manager;

        public BooksController(BookManager manager)
        {
            _manager = manager;
        }
        
        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                return Ok(await _manager.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error with data from the database");
            }
            
        }

        //GET api/Books/0 or 1 or 2 or 3
        [HttpGet("{ibs:int}")]
        public async Task<ActionResult<Book>> GetBookById(int ibs)
        {
            try
            {
                var result = await _manager.GetById(ibs);

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

        [HttpPost("")]
        public void CreateBook([FromBody]Book book)
        {
            try
            {
                if (book is null)
                { 
                    BadRequest();
                }
                _manager.AddBook(book);

            
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut("{ibs:int}")]
        public async Task<ActionResult<Book>> UpdateBook(int ibs, Book book)
        {
            try
            {
                if (ibs != book.ISBN13)
                {
                    return BadRequest("Book ISBN mismatch");
                }
                var bookUpdate = await _manager.GetById(ibs);
                if (bookUpdate is null)
                {
                    return NotFound($"Book with Id = {ibs} not found");
                }

                return await _manager.UpdateBook(book);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        //DELETE api/<BooksController>/5
        [HttpDelete("{ibs:int}")]
        public async Task<ActionResult<Book>> DeleteBook(int ibs)
        {
            try
            {
                var deleteBook = await _manager.GetById(ibs);
                if (deleteBook is null)
                {
                    return NotFound($"Book with Id = {ibs} not found");
                }

                return await _manager.DeleteBook(ibs);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
