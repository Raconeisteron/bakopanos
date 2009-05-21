using System;

namespace ForEachIteratoren
{
    interface IAction
    {
        void Action();
    }

    static class Program
    {
        public static void ForEachI<T>(this T[] array) where T : IAction
        {
            foreach (T elem in array)
            {
                elem.Action();
            }
        }

        public class MyString : IAction
        {
            readonly string _s;
            public MyString(string s) { _s = s; }

            public void Action()
            {
                Console.WriteLine("I'm a string with {0}", _s);
            }
        }

        public delegate void Action<T>(T obj);

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            if (action != null)
                foreach (T elem in array)
                {
                    action(elem);
                }
        }
        //leave private 
        static void StringAction(this string s)
        {
            Console.WriteLine("a String {0}", s);
        }
        //leave private 
        static void IntAction(this int i)
        {
            Console.WriteLine("an Integer {0}", i);
        }

        static void Main()
        {
            var arr0 = new MyString[2];
            arr0[0] = new MyString("Hallo");
            arr0[1] = new MyString("value");
            arr0.ForEachI();

            var arr1 = new [] { "Hello", "World" };
            arr1.ForEach(StringAction);
            var arr2 = new [] { 2, 6, 3, 9 };
            arr2.ForEach(IntAction);
            arr2.ForEach(IntAction); //Delegate inference
            arr2.ForEach(i => Console.WriteLine("lambda: {0}", i));

            Console.ReadLine();
        }
    }
}
