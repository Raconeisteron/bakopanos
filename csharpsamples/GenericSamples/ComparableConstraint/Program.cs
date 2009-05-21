using System;
using System.Collections.Generic;

namespace ComparableConstraint
{
    interface IPriorityQueue<TE, TP> where TP : IComparable<TP>
    {
        //
        void Put(TE element, TP priority);
        //
        TE Get();
        //
        int Count { get;}       
    }
    
    class PriorityQueue<TE, TP> : IPriorityQueue<TE, TP>
    where TP : IComparable<TP>
    {
        struct KeyValuePair<TK, TV>
        {
            public readonly TK Key;
            public readonly TV Value;

            public KeyValuePair(TK key, TV value)
            {
                Key = key;
                Value = value;
            }
        }

        readonly List<KeyValuePair<TP, TE>> _list = new List<KeyValuePair<TP, TE>>();

        public void Put(TE element, TP priority) { 
          _list.Add(new KeyValuePair<TP,TE>(priority, element));
        }

        public TE Get()
        {
            int maxInd = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Key.CompareTo(_list[maxInd].Key) > 0)
                    maxInd = i;
            }
            TE val = _list[maxInd].Value;
            _list.RemoveAt(maxInd);
            return val ;
        }

        public int Count { get { return _list.Count; }}
    }

    class Program
    {
        static void Main()
        {
            IPriorityQueue<String, int> pq1 = new PriorityQueue<String, int>();	//ok
            pq1.Put("String 1 mit Prio 7", 7);
            pq1.Put("String 2 mit Prio 4", 4);
            pq1.Put("String 3 mit Prio 1", 1);
            pq1.Put("String 4 mit Prio 5", 5);

            while (pq1.Count > 0)
            {
                string s = pq1.Get();
                Console.WriteLine(s);
            }
            Console.ReadLine();            
        }
    }
}
