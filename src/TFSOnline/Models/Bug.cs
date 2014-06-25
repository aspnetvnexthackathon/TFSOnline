using System;
using System.ComponentModel.DataAnnotations;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for Bug
    /// </summary>
    public class Bug
    {
        [Required]
        public int BugId { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        [Display(Name = "Title")]
        public string BugTitle { get; set; }

        [StringLength(160, MinimumLength = 2)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Range(0, 3, ErrorMessage="Priority of the bug has to be with in range 0-3")]
        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }

        [Required]
        [Display(Name = "Status")]
        public BugState State { get; set; }
    }
}