using System.Text;
using ApiAuthors.DTOs;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;

namespace Test;

public class Post_AuthorsEnpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Post_AuthorsEnpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory; 
    }

    [Theory]
    [InlineData("Pepe pipas")]
    [InlineData("Name That Meets The Rule Of Caps But Is Too Long")]
    public async Task Post_Author_BadRequestsTests(string name)
    {
        var client = _factory.CreateClient();
        var httpContent = this.GetHttpContent(name);

        var response = await client.PostAsync("/Api/Author", httpContent); 

        Assert.Equal("BadRequest", response.StatusCode.ToString());   
        client.Dispose();     
    }

    // [Fact]
    // public async Task Post_Author_SuccessTest()
    // {
    //     var client = _factory.CreateClient();
    //     var httpContent = this.GetHttpContent("Kent Beck");

    //     var response = await client.PostAsync("Api/Author", httpContent);
        
    //     var responseContent = await response.Content.ReadAsStringAsync();
    //     Assert.True(
    //         response.StatusCode.ToString() == "OK",
    //         $"Expected status code OK but got {response.StatusCode}, "+
    //         $"Response content: {responseContent}");
    //     client.Dispose();     
    // }

    [Fact]
    public async Task Post_AuthorAlreadyExist_BadRequestTest()
    {
        var client = _factory.CreateClient();
        var httpContent1 = this.GetHttpContent("Robert C. Martin");
        var httpContent2 = this.GetHttpContent("Robert C. Martin");

        var response1 =  await client.PostAsync("Api/Author", httpContent1);
        var response2 =  await client.PostAsync("Api/Author", httpContent2);

        Assert.Equal("BadRequest", response2.StatusCode.ToString());
        client.Dispose();     
    }

    private HttpContent GetHttpContent(string name)
    {
        var author = new AuthorRequestDTO{ Name = name };
        var jsonContent = JsonConvert.SerializeObject(author);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return httpContent;
    }
}
    