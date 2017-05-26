/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/5/20
 * 时间: 19:46
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace 词频统计
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newNToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem toolsTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spliterSToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem saveSpliterResultRToolStripMenu;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem 过滤连词ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 区分大小写ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 过滤介词ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 过滤疑问词ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 实时统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 排除ToolStripMenuItem;
        
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
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spliterSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSpliterResultRToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.过滤介词ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.过滤连词ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.过滤疑问词ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.区分大小写ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.实时统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.排除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.toolsTToolStripMenuItem,
            this.aboutAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(744, 34);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newNToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openOToolStripMenuItem,
            this.saveSToolStripMenuItem,
            this.saveAsAToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitEToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(74, 28);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // newNToolStripMenuItem
            // 
            this.newNToolStripMenuItem.Name = "newNToolStripMenuItem";
            this.newNToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
            this.newNToolStripMenuItem.Text = "New(&N)";
            this.newNToolStripMenuItem.Click += new System.EventHandler(this.NewNToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
            // 
            // openOToolStripMenuItem
            // 
            this.openOToolStripMenuItem.Name = "openOToolStripMenuItem";
            this.openOToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
            this.openOToolStripMenuItem.Text = "Open(&O)";
            this.openOToolStripMenuItem.Click += new System.EventHandler(this.OpenOToolStripMenuItemClick);
            // 
            // saveSToolStripMenuItem
            // 
            this.saveSToolStripMenuItem.Enabled = false;
            this.saveSToolStripMenuItem.Name = "saveSToolStripMenuItem";
            this.saveSToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
            this.saveSToolStripMenuItem.Text = "Save(&S)";
            this.saveSToolStripMenuItem.Click += new System.EventHandler(this.SaveSToolStripMenuItemClick);
            // 
            // saveAsAToolStripMenuItem
            // 
            this.saveAsAToolStripMenuItem.Enabled = false;
            this.saveAsAToolStripMenuItem.Name = "saveAsAToolStripMenuItem";
            this.saveAsAToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
            this.saveAsAToolStripMenuItem.Text = "Save As(&A)...";
            this.saveAsAToolStripMenuItem.Click += new System.EventHandler(this.SaveAsAToolStripMenuItemClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
            // 
            // exitEToolStripMenuItem
            // 
            this.exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            this.exitEToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
            this.exitEToolStripMenuItem.Text = "Exit(&E)";
            this.exitEToolStripMenuItem.Click += new System.EventHandler(this.ExitEToolStripMenuItemClick);
            // 
            // toolsTToolStripMenuItem
            // 
            this.toolsTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spliterSToolStripMenuItem,
            this.saveSpliterResultRToolStripMenu,
            this.toolStripMenuItem3,
            this.过滤介词ToolStripMenuItem,
            this.过滤连词ToolStripMenuItem,
            this.过滤疑问词ToolStripMenuItem,
            this.区分大小写ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.实时统计ToolStripMenuItem});
            this.toolsTToolStripMenuItem.Name = "toolsTToolStripMenuItem";
            this.toolsTToolStripMenuItem.Size = new System.Drawing.Size(89, 28);
            this.toolsTToolStripMenuItem.Text = "Tools(&T)";
            // 
            // spliterSToolStripMenuItem
            // 
            this.spliterSToolStripMenuItem.Enabled = false;
            this.spliterSToolStripMenuItem.Name = "spliterSToolStripMenuItem";
            this.spliterSToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.spliterSToolStripMenuItem.Size = new System.Drawing.Size(265, 30);
            this.spliterSToolStripMenuItem.Text = "Spliter(&S)";
            this.spliterSToolStripMenuItem.Click += new System.EventHandler(this.SpliterSToolStripMenuItemClick);
            // 
            // saveSpliterResultRToolStripMenu
            // 
            this.saveSpliterResultRToolStripMenu.Enabled = false;
            this.saveSpliterResultRToolStripMenu.Name = "saveSpliterResultRToolStripMenu";
            this.saveSpliterResultRToolStripMenu.Size = new System.Drawing.Size(265, 30);
            this.saveSpliterResultRToolStripMenu.Text = "SaveSpliterResult(&R)";
            this.saveSpliterResultRToolStripMenu.Click += new System.EventHandler(this.SaveSpliterResultRToolStripMenuItemClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(262, 6);
            // 
            // 过滤介词ToolStripMenuItem
            // 
            this.过滤介词ToolStripMenuItem.Checked = true;
            this.过滤介词ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.过滤介词ToolStripMenuItem.Name = "过滤介词ToolStripMenuItem";
            this.过滤介词ToolStripMenuItem.Size = new System.Drawing.Size(265, 30);
            this.过滤介词ToolStripMenuItem.Text = "过滤介词";
            this.过滤介词ToolStripMenuItem.Click += new System.EventHandler(this.过滤介词ToolStripMenuItemClick);
            // 
            // 过滤连词ToolStripMenuItem
            // 
            this.过滤连词ToolStripMenuItem.Checked = true;
            this.过滤连词ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.过滤连词ToolStripMenuItem.Name = "过滤连词ToolStripMenuItem";
            this.过滤连词ToolStripMenuItem.Size = new System.Drawing.Size(265, 30);
            this.过滤连词ToolStripMenuItem.Text = "过滤连词";
            this.过滤连词ToolStripMenuItem.Click += new System.EventHandler(this.过滤连词ToolStripMenuItemClick);
            // 
            // 过滤疑问词ToolStripMenuItem
            // 
            this.过滤疑问词ToolStripMenuItem.Name = "过滤疑问词ToolStripMenuItem";
            this.过滤疑问词ToolStripMenuItem.Size = new System.Drawing.Size(265, 30);
            this.过滤疑问词ToolStripMenuItem.Text = "过滤疑问词";
            this.过滤疑问词ToolStripMenuItem.Click += new System.EventHandler(this.过滤疑问词ToolStripMenuItemClick);
            // 
            // 区分大小写ToolStripMenuItem
            // 
            this.区分大小写ToolStripMenuItem.Name = "区分大小写ToolStripMenuItem";
            this.区分大小写ToolStripMenuItem.Size = new System.Drawing.Size(265, 30);
            this.区分大小写ToolStripMenuItem.Text = "区分大小写";
            this.区分大小写ToolStripMenuItem.Click += new System.EventHandler(this.区分大小写ToolStripMenuItemClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(262, 6);
            // 
            // 实时统计ToolStripMenuItem
            // 
            this.实时统计ToolStripMenuItem.Name = "实时统计ToolStripMenuItem";
            this.实时统计ToolStripMenuItem.Size = new System.Drawing.Size(265, 30);
            this.实时统计ToolStripMenuItem.Text = "实时统计";
            this.实时统计ToolStripMenuItem.Click += new System.EventHandler(this.实时统计ToolStripMenuItemClick);
            // 
            // aboutAToolStripMenuItem
            // 
            this.aboutAToolStripMenuItem.Name = "aboutAToolStripMenuItem";
            this.aboutAToolStripMenuItem.Size = new System.Drawing.Size(101, 28);
            this.aboutAToolStripMenuItem.Text = "About(&A)";
            this.aboutAToolStripMenuItem.Click += new System.EventHandler(this.AboutAToolStripMenuItemClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 17, 0);
            this.statusStrip1.Size = new System.Drawing.Size(744, 29);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(82, 24);
            this.toolStripStatusLabel1.Text = "总字数：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(21, 24);
            this.toolStripStatusLabel2.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(82, 24);
            this.toolStripStatusLabel3.Text = "总词数：";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(21, 24);
            this.toolStripStatusLabel4.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(100, 24);
            this.toolStripStatusLabel5.Text = "文件路径：";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(136, 24);
            this.toolStripStatusLabel6.Text = "尚未打开文件！";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 392);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(744, 392);
            this.splitContainer1.SplitterDistance = 514;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(514, 392);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox1DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox1DragEnter);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(225, 392);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Word";
            this.columnHeader1.Width = 96;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Frequency";
            this.columnHeader2.Width = 124;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.排除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(212, 97);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
            // 
            // 排除ToolStripMenuItem
            // 
            this.排除ToolStripMenuItem.Name = "排除ToolStripMenuItem";
            this.排除ToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.排除ToolStripMenuItem.Text = "排除";
            this.排除ToolStripMenuItem.Click += new System.EventHandler(this.排除ToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 455);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "词频统计";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFormDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFormDragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
