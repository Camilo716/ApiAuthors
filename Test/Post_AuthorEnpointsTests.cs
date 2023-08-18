using System.Text;
using ApiAuthors.DTOs;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using Test.Helpers;

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
        var httpContent = Utilities.GetAuthorHttpContent(name);

        try
        {
            var response = await client.PostAsync("/Api/Author", httpContent); 

            Assert.Equal("BadRequest", response.StatusCode.ToString());   
        }
        finally
        {
            await Utilities.CleanDatabase(_factory);
        }
    }

    [Fact]
    public async Task Post_Author_SuccessTest()
    {
        await Utilities.CleanDatabase(_factory);
        var client = _factory.CreateClient();
        var httpContent = Utilities.GetAuthorHttpContent("Kent Beck");

        try
        {
            var response = await client.PostAsync("Api/Author", httpContent);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.True(
                response.StatusCode.ToString() == "OK",
                $"Expected status code OK but got {response.StatusCode}, "+
                $"Response content: {responseContent}");
        }
        finally
        {
            await Utilities.CleanDatabase(_factory);
        }
    }

    [Fact]
    public async Task Post_AuthorAlreadyExist_BadRequestTest()
    {
        await Utilities.CleanDatabase(_factory);
        var client = _factory.CreateClient();
        var httpContent1 = Utilities.GetAuthorHttpContent("Robert C. Martin");
        var httpContent2 = Utilities.GetAuthorHttpContent("Robert C. Martin");

        try
        {
            var response1 =  await client.PostAsync("Api/Author", httpContent1);
            var response2 =  await client.PostAsync("Api/Author", httpContent2);
            Assert.Equal("BadRequest", response2.StatusCode.ToString());
        }
        finally
        {
            await Utilities.CleanDatabase(_factory);
        }
    }
}
    