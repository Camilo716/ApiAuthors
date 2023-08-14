using System.ComponentModel.DataAnnotations;
using ApiAuthors.Models;

namespace ApiAuthors.DTOs;

public class BookRequestDTO
{
    [StringLength(maximumLength: 250)]
    public string Tittle { get; set; }
} 