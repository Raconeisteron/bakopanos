using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PeoplePage : Page
{
    ///<summary>
    /// Hides rows that have 'invisible' = false
    /// Also hides the image if none is defined
    ///</summary>
    protected void GridViewAllNews_RowCreated(object source, GridViewRowEventArgs e)
    {
        if (e.Row == null) return;

        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.DataItem != null)
        {
            var visible = (bool) DataBinder.Eval(e.Row.DataItem, "Visible");
            e.Row.Visible = visible;
        }

        // display image if a url is specifid
        var imageUrl = (string) DataBinder.Eval(e.Row.DataItem, "ImageUrl");
        if (String.Empty.Equals(imageUrl))
        {
            var newsImage = (Image) e.Row.FindControl("Image2");
            if (newsImage != null)
                newsImage.Visible = false;
        }
    }
}