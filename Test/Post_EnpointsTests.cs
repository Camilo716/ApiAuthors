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

    [Theory]
    [InlineData("Camilo gonzalez")]
    [InlineData("Name That Meets The Rule Of Caps But Is Too Long")]
    public async Task Post_AuthorBadRequests(string name)
    {
        var client = _factory.CreateClient();
        var author = new AuthorRequestDTO { Name = name };
        var jsonContent = JsonConvert.SerializeObject(author);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/Api/Author", httpContent); 

        Assert.Equal("BadRequest", response.StatusCode.ToString());        
    }
}
    