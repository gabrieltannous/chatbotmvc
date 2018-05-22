using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_MVC.Models
{
    // a collection of Rule objects
    public class Rules
    {
        public Reaction DataSource = new Reaction();
        private List<Rule> ruleList = new List<Rule>();

        #region Reporting methods
        public Summary Summarize() // american spelling used for language consistency (eg. Color, Serializable)
        {
            // returns a Summary object of all rules
            return new Summary
            {
                ApprovedCount = ruleList.Count<Rule>(r => r.Approved == true),
                RejectedCount = ruleList.Count<Rule>(r => r.Approved == false),
                PendingCount = ruleList.Count<Rule>(r => r.Approved == null)
            };
        }

        public Dictionary<String, Summary> SummarizeByEditor() // american spelling used for language consistency (eg. Color, Serializable)
        {
            // returns a Summary object of all rules grouped by LastEditor
            Dictionary<String, Summary> dictionary = new Dictionary<string, Summary>();
            Summary s;
            foreach (Rule rule in ruleList)
            {
                if (!dictionary.ContainsKey(rule.LastEditor))
                {
                    dictionary.Add(rule.LastEditor, new Summary {
                        Editor = rule.LastEditor
                    });
                }
                s = dictionary[rule.LastEditor];
                if (rule.Approved == true)
                {
                    s.ApprovedCount++;
                }
                else if (rule.Approved == false)
                {
                    s.RejectedCount++;
                }
                else
                {
                    s.PendingCount++;
                }
            }
            return dictionary;
        }
        #endregion

        #region Processing methods
        public string RespondToInput(string inputtedText, List<Rule> ruleList)
        {
            // returns response if there is a match, returns null if there is none
            var approvedRules = ruleList.Where(r => r.Approved == true);
            string matchedText;
            foreach(Rule rule in approvedRules)
            {
                matchedText = rule.Match(inputtedText);
                if(matchedText != null)
                {
                    // rule matched
                    if (rule.RuleField == null || rule.ResponseField == null) return rule.Respond("");
                    string matchedData = DataSource.Lookup(matchedText, rule.RuleField, rule.ResponseField);
                    if (matchedData != null) return rule.Respond(matchedData);
                }
            }
            // no match
            return null;
        }
        #endregion

        #region Collection methods
        public void Add(Rule rule)
        {
            // add rule if it doesn't already match existing
            if (rule == null) throw new ArgumentNullException();
            if (ruleList.Any(r => r.Match(rule.RuleString) != null)) throw new ArgumentException("Duplicate rule exists.");
            ruleList.Add(rule);
        }

        public void ValidateEdit(Rule rule)
        {
            // confirm edited rule doesn't match already existing rule (excluding itself)
            if (rule == null) throw new ArgumentNullException();
            if (ruleList.Any(r => r.Match(rule.RuleString) != null && r.Id != rule.Id)) throw new ArgumentException("Duplicate rule exists.");
        }

        public void Remove(Rule rule)
        {
            // removes a rule if rule exists
            if (rule == null) throw new ArgumentNullException();
            if (!ruleList.Remove(rule)) throw new ArgumentException("Rule not in collection.");
        }

        public Rule GetRuleById(int id)
        {
            // return Rule specified by id
            return (from rule in ruleList
                    where rule.Id.Equals(id)
                    select rule).FirstOrDefault<Rule>(); // returns null if not found
        }

        public ReadOnlyCollection<Rule> GetList()
        {
            // returns a read only collection so that specific Add/Remove methods must be called, however individual Rule objects in this collection are mutable
            return ruleList.AsReadOnly();
        }
        #endregion
    }
}