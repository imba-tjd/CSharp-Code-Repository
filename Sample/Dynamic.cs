// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604
/* 不能用来调用扩展方法。需用强制类型转换或者静态方法调用。
委托与动态类型间不能做隐式转换。必须指定委托类型：dynamic rightlambda = (Func<int,int>)(x => x+1);
不能用于基类声明、类型参数的约束、所实现接口的一部分。
*/
using System;
using System.Dynamic;

namespace _18._1
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic expand = new ExpandoObject();
            // 动态绑定成员
            expand.Name = "Learning Hard";
            expand.Age = 24;
            expand.Addmethod = (Func<int, int>)(x => x + 1);
            // 调用类型成员
            Console.WriteLine("expand类型的姓名为：" + expand.Name + " 年龄为： " + expand.Age);
            Console.WriteLine("调用expand类型的动态绑定的方法：" + expand.Addmethod(5));
            Console.Read();
        }
    }
}

namespace _18._2
{
    class DynamicType : DynamicObject
    {
        // 重写TryXXX方法，该方法表示对对象的动态调用
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Console.WriteLine(binder.Name + " 方法正在被调用");
            result = null;
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Console.WriteLine(binder.Name + " 属性被设置，" + "设置的值为： " + value);
            return true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            dynamic dynamicobj = new DynamicType();
            dynamicobj.CallMethod();
            dynamicobj.Name = "Learning Hard";
            dynamicobj.Age = "24";
            Console.Read();
        }
    }
}

// 由于Dynamic类型是在运行过程中动态创建对象的，所以在对该类型的每个成员进行访问时，都会首先调用GetMetaObject方法来获得动态对象，然后再通过这个动态对象完成调用。
// 为了实现IDynamicMetaObjectProvider接口，需要实现GetMetaObject方法，用于返回DynamicMetaObject对象
namespace _18._3
{
    public class DynamicType2 : IDynamicMetaObjectProvider
    {
        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            Console.WriteLine("开始获得元数据......");
            return new Metadynamic(parameter, this);
        }
    }

    // 自定义Metadynamic类
    public class Metadynamic : DynamicMetaObject
    {
        internal Metadynamic(Expression expression, DynamicType2 value)
            : base(expression, BindingRestrictions.Empty, value)
        {
        }
        // 重写响应成员调用方法
        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        {
            // 获得真正的对象
            DynamicType2 target = (DynamicType2)base.Value;
            Expression self = Expression.Convert(base.Expression, typeof(DynamicType2));
            var restrictions = BindingRestrictions.GetInstanceRestriction(self, target);
            // 输出绑定方法名
            Console.WriteLine(binder.Name + " 方法被调用了");
            return new DynamicMetaObject(self, restrictions);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            dynamic dynamicobj2 = new DynamicType2();
            dynamicobj2.Call();
            Console.Read();
        }
    }
}
