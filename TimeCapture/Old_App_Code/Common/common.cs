using System;
using System.Security.Principal;
using System.Web;

public class Common
{
    private static Common _instance;

    public static Common Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Common();

            return _instance;
        }
    }

    public UsersEntity CurrentUser;

    public bool CheckAccess()
    {

        string currentPage = System.Web.HttpContext.Current.Request.Url.AbsolutePath;

        IPrincipal Username = HttpContext.Current.User;

        if (Username != null)
        {
            string AccessQuery = "select ua.access from UserAccess ua inner join Users u on ua.UserID=u.UserID where ua.pagename = '" + currentPage + "' and u.username='" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
            string returnedValue = DataAccessLayer.Instance.ExecuteSimpleQuery(AccessQuery);
            if (returnedValue != null)
            {
                if (!Convert.ToBoolean(returnedValue))
                    return false;
                else
                    return true;
            }
            else
                return false;
            
        }
        else
            return false;
    }

}
 

