using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_MVC.Models
{
    // an object representing a set of rule summary statistics
    public class Summary
    {
        public string Editor { get; set; } = "";
        public int ApprovedCount { get; set; } = 0;
        public int RejectedCount { get; set; } = 0;
        public int PendingCount { get; set; } = 0;

        #region Convenience properties
        public double? SuccessRate
        {
            get
            {
                if (ApprovedCount + RejectedCount == 0)
                {
                    return null;
                }
                else
                {
                    return Math.Round(100.0 * ApprovedCount / (ApprovedCount + RejectedCount), 1);
                }
            }
        }
        public string SuccessString
        {
            get
            {
                return (SuccessRate != null) ? (SuccessRate + "%") : "N/A"; // compiler is smart enough to not evaluate twice on the same line
            }
        }
        #endregion
    }
}
