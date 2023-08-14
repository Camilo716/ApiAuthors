using System.ComponentModel.DataAnnotations;
using ApiAuthors.Validations;

namespace ApiAuthors.DTOs;

public class AuthorRequestDTO
{
    [Required]
    [StringLength(maximumLength: 40)]
    [CapitalizedWords]
    public string Name { get; set; }
} 