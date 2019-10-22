
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class SearchHistory
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string SearchTerm { get; set; }
    }
}
