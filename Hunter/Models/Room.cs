using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Models
{
    class Room
    {
    }
    public class MissionList
    {
        public string name { get; set; }
    }

    public class ListManager
    {

        public static List<MissionList> getInstance()
        {
            var lists = new List<MissionList> { };
            lists.Add(new MissionList { name = "新手引导任务" });
            lists.Add(new MissionList { name = "故事模式1" });
            lists.Add(new MissionList { name = "故事模式2" });
            lists.Add(new MissionList { name = "故事模式3" });
            return lists;
        }
    }



   
}
