using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Merging_files
{
    public partial class MergingFiles : Form
    {
        public MergingFiles()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
         //   opdAddInput.InitialDirectory = "C:";
            opdAddInput.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            opdAddInput.Title = "Select input files";
            opdAddInput.Filter = "Flat files (*.TXT;*.CSV)|*.TXT;*.CSV|" + "All files (*.*)|*.*";
            opdAddInput.ShowDialog();
            //   MessageBox.Show(filePath);
            foreach (String file in opdAddInput.FileNames)
            {
                ListViewItem item = new ListViewItem(file);
                listView1.Items.Add(item);
            }
        }

        private void btnDleleteAll_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string allDocText = "";
            int i=0;

            foreach (var ittem in listView1.Items)
            {
                StreamReader sr = new StreamReader(listView1.Items[i].Text);
                allDocText+= sr.ReadToEnd();
                sr.Close();
                allDocText += "\n";
                i++;
            }

            allDocText = allDocText.Trim('\r', '\n'); // remove empty lines on the end of file

            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.Title = "Select input files";
            saveFileDialog1.Filter = "Flat files (*.TXT;*.CSV)|*.TXT;*.CSV|" + "All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();

            try
            {
                StreamWriter plik = new StreamWriter(saveFileDialog1.FileName, false); // false to clear file before write
                plik.WriteLine(allDocText);
                plik.Close();
                MessageBox.Show("Saving completed");

            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
