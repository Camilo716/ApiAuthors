
namespace ApiAuthors.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Tittle { get; set; }

    public int AuthorId { get; set; }
    public AuthorModel? Author { get; set; }
}
