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
            RootObject[] data = (RootObject[])s.ReadObject(ms);
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
        public string Img1 { get; set; }
        [DataMember]
        public string Img2 { get; set; }
        [DataMember]
        public string Img3 { get; set; }
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

    public class StoryMission
    {
        public static ObservableCollection<RootObject> Story;
        public static ObservableCollection<RootObject> getInstance()
        {
            Story = new ObservableCollection<RootObject> { };
            Story.Add(new RootObject {
                Title = "Liberation序章",
                Classes = "惊悚",
                Addr = "校园",
                Content1 = "一所高中，下午第一节历史课，伴着殷老师的讲课声，魏少辉渐渐进入梦乡，一觉醒来，发现教室空无一人，天已经黑了，外面下着暴风雨，黑板上写着：“台风警告”。\n  “怎么睡到现在，赶快回家吧！”。\n  魏少辉跑到楼下发现楼下的大门锁住了，旁边的工作间里应该有什么东西能打开门。",
                Tips1 = "工作间的柜子里",
                Answer1 = "钥匙",
                Content2 = "门开了，回家最快的路是从图书馆，魏少辉顶着暴雨快速跑到图书馆，发现大厅的中央睡着一个女孩，穿着学生服，面容清秀。\n  魏少辉走上前去，推醒女孩：“同学，醒醒，台风要来了！”\n  女孩睁开双眼问：“这是哪？”\n  “你睡在大厅中央，学校已经放学，台风快来了刚快回家吧。”\n “嗯...等等我的项链不见了。”\n  “你还记得之前在做什么吗？”\n  “我刚才一直在图书馆三楼看《构建之法》。”\n  “那我们一起找找吧！”“可是我不记得在图书馆的哪个位置了，怎么办？”",
                Tips2 = "图书馆图书查询系统",
                Answer2 = "C区1号书架",
                Content3 = "魏少辉在C区1号书架前停了下来，搜寻一番发现项链掉到了缝隙中。\n  “太里面了，够不到啊，附近应该有什么东西可以取出来吧。”\n\n  取到项链后，魏少辉把项链还给了女孩，然后两个人一起走向校外，走到大门时，发现门锁着，警务室空无一人。 \n  女孩问：“怎么办？”先回教室吧，这样淋雨也不是办法，于是两个人一起回到教室，两个人交流一番，得知女孩叫王欣。\n  魏少辉说：“我到楼下的管理室打了电话，看看能不能联系到别人”。他的脚步声轻声回荡，勾起心中一丝悬念，时间戛然停止，世界分崩离析，转眼间，虚无已将我吞噬。",
                Tips3 = "图书馆的清洁房",
                Answer3 = "扫把",
                Img1="",
                Img2="",
                Img3=""
            });
            
            return Story;
        }
    }

    public class User_Answer
    {
        public string answer { get; set; }
        public int time { get; set; }
        public int show { get; set; }
    }
    public class UserAnswer
    {
        public static User_Answer Answer;
        public static User_Answer getInstance()
        {
            if (Answer == null)
            {
                Answer = new User_Answer();
                Answer.time = 1;
            }
            return Answer;
        }
    }
}
