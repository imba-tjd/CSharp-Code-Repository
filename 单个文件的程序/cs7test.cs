using static System.Console;

class Test
{
    static void Main() { }

    object o = 1;
    void F0()
    {
        // normal usage
        if (o is int i)
            WriteLine(i);
    }

    void F1()
    {
        if (o is int i)
            WriteLine(i);
        else
            WriteLine(i); // Use of unassigned local variable 'i'
        WriteLine(i); // the same as above
    }

    void F2()
    {
        int i;
        if (o is int)
        {
            i = (int)o; // just for simulation because 'as' can't unbox
            WriteLine(i);
        }
        else
            WriteLine(i); // Use of unassigned local variable 'i'
        WriteLine(i); // Use of unassigned local variable 'i'
    }

    void F3()
    {
        if (!(o is int i))
            // WriteLine(i); // Use of unassigned local variable 'i'
            // Console.WriteLine("Hi.");
            i = 1;
        else
            WriteLine(i); // compile
        WriteLine(i);
    }

    void F4()
    {
        // _ = (!(o is int i));
        // Console.WriteLine(i); // Use of unassigned local variable 'i'

        _ = (o is int i);
        WriteLine(i); // Use of unassigned local variable 'i'
    }
}
