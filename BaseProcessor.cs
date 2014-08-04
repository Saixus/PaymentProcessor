using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaymentProcessor
{
    public abstract class BaseProcessor
    {
        protected WebBrowser wbView;
        protected Data.ProcessContract userData;

        protected BaseProcessor(WebBrowser wb)
        {
            wbView = wb;
            wbView.DocumentCompleted += wbView_DocumentCompleted;
        }

        protected abstract void wbView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e);
        public abstract void Execute(Data.ProcessContract userData, string url);
    }
}
