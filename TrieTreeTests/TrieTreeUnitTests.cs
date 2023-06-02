using Xunit;
using TrieTree;

namespace TrieTreeTests;

public class TrieTests
{
    private readonly ITrie _sut;

    public TrieTests()
    {
        _sut = new Trie();
    }

    [Theory]
    [InlineData("Cat")]
    [InlineData("Dog")]
    [InlineData("Doggo")]
    [InlineData("cat")]
    public void InsertAndSearch_ShouldAddWordToTree_WhenCallingFunctionWithStringThatContainsOnlyLetters(string value)
    {
        _sut.Insert(value);

        Assert.True(_sut.Search(value));
    }

    [Theory]
    [InlineData("Prince Charles")]
    [InlineData("360 NoScope")]
    [InlineData("McDonald's")]
    public void InsertAndSearch_ShouldNotAddWordToTree_WhenCallingFunctionWithStringThatContainsNotOnlyLetters(string value)
    {
        _sut.Insert(value);

        Assert.False(_sut.Search(value));
    }

    [Fact]
    public void InsertAndSearch_ShouldReturnFalse_WhenSearchedWordDoesntHaveEndOfWordParamaterSetToTrue()
    {
        _sut.Insert("Elizabeth")
            .Insert("Horseradish");

        Assert.False(_sut.Search("Eliza"));

        Assert.False(_sut.Search("Horse"));
    }

    [Fact]
    public void InsertAndSearch_ShouldReturnTrue_WhenShorterWordWasAddedAfterLongerWord()
    {
        _sut.Insert("Elizabeth")
            .Insert("Horseradish")
            .Insert("Eliza")
            .Insert("Horse");

        Assert.True(_sut.Search("Eliza"));

        Assert.True(_sut.Search("Horse"));
    }

    [Fact]
    public void Search_ShouldReturnFalse_WhenWordWasntFound()
    {
        _sut.Insert("Fire")
            .Insert("Water")
            .Insert("Earthquake");

        Assert.False(_sut.Search("Waterloo"));

        Assert.False(_sut.Search("Earth"));

        Assert.True(_sut.Search("Fire"));

        Assert.True(_sut.Search("Water"));

        Assert.True(_sut.Search("Earthquake"));
    }

    [Fact]
    public void Delete_ShouldReturnDeletedWord_WhenTreeNodesContainsOnlyOneChild()
    {
        _sut.Insert("Fire")
            .Insert("Water")
            .Insert("Earth");

        string wholeWord = _sut.Delete("Water");

        Assert.Equal("water", wholeWord);

        Assert.False(_sut.Search("Water"));
    }

    [Fact]
    public void Delete_ShouldReturnPartOfDeletedWord_WhenTreeNodesContainsMoreThanOneChild()
    {
        _sut.Insert("Fire")
            .Insert("Water")
            .Insert("Earth")
            .Insert("Fireplace")
            .Insert("Warframe")
            .Insert("Watcher");

        string partOfWord = _sut.Delete("Watcher");

        Assert.Equal("cher", partOfWord);

        Assert.False(_sut.Search("Watcher"));
    }

    [Fact]
    public void Delete_ShouldReturnEmptyString_WhenDeletedWordIsPartOfLonderWord()
    {
        _sut.Insert("Fire")
            .Insert("Water")
            .Insert("Earth")
            .Insert("Earthquake");

        string deletedWord = _sut.Delete("Earth");

        Assert.Equal("", deletedWord);

        Assert.False(_sut.Search("Earth"));
    }

    [Fact]
    public void Delete_ShouldReturnEmptyString_WhenWordWasntFound()
    {
        _sut.Insert("Fire")
            .Insert("Water")
            .Insert("Earth");

        string deletedWord = _sut.Delete("Waterloo");

        Assert.Equal("", deletedWord);

        Assert.False(_sut.Search("Waterloo"));

        Assert.True(_sut.Search("Water"));

        Assert.True(_sut.Search("Fire"));

        Assert.True(_sut.Search("Earth"));
    }

    [Fact]
    public void LongestPrefix_ShouldReturnEmptyString_WhenRootContainsMoreThanOneChildren()
    {
        _sut.Insert("Fire")
            .Insert("Firefighter")
            .Insert("Fireblaze")
            .Insert("Earthquake");

        string longestPrefix = _sut.LongestPrefix();

        Assert.Equal("", longestPrefix);
    }

    [Fact]
    public void LongestPrefix_ShouldReturnEmptyString_WhenTrieTreeIsEmpty()
    {
        string longestPrefix = _sut.LongestPrefix();

        Assert.Equal("", longestPrefix);
    }

    [Fact]
    public void LongestPrefix_ShouldReturnCommonPrefix_WhenTreeContainsWordWithTheSameBeginning()
    {
        _sut.Insert("Wood")
            .Insert("Woodcutter")
            .Insert("Woodchopper");

        string longestPrefix = _sut.LongestPrefix();

        Assert.Equal("wood", longestPrefix);

        _sut.Insert("Would");

        longestPrefix = _sut.LongestPrefix();

        Assert.Equal("wo", longestPrefix);

        _sut.Insert("Write");

        longestPrefix = _sut.LongestPrefix();

        Assert.Equal("w", longestPrefix);

        _sut.Delete("Write");

        longestPrefix = _sut.LongestPrefix();

        Assert.Equal("wo", longestPrefix);
    }

    [Fact]
    public void SearchWords_ShouldReturnListOfWords_WhenTreeContainWordsThatMatchesNeedle()
    {
        _sut.Insert("Wait")
            .Insert("What")
            .Insert("Watts")
            .Insert("Waterfall");

        IList<string> words = new List<string>() { "wait", "waterfall", "watts" }; 

        IList<string> result = _sut.SearchWords("wa");

        Assert.Equal(3, result.Count);

        Assert.True(Enumerable.SequenceEqual(words, result));

        result = _sut.SearchWords("what");

        Assert.Equal(1, result.Count);

        Assert.Equal("what", result[0]);
    }

    [Fact]
    public void SearchWords_ShouldReturnEmptyList_WhenTreeDoesntContainWordsThatMatchesNeedle()
    {
        _sut.Insert("Wait")
            .Insert("What")
            .Insert("Watts")
            .Insert("Waterfall");

        IList<string> result = _sut.SearchWords("when");

        Assert.Empty(result);
    }
}