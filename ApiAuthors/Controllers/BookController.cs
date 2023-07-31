using ApiAuthors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookModel>> GetById(int id)
    {
        var book =  await _context.books.Include(b => b.author).FirstOrDefaultAsync(b => b.Id == id);
        return book == null ? NotFound() : book;
    }

    [HttpPost]
    public async Task<ActionResult> Post(BookModel book)
    { 
        var authorExist = await _context.authors.AnyAsync(a => a.Id == book.authorId);

        if(!authorExist)
            return BadRequest($"Author with id {book.authorId} doesn't exist");


        _context.Add(book);
        await _context.SaveChangesAsync();
        return Ok();
        

        
    } 
}