using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COVIDAPI;

namespace CovidIndia
{
    public partial class CovidInformation : Form
    {
        public CovidInformation()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.BaseAddress = "https://api.covid19india.org/data.json";
            var jsonString = client.DownloadString(client.BaseAddress);
            var CovidObject = COVIDAPI.CovidIndia.FromJson(jsonString);
            var stateData = CovidObject.Statewise;

            var CovidTable = ConverListToDataTable(stateData.OrderByDescending(x => Convert.ToInt32(x.Confirmed)).ToList(), "TT");

            dataGridView1.DataSource = CovidTable;
        }

        private DataTable ConverListToDataTable(List<Statewise> statewise, string Header)
        {

            decimal iTotal = 0;

            DataTable table = new DataTable("Covid");

            DataColumn state = new DataColumn("STATE");
            DataColumn active = new DataColumn("ACTIVE");
            DataColumn confirmed = new DataColumn("CONFIRMED");
            DataColumn recovered = new DataColumn("RECOVERED");
            DataColumn deaths = new DataColumn("FAILED");
            DataColumn statecode = new DataColumn("STATE CODE");
            DataColumn percentage = new DataColumn("CONFIRMED PERC (%)");
            DataColumn deltaconfirmed = new DataColumn("CONFIRMED TODAY");
            DataColumn deltadeaths = new DataColumn("FAILED TODAY");
            DataColumn deltarecovered = new DataColumn("RECOVERED TODAY");
            
            active.DataType = typeof(int);
            confirmed.DataType = typeof(int);
            recovered.DataType = typeof(int);
            deaths.DataType = typeof(int);
            deltaconfirmed.DataType = typeof(int);
            deltadeaths.DataType = typeof(int);
            deltarecovered.DataType = typeof(int);
            percentage.DataType = typeof(double);

            table.Columns.Add(state);
            table.Columns.Add(statecode);
            table.Columns.Add(percentage);
            table.Columns.Add(active);
            table.Columns.Add(confirmed);
            table.Columns.Add(recovered);
            table.Columns.Add(deaths);
            table.Columns.Add(deltaconfirmed);
            table.Columns.Add(deltadeaths);
            table.Columns.Add(deltarecovered);

            foreach (var item in statewise)
            {

                if (item.Statecode == Header)
                {
                    iTotal = Convert.ToInt32(item.Confirmed);
                    continue;
                }

                DataRow row = table.NewRow();

                row[state] = item.State.ToUpper();
                row[statecode] = item.Statecode;
                row[percentage] = Convert.ToDecimal(string.Format("{0:F2}", ((Convert.ToDecimal(item.Confirmed) / iTotal) * 100)));
                row[active] = item.Active;
                row[confirmed] = item.Confirmed;
                row[recovered] = item.Recovered;
                row[deaths] = item.Deaths;
                row[deltaconfirmed] = item.Deltaconfirmed;
                row[deltadeaths] = item.Deltadeaths;
                row[deltarecovered] = item.Deltarecovered;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
