
namespace ApiAuthors.Models;

public class BookModel
{
    public int Id { get; set; }
    public string title { get; set; }

    public int authorId { get; set; }
    public AuthorModel? author { get; set; }
}
