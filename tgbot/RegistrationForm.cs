
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TgBot
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        public Boolean isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Users WHERE Name ="+"'" + loginField.Text + "'" , db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Use different login");
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean isChatIDExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Users WHERE PassCode =" + "'" + telegramField.Text + "'", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Use different chat ID");
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean isFieldsEmpty()
        {
            if (loginField.Text == "" || passwordField.Text == "")
            {
                MessageBox.Show("Fill all forms");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (isUserExists() || isFieldsEmpty()|| isChatIDExists())
                {
                    return;
                }

                DB db = new DB();

                SqlCommand command = new SqlCommand("INSERT INTO dbo.Users (Name, Password, PassCode) VALUES (" + "'"+ loginField.Text + "'" + "," + "'" + passwordField.Text + "'" + "," + "'" + telegramField.Text + "'" + ")", db.GetConnection());

                db.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Account created");
                    this.Hide();
                    Login logForm = new Login();
                    logForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Fill all forms!");
                }

                db.closeConnection();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
