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
    using System.Linq;
    using System.Collections.Generic;
    
    public partial class Reaction
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public string Action { get; set; }
        public string Mood { get; set; }
        public string Emoji { get; set; }
        
        #region Public methods
        public string Lookup(string searchData, string searchField, string returnField)
        {
            // checks if the search field contains the search data and returns the return field data, no match returns null
            if (searchData != null)// && searchData.Equals(GetByField(searchField), StringComparison.CurrentCultureIgnoreCase))
            {
                DefaultConnectionEntities db = new DefaultConnectionEntities();
                Reaction reaction;
                switch (searchField)
                {
                    case "Object":
                        reaction = (from u in db.Reactions where u.Object == searchData select u).FirstOrDefault();
                    break;
                    case "Mood":
                        reaction = (from u in db.Reactions where u.Mood == searchData select u).FirstOrDefault();
                        break;
                    case "Action":
                        reaction = (from u in db.Reactions where u.Action == searchData select u).FirstOrDefault();
                        break;
                    case "Emoji":
                        reaction = (from u in db.Reactions where u.Emoji == searchData select u).FirstOrDefault();
                        break;
                    default:
                        return null;   
                }
                return reaction.GetByField(returnField);
            }
            return null;
        }

        public string GetByField(string field)
        {
            // additional getter for Object, Action, Mood, Emoji
            switch (field)
            {
                case "Object":
                    return Object;
                case "Action":
                    return Action;
                case "Mood":
                    return Mood;
                case "Emoji":
                    return Emoji;
                default:
                    return null;
            }
        }

        #endregion

        #region Comparison methods
        public override bool Equals(object obj)
        {
            if (obj is Reaction)
            {
                var dr = obj as Reaction;
                return (dr.Object == Object && dr.Action == Action && dr.Mood == Mood && dr.Emoji == Emoji);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Object.GetHashCode() + Action.GetHashCode() + Mood.GetHashCode() + Emoji.GetHashCode();
        }
        #endregion
    }
}
