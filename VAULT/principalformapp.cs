using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Xml;
using System.Diagnostics;

namespace VAULT
{
    public partial class principalformapp : Form
    {
        //---------------------------------------------------------------------------------code and variable for googlemaps--------------------------
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        double LatInitial = 44.4268078899587;
        double LngInitial = 26.102442741394;
        //------------------------------------------------------------------------------------------------VARIABLE DECLARATIONS---------------------
        public bool messageboxshow = false;
        public bool messageboxresult = false;

        public int markerinitialize = 0;
        public int counterseccondforvalidateerror = 0;
        public int manageroragent;
        //--------------------------------------------------------------------------------------------------------------------------------------------
        public principalformapp()
        {
            InitializeComponent();
            try
            {
                //------------------------------------------------------------------------------------------------------BNR xml---------------------------
                xmlbnr();
                if (dropdownlanguage.selectedValue == "RO") { label54.Text = "Cursul valutar BNR comunicat în " + databnrvalutecurrent + " este: EUR=" + eurcoinvalue + " LEI" + " USD=" + usdcoinvalue + " LEI" + " CHF=" + chfcoinvalue + " LEI" + " GBP=" + gbpcoinvalue + " LEI"; }
                //----------------------------------------------------------------------------------------------------------------------------------------
                dashboard_panel.Visible = true;//bringtofront dashboard panel when form is initialize
                slidebarmenu.BringToFront();
                LastloginAndNumberOflogin();
                ProfilepictureAndNameFuction();
                LaguageChange();
                //initialize new mousewheel for all data grid in app------------------------------------------------------------------
                this.teamgrid.ScrollBars = ScrollBars.None;
                this.teamgrid.MouseWheel += new MouseEventHandler(teamgrid_mousewheel);
                this.bunifuCustomDataGrid1.ScrollBars = ScrollBars.None;
                this.bunifuCustomDataGrid1.MouseWheel += new MouseEventHandler(bunifuCustomGrid1_mousewheel);

                //--------------------------------------------------------------------------------------------------------------------
                SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Select * from team where email=@email2";
                cmd.Parameters.AddWithValue("@email2", loginDEVELOPBUILDING.Email);

                con.Open();
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da1.Fill(ds);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    manageroragent = Convert.ToInt16(dr["role"].ToString());

                }
                con.Close();
                if (manageroragent == 0)
                {
                    addteammate_button.Visible = true;
                    editteammate_button.Visible = true;
                    passwordreset_button.Visible = true;
                    fire_button.Visible = true;
                    bunifuDropdown1.Visible = true;
                    materialDivider13.Visible = true;
                }
                else
                {
                    addteammate_button.Visible = false;
                    teamlist_button.Location = new Point(1022, 3);
                    statisticsbutton.Visible = false;
                    teambutton.Location = new Point(0, 434);
                    websitebutton.Visible = false;

                }
            }
            catch (Exception e) { e.ToString(); throw; }
        }

        private void currenttimeanddate_Tick(object sender, EventArgs e)//real time date and time
        {
            string currenttime = DateTime.Now.ToLongTimeString();
            string currentdate = DateTime.Now.ToShortDateString();
            dateandtime.Text = currenttime + " - " + currentdate;
        }
        //this block change color of prgoressbar when mouse enter and leave of the profile picture box
        private void profilepicture_MouseEnter(object sender, EventArgs e)
        {
            profilepicture.BorderColor = System.Drawing.Color.FromArgb(37, 157, 201);
        }

        private void profilepicture_MouseLeave(object sender, EventArgs e)
        {
            profilepicture.BorderColor = System.Drawing.Color.Gainsboro;
        }
        public string databnrvalutecurrent, eurcoinvalue, usdcoinvalue, chfcoinvalue, gbpcoinvalue;
        //------------------------------------------------------------------------------------------------------------------------------------------
        private void xmlbnr()
        {
            string currentdata = string.Empty;
            string coin = string.Empty;
            string value = string.Empty;
            DataTable dt10 = new DataTable();
            dt10.Columns.Add("Date", typeof(string));
            dt10.Columns.Add("Coin", typeof(string));
            dt10.Columns.Add("Value", typeof(string));
            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.bnr.ro/nbrfxrates.xml");
            XmlNodeList xmlDate = doc.GetElementsByTagName("Cube");
            XmlNodeList listdata = doc.GetElementsByTagName("Rate");
            foreach (XmlNode item in xmlDate)
            {
                currentdata = item.Attributes["date"].Value.ToString();
            }
            foreach (XmlNode item in listdata)
            {
                coin = item.Attributes["currency"].Value.ToString();
                value = item.InnerText;
                dt10.Rows.Add(currentdata, coin, value);
            }
            DataSet ds = new DataSet();
            foreach (DataRow dr in dt10.Rows)
            {
                string databnrvalute = dr["Date"].ToString();
                string eur = dr["Coin"].ToString();
                string gbp = dr["Coin"].ToString();
                string usd = dr["Coin"].ToString();
                string CHF = dr["Coin"].ToString();
                if (eur == "EUR") { databnrvalutecurrent = dr["Date"].ToString(); }

                if (eur == "EUR") { eurcoinvalue = dr["Value"].ToString(); }
                if (usd == "USD") { usdcoinvalue = dr["Value"].ToString(); }
                if (gbp == "GBP") { gbpcoinvalue = dr["Value"].ToString(); }
                if (CHF == "CHF") { chfcoinvalue = dr["Value"].ToString(); }
            }
        }
        public string language;
        private void LaguageChange()
        {

            SqlConnection con = new SqlConnection(stringcon); //CONNECTION
            cmd.Connection = con;
            cmd.CommandText = "Select * from team where email=@email";
            cmd.Parameters.AddWithValue("@email", loginDEVELOPBUILDING.Email);

            con.Open();
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                string language = dr["language"].ToString();
                if (language == "EN") { dropdownlanguage.selectedIndex = 1; } else { dropdownlanguage.selectedIndex = 0; }

            }
            con.Close();

        }

        private void ProfilepictureAndNameFuction()
        {
            string agentname;// take this from database
            string agentpremane;// take this from database

            SqlConnection con = new SqlConnection(stringcon);

            SqlCommand cmd = new SqlCommand("select profilepicture from team where email=@EMAIL", con);
            SqlCommand cmd2 = new SqlCommand("select lastname from team where email=@EMAIL", con);
            SqlCommand cmd3 = new SqlCommand("select firstname from team where email=@EMAIL", con);
            SqlCommand cmd4 = new SqlCommand("select [role] from team where email=@EMAIL", con);

            cmd.Parameters.AddWithValue("@EMAIL", loginDEVELOPBUILDING.Email);
            cmd2.Parameters.AddWithValue("@EMAIL", loginDEVELOPBUILDING.Email);
            cmd3.Parameters.AddWithValue("@EMAIL", loginDEVELOPBUILDING.Email);
            cmd4.Parameters.AddWithValue("@EMAIL", loginDEVELOPBUILDING.Email);

            SqlDataAdapter dt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataSet ds12 = new DataSet();
            dt.Fill(ds);
            con.Open();
            Object temp = cmd4.ExecuteScalar();
            int tem = Convert.ToInt16(temp.ToString());
            if (tem == 0) { functionlabel.Text = "Manager"; } else { functionlabel.Text = "Agent"; }
            Object temp_dash = cmd3.ExecuteScalar();
            agentfullnamevieww.Text = temp_dash.ToString();
            agentpremane = temp_dash.ToString();
            Object temp_dash1 = cmd2.ExecuteScalar();
            agentfullnamevieww.Text = temp_dash.ToString();
            agentname = temp_dash1.ToString();
            string agentfullname = agentname + " " + agentpremane;
            agentfullnamevieww.Text = agentfullname;
            con.Close();
            byte[] ap = (byte[])ds.Tables[0].Rows[0]["profilepicture"];
            MemoryStream ms = new MemoryStream(ap);
            profilepicture.Image = Image.FromStream(ms);
            //show datagridview columns
            con.Open();
        }

        //--------------------------------------------------------------------------------------------
        private void slidemenu()//function for slidebarmenu
        {
            if (slidebarmenu.Width == 50)
            {
                slidebarmenu.BringToFront();
                slidebarmenu.Visible = false;
                slidebarmenu.Width = 175;
                slidemenu_animator.ShowSync(slidebarmenu);
                animatorforrestofcomponents.Show(profilepicture);
                animatorforrestofcomponents.Show(agentfullnamevieww);
                animatorforrestofcomponents.Show(functionlabel);
                animatorforrestofcomponents.Show(versioncontrollabel);
                animatorforrestofcomponents.Show(DEVELOPBYLABEL);
                animatorforrestofcomponents.Show(logo_company);
                animatorforrestofcomponents.Hide(logo_company1);
                logo_company1.Visible = false;
                autoslidemenu.Start();
            }
            else
            {
                animatorforrestofcomponents.Hide(profilepicture);
                animatorforrestofcomponents.Hide(agentfullnamevieww);
                animatorforrestofcomponents.Hide(functionlabel);
                animatorforrestofcomponents.Hide(versioncontrollabel);
                animatorforrestofcomponents.Hide(DEVELOPBYLABEL);
                animatorforrestofcomponents.Hide(logo_company);
                logo_company1.Visible = true;
                animatorforrestofcomponents.Show(logo_company1);
                slidebarmenu.Visible = false;
                slidebarmenu.Width = 50;
                slidemenu_animator.ShowSync(slidebarmenu);
                autoslidemenu.Stop();
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)//call function to slide menu
        {
            slidemenu();
        }

        private void autoslidemenu_Tick(object sender, EventArgs e)//auto call menu if is width=175 after 15 seconds
        {
            if (slidebarmenu.Width == 175)
            {
                slidemenu();
                autoslidemenu.Stop();
            }
        }

        private void close_button_Click(object sender, EventArgs e)// close the application
        {
            Application.Exit();
        }

        private void infolabelmove_Tick(object sender, EventArgs e)
        {
            if (label54.Left < -700) { label54.Left = label54.Left + 1800; } else { label54.Left -= 1; }//code for infolabel moved
        }

        private void minimize_button_Click(object sender, EventArgs e)//minimize this form
        {
            WindowState = FormWindowState.Minimized;
        }

        private void logoutbutton_Click(object sender, EventArgs e)//logout button
        {
            var loginform = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is loginDEVELOPBUILDING);
            if (loginform != null)
            {
                if (slidebarmenu.Width == 175)
                {
                    animatorforrestofcomponents.Hide(profilepicture);
                    animatorforrestofcomponents.Hide(agentfullnamevieww);
                    animatorforrestofcomponents.Hide(functionlabel);
                    animatorforrestofcomponents.Hide(versioncontrollabel);
                    animatorforrestofcomponents.Hide(DEVELOPBYLABEL);
                    animatorforrestofcomponents.Hide(logo_company);
                    logo_company1.Visible = true;
                    animatorforrestofcomponents.Show(logo_company1);
                    slidebarmenu.Visible = false;
                    slidebarmenu.Width = 50;
                    slidemenu_animator.ShowSync(slidebarmenu);
                    autoslidemenu.Stop();
                    loginform.Show();
                    this.Close();
                }
                else
                {
                    loginform.Show();
                    this.Close();
                }
            }
        }
        //----------------------------------------------------------------------------------------------BLOCK FOR LANGUAGE AND MESSAGEBOXES----------------
        private void dropdownlanguage_onItemSelected(object sender, EventArgs e)
        {
            if (dropdownlanguage.selectedValue == "EN")//english language
            {
                SqlConnection con2 = new SqlConnection(stringcon); //CONNECTION
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.Clear();
                cmd.Connection = con2;
                cmd.CommandText = "update dbo.team set language=@lang where email=@email";
                cmd.Parameters.AddWithValue("@lang", dropdownlanguage.selectedValue);
                cmd.Parameters.AddWithValue("@email", loginDEVELOPBUILDING.Email);

                con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
                label54.Text = "The BNR exchange rate communicated in " + databnrvalutecurrent + " is: EUR =" + eurcoinvalue + " LEI" + " USD=" + usdcoinvalue + " LEI" + " CHF=" + chfcoinvalue + " LEI" + " GBP=" + gbpcoinvalue + " LEI";

                //------------------------------------------------------------------------------TEAM-------------------------
                clientsbutton.Text = "   Clients";
                propertiesbutton.Text = "   Properties";
                projectsbutton.Text = "   Projects";
                financialbutton.Text = "   Financial";
                statisticsbutton.Text = "   Statistics";
                teambutton.Text = "   Team";
                DEVELOPBYLABEL.Text = "DEVELOPED BY";
                addteammate_button.Text = "   ADD MEMBER";
                label1.Text = "Add member to the team";
                label2.Text = "Fields marked with star (*) are required to add a new member to the team.";
                label3.Text = "Profile picture";
                label4.Text = "Click on the photo to add or change profile photo.";
                firstname_textbox.Hint = "Firstname *";
                lastname_textbox.Hint = "Lastname *";
                phone_textbox.Hint = "Phone *";
                label14.Text = "The phone number must begin with the country prefix(ex. 44 0712 345678).";
                label15.Text = "ex. myemail@domain.com";
                function_textbox.Hint = "Function in the company";
                label5.Text = "Personal description";
                validateaddmember_button.Text = "ADD MEMBER";
                label6.Text = "This field is empty.";
                label7.Text = "This field is empty.";
                label8.Text = "This field is empty.";
                label9.Text = "This field is empty.";
                label10.Text = "This field is empty.";
                label11.Text = "This field is empty.";
                label12.Text = "Passwords not match.";
                teamlist_button.Text = "TEAM LIST";
                password_textbox.Hint = "Password *";
                repeatpassword_textbox.Hint = "Repeat password *";
                bunifuCustomLabel6.Text = "Data was successfully inserted.";
                label13.Text = "    ID                     Name                                Active                                Rank                         Customers Added            Logins in system                  Last login                   Registration date";
                data_registerlabel.Text = "Member registration date :";
                profilepictureinterogate_label.Text = "Profile picture";
                personaledata_label.Text = "Personal data";
                firstname_label.Text = "First name :";
                lastname_label.Text = "Last name :";
                agentphonenumber_label.Text = "Phone number :";
                functionincomplany_label.Text = "Function :";
                roleagent_label.Text = "Rank :";
                asociatecielnts_label.Text = "Customers added :";
                propertiesadd_label.Text = "Properties added :";
                personaldescriprion.Text = "Personal description";
                editteammate_button.Text = "EDIT";
                passwordreset_button.Text = "CHANGE PASSWORD";
                fire_button.Text = "DISMISS";
                label35.Text = "Profile picture";
                label34.Text = "Click on the photo to change the profile picture.";
                materialSingleLineTextField7.Hint = "First name *";
                materialSingleLineTextField6.Hint = "Last name *";
                materialSingleLineTextField4.Hint = "Phone *";
                label16.Text = "ex. myemail@domain.com";
                label25.Text = "The phone number must begin with the country prefix(ex. 44 0712 345678).";
                materialSingleLineTextField3.Hint = "Function in the company";
                label33.Text = "Personal description";
                materialSingleLineTextField2.Hint = "New password *";
                materialSingleLineTextField1.Hint = "Repeat new password *";
                if (label37.Text == "Editează profilul" || label37.Text == "Edit profile")
                { label37.Text = "Edit profile"; label36.Text = "You can edit any field. All fields are required."; bunifuFlatButton1.Text = "EDIT"; }
                else
                { label37.Text = "Change password"; label36.Text = "The fields for changing the password must be completed and coincide."; bunifuFlatButton1.Text = "CHANGE PASSWORD"; }
                label32.Text = "This field is empty.";
                label31.Text = "This field is empty.";
                label30.Text = "This field is empty.";
                label29.Text = "This field is empty.";
                label28.Text = "This field is empty.";
                label27.Text = "This field is empty.";
                label26.Text = "Passwords not match.";
                label45.Text = "You need to add a profile photo.";
                materialSingleLineTextField9.Hint = "Member name";
                label47.Text = "No";
                label46.Text = "Yes";
                bunifuFlatButton4.Text = "RESET FILTERS";
                //----------------------------------------------------------------------------CLIENTS------------------------------------------
                bunifuFlatButton2.Text = "   ADD CLIENT";
                bunifuFlatButton3.Text = "CLIENTS LIST";
                bunifuFlatButton5.Text = "RESET FILTERS";
                label50.Text = "    ID                     Name                                Active                                Rank                         Customers Added            Logins in system                  Last login                   Registration date";
                //-----------------------------------------------------------------------------PROJECT------------------------------------------
                bunifuFlatButton8.Text = "PROJECTS LIST";
                bunifuFlatButton9.Text = "ADD PROJECT";
                label58.Text = "Add real estate project";
                label68.Text = "To add a real estate project you need to fill in the add-on form. \nThe fields marked with star (*) are required.";
                label69.Text = "To set the project location you need to position the pin on the map.";
                label80.Text = "Select the project location on the map.";
                label74.Text = "Project details";
                label62.Text = "Project location";
                materialSingleLineTextField19.Hint = "Project name *";
                project_region.Hint = "County *";
                project_city.Hint = "City *";
                materialSingleLineTextField11.Hint = "Due date *";
                materialSingleLineTextField17.Hint = "The exact address *";
                materialSingleLineTextField10.Hint = "Energetic efficiency";
                materialCheckBox1.Text = "Surface parking";
                materialCheckBox2.Text = "Underground parking";
                materialCheckBox3.Text = "Lodge (storage)";
                label67.Text = "Project description";
                label72.Text = "Select the view on the website :";
                materialRadioButton1.Text = "Satellite";
                materialRadioButton2.Text = "Standard";
                materialRadioButton3.Text = "Relieve";
                label76.Text = "Image Gallery";
                label79.Text = "Click on the field to add a project image.";
                bunifuFlatButton10.Text = "ADD PROJECT";
                materialSingleLineTextField20.Hint = "Image Title 1";
                materialSingleLineTextField21.Hint = "Image Title 2";
                materialSingleLineTextField22.Hint = "Image Title 3";
                materialSingleLineTextField23.Hint = "Image Title 4";
                materialSingleLineTextField24.Hint = "Image Title 5";
                label77.Text = "This field is empty.";
                label75.Text = "This field is empty.";
                label66.Text = "This field is empty.";
                label63.Text = "This field is empty.";
                label73.Text = "This field is empty.";
                label78.Text = "You must add 5(five) pictures.";
                bunifuFlatButton7.Text = "RESET FILTERS";
                bunifuDropdown2.Clear();
                bunifuDropdown2.AddItem(Convert.ToString("Completed"));
                bunifuDropdown2.AddItem(Convert.ToString("Underconstruction"));
                bunifuDropdown2.AddItem(Convert.ToString("Design"));
                bunifuDropdown2.selectedIndex = 0;
                label56.Text = "    ID             Agent name                Gallery                    Project name                   Sales status         Building status            Properties                          Email                     Registration date";
                label85.Text = "Project registration date :";
                label88.Text = "Project edit date :";
                label89.Text = "Project details";
                label111.Text = "Building details";
                label90.Text = "Project location";
                bunifuFlatButton13.Text = "ASSOCIATED PROPERTIES LIST";
                bunifuFlatButton14.Text = "PROJECT CLIENTS";
                label82.Text = "Project name :";
                label94.Text = "Project Email :";
                label96.Text = "Due date :";
                label100.Text = "Sale status :";
                label102.Text = "Construction status :";
                label104.Text = "Surface parking :";
                label106.Text = "Underground parking :";
                label108.Text = "Lodge (storage) :";
                label110.Text = "Energetic efficiency :";
                label112.Text = "Project documents";
                label86.Text = "Project descriprion";
                label84.Text = "Image gallery";
                bunifuFlatButton11.Text = "EDIT";
                label136.Text = "To edit the project, you need to modify the data in the fields you want to edit.\nFields marked with star (*) are required.";
                label133.Text = "To change the project location you need to position the pin on the map.";
                label128.Text = "Project details";
                label129.Text = "Project location";
                materialSingleLineTextField32.Hint = "Project name *";
                materialSingleLineTextField36.Hint = "County *";
                label127.Text = "This field is empty.";
                materialSingleLineTextField35.Hint = "City *";
                materialSingleLineTextField30.Hint = "Due date *";
                materialSingleLineTextField33.Hint = "Exact address *";
                label126.Text = "This field is empty.";
                label130.Text = "This field is empty.";
                materialSingleLineTextField34.Hint = "Energetic efficiency";
                materialCheckBox6.Text = "Surface parking";
                materialCheckBox5.Text = "UnderGround parking";
                materialCheckBox4.Text = "Lodge (storage)";
                label125.Text = "Project description";
                label131.Text = "Select the view on the website :";
                materialRadioButton6.Text = "Satellite";
                materialRadioButton5.Text = "Standard";
                materialRadioButton4.Text = "Relieve";
                label138.Text = "Project documents";
                file1.FileLocation = "Document path";
                file2.FileLocation = "Document path";
                file3.FileLocation = "Document path";
                materialSingleLineTextField37.Hint = "Document Title 1";
                materialSingleLineTextField38.Hint = "Document Title 2";
                materialSingleLineTextField39.Hint = "Document Title 3";
                label123.Text = "Image gallery";
                label122.Text = "Click on the field to change a project image.";
                label120.Text = "Documents are not required.";
                materialSingleLineTextField29.Hint = "Image Title 1";
                materialSingleLineTextField28.Hint = "Image Title 2";
                materialSingleLineTextField27.Hint = "Image Title 3";
                materialSingleLineTextField26.Hint = "Image Title 4";
                materialSingleLineTextField25.Hint = "Image Title 5";
            }
            else//romanian language
            {
                SqlConnection con2 = new SqlConnection(stringcon); //CONNECTION
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.Clear();
                cmd.Connection = con2;
                cmd.CommandText = "update dbo.team set language=@lang where email=@email";
                cmd.Parameters.AddWithValue("@lang", dropdownlanguage.selectedValue);
                cmd.Parameters.AddWithValue("@email", loginDEVELOPBUILDING.Email);

                con2.Open();
                cmd.ExecuteNonQuery();
                con2.Close();
                label54.Text = "Cursul valutar BNR comunicat în " + databnrvalutecurrent + " este: EUR=" + eurcoinvalue + " LEI" + " USD=" + usdcoinvalue + " LEI" + " CHF=" + chfcoinvalue + " LEI" + " GBP=" + gbpcoinvalue + " LEI";

                //----------------------------------------------TEAM------------------------------------------
                clientsbutton.Text = "   Clienți";
                propertiesbutton.Text = "   Proprietăți";
                projectsbutton.Text = "   Proiecte";
                financialbutton.Text = "   Financiar";
                statisticsbutton.Text = "   Statistici";
                teambutton.Text = "   Echipă";
                DEVELOPBYLABEL.Text = "DEZVOLTAT DE";
                addteammate_button.Text = "ADAUGĂ MEMBRU";
                label1.Text = "Adaugă membru in echipă";
                label2.Text = "Câmpurile marcate cu steluță (*) sunt obligatorii pentru a adăuga un membru nou in echipă.";
                label3.Text = "Poză de profil";
                label4.Text = "Click pe poză pentru a adăuga sau schimba poza de profil.";
                firstname_textbox.Hint = "Prenume *";
                lastname_textbox.Hint = "Nume *";
                phone_textbox.Hint = "Telefon *";
                label14.Text = "Numărul de telefon trebuie să înceapă cu prefixul de țară(ex. 44 0712 345678).";
                label15.Text = "ex. emailulmeu@domeniu.ro";
                function_textbox.Hint = "Funcția în companie";
                label5.Text = "Descriere personală";
                validateaddmember_button.Text = "   ADAUGĂ MEMBRU";
                label6.Text = "Acest câmp este gol.";
                label7.Text = "Acest câmp este gol.";
                label8.Text = "Acest câmp este gol.";
                label9.Text = "Acest câmp este gol.";
                label10.Text = "Acest câmp este gol.";
                label11.Text = "Acest câmp este gol.";
                label12.Text = "Parolele nu coincid.";
                teamlist_button.Text = "LISTĂ ECHIPĂ";
                password_textbox.Hint = "Parolă *";
                repeatpassword_textbox.Hint = "Repetă parola *";
                bunifuCustomLabel6.Text = "Datele au fost inserate cu succes.";
                label13.Text = "    ID                     Nume                                 Activ                                 Gradul                     Clienți înregistrați             Logări în sistem                 Ultima logare                Dată înregistrare";
                data_registerlabel.Text = "Dată înregistrare membru :";
                profilepictureinterogate_label.Text = "Poză de profil";
                personaledata_label.Text = "Date personale";
                firstname_label.Text = "Prenume :";
                lastname_label.Text = "Nume :";
                agentphonenumber_label.Text = "Număr de telefon :";
                functionincomplany_label.Text = "Funcția :";
                roleagent_label.Text = "Gradul :";
                asociatecielnts_label.Text = "Clienți adăugați :";
                propertiesadd_label.Text = "Proprietăți adăugate :";
                personaldescriprion.Text = "Descriere personală";
                editteammate_button.Text = "EDITEAZĂ";
                passwordreset_button.Text = "SCHIMBĂ PAROLA";
                fire_button.Text = "CONCEDIAZĂ";
                label35.Text = "Poză de profil";
                label34.Text = "Faceți clic pe poză pentru a schimba poza de profil.";
                materialSingleLineTextField7.Hint = "Prenume *";
                materialSingleLineTextField6.Hint = "Nume *";
                materialSingleLineTextField4.Hint = "Telefon *";
                label16.Text = "ex. emailulmeu@domeniu.ro";
                label25.Text = "Numărul de telefon trebuie să înceapă cu prefixul de țară(ex. 44 0712 345678).";
                materialSingleLineTextField3.Hint = "Funcția în companie";
                label33.Text = "Descriere personală";
                materialSingleLineTextField2.Hint = "Parolă nouă *";
                materialSingleLineTextField1.Hint = "Repetă parola nouă *";
                if (label37.Text == "Editează profilul" || label37.Text == "Edit profile")
                { label37.Text = "Editează profilul"; label36.Text = "Poți edita orice câmp. Toate câmpurile sunt obligatorii."; bunifuFlatButton1.Text = "EDITEAZĂ"; }
                else
                { label37.Text = "Schimbă parola"; label36.Text = "Câmpurile specifice schimbării parolei trebuie să fie completate și să coincidă."; bunifuFlatButton1.Text = "SCHIMBĂ PAROLA"; }
                label32.Text = "Acest câmp este gol.";
                label31.Text = "Acest câmp este gol.";
                label30.Text = "Acest câmp este gol.";
                label29.Text = "Acest câmp este gol.";
                label28.Text = "Acest câmp este gol.";
                label27.Text = "Acest câmp este gol.";
                label26.Text = "Parolele nu coincid.";
                label45.Text = "Trebuie să adăugați o poză de profil.";
                materialSingleLineTextField9.Hint = "Nume membru";
                label47.Text = "Nu";
                label46.Text = "Da";
                bunifuFlatButton4.Text = "RESETEAZĂ FILTRELE";
                //------------------------------------------------------------------------CLIENTS--------------------------
                bunifuFlatButton2.Text = "   ADAUGĂ CLIENT";
                bunifuFlatButton3.Text = "LISTĂ CLIENȚI";
                bunifuFlatButton5.Text = "RESETEAZĂ FILTRELE";
                label50.Text = "    ID                     Nume                                 Activ                                 Gradul                     Clienți înregistrați             Logări în sistem                 Ultima logare                Dată înregistrare";
                //---------------------------------------------------------------------------PROJECT----------------------------
                bunifuFlatButton8.Text = "LISTĂ PROIECTE";
                bunifuFlatButton9.Text = "ADAUGĂ PROIECT";
                label58.Text = "Adaugă proiect imobiliar";
                label68.Text = "Pentru a adăuga un proiect imobiliar trebuie să completati formularul de adăugare.\nCâmpurile marcate cu steluță(*) sunt obligatorii.";
                label69.Text = "Pentru a seta locația proiectului trebuie să poziționați pinul pe hartă.";
                label80.Text = "Selectați locația proiectului pe hartă.";
                label74.Text = "Detalii proiect";
                label62.Text = "Locație proiect";
                materialSingleLineTextField19.Hint = "Nume proiect *";
                project_region.Hint = "Județ *";
                project_city.Hint = "Oraș *";
                materialSingleLineTextField11.Hint = "An finalizare *";
                materialSingleLineTextField17.Hint = "Adresa exactă *";
                materialSingleLineTextField10.Hint = "Eficiență energetică";
                materialCheckBox1.Text = "Parcare suprafață";
                materialCheckBox2.Text = "Parcare subterană";
                materialCheckBox3.Text = "Boxă (depozit)";
                label67.Text = "Descriere proiect";
                label72.Text = "Selectați modul de vizualizare pe website :";
                materialRadioButton1.Text = "Satelit";
                materialRadioButton2.Text = "Normal";
                materialRadioButton3.Text = "Relief";
                label76.Text = "Galerie de imagini";
                label79.Text = "Click pe câmp pentru a adăuga o imagine proiectului.";
                bunifuFlatButton10.Text = "ADAUGĂ PROIECT";
                materialSingleLineTextField20.Hint = "Titlu imagine 1";
                materialSingleLineTextField21.Hint = "Titlu imagine 2";
                materialSingleLineTextField22.Hint = "Titlu imagine 3";
                materialSingleLineTextField23.Hint = "Titlu imagine 4";
                materialSingleLineTextField24.Hint = "Titlu imagine 5";
                label77.Text = "Acest câmp este gol.";
                label75.Text = "Acest câmp este gol.";
                label66.Text = "Acest câmp este gol.";
                label63.Text = "Acest câmp este gol.";
                label73.Text = "Acest câmp este gol.";
                label78.Text = "Trebuie să adăugați 5(cinci) poze.";
                bunifuFlatButton7.Text = "RESETEAZĂ FILTRELE";
                bunifuDropdown2.Clear();
                bunifuDropdown2.AddItem(Convert.ToString("Finalizat"));
                bunifuDropdown2.AddItem(Convert.ToString("În construcție"));
                bunifuDropdown2.AddItem(Convert.ToString("Proiect"));
                bunifuDropdown2.selectedIndex = 0;
                label56.Text = "    ID             Nume agent                Galerie                    Nume proiect               Status vânzare    Status construcție        Proprietăți                           Email                     Dată înregistrare";
                label85.Text = "Dată înregistrare proiect :";
                label88.Text = "Dată editare proiect :";
                label89.Text = "Detalii proiect";
                label111.Text = "Detalii imobil";
                label90.Text = "Locație proiect";
                bunifuFlatButton13.Text = "LISTĂ PROPRIETĂȚI ASOCIATE";
                bunifuFlatButton14.Text = "CLIENȚI PROIECT";
                label82.Text = "Nume proiect :";
                label94.Text = "Email proiect :";
                label96.Text = "An finalizare :";
                label100.Text = "Status vânzare :";
                label102.Text = "Status construcție :";
                label104.Text = "Parcare suprafață :";
                label106.Text = "Parcare subterană :";
                label108.Text = "Boxă (depozit) :";
                label110.Text = "Eficiență energetică :";
                label112.Text = "Documente proiect";
                label86.Text = "Descriere proiect";
                label84.Text = "Galerie de imagini";
                bunifuFlatButton11.Text = "EDITEAZĂ";
                label136.Text = "Pentru a edita proiectul trebuie să modificați date in câmpurile pe care doriți să le editați.\nCâmpurile marcate cu steluță(*) sunt obligatorii.";
                label133.Text = "Pentru a modifica locația proiectului trebuie să poziționați pinul pe hartă.";
                label128.Text = "Detalii proiect";
                label129.Text = "Locație proiect";
                materialSingleLineTextField32.Hint = "Nume proiect *";
                materialSingleLineTextField36.Hint = "Județ *";
                label127.Text = "Acest câmp este gol.";
                materialSingleLineTextField35.Hint = "Oraș *";
                materialSingleLineTextField30.Hint = "An finalizare *";
                materialSingleLineTextField33.Hint = "Adresa exactă *";
                label126.Text = "Acest câmp este gol.";
                label130.Text = "Acest câmp este gol.";
                materialSingleLineTextField34.Hint = "Eficiență energetică";
                materialCheckBox6.Text = "Parcare suprafață";
                materialCheckBox5.Text = "Parcare subterană";
                materialCheckBox4.Text = "Boxă (depozit)";
                label125.Text = "Descriere proiect";
                label131.Text = "Selectați modul de vizualizare pe website :";
                materialRadioButton6.Text = "Satelit";
                materialRadioButton5.Text = "Normal";
                materialRadioButton4.Text = "Relief";
                label138.Text = "Documente proiect";
                file1.FileLocation = "Locație document";
                file2.FileLocation = "Locație document";
                file3.FileLocation = "Locație document";
                materialSingleLineTextField37.Hint = "Titlu document 1";
                materialSingleLineTextField38.Hint = "Titlu document 2";
                materialSingleLineTextField39.Hint = "Titlu document 3";
                label123.Text = "Galerie de imagini";
                label122.Text = "Click pe câmp pentru a schimba o imagine proiectului.";
                label120.Text = "Documentele nu sunt obligatorii.";
                materialSingleLineTextField29.Hint = "Titlu imagine 1";
                materialSingleLineTextField28.Hint = "Titlu imagine 2";
                materialSingleLineTextField27.Hint = "Titlu imagine 3";
                materialSingleLineTextField26.Hint = "Titlu imagine 4";
                materialSingleLineTextField25.Hint = "Titlu imagine 5";
            }
        }
        //--------------------------------------------------------------------------------------------------------------MESSAGEBOX FUNCTION------------------
        private void messageboxforexit()
        {
            if (dropdownlanguage.selectedValue == "RO")
            {
                if (messageboxshow == true)
                {
                    if (MetroFramework.MetroMessageBox.Show(this, "Dacă ieșiti datele nu vor fi salvate.", "Ești sigur că vrei să ieși?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        messageboxresult = true;
                    }
                    else
                    {
                        messageboxresult = false;
                    }
                }
                else
                    return;
            }
            else
            {
                if (messageboxshow == true)
                {
                    if (MetroFramework.MetroMessageBox.Show(this, "If you exit the data will not be saved.", "Are you sure you want to do this?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        messageboxresult = true;
                    }
                    else
                    {
                        messageboxresult = false;
                    }
                }
                else
                    return;
            }

        }
        //--------------------------------------------------------------------------------------------------------------BLOCK FOR ALL BUTTONS IN MENU-------
        private void dashboardbutton_Click(object sender, EventArgs e)//dashboard button
        {
            zoomoutpictureboxes();
            while (messageboxshow == false || messageboxshow == true && messageboxresult == false || messageboxresult == true)
            {
                if (messageboxshow == false && messageboxresult == false)
                {
                    clients_panel.Visible = false;
                    properties_panel.Visible = false;
                    projects_panel.Visible = false;
                    financial_panel.Visible = false;
                    statistics_panel.Visible = false;
                    team_panel.Visible = false;
                    website_panel.Visible = false;
                    dashboard_panel.Visible = true;
                    interogate_teammate_panel.Visible = false;
                    slidebarmenu.BringToFront();
                    //---------------------------------------------
                    break;
                }
                else
                {
                    messageboxforexit();
                }
                if (messageboxshow == true && messageboxresult == false)
                {
                    break;
                }
                else //if (messageboxshow == true && messageboxresult)
                {
                    messageboxshow = false;
                    messageboxresult = false;
                    //-------------------------------------------
                    restartformularaddteammate();
                    //-------------------------------------------
                    clients_panel.Visible = false;
                    properties_panel.Visible = false;
                    projects_panel.Visible = false;
                    financial_panel.Visible = false;
                    statistics_panel.Visible = false;
                    team_panel.Visible = false;
                    website_panel.Visible = false;
                    dashboard_panel.Visible = true;
                    interogate_teammate_panel.Visible = false;
                    editteammate_panel.Visible = false;
                    slidebarmenu.BringToFront();
                    pictureBox1.Focus();
                    //-------------------------------------------
                    break;
                }
            }
        }

        private void clientsbutton_Click(object sender, EventArgs e)//clients button
        {
            zoomoutpictureboxes();

            clients_panel.Visible = true;
            properties_panel.Visible = false;
            projects_panel.Visible = false;
            financial_panel.Visible = false;
            statistics_panel.Visible = false;
            team_panel.Visible = false;
            website_panel.Visible = false;
            dashboard_panel.Visible = false;
            slidebarmenu.BringToFront();
        }

        private void propertiesbutton_Click(object sender, EventArgs e)//properties button
        {
            zoomoutpictureboxes();

            clients_panel.Visible = false;
            properties_panel.Visible = true;
            projects_panel.Visible = false;
            financial_panel.Visible = false;
            statistics_panel.Visible = false;
            team_panel.Visible = false;
            website_panel.Visible = false;
            dashboard_panel.Visible = false;
            slidebarmenu.BringToFront();
        }

        private void projectsbutton_Click(object sender, EventArgs e)//projects button
        {
            zoomoutpictureboxes();
            while (messageboxshow == false || messageboxshow == true && messageboxresult == false || messageboxresult == true)
            {
                if (messageboxshow == false && messageboxresult == false)
                {
                    if (markerinitialize == 0)
                    {
                        clients_panel.Visible = false;
                        properties_panel.Visible = false;
                        projects_panel.Visible = true;
                        financial_panel.Visible = false;
                        statistics_panel.Visible = false;
                        team_panel.Visible = false;
                        website_panel.Visible = false;
                        dashboard_panel.Visible = false;
                        interogateproject_panel.Visible = false;
                        editproject_panel.Visible = false;
                        addproject_panel.Visible = false;
                        slidebarmenu.BringToFront();
                        projectgrid();
                    }
                    else
                    {
                        clients_panel.Visible = false;
                        properties_panel.Visible = false;
                        projects_panel.Visible = true;
                        financial_panel.Visible = false;
                        statistics_panel.Visible = false;
                        team_panel.Visible = false;
                        website_panel.Visible = false;
                        dashboard_panel.Visible = false;
                        interogateproject_panel.Visible = false;
                        editproject_panel.Visible = false;
                        addproject_panel.Visible = false;
                        marker.IsVisible = false;
                        slidebarmenu.BringToFront();
                        projectgrid();
                    }
                    //---------------------------------------------
                    break;
                }
                else
                {
                    messageboxforexit();
                }
                if (messageboxshow == true && messageboxresult == false)
                {
                    break;
                }
                else //if (messageboxshow == true && messageboxresult)
                {
                    messageboxshow = false;
                    messageboxresult = false;
                    //-------------------------------------------
                    restartformularaddproject();
                    marker.IsVisible = false;
                    //-------------------------------------------
                    clients_panel.Visible = false;
                    properties_panel.Visible = false;
                    projects_panel.Visible = true;
                    financial_panel.Visible = false;
                    statistics_panel.Visible = false;
                    team_panel.Visible = false;
                    website_panel.Visible = false;
                    dashboard_panel.Visible = false;
                    interogateproject_panel.Visible = false;
                    editproject_panel.Visible = false;
                    addproject_panel.Visible = false;
                    slidebarmenu.BringToFront();
                    pictureBox1.Focus();
                    projectgrid();
                    //-------------------------------------------
                    break;
                }
            }

        }

        private void financialbutton_Click(object sender, EventArgs e)//financial button
        {
            zoomoutpictureboxes();

            clients_panel.Visible = false;
            properties_panel.Visible = false;
            projects_panel.Visible = false;
            financial_panel.Visible = true;
            statistics_panel.Visible = false;
            team_panel.Visible = false;
            website_panel.Visible = false;
            dashboard_panel.Visible = false;
            slidebarmenu.BringToFront();
        }

        private void statisticsbutton_Click(object sender, EventArgs e)//statistics button
        {
            zoomoutpictureboxes();

            clients_panel.Visible = false;
            properties_panel.Visible = false;
            projects_panel.Visible = false;
            financial_panel.Visible = false;
            statistics_panel.Visible = true;
            team_panel.Visible = false;
            website_panel.Visible = false;
            dashboard_panel.Visible = false;
            slidebarmenu.BringToFront();
        }

        private void teambutton_Click(object sender, EventArgs e)//team button
        {
            zoomoutpictureboxes();

            Teamgrid();
            clients_panel.Visible = false;
            properties_panel.Visible = false;
            projects_panel.Visible = false;
            financial_panel.Visible = false;
            statistics_panel.Visible = false;
            team_panel.Visible = true;
            website_panel.Visible = false;
            dashboard_panel.Visible = false;
            slidebarmenu.BringToFront();
        }

        private void websitebutton_Click(object sender, EventArgs e)//website button
        {
            zoomoutpictureboxes();

            clients_panel.Visible = false;
            properties_panel.Visible = false;
            projects_panel.Visible = false;
            financial_panel.Visible = false;
            statistics_panel.Visible = false;
            team_panel.Visible = false;
            website_panel.Visible = true;
            dashboard_panel.Visible = false;
            slidebarmenu.BringToFront();
        }
        //--------------------------------------------------------------------------------block of function scroll for all grids-----------------//////////////
        private void dashboard_panel_MouseHover(object sender, EventArgs e)
        {
            dashboard_panel.Focus();
        }
        private void teamgrid_mousewheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && teamgrid.FirstDisplayedScrollingRowIndex > 0)
            {
                teamgrid.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0)
            {
                teamgrid.FirstDisplayedScrollingRowIndex++;
            }
        }
        private void bunifuCustomGrid1_mousewheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && bunifuCustomDataGrid1.FirstDisplayedScrollingRowIndex > 0)
            {
                bunifuCustomDataGrid1.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0)
            {
                bunifuCustomDataGrid1.FirstDisplayedScrollingRowIndex++;
            }
        }
        private void teamgrid_MouseHover(object sender, EventArgs e)
        {
            teamgrid.Focus();
        }
        //--------------------------------------------------------------------------------------------------------------------------------//////////////
        //----------------------------------------------------------------------------------------------------------------------------------------------

        private void personaldescription_textbox_MouseEnter(object sender, EventArgs e)
        {
            personaldescription_textbox.Focus();
            materialDivider5.BackColor = System.Drawing.Color.FromArgb(55, 71, 79);
        }

        private void personaldescription_textbox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            materialDivider5.BackColor = System.Drawing.Color.FromArgb(193, 193, 193);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------

        private void addteammate_button_Click(object sender, EventArgs e)//add member button
        {
            while (messageboxshow == false || messageboxshow == true && messageboxresult == false || messageboxresult == true)
            {
                if (messageboxshow == false && messageboxresult == false)
                {
                    interogate_teammate_panel.Visible = false;
                    addteammate_panel.Visible = true;
                    addteammate_panel.BringToFront();
                    messageboxshow = true;
                    break;
                }
                else
                {
                    messageboxforexit();
                }
                if (messageboxshow == true && messageboxresult == false)
                {
                    break;
                }
                else //if (messageboxshow == true && messageboxresult)
                {
                    messageboxshow = false;
                    messageboxresult = false;
                    //-------------------------------------------
                    restartformularaddteammate();
                    //-------------------------------------------
                    editteammate_panel.Visible = false;
                    interogate_teammate_panel.Visible = false;
                    addteammate_panel.Visible = true;
                    addteammate_panel.BringToFront();
                    messageboxshow = true;
                    slidebarmenu.BringToFront();
                    //-------------------------------------------
                    break;
                }
            }
        }

        private void phone_textbox_KeyPress(object sender, KeyPressEventArgs e)//code for textbox to take just numberkey
        {
            char phonenumbermemberbox = e.KeyChar;
            if (!Char.IsDigit(phonenumbermemberbox) && phonenumbermemberbox != 8 && phonenumbermemberbox != 46)
            { e.Handled = true; }
        }

        private void validateaddmember_button_Click(object sender, EventArgs e)//vaidate button for adding teammate
        {
            if (string.IsNullOrEmpty(firstname_textbox.Text) || string.IsNullOrWhiteSpace(firstname_textbox.Text)) { label6.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(lastname_textbox.Text) || string.IsNullOrWhiteSpace(lastname_textbox.Text)) { label7.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(email_textbox.Text) || string.IsNullOrWhiteSpace(email_textbox.Text)) { label8.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(phone_textbox.Text) || string.IsNullOrWhiteSpace(phone_textbox.Text)) { label9.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(password_textbox.Text) || string.IsNullOrWhiteSpace(password_textbox.Text)) { label10.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(password_textbox.Text) || string.IsNullOrWhiteSpace(password_textbox.Text)) { label10.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(repeatpassword_textbox.Text) || string.IsNullOrWhiteSpace(repeatpassword_textbox.Text)) { label11.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (pictureBox4.Image == null) { label45.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (password_textbox.Text != repeatpassword_textbox.Text) { label12.Visible = true; validateerrorforteammatetimer.Start(); }
            else//code for insert teammate in databae
            {
                messageboxshow = false;
                //-----------------------------------------------

                Team();

                //-----------------------------------------------
                restartformularaddteammate();
                validateerrorforteammatetimer.Start(); insertpopupanimation.Show(bunifuCustomLabel6); bunifuCustomLabel6.Visible = true; bunifuCustomLabel6.BringToFront();
                //-------------------------------------------------
            }
        }
        private void restartformularaddteammate()
        {
            team_panel.BringToFront(); addteammate_panel.Visible = false;
            firstname_textbox.Text = string.Empty; lastname_textbox.Text = string.Empty; email_textbox.Text = string.Empty;
            phone_textbox.Text = string.Empty; function_textbox.Text = string.Empty;
            password_textbox.Text = string.Empty; repeatpassword_textbox.Text = string.Empty;
            personaldescription_textbox.Text = string.Empty; pictureBox4.Image = null; pictureBox4.Invalidate();
        }

        private void validateerrortimerforteammate_Tick(object sender, EventArgs e)//timer for some function and action
        {
            counterseccondforvalidateerror = counterseccondforvalidateerror + 100;
            if (counterseccondforvalidateerror == 4000)
            {
                label127.Visible = false;
                label126.Visible = false;
                label130.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                label32.Visible = false;
                label31.Visible = false;
                label30.Visible = false;
                label29.Visible = false;
                label28.Visible = false;
                label27.Visible = false;
                label26.Visible = false;
                label45.Visible = false;
                label77.Visible = false;
                label75.Visible = false;
                label66.Visible = false;
                label63.Visible = false;
                label73.Visible = false;
                label78.Visible = false;
                label80.Visible = false;
                insertpopupanimation.Hide(bunifuCustomLabel6);
                bunifuCustomLabel6.Visible = false;
                validateerrorforteammatetimer.Stop();
                counterseccondforvalidateerror = 0;
            }
            else
                return;
        }
        //---------------profile picture insert
        public void pictureBox4_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.Image = Image.FromFile(f.FileName);
            }
        }
        //--------------------------------------

        //sql connection
        string stringcon = System.Configuration.ConfigurationManager.ConnectionStrings["devbuild"].ConnectionString;
        SqlCommand cmd = new SqlCommand();

        private void Team()//insert into db new teammate
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
                byte[] a = ms.ToArray();
                ms.Close();

                SqlConnection con = new SqlConnection(stringcon);//CONNECTION

                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO team(lastname,firstname,phonenumber,email,[password],[function],[role],registerdata,personaldescription,profilepicture) VALUES(@lastname,@firstname,@phonenumber,@email,@password,@function,@role,@registerdata,@personaldescription,@profilepicture)";

                cmd.Parameters.AddWithValue("@lastname", lastname_textbox.Text);
                cmd.Parameters.AddWithValue("@firstname", firstname_textbox.Text);
                cmd.Parameters.AddWithValue("@phonenumber", "+" + phone_textbox.Text);
                cmd.Parameters.AddWithValue("@email", email_textbox.Text);
                cmd.Parameters.AddWithValue("@password", repeatpassword_textbox.Text);
                cmd.Parameters.AddWithValue("@function", function_textbox.Text);
                cmd.Parameters.AddWithValue("@role", role_dropbox.selectedIndex);
                cmd.Parameters.AddWithValue("@registerdata", DateTime.Now.ToString("yyyy-MM-dd HH: mm:ss"));
                cmd.Parameters.AddWithValue("@personaldescription", personaldescription_textbox.Text);
                cmd.Parameters.AddWithValue("@profilepicture", a);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                pictureBox4.Image.Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return;
            }
        }
        public void LastloginAndNumberOflogin()//update intodb lastlogin data of an user
        {
            SqlConnection con = new SqlConnection(stringcon); //CONNECTION
            SqlCommand cmd2 = new SqlCommand();

            cmd2.Parameters.Clear();
            cmd2.Connection = con;
            cmd2.CommandText = "update dbo.team set lastlogin=@lastlogin where email=@email";
            cmd2.Parameters.AddWithValue("@lastlogin", DateTime.Now.ToString("yyyy-MM-dd HH: mm:ss"));
            cmd2.Parameters.AddWithValue("@email", loginDEVELOPBUILDING.Email);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();

            SqlConnection con2 = new SqlConnection(stringcon); //CONNECTION
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Clear();
            cmd.Connection = con2;
            cmd.CommandText = "update dbo.team set numberoflogin=numberoflogin+1 where email=@email";
            cmd.Parameters.AddWithValue("@email", loginDEVELOPBUILDING.Email);

            con2.Open();
            cmd.ExecuteNonQuery();
            con2.Close();
        }

        public void Teamgrid()//show teamgrid in team panel
        {
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            if (dropdownlanguage.selectedValue == "RO")
            {
                SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Da' when status=0 then 'Nu' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team", con);
                cmdread3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                sda2.Fill(dt3);
                teamgrid.DataSource = dt3;

                //store autosized widths
                int colw = teamgrid.Columns[0].Width;
                //remove autosizing
                teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                teamgrid.Columns[0].Width = 50;

                con.Close();
            }
            else
            {
                SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Yes' when status=0 then 'No' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team", con);
                cmdread3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                sda2.Fill(dt3);
                teamgrid.DataSource = dt3;

                //store autosized widths
                int colw = teamgrid.Columns[0].Width;
                //remove autosizing
                teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                teamgrid.Columns[0].Width = 50;

                con.Close();
            }

        }

        private void watchpassbutton_MouseEnter(object sender, EventArgs e)
        {
            password_textbox.UseSystemPasswordChar = false;
            repeatpassword_textbox.UseSystemPasswordChar = false;
        }//to watchpassword character

        private void watchpassbutton_MouseLeave(object sender, EventArgs e)
        {
            password_textbox.UseSystemPasswordChar = true;
            repeatpassword_textbox.UseSystemPasswordChar = true;
        }//to watchpassword systempass

        private void teamlist_button_Click(object sender, EventArgs e)// team list show button
        {
            Teamgrid();
            while (messageboxshow == false || messageboxshow == true && messageboxresult == false || messageboxresult == true)
            {
                if (messageboxshow == false && messageboxresult == false)
                {
                    interogate_teammate_panel.Visible = false;
                    break;
                }
                else
                {
                    messageboxforexit();
                }
                if (messageboxshow == true && messageboxresult == false)
                {
                    break;
                }
                else //if (messageboxshow == true && messageboxresult)
                {
                    messageboxshow = false;
                    messageboxresult = false;
                    //-------------------------------------------
                    restartformularaddteammate();
                    //-------------------------------------------
                    addteammate_panel.Visible = false;
                    editteammate_panel.Visible = false;
                    slidebarmenu.BringToFront();
                    //-------------------------------------------
                    mar = false;
                    break;
                }
            }
        }

        public int id, id3, id2;
        public string id4;
        private void teamgrid_CellClick(object sender, DataGridViewCellEventArgs e)//event when clicked an row in teamgrid
        {
            interogate_teammate_panel.Visible = true;
            interogate_teammate_panel.BringToFront();

            int id;
            id = Convert.ToInt32(teamgrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "select * from team where id=" + id + "";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);




            foreach (DataRow dr in dt.Rows)
            {
                string fullname;
                string firstname;
                string lastname;
                int role;
                firstname = dr["firstname"].ToString();
                lastname = dr["lastname"].ToString();
                label19.Text = dr["phonenumber"].ToString();
                dateandtime_label.Text = Convert.ToDateTime(dr["registerdata"]).ToString("dd/MM/yyyy - HH:mm");
                label20.Text = dr["email"].ToString();
                label21.Text = dr["function"].ToString();
                role = Convert.ToInt16(dr["role"].ToString());

                if (role == 0) { label22.Text = "Manager"; } else { label22.Text = "Agent"; }

                label23.Text = dr["asociateclients"].ToString();
                label24.Text = dr["asociateproperties"].ToString();
                personaldescriprion_label.Text = dr["asociateproperties"].ToString();
                label17.Text = firstname;
                label18.Text = lastname;
                fullname = lastname + " " + firstname;
                agentname_label.Text = fullname;
                personaldescriprion_label.Text = dr["personaldescription"].ToString();
                byte[] ap = (byte[])ds.Tables[0].Rows[0]["profilepicture"];
                MemoryStream ms = new MemoryStream(ap);
                pictureBox2.Image = Image.FromStream(ms);
            }
            con.Close();

            id2 = Convert.ToInt32(teamgrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
            id3 = Convert.ToInt32(teamgrid.Rows[e.RowIndex].Cells["id"].Value.ToString());

            if (manageroragent == 0)
            {
                editteammate_button.Visible = true;
                passwordreset_button.Visible = true;
                if (loginDEVELOPBUILDING.Email == label20.Text)
                {
                    passwordreset_button.Location = new Point(1101, 622);
                    editteammate_button.Location = new Point(974, 622);
                    fire_button.Visible = false;
                }
                else
                {
                    passwordreset_button.Location = new Point(974, 622);
                    editteammate_button.Location = new Point(847, 622);
                    fire_button.Visible = true;
                }
            }
            else
            {
                if (loginDEVELOPBUILDING.Email == label20.Text)
                {
                    passwordreset_button.Location = new Point(1101, 622);
                    editteammate_button.Location = new Point(974, 622);
                    editteammate_button.Visible = true;
                    passwordreset_button.Visible = true;

                }
                else
                {
                    editteammate_button.Visible = false;
                    passwordreset_button.Visible = false;
                }
            }
        }
        private void editteammate_button_Click(object sender, EventArgs e)
        {

            if (dropdownlanguage.selectedValue == "EN")
            {
                label37.Text = "Edit profile";
                label36.Text = "You can edit any field. All fields are required.";
                bunifuFlatButton1.Text = "EDIT";
            }
            else
            {
                label37.Text = "Editează profilul";
                label36.Text = "Poți edita orice câmp. Toate câmpurile sunt obligatorii.";
                bunifuFlatButton1.Text = "EDITEAZĂ";
            }
            if (mar == false)
            {
                SqlConnection con = new SqlConnection(stringcon);
                con.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "select * from team where id=" + id2 + "";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                DataTable dt12 = new DataTable();
                SqlDataAdapter da12 = new SqlDataAdapter(cmd2);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da12.Fill(ds2);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    materialSingleLineTextField7.Text = dr["firstname"].ToString();
                    materialSingleLineTextField6.Text = dr["lastname"].ToString();
                    materialSingleLineTextField5.Text = dr["email"].ToString();
                    materialSingleLineTextField4.Text = dr["phonenumber"].ToString();
                    bunifuDropdown1.selectedIndex = Convert.ToInt16(dr["role"].ToString());
                    jwRichTextBox1.Text = dr["personaldescription"].ToString();
                    materialSingleLineTextField3.Text = dr["function"].ToString();
                    byte[] ap = (byte[])ds2.Tables[0].Rows[0]["profilepicture"];
                    MemoryStream ms = new MemoryStream(ap);
                    pictureBox3.Image = Image.FromStream(ms);
                }
            }
            else
            {
                SqlConnection con = new SqlConnection(stringcon);
                con.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Parameters.Clear();
                cmd2.Connection = con;
                cmd2.CommandText = "select * from team where email=@em";
                cmd2.Parameters.AddWithValue("@em", loginDEVELOPBUILDING.Email);
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                DataTable dt12 = new DataTable();
                SqlDataAdapter da12 = new SqlDataAdapter(cmd2);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da12.Fill(ds2);
                da2.Fill(dt2);
                foreach (DataRow dr in dt2.Rows)
                {
                    materialSingleLineTextField7.Text = dr["firstname"].ToString();
                    materialSingleLineTextField6.Text = dr["lastname"].ToString();
                    materialSingleLineTextField5.Text = dr["email"].ToString();
                    materialSingleLineTextField4.Text = dr["phonenumber"].ToString();
                    bunifuDropdown1.selectedIndex = Convert.ToInt16(dr["role"].ToString());
                    jwRichTextBox1.Text = dr["personaldescription"].ToString();
                    materialSingleLineTextField3.Text = dr["function"].ToString();
                    byte[] ap = (byte[])ds2.Tables[0].Rows[0]["profilepicture"];
                    MemoryStream ms = new MemoryStream(ap);
                    pictureBox3.Image = Image.FromStream(ms);
                }
            }
            editteammate_panel.Visible = true;
            editteammate_panel.BringToFront();
            interogate_teammate_panel.Visible = false;
            pictureBox3.BringToFront();
            messageboxshow = true;
            materialDivider11.Visible = false;
            materialSingleLineTextField2.Visible = false;
            materialSingleLineTextField1.Visible = false;
            bunifuImageButton1.Visible = false;
            //---------------------------------------------------------------------------------------------------------------------------
            label38.Visible = false; label38.Parent = materialSingleLineTextField7; label38.Dock = DockStyle.Fill; label38.BringToFront();
            label39.Visible = false; label39.Parent = materialSingleLineTextField6; label39.Dock = DockStyle.Fill; label39.BringToFront();
            label40.Visible = false; label40.Parent = materialSingleLineTextField5; label40.Dock = DockStyle.Fill; label40.BringToFront();
            label41.Visible = false; label41.Parent = materialSingleLineTextField4; label41.Dock = DockStyle.Fill; label41.BringToFront();
            label42.Visible = false; label42.Parent = bunifuDropdown1; label42.Dock = DockStyle.Fill; label42.BringToFront();
            label43.Visible = false; label43.Parent = materialSingleLineTextField3; label43.Dock = DockStyle.Fill; label43.BringToFront();
            label44.Visible = false; label44.Parent = jwRichTextBox1; label44.Dock = DockStyle.Fill; label44.BringToFront(); jwRichTextBox1.Cursor = DefaultCursor;

        }

        private void passwordreset_button_Click(object sender, EventArgs e)
        {
            if (dropdownlanguage.selectedValue == "EN")
            {
                label37.Text = "Change password";
                label36.Text = "The fields for changing the password must be completed and coincide.";
                bunifuFlatButton1.Text = "CHANGE PASSWORD";
            }
            else
            {
                label37.Text = "Schimbă parola";
                label36.Text = "Câmpurile specifice schimbării parolei trebuie să fie completate și să coincidă.";
                bunifuFlatButton1.Text = "SCHIMBĂ PAROLA";
            }

            if (mar == false)
            {
                SqlConnection con = new SqlConnection(stringcon);
                con.Open();

                SqlCommand cmd2 = new SqlCommand();

                cmd2.Connection = con;
                cmd2.CommandText = "select * from team where id=" + id3 + "";
                cmd2.ExecuteNonQuery();

                DataTable dt2 = new DataTable();
                DataTable dt12 = new DataTable();
                SqlDataAdapter da12 = new SqlDataAdapter(cmd2);

                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da12.Fill(ds2);
                da2.Fill(dt2);

                foreach (DataRow dr in dt2.Rows)
                {

                    label38.Text = dr["firstname"].ToString();
                    label39.Text = dr["lastname"].ToString();
                    label40.Text = dr["email"].ToString();
                    label41.Text = dr["phonenumber"].ToString();
                    int role = Convert.ToInt16(dr["role"].ToString());
                    if (role == 0) { label42.Text = "Manager"; } else { label42.Text = "Agent"; }
                    label44.Text = dr["personaldescription"].ToString();
                    label43.Text = dr["function"].ToString();


                    byte[] ap = (byte[])ds2.Tables[0].Rows[0]["profilepicture"];
                    MemoryStream ms = new MemoryStream(ap);
                    pictureBox5.Image = Image.FromStream(ms);
                }
                con.Close();
            }
            else
            {

                SqlConnection con = new SqlConnection(stringcon);
                con.Open();

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Parameters.Clear();
                cmd2.Connection = con;
                cmd2.CommandText = "select * from team where email=@em";
                cmd2.Parameters.AddWithValue("@em", loginDEVELOPBUILDING.Email);
                cmd2.ExecuteNonQuery();

                DataTable dt2 = new DataTable();
                DataTable dt12 = new DataTable();
                SqlDataAdapter da12 = new SqlDataAdapter(cmd2);

                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da12.Fill(ds2);
                da2.Fill(dt2);

                foreach (DataRow dr in dt2.Rows)
                {

                    label38.Text = dr["firstname"].ToString();
                    label39.Text = dr["lastname"].ToString();
                    label40.Text = dr["email"].ToString();
                    label41.Text = dr["phonenumber"].ToString();
                    int role = Convert.ToInt16(dr["role"].ToString());
                    if (role == 0) { label42.Text = "Manager"; } else { label42.Text = "Agent"; }
                    label44.Text = dr["personaldescription"].ToString();
                    label43.Text = dr["function"].ToString();


                    byte[] ap = (byte[])ds2.Tables[0].Rows[0]["profilepicture"];
                    MemoryStream ms = new MemoryStream(ap);
                    pictureBox5.Image = Image.FromStream(ms);
                }
                con.Close();
            }
            messageboxshow = true;
            editteammate_panel.Visible = true;
            interogate_teammate_panel.Visible = false;
            editteammate_panel.BringToFront();
            materialDivider11.Visible = true;
            materialSingleLineTextField2.Visible = true;
            materialSingleLineTextField1.Visible = true;
            bunifuImageButton1.Visible = true;
            materialSingleLineTextField2.Text = "";
            materialSingleLineTextField1.Text = "";
            //---------------------------------------------------------------------------------------------------------------------------
            label38.Visible = true; label38.Parent = materialSingleLineTextField7; label38.Dock = DockStyle.Fill; label38.BringToFront();
            label39.Visible = true; label39.Parent = materialSingleLineTextField6; label39.Dock = DockStyle.Fill; label39.BringToFront();
            label40.Visible = true; label40.Parent = materialSingleLineTextField5; label40.Dock = DockStyle.Fill; label40.BringToFront();
            label41.Visible = true; label41.Parent = materialSingleLineTextField4; label41.Dock = DockStyle.Fill; label41.BringToFront();
            label42.Visible = true; label42.Parent = bunifuDropdown1; label42.Dock = DockStyle.Fill; label42.BringToFront();
            label43.Visible = true; label43.Parent = materialSingleLineTextField3; label43.Dock = DockStyle.Fill; label43.BringToFront();
            label44.Visible = true; label44.Parent = jwRichTextBox1; label44.Dock = DockStyle.Fill; label44.BringToFront(); jwRichTextBox1.Cursor = DefaultCursor;
            pictureBox5.BringToFront();
        }

        private void materialSingleLineTextField4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char phonenumbermemberbox = e.KeyChar;
            if (!Char.IsDigit(phonenumbermemberbox) && phonenumbermemberbox != 8 && phonenumbermemberbox != 46)
            { e.Handled = true; }
        }

        private void jwRichTextBox1_MouseEnter(object sender, EventArgs e)
        {
            jwRichTextBox1.Focus();
            materialDivider12.BackColor = System.Drawing.Color.FromArgb(55, 71, 79);
        }

        private void jwRichTextBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            materialDivider12.BackColor = System.Drawing.Color.FromArgb(193, 193, 193);
        }

        private void bunifuImageButton1_MouseEnter(object sender, EventArgs e)
        {
            materialSingleLineTextField2.UseSystemPasswordChar = false;
            materialSingleLineTextField1.UseSystemPasswordChar = false;
        }

        private void bunifuImageButton1_MouseLeave(object sender, EventArgs e)
        {
            materialSingleLineTextField2.UseSystemPasswordChar = true;
            materialSingleLineTextField1.UseSystemPasswordChar = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = Image.FromFile(f.FileName);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            if (label37.Text == "Editează profilul" || label37.Text == "Edit profile")
            {
                if (string.IsNullOrEmpty(materialSingleLineTextField7.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField7.Text)) { label32.Visible = true; validateerrorforteammatetimer.Start(); }
                else if (string.IsNullOrEmpty(materialSingleLineTextField6.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField6.Text)) { label31.Visible = true; validateerrorforteammatetimer.Start(); }
                else if (string.IsNullOrEmpty(materialSingleLineTextField5.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField5.Text)) { label30.Visible = true; validateerrorforteammatetimer.Start(); }
                else if (string.IsNullOrEmpty(materialSingleLineTextField4.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField4.Text)) { label29.Visible = true; validateerrorforteammatetimer.Start(); }
                else//code for insert teammate in databae
                {

                    messageboxshow = false;
                    //-----------------------------------------------
                    if (mar == false)
                    {
                        MemoryStream ms = new MemoryStream();
                        pictureBox3.Image.Save(ms, pictureBox3.Image.RawFormat);
                        byte[] a = ms.ToArray();
                        ms.Close();

                        SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                        SqlCommand cmd2 = new SqlCommand();

                        cmd2.Parameters.Clear();
                        cmd2.Connection = con;
                        cmd2.CommandText = "update dbo.team set firstname=@fn,lastname=@ln,email=@ema,phonenumber=@pn,[role]=@role,[function]=@f,personaldescription=@pd,profilepicture=@p where id=@id";
                        cmd2.Parameters.AddWithValue("@fn", materialSingleLineTextField7.Text);
                        cmd2.Parameters.AddWithValue("@ln", materialSingleLineTextField6.Text);
                        cmd2.Parameters.AddWithValue("@ema", materialSingleLineTextField5.Text);
                        cmd2.Parameters.AddWithValue("@pn", "+" + materialSingleLineTextField4.Text);
                        cmd2.Parameters.AddWithValue("@role", bunifuDropdown1.selectedIndex);
                        cmd2.Parameters.AddWithValue("@f", materialSingleLineTextField3.Text);
                        cmd2.Parameters.AddWithValue("@pd", jwRichTextBox1.Text);
                        cmd2.Parameters.AddWithValue("@p", a);
                        cmd2.Parameters.AddWithValue("@id", id3);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();

                        ProfilepictureAndNameFuction();
                        Teamgrid();
                        //-----------------------------------------------

                        validateerrorforteammatetimer.Start(); insertpopupanimation.Show(bunifuCustomLabel6); bunifuCustomLabel6.Visible = true; bunifuCustomLabel6.BringToFront();
                        editteammate_panel.Visible = false;
                        //-------------------------------------------------
                    }
                    else
                    {
                        mar = false;
                        MemoryStream ms = new MemoryStream();
                        pictureBox3.Image.Save(ms, pictureBox3.Image.RawFormat);
                        byte[] a = ms.ToArray();
                        ms.Close();

                        SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                        SqlCommand cmd2 = new SqlCommand();

                        cmd2.Parameters.Clear();
                        cmd2.Connection = con;
                        cmd2.CommandText = "update dbo.team set firstname=@fn,lastname=@ln,email=@ema,phonenumber=@pn,[role]=@role,[function]=@f,personaldescription=@pd,profilepicture=@p where email=@em";
                        cmd2.Parameters.AddWithValue("@fn", materialSingleLineTextField7.Text);
                        cmd2.Parameters.AddWithValue("@ln", materialSingleLineTextField6.Text);
                        cmd2.Parameters.AddWithValue("@ema", materialSingleLineTextField5.Text);
                        cmd2.Parameters.AddWithValue("@pn", "+" + materialSingleLineTextField4.Text);
                        cmd2.Parameters.AddWithValue("@role", bunifuDropdown1.selectedIndex);
                        cmd2.Parameters.AddWithValue("@f", materialSingleLineTextField3.Text);
                        cmd2.Parameters.AddWithValue("@pd", jwRichTextBox1.Text);
                        cmd2.Parameters.AddWithValue("@p", a);
                        cmd2.Parameters.AddWithValue("@em", loginDEVELOPBUILDING.Email);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();

                        ProfilepictureAndNameFuction();
                        Teamgrid();
                        //-----------------------------------------------

                        validateerrorforteammatetimer.Start(); insertpopupanimation.Show(bunifuCustomLabel6); bunifuCustomLabel6.Visible = true; bunifuCustomLabel6.BringToFront();
                        editteammate_panel.Visible = false;
                        //-------------------------------------------------
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(materialSingleLineTextField2.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField2.Text)) { label28.Visible = true; validateerrorforteammatetimer.Start(); }
                else if (string.IsNullOrEmpty(materialSingleLineTextField1.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField1.Text)) { label27.Visible = true; validateerrorforteammatetimer.Start(); }
                else if (materialSingleLineTextField2.Text != materialSingleLineTextField1.Text) { label26.Visible = true; validateerrorforteammatetimer.Start(); }
                else//code for insert teammate in databae
                {
                    messageboxshow = false;
                    //-----------------------------------------------
                    if (mar == false)
                    {
                        SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                        SqlCommand cmd2 = new SqlCommand();

                        cmd2.Parameters.Clear();
                        cmd2.Connection = con;
                        cmd2.CommandText = "update dbo.team set [password]=@pass where id=@id";
                        cmd2.Parameters.AddWithValue("@pass", materialSingleLineTextField1.Text);
                        cmd2.Parameters.AddWithValue("@id", id3);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();

                        //-----------------------------------------------

                        validateerrorforteammatetimer.Start(); insertpopupanimation.Show(bunifuCustomLabel6); bunifuCustomLabel6.Visible = true; bunifuCustomLabel6.BringToFront();
                        editteammate_panel.Visible = false;
                        //-------------------------------------------------
                    }
                    else
                    {
                        mar = false;
                        SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                        SqlCommand cmd2 = new SqlCommand();

                        cmd2.Parameters.Clear();
                        cmd2.Connection = con;
                        cmd2.CommandText = "update dbo.team set [password]=@pass where email=@em";
                        cmd2.Parameters.AddWithValue("@pass", materialSingleLineTextField1.Text);
                        cmd2.Parameters.AddWithValue("@em", loginDEVELOPBUILDING.Email);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();

                        //-----------------------------------------------

                        validateerrorforteammatetimer.Start(); insertpopupanimation.Show(bunifuCustomLabel6); bunifuCustomLabel6.Visible = true; bunifuCustomLabel6.BringToFront();
                        editteammate_panel.Visible = false;
                        //-------------------------------------------------
                    }
                }
            }

        }

        private void teamgrid_MouseMove(object sender, MouseEventArgs e)
        {

            DataGridView.HitTestInfo hit = teamgrid.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                teamgrid.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void bunifuiOSSwitch1_OnValueChange(object sender, EventArgs e)
        {

        }

        private void bunifuiOSSwitch2_OnValueChange(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char teamgridid = e.KeyChar;
            if (!Char.IsDigit(teamgridid) && teamgridid != 8 && teamgridid != 46)
            { e.Handled = true; }
        }

        public bool mar = false;
        private void profilepicture_Click(object sender, EventArgs e)
        {
            mar = true;
            zoomoutpictureboxes();
            SqlConnection con = new SqlConnection(stringcon); //CONNECTION
            pictureBox2.Image = profilepicture.Image;

            con.Open();

            SqlCommand cmd2 = new SqlCommand();

            cmd2.Connection = con;
            cmd2.CommandText = "select * from team where email=@em";
            cmd2.Parameters.AddWithValue("@em", loginDEVELOPBUILDING.Email);
            cmd2.ExecuteNonQuery();

            DataTable dt2 = new DataTable();
            DataTable dt12 = new DataTable();
            SqlDataAdapter da12 = new SqlDataAdapter(cmd2);

            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da12.Fill(ds2);
            da2.Fill(dt2);

            foreach (DataRow dr in dt2.Rows)
            {

                string fullname;
                string firstname;
                string lastname;
                int role;
                firstname = dr["firstname"].ToString();
                lastname = dr["lastname"].ToString();
                label19.Text = dr["phonenumber"].ToString();
                dateandtime_label.Text = Convert.ToDateTime(dr["registerdata"]).ToString("dd/MM/yyyy - HH:mm");
                label20.Text = dr["email"].ToString();
                label21.Text = dr["function"].ToString();
                role = Convert.ToInt16(dr["role"].ToString());

                if (role == 0) { label22.Text = "Manager"; } else { label22.Text = "Agent"; }

                label23.Text = dr["asociateclients"].ToString();
                label24.Text = dr["asociateproperties"].ToString();
                personaldescriprion_label.Text = dr["asociateproperties"].ToString();
                label17.Text = firstname;
                label18.Text = lastname;
                fullname = lastname + " " + firstname;
                agentname_label.Text = fullname;
                personaldescriprion_label.Text = dr["personaldescription"].ToString();

            }
            con.Close();
            con.Open();
            if (manageroragent == 0)
            {
                editteammate_button.Visible = true;
                passwordreset_button.Visible = true;
                if (loginDEVELOPBUILDING.Email == label20.Text)
                {
                    passwordreset_button.Location = new Point(1101, 622);
                    editteammate_button.Location = new Point(974, 622);
                    fire_button.Visible = false;
                }
                else
                {
                    passwordreset_button.Location = new Point(974, 622);
                    editteammate_button.Location = new Point(847, 622);
                    fire_button.Visible = true;
                }
            }
            else
            {
                if (loginDEVELOPBUILDING.Email == label20.Text)
                {
                    passwordreset_button.Location = new Point(1101, 622);
                    editteammate_button.Location = new Point(974, 622);
                    editteammate_button.Visible = true;
                    passwordreset_button.Visible = true;

                }
                else
                {
                    editteammate_button.Visible = false;
                    passwordreset_button.Visible = false;
                }
            }

            team_panel.Visible = true;
            team_panel.BringToFront();
            interogate_teammate_panel.Visible = true;
            interogate_teammate_panel.BringToFront();

        }


        private void materialSingleLineTextField9_TextChanged_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            if (dropdownlanguage.selectedValue == "RO")
            {
                SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Da' when status=0 then 'Nu' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where id like @id and CONCAT(team.lastname,' ',team.firstname) like @name", con);
                cmdread3.Parameters.AddWithValue("@id", materialSingleLineTextField8.Text + "%");
                cmdread3.Parameters.AddWithValue("@name", "%" + materialSingleLineTextField9.Text + "%");
                cmdread3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                sda2.Fill(dt3);
                teamgrid.DataSource = dt3;

                //store autosized widths
                int colw = teamgrid.Columns[0].Width;
                //remove autosizing
                teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                teamgrid.Columns[0].Width = 50;

                con.Close();
            }
            else
            {
                cmd.Parameters.Clear();
                SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Yes' when status=0 then 'No' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where id like @id and CONCAT(team.lastname,' ',team.firstname) like @name", con);
                cmdread3.Parameters.AddWithValue("@id", materialSingleLineTextField8.Text + "%");
                cmdread3.Parameters.AddWithValue("@name", "%" + materialSingleLineTextField9.Text + "%");

                cmdread3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                sda2.Fill(dt3);
                teamgrid.DataSource = dt3;

                //store autosized widths
                int colw = teamgrid.Columns[0].Width;
                //remove autosizing
                teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                teamgrid.Columns[0].Width = 50;

                con.Close();
            }
        }

        private void bunifuiOSSwitch2_OnValueChange_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            if (bunifuiOSSwitch1.Value == true && bunifuiOSSwitch2.Value == true)
            {
                if (dropdownlanguage.selectedValue == "RO")
                {
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Da' when status=0 then 'Nu' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=1 and [role]=0", con);
                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }
                else
                {
                    cmd.Parameters.Clear();
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Yes' when status=0 then 'No' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=1 and [role]=0", con);

                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }


            }
            if (bunifuiOSSwitch1.Value == false && bunifuiOSSwitch2.Value == false)
            {
                if (dropdownlanguage.selectedValue == "RO")
                {
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Da' when status=0 then 'Nu' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=0 and [role]=1", con);
                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }
                else
                {
                    cmd.Parameters.Clear();
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Yes' when status=0 then 'No' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=0 and [role]=1", con);

                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }
            }
            if (bunifuiOSSwitch1.Value == true && bunifuiOSSwitch2.Value == false)
            {
                if (dropdownlanguage.selectedValue == "RO")
                {
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Da' when status=0 then 'Nu' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=1 and [role]=1", con);
                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }
                else
                {
                    cmd.Parameters.Clear();
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Yes' when status=0 then 'No' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=1 and [role]=1", con);

                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }
            }
            if (bunifuiOSSwitch1.Value == false && bunifuiOSSwitch2.Value == true)
            {
                if (dropdownlanguage.selectedValue == "RO")
                {
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Da' when status=0 then 'Nu' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=0 and [role]=0", con);
                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }
                else
                {
                    cmd.Parameters.Clear();
                    SqlCommand cmdread3 = new SqlCommand("Select team.id as ID, CONCAT(team.lastname,' ',team.firstname) as NAME, case when status = 1 then 'Yes' when status=0 then 'No' END AS Active, case when [role] = 0 then 'Manager' when [role] = 1 then 'Agent' END AS Role, team.asociateclients as [REGISTRED CLIENTS], team.numberoflogin as [SISTEM LOGIN],FORMAT(team.lastlogin,'dd/MM/yyyy - HH:mm') as [LAST LOGIN],FORMAT(team.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from dbo.team where [status]=0 and [role]=0", con);

                    cmdread3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmdread3);
                    sda2.Fill(dt3);
                    teamgrid.DataSource = dt3;

                    //store autosized widths
                    int colw = teamgrid.Columns[0].Width;
                    //remove autosizing
                    teamgrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //set width to calculated by autosize
                    teamgrid.Columns[0].Width = 50;

                    con.Close();
                }
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            materialSingleLineTextField8.Text = string.Empty;
            materialSingleLineTextField9.Text = string.Empty;
            bunifuiOSSwitch1.Value = true;
            bunifuiOSSwitch2.Value = true;
            Teamgrid();
        }

        //--------------------------------------------------------------------------------------------------------PROJECT BLOCK----------------------------
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            zoomoutpictureboxes();
            markerinitialize++;
            messageboxshow = true;
            addproject_panel.Visible = true;
            addproject_panel.BringToFront();
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInitial, LngInitial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 20;
            gMapControl1.Zoom = 15;
            gMapControl1.AutoScroll = true;
            markerOverlay = new GMapOverlay("Marker");
            marker = new GMarkerGoogle(new PointLatLng(LatInitial, LngInitial), GMarkerGoogleType.red_dot);
            markerOverlay.Markers.Add(marker);
            marker.ToolTipMode = MarkerTooltipMode.Never;
            gMapControl1.Overlays.Add(marker.Overlay);
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            label71.Text = lat.ToString();
            label71.Visible = true; label71.Parent = project_region; label71.Dock = DockStyle.Fill; label71.BringToFront();
            label70.Text = lng.ToString();
            label70.Visible = true; label70.Parent = project_city; label70.Dock = DockStyle.Fill; label70.BringToFront();
            marker.Position = new PointLatLng(lat, lng);
        }
        public byte[] a, a1, a2, a3, a4;
        public int mapviewwebsite;
        SqlCommand projectinserts = new SqlCommand();
        public void projectinsert()
        {

            MemoryStream ms = new MemoryStream();
            pictureBox6.Image.Save(ms, pictureBox6.Image.RawFormat);
            a = ms.ToArray();
            ms.Close();


            MemoryStream ms1 = new MemoryStream();
            pictureBox7.Image.Save(ms1, pictureBox7.Image.RawFormat);
            a1 = ms1.ToArray();
            ms1.Close();


            MemoryStream ms2 = new MemoryStream();
            pictureBox8.Image.Save(ms2, pictureBox8.Image.RawFormat);
            a2 = ms2.ToArray();
            ms2.Close();


            MemoryStream ms3 = new MemoryStream();
            pictureBox9.Image.Save(ms3, pictureBox9.Image.RawFormat);
            a3 = ms3.ToArray();
            ms3.Close();


            MemoryStream ms4 = new MemoryStream();
            pictureBox10.Image.Save(ms4, pictureBox10.Image.RawFormat);
            a4 = ms4.ToArray();
            ms4.Close();

            SqlConnection con = new SqlConnection(stringcon);
            projectinserts.Parameters.Clear();
            projectinserts.Connection = con;
            projectinserts.CommandText = "insert into project(name,email,year,energeticefficiency,statusconstruction,lat,lng,address,surfaceparking,undergroundparking,lodge,projectdescription,viewmap,imagetitle1,imagetitle2,imagetitle3,imagetitle4,imagetitle5,image1,image2,image3,image4,image5,registerdata,agentname,editdata,editname) values(@name,@email,@year,@energeticefficiency,@statusconstruction,@lat,@lng,@address,@surfaceparking,@undergroundparking,@lodge,@projectdescription,@viewmap,@imagetitle1,@imagetitle2,@imagetitle3,@imagetitle4,@imagetitle5,@image1,@image2,@image3,@image4,@image5,@registerdata,(select concat(lastname,' ',firstname) as Name from team where email=@emailagentinsert),@editdata,(select concat(lastname,' ',firstname) as Name from team where email=@emailagentinsert))";
            if (materialRadioButton1.Checked == true) { mapviewwebsite = 0; }
            if (materialRadioButton2.Checked == true) { mapviewwebsite = 1; }
            if (materialRadioButton3.Checked == true) { mapviewwebsite = 2; }
            projectinserts.Parameters.AddWithValue("@name", materialSingleLineTextField19.Text);
            projectinserts.Parameters.AddWithValue("@email", materialSingleLineTextField18.Text);
            projectinserts.Parameters.AddWithValue("@year", materialSingleLineTextField11.Text);
            projectinserts.Parameters.AddWithValue("@energeticefficiency", materialSingleLineTextField10.Text);
            projectinserts.Parameters.AddWithValue("@statusconstruction", bunifuDropdown2.selectedIndex);
            projectinserts.Parameters.AddWithValue("@lat", Convert.ToDouble(label71.Text));
            projectinserts.Parameters.AddWithValue("@lng", Convert.ToDouble(label70.Text));
            projectinserts.Parameters.AddWithValue("@address", materialSingleLineTextField17.Text);
            projectinserts.Parameters.AddWithValue("@surfaceparking", materialCheckBox1.Checked);
            projectinserts.Parameters.AddWithValue("@undergroundparking", materialCheckBox2.Checked);
            projectinserts.Parameters.AddWithValue("@lodge", materialCheckBox3.Checked);
            projectinserts.Parameters.AddWithValue("@projectdescription", jwRichTextBox3.Text);
            projectinserts.Parameters.AddWithValue("@viewmap", mapviewwebsite);
            projectinserts.Parameters.AddWithValue("@imagetitle1", materialSingleLineTextField20.Text);
            projectinserts.Parameters.AddWithValue("@imagetitle2", materialSingleLineTextField21.Text);
            projectinserts.Parameters.AddWithValue("@imagetitle3", materialSingleLineTextField22.Text);
            projectinserts.Parameters.AddWithValue("@imagetitle4", materialSingleLineTextField23.Text);
            projectinserts.Parameters.AddWithValue("@imagetitle5", materialSingleLineTextField24.Text);
            projectinserts.Parameters.AddWithValue("@image1", a);
            projectinserts.Parameters.AddWithValue("@image2", a1);
            projectinserts.Parameters.AddWithValue("@image3", a2);
            projectinserts.Parameters.AddWithValue("@image4", a3);
            projectinserts.Parameters.AddWithValue("@image5", a4);
            projectinserts.Parameters.AddWithValue("@registerdata", DateTime.Now.ToString("yyyy-MM-dd HH: mm:ss"));
            projectinserts.Parameters.AddWithValue("@editdata", DateTime.Now.ToString("yyyy-MM-dd HH: mm:ss"));
            projectinserts.Parameters.AddWithValue("@emailagentinsert", loginDEVELOPBUILDING.Email);

            con.Open();
            projectinserts.ExecuteNonQuery();
            con.Close();


        }

        public void projectgrid()
        {
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            if (dropdownlanguage.selectedValue == "RO")
            {
                cmd.Connection = con;
                cmd.CommandText = "select id,agentname,image1,name,case when statussell = 0 then 'Disponibil' when statussell=1 then 'Vândut integral' END AS Statussell,case when statusconstruction=0 then 'Finalizat' when statusconstruction=1 then 'În construcție' when statusconstruction=2 then 'Proiect'  end as statusconstruction,propertiesassociated,email,FORMAT(project.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from project";
                cmd.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd);
                sda2.Fill(dt3);
                bunifuCustomDataGrid1.DataSource = dt3;

                //store autosized widths
                int colw = bunifuCustomDataGrid1.Columns[0].Width;
                //remove autosizing
                bunifuCustomDataGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                bunifuCustomDataGrid1.Columns[0].Width = 50;
                //store autosized widths
                int colw1 = bunifuCustomDataGrid1.Columns[2].Width;
                //remove autosizing
                bunifuCustomDataGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                ((DataGridViewImageColumn)bunifuCustomDataGrid1.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                //set width to calculated by autosize
                bunifuCustomDataGrid1.Columns[2].Width = 100;
                bunifuCustomDataGrid1.Columns[2].DefaultCellStyle.Padding = new Padding(3, 3, 3, 3);
                bunifuCustomDataGrid1.Columns[3].Width = 200;
                bunifuCustomDataGrid1.Columns[4].Width = 130;
                bunifuCustomDataGrid1.Columns[5].Width = 130;
                bunifuCustomDataGrid1.Columns[6].Width = 150;
                bunifuCustomDataGrid1.Columns[8].Width = 150;
                con.Close();
            }
            else
            {
                cmd.Connection = con;
                cmd.CommandText = "select id,agentname,image1,name,case when statussell = 0 then 'Available' when statussell=1 then 'Sold out' END AS Statussell,case when statusconstruction=0 then 'Completed' when statusconstruction=1 then 'Underconstruction' when statusconstruction=2 then 'Design'  end as statusconstruction,propertiesassociated,email,FORMAT(project.registerdata,'dd/MM/yyyy - HH:mm') AS 'DATA' from project";
                cmd.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd);
                sda2.Fill(dt3);
                bunifuCustomDataGrid1.DataSource = dt3;

                //store autosized widths
                int colw = bunifuCustomDataGrid1.Columns[0].Width;
                //remove autosizing
                bunifuCustomDataGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                bunifuCustomDataGrid1.Columns[0].Width = 50;
                //store autosized widths
                int colw1 = bunifuCustomDataGrid1.Columns[2].Width;
                //remove autosizing
                bunifuCustomDataGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                ((DataGridViewImageColumn)bunifuCustomDataGrid1.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                //set width to calculated by autosize
                bunifuCustomDataGrid1.Columns[2].Width = 100;
                bunifuCustomDataGrid1.Columns[2].DefaultCellStyle.Padding = new Padding(3, 3, 3, 3);
                bunifuCustomDataGrid1.Columns[3].Width = 200;
                bunifuCustomDataGrid1.Columns[4].Width = 130;
                bunifuCustomDataGrid1.Columns[5].Width = 130;
                bunifuCustomDataGrid1.Columns[6].Width = 150;
                bunifuCustomDataGrid1.Columns[8].Width = 150;

                con.Close();
            }
        }
        private void materialRadioButton1_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleChinaSatelliteMap;
        }

        private void materialRadioButton2_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void materialRadioButton3_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleTerrainMap;
        }

        private void bunifuCustomDataGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = bunifuCustomDataGrid1.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                bunifuCustomDataGrid1.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void bunifuCustomDataGrid1_MouseHover(object sender, EventArgs e)
        {
            bunifuCustomDataGrid1.Focus();
        }

        public string path1, path2, path3;
        private void file1_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF (*.pdf*)|*.pdf*";// file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                path1 = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path1, FileMode.Open), new UTF8Encoding()))
                {
                    file1.FileLocation = path1;
                    file2.Visible = true;
                    pictureBox22.Visible = true;
                    materialSingleLineTextField38.Visible = true;
                    label140.Visible = true;
                }   
            }
        }

        private void file2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF (*.pdf*)|*.pdf*";// file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                path2 = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path2, FileMode.Open), new UTF8Encoding()))
                {
                    file2.FileLocation = path2;
                    file3.Visible = true;
                    pictureBox23.Visible = true;
                    materialSingleLineTextField39.Visible = true;
                    label141.Visible = true;
                }
            }
        }
        StreamReader reader3;
        private void file3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF (*.pdf*)|*.pdf*";// file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                path3 = dialog.FileName; // get name of file
                using (reader3 = new StreamReader(new FileStream(path3, FileMode.Open), new UTF8Encoding()))
                {
                    file3.FileLocation = path3;
                }
            }
        }
        public double lateditproject = 44.4268078899587;
        public double lngeditproject = 26.102442741394;

        private void bunifuFlatButton11_Click(object sender, EventArgs e)
        {
            //----------------------------------------------------
            materialSingleLineTextField31.Text = string.Empty;
            materialSingleLineTextField34.Text = string.Empty;
            materialSingleLineTextField37.Text = string.Empty;
            materialSingleLineTextField38.Text = string.Empty;
            materialSingleLineTextField39.Text = string.Empty;
            materialSingleLineTextField29.Text = string.Empty;
            materialSingleLineTextField28.Text = string.Empty;
            materialSingleLineTextField27.Text = string.Empty;
            materialSingleLineTextField26.Text = string.Empty;
            materialSingleLineTextField25.Text = string.Empty;
            jwRichTextBox4.Text = string.Empty;
            materialCheckBox6.Checked = false;
            materialCheckBox5.Checked = false;
            materialCheckBox4.Checked = false;
            //----------------------------------------------------
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();


            cmd.Connection = con;
            cmd.CommandText = "select * from project where id=" + idproject + "";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                label137.Text = dr["name"].ToString();
                materialSingleLineTextField32.Text = dr["name"].ToString();
                materialSingleLineTextField31.Text = dr["email"].ToString();
                materialSingleLineTextField30.Text = dr["year"].ToString();
                materialSingleLineTextField34.Text = dr["energeticefficiency"].ToString();
                bunifuDropdown3.selectedIndex = Convert.ToInt16(dr["statusconstruction"].ToString());
                lateditproject = Convert.ToDouble(dr["lat"].ToString());
                lngeditproject = Convert.ToDouble(dr["lng"].ToString());
                label132.Text = Convert.ToString(lateditproject);
                label124.Text = Convert.ToString(lngeditproject);
                materialSingleLineTextField33.Text = dr["address"].ToString();
                if (dr["surfaceparking"].ToString() == "True") { materialCheckBox6.Checked = true; }
                if (dr["undergroundparking"].ToString() == "True") { materialCheckBox5.Checked = true; }
                if (dr["lodge"].ToString() == "True") { materialCheckBox4.Checked = true; }
                jwRichTextBox4.Text = dr["projectdescription"].ToString();
                viewmapint = Convert.ToInt16(dr["viewmap"].ToString());

                byte[] ap = (byte[])ds.Tables[0].Rows[0]["image1"];
                MemoryStream ms = new MemoryStream(ap);
                pictureBox20.Image = Image.FromStream(ms);

                byte[] ap2 = (byte[])ds.Tables[0].Rows[0]["image2"];
                MemoryStream ms2 = new MemoryStream(ap2);
                pictureBox19.Image = Image.FromStream(ms2);

                byte[] ap3 = (byte[])ds.Tables[0].Rows[0]["image3"];
                MemoryStream ms3 = new MemoryStream(ap3);
                pictureBox18.Image = Image.FromStream(ms3);

                byte[] ap4 = (byte[])ds.Tables[0].Rows[0]["image4"];
                MemoryStream ms4 = new MemoryStream(ap4);
                pictureBox17.Image = Image.FromStream(ms4);

                byte[] ap5 = (byte[])ds.Tables[0].Rows[0]["image5"];
                MemoryStream ms5 = new MemoryStream(ap5);
                pictureBox16.Image = Image.FromStream(ms5);

                materialSingleLineTextField29.Text = dr["imagetitle1"].ToString();
                materialSingleLineTextField28.Text = dr["imagetitle2"].ToString();
                materialSingleLineTextField27.Text = dr["imagetitle3"].ToString();
                materialSingleLineTextField26.Text = dr["imagetitle4"].ToString();
                materialSingleLineTextField25.Text = dr["imagetitle5"].ToString();
            }


            con.Close();

            //---------------------------------------------------------------------------------------------------------------------------------------------
            messageboxshow = true;
            label132.Parent = materialSingleLineTextField36; label124.Parent = materialSingleLineTextField35; label132.Visible = true; label132.Dock = DockStyle.Fill; label132.BringToFront(); label124.Visible = true; label124.Dock = DockStyle.Fill; label124.BringToFront();
            interogateproject_panel.Visible = false;
            editproject_panel.Visible = true;
            editproject_panel.BringToFront();
            gMapControl3.DragButton = MouseButtons.Left;
            gMapControl3.CanDragMap = true;
            gMapControl3.MapProvider = GMapProviders.GoogleMap;
            gMapControl3.Position = new PointLatLng(lateditproject, lngeditproject);
            gMapControl3.MinZoom = 0;
            gMapControl3.MaxZoom = 20;
            gMapControl3.Zoom = 15;
            gMapControl3.AutoScroll = true;
            markerOverlay = new GMapOverlay("Marker");
            marker = new GMarkerGoogle(new PointLatLng(lateditproject, lngeditproject), GMarkerGoogleType.red_dot);
            markerOverlay.Markers.Add(marker);
            marker.ToolTipMode = MarkerTooltipMode.Never;
            gMapControl3.Overlays.Add(marker.Overlay);
            if (viewmapint == 0) { materialRadioButton6.Checked = true; gMapControl3.MapProvider = GMapProviders.GoogleChinaSatelliteMap; }
            if (viewmapint == 1) { materialRadioButton5.Checked = true; gMapControl3.MapProvider = GMapProviders.GoogleMap; }
            if (viewmapint == 2) { materialRadioButton5.Checked = true; gMapControl3.MapProvider = GMapProviders.GoogleTerrainMap; }

            //----------------------------------------------------------------------------------------------------------------------------------------------
        }
        public byte[] fileinsert1, fileinsert2, fileinsert3;

        public void insertfile1editproject()
        {

            using (var stream = new FileStream(path1, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    fileinsert1 = reader.ReadBytes((int)stream.Length);
                    SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "update dbo.project set fileproject1=@fl, fileprojecttitle1=@t where id=@id";
                    cmd2.Parameters.Clear();
                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@fl", fileinsert1);
                    cmd2.Parameters.AddWithValue("@t", materialSingleLineTextField37.Text);
                    cmd2.Parameters.AddWithValue("@id", idproject);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void insertfile2editproject()
        {

            using (var stream = new FileStream(path2, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    fileinsert2 = reader.ReadBytes((int)stream.Length);
                    SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "update dbo.project set fileproject2=@fl, fileprojecttitle2=@t where id=@id";
                    cmd2.Parameters.Clear();
                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@fl", fileinsert2);
                    cmd2.Parameters.AddWithValue("@t", materialSingleLineTextField38.Text);
                    cmd2.Parameters.AddWithValue("@id", idproject);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void insertfile3editproject()
        {

            using (var stream = new FileStream(path3, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    fileinsert3 = reader.ReadBytes((int)stream.Length);
                    SqlConnection con = new SqlConnection(stringcon); //CONNECTION
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "update dbo.project set fileproject3=@fl, fileprojecttitle3=@t where id=@id";
                    cmd2.Parameters.Clear();
                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@fl", fileinsert3);
                    cmd2.Parameters.AddWithValue("@t", materialSingleLineTextField39.Text);
                    cmd2.Parameters.AddWithValue("@id", idproject);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        private void bunifuFlatButton17_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(materialSingleLineTextField32.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField32.Text)) { label127.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(materialSingleLineTextField30.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField30.Text)) { label126.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(materialSingleLineTextField33.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField33.Text)) { label130.Visible = true; validateerrorforteammatetimer.Start(); }
            else//code for insert project in databae
            {
                messageboxshow = false;
                //-----------------------------------------------

                if (file1.FileLocation == "Locație document" || file1.FileLocation == "Document path") { } else { insertfile1editproject(); }
                if (file2.FileLocation == "Locație document" || file2.FileLocation == "Document path") { } else { insertfile2editproject(); }
                if (file3.FileLocation == "Locație document" || file3.FileLocation == "Document path") { } else { insertfile3editproject(); }


                //-----------------------------------------------

                validateerrorforteammatetimer.Start(); insertpopupanimation.Show(bunifuCustomLabel6); bunifuCustomLabel6.Visible = true; bunifuCustomLabel6.BringToFront();
                addproject_panel.Visible = false; editproject_panel.Visible = false;
                projectgrid();
                //-------------------------------------------------
            }
        }

        private void gMapControl3_MouseClick(object sender, MouseEventArgs e)
        {

            double lateditproject = gMapControl3.FromLocalToLatLng(e.X, e.Y).Lat;
            double lngeditproject = gMapControl3.FromLocalToLatLng(e.X, e.Y).Lng;
            label132.Text = lateditproject.ToString();
            label124.Text = lngeditproject.ToString();
            marker.Position = new PointLatLng(lateditproject, lngeditproject);
        }
        public byte[] ap4;

        private void bunifuFlatButton12_Click(object sender, EventArgs e)
        {

            FileStream FS = null;
            byte[] dbbyte;
            //Get selected file ID field
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            cmd = new SqlCommand("select * from project where id='" + idproject + "'", con);
           SqlDataAdapter da = new SqlDataAdapter(cmd);
           DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                 dbbyte = (byte[])dt.Rows[0]["fileproject1"];
                //store file Temporarily                
                string filepath = "D:\\temp1.pdf";
                //Assign File path create file
                FS = new FileStream(filepath, System.IO.FileMode.Create);
                //Write bytes to create file
                FS.Write(dbbyte, 0, dbbyte.Length);
                //Close FileStream instance
                FS.Close();
                // Open fila after write 
                //Create instance for process class
                Process Proc = new Process();
                //assign file path for process
                Proc.StartInfo.FileName = filepath;
                Proc.Start();
            }
            con.Close();
        
   

        }

        private void bunifuFlatButton15_Click(object sender, EventArgs e)
        {
            FileStream FS = null;
            byte[] dbbyte;
            //Get selected file ID field
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            cmd = new SqlCommand("select * from project where id='" + idproject + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                dbbyte = (byte[])dt.Rows[0]["fileproject2"];
                //store file Temporarily                
                string filepath = "D:\\temp2.pdf";
                //Assign File path create file
                FS = new FileStream(filepath, System.IO.FileMode.Create);
                //Write bytes to create file
                FS.Write(dbbyte, 0, dbbyte.Length);
                //Close FileStream instance
                FS.Close();
                // Open fila after write 
                //Create instance for process class
                Process Proc = new Process();
                //assign file path for process
                Proc.StartInfo.FileName = filepath;
                Proc.Start();
            }
            con.Close();

        }

        private void bunifuFlatButton16_Click(object sender, EventArgs e)
        {
            FileStream FS = null;
            byte[] dbbyte;
            //Get selected file ID field
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();
            cmd = new SqlCommand("select * from project where id='" + idproject + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                dbbyte = (byte[])dt.Rows[0]["fileproject3"];
                //store file Temporarily                
                string filepath = "D:\\temp3.pdf";
                //Assign File path create file
                FS = new FileStream(filepath, System.IO.FileMode.Create);
                //Write bytes to create file
                FS.Write(dbbyte, 0, dbbyte.Length);
                //Close FileStream instance
                FS.Close();
                // Open fila after write 
                //Create instance for process class
                Process Proc = new Process();
                //assign file path for process
                Proc.StartInfo.FileName = filepath;
                Proc.Start();
            }
            con.Close();

        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            while (messageboxshow == false || messageboxshow == true && messageboxresult == false || messageboxresult == true)
            {
                if (messageboxshow == false && messageboxresult == false)
                {
                    clients_panel.Visible = false;
                    properties_panel.Visible = false;
                    projects_panel.Visible = true;
                    financial_panel.Visible = false;
                    statistics_panel.Visible = false;
                    team_panel.Visible = false;
                    website_panel.Visible = false;
                    dashboard_panel.Visible = false;
                    interogateproject_panel.Visible = false;
                    editproject_panel.Visible = false;
                    addproject_panel.Visible = false;
                    marker.IsVisible = false;
                    slidebarmenu.BringToFront();

                    //---------------------------------------------
                    break;
                }
                else
                {
                    messageboxforexit();
                }
                if (messageboxshow == true && messageboxresult == false)
                {
                    break;
                }
                else //if (messageboxshow == true && messageboxresult)
                {
                    messageboxshow = false;
                    messageboxresult = false;
                    //-------------------------------------------
                    restartformularaddproject();
                    marker.IsVisible = false;
                    //-------------------------------------------
                    clients_panel.Visible = false;
                    properties_panel.Visible = false;
                    projects_panel.Visible = true;
                    financial_panel.Visible = false;
                    statistics_panel.Visible = false;
                    team_panel.Visible = false;
                    website_panel.Visible = false;
                    dashboard_panel.Visible = false;
                    interogateproject_panel.Visible = false;
                    editproject_panel.Visible = false;
                    addproject_panel.Visible = false;
                    slidebarmenu.BringToFront();
                    pictureBox1.Focus();
                    //-------------------------------------------
                    break;
                }
            }
            zoomoutpictureboxes();
        }

        int idproject;

        private int statussell, statusconstruction, viewmapint;

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            double Latinterogate = 44.4268078899587;
            double Lnginterogate = 26.102442741394;
            markerinitialize++;
            //---------------------------------------------------------------------------------

            int id;
            id = Convert.ToInt32(bunifuCustomDataGrid1.Rows[e.RowIndex].Cells["id"].Value.ToString());
            idproject = Convert.ToInt32(bunifuCustomDataGrid1.Rows[e.RowIndex].Cells["id"].Value.ToString());
            SqlConnection con = new SqlConnection(stringcon);
            con.Open();


            cmd.Connection = con;
            cmd.CommandText = "select * from project where id=" + id + "";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            da.Fill(dt);




            foreach (DataRow dr in dt.Rows)
            {
                bunifuFlatButton12.Text = dr["fileprojecttitle1"].ToString();
                bunifuFlatButton15.Text = dr["fileprojecttitle2"].ToString();
                bunifuFlatButton16.Text = dr["fileprojecttitle3"].ToString();
                label98.Text = dr["name"].ToString();
                label83.Text = Convert.ToDateTime(dr["registerdata"]).ToString("dd/MM/yyyy - HH:mm");
                label91.Text = dr["agentname"].ToString();
                label87.Text = Convert.ToDateTime(dr["editdata"]).ToString("dd/MM/yyyy - HH:mm");
                label92.Text = dr["editname"].ToString();
                label81.Text = label98.Text;
                label93.Text = dr["email"].ToString();
                label95.Text = dr["year"].ToString();
                statusconstruction = Convert.ToInt16(dr["statusconstruction"].ToString());
                if (dropdownlanguage.selectedValue == "EN") { if (statusconstruction == 0) { label101.Text = "Completed"; } if (statusconstruction == 1) { label101.Text = "Underconstruction"; } if (statusconstruction == 2) { label101.Text = "Design"; } } else { { if (statusconstruction == 0) { label101.Text = "Finalizat"; } if (statusconstruction == 1) { label101.Text = "În construcţie"; } if (statusconstruction == 2) { label101.Text = "Proiect"; } } }
                statussell = Convert.ToInt16(dr["statussell"].ToString());
                if (dropdownlanguage.selectedValue == "EN") { if (statussell == 0) { label99.Text = "Available"; } if (statussell == 1) { label99.Text = "Sold out"; } } else { { if (statussell == 0) { label99.Text = "Disponibil"; } if (statussell == 1) { label99.Text = "Vândut integral"; } } }
                label103.Text = dr["surfaceparking"].ToString();
                if (dropdownlanguage.selectedValue == "EN") { if (label103.Text == "True") { label103.Text = "Yes"; } else { { label103.Text = "No"; } } } else { if (label103.Text == "True") { label103.Text = "Da"; } else { { label103.Text = "Nu"; } } }
                label105.Text = dr["undergroundparking"].ToString();
                if (dropdownlanguage.selectedValue == "EN") { if (label105.Text == "True") { label105.Text = "Yes"; } else { { label105.Text = "No"; } } } else { if (label105.Text == "True") { label105.Text = "Da"; } else { { label105.Text = "Nu"; } } }
                label107.Text = dr["lodge"].ToString();
                if (dropdownlanguage.selectedValue == "EN") { if (label107.Text == "True") { label107.Text = "Yes"; } else { { label107.Text = "No"; } } } else { if (label107.Text == "True") { label107.Text = "Da"; } else { { label107.Text = "Nu"; } } }
                label109.Text = dr["energeticefficiency"].ToString();
              
                label97.Text = dr["projectdescription"].ToString();
                label113.Text = dr["address"].ToString();
                Latinterogate = Convert.ToDouble(dr["lat"].ToString());
                Lnginterogate = Convert.ToDouble(dr["lng"].ToString());

                byte[] ap = (byte[])ds.Tables[0].Rows[0]["image1"];
                MemoryStream ms = new MemoryStream(ap);
                pictureBox15.Image = Image.FromStream(ms);
                byte[] ap2 = (byte[])ds.Tables[0].Rows[0]["image2"];
                MemoryStream ms2 = new MemoryStream(ap2);
                pictureBox14.Image = Image.FromStream(ms2);
                byte[] ap3 = (byte[])ds.Tables[0].Rows[0]["image3"];
                MemoryStream ms3 = new MemoryStream(ap3);
                pictureBox13.Image = Image.FromStream(ms3);
                byte[] ap4 = (byte[])ds.Tables[0].Rows[0]["image4"];
                MemoryStream ms4 = new MemoryStream(ap4);
                pictureBox12.Image = Image.FromStream(ms4);
                byte[] ap5 = (byte[])ds.Tables[0].Rows[0]["image5"];
                MemoryStream ms5 = new MemoryStream(ap5);
                pictureBox11.Image = Image.FromStream(ms5);
                label115.Text = dr["imagetitle1"].ToString();
                label116.Text = dr["imagetitle2"].ToString();
                label117.Text = dr["imagetitle3"].ToString();
                label118.Text = dr["imagetitle4"].ToString();
                label119.Text = dr["imagetitle5"].ToString();

                viewmapint = Convert.ToInt16(dr["viewmap"].ToString());
                if (dropdownlanguage.selectedValue == "EN") { if (viewmapint == 0)  {   label114.Text = "Satellite";  }  if (viewmapint == 1)  {  label114.Text = "Standard";  } if (viewmapint == 2) { label114.Text = "Relieve";} } else {  { if (viewmapint == 0) {    label114.Text = "Satelit"; }   if (viewmapint == 1) {  label114.Text = "Normal"; } if (viewmapint == 2) { label114.Text = "Relief"; }} }
            }
            con.Close();
            
            //---------------------------------------------------------------------------------
            gMapControl2.DragButton = MouseButtons.Left;
            gMapControl2.CanDragMap = true;
            gMapControl2.MapProvider = GMapProviders.GoogleMap;
            gMapControl2.Position = new PointLatLng(Latinterogate, Lnginterogate);
            gMapControl2.MinZoom = 0;
            gMapControl2.MaxZoom = 20;
            gMapControl2.Zoom = 15;
            gMapControl2.AutoScroll = true;
            markerOverlay = new GMapOverlay("Marker");
            marker = new GMarkerGoogle(new PointLatLng(Latinterogate, Lnginterogate), GMarkerGoogleType.red_dot);
            markerOverlay.Markers.Add(marker);
            marker.ToolTipMode = MarkerTooltipMode.Never;
            gMapControl2.Overlays.Add(marker.Overlay);
            //---------------------------------------------------------------------------------
            if (viewmapint == 0){ gMapControl2.MapProvider = GMapProviders.GoogleChinaSatelliteMap; }if (viewmapint == 1){gMapControl2.MapProvider = GMapProviders.GoogleMap;} if (viewmapint == 2){ gMapControl2.MapProvider = GMapProviders.GoogleTerrainMap;}
            //---------------------------------------------------------------------------------
            interogateproject_panel.Visible = true;
            interogateproject_panel.BringToFront();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox6.Image = Image.FromFile(f.FileName);
                pictureBox7.Visible = true;
                materialSingleLineTextField21.Visible = true;
            }f.Dispose();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox7.Image = Image.FromFile(f.FileName);
                pictureBox8.Visible = true;
                materialSingleLineTextField22.Visible = true;
            }f.Dispose();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox8.Image = Image.FromFile(f.FileName);
                pictureBox9.Visible = true;
                materialSingleLineTextField23.Visible = true;
            }f.Dispose();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox9.Image = Image.FromFile(f.FileName);
                pictureBox10.Visible = true;
                materialSingleLineTextField24.Visible = true;
            }f.Dispose();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox10.Image = Image.FromFile(f.FileName);
            }f.Dispose();

        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(materialSingleLineTextField19.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField19.Text)) { label77.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(materialSingleLineTextField11.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField11.Text)) { label75.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (string.IsNullOrEmpty(materialSingleLineTextField17.Text) || string.IsNullOrWhiteSpace(materialSingleLineTextField17.Text)) { label73.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (pictureBox6.Image == null) { label78.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (pictureBox7.Image == null) { label78.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (pictureBox8.Image == null) { label78.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (pictureBox9.Image == null) { label78.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (pictureBox10.Image == null) { label78.Visible = true; validateerrorforteammatetimer.Start(); }
            else if (label71.Text == "lat" && label70.Text == "lng") { label80.Visible = true; validateerrorforteammatetimer.Start(); }
            else//code for insert project in databae
            {
                messageboxshow = false;
                //-----------------------------------------------
                projectinsert();
                //-----------------------------------------------
                restartformularaddproject();
                validateerrorforteammatetimer.Start(); insertpopupanimation.Show(bunifuCustomLabel6); bunifuCustomLabel6.Visible = true; bunifuCustomLabel6.BringToFront();
                addproject_panel.Visible = false;
                //-------------------------------------------------
            }
        }

        private void materialSingleLineTextField11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char finalyear = e.KeyChar;
            if (!Char.IsDigit(finalyear) && finalyear != 8 && finalyear != 46)
            { e.Handled = true; }
        }
        private void restartformularaddproject()
        {
            materialSingleLineTextField19.Text = string.Empty;
            materialSingleLineTextField18.Text = string.Empty;
            materialSingleLineTextField11.Text = string.Empty;
            materialSingleLineTextField10.Text = string.Empty;
            bunifuDropdown2.selectedIndex = 0;
            label71.Text = "lat";
            label70.Text = "lng";
            label71.Visible = false;
            label70.Visible = false;
            materialSingleLineTextField17.Text = string.Empty;
            project_region.Text = string.Empty;
            project_city.Text = string.Empty;
            materialCheckBox1.Checked = false;
            materialCheckBox2.Checked = false;
            materialCheckBox3.Checked = false;
            jwRichTextBox3.Text = string.Empty;
            materialRadioButton1.Checked = false;
            materialRadioButton2.Checked = true;
            materialRadioButton3.Checked = false;
            pictureBox6.Image = null; pictureBox6.Invalidate();
            pictureBox7.Image = null; pictureBox7.Invalidate(); pictureBox7.Visible = false;
            pictureBox8.Image = null; pictureBox8.Invalidate(); pictureBox8.Visible = false;
            pictureBox9.Image = null; pictureBox9.Invalidate(); pictureBox9.Visible = false;
            pictureBox10.Image = null; pictureBox10.Invalidate(); pictureBox10.Visible = false;
            materialSingleLineTextField20.Text = string.Empty;
            materialSingleLineTextField21.Text = string.Empty; materialSingleLineTextField21.Visible = false;
            materialSingleLineTextField22.Text = string.Empty; materialSingleLineTextField22.Visible = false;
            materialSingleLineTextField23.Text = string.Empty; materialSingleLineTextField23.Visible = false;
            materialSingleLineTextField24.Text = string.Empty; materialSingleLineTextField24.Visible = false;
            marker.IsVisible = false;
        }

        private void jwRichTextBox3_MouseEnter(object sender, EventArgs e)
        { 
            jwRichTextBox3.Focus();
            materialDivider23.BackColor = System.Drawing.Color.FromArgb(55, 71, 79);
        }

        private void jwRichTextBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            materialDivider23.BackColor = System.Drawing.Color.FromArgb(193, 193, 193);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox20.Image = Image.FromFile(f.FileName);
            }
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox19.Image = Image.FromFile(f.FileName);
            }
        }
        
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox18.Image = Image.FromFile(f.FileName);
            }
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox17.Image = Image.FromFile(f.FileName);
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*JPG)|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                pictureBox16.Image = Image.FromFile(f.FileName);
            }
        }
        //------------------------------------zoom project pictures---------
        public bool clickpicture = true;
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            if (clickpicture == true)
            {
                pictureBox15.Dock = DockStyle.Fill;
                pictureBox15.BringToFront();
                clickpicture = false;
            }
            else
            {
                pictureBox15.Dock = DockStyle.None;
                pictureBox15.SendToBack();
                clickpicture = true;
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (clickpicture == true)
            {
                pictureBox14.Dock = DockStyle.Fill;
                pictureBox14.BringToFront();
                clickpicture = false;
            }
            else
            {
                pictureBox14.Dock = DockStyle.None;
                pictureBox14.SendToBack();
                clickpicture = true;
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (clickpicture == true)
            {
                pictureBox13.Dock = DockStyle.Fill;
                pictureBox13.BringToFront();
                clickpicture = false;
            }
            else
            {
                pictureBox13.Dock = DockStyle.None;
                pictureBox13.SendToBack();
                clickpicture = true;
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            if (clickpicture == true)
            {
                pictureBox12.Dock = DockStyle.Fill;
                pictureBox12.BringToFront();
                clickpicture = false;
            }
            else
            {
                pictureBox12.Dock = DockStyle.None;
                pictureBox12.SendToBack();
                clickpicture = true;
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (clickpicture == true)
            {
                pictureBox11.Dock = DockStyle.Fill;
                pictureBox11.BringToFront();
                clickpicture = false;
            }
            else
            {
                pictureBox11.Dock = DockStyle.None;
                pictureBox11.SendToBack();
                clickpicture = true;
            }
        }
        private void zoomoutpictureboxes()
        {
            if(clickpicture == false)
            {
                pictureBox15.Dock = DockStyle.None;
                pictureBox15.SendToBack();
                pictureBox14.Dock = DockStyle.None;
                pictureBox14.SendToBack();
                pictureBox13.Dock = DockStyle.None;
                pictureBox13.SendToBack();
                pictureBox12.Dock = DockStyle.None;
                pictureBox12.SendToBack();
                pictureBox11.Dock = DockStyle.None;
                pictureBox11.SendToBack();
                clickpicture = true;
                interogateproject_panel.Visible = false;
            }
        }
        //-------------------------------------------------------------------
    }
}

