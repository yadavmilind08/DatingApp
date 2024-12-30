using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class Test
    {
        string name = "Hi milind welcome to cloud raptor";

        static char? GetSecondNonRepeatedCharacter(string str)
        {

            Dictionary<char, int> charCount = new Dictionary<char, int>();
            foreach (char c in str)
            {
                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
                else
                {
                    charCount[c] = 1;
                }
            }

            int nonRepeatedCount = 0;

            foreach (char c in str)
            {
                if (charCount[c] == 1)
                {
                    nonRepeatedCount++;

                    if (nonRepeatedCount == 2)
                    {
                        return c;
                    }
                }
            }

            return null;
        }
    }
}