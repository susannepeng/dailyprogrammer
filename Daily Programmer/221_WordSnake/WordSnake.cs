using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Programmer._221_WordSnake
{
    public class WordSnake
    {
        public void GetChallengeResults()
        {
            var wordSnake = new _221_WordSnake.WordSnake();
            wordSnake.PrintSnake("SHENANIGANS SALTY YOUNGSTER ROUND DOUBLET TERABYTE ESSENCE");
            Console.WriteLine();
            wordSnake.PrintSnake("CAN NINCOMPOOP PANTS SCRIMSHAW WASTELAND DIRK KOMBAT TEMP PLUNGE ESTER REGRET TOMBOY");
            Console.WriteLine();
            wordSnake.PrintSnake("NICKEL LEDERHOSEN NARCOTRAFFICANTE EAT TO OATS SOUP PAST TELEMARKETER RUST THINGAMAJIG GROSS SALTPETER REISSUE ELEPHANTITIS");
        }

        public void PrintSnake(string input)
        {
            foreach(string row in GetSnake(input))
            {
                Console.WriteLine(row);
            }
        }

        public string[] GetSnake(string input)
        {
            string[] words = GetWords(input);
            return GetRows(words.Skip(1).ToArray(), new string[] { words[0] });
        }

        string[] GetWords(string input)
        {
            return input.Split(' ');
        }

        string[] GetRows(string[] words, string[] result)
        {
            if (words.Length == 0) return result;

            var word = words[0];
            var lastWord = result[result.Length - 1];
            var lastWordDirection = lastWord.ToCharArray().Count(c => c != ' ') > 1 ? WordDirection.Right : WordDirection.Down;
            var newWordDirection = lastWordDirection == WordDirection.Down ? WordDirection.Right : WordDirection.Down;

            return GetRows(words.Skip(1).ToArray(), AddWord(newWordDirection, word, result));
        }

        string[] AddWord(WordDirection direction, string word, string[] result)
        {
            if (direction == WordDirection.Down) return AddWordDown(word, result);
            else return AddWordRight(word, result);
        }

        string[] AddWordDown(string word, string[] result)
        {
            int startIndex = Array.FindLastIndex(result.Last().ToCharArray(), (c => c != ' '));
            List<string> newRows = new List<string>();
            foreach (char c in word.ToCharArray().Skip(1).ToArray())
            {
                var newLine = Enumerable.Repeat(' ', startIndex + 1).ToList();
                newLine[newLine.Count - 1] = c;

                newRows.Add(new string(newLine.ToArray()));
            }
            return result.Concat(newRows).ToArray();
        }

        string[] AddWordRight(string word, string[] result)
        {
            var tail = word.ToCharArray().Skip(1).ToArray();
            var newLine = new string(result.Last().ToCharArray()) + new string(tail);
            result[result.Length - 1] = newLine;
            return result;
        }
    }

    enum WordDirection
    {
        Right,
        Down
    }
}
