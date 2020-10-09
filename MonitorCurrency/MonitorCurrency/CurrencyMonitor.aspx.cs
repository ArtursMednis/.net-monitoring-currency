using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace currencyMonitor
{
    public partial class CurrencyMonitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<CurrencyRates> rates = getCurrencyDataFromSqlServer();
            addCurrencyRatesToClientScriptBlock(rates);
        }

        List<CurrencyRates> getCurrencyDataFromSqlServer()
        {
            StringBuilder sb = new StringBuilder();
            List<CurrencyRates> ratesList = new List<CurrencyRates>();
            string connectionString = ConfigurationManager.ConnectionStrings["localDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM rates", connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CurrencyRates rates = new CurrencyRates();
                            rates.date = DateTime.Parse(reader["date"].ToString());
                            rates.usd = Decimal.Parse(reader["usd"].ToString());
                            rates.gbp = Decimal.Parse(reader["gbp"].ToString());
                            ratesList.Add(rates);
                        }
                    }
                }
            }
            return ratesList;
        }

        private void addCurrencyRatesToClientScriptBlock(List<CurrencyRates> ratesList)
        {
            List<string> usdChartPoints = new List<string>();
            List<string> gbpChartPoints = new List<string>();

            foreach (CurrencyRates rates in ratesList)
            {
                usdChartPoints.Add("['" + rates.date.ToString("MM-dd-yyyy") + "'," + rates.usd.ToString() + "]");
                gbpChartPoints.Add("['" + rates.date.ToString("MM-dd-yyyy") + "'," + rates.gbp.ToString() + "]");
            }

            string chartData = "[[" + string.Join(", ", usdChartPoints.ToArray()) + "],[" + string.Join(", ", gbpChartPoints.ToArray())+"]]";

            ClientScript.RegisterStartupScript(this.GetType(), "CurrencyRates", "<script>displayCurrency("+ chartData + ");</script>");
        }

        private struct CurrencyRates
        {
            public DateTime date;
            public decimal usd;
            public decimal gbp;
        }
    }
}