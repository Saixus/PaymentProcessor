using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Awesomium.Core;

namespace PaymentProcessor
{
    public partial class AwesomiumBrowser : Form
    {
        public AwesomiumBrowser()
        {
            InitializeComponent();
        }

        private WebView webView;
        private void addressBox1_Navigate(object sender, Awesomium.Core.UrlEventArgs e)
        {
            //wcView.
            //wcView.Source = new Uri(addressBox1.Text);// addressBox1.URL;

            //WebCore.Config conf = new WebCore.Config();
            //WebCore.Initialize(conf);

            // Setup WebView
            webView = WebCore.CreateWebView(1024, 768);
            webView.DocumentReady += webView_DocumentReady;
            webView.Source = new Uri(addressBox1.Text);
        }

        void webView_DocumentReady(object sender, UrlEventArgs e)
        {
            //webView.ExecuteJavascript("document.getElementById('input_3').value='Username'");
            //webView.ExecuteJavascript("alert(111);");
            //JSObject elements = webView.ExecuteJavascriptWithResult("document.getElementsByTagName('select')");
            //int len = elements["length"];
            //Process();
        }

        private void Process()
        {
            //webView.ExecuteJavascript("window.alert('aaa');");
            dynamic elements = (JSObject)webView.ExecuteJavascriptWithResult("document.getElementsByTagName('select')[0].getElementsByTagName('option');");



            if (elements == null)
                return;

            int length = elements.length;
            if (length > 0)
            {
                var len = elements.length;
                for (int i = 0; i < len; ++i)
                {
                    if (elements[i].innerText.ToString().Trim() == "Medium")
                    {
                        elements[i].selected = "selected";
                        dynamic buttons = (JSObject)webView.ExecuteJavascriptWithResult("document.getElementsByTagName('input');");

                        for (int j = 0; j < buttons.length; ++j)
                        {
                            if (buttons[j].type == "submit")
                            {
                                buttons[j].click();
                            }
                        }
                    }
                }
            }
        }

        private void Awesomium_Windows_Forms_WebControl_DocumentReady(object sender, Awesomium.Core.UrlEventArgs e)
        {
            //wcView.
            //WebCore.Cre
            if (webView != null) webView.ExecuteJavascript("alert(222);");
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            webView = WebCore.CreateWebView(1024, 768, WebViewType.Window);
            webView.LoadingFrameComplete += webView_LoadingFrameComplete;
            webView.ParentWindow = panel1.Handle;
            webView.DocumentReady += webView_DocumentReady;
            webView.Source = new Uri(addressBox1.Text);
        }

        void webView_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            if (webView == null || !webView.IsLive) return;

            if (webView.IsDocumentReady)
            {
                Process();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Process();
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            webView.ExecuteJavascript("document.getElementById('Email').value = 'ds@test.com';");
        }
    }
}
