namespace PartnerWebApi.Models.OutgoingModels
{
    public class Package
    {
        public PackageType PackageType { get; set; }
        public string PackageName { get; set; }
        public int PackageQuantity { get; set; }
        public int PackageBalance { get; set; }
    }

}