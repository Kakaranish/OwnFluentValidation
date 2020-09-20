using System;
using System.Collections.Generic;
using System.Text;

namespace CustomValidation.Misc
{
    public class TestClass
    {
        public bool Foo(string objStr, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            Console.WriteLine();
            return true;
        }

        public bool Foo<TObject>(TObject obj)
        {

            return true;
        }
    }

    public class Entrypoint
    {
        public static void Main()
        {
            var testObj = new TestClass();
            testObj.Foo(123);
            testObj.Foo("");
        }

    }
}
