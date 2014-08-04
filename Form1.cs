using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaymentProcessor.Data;
using PaymentProcessor.DataAccess;

namespace PaymentProcessor
{
    public partial class Form1 : Form
    {
        private int orderId = 0;
        public Form1(int orderId)
        {
            this.orderId = orderId;
            InitializeComponent();

            if (orderId > 0) ExecuteOrder();
        }

        private StylePitProcessor p;
        private void btnGo_Click(object sender, EventArgs e)
        {
            //step = 0;
            
            
            p = new StylePitProcessor(wbView);
            p.Execute(new ProcessContract()
                {
                    EMail = "ds@test.com",
                    PaymentFirstName = "Jhon",
                    PaymentLastName = "Smith",
                    CreditCardType = CardType.Visa,
                    PaymentAddress = "5th ave, 4, suite 14",
                    PaymentZip = "10101",
                    PaymentCountry = "USA",//"UA",
                    DressSize =  "medium"
                }, txtUrl.Text);

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var f = new AwesomiumBrowser();
            f.Show();
            return;
            wbView.Document.GetElementById("web").Document.GetElementsByTagName("select");
            return;
            var userData = new ProcessContract()
                {
                    EMail = "test@email.com",
                    PaymentFirstName = "Jhon",
                    PaymentLastName = "Smith",
                    CreditCardType = CardType.Visa,
                    PaymentAddress = "5th ave, 4, suite 14",
                    PaymentZip = "10101",
                    PaymentCountry = "FR"
                };
            HtmlElement ctype = null;
            switch (userData.CreditCardType)
            {
                case CardType.MasterCard:
                    ctype = wbView.Document.GetElementById("MC_GC");
                    break;
                case CardType.Visa:
                    ctype = wbView.Document.GetElementById("VISA_GC");
                    break;
                case CardType.VisaElectron:
                    ctype = wbView.Document.GetElementById("VISA_ELEC_GC");
                    break;
            }
            if (ctype == null) return;

            ctype.InvokeMember("Click");

            //GetNextInput("first name").SetAttribute("value", userData.PaymentFirstName);
            //GetNextInput("last name").SetAttribute("value", userData.PaymentLastName);
            //GetNextInput("zipcode").SetAttribute("value", userData.PaymentZip);
            //GetNextInput("address").SetAttribute("value", userData.PaymentAddress);
        }

        private void tbnStartTimer_Click(object sender, EventArgs e)
        {
            //wbView.Url = new Uri(@"file://C:\Work\PaymentProcessor\PaymentProcessor\StylepitContainer.html ");

            HtmlElement email = wbView.Document.GetElementById("Email");
            email.SetAttribute("value", "ds2@test.com");

            //p.txt = txtUrl;
            //p.SendKey(System.Windows.Forms.Keys.D);
            //p.SendChar('a');

            ////ProductDA.GetUserSetting()
            //StylePitProcessor p = new StylePitProcessor(wbView);
            //p.Execute(new ProcessContract()
            //{
            //    EMail = "test@email.com",
            //    PaymentFirstName = "Jhon",
            //    PaymentLastName = "Smith",
            //    CreditCardType = CardType.Visa,
            //    PaymentAddress = "5th ave, 4, suite 14",
            //    PaymentZip = "10101",
            //    PaymentCountry = "UA",
            //    DressSize = "medium"
            //}, txtUrl.Text);
        }

        private void ExecuteOrder()
        {
            StylePitProcessor p = new StylePitProcessor(wbView);
            ProcessContract pc = ProductDA.GetOrderDetails(orderId);
            p.Execute(pc, pc.productUrl);
        }
    }
}
