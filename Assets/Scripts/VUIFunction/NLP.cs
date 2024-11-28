using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace NLP
{
    public static class NLP
    {
        public static Dictionary<string, string> verbDictionary = new Dictionary<string, string>()
        {
            {"set","set" },
            {"adjust", "set"},
            {"change", "set"},
            {"increase", "increase"},
            {"raise", "increase"},
            {"decrease", "decrease"},
            {"lower", "decrease"}
        };

        public static List<string> volumeTypes = new List<string> { "Master", "Sound", "Music" };

        public static string percent = "percentage";

        //Analyze Sentence
        public static (string verb, string noun, int number, bool ByPercent) AnalyzeSentence(string sentence)
        {
            string verb = string.Empty;
            string noun = string.Empty;
            int number = -1;
            bool byPercent = false;

            sentence = sentence.ToLower();

            // get number
            Match numberMatch = Regex.Match(sentence, @"\d+");
            if (numberMatch.Success)
            {
                number = int.Parse(numberMatch.Value);
            }

            // get verb
            foreach (var kvp in verbDictionary)
            {
                if (sentence.Contains(kvp.Key))
                {
                    verb = kvp.Value;
                    break;
                }
            }

            // get noun
            foreach (string type in volumeTypes)
            {
                if (sentence.Contains(type.ToLower()))
                {
                    noun = type;
                    break;
                }
            }

            // check if it is by percentage or not
            if (sentence.Contains(percent))
            {
                byPercent = true;
            }

            return (verb, noun, number, byPercent);
        }

    }
}

