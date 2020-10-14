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
    public partial class Form2 : Form
    {
        private static string dbCommand = "";
        private static BindingSource bindingSrc;

        private static string dbPath = Application.StartupPath + "\\" + "bd.db";
        private static string conString = "Data Source=" + dbPath;

        private static SQLiteConnection connection = new SQLiteConnection(conString);
        private static SQLiteCommand command = new SQLiteCommand("", connection);

        private static string sql;
        DataSet dataSt = new DataSet();
        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM log", connection);
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f2 = new Form1();
            f2.Show();
            textBox1.Text = "";
            textBox2.Text = "";
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }


            SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(adapter);
            //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dataSt);
            dataGridView1.DataSource = dataSt.Tables[0];
            bindingSrc = new BindingSource();
            bindingSrc.DataSource = dataSt.Tables[0];
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            Form1 f2 = new Form1();
            f2.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            f2.Show();
            this.Hide();       
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (textBox1.Text == "admin" && textBox2.Text == "yravn")
                {
                    Form4 f2 = new Form4();
                    f2.Show();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    this.Hide();
                }
                else
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                        if (dataGridView1[0, i].FormattedValue.ToString() == textBox1.Text && dataGridView1[1, i].FormattedValue.ToString() == textBox2.Text)
                        {
                            a = 1;
                            MessageBox.Show("Вход успешен");
                            Form7 f7 = new Form7();
                            Form3 f2 = new Form3();
                            if (dataGridView1[5, i].FormattedValue.ToString() != "Пользователь")
                                f7.Show();
                            else
                                f2.Show();
                            f2.label3.Text = textBox1.Text;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            this.Hide();
                            break;
                        }

                    if (a == 0)
                    {

                        MessageBox.Show("Вы ввели неверные данные");

                    }
                }
            }
            else
                MessageBox.Show("Вы не ввели данные");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            f2.Show();
            textBox1.Text = "";
            textBox2.Text = "";
            this.Hide();
        }
    }
}
