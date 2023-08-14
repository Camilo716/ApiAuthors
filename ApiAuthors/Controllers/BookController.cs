using ApiAuthors.DTOs;
using ApiAuthors.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

    public BookController(ApplicationDbContext context, IMapper mapper) 
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookModel>> GetById(int id)
    {
        var book =  await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        return book == null ? NotFound() : book;
    }

    [HttpPost]
    public async Task<ActionResult> Post(BookRequestDTO bookDto)
    { 
        var book = _mapper.Map<BookModel>(bookDto);
        _context.Add(book);
        await _context.SaveChangesAsync();
        return Ok();
    } 
}
