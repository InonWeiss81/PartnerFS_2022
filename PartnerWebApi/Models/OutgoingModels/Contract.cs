using System.Collections.Generic;

namespace PartnerWebApi.Models.OutgoingModels
{
    public class Contract
    {
        public string ContractId { get; set; }
        public string ContractName { get; set; }
        public List<Package> PackagesList { get; set; }
    }

}