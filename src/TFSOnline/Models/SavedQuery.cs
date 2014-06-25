using System;
using System.ComponentModel.DataAnnotations;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for SavedQuery
    /// </summary>
    public class SavedQuery
    {
        [Required]
        public int RecordId { get; set; }

        [Required]
        public int QueryId { get; set; }

        [Required]
        public string QueryField { get; set; }

        [Required]
        public QueryOperator QueryOperator { get; set; }

        [Required]
        public string QueryValue { get; set; }
    }
}