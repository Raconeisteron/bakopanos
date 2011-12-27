using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ASPNETPortal
{
    [TestFixture]
    public class ModuleDbFixture : BaseFixture<IModuleDb>
    {
        [Test]
        public void GetModulesByTabId()
        {
            //act
            IEnumerable<ModuleItem> modules = Db.GetModulesByTabId(1);
            //assert
            Assert.AreEqual(modules.Count(), 6);
        }

        [Test]
        public void UpdateModuleOrder()
        {

        }

        [Test]
        public void AddModule()
        {

        }

        [Test]
        public void UpdateModule()
        {

        }

        [Test]
        public void DeleteModule()
        {
            //arrange
            ModuleItem moduleBefore = Db.GetModulesByTabId(1).First();
            //act
            Db.DeleteModule(moduleBefore.ModuleId);
            //assert
            ModuleItem moduleAfter = Db.GetModulesByTabId(1).First();
            Assert.AreNotEqual(moduleBefore.ModuleId, moduleAfter.ModuleId);
        }

        [Test]
        public void UpdateModuleSetting()
        {

        }

        [Test]
        public void GetModuleSettings()
        {

        }
    }
}