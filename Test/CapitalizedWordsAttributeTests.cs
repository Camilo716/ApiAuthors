namespace Test;

using System.ComponentModel.DataAnnotations;
using ApiAuthors.Validations;

public class CapitalizedWordsAttributeTests
{
    [Fact]
    public void EachWordMustStartWithCapsTest()
    {
        var capsValidator = new CapitalizedWordsAttribute();

        Assert.Throws<ValidationException>(() =>
            capsValidator.Validate("Camilo gonzalez", ""));
    }

    [Fact]
    public void ValidWordsStartingWithCapsTest()
    {
        var capsValidator = new CapitalizedWordsAttribute();

        try
        {
            capsValidator.Validate("Camilo Gonzalez", "");
            Assert.True(true);
        }
        catch (ValidationException ex)
        {
            Assert.True(false, $"Throws an unexpected expection: {ex}");
        }
    }
}