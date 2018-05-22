//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatBot_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Rule
    {
        public static char VariableSymbol = '%'; // used to indicate variable position in data-driven rule

        public int Id { get; set; }
        public string RuleString { get; set; }
        public string ResponseString { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string RuleField { get; set; }
        public string ResponseField { get; set; }
        public string LastEditor { get; set; }

        #region Convenience properties
        public string Status
        {
            // "Approved" as a human readable string
            get
            {
                if (Approved == null) return "Pending";
                return (bool)Approved ? "Approved" : "Rejected";
            }
        }
        #endregion

        #region Public methods
        public string Match(string inputtedText)
        {
            // checks for match and returns the matched variable string or "" if no variable string, no match returns null
            string cleanText = StringModifier.RemovePunctuationAndWhiteSpace(inputtedText);
            string[] sections = RuleString.Split(VariableSymbol);
            string[] cleanSections = sections.Select(s => StringModifier.RemovePunctuationAndWhiteSpace(s)).ToArray<string>();
            if (cleanSections.Length == 1)
            {
                // no variable symbol, fixed rule
                if (cleanText.Equals(cleanSections[0]))
                {
                    return "";
                }
            }
            else
            {
                // variable section, data-driven rule
                if (cleanText.StartsWith(cleanSections[0]) && cleanText.EndsWith(cleanSections[1]))
                {
                    return cleanText.Substring(cleanSections[0].Length, cleanText.Length - cleanSections[0].Length - cleanSections[1].Length);
                }
            }
            // no match
            return null;
        }

        public string Respond(string matchedData)
        {
            // evaluates the response to this rule with the given matched data
            return ResponseString.Replace(VariableSymbol.ToString(), matchedData);
        }
        #endregion

        
    }
}
