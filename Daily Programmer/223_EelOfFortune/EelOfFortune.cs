using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Programmer._223_EelOfFortune
{
    public class EelOfFortune
    {
        public void GetChallengeResults()
        {
            Console.WriteLine(Problem("synchronized", "snond"));
            Console.WriteLine(Problem("misfunctioned", "snond"));
            Console.WriteLine(Problem("mispronounced", "snond"));
            Console.WriteLine(Problem("shotgunned", "snond"));
            Console.WriteLine(Problem("snond", "snond"));
        }

        public bool Problem(string targetWord, string badWord)
        {
            return new string(
                targetWord.ToCharArray().Where(c => badWord.Contains(c)).ToArray()
                ) == badWord;
        }
    }
}
