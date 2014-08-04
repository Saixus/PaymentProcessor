using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentProcessor.Data;

namespace PaymentProcessor.DataAccess
{
    public static class ProductDA
    {
        private static string connectionString = @"Data Source=.\sql2012;Initial Catalog=ZyShopper;user=sa;password=sasasa;";

        public static List<Order> GetAvailableOrders()
        {
            using (var dc = new ProductDBDataContext(connectionString))
            {
                return dc.Orders.Where(
                    x => !x.ExecuteDate.HasValue && !x.CancelDate.HasValue && x.CreateDate <= DateTime.Now.AddSeconds(10)).ToList();

            }
            //return null;
        }

        public static UserSetting GetUserSetting(Guid uid)
        {
            using (var dc = new ProductDBDataContext(connectionString))
            {
                return dc.UserSettings.FirstOrDefault(x => x.IsDefault.HasValue && x.IsDefault.Value && x.Userid == uid);
            }
        }

        public static ProcessContract GetOrderDetails(int orderId)
        {
            using (var dc = new ProductDBDataContext(connectionString))
            {
                Order o = dc.Orders.First(x => x.OrderId == orderId);
                var uid = o.UserId;
                var userSettings = dc.UserSettings.First(x => x.IsDefault.HasValue && x.IsDefault.Value && x.Userid == uid);
                var pr = dc.Products.First(x => x.ProductId == o.ProductId);
                var prod = new ProcessContract()
                {
                    EMail = userSettings.Email,
                    PaymentFirstName = userSettings.FirstName,
                    PaymentLastName = userSettings.LastName,
                    PaymentCountry = "UA",//userSettings.Country
                    PaymentAddress = userSettings.Address,
                    PaymentZip = userSettings.Zip,
                    DressSize = "m",
                    CreditCardType = (CardType)(userSettings.CreditCardType ?? 0),
                    productUrl = pr.Reference
                };

                return prod;
            }
        }
    }
}
