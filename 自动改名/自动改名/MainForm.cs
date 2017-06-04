/*
 * 由SharpDevelop创建。
 * 用户： Imba-Tjd
 * 日期: 2017/6/3
 * 时间: 21:57
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace 自动改名
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        static string sourceFolder;
        static string destinationFolder;
        List<string> sourceFiles;
        List<string> destinationFiles;
        
        void Button1Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                textBox1.Text = sourceFolder = folderBrowserDialog1.SelectedPath;
            }
        }
        void Button2Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                textBox2.Text = destinationFolder = folderBrowserDialog1.SelectedPath;
            }
        }
        void Button3Click(object sender, EventArgs e)
        {
            if (sourceFolder == null || destinationFolder == null || string.IsNullOrWhiteSpace(textBox3.Text))
                MessageBox.Show("文件夹路径不能为空或未填写自动改名应添加的字符串！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                try {
                    sourceFiles = AddFiles(sourceFolder);
                    destinationFiles = AddFiles(destinationFolder);
                    CopyFiles();
                    MessageBox.Show("成功", "成功");
                } catch {
                    MessageBox.Show("发生了一个错误！", "严重错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } finally {
                    button3.Enabled = false;
                }
        }
        List<string> AddFiles(string path)
        {
            var tempList = new List<string>();
            foreach (var file in Directory.GetFiles(path)) {
                tempList.Add(Path.GetFileName(file));
            }
            return tempList;
        }
        void CopyFiles()
        {
            string newName;
            foreach (var file in sourceFiles) {
                newName = destinationFiles.Contains(file) ? Path.GetFileNameWithoutExtension(file) + textBox3.Text + Path.GetExtension(file) : file;
                File.Copy(Path.Combine(sourceFolder, file), Path.Combine(destinationFolder, newName));
            }
        }
    }
}
