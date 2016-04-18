using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BOR.Models;

namespace BOR.ViewModels
{
    public class RankingPartsVotingResults
    {
        public RankingPartsVotingResults()
        {
            Results = new Dictionary<Article, int>();
        }
        public Dictionary<Article, int> Results { get; set; }
    }
}