
using System;
using System.ComponentModel;
using NUnit.Framework;
using PostSharp;

namespace Evolutil.Domain.Postsharp
{
    /// <summary>
    /// Tests the PropertyChangedAspect
    /// </summary>
    [TestFixture]
    public class NotifyPropertyChangedFixture
    {
        /// <summary>
        /// Tests the NotifyPropertyChanged for correctly firing the PropertyChanged event
        /// </summary>
        [Test]
        public void NotifyPropertyChangedTest()
        {
            var p = new ProductBO();

            // Get the INotifyPropertyChanged interface for the new customer.
            // Note the post-compilation cast.           
            //this is due to the fact that the Customer type do not directly derive from INotifyPropertyChanged
            //the following statement is equal to the traditional
            //INotifyPropertyChanged observable = (INotifyPropertyChanged)customer;
            INotifyPropertyChanged observable = Post.Cast<ProductBO, INotifyPropertyChanged>(p);

            // Now that we have INotifyPropertyChanged, we can register our PropertyChanged event handler.
            //We register an event handler for the PropertyChanged event, 
            //so we will be notified when a property will change.
            bool flag = false;
            observable.PropertyChanged += ((sender, e) => flag = true);

            Assert.IsFalse(flag);//should be false

            //We initialize the customer name to 'Sylvestre' 
            p.ProductName = "Sylvestre";

            Assert.IsTrue(flag);

            Console.WriteLine( p.ProductName );
        }
      
    }
}