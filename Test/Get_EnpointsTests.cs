
using System.Text;
using ApiAuthors.DTOs;
using Newtonsoft.Json;

namespace Test;

public class Get_EnpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Get_EnpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory; 
    }

    [Theory]
    [InlineData("/Api/Book/1")]
    [InlineData("/Api/Author")]
    [InlineData("/Api/Author/first")]
    [InlineData("/Api/Author/3")]
    [InlineData("/Api/Author/Camilo")]
    [InlineData("/Api/Book/1/Comment")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }
}