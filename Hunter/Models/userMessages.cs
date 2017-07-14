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
    /// <summary>
    /// 解谜统计情况
    /// </summary>
    public class solve  
    {
        /// <summary>
        /// 解谜难度分类
        /// </summary>
        public string difficulty { get; set; }
        /// <summary>
        /// 对应难度的成功/失败情况
        /// </summary>
        public string difficultyScores { get; set; } 
    }

    /// <summary>
    /// 设迷统计情况
    /// </summary>
    public class create
    {
        /// <summary>
        /// 所设迷题标题
        /// </summary>
        public string createTitle { get; set; }
        /// <summary>
        /// 对应谜题被解决成功/失败情况
        /// </summary>
        public string createScores { get; set; }
    }
}
