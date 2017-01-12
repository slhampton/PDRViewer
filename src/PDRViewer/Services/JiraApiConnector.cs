using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDRViewer.Models;
// ReSharper disable InconsistentNaming

namespace PDRViewer.Services
{
    public class JiraApiConnector
    {
        private string baseUrl;

        public JiraApiConnector()
        {
            this.baseUrl = "https://acs-sandbox.atlassian.net/";
        }

        public List<PDR> GetPDRs()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    "c2FtLmhhbXB0b246dGVzdA==");
                client.DefaultRequestHeaders.Add("X-Atlassian-Token", "nocheck");
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "rest/api/latest/search");
                request.Content = new StringContent("{\"jql\":\"project=ADASTRA&issuetype=\\\"Improvement/Suggestion\\\" ORDER BY \\\"Customer Votes\\\" DESC\"}",
                                    Encoding.UTF8,
                                    "application/json");

                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                var responseString = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Root>(responseString);

                var PdrList = new List<PDR>();

                for (var i = 0; i < result.Issues.Count; i++)
                {
                    var pdr = new PDR
                    {
                        Id = result.Issues[i].Key,
                        Title = result.Issues[i].Fields.Title,
                        Description = result.Issues[i].Fields.Description,
                        Votes = result.Issues[i].Fields.Votes
                    };
                    PdrList.Add(pdr);
                }

                return PdrList;
            }
        }

        public void UpVote(string id, int votes)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    "c2FtLmhhbXB0b246dGVzdA==");
                client.DefaultRequestHeaders.Add("X-Atlassian-Token", "nocheck");
                
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"rest/api/latest/issue/{id}");
                request.Content = new StringContent("{\"fields\": {\"customfield_16000\":" + (votes+1) + "}}",
                                    Encoding.UTF8,
                                    "application/json");
                

                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
