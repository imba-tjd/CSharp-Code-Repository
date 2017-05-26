// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604
/* APM（Asynchronous Programming Model）：实现了返回类型为IAsyncResult接口的Beginxxx方法和Endxxx方法的类。
委托类型定义了BeginInvoke方法和EndInvoke方法，所以委托类型都实现了APM。

APM给出了四种方式来访问异步操作所得到的结果。
1. 在调用Beginxxx方法的线程上调用Endxxx方法来得到异步操作的结果。Endxxx会堵塞线程。
IAsyncResult result = myHttpWebRequest.BeginGetResponse(null, null);
myWebResponse = (HttpWebResponse)myHttpWebRequest.EndGetResponse(result);
2. 在调用Beginxxx方法的线程上查询IAsyncResult的AsyncWaitHandle属性，从而得到WaitHandle对象，接着调用该对象的WaitOne方法来堵塞线程并等待操作完成，最后调用Endxxx方法来返回结果。
3. 在调用Beginxxx方法的线程上循环查询IAsyncResult的IsComplete属性，操作完成后再调用Endxx方法来返回结果
4. 使用AsyncCallback委托来指定操作完成时要调用的方法，在回调方法中调用Endxxx方法来获得异步操作返回的结果。
*/
using System;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Forms;

namespace _20._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txbUrl.Text = "http://download.microsoft.com/download/7/0/3/703455ee-a747-4cc8-bd3e-98a615c3aedb/dotNetFx35setup.exe";
        }

        // 定义用来实现异步编程的委托
        private delegate string AsyncMethodCaller(string fileurl);

        SynchronizationContext sc;
        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            rtbState.Text = "下载中.....";
            btnDownLoad.Enabled = false; // 把按钮设置不可用
            if (txbUrl.Text == String.Empty)
            {
                MessageBox.Show("请先输入下载地址！");
                return;
            }
            sc = SynchronizationContext.Current; // 获得调用线程的同步上下文对象
            AsyncMethodCaller methodCaller = new AsyncMethodCaller(DownLoadFileAsync);
            methodCaller.BeginInvoke(txbUrl.Text.Trim(), GetResult, null);

        }

        public string DownLoadFileAsync(string url)
        {
            int BufferSize = 2048;
            byte[] BufferRead = new byte[BufferSize];
            string savepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\dotNetFx35setup.exe";
            FileStream filestream = null;
            HttpWebResponse myWebResponse = null;
            if (File.Exists(savepath))
            {
                File.Delete(savepath);
            }

            filestream = new FileStream(savepath, FileMode.OpenOrCreate);
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                if (myHttpWebRequest != null)
                {
                    myWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    Stream responseStream = myWebResponse.GetResponseStream();
                    int readSize = responseStream.Read(BufferRead, 0, BufferSize);
                    while (readSize > 0)
                    {
                        filestream.Write(BufferRead, 0, readSize);
                        readSize = responseStream.Read(BufferRead, 0, BufferSize);
                    }
                }
                // 执行该方法的线程是线程池线程，该线程不是与创建richTextBox控件的线程不是一个线程
                return string.Format("文件下载完成，文件大小为:{0}, 下载路径为：{1}", filestream.Length, savepath);
            }
            catch (Exception e)
            {
                return string.Format ("下载过程中发生异常，异常信息为: {0}" ,e.Message);
            }
            finally
            {
                if (myWebResponse != null)
                {
                    myWebResponse.Close();
                }
                if (filestream != null)
                {
                    filestream.Close();
                }
            }
        }

        // 异步操作完成时执行的方法
        private void GetResult(IAsyncResult result)
        {
            AsyncMethodCaller caller = (AsyncMethodCaller)((AsyncResult)result).AsyncDelegate;

            // 调用EndInvoke去等待异步调用完成并且获得返回值
            // 如果异步调用尚未完成，则 EndInvoke 会一直阻止调用线程，直到异步调用完成
            string returnstring = caller.EndInvoke(result);

            // 通过获得GUI线程的同步上下文的派生对象，
            // 然后调用Post方法来使更新GUI操作方法由GUI 线程去执行
            sc.Post(ShowState, returnstring);
        }

        // 显示结果到richTextBox
        // 因为该方法是由GUI线程执行的，所以当然就可以访问窗体控件了
        private void ShowState(object result)
        {
            rtbState.Text = result.ToString();
            btnDownLoad.Enabled = true;
        }
    }
}
