using System;
using System.Collections;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // TabSettings Class
    //
    // Class that encapsulates the detailed settings for a specific Tab 
    // in the Portal
    //
    //*********************************************************************

    public class TabSettings
    {
        public String AuthorizedRoles;
        public String MobileTabName;
        public ArrayList Modules = new ArrayList();
        public bool ShowMobile;
        public int TabId;
        public int TabIndex;
        public String TabName;
        public int TabOrder;
    }
}