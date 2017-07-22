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
namespace Hunter.Room
{
    class Proxy
    {
        public static async Task<RootObject[]> GetMission()
        {
            HttpClient h = new HttpClient();
            var kvp = new List<KeyValuePair<string, string>>
                    {

                        new KeyValuePair<string,string>("action", "getmission"),
                    };
            var response = await h.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
            string result = await response.Content.ReadAsStringAsync();
             var s = new DataContractJsonSerializer(typeof(RootObject[]));
             var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
             RootObject[] data =(RootObject[]) s.ReadObject(ms);
            /*JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
            ToJsonMy list = js.Deserialize<ToJsonMy>(json);*/
            return data;
        }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Classes { get; set; }
        [DataMember]
        public string Addr { get; set; }
        [DataMember]
        public string Content1 { get; set; }
        [DataMember]
        public string Content2 { get; set; }
        [DataMember]
        public string Content3 { get; set; }
        [DataMember]
        public object Img1 { get; set; }
        [DataMember]
        public object Img2 { get; set; }
        [DataMember]
        public object Img3 { get; set; }
        [DataMember]
        public string Tips1 { get; set; }
        [DataMember]
        public string Tips2 { get; set; }
        [DataMember]
        public string Tips3 { get; set; }
        [DataMember]
        public string Answer1 { get; set; }
        [DataMember]
        public string Answer2 { get; set; }
        [DataMember]
        public string Answer3 { get; set; }
        [DataMember]
        public string User { get; set; }
    }

    public class MissionManager
    {

        public static ObservableCollection<RootObject> getInstance()
        {
            var lists = new ObservableCollection<RootObject> { };
            return lists;
        }
    }
    public class NowMission
    {
        public static RootObject Task;
        public static RootObject getInstance()
        {
            if (Task == null)
            {
                Task = new RootObject();
            }
            return Task;
        }
    }

}
