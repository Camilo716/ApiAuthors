using System.Text;
using ApiAuthors.DTOs;
using Newtonsoft.Json;

namespace Test;

public class Post_EnpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Post_EnpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory; 
    }

    [Fact]
    public async Task Post_NotAllNamesWordsInCaps_BadRequest()
    {
        var client = _factory.CreateClient();
        var author = new AuthorRequestDTO { Name = "Camilo gonzalez" };

        var jsonContent = JsonConvert.SerializeObject(author);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/Api/Author", httpContent); 
        Assert.Equal("BadRequest", response.StatusCode.ToString());        
    }
}
    