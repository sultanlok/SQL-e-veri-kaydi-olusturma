using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CsharpSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         
        SqlConnection baglanti = new SqlConnection("Data Source=SAMSUNGPC;Initial Catalog=projeee;Integrated Security=true;");
         
        private void verilerigoster()
        {
            if (baglanti.State == ConnectionState.Closed) {
            baglanti.Open();
            }

            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = Convert.ToString("");
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
           listView1.Items.Clear();
            try
            {
                SqlCommand kmt = new SqlCommand("select * from projemm", baglanti);
                SqlDataReader rdr = kmt.ExecuteReader();
                while (rdr.Read())
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = rdr["numara"].ToString();
                    li.SubItems.Add(rdr["ad"].ToString());
                    li.SubItems.Add(rdr["soyad"].ToString());
                    li.SubItems.Add(rdr["cinsiyet"].ToString());
                    li.SubItems.Add(rdr["bolum"].ToString());
                    li.SubItems.Add(rdr["yas"].ToString());
                    listView1.Items.Add(li);
                }
            }
            catch { MessageBox.Show("hata!!"); }
           baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {        //TÜM VERİLERİ GÖSTER    
                verilerigoster();       
        }
        
        private void button1_Click(object sender, EventArgs e)
        {// KAYDET TUŞU
            baglanti.Open(); textBox1.Focus();
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text !=Convert.ToString("") && textBox3.Text != "" && textBox4.Text != "")

            {               
                    SqlCommand komut = new SqlCommand("insert into projemm(ad,soyad,cinsiyet,bolum,yas)values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + comboBox1.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "')", baglanti);
                    komut.ExecuteNonQuery();
                    verilerigoster();
                
            }
            else { MessageBox.Show("Lütfen tüm bilgilerinizi doldurunuz.."); }                
         baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)  {   }
        private void button2_Click(object sender, EventArgs e)
        {//düzenle
            baglanti.Open();
            ata = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand komut = new SqlCommand("update projemm set ad='" + textBox1.Text.ToString() + "',soyad='" + textBox2.Text.ToString() + "',cinsiyet='" + comboBox1.Text.ToString() + "',bolum='" + textBox3.Text.ToString() + "',yas='" + textBox4.Text.ToString() + "' where numara=" + ata + "", baglanti);
            komut.ExecuteNonQuery();
            verilerigoster();
            baglanti.Close();
        }
        int ata = 0;
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int ata = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[4].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[5].Text;     
        }
        
        private void button3_Click(object sender, EventArgs e)
        {//sil tuşu
            
            baglanti.Open();
            ata=Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            SqlCommand komut = new SqlCommand("Delete from projemm where numara="+ata+"", baglanti);
            komut.ExecuteNonQuery();
            verilerigoster();
            baglanti.Close();
        }

       

        private void button4_Click(object sender, EventArgs e)
        {// arama duşu
            listView1.Items.Clear();
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("select * from projemm where ad like'%" + textBox5.Text.ToString() + "%' and soyad like'%" + textBox6.Text.ToString() + "%'", baglanti);
                SqlDataReader rdr = kmt.ExecuteReader();
                while (rdr.Read())
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = rdr["numara"].ToString();
                    li.SubItems.Add(rdr["ad"].ToString());
                    li.SubItems.Add(rdr["soyad"].ToString());
                    li.SubItems.Add(rdr["cinsiyet"].ToString());
                    li.SubItems.Add(rdr["bolum"].ToString());
                    li.SubItems.Add(rdr["yas"].ToString());
                    listView1.Items.Add(li);
                }
            
           baglanti.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        { //textBox4 e yazı girmesini engeller. (keypress özelliğinden )
            e.Handled = Char.IsLetter(e.KeyChar);
            
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetterOrDigit(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsNumber(e.KeyChar);

        }

       

       
    }
}
