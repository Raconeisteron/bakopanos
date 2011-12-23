using NUnit.Framework;

namespace ASPNETPortal
{
    [TestFixture]
    public class GlobalDbFixture:BaseFixture<IGlobalDb>
    {
        [Test]
        public void GetAndUpdate()
        {
            Db.UpdatePortalInfo(0, "test1", true);
            {
                GlobalItem item = Db.GetGlobalByPortalId(0);
                Assert.IsTrue(item.AlwaysShowEditButton);
                Assert.AreEqual(item.PortalName, "test1");
                
            }
            Db.UpdatePortalInfo(0, "test2", false);
            {
                GlobalItem item = Db.GetGlobalByPortalId(0);
                Assert.IsTrue(!item.AlwaysShowEditButton);
                Assert.AreEqual(item.PortalName, "test2");
            }
        }

    }
}