using System.Text;
using ApiAuthors.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;

namespace Test;

public class Post_CommentEndpointsTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Post_CommentEndpointsTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory; 
    }

    [Fact]
    public async Task Post_CreateCommentWhichBookIdDontExist_NotFoundTest()
    {
        var client = _factory.CreateClient();
        var httpContent = GetCommentHttpContent("Awesome book");

        var response = await client.PostAsync("/Api/Book/99999/comment", httpContent);

        Assert.Equal("NotFound", response.StatusCode.ToString());
    }

    private HttpContent GetCommentHttpContent(string content)
    {
        var author = new CommentRequestDTO{ Content = content};
        var jsonContent = JsonConvert.SerializeObject(author);
        HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        return httpContent;
    }
}