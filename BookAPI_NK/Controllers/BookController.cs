using BookAPI_NK.Context;
using BookAPI_NK.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookAPI_NK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(ApplicationDbContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Read), new { id = book.Id}, book);
        }

        [HttpGet("all")]
        public async Task<IActionResult> ReadAll()
        {
            var books = await context.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Read(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (book is null) { return NotFound(); }
            return Ok(book);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Book updatedBook)
        {
            var bookToUpdate = await context.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (bookToUpdate is null) { return NotFound(); }
            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.Author = updatedBook.Author;
            bookToUpdate.Genre = updatedBook.Genre;
            bookToUpdate.PublishedDate = updatedBook.PublishedDate;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await context.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (bookToDelete is null) { return NotFound(); }

            context.Books.Remove(bookToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
