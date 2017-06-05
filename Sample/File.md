文件操作
===

1. File and FileInfo
* AppendText 创建一个StreamWriter类型，用于向文件追加文本
* Create 创建或覆盖文件
* Delete
* Open
* OpenRead
* OpenWrite 打开或创建
* ReadAllText 读取所有行，然后关闭文件
* WtrteAllText

2. Directory and DirectoryInfo
* CreateDirectory 创建目录和子目录
* Delete
* Exists
* GetFiles 获得目录下所有文件名称的数组
* GetDirectories
* GetFileSystemEntries 返回指定目录中所有文件和子目录的名称。
* GetParent 获取指定目录的父目录
* GetCurrentDirectory 获取应用程序的当前工作目录
* Move
* GetCreationTime

3. Path（仅仅是对**路径字符串**操作，并不真正的修改文件，一般返回修改后的string. path 指定的文件或目录不需要存在。 例如，如果 c:\temp\newdir 是当前目录，则对文件名（例如 test.txt）调用 GetFullPath 将返回 c:\temp\newdir\test.txt。 该文件不需要存在。）
* Combine()
* GetDirectoryName()
* GetFileName()
* GetExtension()
* GetFullPath()
* GetPathRoot()
* ChangeExtension(string path,string extension)


4. Stream(abstract)
* CanRead
* CanSeek
* CanWrite
* Length
* Position 获取或设置当前流中的位置
* BeginRead
* BeginWrite
* Close
* EndRead
* EndWrite
* Flush
* Write

* 派生类
- NetworkStream
+ NetworkStream nwkStream = new NetworkStream(socket);
+ NetworkStream不支持随机访问，Can.Seek属性总为假
- FileStream
- MemoryStream
- GZipStream

* 读写器
- TextReader/TextWtiter
- StringReader/StringWriter
- BinaryReader/BinaryWriter
+ ReadBoolean()/ReadBytes()/ReadChar()……; 返回一个指定类型的数据
- StreamReader/StreamWriter
+ int Read() 从流中读取一个字符，并使读字符位置移动到下一个字符，返回代表读取字符ASCII字符值的int类型整数，-1表示没有字符可以读取。
+ string ReadLine();
