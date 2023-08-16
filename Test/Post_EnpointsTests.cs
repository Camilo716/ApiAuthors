using System.Text;
using ApiAuthors.DTOs;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
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
    [InlineData("Martin fowler")]
    [InlineData("Name That Meets The Rule Of Caps But Is Too Long")]
    public async Task Post_AuthorBadRequestsTest(string name)
    {
        var client = _factory.CreateClient();
        var author = new AuthorRequestDTO { Name = name };
        var jsonContent = JsonConvert.SerializeObject(author);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/Api/Author", httpContent); 

        Assert.Equal("BadRequest", response.StatusCode.ToString());        
    }

    [Fact]
    public async Task Post_AuthorSuccessRequestTest()
    {
        var client = _factory.CreateClient();
        var author = new AuthorRequestDTO{ Name = "Martin Fowler" };
        var jsonContent = JsonConvert.SerializeObject(author);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("Api/Author", httpContent);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(
            response.StatusCode.ToString() == "OK",
            $"Expected status code OK but got {response.StatusCode}, "+
            $"Response content: {responseContent}");
    }
}
    