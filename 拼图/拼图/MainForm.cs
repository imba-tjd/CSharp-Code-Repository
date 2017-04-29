/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/4/8
 * 时间: 13:07
 *
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 拼图
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm :Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			this.InitializeComponent();
			this.InitializeOthers();//进行别的初始化工作

			//
			//
			//
		}

		void 退出ToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		void 关于ToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("By : IMBA-TJD\nLast Update Time：2017/4/20","关于");
		}

		void 初级ToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Reset();//清除panel，和做一些其它工作
			this.初级ToolStripMenuItem.Checked = true;
			BuildBlock(3,3);//生成方块
		}

		void  中级ToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Reset();
			this.中级ToolStripMenuItem.Checked = true;
			BuildBlock(4,4);
		}

		void 高级ToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Reset();
			this.高级ToolStripMenuItem.Checked = true;
			BuildBlock(5,5);
		}

		void 自定义ToolStripMenuItemClick(object sender, EventArgs e)
		{
			Custom custom = new Custom();//Custom类写在另一个文件里
			custom.ShowDialog();
			if(custom.DialogResult == DialogResult.OK){
			this.Reset();
			BuildBlock(custom.getRow,custom.getColumn);
			this.自定义ToolStripMenuItem.Checked = true;
			}
			custom.Dispose();
		}

		void 开局ToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.panel1.Enabled = true;
			RandomBlock();//随机打乱方块
			this.timer1.Enabled = true;
			time = 0;
		}

		void Timer1Tick(object sender, EventArgs e)
		{
			this.toolStripTextBox1.Text = (++time).ToString();
//			this.toolStripTextBox1.Text = (int.Parse(this.toolStripTextBox1.Text) + 1).ToString();
//			this.toolStripTextBox1.Text = Convert.ToInt32(this.toolStripTextBox1.Text) + 1;
		}

		void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
				Application.Exit();
		}
	}
}
