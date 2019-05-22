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
    public partial class adminraporlama : Form
    {
        public adminraporlama()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DATA.mdb");
        private void adminraporlama_Load(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                DataTable tb = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from ADMIN", baglan);
                adp.Fill(tb);
                baglan.Close();
                raporadmin rpr = new raporadmin();
                rpr.SetDataSource(tb);
                crystalReportViewer1.ReportSource = rpr;
            }
            catch (Exception)
            {

                MessageBox.Show("Raporlama İşlemi Yapılamadı Tekrar Deneyiniz");
            }
            
        }

        private void adminraporlama_FormClosing(object sender, FormClosingEventArgs e)
        {
            adminraporlama adm = new adminraporlama();
            adm.Close();
        }
    }
}
