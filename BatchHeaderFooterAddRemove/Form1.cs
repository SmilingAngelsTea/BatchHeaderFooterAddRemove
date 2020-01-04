using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchHeaderFooterAddRemove {
    public partial class BatchHeaderFooterAddRemove : Form {
        public List<string> fileList;
        public List<string> tempFileList;
        public BatchHeaderFooterAddRemove() {
            InitializeComponent();
            fileList = new List<string>();
            tempFileList = new List<string>();
        }
        private void Button1_Click(object sender, EventArgs e) {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    fileList.Add(openFileDialog.FileName);
                }
            }
            fileList = fileList.Distinct().ToList();
            listBox1.Items.Clear();
            for (int i = 0; i < fileList.Count; i++) {
                listBox1.Items.Add(fileList[i]);
            }
        }

        public void ListBox1_SelectedIndexChanged(object sender, EventArgs e) {
        }

        private void Button2_Click(object sender, EventArgs e) {
            FolderBrowserDialog Klasor = new FolderBrowserDialog();
            Klasor.ShowDialog();
            if (Klasor.SelectedPath != "") {
                string extension;
                if (textBox1.Text == "") {
                    extension = "*";
                } else {
                    extension = textBox1.Text;
                }
                string[] fileArray = Directory.GetFiles(Klasor.SelectedPath, "*." + extension, SearchOption.AllDirectories);
                for (int i = 0; i < fileArray.Length; i++) {
                    fileList.Add(fileArray[i]);
                }
            }
            fileList = fileList.Distinct().ToList();
            listBox1.Items.Clear();
            for (int i = 0; i < fileList.Count; i++) {
                listBox1.Items.Add(fileList[i]);
            }
        }

        private void Button3_Click(object sender, EventArgs e) {
            FolderBrowserDialog Klasor = new FolderBrowserDialog();
            Klasor.ShowDialog();
            if (Klasor.SelectedPath != "") {
                string extension;
                if (textBox1.Text == "") {
                    extension = "*";
                } else {
                    extension = textBox1.Text;
                }
                string[] fileArray = Directory.GetFiles(Klasor.SelectedPath, "*." + extension, SearchOption.TopDirectoryOnly);
                for (int i = 0; i < fileArray.Length; i++) {
                    fileList.Add(fileArray[i]);
                }
            }
            fileList = fileList.Distinct().ToList();
            listBox1.Items.Clear();
            for (int i = 0; i < fileList.Count; i++) {
                listBox1.Items.Add(fileList[i]);
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e) {
            string text = textBox1.Text;
        }
        private void TextBox2_TextChanged(object sender, EventArgs e) {
            string text = textBox2.Text;
        }
        private void TextBox5_TextChanged(object sender, EventArgs e) {
            string text = textBox5.Text;
        }
        private void TextBox3_TextChanged_1(object sender, EventArgs e) {
            string text = textBox3.Text;
        }
        private void TextBox4_TextChanged(object sender, EventArgs e) {
            string text = textBox4.Text;
        }

        private void Button5_Click(object sender, EventArgs e) {
            if (fileList.Count == 0) {
                MessageBox.Show("No files selected.");
            } else {
                if (textBox2.Text == "") {
                    textBox2.Text = "0";
                }
                if (textBox5.Text == "") {
                    textBox5.Text = "0";
                }
                int successful = 0;
                int failed = 0;
                for (int i = 0; i < fileList.Count; i++) {
                    byte[] raw = File.ReadAllBytes(fileList[i]);
                    if (Int32.Parse(textBox2.Text) + Int32.Parse(textBox5.Text) >= raw.Length) {
                        MessageBox.Show(fileList[i] + " is too small to remove " + (Int32.Parse(textBox2.Text) + Int32.Parse(textBox5.Text)) + " bytes from.");
                        failed++;
                    } else {
                        raw = raw.Skip(Int32.Parse(textBox2.Text)).ToArray();
                        byte[] temp = new byte[raw.Length - Int32.Parse(textBox5.Text)];
                        Array.Copy(raw, temp, raw.Length - Int32.Parse(textBox5.Text));
                        byte[] addBeginning = Encoding.ASCII.GetBytes(textBox4.Text);
                        byte[] addEnding = Encoding.ASCII.GetBytes(textBox3.Text);
                        byte[] allByteArray = new byte[addBeginning.Length + temp.Length + addEnding.Length];

                        System.Buffer.BlockCopy(addBeginning, 0, allByteArray, 0, addBeginning.Length);
                        System.Buffer.BlockCopy(temp, 0, allByteArray, addBeginning.Length, temp.Length);
                        System.Buffer.BlockCopy(addEnding, 0, allByteArray, addBeginning.Length + temp.Length, addEnding.Length);

                        if (textBox6.Text == "") {
                            if (textBox7.Text == "") {
                                File.WriteAllBytes(Path.GetDirectoryName(fileList[i]) + "/" + Path.GetFileName(fileList[i]), allByteArray);
                            } else {
                                File.WriteAllBytes(Path.GetDirectoryName(fileList[i]) + "/" + Path.GetFileNameWithoutExtension(fileList[i]) + "." + textBox7.Text, allByteArray);
                            }
                        } else {
                            if (textBox7.Text == "") {
                                File.WriteAllBytes(textBox6.Text + "/" + Path.GetFileName(fileList[i]), allByteArray);
                            } else {
                                File.WriteAllBytes(textBox6.Text + "/" + Path.GetFileNameWithoutExtension(fileList[i]) + "." + textBox7.Text, allByteArray);
                            }
                        }
                        successful++;
                    }
                    MessageBox.Show(failed + " failed " + successful + " successful conversions.");
                }
            }
        }
        private void Button6_Click(object sender, EventArgs e) {
            FolderBrowserDialog Klasor = new FolderBrowserDialog();
            Klasor.ShowDialog();
            if (Klasor.SelectedPath != "") {
                textBox6.Text = Klasor.SelectedPath;
            }
        }

        private void TextBox6_TextChanged(object sender, EventArgs e) {
            string text = textBox6.Text;
        }

        private void TextBox7_TextChanged(object sender, EventArgs e) {
            string text = textBox7.Text;
        }

        private void Button4_Click(object sender, EventArgs e) {
            if (listBox1.SelectedItems.Count > 0) {
                tempFileList.Clear();
                for (int i = 0; i < fileList.Count; i++) {
                    tempFileList.Add(fileList[i]);
                }
                fileList.Clear();
                int[] selected = new int[listBox1.SelectedItems.Count];
                for (int y = 0; y < listBox1.SelectedItems.Count; y++) {
                    selected[y] = listBox1.Items.IndexOf(listBox1.SelectedItems[y]);
                }
                for (int i = 0; i < tempFileList.Count; i++) {
                    bool toBeDeleted = false;
                    for(int y = 0; y < selected.Length; y++) {
                        if(i == selected[y]) {
                            toBeDeleted = true;
                        }
                    }
                    if(!toBeDeleted) {
                        fileList.Add(tempFileList[i]);
                    }
                    listBox1.Items.Clear();
                    for (int x = 0; x < fileList.Count; x++) {
                        listBox1.Items.Add(fileList[x]);
                    }
                }
            }

        }
    }
}
