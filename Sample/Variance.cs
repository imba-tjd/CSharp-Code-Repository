// Illustrated C# 2012
// 如果派生类只是用于输出值，那么这种结构化的委托有效性之间的常数关系叫做协变
// 在期望传入基类时允许传入派生类对象的特性叫做逆变
// 显式变化使用in和out关键字，适用于委托和接口，不适用于类、结构和方法
// 不包括in和out关键字的委托和接口类型参数叫不变

namespace Variance
{
    class Animal { }
    class Dog : Animal { }

    delegate T Factory<out T>(); // 协变
    delegate void Action<in T>(T a); // 逆变

    interface IMyIfc<out T> { }
    class SimpleClass<T> : IMyIfc<T> { } // 接口的协变

    class Program
    {
        static Dog MakeDog() => new Dog(); // 符合Factory委托的方法
        static void ActOnAnimal(Animal a) { } // 符合Action委托的方法
        static void DoSomething(IMyIfc<Animal> a) { }

        static void Main(string[] args)
        {
            Factory<Dog> dogMaker = MakeDog;
            Factory<Animal> animalMaker = dogMaker; // 协变
            animalMaker();

            Action<Animal> act = ActOnAnimal;
            Action<Dog> dog = act; // 逆变
            dog(new Dog());
            act(new Dog());
            act(new Animal());

            SimpleClass<Dog> doge = new SimpleClass<Dog>();
            IMyIfc<Animal> animal = doge;
            DoSomething(doge);
        }
    }
}
