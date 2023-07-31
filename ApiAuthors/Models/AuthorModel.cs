using System.Text.Json.Serialization;

namespace ApiAuthors.Models;

public class AuthorModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public List<BookModel> books { get; set; }
}
