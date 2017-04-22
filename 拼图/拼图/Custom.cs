/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/4/13
 * 时间: 22:06
 *
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace 拼图
{
    /// <summary>
    /// Description of Custom.
    /// </summary>
    public partial class Custom : Form
    {
        public Custom()//自定义窗口
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private int Row, Column;

        private void TextBoxTextChanged(object sender, EventArgs e)//两文本框都已绑定该事件
        {
//			if(textBox1.Text != "" && textBox2.Text != "")
//			{
//				Row = Convert.ToInt32(textBox1.Text);
//				Column = Convert.ToInt32(textBox2.Text);
//				label4.Text = (Row*Column).ToString();
//			}

            TextBox box = (TextBox)sender;
            if (box.Text != "")//允许文本框为空
			try {//防止输入非数字
                    Convert.ToInt32(box.Text);
//				if(box.Text == "" || Convert.ToInt32(box.Text))
//					;
//				else {MessageBox.Show("请输入正数！");}
                } catch {
                    MessageBox.Show("只能输入正数！");
                    box.Text = "";
                }

            if (textBox1.Text == "" || textBox2.Text == "")
                label4.Text = "";
            else {
                Row = Convert.ToInt32(textBox1.Text);
                Column = Convert.ToInt32(textBox2.Text);
                label4.Text = (Row * Column).ToString();//两文本框都不为空时实时计算方块数量
            }
        }

        void Button2Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Button1Click(object sender, EventArgs e)
        {
            if (Row <= 1 || Column <= 1) {//只有一列或者一行无法进行游戏
                MessageBox.Show("你告诉我怎么玩╭(￣▽￣)╮", "别闹~");
            } else {
                this.Visible = false;
                this.DialogResult = DialogResult.OK;
            }
        }

        internal int getRow {
            get { return Row; }
        }
        internal int getColumn {
            get { return Column; }
        }
    }
}
