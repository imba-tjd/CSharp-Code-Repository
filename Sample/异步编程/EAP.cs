﻿// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604
/* 实现了EAP的类都具有以Async为后缀的异步方法和对应的Completed事件，并且这些类至此异步方法的取消和进度报告。
属性：
CancellationPending，用来指示应用程序是否已请求取消后台操作
IsBusy，指示异步操作是否正在运行
WorkReportsProgress，指示BackgroundWorker能否报告进度
WorkerSupportsCancellation，指示BackgroundWorker是否支持异步取消操作
方法：
CancelAsync，请求取消异步操作
ReportProgress，用于引发ProgressChanged事件
RunWorkAsync，调用后开始执行异步操作
事件：
DoWork，调用RunWorkerAsync时触发的事件
ProgressChanged，调用ReportProgress时触发的事件，程序会在该事件中进行进度报告的更新
RunWorkerCompleted，当异步操作已完成、被取消或引发异常时触发
*/
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace _20._4
{
    public partial class Form1 : Form
    {
        public int DownloadSize = 0;
        public string downloadPath = null;
        long totalSize = 0;
        const int BufferSize = 2048;
        byte[] BufferRead = new byte[BufferSize];
        FileStream filestream = null;
        HttpWebResponse myWebResponse = null;

        public Form1()
        {
            InitializeComponent();
            string url = "http://download.microsoft.com/download/7/0/3/703455ee-a747-4cc8-bd3e-98a615c3aedb/dotNetFx35setup.exe";

            txbUrl.Text = url;
            this.btnPause.Enabled = false;

            GetTotalSize();
            downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + Path.GetFileName(this.txbUrl.Text.Trim());
            if (File.Exists(downloadPath))
            {
                FileInfo fileInfo = new FileInfo(downloadPath);
                DownloadSize = (int)fileInfo.Length;
                progressBar1.Value = (int)((float)DownloadSize / (float)totalSize * 100);
            }

            // 使BackgroundWorker类支持ReportProgress和Cancellation操作
            bgWorkerFileDownload.WorkerReportsProgress = true;
            bgWorkerFileDownload.WorkerSupportsCancellation = true;
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (bgWorkerFileDownload.IsBusy != true)
            {
                bgWorkerFileDownload.RunWorkerAsync();

                filestream = new FileStream(downloadPath, FileMode.OpenOrCreate);

                filestream.Seek(DownloadSize, SeekOrigin.Begin);
                this.btnDownLoad.Enabled = false;
                this.btnPause.Enabled = true;
            }
            else
            {
                MessageBox.Show("正在执行操作，请稍后");
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (bgWorkerFileDownload.IsBusy && bgWorkerFileDownload.WorkerSupportsCancellation == true)
            {
                bgWorkerFileDownload.CancelAsync(); // 取消下载操作
            }
        }

        private void bgWorkerFileDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgworker = sender as BackgroundWorker;
            try
            {
                // Do the DownLoad operation
                // Initialize an HttpWebRequest object
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(txbUrl.Text.Trim());

                // If the part of the file have been downloaded,
                // The server should start sending data from the DownloadSize to the end of the data in the HTTP entity.
                if (DownloadSize != 0)
                {
                    myHttpWebRequest.AddRange(DownloadSize);
                }

                // assign HttpWebRequest instance to its request field.
                myWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream responseStream = myWebResponse.GetResponseStream();
                int readSize = 0;
                while (true)
                {
                    if (bgworker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    readSize = responseStream.Read(BufferRead, 0, BufferRead.Length);
                    if (readSize > 0)
                    {
                        DownloadSize += readSize;
                        int percentComplete = (int)((float)DownloadSize / (float)totalSize * 100);
                        filestream.Write(BufferRead, 0, readSize);

                        // 报告进度，引发ProgressChanged事件的发生
                        bgworker.ReportProgress(percentComplete);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void bgWorkerFileDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void bgWorkerFileDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                myWebResponse.Close();
            }
            else if (e.Cancelled)
            {
                MessageBox.Show(String.Format("下载暂停，下载的文件地址为：{0}\n 已经下载的字节数为: {1}字节", downloadPath, DownloadSize));
                myWebResponse.Close();
                filestream.Close();

                this.btnDownLoad.Enabled = true;
                this.btnPause.Enabled = false;
            }
            else
            {
                MessageBox.Show(String.Format("下载已完成，下载的文件地址为：{0}，文件的总字节数为: {1}字节", downloadPath, totalSize));

                this.btnDownLoad.Enabled = false;
                this.btnPause.Enabled = false;
                myWebResponse.Close();
                filestream.Close();
            }
        }

        private void GetTotalSize() // 获得文件总大小
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(txbUrl.Text.Trim());
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            totalSize = response.ContentLength;
            response.Close();
        }
    }
}
