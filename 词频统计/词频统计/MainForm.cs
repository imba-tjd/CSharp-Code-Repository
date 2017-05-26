/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/5/20
 * 时间: 19:46
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;

namespace 词频统计
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateToRemove();
        }
        
        static string fileFullPath;
        static Dictionary<string,int> statistics = new Dictionary<string, int>();
        static List<string> toRemove = new List<string>();
        
        ///////////////////////////////////////////////////////////////
        // 处理勾上和取消勾和一些其他的简单的功能
        void 过滤介词ToolStripMenuItemClick(object sender, EventArgs e)
        {
            过滤介词ToolStripMenuItem.Checked = !过滤介词ToolStripMenuItem.Checked;
            UpdateToRemove();
        }
        
        void 过滤连词ToolStripMenuItemClick(object sender, EventArgs e)
        {
            过滤连词ToolStripMenuItem.Checked = !过滤连词ToolStripMenuItem.Checked;
            UpdateToRemove();
        }
            
        void 过滤疑问词ToolStripMenuItemClick(object sender, EventArgs e)
        {
            过滤疑问词ToolStripMenuItem.Checked = !过滤疑问词ToolStripMenuItem.Checked;
            UpdateToRemove();
        }
        
        void 区分大小写ToolStripMenuItemClick(object sender, EventArgs e)
        {
            区分大小写ToolStripMenuItem.Checked = !区分大小写ToolStripMenuItem.Checked;
            UpdateToRemove();
        }
        
        void 实时统计ToolStripMenuItemClick(object sender, EventArgs e)
        {
            实时统计ToolStripMenuItem.Checked = !实时统计ToolStripMenuItem.Checked;
            SpliterSToolStripMenuItemClick(null, null);
        }
        
        void ExitEToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("真的要退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                Application.Exit();
        }
        
        void AboutAToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show("by Imba-Tjd\n    2017/5/20", "关于");
        }
        
        ///////////////////////////////////////////////////////////////
        // 处理拖拽
        void MainFormDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }
        
        void MainFormDragDrop(object sender, DragEventArgs e)
        {
            var s = e.Data.GetData(DataFormats.FileDrop) as string[];
            LoadFile(s[0]);
        }
        
        void TextBox1DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }
        
        void TextBox1DragDrop(object sender, DragEventArgs e)
        {
            LoadFile(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
        }
        
        ///////////////////////////////////////////////////////////////
        // 需要过滤掉的的单词
        void UpdateToRemove()
        {
            toRemove.Clear();
            toRemove.AddRange(new List<string> {
                ",",
                ".",
                " ",
                "+",
                "-",
                "*",
                "/",
                "=",
                "\\",
                "?",
                "!",
                ";",
                ":",
                "",
                Environment.NewLine
            });
            
            if (过滤连词ToolStripMenuItem.Checked)
                toRemove.AddRange(new List<string> {
                    "and",
                    "but",
                    "or",
                    "nor",
                    "so",
                    "yet",
                    "for"
                });
            
            if (过滤介词ToolStripMenuItem.Checked)
                toRemove.AddRange(new List<string> {
                    "in",
                    "on",
                    "with",
                    "by",
                    "for",
                    "at",
                    "about",
                    "under",
                    "of",
                    "to"
                });
            
            if (过滤疑问词ToolStripMenuItem.Checked)
                toRemove.AddRange(new List<string> {
                    "what",
                    "why",
                    "when",
                    "where",
                    "how"
                });
        }
        
        ///////////////////////////////////////////////////////////////
        // 读写文件
        bool WriteText(string text, string path)
        {
            FileStream fs = null;
            try {
                fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(Encoding.ASCII.GetBytes(text), 0, text.Length);
                fs.Flush();
                return true;
            } catch {
                MessageBox.Show("写入文件时发生了一个错误！\n请检查是否有足够的权限！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } finally {
                if (fs != null)
                    fs.Close();
            }
        }
        
        bool LoadText(string path)
        {
            FileStream fs = null;
            StreamReader sr = null;
            try {
                fs = File.Open(path, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.ASCII);
                textBox1.Text = sr.ReadToEnd();
                return true;
            } catch {
                MessageBox.Show("读取文件时发生了一个错误！\n请检查是否有足够的权限！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } finally {
                if (fs != null)
                    fs.Close();
                if (sr != null)
                    sr.Close();
            }
        }
        
        void LoadFile(string path)
        {
            if (LoadText(path)) {
                fileFullPath = path;
                textBox1.SelectionStart = textBox1.Text.Length;
                saveSToolStripMenuItem.Enabled = false;
                saveAsAToolStripMenuItem.Enabled = false;
                toolStripStatusLabel6.Text = fileFullPath;
                listView1.Items.Clear();
            }
            
        }
        
        ///////////////////////////////////////////////////////////////
        void NewNToolStripMenuItemClick(object sender, EventArgs e)// 按下New
        {
            DialogResult result = DialogResult.Cancel;
            if (saveSToolStripMenuItem.Enabled)
                result = MessageBox.Show("是否保存更改？", "新建", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            
            if (result == DialogResult.OK)
                SaveSToolStripMenuItemClick(null, null);
            if (result != DialogResult.Cancel)
                textBox1.Text = "";
        }
        
        void OpenOToolStripMenuItemClick(object sender, EventArgs e)// 按下Open
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog() {
                Filter = "文本文件|*.txt|所有文件|*.*"
            };
            
            if (fileFullPath == null)// 设置打开窗口后的路径和文件名
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(fileFullPath);
                openFileDialog1.FileName = Path.GetFileName(fileFullPath);
            }
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK) // 载入文件
                LoadFile(openFileDialog1.FileName);
        }
        
        void TextBox1TextChanged(object sender, EventArgs e)
        {
            saveSToolStripMenuItem.Enabled = true;
            saveAsAToolStripMenuItem.Enabled = true;
            spliterSToolStripMenuItem.Enabled = true;
            saveSpliterResultRToolStripMenu.Enabled = true;
            if (实时统计ToolStripMenuItem.Checked)// 实时统计功能
                SpliterSToolStripMenuItemClick(null, null);
        }
        
        void SaveSToolStripMenuItemClick(object sender, EventArgs e)// 按下Save
        {
            saveSToolStripMenuItem.Enabled = false;
            saveAsAToolStripMenuItem.Enabled = false;
            if (fileFullPath == null) {
                SaveAsAToolStripMenuItemClick(null, null);
            } else
                WriteText(textBox1.Text, fileFullPath);
        }
        
        void SaveAsAToolStripMenuItemClick(object sender, EventArgs e)// 按下Save As
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() {
                InitialDirectory = Path.GetDirectoryName(fileFullPath),
                FileName = Path.GetFileName(fileFullPath),
                Filter = "文本文件(*.txt)|*.txt",
                AddExtension = true
            };
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                fileFullPath = saveFileDialog.FileName;
                saveSToolStripMenuItem.Enabled = false;
                saveAsAToolStripMenuItem.Enabled = false;
                toolStripStatusLabel6.Text = fileFullPath;
                WriteText(textBox1.Text, fileFullPath);
            }
        }
        
        void SpliterSToolStripMenuItemClick(object sender, EventArgs e)// 按下Spliter
        {
            statistics = new Dictionary<string, int>();
            StatisticAdder();
            CalculateWords();
            StatisticFilter();
            
            listView1.Items.Clear();
            foreach (var word in statistics) {
                ListViewItem item = new ListViewItem(word.Key);
                item.SubItems.Add(word.Value.ToString());
                listView1.Items.Add(item);
            }
        }
        
        void StatisticAdder()// 把数据加到dictionary里
        {
            string[] text = textBox1.Text.Split(' ', ',', '.', '?', '!');
            string newKey;
            foreach (string key in text) {
                if (!区分大小写ToolStripMenuItem.Checked)
                    newKey = key.ToLower();
                else
                    newKey = key;
                if (statistics.ContainsKey(newKey))
                    ++statistics[newKey];
                else
                    statistics.Add(newKey, 1);
            }
        }
        
        void CalculateWords()// 计算单词数和字母数
        {
            int letters = 0, words = 0;
            foreach (var e in statistics)
                if (e.Key != " " || e.Key != "," || e.Key != ".") {
                    words += e.Value;
                    letters += e.Key.Length * e.Value;
                }
            toolStripStatusLabel2.Text = letters.ToString();
            toolStripStatusLabel4.Text = words.ToString();
        }
        
        void StatisticFilter()// 过滤一些单词
        {
            //TODO:过滤大写单词
//            var r = from e in statistics
//                where toRemove.Contains(e.Key.ToLower())
//                select e;
//            foreach (KeyValuePair<string,int> e in r) {
//                statistics.Remove(e.Key);
//            }

            foreach (var e in toRemove) {
                if (statistics.ContainsKey(e))
                    statistics.Remove(e);
            }
        }
        
        void SaveSpliterResultRToolStripMenuItemClick(object sender, EventArgs e)// 按下SaveSpliterResult
        {
            string resultFullPath = Path.GetDirectoryName(fileFullPath) + "\\" + "SpliterResult_" + Path.GetFileName(fileFullPath);
            FileStream fs = null;
            StreamWriter sw = null;
            try {
                if (File.Exists(resultFullPath))
                    File.Delete(resultFullPath);
                fs = new FileStream(resultFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                sw = new StreamWriter(fs);
                sw.WriteLine("单词名\t\t次数");
                foreach (ListViewItem item in listView1.Items) {
                    sw.WriteLine("{0,-14}{1}\n", item.Text, item.SubItems[1].Text);
                }
            } catch {
                MessageBox.Show("写文件时发生了一个错误！\n请检查是否有足够的权限！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                if (sw != null)
                    sw.Close();
                if (fs != null)
                    fs.Close();
            }
        }
        
        void ListView1ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //TODO:排序
//            if(e.Column == 0)
//            {
//                MessageBox.Show("hello");
//            }
//            if(e.Column == 1){
//            for (int i = 0; i < listView1.Items.Count - 1; i++)
//                for (int j = 0; j < listView1.Items.Count; j++) {
//                if (int.Parse(listView1.Items[i].SubItems[1].Text) < int.Parse(listView1.Items[j].SubItems[1].Text)) {
//                        var t = listView1.Items[i];
//                        listView1.Items[i] = listView1.Items[j];
//                        listView1.Items[j] = t;
//                    }
//                }
//            }
        }
        
        void ContextMenuStrip1Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
//            if(sender.Equals(listView1))
                contextMenuStrip1.Close();
        }
        void CopyToolStripMenuItemClick(object sender, EventArgs e)
        {
            
        }
        void 排除ToolStripMenuItemClick(object sender, EventArgs e)
        {
            
        }
    }
}