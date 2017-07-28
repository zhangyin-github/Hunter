using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Models
{
    public class link
    {
        public string ip;
    }

    public class getlink {
        public static link Ip;
        public static link getInstance()
        {
            if(Ip==null)
            {
                Ip = new link();
                Ip.ip = "http://qwq.itbears.club/hunter.php";
            }

            return Ip;
        }
    }
}
