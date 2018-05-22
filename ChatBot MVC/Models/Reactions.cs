using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_MVC.Models
{
    // a collection of DataRow objects
    public class Reactions
    {
        private List<Reaction> rowList = new List<Reaction>();

        #region Processing methods
        public string Lookup(string searchData, string searchField, string returnField)
        {
            // checks each row's search field for the search data and returns the return field data, no match returns null
            string foundData;
            foreach (var row in rowList)
            {
                if ((foundData = row.Lookup(searchData, searchField, returnField)) != null)
                {
                    return foundData;
                }
            }
            return null;
        }
        #endregion

        #region Collection methods
        public void Add(Reaction row)
        {
            // add non-duplicate DataRow
            if (row == null) throw new ArgumentNullException();
            if (rowList.Any(r => r.Object == row.Object)) throw new ArgumentException("Duplicate object exists.");
            if (rowList.Any(r => r.Action == row.Action)) throw new ArgumentException("Duplicate action exists.");
            if (rowList.Any(r => r.Mood == row.Mood)) throw new ArgumentException("Duplicate mood exists.");
            if (rowList.Any(r => r.Emoji == row.Emoji)) throw new ArgumentException("Duplicate emoji exists.");
            rowList.Add(row);
        }

        public void ValidateEdit(Reaction row)
        {
            // confirm edited row doesn't match any values in already existing rows
            if (row == null) throw new ArgumentNullException();
            if (rowList.Any(r => r.Object == row.Object && r.Id != row.Id)) throw new ArgumentException("Duplicate object exists.");
            if (rowList.Any(r => r.Action == row.Action && r.Id != row.Id)) throw new ArgumentException("Duplicate action exists.");
            if (rowList.Any(r => r.Mood == row.Mood && r.Id != row.Id)) throw new ArgumentException("Duplicate mood exists.");
            if (rowList.Any(r => r.Emoji == row.Emoji && r.Id != row.Id)) throw new ArgumentException("Duplicate emoji exists.");
        }

        public void Remove(Reaction row)
        {
            // removes a DataRow
            if (row == null) throw new ArgumentNullException();
            if (!rowList.Remove(row)) throw new ArgumentException("Row not found.");
        }

        public Reaction GetDataRowById(int id)
        {
            // return DataRow specified by id
            return (from row in rowList
                    where row.Id.Equals(id)
                    select row).FirstOrDefault(); // returns null if not found
        }

        public ReadOnlyCollection<Reaction> GetList()
        {
            // returns a read only collection so that specific Add/Remove methods must be called, however individual DataRow objects in this collection are mutable
            return rowList.AsReadOnly();
        }
        #endregion
    }
}
