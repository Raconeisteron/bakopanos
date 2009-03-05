<%@ Page Language="c#" AutoEventWireup="true" %>

<%--

   The Default.aspx page simply tests the browser type and redirects either to
   the DesktopDefault or MobileDefault pages, depending on the device type.

--%>

<script runat="server">

    void Page_Load(object sender, EventArgs e)
    {
        if (Request.Browser["IsMobileDevice"] == "true")
        {
            Response.Redirect("MobileDefault.aspx");
        }
        else
        {
            Response.Redirect("DesktopDefault.aspx");
        }
    }

</script>
