using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartnerWebApi.Models.OutgoingModels
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public Address Address { get; set; }
        public List<Contract> ContractList { get; set; }
    }

}