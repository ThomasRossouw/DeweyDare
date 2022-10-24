using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_ST10121910
{
    internal class Dictionary
    {
        //Dictionary to store the categories and numbers of the dewey decimal system.
        public Dictionary<string, string> DeweyDictionary = new Dictionary<string, string>() {
            {"000", "General Knowledge"},
            {"100", "Philosophy & Phsychology"},
            {"200", "Religion"},
            {"300", "Social Science"},
            {"400", "Languages"},
            {"500", "Science"},
            {"600", "Technology"},
            {"700", "Arts & Recreation"},
            {"800", "Literature"},
            {"900", "History & Geography"}
        };
    }
}
