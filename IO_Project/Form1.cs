using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
namespace IO_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string sourceFolderPath = @"U:\source";
        string targetFolderPath = @"U:\target";

        public void RefreshListBox(ListBox lb, string[] sa)
        {
            lb.Items.Clear();
            lb.Items.AddRange(sa);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] y = Directory.GetFiles(@"c:\windows");
            listBox.Items.Clear();
            listBox.Items.AddRange(y);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            dialog.SelectedPath = sourceFolderPath;
            dialog.Description = "Please select a folder.";
            dialog.ShowNewFolderButton = true;
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                sourceFolderPath = dialog.SelectedPath;
                lblDescription.Text = "Folder: " + sourceFolderPath;
                rtbDisplay.Text = "You have selected " + sourceFolderPath +
                    " as source folder.";
            }
            else
            {
                MessageBox.Show("operation cenceled by user");
                return;
            }



            //if (File.Exists(@"U:\source\ChoosePrintServerByIp.vbs"))
            //{
            //    //File.Delete(@"U:\source\ChoosePrintServerByIp.vbs");
            //    //rtbDisplay.Text = "The file is deleted!";

            //    File.Copy(@"U:\source\ChoosePrintServerByIp.vbs",
            //  @"U:\target\ChoosePrintServerByIp.vbs", true);

            //    rtbDisplay.Text = "The file is copied!";

            //}
            //else
            //{
            //    rtbDisplay.Text = "The file doesn't exist!";
            //}

            //string[] x = Directory.GetFiles(sourceFolderPath);
            //listBox.Items.Clear();
            //listBox.Items.AddRange(x);
            //listTarget.Items.Clear();
            //listTarget.Items.AddRange(x);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if ((listBox.SelectedIndex != -1))
            {
                string filePath = listBox.SelectedItem.ToString();

                string targetFilePath = filePath.Replace(sourceFolderPath, targetFolderPath);
                try
                {
                    File.Copy(filePath, targetFilePath, true);
                    rtbDisplay.Text = filePath + " is copied to" + targetFolderPath;
                }
                catch (Exception ex)
                {
                    rtbDisplay.Text = ex.Message;
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnListSub_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            String[] subFolderPaths = Directory.GetDirectories(sourceFolderPath);
            if (subFolderPaths.Length > 0)
            {
                RefreshListBox(listBox, subFolderPaths);
                rtbDisplay.Text = "Subfolders of " + sourceFolderPath + " are listed now.";

            }
            else
            {
                rtbDisplay.Text = sourceFolderPath + " doesn't ahve subfolders.";

            }
        }

        private void btnListFiles_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            listTarget.Items.Clear();
            //String[] filePaths = Directory.GetFiles(sourceFolderPath);
            //if (filePaths.Length > 0)
            //{
            //    RefreshListBox(listBox, filePaths);
            //    rtbDisplay.Text = "Files in " + sourceFolderPath + " are listed now.";

            //}
            //else
            //{
            //    rtbDisplay.Text = sourceFolderPath + " doesn't have any file.";
            //}
            String[] filePaths = Directory.GetFiles(sourceFolderPath);
            RefreshListBox(listBox, filePaths);

            filePaths = Directory.GetFiles(targetFolderPath);
            RefreshListBox(listTarget, filePaths);
            rtbDisplay.Text = "Files in both ListBoxes are listed.";
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if ((listBox.SelectedIndex != -1))
            {
                string filePath = listBox.SelectedIndex.ToString();
                string newFilePath = filePath.Replace(sourceFolderPath, targetFolderPath);
                try
                {
                    File.Move(filePath, newFilePath);
                    btnListFiles.PerformClick();
                    rtbDisplay.Text = filePath + " is moved to " + targetFolderPath;

                }
                catch (Exception ex)
                {
                    rtbDisplay.Text = ex.Message;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((listBox.SelectedIndex != -1))
            {
                string filePath = listBox.SelectedIndex.ToString();
                try
                {
                    File.Delete(filePath);
                    rtbDisplay.Text = filePath + " is deleted.";
                    btnListFiles.PerformClick();
                }
                catch (Exception ex)
                {
                    rtbDisplay.Text = ex.Message;
                }
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((listBox.SelectedIndex != -1))
            {
                string filePath = listBox.SelectedIndex.ToString();
                string currentFileName = Path.GetDirectoryName(filePath);
                string newFileName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Current file name is " + currentFileName + @".
Please type new file name.");
                string newFilePath = Path.Combine(sourceFolderPath, newFileName);
                try
                {
                    File.Move(filePath, newFilePath);
                    btnListFiles.PerformClick();
                    rtbDisplay.Text = currentFileName = " is renamed " + newFileName;
                }
                catch (Exception ex)
                {
                    rtbDisplay.Text = ex.Message;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(sourceFolderPath))
            {
                Directory.CreateDirectory(sourceFolderPath);

            }
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
        }
    }
}
