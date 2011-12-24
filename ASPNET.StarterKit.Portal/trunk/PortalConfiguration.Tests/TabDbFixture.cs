using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ASPNETPortal
{
    [TestFixture]
    public class TabDbFixture : BaseFixture<ITabDb>
    {
        [Test]
        public void GetTabs()
        {
            //act
            IEnumerable<TabItem> tabs = Db.GetTabs();
            //assert
            Assert.AreEqual(tabs.Count(), 6);
        }

        [Test]
        public void GetMobileTabs()
        {
            //act
            IEnumerable<TabItem> tabs = Db.GetMobileTabs();
            //assert
            Assert.AreEqual(tabs.Count(), 4);
        }

        [Test]
        public void GetSingleTabByTabId()
        {
            //act
            TabItem tab = Db.GetSingleTabByTabId(1);
            //assert
            Assert.AreEqual(tab.TabName, "Home");
            Assert.AreEqual(tab.MobileTabName, "Home");
            Assert.AreEqual(tab.AccessRoles, "All Users;");
            Assert.AreEqual(tab.TabOrder, 1);
            Assert.AreEqual(tab.ShowMobile, true);
        }

        [Test]
        public void AddTab()
        {
            //act
            int tabId = Db.AddTab(0, "sometab", 2);
            //
            TabItem tab = Db.GetSingleTabByTabId(tabId);
            Assert.AreEqual(tab.TabName, "sometab");
            Assert.AreEqual(tab.MobileTabName, "");
            Assert.AreEqual(tab.AccessRoles, "All Users;");
            Assert.AreEqual(tab.TabOrder, 2);
            Assert.AreEqual(tab.ShowMobile, true);
        }

        [Test]            
        public void UpdateTab()
        {
            //act
            Db.UpdateTab(0, 1, "sometab",2,"Guest;","sometab",false);
            //
            TabItem tab = Db.GetSingleTabByTabId(1);
            Assert.AreEqual(tab.TabName, "sometab");
            Assert.AreEqual(tab.MobileTabName, "sometab");
            Assert.AreEqual(tab.AccessRoles, "Guest;");
            Assert.AreEqual(tab.TabOrder, 2);
            Assert.AreEqual(tab.ShowMobile, false);
        }

        [Test]       
        public void UpdateTabOrder()
        {
            //act
            Db.UpdateTabOrder(1, 20);
            //
            TabItem tab = Db.GetSingleTabByTabId(1);
            Assert.AreEqual(tab.TabOrder, 20);
        }

        [Test]        
        void DeleteTab()
        {
            //act
            Db.DeleteTab(1);
            //assert
            IEnumerable<TabItem> tabs = Db.GetTabs();
            Assert.AreEqual(tabs.Count(), 5);
        }
    }
}