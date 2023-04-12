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

        private void CloseApp () 
        {
            DialogResult result = MessageBox.Show("Do you want close that application?", "Confirmation", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                this.Close();
            }
            else
            {

            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
         //   opdAddInput.InitialDirectory = "C:";
            opdAddInput.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            opdAddInput.Title = "Select input files";
            opdAddInput.Filter = "Flat files (*.txt;*.csv)|*.txt;*.csv|" + "Text files (*.txt)|*.txt|" + "CSV files (*.csv)|*.csv|" + "All files (*.*)|*.*";
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
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                listView1.Items.Clear();
            }
            else
            {
                 
            }
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string allDocText = "";
            int i=0;

            try
            {
                foreach (var ittem in listView1.Items)
                {
                    StreamReader sr = new StreamReader(listView1.Items[i].Text);
                    allDocText += sr.ReadToEnd();
                    sr.Close();
                    allDocText += "\n";
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            allDocText = allDocText.Trim('\r', '\n'); // remove empty lines on the end of file

            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.Title = "Select input files";
            saveFileDialog1.Filter = "Flat files (*.txt;*.csv)|*.txt;*.csv|" + "Text files (*.txt)|*.txt|" + "CSV files (*.csv)|*.csv|" + "All files (*.*)|*.*";
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
            CloseApp();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Choose your catalog";
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                dialog.ShowNewFolderButton = false;

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    foreach (string file in Directory.EnumerateFiles(dialog.SelectedPath, "*.*"))
                    {
                        string extension = Path.GetExtension(file);
                        if (extension == ".txt" || extension == ".csv")
                        {
                            ListViewItem item = new ListViewItem(file);
                            listView1.Items.Add(item);
                        }
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    // MessageBox.Show("You canceled...");
                }
            }
        }

        private void btnDleleteSelected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                listView1.Items.Remove(item);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            // if (listView1.SelectedItems.Count > 0) // throw an error when you try to move more than one element - need fix it
            {
                ListViewItem[] selectedItems = new ListViewItem[listView1.SelectedItems.Count];
                listView1.SelectedItems.CopyTo(selectedItems, 0);

                int index = selectedItems[0].Index;

                if (index > 0)
                {
                    listView1.Items.RemoveAt(index);

                    foreach (ListViewItem item in selectedItems)
                    {
                        listView1.Items.Insert(index - 1, item);
                    }

                    listView1.Items[index - 1].Selected = true;
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListViewItem[] selectedItems = new ListViewItem[listView1.SelectedItems.Count];
                listView1.SelectedItems.CopyTo(selectedItems, 0);

                int index = selectedItems[0].Index;

                if (index < listView1.Items.Count-1)
                {
                    listView1.Items.RemoveAt(index);

                    foreach (ListViewItem item in selectedItems)
                    {
                        listView1.Items.Insert(index + 1, item);
                    }

                    listView1.Items[index + 1].Selected = true;
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseApp();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }
    }
}
