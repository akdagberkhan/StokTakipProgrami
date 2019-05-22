using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StokTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        int süre = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            süre = rnd.Next(2,4);
            timer1.Start();
        }
        int giris = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {

            giris++;
            if (giris==süre)
            {
                timer1.Stop();
                admingiris admgir = new admingiris();
                admgir.Show();
                 this.Hide();
            }
        }
    }
}
