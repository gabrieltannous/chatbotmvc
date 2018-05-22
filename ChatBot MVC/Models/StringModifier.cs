using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatBot_MVC.Models
{
    // a static helper class containing string functions
    public static class StringModifier
    {
        /* unused
        public static string RemovePunctuation(string input)
        {
            // returns a copy of the input string without punctuation (excluding Rule.VariableString)
            return new string(input.Where(c => !char.IsPunctuation(c) || c == Rule.VariableSymbol).ToArray());
        }
        */

        #region Helper methods
        public static string RemovePunctuationAndWhiteSpace(string input)
        {
            // returns a lowercase copy of the input string without whitespace and punctuation
            Regex regex = new Regex("[A-z0-9]");
            var regexResult = regex.Matches(input);
            var formattedRule = "";
            foreach (Match match in regexResult)
            {
                formattedRule += match.Value;
            }
            return formattedRule.ToLower();
        }
        #endregion
    }
}
