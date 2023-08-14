using ApiAuthors.DTOs;
using ApiAuthors.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AuthorController(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuthorDTO>>> Get()
    {
        var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
        var authorsDto = _mapper.Map<List<AuthorDTO>>(authors);
        return authorsDto;
    }

    [HttpGet("first")]
    public async Task<ActionResult<AuthorDTO>> GetFirst()
    {   
        var author =  await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync();

        if (author is null)
            return NotFound();

        var authorsDto =  _mapper.Map<AuthorDTO>(author);
        return authorsDto;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorModel>> GetById(int id)
    {
        var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);

        if(author is null)
            return NotFound();

        return author;
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<List<AuthorDTO>>> GetByName(string name)
    {
        var authors = await _context.Authors
                            .Include(a => a.Books)
                            .Where(a => a.Name.Contains(name))
                            .ToListAsync();
        if (authors is null)
            return NotFound($"There is not coincidences for name {name}");

        var authorsDto = _mapper.Map<List<AuthorDTO>>(authors);
        return authorsDto;
    }

    [HttpPost] 
    public async Task<ActionResult> Post([FromBody] AuthorDTO authorDTO)
    {
        bool existAnAuthorWithTheSameName = await _context.Authors.AnyAsync(a => a.Name == authorDTO.Name);

        if (existAnAuthorWithTheSameName)
            return BadRequest($"Already exist an author with the name {authorDTO.Name}"); 

        var author = _mapper.Map<AuthorModel>(authorDTO);
        _context.Add(author);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put([FromBody] AuthorModel author, int id)
    {
        if (author.Id != id)
            return BadRequest("Author's doesn't match with URL's id");

        bool exist = await _context.Authors.AnyAsync(a => a.Id == id);
        if (!exist)
        {
            return NotFound();
        } 

        _context.Update(author);
        await _context.SaveChangesAsync();
        return Ok();        
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool exist = await _context.Authors.AnyAsync(a => a.Id == id);
        if (!exist)
        {
            return NotFound();
        } 

        AuthorModel author = _context.Authors.Find(id)!;
        _context.Remove(author);
        await _context.SaveChangesAsync();
        return Ok();
    }
}

