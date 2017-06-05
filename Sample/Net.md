Net类
===

1. 定义主机对象
* IPAddress ip = IPAddress.Parse("127.0.0.1");
* IPEndPoint ipe = new IPEndPont(ip,8888);

2. 获取主机信息
* IPAddress[] ip = Dns.GetHostAddresses("www.cctv.com");
* string hostname = Dns.GetHostName();

## Socket套接字
命名空间：System.Net.Socket

1. 构造函数
* Socket(AddressFamily af,SocketType st,ProtocolType pt);

AddressFamily.InterNetwork：IPV4

* 套接字组合

SocketType|ProtocolType|说明
:-:|:-:|:-:
Dgram|Udp|无连接的通信
Stream|Tcp|面向连接的同学
Raw|Icmp|Internet控制报文协议
Raw|Raw|简单IP包通信

2. 常用方法
* Accept() 为链接创建新的Socket
* Receive() 接收来自绑定的Socket的数据
* ReceiveFrom() 接收数据报并存储源、终结点
* BeginAccept() 异步，接收一个传入的链接尝试
* BeginConnect() 异步，对远程主机的连接请求
* BeginReceive() 异步，从连接的Socket中异步接受数据
* BeginReceiveFrom() 异步，从指定网络设备中接受数据
* BeginSend() 异步，将数据发送到连接的Socket
* Send()
* Listen() 使Socket置于侦听状态
* Bind() 使Socket与一个本地终结点（IPEndPoint）相关联
* Close() 关闭Socket连接并释放所有关联的资源
* Connect() 建立与远程主机的连接
* Shutdown() 禁用Socket发送和接收

3. 属性
* Available 获取已经从网络接收且可供读取的数据量
* Blocking 指示Socket是否处于阻止模式
* Connected 指示Socket是在上次Send还是Receive操作时连接到主机
* LocalEndPoint 获取本地终结点
* RemoteEndPonint 获取远程终结点
* SendBufferSize 获取或设置Socket发送缓冲区的大小

## 编程流程

```csharp
// 含跨线程访问控件
void MyConnect()
{
    // 服务端
    ipe = new IPEndPont(IPAddress.Any,65535);
    socket = new Socket(AddressFamily.InterNetwork,SocketType.stream,ProtocolType.Tcp);
    socket.Bind(ipe);
    socket.Listen(0); // 0表示连接数量不限
    tAcceptMsg = new Thread(new ThreadStart(this.AcceptMessage));
    tAcceptMsg.Start();

    //客户端
    ipe = new IPEndPont(IPAddress.Parse(textBox1.Text),int.Parse(textBox2.Text));
    socket = new Socket(AddressFamily.InterNetwork,SocketType.stream,ProtocolType.Tcp);
    socket.Connect(ipe);
    ...
}

void AcceptMessage()
{
    clientSocket = socket.Accept();
    if(clientSocket != null)
    {
        bConnected = true;
    }
    nStream = new NetworkStream(clientSocket);
    tReader = new StreamReader(nStream);
    wReader = new StreamWriter(nStream);
    while(bConnected) // 标记客户机与服务器之间的连接状态，不清楚为什么不用Connected属性，也不清楚可以不可以
    {

        try{...} catch {
            tAcceptMsg.Abort();
        }
    }
    clientSocket.Shutdown(SocketShutdown.Both);
    clientSocket.Close();
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}

void SendMessage()
{
    if(bConnected)
    {
        try{
            lock(this)
            {
                wReader.WriteLine(richTextBox1.Text); // 将数据写入缓冲区
                wReader.Flush();
                richTextBox1.Text = "";
                richTextBox1.Focus();
            } catch {...}
        }
    } else {...}
}

private void Form1_FormClosing()
{
    try{
        socket.Close();
        tAcceptMsg.Abort();
    } catch {}
}
```
