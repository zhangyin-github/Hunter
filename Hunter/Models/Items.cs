using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Models
{
    class Items
    {
        public static async Task<User_ItemList[]> GetItem()
        {
            HttpClient h = new HttpClient();
            var kvp = new List<KeyValuePair<string, string>>
                    {

                        new KeyValuePair<string,string>("action", "getmission"),
                    };
            var response = await h.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
            string result = await response.Content.ReadAsStringAsync();
            var s = new DataContractJsonSerializer(typeof(User_ItemList[]));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            User_ItemList[] data = (User_ItemList[])s.ReadObject(ms);
            /*JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
            ToJsonMy list = js.Deserialize<ToJsonMy>(json);*/
            return data;
        }
    }

    [DataContract]
    public class User_ItemList
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string count { get; set; }
        [DataMember]
        public string content { get; set; }
    }

    public class User_ItemMenager
    {

        public static ObservableCollection<User_ItemList> getInstance()
        {
            var lists = new ObservableCollection<User_ItemList> { };

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
            lists.Add(new Shop_ItemList { name = "双倍经验卡" ,cost=500,content= "一段时间内经验值获取量翻倍（暂不可用）" });
            lists.Add(new Shop_ItemList { name = "跳关卡" ,cost = 1000, content= "跳过此关卡（暂不可用）" });
            lists.Add(new Shop_ItemList { name = "双倍积分卡", cost=500, content= "一段时间内积分值获取量翻倍（暂不可用）" });
            lists.Add(new Shop_ItemList { name = "好人卡" ,cost = 0, content = "假装你是个好人，实际没什么卵用" });
            return lists;
        }
    }
}
