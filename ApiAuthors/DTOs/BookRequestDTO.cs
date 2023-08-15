using System.ComponentModel.DataAnnotations;
using ApiAuthors.Models;

namespace ApiAuthors.DTOs;

public class BookRequestDTO
{
    [Required]
    [StringLength(maximumLength: 250)]
    public string Tittle { get; set; }
} 