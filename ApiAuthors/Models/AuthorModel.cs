namespace ApiAuthors.Models;

public class AuthorModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<BookModel> books { get; set; }
}
