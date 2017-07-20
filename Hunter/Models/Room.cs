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



    [DataContract]
    public class RootObject
    {
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string classes { get; set; }
        [DataMember]
        public string addr { get; set; }
        [DataMember]
        public string content1 { get; set; }
        [DataMember]
        public string content2 { get; set; }
        [DataMember]
        public string content3 { get; set; }
        [DataMember]
        public object img1 { get; set; }
        [DataMember]
        public object img2 { get; set; }
        [DataMember]
        public object img3 { get; set; }
        [DataMember]
        public string tips1 { get; set; }
        [DataMember]
        public string tips2 { get; set; }
        [DataMember]
        public string tips3 { get; set; }
        [DataMember]
        public string answer1 { get; set; }
        [DataMember]
        public string answer2 { get; set; }
        [DataMember]
        public string answer3 { get; set; }
        [DataMember]
        public string user { get; set; }
    }
}
