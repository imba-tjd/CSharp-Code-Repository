/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/4/8
 * 时间: 13:07
 *
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace 拼图
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

	}

	public partial class MainForm//使用partial类
	{
	    private int time;
        private int flag;//判断是否为模拟行为
        internal static int Row, Column;//输入的总行列数
        private Block[,] blocks;
        internal int getRow{get{return Row;}}
        internal int getColumn{get{return Column;}}
        
		private void InitializeOthers()
		{
			BuildBlock(3,3);//刚载入窗体，建立3*3方格
			this.panel1.Enabled = false;
		}

		private void Reset()//做一些清理工作
		{
			this.初级ToolStripMenuItem.Checked = false;
			this.中级ToolStripMenuItem.Checked = false;
			this.高级ToolStripMenuItem.Checked = false;
			this.自定义ToolStripMenuItem.Checked = false;
			this.panel1.Controls.Clear();
			this.panel1.Enabled = false;
			this.toolStripTextBox1.Text = "尚未开始";
			this.timer1.Enabled = false;
		}

        /*
        private bool isSucceed()
        {
            int t = 1;
            foreach (Block e in blocks) {
                if(e.Text != t.ToString())
                    if (blocks[Row-1,Column-1].Text != "") {
                        return false;
                }
                    return false;
                t++;
            }
            return true;
        }*/
        
        private bool isSucceed()
        {
            bool flag=true;
            for(int i=0;i<Row*Column-1;i++)
                if(blocks[i/Column,i%Column].Text != (i+1).ToString())
                    flag=false;
            if(flag==true)
                if(blocks[Row-1,Column-1].Text == "")
                    flag=true;
            return flag;
        }
        
        private void BlockClick(object sender, EventArgs e)
        {
            Block clickedButton = (Block)sender;
            Block empty = Block.getEmptyBlock(blocks);//确定空方块
            if(empty.index == clickedButton.index+1 && empty.index/Column == clickedButton.index/Column
               || empty.index == clickedButton.index-1 && empty.index/Column == clickedButton.index/Column
               || empty.index == clickedButton.index+Column
               || empty.index == clickedButton.index-Column
              )//空方块是否在被点击方块的四周
                Block.ExchangeBlock(clickedButton, empty);
            if(isSucceed() && flag == 1)//若为模拟点击，则不会成功
            {
                timer1.Enabled = false;
                MessageBox.Show("成功！\n你一共用时：" + time + "秒。", "succeed!");
            }
        }
        
        internal void RandomBlock()//模拟点击方块
        {
            Random rand = new Random();
            int index, row, column;
            /*直接交换，会产生无法还原的情况。
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Column; j++)
                {
                    index = rand.Next(0,Row * Column);
                    row = index / Column;
                    column = index % Column;
                    ExchangeBlock(block[row,column], block[i,j]);
                }
            */
            flag = 0;//标记是否为模拟行为
            for (int i = 0; i < 666; i++){
                    index = rand.Next(0,Row * Column);
                    row = index / Column;
                    column = index % Column;
                    BlockClick(blocks[row,column],null);
                }
            flag = 1;
        }
        
        internal void BuildBlock(int row, int column)
        {
            Row = row;
            Column = column;
            blocks = new Block[Row,Column];
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Column; j++)
            {
                    panel1.Controls.Add(blocks[i,j] = Block.getBlocks(i,j));
                    blocks[i,j].Click += new System.EventHandler(BlockClick);
            }
            blocks[row-1,column-1].Text = "";
            blocks[row-1,column-1].Visible = false;
        }
	}

	internal class Block :Button
	{
		
		private const int SQUARESIDE = 88;//方块大小
        internal readonly int index;//一维标记
		
//		internal Block this[Block a,Block b]
//		{
//		    get{return block[a,b];}
//		}

		private Block(int row, int column)//私有构造函数
		{
			index = row*MainForm.Column + column;
			Size = new System.Drawing.Size(SQUARESIDE, SQUARESIDE);
			Location = new System.Drawing.Point(column * SQUARESIDE +3, row * SQUARESIDE + 3);
			Font = new System.Drawing.Font("Microsoft YaHei UI", 18);
			Text = (index +1).ToString();
		}

		internal static Block getBlocks(int row, int column)
		{
		    return new Block(row,column);
		}

		internal static void ExchangeBlock(Block a, Block b)//交换任意两个方块（的文字）
		{
			string temp;
			temp = a.Text;
			a.Text = b.Text;
			b.Text = temp;/*
			if(a.Text == ""){//如果其中一个方块为不可见方块，则设置它交换后为不可见
				b.Visible = true;
				a.Visible = false;
			} else if(b.Text == ""){
				a.Visible = true;
				b.Visible = false;
			}//必须先设置ture再设置false，因为不可见方块和自己交换后应为不可见*/
		}

		internal static Block getEmptyBlock(Block[,] blocks)//确定空方块
		{
			foreach (Block emptyBlock in blocks)
				if(emptyBlock.Text == "")
					return emptyBlock;
			return null;
		}
	}
}
