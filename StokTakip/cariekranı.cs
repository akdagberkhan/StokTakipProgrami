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
    public partial class cariekranı : Form
    {
        admingiris adm;
        public cariekranı(Form gelen)
        {
            adm = gelen as admingiris;
            InitializeComponent();
        }
        public cariekranı()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");
        string yapilan ,tarih,saat;

        //Hareketler Tablosuna Bilgi Ekle
        public void hareketler()
        {
            try
            {
                baglan.Open();
                OleDbCommand ekle = new OleDbCommand("insert into HAREKETLER(YAPILAN,TARIH,SAAT) values('" + yapilan + "','" + tarih + "','" + saat + "')", baglan);
                ekle.ExecuteNonQuery();
                baglan.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("İşlem Hareketler Tablosuna Eklenemedi");
            }
           
        }

        //Cari Hesap Ekleme
        private void button2_Click(object sender, EventArgs e)
        {
            if (cboxhesapturekle.Text==""||cboxveresiyesatisekle.Text==""||txtadres1.Text==""||txtadres2.Text==""||txteposta.Text==""||txthesapadiekle.Text==""||txtilce.Text==""||txtkartnoekle.Text==""||txtsehir.Text==""||txtsemt.Text==""||txttel1.Text==""||txttel2.Text==""||txtvergidairesiekle.Text==""||txtverginumarasi.Text==""||txtyetkilikisiekle.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    baglan.Open();
                    OleDbCommand ekle = new OleDbCommand("insert into CARI_BILGI(kart_no,hesap_tur,veresiye_satis,hesap_adi,yetkili_kisi,vergi_dairesi,vergi_numarasi,adres,adres2,semt,ilce,sehir,tel1,tel2,eposta) values('" + txtkartnoekle.Text + "','" + cboxhesapturekle.Text + "','" + cboxveresiyesatisekle.Text + "','" + txthesapadiekle.Text + "','" + txtyetkilikisiekle.Text + "','" + txtvergidairesiekle.Text + "','" + txtverginumarasi.Text + "','" + txtadres1.Text + "','" + txtadres2.Text + "','" + txtsemt.Text + "','" + txtilce.Text + "','" + txtsehir.Text + "','" + txttel1.Text + "','" + txttel2.Text + "','" + txteposta.Text + "')", baglan);
                    ekle.ExecuteNonQuery();
                    baglan.Close();
                    yapilan = "Cari Hesabı Oluşturuldu";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                    MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception)
                {

                    MessageBox.Show("Boş Alan Bıraktınız Yada Hatalı Giriş Yaptınız", "Hesap Eklenemedi");
                }
            }

            
            
        }

        //Cari Ekranı Açıldığında Cari tablosundaki verileri getirme ve datagridview e yazdırma
        private void cariekranı_Load(object sender, EventArgs e)
        {
            txtguncelyetkili.Text = admingiris.kullanci;
            cariyetkikontrol();
            baglan.Open();
            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from CARI_BILGI",baglan);
            adp.Fill(tb);
            dataGridView1.DataSource = tb;
            baglan.Close();
            
        }

        // Cari tablosundaki verileri getirme ve datagridview e yazdırma
        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from CARI_BILGI", baglan);
            adp.Fill(tb);
            dataGridView1.DataSource = tb;
            baglan.Close();
        }

        //Cari Tablosundan Belirtilen Hesabı Silme İşlemi
        private void button7_Click(object sender, EventArgs e)
        {
            if (txthesapsil.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    baglan.Open();
                    OleDbCommand sil = new OleDbCommand("delete from CARI_BILGI where kart_no='" + txthesapsil.Text + "'", baglan);
                    sil.ExecuteNonQuery();
                    baglan.Close();
                    yapilan = "Cari Hesabı Silindi";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                    MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception)
                {

                    MessageBox.Show("Silme İşelmi Yapılamadı Lütfen Bilgileri Kontrol Ediniz.");
                }
            }
            
           
        }

        //Cari Tablosundaki Belirtilen Hesabı Arama Ve bulunan Bilgileri datagrridview e yazdırma
        private void button9_Click(object sender, EventArgs e)
        {
            if (txthesapbul.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    baglan.Open();
                    DataTable tb = new DataTable();
                    OleDbDataAdapter adp = new OleDbDataAdapter("select * from CARI_BILGI where kart_no='" + txthesapbul.Text + "'", baglan);
                    adp.Fill(tb);
                    dataGridView2.DataSource = tb;
                    baglan.Close();
                    yapilan = "Cari Hesabı Araması Yapıldı";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                }
                catch (Exception)
                {

                    MessageBox.Show("Lütfen Bilginin Doğruluğundan Emin Olun", "Hesap Bulunamadı");
                }
           
            }
            
        }

        //Cari Tablosundaki Belirtilen hesaba ait bilgileri güncelleme işlemi
        private void button5_Click(object sender, EventArgs e)
        {
            if (cboxguncelhesaptur.Text==""||cboxguncelveresiyesatis.Text==""||txtgunceladres1.Text==""||txtgunceladres2.Text==""||txtgunceleposta.Text==""||txtguncelhesapadi.Text==""||txtguncelilce.Text==""||txtguncelkartno.Text==""||txtguncelsehir.Text==""||txtguncelsemt.Text==""||txtgunceltel1.Text==""||txtgunceltel2.Text==""||txtguncelvergidairesi.Text==""||txtguncelvergino.Text==""||txtguncelyetkili.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                try
                {
                    baglan.Open();
                    OleDbCommand guncelle = new OleDbCommand("update CARI_BILGI set kart_no='" + txtguncelkartno.Text + "',hesap_tur='" + cboxguncelhesaptur.Text + "',veresiye_satis='" + cboxguncelveresiyesatis.Text + "',hesap_adi='" + txtguncelhesapadi.Text + "',yetkili_kisi='" + txtguncelyetkili.Text + "',vergi_dairesi='" + txtguncelvergidairesi.Text + "',vergi_numarasi='" + txtguncelvergino.Text + "',adres='" + txtgunceladres1.Text + "',adres2='" + txtgunceladres2.Text + "',semt='" + txtguncelsemt.Text + "',ilce='" + txtguncelilce.Text + "',sehir='" + txtguncelsehir.Text + "',tel1='" + txtgunceltel1.Text + "',tel2='" + txtgunceltel2.Text + "',eposta='" + txtgunceleposta.Text + "' where kart_no='" + txtguncellekartno.Text + "'", baglan);
                    guncelle.ExecuteNonQuery();
                    baglan.Close();
                    yapilan = "Cari Hesab Bilgileri Güncellendi";
                    tarih = DateTime.Now.ToShortDateString().ToString();
                    saat = "Saat : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    hareketler();
                    MessageBox.Show("İşlem Başarıyla Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                catch (Exception)
                {

                    MessageBox.Show("Bilgiler Güncellenemedi Lütfen Kontrol Ediniz...");
                }
            }
            
            
        }

        private void cariekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
            cariekranı cekran = new cariekranı();
            cekran.Close();
        }


        //belirlenen textboxlara sadece sayı girebilme komutu
        private void txtkartnoekle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtverginumarasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txttel1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txttel2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtguncellekartno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtguncelkartno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtgunceltel1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtgunceltel2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtguncelvergino_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txthesapsil_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txthesapbul_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        //Cari Hesap Ekle Alanındaki Textboxları Ve Comboboxları Temizler
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control m in groupBox1.Controls)
            {
                if (m is TextBox)
                {
                    m.Text = "";
                }
                if (m is ComboBox)
                {
                    m.Text = "";
                }
            }
            foreach (Control k in groupBox1.Controls)
            {
                if (k is TextBox)
                {
                    k.Text = "";
                }
               
            }
        }


        //Cari Hesap Güncelleme Alanındaki Textboxları ve Comboboxları Temizler
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control l in groupBox4.Controls)
            {
                if (l is TextBox)
                {
                    l.Text = "";
                }
                if (l is ComboBox)
                {
                    l.Text = "";
                }
            }
            foreach (Control k in groupBox3.Controls)
            {
                if (k is TextBox)
                {
                    k.Text = "";
                }

            }
            txtguncelkartno.Text = "";
        }



        //Cari Hesap Sil Alanındaki Textboxu temizler
        private void button6_Click(object sender, EventArgs e)
        {
            txthesapsil.Text = "";
        }



        //Cari Hesap Arama Alanındaki Textboxu temizler
        private void button8_Click(object sender, EventArgs e)
        {
            txthesapbul.Text = "";
        }

        string yetkikontrol;
        public void cariyetkikontrol()
        {
            yetkikontrol = admingiris.kullanci;
            DataTable tb = new DataTable();
            baglan.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN where kullanici_adi='" + yetkikontrol + "'", baglan);
            adp.Fill(tb);
            baglan.Close();
            if (tb.Rows[0][8].ToString() == "1")
            {
            }
            else if (tb.Rows[0][8].ToString() == "0")
            {
                MessageBox.Show("Cari Hesap Yetkiniz Bulunmamaktadır.");
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                hesapsil.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }
        }

        private void txtguncellekartno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bilgilerilistele();
                for (int i = 0; i < tabl.Rows.Count; i++)
                {
                    if (tabl.Rows[i][0].ToString() == txtguncellekartno.Text)
                    {
                        txtguncelkartno.Text = tabl.Rows[i][0].ToString();
                        txtgunceladres1.Text = tabl.Rows[i][7].ToString();
                        txtgunceladres2.Text = tabl.Rows[i][8].ToString();
                        txtgunceleposta.Text = tabl.Rows[i][14].ToString();
                        txtguncelhesapadi.Text = tabl.Rows[i][3].ToString();
                        txtguncelilce.Text = tabl.Rows[i][10].ToString();
                        txtguncelsehir.Text = tabl.Rows[i][11].ToString();
                        txtguncelsemt.Text = tabl.Rows[i][9].ToString();
                        txtgunceltel1.Text = tabl.Rows[i][12].ToString();
                        txtgunceltel2.Text = tabl.Rows[i][13].ToString();
                        txtguncelvergino.Text = tabl.Rows[i][6].ToString();
                        txtguncelvergidairesi.Text= tabl.Rows[i][5].ToString();
                        txtguncelyetkili.Text = tabl.Rows[i][4].ToString();
                        cboxguncelhesaptur.Text= tabl.Rows[i][1].ToString();
                        cboxguncelveresiyesatis.Text = tabl.Rows[i][2].ToString();
                        
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        DataTable tabl = new DataTable();
        public void bilgilerilistele()
        {

            baglan.Open();
            OleDbDataAdapter veri = new OleDbDataAdapter("select * from CARI_BILGI", baglan);
            veri.Fill(tabl);
            baglan.Close();
        }
    }
}
