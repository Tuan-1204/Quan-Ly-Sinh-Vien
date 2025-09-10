namespace Quan_Ly_Sinh_Vien
{
    partial class FDangNhap
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblDangnhap = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbPassWord = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.lBLTenDangNhap = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chekShowPass = new System.Windows.Forms.CheckBox();
            this.linkQuenPass = new System.Windows.Forms.LinkLabel();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblDangnhap);
            this.panel6.Location = new System.Drawing.Point(93, 12);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(385, 50);
            this.panel6.TabIndex = 5;
            // 
            // lblDangnhap
            // 
            this.lblDangnhap.AutoSize = true;
            this.lblDangnhap.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangnhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDangnhap.Location = new System.Drawing.Point(66, 14);
            this.lblDangnhap.Name = "lblDangnhap";
            this.lblDangnhap.Size = new System.Drawing.Size(268, 26);
            this.lblDangnhap.TabIndex = 0;
            this.lblDangnhap.Text = "ĐĂNG NHẬP HỆ THỐNG";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txbPassWord);
            this.panel2.Controls.Add(this.lblPassword);
            this.panel2.Location = new System.Drawing.Point(93, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 50);
            this.panel2.TabIndex = 6;
            // 
            // txbPassWord
            // 
            this.txbPassWord.Enabled = true;       // bật nhập
            this.txbPassWord.ReadOnly = false;     // cho phép gõ
            this.txbPassWord.Location = new System.Drawing.Point(138, 11);
            this.txbPassWord.Name = "txbPassWord";
            this.txbPassWord.Size = new System.Drawing.Size(228, 26);
            this.txbPassWord.TabIndex = 2;
            this.txbPassWord.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(22, 11);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(86, 18);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Mật Khẩu : ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbUserName);
            this.panel1.Controls.Add(this.lBLTenDangNhap);
            this.panel1.Location = new System.Drawing.Point(93, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 50);
            this.panel1.TabIndex = 2;
            // 
            // txbUserName
            // 
            this.txbUserName.Enabled = true;       // bật nhập
            this.txbUserName.ReadOnly = false;     // thêm dòng này nếu cần
            this.txbUserName.Location = new System.Drawing.Point(138, 11);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(228, 26);
            this.txbUserName.TabIndex = 1;
            // 
            // lBLTenDangNhap
            // 
            this.lBLTenDangNhap.AutoSize = true;
            this.lBLTenDangNhap.Location = new System.Drawing.Point(22, 14);
            this.lBLTenDangNhap.Name = "lBLTenDangNhap";
            this.lBLTenDangNhap.Size = new System.Drawing.Size(91, 18);
            this.lBLTenDangNhap.TabIndex = 0;
            this.lBLTenDangNhap.Text = "Tài Khoản : ";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnLogin);
            this.panel8.Location = new System.Drawing.Point(211, 220);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(116, 39);
            this.panel8.TabIndex = 7;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(3, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(110, 32);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Location = new System.Drawing.Point(343, 220);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(116, 39);
            this.panel3.TabIndex = 8;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 32);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.linkQuenPass);
            this.panel4.Controls.Add(this.chekShowPass);
            this.panel4.Location = new System.Drawing.Point(190, 180);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(288, 34);
            this.panel4.TabIndex = 9;
            // 
            // chekShowPass
            // 
            this.chekShowPass.AutoSize = true;
            this.chekShowPass.Location = new System.Drawing.Point(3, 11);
            this.chekShowPass.Name = "chekShowPass";
            this.chekShowPass.Size = new System.Drawing.Size(146, 22);
            this.chekShowPass.TabIndex = 3;
            this.chekShowPass.Text = "Hiển thị mật khẩu";
            this.chekShowPass.UseVisualStyleBackColor = true;
            this.chekShowPass.CheckedChanged += new System.EventHandler(this.chekShowPass_CheckedChanged);
            // 
            // linkQuenPass
            // 
            this.linkQuenPass.AutoSize = true;
            this.linkQuenPass.Location = new System.Drawing.Point(155, 12);
            this.linkQuenPass.Name = "linkQuenPass";
            this.linkQuenPass.Size = new System.Drawing.Size(119, 18);
            this.linkQuenPass.TabIndex = 4;
            this.linkQuenPass.TabStop = true;
            this.linkQuenPass.Text = "Quên Mật Khẩu ";
            this.linkQuenPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkQuenPass_LinkClicked);
            // 
            // FDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 278);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel6);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập Hệ Thống";
            this.Load += new System.EventHandler(this.FDangNhap_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblDangnhap;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txbPassWord;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.Label lBLTenDangNhap;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel linkQuenPass;
        private System.Windows.Forms.CheckBox chekShowPass;
    }
}