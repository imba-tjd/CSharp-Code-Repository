// 《Illustrated C# 2012 (4th Edition)》
using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserDrawItem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 所有项均为手动绘制且大小可以不同
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
        }

        const float FONT_SIZE = 16f;

        // 改变item的时候（包括初始化）才会触发？
        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            label2.Text = (int.Parse(label2.Text) + 1).ToString();
            // 取出项中的文本
            string itemText = (sender as ListBox).Items[e.Index] as string;
            // 创建相应的字体
            using (Font font = new Font(itemText, FONT_SIZE))
            {
                // 计算出要绘制的文本的大小（宽度和高度）
                SizeF size = e.Graphics.MeasureString(itemText, font);
                // 设置项的高度
                e.ItemHeight = Convert.ToInt32(size.Height);
            }
        }

        // 初始化触发5次，第一次点触发3次，之后每次改变触发4次？
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            label1.Text = (int.Parse(label1.Text) + 1).ToString();
            string itemText = (sender as ListBox).Items[e.Index] as string;
            using (Font font = new Font(itemText, FONT_SIZE))
            {
                // 创建用于设置文本格式的对象
                StringFormat sf = new StringFormat();
                // 文本在水平方向上左对齐，垂直方向上居中对齐。（默认就是这样？）
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                // 绘制默认背景
                e.DrawBackground();
                // 绘制文本
                // 或if(e.State.HasFlag(DrawItemState.Selected))
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    // 当前项被选中
                    e.Graphics.DrawString(itemText, font, SystemBrushes.HighlightText, e.Bounds, sf);
                }
                else
                {
                    // 当前项未被选中
                    e.Graphics.DrawString(itemText, font, SystemBrushes.ControlText, e.Bounds, sf);
                }
                sf.Dispose();
            }
        }
    }
}

