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
    public partial class Form5 : Form
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
        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM qest", connection);
        SQLiteDataAdapter adapter1 = new SQLiteDataAdapter("SELECT * FROM qest WHERE tem='Введение'", connection);

        int qestionn = 0;
        int y = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
            qestionn = 1;
            adapter.Fill(dataSt);
            dataGridView1.DataSource = dataSt.Tables[0];
            bindingSrc = new BindingSource();
            bindingSrc.DataSource = dataSt.Tables[0];
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {

            Form3 f3 = new Form3();
            f3.label3.Text = label1.Text;
            f3.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            y = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            y = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            y = 3;
        }

        private void button1_Click(object sender, EventArgs e)
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
                        else
                        {
                            connection.Open();
                            dataSt3.Clear();
                            adapter = new SQLiteDataAdapter("SELECT * FROM test", connection);
                            adapter.Fill(dataSt3);
                            dataGridView2.DataSource = dataSt3.Tables[0];
                            bindingSrc2 = new BindingSource();
                            bindingSrc2.DataSource = dataSt3.Tables[0];
                            for (int j = 0; j < dataGridView2.Rows.Count - 1; j++)
                            {

                                if (dataGridView2[0, j].FormattedValue.ToString() == label1.Text && dataGridView2[1, j].FormattedValue.ToString() == label3.Text)
                                {
                                    command = new SQLiteCommand("UPDATE test SET status='on' WHERE teori='" + label3.Text + "'", connection);
                                    command.ExecuteNonQuery();
                                    adapter.Update(dataSt3.Tables[0]);
                                }
                            }

                            //вариант увеличения рейтинга за прохождение теста

                            dataSt3.Clear();
                            adapter = new SQLiteDataAdapter("SELECT * FROM log", connection);
                            adapter.Fill(dataSt3);
                            dataGridView2.DataSource = dataSt3.Tables[0];
                            bindingSrc2 = new BindingSource();
                            bindingSrc2.DataSource = dataSt3.Tables[0];
                            for (int j = 0; j < dataGridView2.Rows.Count - 1; j++)
                            {

                                if (dataGridView2[3, j].FormattedValue.ToString() == label1.Text)
                                {
                                    command = new SQLiteCommand("UPDATE log SET re='" + Convert.ToString(int.Parse(dataGridView2[5, j].FormattedValue.ToString()) + 1) + "' WHERE login='" + label1.Text + "'", connection);
                                    command.ExecuteNonQuery();
                                    adapter.Update(dataSt3.Tables[0]);
                                }
                            }


                            MessageBox.Show("Вы успешно прошли тест");
                            Form3 ff = new Form3();
                            ff.label3.Text = label1.Text;
                            connection.Close();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы ошиблись,грустно(");
                        Form3 ff = new Form3();
                        ff.label3.Text = label1.Text;
                        this.Close();
                    }
            }

        }

        private void Form5_Activated(object sender, EventArgs e)
        { string[] mass = new string[7];
            int schetch = 0;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {

                if (dataGridView1[0, i].FormattedValue.ToString() == qestionn.ToString() && dataGridView1[4, i].FormattedValue.ToString() == label3.Text)
                {
                    //for (int j = 0; j < 11; j++)
                    //{
                    //    if (dataGridView1[j, i].FormattedValue.ToString() == "")
                    //    {
                    //        schetch = j-1;
                    //        break;
                    //    }
                    //}
                    //for(int j=0;j<schetch;j++)
                    //{
                    //    if(j!=0 ||j!=4 || j!=5 ||j!=6)
                    //    {
                    //        mass
                    //    }
                    //}
                    label2.Text = dataGridView1[6, i].FormattedValue.ToString();
                    radioButton1.Text = dataGridView1[1, i].FormattedValue.ToString();
                    radioButton2.Text = dataGridView1[2, i].FormattedValue.ToString();
                    radioButton3.Text = dataGridView1[3, i].FormattedValue.ToString();
                    if (dataGridView1[7, i].FormattedValue.ToString() != "")
                    {
                        radioButton4.Visible = true;
                        radioButton4.Text = dataGridView1[7, i].FormattedValue.ToString();
                    }
                    else
                        radioButton4.Visible = false;
                    if (dataGridView1[8, i].FormattedValue.ToString() != "")
                    {
                        radioButton5.Visible = true;
                        radioButton5.Text = dataGridView1[8, i].FormattedValue.ToString();
                    }
                    else
                        radioButton5.Visible = false;
                    if (dataGridView1[9, i].FormattedValue.ToString() != "")
                    {
                        radioButton6.Visible = true;
                        radioButton6.Text = dataGridView1[9, i].FormattedValue.ToString();
                    }
                    else
                        radioButton6.Visible = false;
                    if (dataGridView1[10, i].FormattedValue.ToString() != "")
                    {
                        radioButton7.Visible = true;
                        radioButton7.Text = dataGridView1[10, i].FormattedValue.ToString();
                    }
                    else
                        radioButton7.Visible = false;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            y = 4;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            y = 5;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            y = 6;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            y = 7;
        }
    }
}
