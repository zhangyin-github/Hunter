using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Models
{
    public class userMessages
    {
        public string dickName { get; set; }
        public string ID { get; set; }
        public int rate { get; set; }
        public double score { get; set; }
        public string difficulty { get; set; }
        public int successCount { get; set; }
        public int defeatCount { get; set; }
        public int successTBCount { get; set; }
        public int defeatTBCount { get; set; }
        public string ps { get; set; }

    }

    public class solve
    {
        public string difficulty { get; set; }
        public string difficultyScore { get; set; }
    }
}
