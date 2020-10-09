using System;
using System.Linq;
using System.Text;

using System.Net.Http;
using System.Net.Http.Headers;

using System.Xml;
using System.Xml.Linq;

using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Configuration;

namespace updateCurrency
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                CurrencyRates rates = getEcbCurrencyData();
                writeRatesInDB(rates);
            }
            catch(Exception ex)
            {
                writeErrorLog(ex.ToString() + "; in " + ex.StackTrace);
            }
        }


        private static CurrencyRates getEcbCurrencyData() {
            string ecbUrl = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(ecbUrl);

            HttpResponseMessage response = client.GetAsync(ecbUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string resultAsString = response.Content.ReadAsStringAsync().Result;
                var xml = XDocument.Parse(resultAsString);

                XAttribute date = (from elem in xml.Root.Descendants()
                            where elem.Attribute("time") != null
                            select elem.Attribute("time")).FirstOrDefault();

                XAttribute usd = (from elem in xml.Root.Descendants()
                           where elem.Attribute("currency") != null && elem.Attribute("currency").Value == "USD"
                           select elem.Attribute("rate")).FirstOrDefault();

                XAttribute GBP = (from elem in xml.Root.Descendants()
                           where elem.Attribute("currency") != null && elem.Attribute("currency").Value == "GBP"
                           select elem.Attribute("rate")).FirstOrDefault();

                CurrencyRates rates = new CurrencyRates();

                rates.date = DateTime.Parse(date.Value);
                rates.usd = Decimal.Parse(usd.Value);
                rates.gbp = Decimal.Parse(GBP.Value);

                return rates;
            }
            else
            {
                throw new Exception("Http client exception. Status code: " + response.StatusCode);
            }
        }

        private static void writeRatesInDB(CurrencyRates rates)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["localDb"].ConnectionString;
            string commandText = "insert into rates(date, usd, gbp) values (@date,@usd,@gbp)";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(commandText, con))
            {
                con.Open();
                command.Parameters.Add(new SqlParameter("@date", SqlDbType.Date) { Value = rates.date.ToShortDateString() });
                command.Parameters.Add(new SqlParameter("@usd", SqlDbType.Decimal) { Value = rates.usd });
                command.Parameters.Add(new SqlParameter("@gbp", SqlDbType.Decimal) { Value = rates.gbp });
                command.ExecuteNonQuery();
            }
        }

        private struct CurrencyRates
        {
            public DateTime date;
            public decimal usd;
            public decimal gbp;
        }

        private static void writeErrorLog(string errorTxt)
        {
            string path = @"C:\ProgramData\updateCurrency\ErrorLog.txt";
            File.AppendAllLines(path, new[] { errorTxt, "" });
        }
    }
}
