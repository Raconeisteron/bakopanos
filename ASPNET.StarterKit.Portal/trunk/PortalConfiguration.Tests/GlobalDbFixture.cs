using NUnit.Framework;

namespace ASPNETPortal
{
    [TestFixture]
    public class GlobalDbFixture:BaseFixture<IGlobalDb>
    {
        [Test]
        public void GetGlobalByPortalId()
        {
            //act
            GlobalItem item = Db.GetGlobalByPortalId(0);
            //assert
            Assert.AreEqual(item.AlwaysShowEditButton, false);
            Assert.AreEqual(item.PortalName, "ASP.NET Portal Starter Kit");
        }


        [Test]
        public void UpdatePortalInfo()
        {
            //act
            Db.UpdatePortalInfo(0, "test1", true);
            //assert
            GlobalItem item = Db.GetGlobalByPortalId(0);
            Assert.AreEqual(item.AlwaysShowEditButton, true);
            Assert.AreEqual(item.PortalName, "test1");


        }

    }
}