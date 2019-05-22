using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StokTakip
{
    public partial class raporlama : Form
    {
        public raporlama()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");
        private void raporlama_FormClosing(object sender, FormClosingEventArgs e)
        {
            raporlama rp = new raporlama();
            rp.Close();
        }

        string suankitarih, geritarih, gerigun = "3";
        private void button1_Click(object sender, EventArgs e)
        {
            suankitarih = DateTime.Now.ToShortDateString().ToString();
            int gun = -1 * Int32.Parse(gerigun);
            string labelTarih = suankitarih;
            var tarih = DateTime.Parse(labelTarih).AddDays(gun);
            geritarih = tarih.ToString();

            if (comboBox1.SelectedIndex == 0)
            {
                if (radioButton1.Checked)
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI where barkod_no='" + textBox1.Text + "'", baglan);
                    adp.Fill(tb);
                    baglan.Close();
                    raporsatis rpr = new raporsatis();
                    rpr.SetDataSource(tb);
                    crystalReportViewer1.ReportSource = rpr;
                }
                else if (radioButton2.Checked)
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI where musteri_adi='" + textBox2.Text + "'", baglan);
                    adp.Fill(tb);
                    baglan.Close();
                    raporsatis rpr = new raporsatis();
                    rpr.SetDataSource(tb);
                    crystalReportViewer1.ReportSource = rpr;
                }
                else if (radioButton3.Checked)
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI where tarih between '" + dateTimePicker1.Value.ToString() + "' and '" + dateTimePicker2.Value.ToString() + "'", baglan);
                    adp.Fill(tb);
                    baglan.Close();
                    raporsatis rpr = new raporsatis();
                    rpr.SetDataSource(tb);
                    crystalReportViewer1.ReportSource = rpr;
                }
                else
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from SATIS_EKRANI", baglan);
                    adp.Fill(tb);
                    baglan.Close();
                    raporsatis rpr = new raporsatis();
                    rpr.SetDataSource(tb);
                    crystalReportViewer1.ReportSource = rpr;
                }


            }
            if (comboBox1.SelectedIndex == 1)
            {
                baglan.Open();
                DataTable tb = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from CARI_BILGI", baglan);
                adp.Fill(tb);
                baglan.Close();
                raporcari rpr = new raporcari();
                rpr.SetDataSource(tb);
                crystalReportViewer1.ReportSource = rpr;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                if (checkBox1.Checked)
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from HAREKETLER", baglan);
                    adp.Fill(tb);
                    baglan.Close();
                    raporhareket rpr = new raporhareket();
                    rpr.SetDataSource(tb);
                    crystalReportViewer1.ReportSource = rpr;
                }
                else
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from HAREKETLER where TARIH >='" + geritarih + "'", baglan);
                    adp.Fill(tb);
                    baglan.Close();
                    raporhareket rpr = new raporhareket();
                    rpr.SetDataSource(tb);
                    crystalReportViewer1.ReportSource = rpr;
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0)
            {
                panel1.Visible = true;

            }
            else
            {
                panel1.Visible = false;
            }

            if (comboBox1.SelectedIndex == 2)
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            textBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            textBox2.Enabled = false;
            textBox1.Enabled = false;
        }

        private void raporlama_Load(object sender, EventArgs e)
        {
            raporyetkikontrol();
        }
        string yetkikontrol;
        public void raporyetkikontrol()
        {
            yetkikontrol = admingiris.kullanci;
            DataTable tb = new DataTable();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='" + yetkikontrol + "'", baglan);
            adp.Fill(tb);
            baglan.Close();
            if (tb.Rows[0][11].ToString() == "1")
            {
            }
            else if (tb.Rows[0][11].ToString() == "0")
            {
                MessageBox.Show("Raporlama Yetkiniz Bulunmamaktadır.");
                comboBox1.Enabled = false;
                button1.Enabled = false;
                crystalReportViewer1.Enabled = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
