using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;



namespace WindowsFormsApp2
{
    

    public partial class Form3 : Form
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
        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM test", connection);
        SQLiteDataAdapter adapter1 = new SQLiteDataAdapter("SELECT * FROM test WHERE tem='Введение'", connection);
        public Form3()
        {
            InitializeComponent();
        }


        private void Form3_Activated(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            f2.Close();
        }

       

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        public void Form3_Load(object sender, EventArgs e)
        {
            
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
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
                {
                    checkedListBox1.Items.Add(dataGridView2[0, i].FormattedValue.ToString());
                    comboBox1.Items.Add(dataGridView2[0, i].FormattedValue.ToString());
                }
                
            }
            checkedListBox1.Items.Remove("");
            comboBox1.Items.Remove("");

            connection.Open();
            dataSt2.Clear();
            adapter = new SQLiteDataAdapter("SELECT * FROM test", connection);
            adapter.Fill(dataSt2);
            dataGridView2.DataSource = dataSt2.Tables[0];
            bindingSrc2 = new BindingSource();
            bindingSrc2.DataSource = dataSt2.Tables[0];
            for (int j = 0; j < checkedListBox1.Items.Count; j++)
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                  //  MessageBox.Show(checkedListBox1.Items[j].ToString() + " " + dataGridView1[1, i].FormattedValue.ToString() + " " + dataGridView1[2, i].FormattedValue.ToString() + " " + dataGridView1[0, i].FormattedValue.ToString() + " " + label3.Text);
                    if (checkedListBox1.Items[j].ToString() == dataGridView2[0, i].FormattedValue.ToString() && dataGridView2[2, i].FormattedValue.ToString() == "on" && dataGridView2[1, i].FormattedValue.ToString() ==label3.Text)
                        checkedListBox1.SetItemChecked(j, true);
                }
            }
            connection.Close();

        }

        public void updateDataBiding(SQLiteCommand cmd = null)
        {

          
        }

        public void closeConnection()
        {

        }

        public void openConnection()
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            //заполняем картинками все
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE 'Введение%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            string aaa;
            for (int i=0; i < dataGridView3.Rows.Count-1; i++)
            {
                aaa = Application.StartupPath + "\\" + "Введение"+(i+1).ToString()+".png";

                command = new SQLiteCommand("UPDATE teorr SET pick='" + aaa + "' WHERE tem='Введение"+(i+1).ToString()+"'", connection);
                command.ExecuteNonQuery();
                adapter.Update(dataSt3.Tables[0]);
            }
            //  adapter1 = new SQLiteDataAdapter("SELECT * FROM teor WHERE tem='Метод подстановки'", connection);
            //   adapter1.Fill(dataSt3);
            //       dataGridView3.DataSource = dataSt3.Tables[0];
            //
            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE 'Метод подстановки%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                aaa = Application.StartupPath + "\\" + "Метод подстановки" + (i + 1).ToString() + ".png";

                command = new SQLiteCommand("UPDATE teorr SET pick='" + aaa + "' WHERE tem='Метод подстановки" + (i + 1).ToString() + "'", connection);
                command.ExecuteNonQuery();
                adapter.Update(dataSt3.Tables[0]);
            }
            ///////////////////////////////////
            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE 'Метод почленного сложения%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                aaa = Application.StartupPath + "\\" + "Метод почленного сложения" + (i + 1).ToString() + ".png";

                command = new SQLiteCommand("UPDATE teorr SET pick='" + aaa + "' WHERE tem='Метод почленного сложения" + (i + 1).ToString() + "'", connection);
                command.ExecuteNonQuery();
                adapter.Update(dataSt3.Tables[0]);
            }
            ///////////////////////////////////
            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE 'По формулам Крамера%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                aaa = Application.StartupPath + "\\" + "По формулам Крамера" + (i + 1).ToString() + ".png";

                command = new SQLiteCommand("UPDATE teorr SET pick='" + aaa + "' WHERE tem='По формулам Крамера" + (i + 1).ToString() + "'", connection);
                command.ExecuteNonQuery();
                adapter.Update(dataSt3.Tables[0]);
            }
            ///////////////////////////////////
            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE 'Методом Гауса%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                aaa = Application.StartupPath + "\\" + "Методом Гауса" + (i + 1).ToString() + ".png";

                command = new SQLiteCommand("UPDATE teorr SET pick='" + aaa + "' WHERE tem='Методом Гауса" + (i + 1).ToString() + "'", connection);
                command.ExecuteNonQuery();
                adapter.Update(dataSt3.Tables[0]);
            }
            ///////////////////////////////////
            ///начинаем просматривать картинки

            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE '"+comboBox1.Text+"%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            number = 1;
            string pic = Application.StartupPath + "\\" + comboBox1.Text+ number.ToString()+ ".png";
            label6.Text = "Страница №1";
            Bitmap image1 = new Bitmap(pic);
            pictureBox1.Image = image1;
            connection.Close();
            if (comboBox1.Text !="" )
            {
                button3.Enabled = true;
            }
            else
                button3.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE '" + comboBox1.Text + "%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            if (number < dataGridView3.Rows.Count - 1)
            {
                if (number == dataGridView3.Rows.Count - 2)
                {
                    button3.Enabled = true;
                }
                else
                    button3.Enabled = false;
                number++;
                string pic = Application.StartupPath + "\\" + comboBox1.Text + number.ToString() + ".png";
                label6.Text = "Страница №" + number.ToString();
                Bitmap image1 = new Bitmap(pic);
                pictureBox1.Image = image1;

            }
            connection.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (number != dataGridView3.Rows.Count - 1)
            {
                button3.Enabled = false;
            }
           
            connection.Open();
            dataSt3.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE '" + comboBox1.Text + "%'", connection);
            adapter1.Fill(dataSt3);
            dataGridView3.DataSource = dataSt3.Tables[0];
            if (number>1)
            number--;
            string pic = Application.StartupPath + "\\" + comboBox1.Text + number.ToString() + ".png";
            label6.Text = "Страница №" + number.ToString();
            Bitmap image1 = new Bitmap(pic);
            pictureBox1.Image = image1;
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form5 f5 = new Form5();
            f5.label1.Text = label3.Text;
            f5.label3.Text = comboBox1.Text;
            f5.Show();
            this.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}