using System.ComponentModel.DataAnnotations;
using ApiAuthors.Validations;

namespace ApiAuthors.Models;

public class AuthorModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(maximumLength: 40)]
    [CapitalizedWords]
    public string Name { get; set; }
    public List<BookModel> Books { get; set; }
}
