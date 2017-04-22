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
	    private static int time;
		private void InitializeOthers()
		{
			Block.setPanel(ref this.panel1);//破坏了对象的思想，不过算了。
			Block.setTimer(ref this.timer1);
			Block.BuildBlock(3,3);//刚载入窗体，建立3*3方格
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
//			flag = 0;
//			time = 0;
		}

		internal static int getTime()
		{
			return time;
		}
	}

	internal class Block :Button
	{
		private static Block[,] block;
		private const int SQUARESIDE = 88;//方块大小
		private static Panel Panel1;
		private static Timer Timer1;
		private static int Row, Column;//输入的总行列数
//		private int[] index;
		private int index;//一维标记
		private static int flag;//判断是否为模拟行为

		internal static void setPanel(ref Panel panel){Panel1 = panel;}
		internal static void setTimer(ref Timer timer){Timer1 = timer;}

		private Block(int row, int column)//私有构造函数
		{
			index = row*Column + column;
			Size = new System.Drawing.Size(SQUARESIDE, SQUARESIDE);
			Location = new System.Drawing.Point(column * SQUARESIDE +3, row * SQUARESIDE + 3);
			Font = new System.Drawing.Font("Microsoft YaHei UI", 18);
			Text = (index +1).ToString();
			Click += new System.EventHandler(BlockClick);
		}

		internal static void BuildBlock(int row, int column)//公共静态方法创建方块对象
		{
			Row = row;
			Column = column;
			block = new Block[Row,Column];
			for (int i = 0; i < Row; i++)
				for (int j = 0; j < Column; j++)
				{
					block[i,j] = new Block(i,j);//调用私有构造函数
                    Panel1.Controls.Add(block[i,j]);
//					block[i,j].Font = new System.Drawing.Font("Microsoft YaHei UI", 18);
//					block[i,j].Text = (i*column + j +1).ToString();
//					block[i,j].Click += new System.EventHandler(block[i,j].BlockClick);
				}
//			block[row-1,column-1].Text = "";
			block[row-1,column-1].Visible = false;//其实空方块是有数字的，只是不可见
		}

		internal static void RandomBlock()//模拟点击方块
		{
			Random rand = new Random();
			int index, row, column;
			/*
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
                    block[row,column].BlockClick(block[row,column],null);
                }
			flag = 1;
		}

		private static void ExchangeBlock(Block a, Block b)//交换任意两个方块（的文字）
		{
			string temp;
			temp = a.Text;
			a.Text = b.Text;
			b.Text = temp;
			if(a.Text == (Row*Column).ToString()){//如果其中一个方块为不可见方块，则设置它交换后为不可见
				b.Visible = true;
				a.Visible = false;
			} else if(b.Text == (Row*Column).ToString()){
				a.Visible = true;
				b.Visible = false;
			}//必须先设置ture再设置false，因为不可见方块和自己交换后应为不可见
		}

		private void BlockClick(object sender, EventArgs e)
		{
			Block clickedButton = (Block)sender;
			Block none = DefineNone();//确定空方块
			if(none.index == clickedButton.index+1 && none.index/Column == clickedButton.index/Column
			   || none.index == clickedButton.index-1 && none.index/Column == clickedButton.index/Column
			   || none.index == clickedButton.index+Column
			   || none.index == clickedButton.index-Column
			  )//空方块是否在被点击方块的四周
				ExchangeBlock(clickedButton, none);
			if(IsSucceed() && flag == 1)//若为模拟点击，则不会成功
			{
				Timer1.Enabled = false;
				MessageBox.Show("成功！\n你一共用时：" + MainForm.getTime() + "秒。", "succeed!");
			}
		}

		private static Block DefineNone()//确定空方块
		{
			foreach (Block none in block)
				if(none.Text == (Row*Column).ToString())
					return none;
			return null;
		}

		private static bool IsSucceed()
		{
			int t = 1;
			foreach (Block e in block) {
				if(e.Text != t.ToString())
					return false;
				t++;
			}
			return true;
		}
	}
}
