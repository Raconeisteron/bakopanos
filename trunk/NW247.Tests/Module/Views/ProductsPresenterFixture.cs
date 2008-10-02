using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using NW247.Services;
using Rhino.Mocks;

namespace NW247.Module.Views
{
    [TestFixture]
    public class ProductsPresenterFixture 
    {
        [Test]
        public void MoveFirstTest()
        {
            TestHelper.GetProductsPresenter().MoveFirst();            
        }

        [Test]
        public void MovePrev()
        {
            TestHelper.GetProductsPresenter().MovePrev();            
        }

        [Test]
        public void MoveNext()
        {
            TestHelper.GetProductsPresenter().MoveNext();            
        }

        [Test]
        public void MoveLast()
        {
            TestHelper.GetProductsPresenter().MoveLast();            
        }

        //public void Add()
        //{
        //    TestHelper.GetProductsPresenter().Add();            
        //}

        //public void Delete()
        //{
        //    TestHelper.GetProductsPresenter().Delete();            
        //}

        //public void Cancel()
        //{
        //    TestHelper.GetProductsPresenter().Cancel();            
        //}

        //public void Save()
        //{
        //    TestHelper.GetProductsPresenter().Save();            
        //}
    }
}
