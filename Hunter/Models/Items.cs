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
            lists.Add(new User_ItemList { name = "好人卡" });
            return lists;
        }
    }
}
