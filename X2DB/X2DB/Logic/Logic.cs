using NLog;
using NPOI.XSSF.Extractor;
using NPOI.OpenXml4Net.OPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using NPOI.XSSF.UserModel;
using System.Data;
using NPOI.SS.UserModel;
using Newtonsoft.Json;
using System.Net;
using Microsoft.SharePoint.Client;
using System.Security;

namespace X2DB
{
    public static class Logic
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Thread InitializeThread;

        public static void Initialize()
        {
            Stream stream = null;
            try
            {
                //var xstream = new FileStream(@"C:\Users\skhandelwal\Desktop\POW.xlsx", FileMode.Open);
                //HttpWebRequest aRequest = (HttpWebRequest)WebRequest.Create(@"https://docs.google.com/spreadsheets/d/e/2PACX-1vT0KB_YuSgmo4vJK59SNYqKvG4a6yRYCDGSIrXiPXv0NmAq70JdiPIybqhqZTKm_A/pub?output=xlsx");
                //HttpWebResponse aResponse = (HttpWebResponse)aRequest.GetResponse();
                //stream = aResponse.GetResponseStream();

                CredentialCache myCache = new CredentialCache();
                //myCache.Add(new Uri("https://apteanonline-my.sharepoint.com/"), "Basic", new NetworkCredential("skhandelwal@aptean.com", "Skgtrx%234"));
                string userName = "skhandelwal@aptean.com";
                string password = "Skgtrx%234";
                ClientContext ctx = new ClientContext(GeneralSettings.inputSource);
                SecureString secureString = new SecureString();
                password.ToList().ForEach(secureString.AppendChar);
                myCache.Add(new Uri("https://apteanonline-my.sharepoint.com/"), "Basic", new NetworkCredential(userName, secureString));
                ctx.Credentials = myCache;
                Site site = ctx.Site;

                ctx.Load(site);
                ctx.ExecuteQuery();

            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
            InitializeThread = new Thread(x => 
            {
                // Thread Logic Implementation
                DataTable dtTable = new DataTable();
                List<string> rowList = new List<string>();
                ISheet sheet;
                using (stream)
                using (MemoryStream ms = new MemoryStream())
                {
                    
                    try
                    {
                        int count = 0;
                        do
                        {
                            byte[] buf = new byte[1024];
                            count = stream.Read(buf, 0, 1024);
                            ms.Write(buf, 0, count);
                        } while (stream.CanRead && count > 0);

                        ms.Position = 0;
                        XSSFWorkbook xssWorkbook = new XSSFWorkbook(ms);
                        sheet = xssWorkbook.GetSheetAt(0);
                        IRow headerRow = sheet.GetRow(0);
                        int cellCount = headerRow.LastCellNum;
                        for (int j = 0; j < cellCount; j++)
                        {
                            ICell cell = headerRow.GetCell(j);
                            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                            {
                                dtTable.Columns.Add(cell.ToString());
                            }
                        }
                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null) continue;
                            if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                                    {
                                        rowList.Add(row.GetCell(j).ToString());
                                    }
                                }
                            }
                            if (rowList.Count > 0)
                                dtTable.Rows.Add(rowList.ToArray());
                            rowList.Clear();
                        }
                    }
                    catch(Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
                logger.Info(JsonConvert.SerializeObject(dtTable));
            });
            InitializeThread.IsBackground = true;
            InitializeThread.Start();
            logger.Info("Initialize thread is started.");
        }
    }
}
