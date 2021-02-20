using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotnetstrawberry
{
    public partial class Form1 : Form
    { //1.0
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try { EasyReorder.Reorder(ComboBox1.Text); TextBox4.Text += EasyReorder.report; }
            catch (TransferringErrorException ex) { TextBox4.Text += "Errore durante il trasferimento di un file, RT Error: " + ex.Message + Environment.NewLine; }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try { AdvancedReorder.Reorder(ComboBox2.Text, TextBox1.Text, TextBox2.Text); TextBox3.Text += EasyReorder.report; }
            catch (TransferringErrorException ex) { TextBox3.Text += "Errore durante il trasferimento di un file, RT Error: " + ex.Message + Environment.NewLine; }           
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            try { KeywordReorder.Reorder(ComboBox3.Text, TextBox6.Text); TextBox5.Text += EasyReorder.report; }
            catch (TransferringErrorException ex) { TextBox5.Text += "Errore durante il trasferimento di un file, RT Error: " + ex.Message + Environment.NewLine; }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            try { ByDateReorder.Reorder(ComboBox4.Text, ComboBox5.Text, DateTimePicker1.Value, DateTimePicker2.Value, CheckBox1.Checked); TextBox7.Text += EasyReorder.report; }
            catch (TransferringErrorException ex) { TextBox7.Text += "Errore durante il trasferimento di un file, RT Error: " + ex.Message + Environment.NewLine; }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(Panel1.Visible == false)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (Panel2.Visible == false)
            {
                Panel2.Visible = true;
                Panel1.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (Panel3.Visible == false)
            {
                Panel3.Visible = true;
                Panel2.Visible = false;
                Panel1.Visible = false;
                Panel4.Visible = false;
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (Panel4.Visible == false)
            {
                Panel4.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel1.Visible = false;
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                ComboBox4.Text = f.SelectedPath;
            }          
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                ComboBox3.Text = f.SelectedPath;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                ComboBox1.Text = f.SelectedPath;
            }
        }
    }
}
