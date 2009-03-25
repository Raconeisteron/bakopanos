
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
            var p = new ProductBO();

            p.ProductID = 1;
            p.ProductName = "test";

            //can rollback
            Post.Cast<ProductBO, IEditableObject>(p).BeginEdit();
            p.ProductName = "test2";
            
            Post.Cast<ProductBO, IEditableObject>(p).CancelEdit();
            Assert.AreEqual(p.ProductName, "test");
            
        }

        /// <summary>
        /// test if your editable object can commit a change
        /// </summary>
        [Test]
        public void CanCommitTest()
        {
            var p = new ProductBO();

            p.ProductName = "test";           

            //can save
            Post.Cast<ProductBO, IEditableObject>(p).BeginEdit();
            p.ProductName = "test1";
            Post.Cast<ProductBO, IEditableObject>(p).EndEdit();
            Assert.AreEqual(p.ProductName, "test1");

        }
    }
}