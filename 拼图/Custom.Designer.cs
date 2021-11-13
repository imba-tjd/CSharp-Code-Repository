/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/4/13
 * 时间: 22:06
 *
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
namespace 拼图
{
	partial class Custom
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

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
		/// </summary>
		private void InitializeComponent()
		{
		    this.label1 = new System.Windows.Forms.Label();
		    this.label2 = new System.Windows.Forms.Label();
		    this.label3 = new System.Windows.Forms.Label();
		    this.textBox1 = new System.Windows.Forms.TextBox();
		    this.textBox2 = new System.Windows.Forms.TextBox();
		    this.label4 = new System.Windows.Forms.Label();
		    this.button1 = new System.Windows.Forms.Button();
		    this.button2 = new System.Windows.Forms.Button();
		    this.SuspendLayout();
		    // 
		    // label1
		    // 
		    this.label1.Location = new System.Drawing.Point(47, 15);
		    this.label1.Name = "label1";
		    this.label1.Size = new System.Drawing.Size(100, 23);
		    this.label1.TabIndex = 0;
		    this.label1.Text = "行数：";
		    // 
		    // label2
		    // 
		    this.label2.Location = new System.Drawing.Point(47, 61);
		    this.label2.Name = "label2";
		    this.label2.Size = new System.Drawing.Size(100, 23);
		    this.label2.TabIndex = 0;
		    this.label2.Text = "列数：";
		    // 
		    // label3
		    // 
		    this.label3.Location = new System.Drawing.Point(47, 101);
		    this.label3.Name = "label3";
		    this.label3.Size = new System.Drawing.Size(100, 23);
		    this.label3.TabIndex = 0;
		    this.label3.Text = "总块数：";
		    // 
		    // textBox1
		    // 
		    this.textBox1.Location = new System.Drawing.Point(192, 12);
		    this.textBox1.Name = "textBox1";
		    this.textBox1.Size = new System.Drawing.Size(100, 30);
		    this.textBox1.TabIndex = 1;
		    this.textBox1.TextChanged += new System.EventHandler(this.TextBoxTextChanged);
		    // 
		    // textBox2
		    // 
		    this.textBox2.Location = new System.Drawing.Point(192, 58);
		    this.textBox2.Name = "textBox2";
		    this.textBox2.Size = new System.Drawing.Size(100, 30);
		    this.textBox2.TabIndex = 2;
		    this.textBox2.TextChanged += new System.EventHandler(this.TextBoxTextChanged);
		    // 
		    // label4
		    // 
		    this.label4.Location = new System.Drawing.Point(192, 101);
		    this.label4.Name = "label4";
		    this.label4.Size = new System.Drawing.Size(100, 23);
		    this.label4.TabIndex = 0;
		    // 
		    // button1
		    // 
		    this.button1.Location = new System.Drawing.Point(58, 144);
		    this.button1.Name = "button1";
		    this.button1.Size = new System.Drawing.Size(88, 33);
		    this.button1.TabIndex = 3;
		    this.button1.Text = "确定";
		    this.button1.UseVisualStyleBackColor = true;
		    this.button1.Click += new System.EventHandler(this.Button1Click);
		    // 
		    // button2
		    // 
		    this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		    this.button2.Location = new System.Drawing.Point(204, 144);
		    this.button2.Name = "button2";
		    this.button2.Size = new System.Drawing.Size(88, 33);
		    this.button2.TabIndex = 4;
		    this.button2.Text = "取消";
		    this.button2.UseVisualStyleBackColor = true;
		    this.button2.Click += new System.EventHandler(this.Button2Click);
		    // 
		    // Custom
		    // 
		    this.AcceptButton = this.button1;
		    this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
		    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		    this.ClientSize = new System.Drawing.Size(362, 200);
		    this.Controls.Add(this.button2);
		    this.Controls.Add(this.button1);
		    this.Controls.Add(this.textBox2);
		    this.Controls.Add(this.textBox1);
		    this.Controls.Add(this.label4);
		    this.Controls.Add(this.label3);
		    this.Controls.Add(this.label2);
		    this.Controls.Add(this.label1);
		    this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
		    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		    this.Margin = new System.Windows.Forms.Padding(4);
		    this.Name = "Custom";
		    this.Opacity = 0.9D;
		    this.ShowInTaskbar = false;
		    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		    this.Text = "自定义大小";
		    this.ResumeLayout(false);
		    this.PerformLayout();

		}
	}
}
