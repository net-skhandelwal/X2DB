using NLog;
using System;
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

            try
            {
                GeneralSettings.inputSource = Properties.Settings.Default.InputSource;
                if(String.IsNullOrEmpty(GeneralSettings.inputSource))
                {
                    throw new Exception("Ex: Input not available.");
                }
            }
            catch (Exception e)
            {
                logger.Warn(e.Message);
                Stop();
            }

            Logic.Initialize();
            
        }
        public void Stop()
        {
            logger.Info(Environment.NewLine + "--- Stopping X2DB... ---" + Environment.NewLine);
        }
    }
}
