using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace X2DB.Logic
{
    public static class Logic
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Thread InitializeThread;

        public static void Initialize()
        {
            InitializeThread = new Thread(x => 
            {
                // Thread Logic Implementation
                //for(int i = 0; i < 1000; i++)
                //{
                //    logger.Info($"Thread Id: {Thread.CurrentThread.ManagedThreadId} - Counter: {i+1}");
                //    Thread.Sleep(1000);
                //}
            });
            InitializeThread.IsBackground = true;
            InitializeThread.Start();
            logger.Info("Initialize thread is started.");
        }
    }
}
