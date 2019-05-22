namespace StokTakip
{
    partial class adminsil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminsil));
            this.txtsoyad = new System.Windows.Forms.TextBox();
            this.txtad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtkulad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txteposta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtnadsoyad = new System.Windows.Forms.RadioButton();
            this.rbtnkulad = new System.Windows.Forms.RadioButton();
            this.rbtneposta = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtsoyad
            // 
            this.txtsoyad.Location = new System.Drawing.Point(69, 59);
            this.txtsoyad.Name = "txtsoyad";
            this.txtsoyad.Size = new System.Drawing.Size(100, 20);
            this.txtsoyad.TabIndex = 13;
            // 
            // txtad
            // 
            this.txtad.Location = new System.Drawing.Point(69, 33);
            this.txtad.Name = "txtad";
            this.txtad.Size = new System.Drawing.Size(100, 20);
            this.txtad.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Soyad :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ad :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Modern No. 20", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(108, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 34);
            this.label7.TabIndex = 14;
            this.label7.Text = "Yetkili Sil";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(265, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 55);
            this.button1.TabIndex = 15;
            this.button1.Text = "SİL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(12, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 51);
            this.label3.TabIndex = 16;
            this.label3.Text = "Yukardaki Bölümlerden\r\nEn Az Bir \'ini Doldurduktan\r\nSonra İşlemi Tamamlayın.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtad);
            this.groupBox1.Controls.Add(this.txtsoyad);
            this.groupBox1.Location = new System.Drawing.Point(5, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 90);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ad Soyad\'a Göre";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtkulad);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(200, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 90);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kullanıcı Adına Göre";
            // 
            // txtkulad
            // 
            this.txtkulad.Location = new System.Drawing.Point(37, 54);
            this.txtkulad.Name = "txtkulad";
            this.txtkulad.Size = new System.Drawing.Size(100, 20);
            this.txtkulad.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(43, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "Kullanıcı Adı";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txteposta);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(101, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(180, 90);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Epostaya Göre";
            // 
            // txteposta
            // 
            this.txteposta.Location = new System.Drawing.Point(8, 54);
            this.txteposta.Name = "txteposta";
            this.txteposta.Size = new System.Drawing.Size(165, 20);
            this.txteposta.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(64, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "E-posta";
            // 
            // rbtnadsoyad
            // 
            this.rbtnadsoyad.AutoSize = true;
            this.rbtnadsoyad.Location = new System.Drawing.Point(263, 261);
            this.rbtnadsoyad.Name = "rbtnadsoyad";
            this.rbtnadsoyad.Size = new System.Drawing.Size(120, 17);
            this.rbtnadsoyad.TabIndex = 9999;
            this.rbtnadsoyad.TabStop = true;
            this.rbtnadsoyad.Text = "Ad ve Soyad\'a Göre";
            this.rbtnadsoyad.UseVisualStyleBackColor = true;
            this.rbtnadsoyad.CheckedChanged += new System.EventHandler(this.rbtnadsoyad_CheckedChanged);
            // 
            // rbtnkulad
            // 
            this.rbtnkulad.AutoSize = true;
            this.rbtnkulad.Location = new System.Drawing.Point(263, 284);
            this.rbtnkulad.Name = "rbtnkulad";
            this.rbtnkulad.Size = new System.Drawing.Size(120, 17);
            this.rbtnkulad.TabIndex = 9999;
            this.rbtnkulad.TabStop = true;
            this.rbtnkulad.Text = "Kullanıcı Adına Göre";
            this.rbtnkulad.UseVisualStyleBackColor = true;
            this.rbtnkulad.CheckedChanged += new System.EventHandler(this.rbtnkulad_CheckedChanged);
            // 
            // rbtneposta
            // 
            this.rbtneposta.AutoSize = true;
            this.rbtneposta.Location = new System.Drawing.Point(263, 307);
            this.rbtneposta.Name = "rbtneposta";
            this.rbtneposta.Size = new System.Drawing.Size(97, 17);
            this.rbtneposta.TabIndex = 9999;
            this.rbtneposta.TabStop = true;
            this.rbtneposta.Text = "Eposta\'ya Göre";
            this.rbtneposta.UseVisualStyleBackColor = true;
            this.rbtneposta.CheckedChanged += new System.EventHandler(this.rbtneposta_CheckedChanged);
            // 
            // adminsil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(390, 390);
            this.Controls.Add(this.rbtneposta);
            this.Controls.Add(this.rbtnkulad);
            this.Controls.Add(this.rbtnadsoyad);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(406, 428);
            this.MinimumSize = new System.Drawing.Size(406, 428);
            this.Name = "adminsil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yetkili Sil";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.adminsil_FormClosing);
            this.Load += new System.EventHandler(this.adminsil_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtsoyad;
        private System.Windows.Forms.TextBox txtad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtkulad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txteposta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtnadsoyad;
        private System.Windows.Forms.RadioButton rbtnkulad;
        private System.Windows.Forms.RadioButton rbtneposta;
    }
}