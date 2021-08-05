using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contatore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            startLog();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) + 1);
            richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Aggiunto 1 punto a " + textBox3.Text + "\n";
            if (Convert.ToInt32(label2.Text) == Convert.ToInt32(textBox1.Text))
            {
                label6.Visible = true;
                
                richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Ha vinto " + textBox3.Text + "\n";
                playTadaSound();
                var result = MessageBox.Show(textBox3.Text + " ha vinto. Vuoi resettare il contatore?", "Vincitore", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    reSet();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = Convert.ToString(Convert.ToInt32(label3.Text) + 1);
            richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Aggiunto 1 punto a " + textBox2.Text + "\n";
            if (Convert.ToInt32(label3.Text) == Convert.ToInt32(textBox1.Text))
            {
                label7.Visible = true;
                
                richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Ha vinto " + textBox2.Text + "\n";
                playTadaSound();
                var result = MessageBox.Show(textBox2.Text + " ha vinto. Vuoi resettare il contatore?", "Vincitore", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    reSet();
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) - 1);
            richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Rimosso 1 punto a " + textBox3.Text + "\n";
            if (label6.Visible == true && Convert.ToInt32(label2.Text) < Convert.ToInt32(textBox1.Text))
            {
                label6.Visible = false;
                richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Rimossa vincita a " + textBox3.Text + "\n";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = Convert.ToString(Convert.ToInt32(label3.Text) - 1);
            richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Rimosso 1 punto a " + textBox2.Text + "\n";
            if (label7.Visible == true && Convert.ToInt32(label3.Text) < Convert.ToInt32(textBox1.Text))
            {
                label7.Visible = false;
                richTextBox1.Text = richTextBox1.Text + DateTime.Now + ": Rimossa vincita a " + textBox2.Text + "\n";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            reSet();
        }
        private void reSet()
        {
            Directory.CreateDirectory(Convert.ToString(Path.GetTempPath()) + @"cont");

            richTextBox1.SaveFile(Convert.ToString(Path.GetTempPath()) + @"cont\log" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".rtf", RichTextBoxStreamType.RichText);
            label2.Text = "0";
            label3.Text = "0";
            label6.Visible = false;
            label7.Visible = false;
            richTextBox1.Text = "Reset; ";
            startLog();
        }
        private void startLog()
        {
            richTextBox1.Text = richTextBox1.Text + "Log partito alle " + DateTime.Now + "\n";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            if (checkBox1.Checked == false)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
        }
        private void playTadaSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\tada.wav");
            simpleSound.Play();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                richTextBox1.Visible = true;
            }
            if (checkBox2.Checked == false)
            {
                richTextBox1.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            saveFileDialog1.ShowDialog();
            richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            richTextBox1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                {
                    Directory.CreateDirectory(Convert.ToString(Path.GetTempPath()) + @"cont");
                    client.DownloadFile("https://download.alegsoftware.ga/ws_switches/contatore/ltromatic.ttf", Convert.ToString(Path.GetTempPath()) + @"cont\font.ttf");
                    MessageBox.Show("Installa il font che viene visualizzato...");
                    //MessageBox.Show("fontview " + Convert.ToString(Path.GetTempPath()) + @"cont\font.ttf");
                    Process.Start("fontview", Convert.ToString(Path.GetTempPath()) + @"cont\font.ttf");
                    
                }
            }
            catch
            {
                //MessageBox.Show("error.");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Directory.CreateDirectory(Convert.ToString(Path.GetTempPath()) + @"cont");
            richTextBox1.Visible = true;
            richTextBox1.SaveFile(Convert.ToString(Path.GetTempPath()) + @"cont\log" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".rtf", RichTextBoxStreamType.RichText);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Convert.ToString(Path.GetTempPath()) + @"cont");
            Process.Start(Convert.ToString(Path.GetTempPath()) + @"cont\");
            

        }
    }
}
