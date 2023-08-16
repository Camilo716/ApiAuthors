using ApiAuthors.DTOs;
using ApiAuthors.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("api/book/{bookId:int}/[Controller]")]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CommentController(ApplicationDbContext context, IMapper mapper) 
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentResponseDTO>>> Get()
    {
        var comments =  await _context.Comments.ToListAsync();
        var commentDto = _mapper.Map<List<CommentResponseDTO>>(comments);
        return Ok(commentDto);
    }
}