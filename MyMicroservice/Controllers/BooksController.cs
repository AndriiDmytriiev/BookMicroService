using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using WebApplication6.Model;

namespace MyMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", PublicationDate = new DateTime(2022, 1, 1) },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", PublicationDate = new DateTime(2022, 2, 1) }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            book.Id = _books.Count + 1;
            _books.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book updatedBook)
        {
            var book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublicationDate = updatedBook.PublicationDate;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _books.Find(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _books.Remove(book);
            return NoContent();
        }
    }
}

