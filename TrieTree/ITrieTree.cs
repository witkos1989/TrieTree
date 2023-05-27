﻿namespace TrieTree;

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
    /// Removes word from Trie Tree
    /// </summary>
    /// <param name="word">Word that will be removed</param>
    /// <returns>String containing letters that was removed from Trie Tree or empty string when word was part of longer word or was not found</returns>
    string Delete(string word);
}