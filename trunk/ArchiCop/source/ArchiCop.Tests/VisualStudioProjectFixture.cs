﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchiCop.Library;
using NUnit.Framework;
using System.Xml.Linq;
using System.Reflection;

namespace ArchiCop
{
    [TestFixture]
    public class VisualStudioProjectFixture
    {
        [Test]
        public void CanDumpAndCanLoadProjectList()
        {
            string path = @"..\..\..";
            
            var list1 = VsProjectHandler.DumpVisualStudioProjectList(path, "test.xml");
            var list2 = VsProjectHandler.LoadVisualStudioProjectList("test.xml");

            Assert.IsTrue(list1.Count() == list2.Count());
            Console.WriteLine("{0} projects dumped!", list1.Count());
        }
        [Test]
        public void CanLoadProjectFile()
        {
            string path = @"..\..\ArchiCop.Tests.csproj";

            VsProject project = new VsProject(path);

            foreach (PropertyInfo item in DataMapper.GetSourceProperties(typeof(VsProject)))
            {
                Console.WriteLine("{0}: {1}", item.Name, DataMapper.GetPropertyValue(project, item.Name));
            }
        }

    }
}
