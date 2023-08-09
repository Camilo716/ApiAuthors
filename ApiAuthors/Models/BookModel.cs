
using System.ComponentModel.DataAnnotations;

namespace ApiAuthors.Models;

public class BookModel
{
    public int Id { get; set; }

    [StringLength(maximumLength: 250)]
    public string Tittle { get; set; }

    public int AuthorId { get; set; }
    public AuthorModel? Author { get; set; }
}
