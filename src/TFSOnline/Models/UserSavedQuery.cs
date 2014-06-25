using System;
using System.ComponentModel.DataAnnotations;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for UserSavedQuery
    /// </summary>
    public class UserSavedQuery
    {
        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string UserId { get; set; }

        [Required]
        public int QueryId { get; set; }
    }
}