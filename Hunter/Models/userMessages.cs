using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Models
{
    /// <summary>
    /// 玩家信息
    /// </summary>
    public class userMessages
    {
        /// <summary>
        /// 玩家昵称dickName
        /// </summary>
        public string dickName { get; set; }
        /// <summary>
        /// 玩家账号ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 玩家等级rate
        /// </summary>
        public int rate { get; set; }
        /// <summary>
        /// 玩家积分score
        /// </summary>
        public double score { get; set; }
        /// <summary>
        /// 玩家游戏宣言ps
        /// </summary>
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
