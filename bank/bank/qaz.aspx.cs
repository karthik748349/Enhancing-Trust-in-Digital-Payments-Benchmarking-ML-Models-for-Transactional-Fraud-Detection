using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;

namespace bank
{
	public partial class qaz : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnScan_Click(object sender, EventArgs e)
        {
            lblResult.Text = "<span class='loading'>Scanning, please wait...</span>";
            string userUrl = txtUrl.Text.Trim();

            if (string.IsNullOrEmpty(userUrl))
            {
                lblResult.Text = "Please enter a valid URL!";
                return;
            }

            try
            {
                string apiKey = "019b3083-c197-7028-9242-333c4d86c967"; // Optional but recommended
                string scanApi = "https://urlscan.io/api/v1/scan/";
                string resultApiBase = "https://urlscan.io/api/v1/result/";

                using (HttpClient client = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(apiKey))
                        client.DefaultRequestHeaders.Add("API-Key", apiKey);

                    // 1️⃣ Submit URL for scan
                    var payload = new { url = userUrl, visibility = "public" };
                    var response = await client.PostAsync(scanApi,
                        new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var uuid = JObject.Parse(jsonResponse)["uuid"].ToString();

                    // 2️⃣ Retry logic to get result (handle 404 if scan not ready)
                    int retries = 0;
                    int maxRetries = 5;
                    string resultResponse = null;
                    bool success = false;

                    while (retries < maxRetries && !success)
                    {
                        try
                        {
                            string resultUrl = resultApiBase + uuid + "/"; // trailing slash required
                            resultResponse = await client.GetStringAsync(resultUrl);
                            success = true;
                        }
                        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
                        {
                            retries++;
                            await Task.Delay(3000); // wait 3 seconds before retry
                        }
                    }

                    if (!success)
                    {
                        lblResult.Text = "Scan result not ready yet. Please try again in a few seconds.";
                        return;
                    }

                    // 3️⃣ Parse JSON and display
                    var resultJson = JObject.Parse(resultResponse);
                    bool malicious = (bool)resultJson["verdicts"]["overall"]["malicious"];
                    int score = (int)resultJson["verdicts"]["overall"]["score"];

                    lblResult.Text = $"URL: {userUrl}<br/>" +
                                     $"Malicious: <b>{malicious}</b><br/>" +
                                     $"Score: {score}/100<br/>" +
                                     $"Scan report: <a href='https://urlscan.io/result/{uuid}/' target='_blank'>View Full Report</a>";
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error scanning URL: " + ex.Message;
            }
        }
    }
}