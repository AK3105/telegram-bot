using Telegram.Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TgBot
{
    public partial class Login : Form
    {
        public static int tg_id;
        public Login()
        {
            InitializeComponent();
        }

        public Boolean isFieldsEmpty()
        {
            if (loginField.Text == "" || passwordField.Text == "")
            {
                MessageBox.Show("fill all fields");
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int RandomNum()
        {
            Random rnd = new Random();
            int rndNum = rnd.Next(1000, 9999);
            return rndNum;
        }
        public static int code = RandomNum();

        private void button1_Click(object sender, EventArgs e)
        {
            var token = "2079170564:AAGkfdXLDF3CVg-tMjH-oMIXqxkC7F_nJD8";
            var telegramUrl = "https://api.telegram.org/bot" + token;
            //var chat_id = 490374577;

            if (isFieldsEmpty())
            {
                return;
            }

            String loginUser = loginField.Text;
            String passUser = passwordField.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Users WHERE Name = "+"'"+loginUser + "'" + "AND Password = " + "'"+passUser + "'" , db.GetConnection());
            

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                tg_id = (int)table.Rows[0].ItemArray[3];

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(telegramUrl + "/sendMessage?chat_id=" + tg_id.ToString() + "&text=" + code);
                HttpWebResponse responese = (HttpWebResponse)request.GetResponse();
                responese.Close();

                
                this.Hide();
                db verCodeForm = new db();
                verCodeForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Unknown user");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
