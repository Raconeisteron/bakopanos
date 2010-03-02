using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchiCop.Library;
using NUnit.Framework;
using System.Reflection;

namespace ArchiCop
{
    [TestFixture]
    public class DataMapperFixture
    {
        [Test]
        public void CanGetPropertyValueTest()
        {
            //arange
            var obj = new TestObject
            {
                TestString = "Costas",
                TestInt = 1,
                TestBool = true
            };
            //act
            string someString = DataMapper.GetPropertyValue(obj, "TestString") as string;
            int someInt = (int)DataMapper.GetPropertyValue(obj, "TestInt");
            bool someBool = (bool)DataMapper.GetPropertyValue(obj, "TestBool");
            //assert
            Assert.IsTrue(obj.TestString == "Costas");
            Assert.IsTrue(obj.TestInt == 1);
            Assert.IsTrue(obj.TestBool == true);
        }

        [Test]
        public void CanSetPropertyValueTest()
        {
            //arange
            var obj = new TestObject{};
            //act
            DataMapper.SetPropertyValue(obj, "TestString", "Costas");
            DataMapper.SetPropertyValue(obj, "TestInt", 1);
            DataMapper.SetPropertyValue(obj, "TestBool", true);
            //assert
            Assert.IsTrue(obj.TestString == "Costas");
            Assert.IsTrue(obj.TestInt == 1);
            Assert.IsTrue(obj.TestBool == true);
        }

        [Test]
        public void CanGetSourcePropertiesTest()
        {
            //arange
            var obj = new TestObject
            {
                TestString = "Costas",
                TestInt = 1,
                TestBool = true
            };
            //act
            PropertyInfo[] props = DataMapper.GetSourceProperties(typeof(TestObject));            
            //assert
            Assert.IsTrue(props.Count() == 3);
            Assert.IsTrue(props[0].Name == "TestString");
            Assert.IsTrue(props[1].Name == "TestInt");
            Assert.IsTrue(props[2].Name == "TestBool");               
        }

        public class TestObject
        {
            public string TestString { get; set; }
            public int TestInt { get; set; }
            public bool TestBool { get; set; }
        }
    }
}
