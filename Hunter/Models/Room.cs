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
    public class NowList
    {
        public string Title { get; set; }
        
        public string Classes { get; set; }
        
        public string Addr { get; set; }
       
        public string Content1 { get; set; }
       
        public string Content2 { get; set; }
    
        public string Content3 { get; set; }
 
        public object Img1 { get; set; }

        public object Img2 { get; set; }
    
        public object Img3 { get; set; }
  
        public string Tips1 { get; set; }

        public string Tips2 { get; set; }
    
        public string Tips3 { get; set; }
    
        public string Answer1 { get; set; }
  
        public string Answer2 { get; set; }

        public string Answer3 { get; set; }
  
        public string User { get; set; }
    }

    public class ListManager
    {

        public static List<NowList> getInstance()
        {
            var lists = new List<NowList> { };
            return lists;
        }
    }



   
}
