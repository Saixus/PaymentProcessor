using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaymentProcessor.Data;
using mshtml;

namespace PaymentProcessor
{
    public class StylePitProcessor : BaseProcessor
    {
        private string curentLanguage = "";
        public StylePitProcessor(WebBrowser wb)
            : base(wb)
        {
        }

        public override void Execute(Data.ProcessContract userData, string url)
        {
            Utilities.ClearCookies();
            this.userData = userData;
            curentLanguage = userData.PaymentCountry;
            step = 0;
            wbView.Url = ApplyDomain(url, userData.PaymentCountry);
        }

        int step = 0;
        protected override void wbView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ProcessPayment();
        }

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
 
        const int WM_CHAR = 0x105;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        public void SendChar(char c)
        {
            IntPtr hWnd = (txt == null) ? wbView.Handle : txt.Handle;

            SendMessage(hWnd, WM_CHAR, (int)c, 0);
        }

        public TextBox txt;
        public void SendKey(Keys key)
        {
            IntPtr hWnd = (txt == null) ? wbView.Handle : txt.Handle;

            SendMessage(hWnd, WM_KEYDOWN, Convert.ToInt32(key), 0);
            SendMessage(hWnd, WM_KEYUP, Convert.ToInt32(key), 0);
        }

        private bool emailFocused = false;
        private void ProcessPayment()
        {
            if (step == 0)
            {
                //wbView.Document.Cookie.Remove(0);
                HtmlElementCollection col = wbView.Document.GetElementsByTagName("select");
                if (col.Count == 0) return;
                HtmlElement sel = col[0];

                var option =
                    sel.Children.Cast<HtmlElement>()
                       .First(x => x.InnerText.Trim().Equals(LangSize(userData.DressSize), StringComparison.InvariantCultureIgnoreCase));
                option.SetAttribute("selected", "selected");

                if (FormSubmit(LanguageDictionary("add to basket"))) step = 1;
            }
            else if (step == 1) // Press checkout in basket
            {
                if (FormSubmit(LanguageDictionary("go to checkout"))) step = 2;
            }
            else if (step == 2) // Enter Email step
            {
                HtmlElement email = wbView.Document.GetElementById("Email");
                if (email != null)
                {
                    if (!emailFocused)
                    {
                        email.Focus();
                        //email.C
                    }
                    emailFocused = true;
                    //email.DomElement
                    //wbView.Handle;
                    Task.Factory.StartNew(() => Thread.Sleep(1000)).ContinueWith(
                        _ =>
                        {
                            HtmlElement emailI2 = wbView.Document.GetElementById("Email");
                            emailI2.Focus();
                        }, TaskScheduler.FromCurrentSynchronizationContext()).ContinueWith(_ => Thread.Sleep(1000)).ContinueWith(_ =>
                    {
                        HtmlElement emailI = wbView.Document.GetElementById("Email");
                        emailI.SetAttribute("value", userData.EMail);
                    }
                        , TaskScheduler.FromCurrentSynchronizationContext());
                    //email.SetAttribute("value", userData.EMail);
                    //email.SetAttribute("placeholder", userData.EMail);
                    //SendKey(System.Windows.Forms.Keys.D);
                    return;
                    if (FormSubmit(LanguageDictionary("next"))) step = 3;
                }
            }
            else if (step == 3) // Card info
            {
                //Thread.Sleep(5000);
                Task.Factory.StartNew(() => Thread.Sleep(1000)).ContinueWith(_ =>
                                                                             UpdatePaymentValues()
                                                                             , TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith(_ => Thread.Sleep(1000))
                    .ContinueWith(
                        _ =>
                            {
                                var selC = FindElementByInnerText("label", "country");
                                if (selC == null) return;
                                HtmlElementCollection col =
                                    selC.Parent.GetElementsByTagName("select");
                                
                                HtmlElement sel = col[0];
                                var option =
                                    sel.Children.Cast<HtmlElement>()
                                       .First(
                                           x =>
                                           x.GetAttribute("value")
                                            .Trim()
                                            .Equals(userData.PaymentCountry, StringComparison.InvariantCultureIgnoreCase));
                                option.SetAttribute("selected", "selected");
                                //wbView.Document.InvokeScript("function() {alert(1); $root.selectedCountryGroup('UA'); countryChanged();}");


                                HtmlElement head = wbView.Document.GetElementsByTagName("head")[0];
                                HtmlElement scriptEl = wbView.Document.CreateElement("script");
                                IHTMLScriptElement element = (IHTMLScriptElement) scriptEl.DomElement;
                                element.text = "function injectionFunction() { mmm.selectedCountryGroup('" +
                                               userData.PaymentCountry + "'); mmm.countryChanged(); }";
                                head.AppendChild(scriptEl);

                                wbView.Document.InvokeScript("injectionFunction");

                                wbView.Document.InvokeScript("countryChanged");
                                //$root.selectedCountryGroup
                                //option.InvokeMember("Click");
                                //option.Focus();
                                sel.Focus();
                                GetNextInput("company").Focus();
                            }
                        , TaskScheduler.FromCurrentSynchronizationContext()).ContinueWith(_ => Thread.Sleep(1000)).
                     ContinueWith(_ =>
                         {
                             HtmlElement inp = GetNextInput(LanguageDictionary("first name"));
                             inp.SetAttribute("value", userData.PaymentFirstName);
                             inp.Focus();
                             inp = GetNextInput(LanguageDictionary("last name"));
                             inp.SetAttribute("value", userData.PaymentLastName);
                             inp.Focus();
                             inp = GetNextInput(LanguageDictionary("zipcode"));
                             inp.SetAttribute("value", userData.PaymentZip);
                             inp.Focus();
                             inp = GetNextInput(LanguageDictionary("address"));
                             inp.SetAttribute("value", userData.PaymentAddress);
                             inp.Focus();

                             GetNextInput(LanguageDictionary("company")).Focus(); // To call onblur of input to avoid validation
                         }, TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith(_ => Thread.Sleep(1000))
                    .ContinueWith(
                        _ => { if (FormSubmit("next")) step = 4; }
                        , TaskScheduler.FromCurrentSynchronizationContext());
                //GetNextInput("address");
            }
            else if (step == 4) // Delivery provider
            {
                Task.Factory.StartNew(() => Thread.Sleep(1000)).ContinueWith(_ => { if (FormSubmit(LanguageDictionary("next"))) step = 5; }
                                                                             ,
                                                                             TaskScheduler
                                                                                 .FromCurrentSynchronizationContext());
            }
            else if (step == 5) // Payment proceed
            {
                if (FormSubmit(LanguageDictionary("payment proceed"))) step = 6;
            }
            else if (step == 6) // Payment details
            {
                if (FormSubmit(LanguageDictionary("continue"))) step = 7;
            }
        }

        private void UpdatePaymentValues()
        {
            //Thread.Sleep(5000);
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

        private HtmlElement FindElementByInnerText(string tagName, string text)
        {
            HtmlElementCollection lbls = wbView.Document.GetElementsByTagName(tagName);
            foreach (HtmlElement lbl in lbls)
            {
                if (lbl.InnerText.Trim().Equals(text, StringComparison.InvariantCultureIgnoreCase)) return lbl;
            }
            return null;
        }

        private HtmlElement GetNextInput(string text)
        {
            HtmlElement lbl = FindElementByInnerText("label", text);
            if (text == LanguageDictionary("zipcode")) lbl = lbl.Parent.Parent;
            return lbl.Parent.GetElementsByTagName("input")[0];
        }

        private bool FormSubmit(string value)
        {
            HtmlElementCollection elc = wbView.Document.GetElementsByTagName("input");
            foreach (HtmlElement el in elc)
            {
                if (el.GetAttribute("type").Equals("submit"))
                    if (el.GetAttribute("value").ToLower().Equals(value))
                    {
                        el.InvokeMember("Click");
                        return true;
                    }
            }
            return false;
        }

        private string GetBaseUserByLanguage(string lang)
        {
            switch (lang)
            {
                case "UA":
                    return "stylepit.ua";
                case "FR":
                    return "stylepit.fr";
                case "UK":
                    return "stylepit.co.uk";
            }
            return "stylepit.com";
        }

        private Uri ApplyDomain(string baseUri, string lang)
        {
            Uri u = new Uri(baseUri);
            return new Uri(baseUri.Replace(u.Host, GetBaseUserByLanguage(lang)));
        }

        private string LanguageDictionary(string value)
        {
            string lang = curentLanguage;
            switch (lang)
            {
                case "UA":
                    switch (value)
                    {
                        case "add to basket":
                            return "в корзину";
                        case "go to checkout":
                            return "оформить";
                        case "next":
                            return "продолжить";
                        case "first name":
                            return "имя";
                        case "last name":
                            return "фамилия";
                        case "zipcode":
                            return "индекс";
                    }
                    return value;
            }
            return value;
        }

        private string LangSize(string size)
        {
            switch (size.ToLower())
            {
                case "small":
                    return "S";
                case "medium":
                    return "medium";//"m";
                case "large":
                    return "m";
            }
            return size;
        }
    }
}
