/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/4/8
 * 时间: 13:07
 *
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace 拼图
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 开局ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 初级ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 中级ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 高级ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 自定义ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripMenuItem 游戏ToolStripMenuItem;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		internal void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.开局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.初级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.中级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.高级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.自定义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			//
			// menuStrip1
			//
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.游戏ToolStripMenuItem,
			this.关于ToolStripMenuItem,
			this.toolStripTextBox1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(340, 34);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			//
			// 游戏ToolStripMenuItem
			//
			this.游戏ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.开局ToolStripMenuItem,
			this.toolStripMenuItem1,
			this.初级ToolStripMenuItem,
			this.中级ToolStripMenuItem,
			this.高级ToolStripMenuItem,
			this.自定义ToolStripMenuItem,
			this.toolStripMenuItem2,
			this.退出ToolStripMenuItem});
			this.游戏ToolStripMenuItem.Name = "游戏ToolStripMenuItem";
			this.游戏ToolStripMenuItem.Size = new System.Drawing.Size(88, 30);
			this.游戏ToolStripMenuItem.Text = "游戏 (&G)";
			//
			// 开局ToolStripMenuItem
			//
			this.开局ToolStripMenuItem.Name = "开局ToolStripMenuItem";
			this.开局ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.开局ToolStripMenuItem.Size = new System.Drawing.Size(160, 30);
			this.开局ToolStripMenuItem.Text = "开局";
			this.开局ToolStripMenuItem.Click += new System.EventHandler(this.开局ToolStripMenuItemClick);
			//
			// toolStripMenuItem1
			//
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
			//
			// 初级ToolStripMenuItem
			//
			this.初级ToolStripMenuItem.Checked = true;
			this.初级ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.初级ToolStripMenuItem.Name = "初级ToolStripMenuItem";
			this.初级ToolStripMenuItem.Size = new System.Drawing.Size(160, 30);
			this.初级ToolStripMenuItem.Text = "初级";
			this.初级ToolStripMenuItem.Click += new System.EventHandler(this.初级ToolStripMenuItemClick);
			//
			// 中级ToolStripMenuItem
			//
			this.中级ToolStripMenuItem.Name = "中级ToolStripMenuItem";
			this.中级ToolStripMenuItem.Size = new System.Drawing.Size(160, 30);
			this.中级ToolStripMenuItem.Text = "中级";
			this.中级ToolStripMenuItem.Click += new System.EventHandler(this.中级ToolStripMenuItemClick);
			//
			// 高级ToolStripMenuItem
			//
			this.高级ToolStripMenuItem.Name = "高级ToolStripMenuItem";
			this.高级ToolStripMenuItem.Size = new System.Drawing.Size(160, 30);
			this.高级ToolStripMenuItem.Text = "高级";
			this.高级ToolStripMenuItem.Click += new System.EventHandler(this.高级ToolStripMenuItemClick);
			//
			// 自定义ToolStripMenuItem
			//
			this.自定义ToolStripMenuItem.Name = "自定义ToolStripMenuItem";
			this.自定义ToolStripMenuItem.Size = new System.Drawing.Size(160, 30);
			this.自定义ToolStripMenuItem.Text = "自定义";
			this.自定义ToolStripMenuItem.Click += new System.EventHandler(this.自定义ToolStripMenuItemClick);
			//
			// toolStripMenuItem2
			//
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
			//
			// 退出ToolStripMenuItem
			//
			this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
			this.退出ToolStripMenuItem.Size = new System.Drawing.Size(160, 30);
			this.退出ToolStripMenuItem.Text = "退出";
			this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItemClick);
			//
			// 关于ToolStripMenuItem
			//
			this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
			this.关于ToolStripMenuItem.Size = new System.Drawing.Size(88, 30);
			this.关于ToolStripMenuItem.Text = "关于 (&A)";
			this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItemClick);
			//
			// toolStripTextBox1
			//
			this.toolStripTextBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripTextBox1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 30);
			this.toolStripTextBox1.Text = "尚未开始";
			this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			// panel1
			//
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 34);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(340, 291);
			this.panel1.TabIndex = 1;
			//
			// timer1
			//
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			//
			// MainForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(340, 325);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "拼图";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyDown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
