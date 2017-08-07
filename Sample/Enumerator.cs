// Illustrated C# 2012

using System;
using System.Collections;
using System.Collections.Generic;

namespace Enumerator
{
    class ColorEnumerator : IEnumerator<string>
    {
        string[] _colors;
        int _position = -1;

        public ColorEnumerator(string[] theColors) => _colors = theColors.Clone() as string[];

        string this[int index] => _colors[index];

        public string Current
        {
            get
            {
                if (_position == -1 || _position >= _colors.Length)
                    throw new InvalidOperationException();
                return this[_position];
            }
        }

        object IEnumerator.Current => Current as object;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (_position < _colors.Length - 1)
            {
                _position++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }

    class Spectrum : IEnumerable<string>
    {
        string[] colors = { "violet", "blue", "cyan", "green", "yellw", "orange", "red" };

        public IEnumerator<string> GetEnumerator()
        {
            return new ColorEnumerator(colors);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ColorEnumerator(colors);

        }
    }
}
