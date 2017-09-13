using System;
using ElectronicMailNotification.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AIMS.Classes
{
    public class FElectronicMail : IFElectronicMail
    {
        public void Send(ElectronicMail electronicMail)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.12.151:1141/");

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("api/ElectronicMail/Send", electronicMail).Result;

            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                
            }
        }
    }
}
