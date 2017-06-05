GDI+
===

1. 命名空间
* System.Drawing
- Graphics
- Bitmap
- Brush
- Font
- Image
- Pen
- Color
* System.Drawing.Drawing2D
- 渐变画笔
- Matrix
- GraphicsPath
* System.Drawing.Imaging
* System.Drawing.Text

2. Graphics类
* Paint事件内：Graphics g = e.Graphics;
* Graphics g = this.CreateGraphics(); 在已存在的窗体或控件上绘图
* Graphics.FromImage()静态方法

Bitmap bitmap = new Bitmap(@"...");
Graphics g = Graphics.FromImage(bitmap)

Image img = Image.FromFile(...);
Graphics g = Graphics.FromImage(img);

g.Dispose();

3. 坐标结构
Point p = new Point(10,20);
Size s = new Size(30,40);
Rectangle rct = new Rectangle(p,s);
Rectangle rct = new Rectangle(10,20,30,40);

4. 笔和画刷
Brush是个抽象类。可从画刷对象实例化笔。
* SolidBrush
* HatchBrush 提供了绘制时要使用的图案
* TextureBrush 使用纹理（如图像）进行绘制
* LineGradientBrush 使用沿渐变混合的两种颜色进行绘制
* PathGradientBrush 使用复杂的混合色渐变进行绘制

5. 绘制（非静态方法）
Point[] points = {new Point(15,20),new Point(30,120),new Point(100,180),new Point(260,50)};

* 直线
DrawLine(Pen pen,Point startPoint,Point endPoint);
DrawLine(Pen pen,int x1,int y1,int x2,int y2);
DrawLines(pen,points);

* 多边形
DrawPolygon(Pen pen,Point[] points); // 空心
FillPolygon(Brush brush,Point[] points); // 实心

* 曲线
DrawCurve(Pen pen,Point[] points); // 默认弯曲强度0.5
DrawCurve(Pen pen,Point[] points,float tension); // 指定弯曲强度，取值范围0.0~1.0f

* 矩形
DrawRectangle(Pen pen,Rectangle rect);
DrawRectangle(Pen pen,int x,int y,int width,int height);
DrawRectangle(Pen pen,Rectangle[] rects);
FillRectangle方法类似

* 椭圆和圆
DrawEllipse(Pen pen,Rectangle rect);
DrawEllipse(Pen pen,int x,int y,int width,int height);
FillEllipse方法类似

* 圆弧
DrawArc(Pen pen,Rectangle rect,int startAngle,int sweepAngle);
DrawArc(Pen pen,int x,int y,int width,int height,int startAngle,int sweepAngle);

* 扇形
DrawPie(Pen pen,Rectangle rect);
DrawPie(Pen pen,int x,int y,int width,int height);
FillPie方法类似

* 字体：略

* 文本
DrawString(string s,Font font,Brush brush,PointF point,StringFormat format)

6. 操作图像像素点
* 直接法：
Color GetPixel(int x, inty);
void SetPixel(int x, int y, Color color);

* 内存法
Rectangle rect = new Tectangle(0, 0, crtBitmap.Width, crtBitmap.Height); // 获取被处理图像的大小
System.Drawing.Imaging.BitmapData bmpData = crtBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, crtBitmap.PixelFormat); //将被处理的图像数据锁存
IntPtr ptr = bmpData.Scan(); // 获取第一个像素的地址
int bytes = crtBitmap.Width * crtBitmap.Height * 3; //计算该位图的字节总数
byte[] rgbValues = new byte[bytes]; // 创建保存图像数据的字节数组
System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes); // 将被锁存的图像数据复制到数组中
for(int i = 0; i < rgbValues.Length; i += 3) // 处理像素
{
    rgbValues[i] = 0; // Blue
    rgbValues[i+1] = 0; // Green
}
System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes); // 将数组数据复制回位图中
crtBitmap.UnlockBits(bmpData); // 解除被处理图像数据的锁存

* 指针法
Rectangle rect = new Tectangle(0, 0, crtBitmap.Width, crtBitmap.Height);
System.Drawing.Imaging.BitmapData bmpData = crtBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, crtBitmap.PixelFormat);
unsafe
{
    byte* ptr = (byte*)(bmpData.Scan0);
    for(int i = 0; i < bmpData.Height; i++)
    {
        for(int j = 0; j < bmpData.Width; j++)
        {
            ptr[0] = 0;
            ptr[1] = 0;
            ptr += 3; // 指向下一个像素
        }
        ptr += bmpData.Stride - bmpData.Width * 3; // 指向下一行的首字节，“3”表示24位图。BitmapData对象会自动将图像每行字节扩展为4的倍数
    }
}
crtBitmap.UnlockBits(bmpData);

