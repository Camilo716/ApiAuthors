namespace ApiAuthors.Models;

public class CommentModel
{
    public int Id { get; set; }
    public string Content { get; set; }

    public int bookId { get; set; }
    public BookModel book { get; set; }
}