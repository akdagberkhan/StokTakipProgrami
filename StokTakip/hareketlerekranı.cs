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
    public partial class hareketlerekranı : Form
    {
        public hareketlerekranı()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");


        //Hareketler Tablosundaki Bütün Bilgileri Alıp Listbox a yazdırma
        void listele()
        {
            try
            {
                baglan.Open();
                DataTable tb = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from HAREKETLER order by TARIH desc", baglan);
                adp.Fill(tb);
                baglan.Close();
                for (int i = 0; i < tb.Rows.Count; i++)
                {

                    listBox1.Items.Add(tb.Rows[i][0].ToString());
                    listBox1.Items.Add("-----------------------------------------------------------------------------");
                    listBox2.Items.Add("Tarih :" + tb.Rows[i][1].ToString());
                    listBox2.Items.Add("-----------------------------------------------------------------------------");
                    listBox3.Items.Add(tb.Rows[i][2].ToString());
                    listBox3.Items.Add("-----------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
       
        //Yukardaki Metod u Çağırma işlemi
        private void hareketlerekranı_Load(object sender, EventArgs e)
        {
            listele();
        }

        //Listboxun verilerini temizleyip yeniden bütün verileri çağırma işlemi
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void hareketlerekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            hareketlerekranı hrkt = new hareketlerekranı();
            hrkt.Close();
            
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }
        int secilen;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            secilen = listBox1.SelectedIndex;
            listBox2.SelectedIndex = secilen;
            listBox3.SelectedIndex = secilen;
            if (listBox1.SelectedItem=="-----------------------------------------------------------------------------")
            {
                if (secilen==listBox1.Items.Count-1)
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                }
                else
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                }
                
            }
            

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            secilen = listBox2.SelectedIndex;
            listBox1.SelectedIndex = secilen;
            listBox3.SelectedIndex = secilen;
            if (listBox2.SelectedItem == "-----------------------------------------------------------------------------")
            {
                if (secilen == listBox2.Items.Count - 1)
                {
                    listBox2.SelectedIndex = listBox2.SelectedIndex - 1;
                }
                else
                {
                    listBox2.SelectedIndex = listBox2.SelectedIndex + 1;
                }

            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            secilen = listBox3.SelectedIndex;
            listBox2.SelectedIndex = secilen;
            listBox1.SelectedIndex = secilen;
            if (listBox3.SelectedItem == "-----------------------------------------------------------------------------")
            {
                if (secilen == listBox3.Items.Count - 1)
                {
                    listBox3.SelectedIndex = listBox3.SelectedIndex - 1;
                }
                else
                {
                    listBox3.SelectedIndex = listBox3.SelectedIndex + 1;
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listele();
            }
            if (radioButton2.Checked)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listele3();
            }
            
        }

        string suankitarih, geritarih, gerigun = "3";
        public void listele3()
        {
            try
            {
                suankitarih = DateTime.Now.ToShortDateString().ToString();
                int gun = -1 * Int32.Parse(gerigun);
                string labelTarih = suankitarih;
                var tarih = DateTime.Parse(labelTarih).AddDays(gun);
                geritarih = tarih.ToString();

                baglan.Open();
                DataTable tb = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from HAREKETLER where TARIH >='" + geritarih + "'", baglan);
                adp.Fill(tb);
                baglan.Close();

                for (int i = 0; i < tb.Rows.Count; i++)
                {

                    listBox1.Items.Add(tb.Rows[i][0].ToString());
                    listBox1.Items.Add("-----------------------------------------------------------------------------");
                    listBox2.Items.Add("Tarih :" + tb.Rows[i][1].ToString());
                    listBox2.Items.Add("-----------------------------------------------------------------------------");
                    listBox3.Items.Add(tb.Rows[i][2].ToString());
                    listBox3.Items.Add("-----------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
