using System;
using System.Collections.Generic;
using System.Text;

namespace GenericStackDelegate
{
    delegate void StackEventHandler<T, U>(T sender, U eventArgs);

    class Stack<T>
    {
        public class StackEventArgs : System.EventArgs { }
        public event StackEventHandler<Stack<T>, StackEventArgs> stackEvent;

        protected virtual void OnStackChanged(StackEventArgs a)
        {
            stackEvent(this, a);
        }
    }

    class SampleClass
    {
        public void HandleStackChange<T>(Stack<T> stack, Stack<T>.StackEventArgs args) { }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Stack<double> s = new Stack<double>();
            SampleClass o = new SampleClass();
            s.stackEvent += o.HandleStackChange;
        }
    }
}
