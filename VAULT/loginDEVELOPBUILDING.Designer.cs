namespace VAULT
{
    partial class loginDEVELOPBUILDING
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginDEVELOPBUILDING));
            this.elipsecornersforloginform = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.developbuildingtext = new System.Windows.Forms.Label();
            this.passwordicon = new System.Windows.Forms.PictureBox();
            this.emailicon = new System.Windows.Forms.PictureBox();
            this.emailerror = new System.Windows.Forms.Label();
            this.passworderror = new System.Windows.Forms.Label();
            this.loginerror = new System.Windows.Forms.Label();
            this.DEVELOPBYLABEL = new System.Windows.Forms.Label();
            this.dropdownlanguage = new Bunifu.Framework.UI.BunifuDropdown();
            this.welcomelabel = new System.Windows.Forms.Label();
            this.loginbutton = new Bunifu.Framework.UI.BunifuFlatButton();
            this.dragcontrolforlogin = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.passwordtextbox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.emailtextbox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.logoline = new MaterialSkin.Controls.MaterialDivider();
            this.logo_company = new System.Windows.Forms.PictureBox();
            this.line1 = new MaterialSkin.Controls.MaterialDivider();
            this.forgetpass = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.telephone = new System.Windows.Forms.PictureBox();
            this.exitlabel = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.errornumberphone = new System.Windows.Forms.Label();
            this.errorpassforget = new System.Windows.Forms.Label();
            this.minimizebutton = new Bunifu.Framework.UI.BunifuImageButton();
            this.closebutton = new Bunifu.Framework.UI.BunifuImageButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.connectionerror = new System.Windows.Forms.Label();
            this.watchpassbutton = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.passwordicon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailicon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo_company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.telephone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizebutton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closebutton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.watchpassbutton)).BeginInit();
            this.SuspendLayout();
            // 
            // elipsecornersforloginform
            // 
            this.elipsecornersforloginform.ElipseRadius = 7;
            this.elipsecornersforloginform.TargetControl = this;
            // 
            // developbuildingtext
            // 
            this.developbuildingtext.BackColor = System.Drawing.Color.Transparent;
            this.developbuildingtext.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.developbuildingtext.ForeColor = System.Drawing.Color.White;
            this.developbuildingtext.Location = new System.Drawing.Point(12, 9);
            this.developbuildingtext.Name = "developbuildingtext";
            this.developbuildingtext.Size = new System.Drawing.Size(73, 33);
            this.developbuildingtext.TabIndex = 5;
            this.developbuildingtext.Text = "DEVELOP\r\nBUILDING";
            this.developbuildingtext.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // passwordicon
            // 
            this.passwordicon.BackColor = System.Drawing.Color.Transparent;
            this.passwordicon.Cursor = System.Windows.Forms.Cursors.Default;
            this.passwordicon.Image = ((System.Drawing.Image)(resources.GetObject("passwordicon.Image")));
            this.passwordicon.Location = new System.Drawing.Point(121, 277);
            this.passwordicon.Name = "passwordicon";
            this.passwordicon.Size = new System.Drawing.Size(33, 28);
            this.passwordicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.passwordicon.TabIndex = 11;
            this.passwordicon.TabStop = false;
            // 
            // emailicon
            // 
            this.emailicon.BackColor = System.Drawing.Color.Transparent;
            this.emailicon.Cursor = System.Windows.Forms.Cursors.Default;
            this.emailicon.Image = ((System.Drawing.Image)(resources.GetObject("emailicon.Image")));
            this.emailicon.Location = new System.Drawing.Point(121, 225);
            this.emailicon.Name = "emailicon";
            this.emailicon.Size = new System.Drawing.Size(33, 28);
            this.emailicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.emailicon.TabIndex = 12;
            this.emailicon.TabStop = false;
            // 
            // emailerror
            // 
            this.emailerror.AutoSize = true;
            this.emailerror.BackColor = System.Drawing.Color.Transparent;
            this.emailerror.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.emailerror.ForeColor = System.Drawing.Color.Red;
            this.emailerror.Location = new System.Drawing.Point(157, 256);
            this.emailerror.Name = "emailerror";
            this.emailerror.Size = new System.Drawing.Size(181, 15);
            this.emailerror.TabIndex = 15;
            this.emailerror.Text = "Datele introduse sunt incorecte.";
            this.emailerror.Visible = false;
            // 
            // passworderror
            // 
            this.passworderror.AutoSize = true;
            this.passworderror.BackColor = System.Drawing.Color.Transparent;
            this.passworderror.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.passworderror.ForeColor = System.Drawing.Color.Red;
            this.passworderror.Location = new System.Drawing.Point(157, 308);
            this.passworderror.Name = "passworderror";
            this.passworderror.Size = new System.Drawing.Size(181, 15);
            this.passworderror.TabIndex = 16;
            this.passworderror.Text = "Datele introduse sunt incorecte.";
            this.passworderror.Visible = false;
            // 
            // loginerror
            // 
            this.loginerror.AutoSize = true;
            this.loginerror.BackColor = System.Drawing.Color.Transparent;
            this.loginerror.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loginerror.ForeColor = System.Drawing.Color.Red;
            this.loginerror.Location = new System.Drawing.Point(157, 368);
            this.loginerror.Name = "loginerror";
            this.loginerror.Size = new System.Drawing.Size(162, 15);
            this.loginerror.TabIndex = 17;
            this.loginerror.Text = "Te rugăm să încerci din nou.";
            this.loginerror.Visible = false;
            // 
            // DEVELOPBYLABEL
            // 
            this.DEVELOPBYLABEL.AutoSize = true;
            this.DEVELOPBYLABEL.BackColor = System.Drawing.Color.Transparent;
            this.DEVELOPBYLABEL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DEVELOPBYLABEL.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DEVELOPBYLABEL.ForeColor = System.Drawing.Color.White;
            this.DEVELOPBYLABEL.Location = new System.Drawing.Point(157, 536);
            this.DEVELOPBYLABEL.Name = "DEVELOPBYLABEL";
            this.DEVELOPBYLABEL.Size = new System.Drawing.Size(85, 14);
            this.DEVELOPBYLABEL.TabIndex = 19;
            this.DEVELOPBYLABEL.Text = "DEZVOLTAT DE";
            this.DEVELOPBYLABEL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DEVELOPBYLABEL.Click += new System.EventHandler(this.DEVELOPBYLABEL_Click);
            // 
            // dropdownlanguage
            // 
            this.dropdownlanguage.BackColor = System.Drawing.Color.Transparent;
            this.dropdownlanguage.BorderRadius = 3;
            this.dropdownlanguage.DisabledColor = System.Drawing.Color.Gray;
            this.dropdownlanguage.ForeColor = System.Drawing.Color.White;
            this.dropdownlanguage.Items = new string[] {
        "RO",
        "EN"};
            this.dropdownlanguage.Location = new System.Drawing.Point(18, 44);
            this.dropdownlanguage.Name = "dropdownlanguage";
            this.dropdownlanguage.NomalColor = System.Drawing.Color.Transparent;
            this.dropdownlanguage.onHoverColor = System.Drawing.Color.Transparent;
            this.dropdownlanguage.selectedIndex = 0;
            this.dropdownlanguage.Size = new System.Drawing.Size(61, 19);
            this.dropdownlanguage.TabIndex = 20;
            this.dropdownlanguage.onItemSelected += new System.EventHandler(this.dropdownlanguage_onItemSelected);
            // 
            // welcomelabel
            // 
            this.welcomelabel.AutoSize = true;
            this.welcomelabel.BackColor = System.Drawing.Color.Transparent;
            this.welcomelabel.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.welcomelabel.Location = new System.Drawing.Point(116, 179);
            this.welcomelabel.Name = "welcomelabel";
            this.welcomelabel.Size = new System.Drawing.Size(220, 30);
            this.welcomelabel.TabIndex = 21;
            this.welcomelabel.Text = "Autentificare cont";
            this.welcomelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // loginbutton
            // 
            this.loginbutton.Activecolor = System.Drawing.Color.White;
            this.loginbutton.BackColor = System.Drawing.Color.White;
            this.loginbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loginbutton.BorderRadius = 7;
            this.loginbutton.ButtonText = "AUTENTIFICARE";
            this.loginbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginbutton.DisabledColor = System.Drawing.Color.Gray;
            this.loginbutton.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loginbutton.Iconcolor = System.Drawing.Color.Transparent;
            this.loginbutton.Iconimage = ((System.Drawing.Image)(resources.GetObject("loginbutton.Iconimage")));
            this.loginbutton.Iconimage_right = null;
            this.loginbutton.Iconimage_right_Selected = null;
            this.loginbutton.Iconimage_Selected = null;
            this.loginbutton.IconMarginLeft = 0;
            this.loginbutton.IconMarginRight = 0;
            this.loginbutton.IconRightVisible = true;
            this.loginbutton.IconRightZoom = 0D;
            this.loginbutton.IconVisible = true;
            this.loginbutton.IconZoom = 35D;
            this.loginbutton.IsTab = false;
            this.loginbutton.Location = new System.Drawing.Point(119, 327);
            this.loginbutton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Normalcolor = System.Drawing.Color.White;
            this.loginbutton.OnHovercolor = System.Drawing.Color.Gainsboro;
            this.loginbutton.OnHoverTextColor = System.Drawing.Color.Black;
            this.loginbutton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.loginbutton.selected = false;
            this.loginbutton.Size = new System.Drawing.Size(253, 37);
            this.loginbutton.TabIndex = 22;
            this.loginbutton.Text = "AUTENTIFICARE";
            this.loginbutton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loginbutton.Textcolor = System.Drawing.Color.Black;
            this.loginbutton.TextFont = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loginbutton.Click += new System.EventHandler(this.loginbutton_Click);
            // 
            // dragcontrolforlogin
            // 
            this.dragcontrolforlogin.Fixed = true;
            this.dragcontrolforlogin.Horizontal = true;
            this.dragcontrolforlogin.TargetControl = this;
            this.dragcontrolforlogin.Vertical = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // passwordtextbox
            // 
            this.passwordtextbox.BackColor = System.Drawing.Color.White;
            this.passwordtextbox.Depth = 0;
            this.passwordtextbox.Hint = "Parolă *";
            this.passwordtextbox.Location = new System.Drawing.Point(160, 282);
            this.passwordtextbox.MouseState = MaterialSkin.MouseState.HOVER;
            this.passwordtextbox.Name = "passwordtextbox";
            this.passwordtextbox.PasswordChar = '\0';
            this.passwordtextbox.SelectedText = "";
            this.passwordtextbox.SelectionLength = 0;
            this.passwordtextbox.SelectionStart = 0;
            this.passwordtextbox.Size = new System.Drawing.Size(253, 23);
            this.passwordtextbox.TabIndex = 101;
            this.passwordtextbox.TabStop = false;
            this.passwordtextbox.UseSystemPasswordChar = true;
            this.passwordtextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordtextbox_KeyDown);
            // 
            // emailtextbox
            // 
            this.emailtextbox.BackColor = System.Drawing.Color.White;
            this.emailtextbox.Depth = 0;
            this.emailtextbox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.emailtextbox.Hint = "Email *";
            this.emailtextbox.Location = new System.Drawing.Point(160, 230);
            this.emailtextbox.MouseState = MaterialSkin.MouseState.HOVER;
            this.emailtextbox.Name = "emailtextbox";
            this.emailtextbox.PasswordChar = '\0';
            this.emailtextbox.SelectedText = "";
            this.emailtextbox.SelectionLength = 0;
            this.emailtextbox.SelectionStart = 0;
            this.emailtextbox.Size = new System.Drawing.Size(253, 23);
            this.emailtextbox.TabIndex = 100;
            this.emailtextbox.TabStop = false;
            this.emailtextbox.UseSystemPasswordChar = false;
            // 
            // logoline
            // 
            this.logoline.BackColor = System.Drawing.Color.White;
            this.logoline.Depth = 0;
            this.logoline.Location = new System.Drawing.Point(9, 40);
            this.logoline.MouseState = MaterialSkin.MouseState.HOVER;
            this.logoline.Name = "logoline";
            this.logoline.Size = new System.Drawing.Size(77, 1);
            this.logoline.TabIndex = 27;
            // 
            // logo_company
            // 
            this.logo_company.BackColor = System.Drawing.Color.Transparent;
            this.logo_company.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo_company.Image = ((System.Drawing.Image)(resources.GetObject("logo_company.Image")));
            this.logo_company.Location = new System.Drawing.Point(279, 522);
            this.logo_company.Name = "logo_company";
            this.logo_company.Size = new System.Drawing.Size(66, 35);
            this.logo_company.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo_company.TabIndex = 102;
            this.logo_company.TabStop = false;
            this.logo_company.Click += new System.EventHandler(this.logo_company_Click);
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.Black;
            this.line1.Depth = 0;
            this.line1.Location = new System.Drawing.Point(121, 409);
            this.line1.MouseState = MaterialSkin.MouseState.HOVER;
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(292, 2);
            this.line1.TabIndex = 104;
            // 
            // forgetpass
            // 
            this.forgetpass.AutoSize = true;
            this.forgetpass.BackColor = System.Drawing.Color.Transparent;
            this.forgetpass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.forgetpass.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.forgetpass.ForeColor = System.Drawing.Color.White;
            this.forgetpass.Location = new System.Drawing.Point(308, 414);
            this.forgetpass.Name = "forgetpass";
            this.forgetpass.Size = new System.Drawing.Size(105, 16);
            this.forgetpass.TabIndex = 106;
            this.forgetpass.Text = "Mi-am uitat parola.";
            this.forgetpass.Click += new System.EventHandler(this.forgetpass_Click);
            this.forgetpass.MouseEnter += new System.EventHandler(this.forgetpass_MouseEnter);
            this.forgetpass.MouseLeave += new System.EventHandler(this.forgetpass_MouseLeave);
            // 
            // telephone
            // 
            this.telephone.BackColor = System.Drawing.Color.Transparent;
            this.telephone.Cursor = System.Windows.Forms.Cursors.Default;
            this.telephone.Image = ((System.Drawing.Image)(resources.GetObject("telephone.Image")));
            this.telephone.Location = new System.Drawing.Point(121, 277);
            this.telephone.Name = "telephone";
            this.telephone.Size = new System.Drawing.Size(33, 28);
            this.telephone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.telephone.TabIndex = 107;
            this.telephone.TabStop = false;
            this.telephone.Visible = false;
            // 
            // exitlabel
            // 
            this.exitlabel.AutoSize = true;
            this.exitlabel.BackColor = System.Drawing.Color.Transparent;
            this.exitlabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitlabel.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exitlabel.ForeColor = System.Drawing.Color.White;
            this.exitlabel.Location = new System.Drawing.Point(118, 414);
            this.exitlabel.Name = "exitlabel";
            this.exitlabel.Size = new System.Drawing.Size(56, 16);
            this.exitlabel.TabIndex = 108;
            this.exitlabel.Text = "Renunță.";
            this.exitlabel.Visible = false;
            this.exitlabel.Click += new System.EventHandler(this.exitlabel_Click);
            this.exitlabel.MouseEnter += new System.EventHandler(this.exitlabel_MouseEnter);
            this.exitlabel.MouseLeave += new System.EventHandler(this.exitlabel_MouseLeave);
            // 
            // errornumberphone
            // 
            this.errornumberphone.AutoSize = true;
            this.errornumberphone.BackColor = System.Drawing.Color.Transparent;
            this.errornumberphone.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.errornumberphone.ForeColor = System.Drawing.Color.Red;
            this.errornumberphone.Location = new System.Drawing.Point(157, 308);
            this.errornumberphone.Name = "errornumberphone";
            this.errornumberphone.Size = new System.Drawing.Size(294, 15);
            this.errornumberphone.TabIndex = 110;
            this.errornumberphone.Text = "Numărul de telefon nu corespunde cu cel înregistrat.";
            this.errornumberphone.Visible = false;
            // 
            // errorpassforget
            // 
            this.errorpassforget.AutoSize = true;
            this.errorpassforget.BackColor = System.Drawing.Color.Transparent;
            this.errorpassforget.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.errorpassforget.ForeColor = System.Drawing.Color.Red;
            this.errorpassforget.Location = new System.Drawing.Point(157, 368);
            this.errorpassforget.Name = "errorpassforget";
            this.errorpassforget.Size = new System.Drawing.Size(162, 15);
            this.errorpassforget.TabIndex = 111;
            this.errorpassforget.Text = "Te rugăm să încerci din nou.";
            this.errorpassforget.Visible = false;
            // 
            // minimizebutton
            // 
            this.minimizebutton.BackColor = System.Drawing.Color.Transparent;
            this.minimizebutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimizebutton.Image = ((System.Drawing.Image)(resources.GetObject("minimizebutton.Image")));
            this.minimizebutton.ImageActive = null;
            this.minimizebutton.Location = new System.Drawing.Point(450, 9);
            this.minimizebutton.Name = "minimizebutton";
            this.minimizebutton.Size = new System.Drawing.Size(30, 30);
            this.minimizebutton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimizebutton.TabIndex = 112;
            this.minimizebutton.TabStop = false;
            this.minimizebutton.Tag = "";
            this.minimizebutton.Zoom = 10;
            this.minimizebutton.Click += new System.EventHandler(this.minimizebutton_Click);
            // 
            // closebutton
            // 
            this.closebutton.BackColor = System.Drawing.Color.Transparent;
            this.closebutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closebutton.Image = ((System.Drawing.Image)(resources.GetObject("closebutton.Image")));
            this.closebutton.ImageActive = null;
            this.closebutton.Location = new System.Drawing.Point(486, 9);
            this.closebutton.Name = "closebutton";
            this.closebutton.Size = new System.Drawing.Size(30, 30);
            this.closebutton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.closebutton.TabIndex = 113;
            this.closebutton.TabStop = false;
            this.closebutton.Tag = "";
            this.closebutton.Zoom = 10;
            this.closebutton.Click += new System.EventHandler(this.closebutton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(415, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 115;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(414, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 116;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // connectionerror
            // 
            this.connectionerror.AutoSize = true;
            this.connectionerror.BackColor = System.Drawing.Color.Transparent;
            this.connectionerror.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.connectionerror.ForeColor = System.Drawing.Color.Red;
            this.connectionerror.Location = new System.Drawing.Point(157, 368);
            this.connectionerror.Name = "connectionerror";
            this.connectionerror.Size = new System.Drawing.Size(230, 15);
            this.connectionerror.TabIndex = 118;
            this.connectionerror.Text = "Nu sunteti conectat la o rețea de internet.";
            this.connectionerror.Visible = false;
            // 
            // watchpassbutton
            // 
            this.watchpassbutton.BackColor = System.Drawing.Color.Transparent;
            this.watchpassbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.watchpassbutton.Image = ((System.Drawing.Image)(resources.GetObject("watchpassbutton.Image")));
            this.watchpassbutton.ImageActive = null;
            this.watchpassbutton.Location = new System.Drawing.Point(415, 287);
            this.watchpassbutton.Name = "watchpassbutton";
            this.watchpassbutton.Size = new System.Drawing.Size(29, 22);
            this.watchpassbutton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.watchpassbutton.TabIndex = 219;
            this.watchpassbutton.TabStop = false;
            this.watchpassbutton.Tag = "";
            this.watchpassbutton.Zoom = 10;
            this.watchpassbutton.MouseEnter += new System.EventHandler(this.watchpassbutton_MouseEnter);
            this.watchpassbutton.MouseLeave += new System.EventHandler(this.watchpassbutton_MouseLeave);
            // 
            // loginDEVELOPBUILDING
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(523, 560);
            this.Controls.Add(this.watchpassbutton);
            this.Controls.Add(this.connectionerror);
            this.Controls.Add(this.minimizebutton);
            this.Controls.Add(this.closebutton);
            this.Controls.Add(this.exitlabel);
            this.Controls.Add(this.forgetpass);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.logo_company);
            this.Controls.Add(this.logoline);
            this.Controls.Add(this.emailicon);
            this.Controls.Add(this.emailtextbox);
            this.Controls.Add(this.passwordtextbox);
            this.Controls.Add(this.loginbutton);
            this.Controls.Add(this.DEVELOPBYLABEL);
            this.Controls.Add(this.dropdownlanguage);
            this.Controls.Add(this.welcomelabel);
            this.Controls.Add(this.loginerror);
            this.Controls.Add(this.emailerror);
            this.Controls.Add(this.passworderror);
            this.Controls.Add(this.passwordicon);
            this.Controls.Add(this.developbuildingtext);
            this.Controls.Add(this.errornumberphone);
            this.Controls.Add(this.telephone);
            this.Controls.Add(this.errorpassforget);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "loginDEVELOPBUILDING";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DEVELOPBUILDING";
            ((System.ComponentModel.ISupportInitialize)(this.passwordicon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailicon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo_company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.telephone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizebutton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closebutton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.watchpassbutton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse elipsecornersforloginform;
        private System.Windows.Forms.Label developbuildingtext;
        private System.Windows.Forms.Label DEVELOPBYLABEL;
        private Bunifu.Framework.UI.BunifuDropdown dropdownlanguage;
        private System.Windows.Forms.Label welcomelabel;
        private System.Windows.Forms.Label loginerror;
        private System.Windows.Forms.PictureBox emailicon;
        private System.Windows.Forms.Label emailerror;
        private System.Windows.Forms.Label passworderror;
        private System.Windows.Forms.PictureBox passwordicon;
        private Bunifu.Framework.UI.BunifuFlatButton loginbutton;
        private Bunifu.Framework.UI.BunifuDragControl dragcontrolforlogin;
        private System.Windows.Forms.Timer timer1;
        private MaterialSkin.Controls.MaterialSingleLineTextField passwordtextbox;
        private MaterialSkin.Controls.MaterialSingleLineTextField emailtextbox;
        private MaterialSkin.Controls.MaterialDivider logoline;
        private System.Windows.Forms.PictureBox logo_company;
        private MaterialSkin.Controls.MaterialDivider line1;
        private Bunifu.Framework.UI.BunifuCustomLabel forgetpass;
        private System.Windows.Forms.PictureBox telephone;
        private Bunifu.Framework.UI.BunifuCustomLabel exitlabel;
        private System.Windows.Forms.Label errorpassforget;
        private System.Windows.Forms.Label errornumberphone;
        private Bunifu.Framework.UI.BunifuImageButton minimizebutton;
        private Bunifu.Framework.UI.BunifuImageButton closebutton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label connectionerror;
        private Bunifu.Framework.UI.BunifuImageButton watchpassbutton;
    }
}

