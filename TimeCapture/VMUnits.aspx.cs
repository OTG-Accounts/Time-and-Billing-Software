using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeCapture
{
    public partial class VMUnits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);
        }
    }
}