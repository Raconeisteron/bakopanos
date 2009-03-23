using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evolutil.Domain;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;

namespace Evolutil.WebForms
{
    public partial class _Default : BasePage<_Default>
    {
        [Dependency]
        public IProductService service { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (ProductBO p in service.Read())
            {
                Response.Write(p.ProductName);
                Response.Write("<br/>");
            }
        }
    }
}
