using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Models
{
    class Items
    {
    }
    public class User_ItemList
    {
        public string name { get; set; }
    }

    public class User_ItemMenager
    {

        public static List<User_ItemList> getInstance()
        {
            var lists = new List<User_ItemList> { };
            lists.Add(new User_ItemList { name = "改名卡" });
            lists.Add(new User_ItemList { name = "双倍经验卡" });
            lists.Add(new User_ItemList { name = "提示卡" });
            lists.Add(new User_ItemList { name = "好人卡" });
            lists.Add(new User_ItemList { name = "好人卡" });
            lists.Add(new User_ItemList { name = "好人卡" });
            lists.Add(new User_ItemList { name = "好人卡" });
            lists.Add(new User_ItemList { name = "好人卡" });
            return lists;
        }
    }
    public class Shop_ItemList
    {
        public string name { get; set; }
        public string img { get; set; }
        public int cost { get; set; }
        public string content { get; set; }
    }

    public class Shop_ItemMenager
    {

        public static List<Shop_ItemList> getInstance()
        {
            var lists = new List<Shop_ItemList> { };
            lists.Add(new Shop_ItemList { name = "提示卡" ,cost=100,content="获取一次提示的机会（暂不可用）"});
            lists.Add(new Shop_ItemList { name = "双倍经验药水" ,cost=500,content= "一段时间内经验值获取量翻倍（暂不可用）" });
            lists.Add(new Shop_ItemList { name = "跳关卡" ,cost = 1000, content= "跳过此关卡（暂不可用）" });
            lists.Add(new Shop_ItemList { name = "双倍积分药水", cost=500, content= "一段时间内积分值获取量翻倍（暂不可用）" });
            lists.Add(new Shop_ItemList { name = "好人卡" ,cost = 0, content = "假装你是个好人，实际没什么卵用" });
            return lists;
        }
    }
}
