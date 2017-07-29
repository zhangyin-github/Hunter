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

            Story.Add(new RootObject
            {
                Title = "第一章",
                Classes = "惊悚",
                Addr = "校园",
                Content1 = "女孩睁开惺忪的双眼，发现自己以前睡过得椅子上，回过头发现魏少辉倒吊在图书馆大厅满脸是伤。“啊....... ，怎么会？魏少辉？魏少辉....不要吓我啊！......没有呼吸和脉搏...死掉了？怎么会？刚才还在聊天呢......天啊，我才刚认识你而已！”\n 我应该先冷静下来，对了......之前他不是去管理室打电话吗，现在去管理室看看，发现管理室的门紧锁着，需要一把钥匙将其打开，旁边的保健室门开着，进去后发现里面的视力表，似乎有些不同。",
                Tips1 = "视力表可以点亮",
                Answer1 = "钥匙",
                Content2 = "进入管理室后，电话正在响，“说不定是有回应了。”接起了电话。“喂？我是实验中学的学生，学校里发生命案了，快点来人。”电话里一阵嘈杂声，并不能听清具体声音，里面似乎有一个断断续续的声音说：“我在三楼辅导室等你......”“到底是什么人，为什么知道电话是我接的。”\n 在去辅导室的路上，经过了厕所，“平时有压力时厕所总是冷静一下的地方，今天只觉得很阴森，嗯......？水槽里卡了几个白色的小东西。”走近一看，发现是三个骰子，出了厕所，发现通往三楼的门被铁丝缠住了，去工作间看看有没有什么东西能帮到忙。\n “....嗯？这是什么，门被油漆画着符咒一样的东西，奇怪，门把转的动，但是推不开，....该不会是符咒的原因吧？仓库里应该有什么东西能把这个东西洗掉吧。”",
                Tips2 = "能洗掉油漆的东西",
                Answer2 = "松香水",
                Content3 = "拿着松香水洗掉了符咒，进入工友伯伯的工作间，桌子里应该有什么东西能把铁丝夹断。\n 夹断了铁丝，上了三楼发现辅导室的路被一个门隔开了。",
                Tips3 = "",
                Answer3 = "铁钳",
                Img1 = "",
                Img2 = "",
                Img3 = ""
            });

            Story.Add(new RootObject
            {
                Title = "第二章",
                Classes = "惊悚",
                Addr = "校园",
                Content1 = "走到通往辅导室的教室，黑板上贴着一张公告，屋内只有一张课桌。王欣走近课桌，“这间教室只有这张课桌椅，给人一种孤单的感觉。”桌面上有一些暗褐色的印子，还有一些刻痕.....像文字又像图案.....光这样看不出所以然。桌角放置着一个碗，想拿却拿不起来，应该是需要往里面扔东西。",
                Tips1 = "王欣身上有的东西",
                Answer1 = "骰子",
                Content2 = "把骰子置到碗里去，“哇，这是什么！牙齿？我不是丢骰子进去了吗，上边有点数....不可能吧...我明明很确定”“这个声音，碗松动了，现在应该可以拿起来了。”板凳上出现一张纸条，是一张图文插画：割喉。上边写着：呜呼哀哉童男逝，以此良机成科仪，刎颈取血钵碗回，乾坤卦象藏其中。按照图的指示，接着应该怎么做呢?",
                Tips2 = "图的内容",
                Answer2 = "割喉取血",
                Content3 = "到图书馆取到魏少辉的血后，回到三楼的教室，把血撒到课桌上，血渗进了桌子上的刻痕，用刚才捡到的插画把桌子上的刻痕印下来，拓印下来的图案写着数字和易经的卦象，来到辅导室的门前，锁上边是八卦卦象，如何排列卦象才能把门打开呢？",
                Tips3 = "图的内容",
                Answer3 = "坎兑離",
                Img1 = "·",
                Img2 = "",
                Img3 = ""
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
