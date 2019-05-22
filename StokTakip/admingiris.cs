using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;


namespace StokTakip
{
    public partial class admingiris : Form
    {
        public admingiris()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");
        public void hareketler()
        {
            baglan.Open();
            OleDbCommand ekle = new OleDbCommand("insert into HAREKETLER(YAPILAN,TARIH,SAAT) values('"+yapilan+"','"+tarih+"','"+saat+"')",baglan);
            ekle.ExecuteNonQuery();
            baglan.Close();
        }
        string yapilan,tarih,saat;
        public static string kullanci;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                yapilan = "Giriş İşlemi Yapıldı" + " Yetkili : " + textBox1.Text;
                tarih = DateTime.Now.ToShortDateString().ToString();
                saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                baglan.Open();
                DataTable tablo = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'", baglan);
                adp.Fill(tablo);
                baglan.Close();
                if (tablo.Rows[0][3].ToString() != "")
                {
                    hatirla();
                    adminekle admekle = new adminekle(this);
                    cariekranı cari = new cariekranı(this);
                    stokekranı stkekran = new stokekranı(this);
                    satisekranı stsekran = new satisekranı(this);
                    kullanci = textBox1.Text;
                    hareketler();
                    anasayfa ana = new anasayfa();
                    ana.Show();
                    this.Hide();

                }
                
            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Giriş");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        DataTable tb = new DataTable();
        private void admingiris_Load(object sender, EventArgs e)
        {
            
            textBox1.Select();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from KULLANICI_HATIRLA", baglan);
            adp.Fill(tb);
            baglan.Close();
        }
        int s = 0;
        int hatirlakontrol;
        public void hatirla()
        {
            if (checkBox1.Checked)
            {
                hatirlakontrol = tb.Rows.Count;
                if (hatirlakontrol!=0)
                {
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {

                        if (tb.Rows[i][0].ToString() != textBox1.Text)
                        {
                            s = 1;
                        }
                        else
                        {
                            s = 0;
                        }
                    }
                    if (s == 1)
                    {
                        baglan.Open();
                        OleDbCommand ekle = new OleDbCommand("insert into KULLANICI_HATIRLA(kul_adi,sifre) values('" + textBox1.Text + "','" + textBox2.Text + "')", baglan);
                        ekle.ExecuteNonQuery();
                        baglan.Close();
                    }  
                }
                else
                {
                    baglan.Open();
                    OleDbCommand ekle = new OleDbCommand("insert into KULLANICI_HATIRLA(kul_adi,sifre) values('" + textBox1.Text + "','" + textBox2.Text + "')", baglan);
                    ekle.ExecuteNonQuery();
                    baglan.Close();
                }
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           /* DataTable t=new DataTable();
            baglan.Open();
            OleDbDataAdapter adp=new OleDbDataAdapter("select kul_adi from KULLANICI_HATIRLA where kul_adi like'"+textBox1.Text+"*'",baglan);
            adp.Fill(t);
            baglan.Close();
            textBox1.Text=t.Rows[0][0].ToString();*/
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (textBox1.Text == tb.Rows[i][0].ToString())
                {
                    button1.Select();
                    textBox2.Text = tb.Rows[i][1].ToString();
                }
            }
            
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void linklblsifreunuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifremiunuttum sf = new sifremiunuttum();
            sf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hatirla();
        }
    }
}
