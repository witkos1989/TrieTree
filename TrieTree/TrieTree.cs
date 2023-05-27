﻿using System.Text;

namespace TrieTree
{
    internal record TrieNode
    {
        private readonly int _alphabetSize = 26;

        internal TrieNode[] Children;

        internal char Content;

        internal bool EndOfWord;

        internal TrieNode() =>
            Children = new TrieNode[_alphabetSize];
    }

    public class Trie : ITrie
	{       
        private readonly TrieNode Root;

        public Trie()
        {
            Root = new TrieNode();
        }

        /// <summary>
        /// Inserts new word to Trie Tree
        /// </summary>
        /// <param name="word">Word that will be added</param>
        /// <returns>Instance of Trie Tree</returns>
        public ITrie Insert(string word)
        {
            string lowerWord = word.Trim().ToLower();
            StringBuilder returnedText = new();
            bool properWord = lowerWord.All(char.IsLetter);

            if (!properWord)
            {
                return this;
            }

            TrieNode current = Root;

            for (int i = 0; i < lowerWord.Length; i++)
            {
                int wordIndex = lowerWord[i] - 'a';

                TrieNode? existingNode = current.Children[wordIndex];

                if (existingNode is null)
                {
                    current.Children[wordIndex] = new TrieNode()
                    {
                        Content = lowerWord[i],
                        EndOfWord = false
                    };
                }

                returnedText.Append(current.Children[wordIndex].Content);
                current = current.Children[wordIndex];
            }

            current.EndOfWord = true;

            return this;
        }

        /// <summary>
        /// Searches word in Trie Tree
        /// </summary>
        /// <param name="word">Word that will be searched</param>
        /// <returns>True if Trie Tree contains searched word</returns>
        public bool Search(string word)
        {
            string lowerWord = word.Trim().ToLower();
            TrieNode current = Root;

            for (int i = 0; i < lowerWord.Length; i++)
            {
                bool isLetter = char.IsLetter(lowerWord[i]);

                if (!isLetter)
                {
                    return false;
                }    

                int wordIndex = lowerWord[i] - 'a';

                if (current.Children[wordIndex] is null)
                {
                    return false;
                }

                current = current.Children[wordIndex];
            }

            return current.EndOfWord;
        }

        /// <summary>
        /// Removes word from Trie Tree
        /// </summary>
        /// <param name="word">Word that will be removed</param>
        /// <returns>String containing letters that was removed from Trie Tree or empty string when word was part of longer word or was not found</returns>
        public string Delete(string word)
        {
            string lowerWord = word.Trim().ToLower();
            TrieNode current = Root;
            int length = 0;
            StringBuilder deletedCharacters = new();

            RemoveStep(current, lowerWord, length, deletedCharacters);

            return new string(deletedCharacters.ToString().Reverse().ToArray());
        }

        private void RemoveStep(TrieNode parent, string word, int length, StringBuilder deletedChars)
        {
            int wordIndex = word[length] - 'a';
            TrieNode? child = parent.Children[wordIndex];

            length++;

            if (child is null)
            {
                return;
            }

            if (length == word.Length)
            {
                if (!child.EndOfWord)
                {
                    return;
                }

                if (child.Children.Any(c => c != null))
                {
                    child.EndOfWord = false;
                }
                else
                {
                    deletedChars.Append(child.Content);

                    parent.Children[wordIndex] = null!;
                }

                return;
            }

            RemoveStep(child, word, length, deletedChars);

            if (child.Children.Any(c => c != null) || child.EndOfWord)
            {
                return;
            }
            else
            {
                deletedChars.Append(child.Content);

                parent.Children[wordIndex] = null!;
            }
        }
    }
}