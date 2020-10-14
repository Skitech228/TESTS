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
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private static string dbCommand = "";
        private static BindingSource bindingSrc;
        private static BindingSource bindingSrc2;

        private static string dbPath = Application.StartupPath + "\\" + "bd.db";
        private static string conString = "Data Source=" + dbPath;

        private static SQLiteConnection connection = new SQLiteConnection(conString);
        private static SQLiteCommand command = new SQLiteCommand("", connection);

        private static string sql;
        DataSet dataSt = new DataSet();
        DataSet dataSt2 = new DataSet();
        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM log", connection);

        public Form1()
        {
            InitializeComponent();

        }



        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            int a = 0;

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                        if (dataGridView1[0, i].FormattedValue.ToString() == textBox1.Text)
                        {
                            a = 1;
                            MessageBox.Show("Логин существует");
                            break;
                        }

                    if (a == 0)
                    {
                        MessageBox.Show("Регистрация успешна");
                        connection.Open();
                        command = new SQLiteCommand("INSERT INTO log (login,pasvord,re) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','0');", connection);
                        command.ExecuteNonQuery();
                        adapter.Update(dataSt.Tables[0]);

                        for (int j = 0; j < checkedListBox1.Items.Count; j++)
                        {

                            command = new SQLiteCommand("INSERT INTO test (log,teori,status) VALUES ('" + textBox1.Text + "','" + checkedListBox1.Items[j].ToString() + "','off');", connection);
                            command.ExecuteNonQuery();
                            adapter.Update(dataSt.Tables[0]);

                        }

                        Form2 f2 = new Form2();
                        f2.Show();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Hide();
                        label4.Text = ("");

                    }
                }
                else
                    MessageBox.Show("Вы не ввели данные");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text!="" && comboBox1.Text!="")
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                    if (dataGridView1[0, i].FormattedValue.ToString() == textBox1.Text)
                    {
                        a = 1;
                        MessageBox.Show("Логин существует");
                        break;
                    }
                if(comboBox1.Text== "Преподаватель ")
                {
                    if (textBox4.Text != "7564")
                    {
                        MessageBox.Show("Неверный код");
                        a++;
                    }
                    
                }
                if (a == 0)
                {

                    MessageBox.Show("Регистрация успешна");
                    connection.Open();
                    command = new SQLiteCommand("INSERT INTO log (login,pasvord,re,FIO,ROLE) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','0','"+textBox3.Text+ "','"+comboBox1.Text+"');", connection);
                    command.ExecuteNonQuery();
                    adapter.Update(dataSt.Tables[0]);

                    for (int j = 0; j < checkedListBox1.Items.Count; j++)
                    {

                        command = new SQLiteCommand("INSERT INTO test (log,teori,status) VALUES ('" + textBox1.Text + "','" + checkedListBox1.Items[j].ToString() + "','off');", connection);
                        command.ExecuteNonQuery();
                        adapter.Update(dataSt.Tables[0]);

                    }


                    Form2 f2 = new Form2();
                    f2.Show();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    this.Hide();
                    label4.Text = ("");

                }
            }
            else
                MessageBox.Show("Вы не ввели данные");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int a = 0;

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                        if (dataGridView1[0, i].FormattedValue.ToString() == textBox1.Text)
                        {
                            a = 1;
                            MessageBox.Show("Логин существует");
                            break;
                        }

                    if (a == 0)
                    {
                        MessageBox.Show("Регистрация успешна");
                        connection.Open();
                        command = new SQLiteCommand("INSERT INTO log (login,pasvord,re) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','0');", connection);
                        command.ExecuteNonQuery();
                        adapter.Update(dataSt.Tables[0]);

                        for (int j = 0; j < checkedListBox1.Items.Count; j++)
                        {

                            command = new SQLiteCommand("INSERT INTO test (log,teori,status) VALUES ('" + textBox1.Text + "','" + checkedListBox1.Items[j].ToString() + "','off');", connection);
                            command.ExecuteNonQuery();
                            adapter.Update(dataSt.Tables[0]);

                        }


                        Form2 f2 = new Form2();
                        f2.Show();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Hide();
                        label4.Text = ("");

                    }
                }
                else
                    MessageBox.Show("Вы не ввели данные");
            }



        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void Form1_Load(object sender, EventArgs e)
        {

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }


            SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(adapter);
            adapter.Fill(dataSt);
            dataGridView1.DataSource = dataSt.Tables[0];
            bindingSrc = new BindingSource();
            bindingSrc.DataSource = dataSt.Tables[0];

            adapter = new SQLiteDataAdapter("SELECT * FROM teor", connection);
            adapter.Fill(dataSt2);
            dataGridView2.DataSource = dataSt2.Tables[0];
            bindingSrc2 = new BindingSource();
            bindingSrc2.DataSource = dataSt2.Tables[0];

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                bool prov = false;
                for (int j = 0; j < checkedListBox1.Items.Count; j++)
                {

                    if (checkedListBox1.Items[j].ToString() == dataGridView2[0, i].FormattedValue.ToString())
                        prov = true;
                }
                if (prov == false)
                    checkedListBox1.Items.Add(dataGridView2[0, i].FormattedValue.ToString());

            }
            checkedListBox1.Items.Remove("");
            //заполняем комбобокс

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
                if (dataGridView1[0, i].FormattedValue.ToString() == textBox1.Text)
                {
                    MessageBox.Show("Логин существует");

                }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            textBox1.Text = "";
            textBox2.Text = "";
            this.Hide();
            label4.Text = ("");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sourcepath = @"c:\FirstFile.jpg";
            string destpath = @"c:\DestFile.jpg";
            FileInfo FFI = new FileInfo(sourcepath);
            FileInfo SFI = new FileInfo(destpath);

                FFI.CopyTo(destpath);
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text== "Преподаватель ")
            {
                label7.Visible = true;
                textBox4.Visible = true;
            }
            else
            {
                label7.Visible = false;
                textBox4.Visible = false;
            }
        }
    }
}
