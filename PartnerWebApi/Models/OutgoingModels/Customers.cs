using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PartnerWebApi.Models.OutgoingModels
{
    public class Customers
    {
        public Customer[] CustomersList { get; set; }

        public Customer GetCustomerByIdNumber(string idNumber)
        {
            return Array.Find(CustomersList, c => c.IdNumber == idNumber);
        }
    }
}

