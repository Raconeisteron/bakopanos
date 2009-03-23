
using System.ComponentModel;
using NUnit.Framework;
using PostSharp;

namespace Evolutil.Domain.Postsharp
{
    /// <summary>
    /// tests an editable object 
    /// </summary>
    [TestFixture]
    public class EditableObjectFixture
    {
        /// <summary>
        /// test if your editable object can rollback changes
        /// </summary>
        [Test]
        public void CanRollbackTest()
        {
            var cust = new Customer();

            cust.FirstName = "test";
            cust.LastName = "test";

            //can rollback
            Post.Cast<Customer, IEditableObject>(cust).BeginEdit();
            cust.FirstName = "test2";
            cust.LastName = "test20";
            Post.Cast<Customer, IEditableObject>(cust).CancelEdit();
            Assert.AreEqual(cust.FirstName, "test");
            Assert.AreEqual(cust.LastName, "test");

        }

        /// <summary>
        /// test if your editable object can commit a change
        /// </summary>
        [Test]
        public void CanCommitTest()
        {
            var cust = new Customer();

            cust.FirstName = "test";           

            //can save
            Post.Cast<Customer, IEditableObject>(cust).BeginEdit();
            cust.FirstName = "test1";
            Post.Cast<Customer, IEditableObject>(cust).EndEdit();
            Assert.AreEqual(cust.FirstName, "test1");

        }

        [EditableObjectAspect]
        class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}