using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using BOR.Models;

namespace BOR.ViewModels
{
    public class MediaVotingResults
    {
        public MediaVotingResults()
        {
            Results = new Dictionary<Medium, int>();
        }
        public Dictionary<Medium, int> Results { get; set; }

    }
}