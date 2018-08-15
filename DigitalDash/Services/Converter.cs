using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using OfficeOpenXml;
using DigitalDash.Models;

namespace DigitalDash.Services
{
    public class Converter
    {
        public static IDictionary<string, string> types = new Dictionary<string, string>();

        public List<Chart> Convert()
        {
            DirectoryInfo di = new DirectoryInfo("./wwwroot/Sheets");
            FileInfo[] files = di.GetFiles("*.xlsx");
            List<Chart> charts = new List<Chart>();

            foreach (FileInfo file in files)
            {
                var ep = new ExcelPackage(new FileInfo(file.FullName));
                var ws = ep.Workbook.Worksheets["Sheet1"];
                Chart chart = new Chart();
                List<String> dataList = new List<String>();
                List<String> labelList = new List<String>();

                dataList.Add(ws.Cells[1, 2].Value.ToString());
                string[] words = (file.Name).Split('.');
                chart.title = words[0];
                if (!types.ContainsKey(words[0]))
                {
                    chart.type = "bar";
                }
                else
                {
                    chart.type = types[words[0]];
                }

                if (chart.type=="pie"|| chart.type == "donut")
                {
                    String dataString="";
                    List <List<String> >da = new List<List<String>>();

                    for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                    {
                        if (ws.Cells[rw, 1].Value != null)
                        {
                            List < String > gg = new  List<String>();
                            gg.Add(ws.Cells[rw, 1].Value.ToString());
                            gg.Add( ws.Cells[rw, 2].Value.ToString());
                            da.Add(gg);

                            dataString =dataString +"["+ ws.Cells[rw, 1].Value.ToString() + "," + ws.Cells[rw, 2].Value.ToString() + "]";
                        }
                        
                    }                    
                    chart.data = dataString;
                    chart.data2 = da;
                }
                else
                {
                    for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                    {
                        if (ws.Cells[rw, 1].Value != null)
                        {
                            dataList.Add(ws.Cells[rw, 2].Value.ToString());
                            labelList.Add(ws.Cells[rw, 1].Value.ToString());
                        }
                    }

                    String dataString = string.Join(",", dataList);
                    chart.data = dataString;
                }

               
                String labelString = string.Join(",", labelList);
                chart.labels = labelString;
                
                chart.datatype = ws.Cells[1,1].Value.ToString(); 

                charts.Add(chart);
            }
            return charts;
        }

        public IDictionary<string, string> GetChartType()
        {
            return types;
        }
        public void SetChartType(string chart, string type)
        {
            Debug.WriteLine(chart);
            Debug.WriteLine(type);
            if (!types.ContainsKey(chart))
            {
                types.Add(chart, type);
            }
            else
            {
                types[chart] = type;
            }
            foreach (KeyValuePair<string, string> kvp in types)
            {

                Debug.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
        }

        public List<String> FileNames()
        {
            List<String> names = new List<string>();
            DirectoryInfo di = new DirectoryInfo("C:/Users/beckfordcm/Documents/Dev/Tabler/DashData");
            FileInfo[] files = di.GetFiles("*.xlsx");
            foreach (FileInfo file in files)
            {
                string[] words = (file.Name).Split('.');
                names.Add(words[0]);

            }
                return names;
        }
    }
}
