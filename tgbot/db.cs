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
using Telegram.Bot;

namespace TgBot
{
    class DB//database connection
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyBot_DataBaseOfChatUsers");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

    }
    public partial class db : Form
    {
        private static string Token { get; set; } =
    "sometoken";
        private static TelegramBotClient client;
        private static int code;
        public db()
        {
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            InitializeComponent();
            client.StopReceiving();

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (CodeField.Text == Login.code.ToString())
            {
                MessageBox.Show("Logged in");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect code");
            }
        }
        private static int RandomNum()
        {
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 999);
            return rndNum;
        }

        private void CodeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
