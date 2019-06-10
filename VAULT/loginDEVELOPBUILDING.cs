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
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace VAULT
{
    public partial class loginDEVELOPBUILDING : Form
    {
        public static string Email = "";
        public string emaillogged;
        public bool checkconnection = NetworkInterface.GetIsNetworkAvailable();
        public bool counterforgetpassword = false;
        public loginDEVELOPBUILDING()
        {
                InitializeComponent();
            if(checkconnection==true)
            {
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox2.Visible = true;
            }
        }

        private void closebutton_Click(object sender, EventArgs e)//close application
        {
            Application.Exit();
        }

        private void minimizebutton_Click(object sender, EventArgs e)// minimize this form
        {
            WindowState = FormWindowState.Minimized;
        }

        private void dropdownlanguage_onItemSelected(object sender, EventArgs e)//change language
        {
            if(dropdownlanguage.selectedValue=="EN")//english language
            {
                loginbutton.ButtonText = "LOGIN";emailerror.Text = "Entered data is incorecte.";passworderror.Text = "Entered data is incorecte.";
                loginerror.Text = "Please try again.";DEVELOPBYLABEL.Text = "DEVELOPED BY";welcomelabel.Text = "Account login";passwordtextbox.Hint = "Password *";
                forgetpass.Text = "I forgot my password."; forgetpass.Location = new Point(290, 414);
                connectionerror.Text = "You are not connected to an internet network.";
            }
            else//romanian language
            {
                loginbutton.ButtonText = "AUTENTIFICARE";emailerror.Text = "Datele introduse sunt incorecte.";passworderror.Text = "Datele introduse sunt incorecte.";
                loginerror.Text = "Te rugăm să încerci din nou.";DEVELOPBYLABEL.Text = "DEZVOLTAT DE";welcomelabel.Text = "Autentificare cont"; passwordtextbox.Hint = "Parolă *";
                forgetpass.Text = "Mi-am uitat parola.";forgetpass.Location = new Point(308, 414);
                connectionerror.Text = "Nu sunteti conectat la o rețea de internet.";
            }
        }
        public string manageroragent;
        private void loginlogin()//function for validate login
        {

            if (checkconnection == true)
            {
                if (counterforgetpassword == false)
                {
                    Email = emailtextbox.Text;

                    string pass = passwordtextbox.Text.Trim();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["devbuild"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("select * from team where email=@LEMAIL and password=@LPAROLA", con);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd.Parameters.AddWithValue("@LEMAIL", emailtextbox.Text);
                    cmd.Parameters.AddWithValue("@LPAROLA", passwordtextbox.Text);

                    SqlDataAdapter dt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);

                    int count = ds.Tables[0].Rows.Count;
                    if (count == 1)
                    {

                        if (pass == ds.Tables[0].Rows[0]["password"].ToString())
                        {
                            var appform = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is principalformapp);
                            if (appform != null)
                            {
                                appform.Show();
                                this.Hide();
                                emailtextbox.Text = String.Empty;
                                passwordtextbox.Text = String.Empty;
                            }
                            else
                            {
                                principalformapp appforms = new principalformapp();
                                this.Hide();
                                appforms.Show();
                                emailtextbox.Text = String.Empty;
                                passwordtextbox.Text = String.Empty;
                            }
                        }
                        else
                        {
                            developbuildingtext.Focus();
                            timer1.Start();//start timer
                            emailerror.Visible = true; passworderror.Visible = true; loginerror.Visible = true;//if email or pass is wrong then show error message
                            emailtextbox.Text = String.Empty;
                            passwordtextbox.Text = String.Empty;
                        }
                    }
                    else
                    {
                        developbuildingtext.Focus();
                        timer1.Start();//start timer
                        emailerror.Visible = true; passworderror.Visible = true; loginerror.Visible = true;//if email or pass is wrong then show error message
                        emailtextbox.Text = String.Empty;
                        passwordtextbox.Text = String.Empty;
                    }
                }

                else
                {
                    //======================================================================================================================
                    //checked if email does match with the phone number


                    string mobile, password, email1;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["devbuild"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("select * from team where email=@LEMAIL and phonenumber=@mobil", con);
                    cmd.Parameters.AddWithValue("@LEMAIL", emailtextbox.Text);
                    cmd.Parameters.AddWithValue("@mobil", passwordtextbox.Text);

                    SqlDataAdapter dt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);

                    int count = ds.Tables[0].Rows.Count;
                    if (count == 1)
                    {
                        email1 = ds.Tables[0].Rows[0]["email"].ToString();
                        mobile = ds.Tables[0].Rows[0]["phonenumber"].ToString();
                        password = ds.Tables[0].Rows[0]["password"].ToString();
                        sendpassword(mobile, password, email1);
                        passwordtextbox.Visible = true;
                    }
                    else
                    {
                        errornumberphone.Visible = true;
                        errorpassforget.Visible = true;
                        timer1.Start();
                        return;
                    }
                    //======================================================================================================================
                    counterforgetpassword = false; telephone.Visible = false; passwordicon.Visible = true; exitlabel.Visible = false;
                    emailtextbox.Text = String.Empty;
                    passwordtextbox.Text = String.Empty;
                    passwordtextbox.UseSystemPasswordChar = true;
                    if (dropdownlanguage.selectedValue == "EN")//english language
                    {
                        passwordtextbox.Hint = "Password *"; loginbutton.Text = "LOGIN"; welcomelabel.Text = "Account login";
                    }
                    else//romanian language
                    {
                        passwordtextbox.Hint = "Parolă *"; loginbutton.Text = "AUTENTIFICARE"; welcomelabel.Text = "Autentificare cont";
                    }
                }
            }
            else
            {
                connectionerror.Visible = true;
                timer1.Start();
            }
        }
        private void loginbutton_Click(object sender, EventArgs e)//validate login
        {
            loginlogin();
        }
        public int countererrorseconds = 0;//count for seconds
        private void timer1_Tick(object sender, EventArgs e)//this hide error message after five seconds
        {
            countererrorseconds = countererrorseconds + 100;
            if (countererrorseconds == 4000)
            {
                errornumberphone.Visible = false;
                errorpassforget.Visible = false;
                emailerror.Visible = false;
                passworderror.Visible = false;
                loginerror.Visible = false;
                connectionerror.Visible = false;
                timer1.Stop();
                countererrorseconds = 0;

            }
            else
                return;
        }

        private void passwordtextbox_KeyDown(object sender, KeyEventArgs e)//if users press enter on password call validate login
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginlogin();
            }
        }

        private void forgetpass_MouseEnter(object sender, EventArgs e)//change in red this label
        {
            forgetpass.ForeColor = System.Drawing.Color.Red;
        }

        private void forgetpass_MouseLeave(object sender, EventArgs e)//change in white this label
        {
            forgetpass.ForeColor = System.Drawing.Color.White;
        }

        private void forgetpass_Click(object sender, EventArgs e)// show passfor form
        {
            counterforgetpassword = true; telephone.Visible = true;passwordicon.Visible = false;exitlabel.Visible = true;
            emailtextbox.Text = String.Empty;
            passwordtextbox.Text = String.Empty;
            passwordtextbox.UseSystemPasswordChar = false;
            watchpassbutton.Visible = false;
            if (dropdownlanguage.selectedValue == "EN")//english language
            {
                passwordtextbox.Hint = "Phone number *";loginbutton.Text = "SEND PASSWORD";welcomelabel.Text = "I forget my password";exitlabel.Text = "Discard.";
                errornumberphone.Text = "The phone number does not match the registered number."; errorpassforget.Text = "Please try again.";
            }
            else//romanian language
            {
                passwordtextbox.Hint = "Număr de telefon *";loginbutton.Text = "TRIMITE PAROLA";welcomelabel.Text = "Mi-am uitat parola";exitlabel.Text = "Renunță.";
                errornumberphone.Text = "Numărul de telefon nu corespunde cu cel înregistrat.";errorpassforget.Text = "Te rugăm să încerci din nou.";
            }
        }

        private void exitlabel_Click(object sender, EventArgs e)
        {
            counterforgetpassword = false; telephone.Visible = false; passwordicon.Visible = true; exitlabel.Visible = false;
            emailtextbox.Text = String.Empty;
            passwordtextbox.Text = String.Empty;
            passwordtextbox.UseSystemPasswordChar = true;
            watchpassbutton.Visible = true;
            if (dropdownlanguage.selectedValue == "EN")//english language
            {
                passwordtextbox.Hint = "Password *";loginbutton.Text = "LOGIN";welcomelabel.Text = "Account login";
            }
            else//romanian language
            {
                passwordtextbox.Hint = "Parolă *";loginbutton.Text = "AUTENTIFICARE";welcomelabel.Text = "Autentificare cont";
            }

           
        }

        private void sendpassword(string mobil1, string password1,string email2)//send to the number phone sms with password
        {
            // Your Account SID from twilio.com/console
            var accountSid = "ACca1285173a69f10c0993f6091e9332b9";
            // Your Auth Token from twilio.com/console
            var authToken = "944f5958e502120c33e03f7cf1cdb0b1";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber(mobil1),
                from: new PhoneNumber("(281) 612-8368"),
                body: "Hello " + email2 + " !" + "\n" + "Your password is: " + password1 + "\n" + "Team Lord Technologies");

        }

        private void exitlabel_MouseEnter(object sender, EventArgs e)//change in red this label
        {
            exitlabel.ForeColor = System.Drawing.Color.Red;
        }

        private void exitlabel_MouseLeave(object sender, EventArgs e)//change in white this label
        {
            exitlabel.ForeColor = System.Drawing.Color.White;
        }

        private void watchpassbutton_MouseEnter(object sender, EventArgs e)
        {
            passwordtextbox.UseSystemPasswordChar = false;
        }//to watchpassword character

        private void watchpassbutton_MouseLeave(object sender, EventArgs e)
        {
            passwordtextbox.UseSystemPasswordChar = true;
        }//to watchpassword systempass

        private void logo_company_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.ro/");//link to company website
        }

        private void DEVELOPBYLABEL_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.ro/");//link to company website
        }
    }
}
