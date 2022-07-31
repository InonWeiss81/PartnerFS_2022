using Newtonsoft.Json;
using PartnerWebApi.Models.OutgoingModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace PartnerWebApi.Controllers
{
    public class CustomersController : ApiController
    {
        public string Get()
        {
            string fileName = "CustomersJson.json";
            string sFileName = HttpContext.Current.Server.MapPath("~/");
            string path = Path.Combine(sFileName, @"Data\", fileName);
            using (var sr = new StreamReader(path))
            {
                var json = sr.ReadToEnd();
                var CustomersList = JsonConvert.DeserializeObject<Customers>(json);
            }
            
            return "";
        }
    }
}
