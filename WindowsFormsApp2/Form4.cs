using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        int number;
        private static BindingSource bindingSrc;
        private static BindingSource bindingSrc2;

        private static string dbPath = Application.StartupPath + "\\" + "bd.db";
        private static string conString = "Data Source=" + dbPath;

        private static SQLiteConnection connection = new SQLiteConnection(conString);
        private static SQLiteCommand command = new SQLiteCommand("", connection);

        private static string sql;
        DataSet dataSt = new DataSet();
        DataSet dataSt2 = new DataSet();
        DataSet dataSt3 = new DataSet();
        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM log", connection);
        SQLiteDataAdapter adapter1 = new SQLiteDataAdapter("SELECT * FROM test WHERE tem='Введение'", connection);
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            adapter.Fill(dataSt);
            dataGridView1.DataSource = dataSt.Tables[0];
            bindingSrc = new BindingSource();
            bindingSrc.DataSource = dataSt.Tables[0];


            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            label3.Text = Convert.ToString(dataGridView1.Rows.Count - 1);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            f.label3.Text = "Шарповничек";
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
