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
            this.quan_Ly_Sinh_VienDataSet1 = new Quan_Ly_Sinh_Vien.Quan_Ly_Sinh_VienDataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.LnkQuenPass = new System.Windows.Forms.LinkLabel();
            this.chkPassWord = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txbPassWord = new System.Windows.Forms.TextBox();
            this.lblPassWord = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.quan_Ly_Sinh_VienDataSet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // quan_Ly_Sinh_VienDataSet1
            // 
            this.quan_Ly_Sinh_VienDataSet1.DataSetName = "Quan_Ly_Sinh_VienDataSet";
            this.quan_Ly_Sinh_VienDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 229);
            this.panel1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.LnkQuenPass);
            this.panel6.Controls.Add(this.chkPassWord);
            this.panel6.Location = new System.Drawing.Point(120, 139);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(420, 36);
            this.panel6.TabIndex = 4;
            // 
            // LnkQuenPass
            // 
            this.LnkQuenPass.AutoSize = true;
            this.LnkQuenPass.LinkColor = System.Drawing.Color.Red;
            this.LnkQuenPass.Location = new System.Drawing.Point(247, 5);
            this.LnkQuenPass.Name = "LnkQuenPass";
            this.LnkQuenPass.Size = new System.Drawing.Size(170, 23);
            this.LnkQuenPass.TabIndex = 4;
            this.LnkQuenPass.TabStop = true;
            this.LnkQuenPass.Text = "Quên Mật Khẩu ? ";
            this.LnkQuenPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkQuenPass_LinkClicked);
            // 
            // chkPassWord
            // 
            this.chkPassWord.AutoSize = true;
            this.chkPassWord.Location = new System.Drawing.Point(6, 4);
            this.chkPassWord.Name = "chkPassWord";
            this.chkPassWord.Size = new System.Drawing.Size(192, 27);
            this.chkPassWord.TabIndex = 3;
            this.chkPassWord.Text = "Hiển Thị Mật Khẩu";
            this.chkPassWord.UseVisualStyleBackColor = true;
            this.chkPassWord.CheckedChanged += new System.EventHandler(this.chkPassWord_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnExit);
            this.panel5.Location = new System.Drawing.Point(331, 182);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(132, 44);
            this.panel5.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(0, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(127, 33);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnLogin);
            this.panel4.Location = new System.Drawing.Point(177, 182);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(132, 44);
            this.panel4.TabIndex = 2;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(3, 3);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(127, 33);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txbPassWord);
            this.panel3.Controls.Add(this.lblPassWord);
            this.panel3.Location = new System.Drawing.Point(36, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(433, 37);
            this.panel3.TabIndex = 1;
            // 
            // txbPassWord
            // 
            this.txbPassWord.Location = new System.Drawing.Point(167, 4);
            this.txbPassWord.Name = "txbPassWord";
            this.txbPassWord.Size = new System.Drawing.Size(260, 30);
            this.txbPassWord.TabIndex = 2;
            this.txbPassWord.UseSystemPasswordChar = true;
            // 
            // lblPassWord
            // 
            this.lblPassWord.AutoSize = true;
            this.lblPassWord.Location = new System.Drawing.Point(3, 7);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(106, 23);
            this.lblPassWord.TabIndex = 0;
            this.lblPassWord.Text = "Mật Khẩu :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txbUserName);
            this.panel2.Controls.Add(this.lblTenDangNhap);
            this.panel2.Location = new System.Drawing.Point(36, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(433, 37);
            this.panel2.TabIndex = 0;
            // 
            // txbUserName
            // 
            this.txbUserName.Location = new System.Drawing.Point(167, 4);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(260, 30);
            this.txbUserName.TabIndex = 1;
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new System.Drawing.Point(3, 7);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(158, 23);
            this.lblTenDangNhap.TabIndex = 0;
            this.lblTenDangNhap.Text = "Tên Đăng Nhập :";
            // 
            // FDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 253);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập Hệ Thống";
            this.Load += new System.EventHandler(this.FDangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.quan_Ly_Sinh_VienDataSet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Quan_Ly_Sinh_VienDataSet quan_Ly_Sinh_VienDataSet1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txbPassWord;
        private System.Windows.Forms.Label lblPassWord;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.LinkLabel LnkQuenPass;
        private System.Windows.Forms.CheckBox chkPassWord;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExit;
    }
}

