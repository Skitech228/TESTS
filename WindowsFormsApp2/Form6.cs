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
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form6 : Form
    {
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
        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM teor", connection);
        SQLiteDataAdapter adapter1 = new SQLiteDataAdapter("SELECT * FROM qest WHERE tem='Введение'", connection);
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            adapter.Fill(dataSt);
            dataGridView1.DataSource = dataSt.Tables[0];
            bindingSrc = new BindingSource();
            bindingSrc.DataSource = dataSt.Tables[0];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                comboBox1.Items.Add(dataGridView1[0, i].FormattedValue.ToString());
            }
            comboBox1.Items.Remove("");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            button2.Visible = false;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox1.Visible = true;
            button2.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text!="" && comboBox3.Text != "" && textBox1.Text!="")
            {
                if(comboBox2.Text == "3" && textBox3.Text!="" && textBox4.Text != "" && textBox5.Text != "")
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {

                        if (dataGridView1[0, i].FormattedValue.ToString() == qestionn.ToString() && dataGridView1[4, i].FormattedValue.ToString() == label3.Text)
                            if (y.ToString() == dataGridView1[5, i].FormattedValue.ToString())
                            {
                                if (qestionn != 3)
                                {

                                    qestionn++;
                                    MessageBox.Show("Вы верно ответили");
                                    break;
                                }
                            }
                    }
                    connection.Open();
                    command = new SQLiteCommand("INSERT INTO qest (RB1,RB2,RB3,tema,true_ansver,qestion) VALUES ('" + textBox3.Text + "','" + textBox4.Text + "','0','" + textBox5.Text + "','" + comboBox1.Text + "','" + comboBox3.Text + "','" + textBox1.Text + "');", connection);
                    command.ExecuteNonQuery();
                    adapter.Update(dataSt.Tables[0]);
                }
            }
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            dataSt.Clear();
            adapter1 = new SQLiteDataAdapter("SELECT * FROM teorr WHERE tem LIKE '" + comboBox1.Text + "%'", connection);
            adapter1.Fill(dataSt);
            dataGridView1.DataSource = dataSt.Tables[0];
            string aaa;

            aaa = Application.StartupPath + "\\" + comboBox1.Text + (dataGridView1.Rows.Count-1).ToString() + ".png";
            if (aaa != Application.StartupPath + "\\" + comboBox1.Text + "1.png")
            {

                FileInfo d = new FileInfo(aaa);
                FileStream fs = d.Create();
                fs.Close();
                d.Delete();
                command = new SQLiteCommand("Delete from teorr where tem = '" + comboBox1.Text + (dataGridView1.Rows.Count - 1).ToString() + "'", connection);
                command.ExecuteNonQuery();
                adapter.Update(dataSt.Tables[0]);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                MessageBox.Show("Удалено");
            }
            else
                MessageBox.Show("За шо,это последняя страница");
            button2.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            comboBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = false;
            comboBox1.Text = "";
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            button2.Visible = true;
            button1.Visible = false;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("'"+comboBox2.Text+"'");
            if (comboBox2.Text == "3")
            {
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
            }
            if (comboBox2.Text == "4")
            {
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;

            }
            if (comboBox2.Text == "5")
            {
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                textBox7.Visible = true;

            }
            if (comboBox2.Text == "6")
            {
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                textBox7.Visible = true;
                textBox8.Visible = true;

            }
            if (comboBox2.Text == "7")
            {
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
