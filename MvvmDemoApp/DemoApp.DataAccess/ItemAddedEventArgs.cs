using System;
using DemoApp.Model;

namespace DemoApp.DataAccess
{    
    /// <summary>
    /// Event arguments used by Repository's ItemAdded event.
    /// </summary>
    public class ItemAddedEventArgs<T> : EventArgs
        where T:class 
    {
        public ItemAddedEventArgs(T newItem)
        {
            NewItem = newItem;
        }

        public T NewItem { get; private set; }
    }
}