using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Data
{
    [DataContract]
    public class ProcessContract
    {
        public string EMail;

        // payment info
        public CardType CreditCardType;
        public string PaymentFirstName;
        public string PaymentLastName;
        public string PaymentCountry;
        public string PaymentZip;
        public string PaymentAddress;

        // producct data
        public string productUrl;

        // Size
        public string DressSize;
    }

    public enum CardType
    {
        None =0,
        MasterCard = 1,
        Visa = 2,
        VisaElectron = 3
    }
}
