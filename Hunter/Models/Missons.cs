using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Models
{
    public class Missons
    {
        string title { get; set; }
        string classes { get; set; }
        string addr { get; set; }
        string content { get; set; }
        string tips { get; set; }
        string answer { get; set; }
        string user { get; set; }

    }
    public class MissonInfo
    {
        public static Missons NewMisson;
        public static Missons getInstance()
        {
            if (NewMisson == null)
            {
                NewMisson = new Missons();
            }
            return NewMisson;
        }
    }
}
