using Newtonsoft.Json;
using PartnerWebApi.Data;
using static PartnerWebApi.Models.Enums.ResponseMessages;
using PartnerWebApi.Models.OutgoingModels;
using System;
using System.IO;
using System.Web;
using System.Web.Http;

namespace PartnerWebApi.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private const string ALL_CUSTOMERS = "customers";

        [Route("getCustomerData")]
        [HttpPost]
        public BaseResponse GetCustomerData([FromBody] string idNumber)
        {
            if (!InputValidation(idNumber))
            {
                return new BaseResponse(System.Net.HttpStatusCode.BadRequest, Messages.InvalidIdNumber, true);
            }
            var customers = GetCustomersFromCacheOrFile();
            if (customers?.CustomersList == null)
            {
                return new BaseResponse(System.Net.HttpStatusCode.InternalServerError, Messages.CustomerDatailsUnavailable, true);
            }
            Customer customer = customers.GetCustomerByIdNumber(idNumber);
            if (customer == null)
            {
                return new BaseResponse(System.Net.HttpStatusCode.NotFound, Messages.CustomerNotExist, true);
            }
            // this is where we retun the customer as json
            return new BaseResponse(System.Net.HttpStatusCode.OK, JsonConvert.SerializeObject(customer));
        }

        [Route("updateCustomerAddress")]
        [HttpPost]
        public BaseResponse UpdateCustomerAddress([FromBody] Customer customerObject)
        {
            try
            {
                if (customerObject == null)
                {
                    return new BaseResponse(System.Net.HttpStatusCode.BadRequest, Messages.DataError, true);
                }
                var customers = GetCustomersFromCacheOrFile();
                var oldCustomer = customers.GetCustomerByIdNumber(customerObject.IdNumber);
                if (oldCustomer == null)
                {
                    return new BaseResponse(System.Net.HttpStatusCode.BadRequest, Messages.CustomerNotExist, true);
                }
                oldCustomer.Address = customerObject.Address;
                if(!SaveCustomers(customers))
                {
                    return new BaseResponse(System.Net.HttpStatusCode.InternalServerError, Messages.CustomerUpdatedFailed, true);
                }
            }
            catch (Exception)
            {
                return new BaseResponse(System.Net.HttpStatusCode.BadRequest, Messages.CustomerUpdatedFailed, true);
            }
            return new BaseResponse(System.Net.HttpStatusCode.OK, Messages.UpdateAddressSuccess);
        }

        #region Private
        private bool SaveCustomers(Customers customers)
        {
            try
            {
                string fileName = "CustomersJson.json";
                string sFileName = HttpContext.Current.Server.MapPath("~/");
                string path = Path.Combine(sFileName, @"Data\", fileName);
                using (var sr = new StreamWriter(path, false))
                {
                    var json = JsonConvert.SerializeObject(customers);
                    sr.Write(json);
                }
                if (!UpdateCache(customers))
                {
                    ClearCache();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void ClearCache()
        {
            new CacheManager<Customers>().ClearCache();
        }

        private bool UpdateCache(Customers customers)
        {
            return CacheManager<Customers>.Update(ALL_CUSTOMERS, customers);
        }

        private Customers GetCustomersFromCacheOrFile()
        {
            return CacheManager<Customers>.GetOrCreate(ALL_CUSTOMERS, () => GetCustomersFromFile());
        }

        private Customers GetCustomersFromFile()
        {
            Customers result = new Customers();
            string fileName = "CustomersJson.json";
            string sFileName = HttpContext.Current.Server.MapPath("~/");
            string path = Path.Combine(sFileName, @"Data\", fileName);
            using (var sr = new StreamReader(path))
            {
                var json = sr.ReadToEnd();
                result = JsonConvert.DeserializeObject<Customers>(json);
            }
            return result;
        }

        private bool InputValidation(string israeliID)
        {
            if (israeliID == null || israeliID.Length > 9)
            {
                return false;
            }
            israeliID = israeliID.PadLeft(9, '0');

            long sum = 0;

            for (int i = 0; i < israeliID.Length; i++)
            {
                var digit = israeliID[israeliID.Length - 1 - i] - '0';
                sum += (i % 2 != 0) ? GetDouble(digit) : digit;
            }

            return sum % 10 == 0;

            int GetDouble(int i)
            {
                if (i < 5)
                {
                    return i * 2;
                }
                return GetDouble(i - 5) + 1;
            }
        }
        #endregion
    }
}
