using System;
using System.Collections.Generic;

namespace TFSOnline
{
    /// <summary>
    /// Summary description for BugsViewModel
    /// </summary>
    public class BugsViewModel
    {
        public BugsViewModel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int TotalWorkItemsCount { get; set; }

        public int ResolvedWorkItemsCount { get; set; }

        public List<UserSavedQuery> SavedQueries { get; set; }
    }
}