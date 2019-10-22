using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class VideoData
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string VideoURL { get; set; }
        public decimal Duration { get; set; }
        public string DurationString { get; set; }

        public VideoData()
        {
            DurationString = " ";
        }
    }
}




