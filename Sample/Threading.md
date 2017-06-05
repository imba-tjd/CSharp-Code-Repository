System.Threading
===

1. 方法

```csharp
// 创建线程
Thread thread1 = new Thread(new ThreadStart(Method1));

// 启动线程
thread1.Start();

// 销毁线程
if (thread1.IsAlive == true)
    thread1.Abort();

// 挂起线程
if (thread1.ThreadState = ThreadState.Running)
    thread1.Suspend();

// 恢复线程
if (thread1.ThreadState = ThreadState.Suspended)
    thread1.Resume();

// 休眠线程
Thread.Sleep();

// 阻塞线程
thread1.Join();
```

2. 公共属性

|属性名|说明|
:-:|:-:
ApartmentState|获取或设置线程的单元状态
CurrentContext|获取线程正在执行的当前上下文
CurrentCulture|获取或设置线程的区域性
CurrentPrinccipal|获取或设置线程当前负责人
CurrentThread|获取当前正在运行的线程
CurrentUICulture|获取或设置资源管理器使用的当前区域性
IsAlive|已启动且尚未终止为true
IsBackGround|是否为后台线程
IsThreasPoolThread|判断是否是线程池地址
Name|获取或设置线程名称
Priority|获取或设置线程调度优先级，枚举类型：ThreadPriority
ThreadState|获取线程状态

3. 委托

* public delegate void ThreadStart();
* public delegate void ParameterizedThreadStart(object obj);

4. 互斥

```csharp
lock(this){...};

Object obj = new object();
...
Monitor.Enter(obj);
try{...} catch{...} finally {Monitor.Exit(obj);}

Mutex mut = new Mutex();
...
mut.WaitOne();
try{...} catch{...} finally {mut.ReleaseMutex();}

ReaderWriterLock rwl = new ReaderWriterLock（）；
...
rwl.AcquireWriterLock();
try{...} catch{...} finally {rewl.ReleaseWriterLock();}

rwl.AcquireReaderLock();
try{...} catch{...} finally {rewl.ReleaseReaderLock();}
```

5. 安全调用

```csharp
// 跨线程访问对象，不安全
// CheckForIllegalCrossThreadCalls = false;

// 安全的方式
delegate void SetTextDelegate(string text);

private void SetText(string text)
{
    if(this.textBox1.InvokeRequired)
    {
        SetTextDelegate std = new SetTextDelegate(SetText);
        this.Invoke(std, new object[] {text})
    } else {
        this.textBox1.Text = text;
    }
}
```
