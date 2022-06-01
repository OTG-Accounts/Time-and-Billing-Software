using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeCapture
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            
            

            if (!this.IsPostBack)
            {


                IPrincipal Username = HttpContext.Current.User;
                if (Username != null)
                {
                    string query = "Select * from Users where Username='" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
                    List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);

                    foreach (UsersEntity CurrentUser in ThisUser)
                    {
                        lblUsername.Text = CurrentUser.DisplayName.ToUpper().TrimEnd(' ');
                        
                    }
                    RefreshGrid();
                }

                string SMQuery = "SELECT * FROM  [ServiceManager].[dbo].[MT_System$Domain$User] WHERE [Domain_E36D56F2_AD60_E76E_CD5D_9F7AB51AD395]=''";

                string UsersQuery = "SELECT * FROM Users";
                List<UsersEntity> ListOfCurrentUsers = DataAccessLayer.Instance.GetEntities<UsersEntity>(UsersQuery);

                foreach (UsersEntity CurrentUser in ListOfCurrentUsers)
                {
                    lstCurrentUsers.Items.Add(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.UserID.ToString()));
                    SMQuery += " AND BaseManagedEntityId <> '" + CurrentUser.UserID.ToString() + "'";
            
                }
            

                List<SMUsersEntity> ThisSMUSer = DataAccessLayer.Instance.GetEntities<SMUsersEntity>(SMQuery);

                foreach (SMUsersEntity CurrentUser in ThisSMUSer)
                {
                    lstSMUsers.Items.Add(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.BaseManagedEntityID.ToString()));
                }    
            }
            
        }


        protected void btnChangePassword_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                IPrincipal Username = HttpContext.Current.User;
                if (Username != null)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@password", tbNewPassword.Text));
                    parameters.Add(new SqlParameter("@username", Username.Identity.Name.ToString().TrimEnd(' ')));
                    DataAccessLayer.Instance.ExecuteQuery(@"Update users set password=@password where username=@username", parameters.ToArray());
                }

                mpeChangePassword.Hide();
            }

        }


        
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("logon.aspx", true);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.BulkEdit = false;
            this.GridView1.PageIndex = e.NewPageIndex;
            RefreshGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int EntityChangeLogId = this.GridView1.GetNewValue<int>(e.RowIndex, 0);
            string Comment = this.GridView1.GetNewValue<string>(e.RowIndex, "txtcomment");
            bool Completed = this.GridView1.GetNewValue<bool>(e.RowIndex, "cbCompleted");
            DateTime EnteredDate = this.GridView1.GetNewValue<DateTime>(e.RowIndex, "txtDate");
            string StartTime = this.GridView1.GetNewValue<string>(e.RowIndex, "txtStartTime");
            bool Onsite = this.GridView1.GetNewValue<bool>(e.RowIndex, "cbOnsite");
            int TimeInMinutes = this.GridView1.GetNewValue<int>(e.RowIndex, "txtTimeInMinutes");

            string oldComment = this.GridView1.GetOldValue<string>(e.RowIndex, "txtcomment");
            bool oldCompleted = this.GridView1.GetOldValue<bool>(e.RowIndex, "cbCompleted");
            DateTime oldEnteredDate = this.GridView1.GetOldValue<DateTime>(e.RowIndex, "txtDate");
            string oldStartTime = this.GridView1.GetOldValue<string>(e.RowIndex, "txtStartTime");
            bool oldOnsite = this.GridView1.GetOldValue<bool>(e.RowIndex, "cbOnsite");
            int oldTimeInMinutes = this.GridView1.GetOldValue<int>(e.RowIndex, "txtTimeInMinutes");


            if (Comment != oldComment || Completed != oldCompleted || EnteredDate != oldEnteredDate || StartTime != oldStartTime || Onsite != oldOnsite || TimeInMinutes != oldTimeInMinutes)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@EntityChangeLogId", EntityChangeLogId));
                parameters.Add(new SqlParameter("@Comment", Comment));
                parameters.Add(new SqlParameter("@Completed", Completed));
                parameters.Add(new SqlParameter("@EnteredDate", EnteredDate));
                parameters.Add(new SqlParameter("@StartTime", StartTime));
                parameters.Add(new SqlParameter("@OnSite", Onsite));
                parameters.Add(new SqlParameter("@TimeInMinutes", TimeInMinutes));

                DataAccessLayer.Instance.ExecuteQuery(@"Update Comments set TimeInMinutes=@TimeInMinutes,OnSite=@OnSite,StartTime=@StartTime,Comment=@Comment,Completed=@Completed,EnteredDate=@EnteredDate where EntityChangeLogId=@EntityChangeLogId", parameters.ToArray());
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed=" + this.ddlCompleted.SelectedValue + "ORDER BY " + e.SortExpression + " " + GetSortDirection();
            //string query = "SELECT * FROM dbo.Entries ORDER BY " + e.SortExpression + " " + GetSortDirection();
            this.GridView1.DataSource = DataAccessLayer.Instance.GetEntities<EntriesView>(query);
            this.GridView1.DataBind();
        }

        private void RefreshGrid()
        {
            string query;
            string DisplayName = "Unknown User";



            IPrincipal Username = HttpContext.Current.User;

            string userQuery = "Select * from Users where Username='" + Username.Identity.Name.ToString() + "'";
            List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(userQuery);

            foreach (UsersEntity CurrentUser in ThisUser)
            {
                DisplayName = CurrentUser.DisplayName.TrimEnd(' ');
            }

            if (txtFromDate.Text != "")
            {
                if (txtToDate.Text != "")
                {
                    query = "SELECT * FROM dbo.Entries WHERE Completed=" + this.ddlCompleted.SelectedValue + " AND EnteredDate >= CONVERT(datetime,'" + txtFromDate.Text + "',103) AND EnteredDate <= CONVERT(datetime,'" + txtToDate.Text + "',103) AND EnteredBy='" + DisplayName + "'";
                }
                else
                {
                    query = "SELECT * FROM dbo.Entries WHERE Completed=" + this.ddlCompleted.SelectedValue + " AND EnteredDate >= CONVERT(datetime,'" + txtFromDate.Text + "',103) AND EnteredBy='" + DisplayName + "'";
                }
            }
            else
            {
                if (txtToDate.Text != "")
                {
                    query = "SELECT * FROM dbo.Entries WHERE Completed=" + this.ddlCompleted.SelectedValue + " AND EnteredDate <= CONVERT(datetime,'" + txtToDate.Text + "',103) AND EnteredBy='" + DisplayName + "'";
                }
                else
                {
                    query = "SELECT * FROM dbo.Entries WHERE Completed=" + this.ddlCompleted.SelectedValue + " AND EnteredBy='" + DisplayName + "'";
                }
            }
            
           // query = "SELECT * FROM dbo.Entries WHERE EnteredBy='" + DisplayName + "'";
            this.GridView1.DataSource = DataAccessLayer.Instance.GetEntities<EntriesView>(query);
            this.GridView1.DataBind();

        }

        private string GetSortDirection()
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;
                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }

            return GridViewSortDirection;
        }

        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "DESC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            this.GridView1.BulkEdit = true;
            RefreshGrid();


            this.GridView1.BulkUpdate();

            this.GridView1.BulkEdit = false;

            RefreshGrid();
            
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            
            RefreshGrid();
        }

        protected void ddlCompleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void btnAddUser_Click(object sender, ImageClickEventArgs e)
        {

            string SMQuery = "SELECT * FROM  [ServiceManager].[dbo].[MT_System$Domain$User] WHERE [BaseManagedEntityID]='" + lstSMUsers.SelectedValue + "'";
            List<SMUsersEntity> ThisSMUSer = DataAccessLayer.Instance.GetEntities<SMUsersEntity>(SMQuery);

            foreach (SMUsersEntity CurrentUser in ThisSMUSer)
            {
                
                lstCurrentUsers.Items.Add(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.BaseManagedEntityID.ToString()));

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserID",CurrentUser.BaseManagedEntityID));
                parameters.Add(new SqlParameter("@Username", CurrentUser.UserName_6AF77E23_669B_123F_B392_323C17097BBD));
                parameters.Add(new SqlParameter("@UPN",CurrentUser.UserName_6AF77E23_669B_123F_B392_323C17097BBD + "UPN_SUFFIX"));
                parameters.Add(new SqlParameter("@Firstname",CurrentUser.FirstName_4424C8D5_9E30_E87D_9124_1816663FAFFC));
                parameters.Add(new SqlParameter("@Lastname",CurrentUser.LastName_651E2AAF_6AA9_9423_9D90_4F150DB24C0D));
                parameters.Add(new SqlParameter("@Domain",CurrentUser.Domain_E36D56F2_AD60_E76E_CD5D_9F7AB51AD395));
                parameters.Add(new SqlParameter("@DisplayName",CurrentUser.DisplayName));
                parameters.Add(new SqlParameter("@Email", CurrentUser.UserName_6AF77E23_669B_123F_B392_323C17097BBD + "EMAIL_DOMAIN"));
                parameters.Add(new SqlParameter("@Password", "password"));
                parameters.Add(new SqlParameter("@Tab1",true));
                parameters.Add(new SqlParameter("@Tab2",false));
                parameters.Add(new SqlParameter("@Tab3",false));
                parameters.Add(new SqlParameter("@Tab4",false));
                parameters.Add(new SqlParameter("@Tab5", false));
                DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO Users VALUES(@UserID,@Username,@UPN,@Firstname,@Lastname,@Domain,@DisplayName,@Email,@Password,@Tab1,@Tab2,@Tab3,@Tab4,@Tab5)", parameters.ToArray());

                lstSMUsers.Items.Remove(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.BaseManagedEntityID.ToString()));

            }

            


        }

        protected void btnRemoveUser_Click(object sender, ImageClickEventArgs e)
        {
            string UserIDToDelete = lstCurrentUsers.SelectedValue;
            string UserNameToDelete = lstCurrentUsers.Items[lstCurrentUsers.SelectedIndex].Text;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserID", UserIDToDelete));

            DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM Users WHERE UserID=@UserID",parameters.ToArray());

            lstCurrentUsers.Items.Remove(new ListItem(UserNameToDelete,UserIDToDelete));

        }

        protected void lstCurrentUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = "SELECT * FROM  Users WHERE [ServiceManagerID]='" + lstCurrentUsers.SelectedValue + "'";
            List<UsersEntity> Users = DataAccessLayer.Instance.GetEntities<UsersEntity>(Query);

            foreach (UsersEntity CurrentUser in Users)
            {
                tbDisplayName.Text = CurrentUser.DisplayName.ToString();
                tbDomain.Text = CurrentUser.Domain.ToString();
                tbEmail.Text = CurrentUser.Email.ToString();
                tbFirstName.Text = CurrentUser.Firstname.ToString();
                tbLastName.Text = CurrentUser.Lastname.ToString();
                tbPassword.Text = CurrentUser.password.ToString();
                tbServiceManagerID.Text = CurrentUser.UserID.ToString();
                tbUsername.Text = CurrentUser.Username.ToString();
                
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
                e.Row.Cells[0].Visible = false;
        }

        protected void btnPassChange_Click(object sender, EventArgs e)
        {
            mpeChangePassword.Show();
        }

        protected void btnChangePasswordCancel_Click(object sender, EventArgs e)
        {
            mpeChangePassword.Hide();
        }

        
        



        

        
    }
}