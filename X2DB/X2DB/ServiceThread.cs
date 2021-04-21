using NLog;
using System;
using X2DB.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X2DB
{
    public class ServiceThread
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Start()
        {
            string currVersion = "Unknown";
            try
            {
                currVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch { }

            logger.Info(Environment.NewLine + "--- Starting X2DB v.{0} ---{1}", currVersion, Environment.NewLine);
            
            // Thread Spawning Mechanism
            //for(int i = 0; i < 5; i++)
            //{
            //    Logic.Logic.Initialize();
            //}
        }
        public void Stop()
        {
            logger.Info(Environment.NewLine + "--- Stopping X2DB... ---" + Environment.NewLine);
        }
    }
}
