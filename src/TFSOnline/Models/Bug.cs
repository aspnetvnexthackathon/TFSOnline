﻿using System;
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
        public string Title { get; set; }

        [StringLength(160, MinimumLength = 2)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Range(0, 3, ErrorMessage="Priority of the bug has to be with in range 0-3")]
        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        [Display(Name = "Assigned To")]
        public int AssignedTo { get; set; }

        [Required]
        [Display(Name = "Status")]
        public State State { get; set; }
    }
}