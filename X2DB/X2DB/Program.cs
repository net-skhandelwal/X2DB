using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X2DB
{
    public static class GeneralSettings
    {
        public static string inputSource = "";
    }
    class Program
    {
        static void Main(string[] args)
        {
            ServiceConfiguration.Configure();
        }
    }
}
