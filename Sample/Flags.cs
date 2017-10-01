using System;

class Program
{
    [Flags]
    enum CardDeckSettings : Byte
    {
        SingleDeck = 0x01,
        LargePictures = 0x02,
        FancyNumbers = 0x04,
        Animation = 0x08
    }

    static void Main()
    {
        CardDeckSettings ops =  CardDeckSettings.SingleDeck |
                                CardDeckSettings.FancyNumbers |
                                CardDeckSettings.Animation;

        bool useFancyNumbers = ops.HasFlag(CardDeckSettings.FancyNumbers);
        bool useAnimation = (ops & CardDeckSettings.Animation) == CardDeckSettings.Animation;

        CardDeckSettings testFlags = CardDeckSettings.Animation | CardDeckSettings.FancyNumbers;
        bool useAnimationAndFancyNumbers = ops.HasFlag(testFlags);

        Console.WriteLine(ops); // 如果枚举不使用FlagsAttribute，这一行将会输出13
    }
}
