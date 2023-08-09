using System.ComponentModel.DataAnnotations;
using ApiAuthors.Validations;

namespace ApiAuthors.DTOs;

public class AuthorDTO
{
    [Required]
    [StringLength(maximumLength: 40)]
    [CapitalizedWords]
    public string Name { get; set; }
}