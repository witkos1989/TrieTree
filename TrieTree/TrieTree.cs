using System.Text;

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

            if (!IsFound(lowerWord, ref current))
            {
                return false;
            }

            return current.EndOfWord;
        }

        /// <summary>
        /// Searches words in Trie Tree that match the word given as a parameter 
        /// </summary>
        /// <param name="needle">Search parameter</param>
        /// <returns>Table of words that matches the paramater</returns>
        public IList<string> SearchWords(string needle)
        {
            string lowerWord = needle.Trim().ToLower();
            TrieNode current = Root;
            List<string> words = new();
            StringBuilder wordBuilder = new();

            if (!IsFound(lowerWord, ref current))
            {
                return words;
            }

            if (current.EndOfWord)
            {
                words.Add(lowerWord);
            }

            wordBuilder.Append(lowerWord);

            SearchWordWalk(current, words, wordBuilder);

            return words;
        }

        /// <summary>
        /// Searches longest common prefix in Trie Tree
        /// </summary>
        /// <returns>String containing longest common prefix</returns>
        public string LongestPrefix()
        {
            StringBuilder prefix = new StringBuilder();
            TrieNode current = Root;

            while (true)
            {
                int childrenCount = current.Children.Count(c => c is not null);

                if (childrenCount == 0 || childrenCount > 1)
                {
                    return prefix.ToString();
                }

                TrieNode child = current.Children.First(c => c is not null);

                prefix.Append(child.Content);

                if (child.EndOfWord)
                {
                    return prefix.ToString();
                }

                current = child;
            }
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

        private static bool IsFound(string word, ref TrieNode node)
        {
            for (int i = 0; i < word.Length; i++)
            {
                bool isLetter = char.IsLetter(word[i]);

                if (!isLetter)
                {
                    return false;
                }

                int wordIndex = word[i] - 'a';

                if (node.Children[wordIndex] is null)
                {
                    return false;
                }

                node = node.Children[wordIndex];
            }

            return true;
        }

        private void SearchWordWalk(TrieNode node, List<string> words, StringBuilder word)
        {
            foreach (TrieNode children in node.Children)
            {
                if (children is null)
                {
                    continue;
                }

                word.Append(children.Content);

                if (children.EndOfWord)
                {
                    words.Add(word.ToString());
                }

                SearchWordWalk(children, words, word);

                word.Remove(word.Length - 1, 1);
            }
        }
    }
}