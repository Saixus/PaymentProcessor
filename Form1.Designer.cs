namespace PaymentProcessor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbView = new System.Windows.Forms.WebBrowser();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbnStartTimer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wbView
            // 
            this.wbView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wbView.Location = new System.Drawing.Point(0, 38);
            this.wbView.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbView.Name = "wbView";
            this.wbView.Size = new System.Drawing.Size(953, 484);
            this.wbView.TabIndex = 0;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(779, 20);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://www.stylepit.com/girl/clothes/skirts/gray_vila-skirt_229571_19";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(797, 10);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(29, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(897, 9);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(56, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbnStartTimer
            // 
            this.tbnStartTimer.Location = new System.Drawing.Point(832, 10);
            this.tbnStartTimer.Name = "tbnStartTimer";
            this.tbnStartTimer.Size = new System.Drawing.Size(59, 22);
            this.tbnStartTimer.TabIndex = 4;
            this.tbnStartTimer.Text = "Timer";
            this.tbnStartTimer.UseVisualStyleBackColor = true;
            this.tbnStartTimer.Click += new System.EventHandler(this.tbnStartTimer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 522);
            this.Controls.Add(this.tbnStartTimer);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.wbView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbView;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button tbnStartTimer;
    }
}

