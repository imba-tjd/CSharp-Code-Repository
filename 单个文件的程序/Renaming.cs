// https://zhidao.baidu.com/question/2122516412305974027
using System;
using System.IO;

public class Renaming
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("本程序可以将指定文件夹下的所有jpg重命名为“文件夹名+序号.jpg”。");
            Console.WriteLine("允许一次输入多个文件夹，以空格分隔。路径最好用引号括起来。")
            Console.Write("请输入文件夹路径：");
            args = Console.ReadLine().Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);
        }

        int count = 0;
        foreach (string dirPath in args)
        {
            if (!Directory.Exists(dirPath))
            {
                Console.WriteLine("路径：" + dirPath + " 不存在或不是文件夹，忽略。");
                continue;
            }

            int index = 0;
            foreach (string file in Directory.GetFiles(dirPath))
            {
                string extension = Path.GetExtension(file);

                if (extension != ".jpg" && extension != ".jpeg")
                    continue;

                File.Move(file,
                    Path.GetFullPath(dirPath) + "\\" +// 获取文件夹的绝对路径
                    Path.GetFileName(Path.GetFullPath(dirPath)) //获取文件夹名
                    + ++index + extension);
                count++;
            }
        }
        Console.WriteLine("共调整{0}个文件。", count);
    }
}
