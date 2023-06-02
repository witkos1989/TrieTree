namespace TrieTree;

public interface ITrie
{
    /// <summary>
    /// Inserts new word to Trie Tree
    /// </summary>
    /// <param name="word">Word that will be added</param>
    /// <returns>Instance of Trie Tree</returns>
    ITrie Insert(string word);

    /// <summary>
    /// Searches word in Trie Tree
    /// </summary>
    /// <param name="word">Word that will be searched</param>
    /// <returns>True if Trie Tree contains searched word</returns>
    bool Search(string word);

    /// <summary>
    /// Searches words in Trie Tree that match the word given as a parameter 
    /// </summary>
    /// <param name="needle">Search parameter</param>
    /// <returns>Table of words that matches the paramater</returns>
    IList<string> SearchWords(string word);

    /// <summary>
    /// Searches longest common prefix in Trie Tree
    /// </summary>
    /// <returns>String containing longest common prefix</returns>
    string LongestPrefix();

    /// <summary>
    /// Removes word from Trie Tree
    /// </summary>
    /// <param name="word">Word that will be removed</param>
    /// <returns>String containing letters that was removed from Trie Tree or empty string when word was part of longer word or was not found</returns>
    string Delete(string word);
}