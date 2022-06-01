using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace TimeCapture
{
    public partial class Site : System.Web.UI.MasterPage
    {
        //public static string UserGroup;

        public string UserGroup;


        protected void Page_Load(object sender, EventArgs e)
        {

            //currentPage = currentPage.Trim('\\');


            IPrincipal Username = HttpContext.Current.User;
            if (Username != null)
            {
            
                string query = "Select * from Users where Username='" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);

                foreach (UsersEntity CurrentUser in ThisUser)
                {
                    lblUsername.Text = CurrentUser.DisplayName.ToUpper().TrimEnd(' ');
                    UserGroup = CurrentUser.Group.ToLower().TrimEnd(' ');
                }
            }
        }



        protected void btnChangePasswordCancel_Click(object sender, EventArgs e)
        {
            mpeChangePassword.Hide();
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                IPrincipal Username = HttpContext.Current.User;
                if (Username != null)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    //parameters.Add(new SqlParameter("@password", Encrypt(tbNewPassword.Text)));
                    parameters.Add(new SqlParameter("@password", tbNewPassword.Text));
                    parameters.Add(new SqlParameter("@username", Username.Identity.Name.ToString().TrimEnd(' ')));
                    DataAccessLayer.Instance.ExecuteQuery(@"Update users set password=@password where username=@username", parameters.ToArray());
                }

                mpeChangePassword.Hide();
            }
            
        }


        public string Encrypt(string plainText)
        {
            if (plainText == null) throw new ArgumentNullException("plainText");

            //encrypt data
            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }




        protected void btnPassChange_Click(object sender, EventArgs e)
        {
            mpeChangePassword.Show();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("logon.aspx", true);
        }

        
    }
}