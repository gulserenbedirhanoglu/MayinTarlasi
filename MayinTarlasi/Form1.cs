using System;
using System.Drawing;
using System.Windows.Forms;

namespace MayinTarlasi
{
    public partial class Form1 : Form
    {
        const int Boyut = 10; // Tahta boyutu (10x10)
        const int MayinSayisi = 10; // Toplam mayın sayısı
        Button[,] dugmeler = new Button[Boyut, Boyut];
        bool[,] mayinlar = new bool[Boyut, Boyut];
        bool oyunBitti = false;

        public Form1()
        {
            InitializeComponent();
            TahtayiOlustur();
            MayinlariYerlestir();
        }

        private void TahtayiOlustur()
        {
            this.ClientSize = new Size(Boyut * 40, Boyut * 40);

            for (int i = 0; i < Boyut; i++)
            {
                for (int j = 0; j < Boyut; j++)
                {
                    Button dugme = new Button
                    {
                        Size = new Size(40, 40),
                        Location = new Point(j * 40, i * 40),
                        Tag = new Point(i, j)
                    };
                    dugme.Click += Dugme_Click;
                    dugmeler[i, j] = dugme;
                    this.Controls.Add(dugme);
                }
            }
        }

        private void MayinlariYerlestir()
        {
            Random rastgele = new Random();
            int yerlestirilenMayin = 0;

            while (yerlestirilenMayin < MayinSayisi)
            {
                int x = rastgele.Next(Boyut);
                int y = rastgele.Next(Boyut);

                if (!mayinlar[x, y])
                {
                    mayinlar[x, y] = true;
                    yerlestirilenMayin++;
                }
            }
        }

        private void Dugme_Click(object sender, EventArgs e)
        {
            if (oyunBitti) return;

            Button tiklananDugme = sender as Button;
            Point koordinat = (Point)tiklananDugme.Tag;
            int x = koordinat.X;
            int y = koordinat.Y;

            if (mayinlar[x, y])
            {
                tiklananDugme.Text = "X";
                tiklananDugme.BackColor = Color.Red;
                OyunBitti("Mayına bastınız! Kaybettiniz.");
            }
            else
            {
                tiklananDugme.Text = "0";
                tiklananDugme.Enabled = false;

                if (OyunuKazandinizMi())
                {
                    OyunBitti("Tebrikler! Oyunu kazandınız.");
                }
            }
        }

        private bool OyunuKazandinizMi()
        {
            foreach (Button dugme in dugmeler)
            {
                Point koordinat = (Point)dugme.Tag;
                if (!mayinlar[koordinat.X, koordinat.Y] && dugme.Enabled)
                {
                    return false;
                }
            }
            return true;
        }

        private void OyunBitti(string mesaj)
        {
            oyunBitti = true;
            MessageBox.Show(mesaj);
            foreach (Button dugme in dugmeler)
            {
                Point koordinat = (Point)dugme.Tag;
                if (mayinlar[koordinat.X, koordinat.Y])
                {
                    dugme.Text = "X";
                    dugme.BackColor = Color.LightGray;
                }
            }
        }
    }
}
