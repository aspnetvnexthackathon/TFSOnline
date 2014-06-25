using System;
using System.ComponentModel.DataAnnotations;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for Announcement
    /// </summary>
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Message { get; set; }
    }
}