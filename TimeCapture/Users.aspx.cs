using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeCapture
{
    public partial class Users : System.Web.UI.Page
    {
        public string UserGroup;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);
            string currentPage = System.Web.HttpContext.Current.Request.Url.AbsolutePath;

            IPrincipal Username = HttpContext.Current.User;
            if (Username != null)
            {
                string query = "Select * from Users where Username='" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);

                foreach (UsersEntity CurrentUser in ThisUser)
                    UserGroup = CurrentUser.Group.ToLower().TrimEnd(' ');

            }

            if (!this.IsPostBack)
            {
                
                string SMQuery = "SELECT * FROM  [ServiceManager].[dbo].[MT_System$Domain$User] WHERE [Domain_E36D56F2_AD60_E76E_CD5D_9F7AB51AD395]=''";
                string UsersQuery;
                if(cbShowActiveUsersOnly.Checked)
                    UsersQuery = "SELECT * FROM Users WHERE Active=1 order by Firstname,Lastname";
                else
                    UsersQuery = "SELECT * FROM Users order by Firstname,Lastname";

                List<UsersEntity> ListOfCurrentUsers = DataAccessLayer.Instance.GetEntities<UsersEntity>(UsersQuery);
                ddlPeerReview.Items.Add(new ListItem("N/A"));

                foreach (UsersEntity CurrentUser in ListOfCurrentUsers)
                {
                    lstCurrentUsers.Items.Add(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.UserID.ToString()));
                    SMQuery += " AND BaseManagedEntityId <> '" + CurrentUser.UserID.ToString() + "'";
                    ddlPeerReview.Items.Add(new ListItem(CurrentUser.DisplayName.ToString()));
                }

				ddlGroup.Items.Add(new ListItem("Users"));
				ddlGroup.Items.Add(new ListItem("Service Desk"));
				ddlGroup.Items.Add(new ListItem("Escalations"));
				ddlGroup.Items.Add(new ListItem("Engineers"));
				ddlGroup.Items.Add(new ListItem("Managers"));
				ddlGroup.Items.Add(new ListItem("Administrators"));

				SMQuery += " order by DisplayName";

                

                List<SMUsersEntity> ThisSMUSer = DataAccessLayer.Instance.GetEntitiesSM<SMUsersEntity>(SMQuery);

                foreach (SMUsersEntity CurrentUser in ThisSMUSer)
                {
                    lstSMUsers.Items.Add(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.BaseManagedEntityID.ToString()));
                }

                string CompanyQuery = "SELECT * From Companies";
                List<CompaniesEntity> ListCompanies = DataAccessLayer.Instance.GetEntities<CompaniesEntity>(CompanyQuery);

                ddlUserRateCompany.Items.Add(new ListItem("N/A", DBNull.Value.ToString()));

                foreach (CompaniesEntity ThisCompany in ListCompanies)
                {
                    ddlUserRateCompany.Items.Add(new ListItem(ThisCompany.CompanyName.ToString(), ThisCompany.CompanyID.ToString()));
                }
            }
        }

        protected void btnAddUser_Click(object sender, ImageClickEventArgs e)
        {
            string SMQuery = "SELECT * FROM  [ServiceManager].[dbo].[MT_System$Domain$User] WHERE [BaseManagedEntityID]='" + lstSMUsers.SelectedValue + "'";
            List<SMUsersEntity> ThisSMUSer = DataAccessLayer.Instance.GetEntitiesSM<SMUsersEntity>(SMQuery);

            foreach (SMUsersEntity CurrentUser in ThisSMUSer)
            {

                lstCurrentUsers.Items.Add(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.BaseManagedEntityID.ToString()));

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserID", CurrentUser.BaseManagedEntityID));
                parameters.Add(new SqlParameter("@Username", CurrentUser.UserName_6AF77E23_669B_123F_B392_323C17097BBD));
                parameters.Add(new SqlParameter("@UPN", CurrentUser.UserName_6AF77E23_669B_123F_B392_323C17097BBD + "UPN_SUFFIX"));
                parameters.Add(new SqlParameter("@Firstname", CurrentUser.FirstName_4424C8D5_9E30_E87D_9124_1816663FAFFC));
                parameters.Add(new SqlParameter("@Lastname", CurrentUser.LastName_651E2AAF_6AA9_9423_9D90_4F150DB24C0D));
                parameters.Add(new SqlParameter("@Domain", CurrentUser.Domain_E36D56F2_AD60_E76E_CD5D_9F7AB51AD395));
                parameters.Add(new SqlParameter("@DisplayName", CurrentUser.DisplayName));
                parameters.Add(new SqlParameter("@Email", CurrentUser.UserName_6AF77E23_669B_123F_B392_323C17097BBD + "EMAIL_SUFFIX"));
                parameters.Add(new SqlParameter("@Password", "password"));
                parameters.Add(new SqlParameter("@Group", "Users"));
                parameters.Add(new SqlParameter("@PeerReview", "N/A"));
                parameters.Add(new SqlParameter("@Active", "1"));
                DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO Users VALUES(@UserID,@Username,@UPN,@Firstname,@Lastname,@Domain,@DisplayName,@Email,@Password,@Group,@PeerReview,@Active)", parameters.ToArray());

                lstSMUsers.Items.Remove(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.BaseManagedEntityID.ToString()));

            }
        }

        protected void btnRemoveUser_Click(object sender, ImageClickEventArgs e)
        {
            string UserIDToDelete = lstCurrentUsers.SelectedValue;
            string UserNameToDelete = lstCurrentUsers.Items[lstCurrentUsers.SelectedIndex].Text;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserID", UserIDToDelete));

            DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM Users WHERE UserID=@UserID", parameters.ToArray());
            lstSMUsers.Items.Add(new ListItem(UserNameToDelete, UserIDToDelete));
            lstCurrentUsers.Items.Remove(new ListItem(UserNameToDelete, UserIDToDelete));
        }

        protected void lstCurrentUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = "SELECT * FROM  Users WHERE [UserID]='" + lstCurrentUsers.SelectedValue + "'";
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
                cbUserActive.Checked = Convert.ToBoolean(CurrentUser.Active);
				ddlGroup.SelectedValue = CurrentUser.Group.ToString();
				/*
				switch (CurrentUser.Group.ToString().TrimEnd(' ').ToLower())
                {
                    case "users":
                        ddlGroup.SelectedIndex = 0;
                        break;
                    case "service desk":
                        ddlGroup.SelectedIndex = 1;
                        break;
                    case "escalations":
                        ddlGroup.SelectedIndex = 2;
                        break;
                    case "engineers":
                        ddlGroup.SelectedIndex = 3;
                        break;
                    case "managers":
                        ddlGroup.SelectedIndex = 4;
                        break;
                    case "administrators":
                        ddlGroup.SelectedIndex = 5;
                        break;
                }*/
                ddlPeerReview.SelectedValue = CurrentUser.PeerReview.ToString(); 
            }
            RefreshGridUserCost();
            RefreshGridUserRate();
            RefreshGridUserWorkTimes();
            btnAddUserCost.Enabled = true;

        }

       
        

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserID", tbServiceManagerID.Text));
            parameters.Add(new SqlParameter("@Username", tbUsername.Text));
            parameters.Add(new SqlParameter("@UPN", tbUsername.Text + "UPN_SUFFIX"));
            parameters.Add(new SqlParameter("@Firstname", tbFirstName.Text));
            parameters.Add(new SqlParameter("@Lastname", tbLastName.Text));
            parameters.Add(new SqlParameter("@Domain", tbDomain.Text));
            parameters.Add(new SqlParameter("@DisplayName", tbDisplayName.Text));
            parameters.Add(new SqlParameter("@Email", tbUsername.Text + "EMAIL_SUFFIX"));
            parameters.Add(new SqlParameter("@Password", tbPassword.Text));
            parameters.Add(new SqlParameter("@Group", ddlGroup.SelectedValue.ToString()));
            parameters.Add(new SqlParameter("@PeerReview", ddlPeerReview.SelectedValue.ToString()));
            if(cbUserActive.Checked)
                parameters.Add(new SqlParameter("@Active", "1"));
            else
                parameters.Add(new SqlParameter("@Active", "0"));
            DataAccessLayer.Instance.ExecuteQuery(@"UPDATE Users SET Username=@Username,UPN=@UPN,Firstname=@Firstname,LastName=@Lastname,Domain=@Domain,Displayname=@DisplayName,Email=@Email,Password=@Password,[Group]=@Group,PeerReview=@PeerReview,Active=@Active WHERE UserID=@UserID", parameters.ToArray());

            this.GridUserCost.BulkEdit = true;
            RefreshGridUserCost();
            this.GridUserCost.BulkUpdate();
            this.GridUserCost.BulkEdit = false;
            RefreshGridUserCost();


            this.GridUserRate.BulkEdit = true;
            RefreshGridUserRate();
            this.GridUserRate.BulkUpdate();
            this.GridUserRate.BulkEdit = false;
            RefreshGridUserRate();

            this.GridUserWorkTimes.BulkEdit = true;
            RefreshGridUserWorkTimes();
            this.GridUserWorkTimes.BulkUpdate();
            this.GridUserWorkTimes.BulkEdit = false;
            RefreshGridUserWorkTimes();
        
        }

        
        private void RefreshGridUserCost()
        {
            string Query = "SELECT * FROM viewUserCost WHERE DisplayName like '" + tbDisplayName.Text + "' order by ValidFrom Desc";
            this.GridUserCost.DataSource = DataAccessLayer.Instance.GetEntities<UserCostView>(Query);
            this.GridUserCost.DataBind();
        }

        private void RefreshGridUserRate()
        {
            string Query = "SELECT * FROM viewUserRate WHERE DisplayName like '" + tbDisplayName.Text + "' order by ValidFrom Desc";
            this.GridUserRate.DataSource = DataAccessLayer.Instance.GetEntities<UserRateView>(Query);
            this.GridUserRate.DataBind();
        }

        private void RefreshGridUserWorkTimes()
        {
            string Query = "SELECT * FROM viewUserWorkTime WHERE DisplayName like '" + tbDisplayName.Text + "' order by ValidFrom Desc";
            this.GridUserWorkTimes.DataSource = DataAccessLayer.Instance.GetEntities<UserWorkTimeView>(Query);
            this.GridUserWorkTimes.DataBind();
        }

        protected void GridUserCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 0)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton Delete = e.Row.FindControl("imgDeleteRecord") as ImageButton;
                    Delete.CommandArgument = e.Row.Cells[0].Text;
                }
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void GridUserCost_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int CostID = this.GridUserCost.GetNewValue<int>(e.RowIndex,0);
            DateTime ValidFrom = this.GridUserCost.GetNewValue<DateTime>(e.RowIndex, "txtDate");
            int Cost = this.GridUserCost.GetNewValue<int>(e.RowIndex, "txtCost");


            DateTime oldValidFrom = this.GridUserCost.GetOldValue<DateTime>(e.RowIndex, "txtDate");
            int oldCost = this.GridUserCost.GetOldValue<int>(e.RowIndex, "txtCost");

            if (ValidFrom != oldValidFrom || Cost != oldCost)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CostID", CostID));
                parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
                parameters.Add(new SqlParameter("@Cost", Cost));

                DataAccessLayer.Instance.ExecuteQuery(@"Update UserCost set ValidFrom=@ValidFrom,Cost=@Cost where CostID=@CostID", parameters.ToArray());
            }
        }

        protected void GridUserRate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int RateID = this.GridUserRate.GetNewValue<int>(e.RowIndex, 0);
            DateTime ValidFrom = this.GridUserRate.GetNewValue<DateTime>(e.RowIndex, "txtDate");
            int DefaultOnsiteRate = this.GridUserRate.GetNewValue<int>(e.RowIndex, "DefaultOnsiteRate");
            int DefaultOffsiteRate = this.GridUserRate.GetNewValue<int>(e.RowIndex, "DefaultOffsiteRate");
            int ProjectOnsiteRate = this.GridUserRate.GetNewValue<int>(e.RowIndex, "ProjectOnsiteRate");
            int ProjectOffsiteRate = this.GridUserRate.GetNewValue<int>(e.RowIndex, "ProjectOffsiteRate");
            int MiscOnsiteRate = this.GridUserRate.GetNewValue<int>(e.RowIndex, "MiscOnsiteRate");
            int MiscOffsiteRate = this.GridUserRate.GetNewValue<int>(e.RowIndex, "MiscOffsiteRate");
            bool Override = this.GridUserRate.GetNewValue<bool>(e.RowIndex, "cbOverride");
            bool IsRoundingRecord = this.GridUserRate.GetNewValue<bool>(e.RowIndex, "cbRounding");
            string CompanyID = this.GridUserRate.GetNewValue<string>(e.RowIndex, "Company");

            DateTime oldValidFrom = this.GridUserRate.GetOldValue<DateTime>(e.RowIndex, "txtDate");
            int oldDefaultOnsiteRate = this.GridUserRate.GetOldValue<int>(e.RowIndex, "DefaultOnsiteRate");
            int oldDefaultOffsiteRate = this.GridUserRate.GetOldValue<int>(e.RowIndex, "DefaultOffsiteRate");
            int oldProjectOnsiteRate = this.GridUserRate.GetOldValue<int>(e.RowIndex, "ProjectOnsiteRate");
            int oldProjectOffsiteRate = this.GridUserRate.GetOldValue<int>(e.RowIndex, "ProjectOffsiteRate");
            int oldMiscOnsiteRate = this.GridUserRate.GetOldValue<int>(e.RowIndex, "MiscOnsiteRate");
            int oldMiscOffsiteRate = this.GridUserRate.GetOldValue<int>(e.RowIndex, "MiscOffsiteRate");
            bool oldOverride = this.GridUserRate.GetOldValue<bool>(e.RowIndex, "cbOverride");
            bool oldIsRoundingRecord = this.GridUserRate.GetOldValue<bool>(e.RowIndex, "cbRounding");
            string oldCompanyID = this.GridUserRate.GetOldValue<string>(e.RowIndex, "Company");



            if (ValidFrom != oldValidFrom || DefaultOnsiteRate != oldDefaultOnsiteRate || DefaultOffsiteRate != oldDefaultOffsiteRate || ProjectOnsiteRate != oldProjectOnsiteRate || ProjectOffsiteRate != oldProjectOffsiteRate || MiscOnsiteRate != oldMiscOnsiteRate || MiscOffsiteRate != oldMiscOffsiteRate || Override != oldOverride || IsRoundingRecord != oldIsRoundingRecord || CompanyID != oldCompanyID)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RateID", RateID));
                parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
                parameters.Add(new SqlParameter("@DefaultOnsiteRate", DefaultOnsiteRate));
                parameters.Add(new SqlParameter("@DefaultOffsiteRate", DefaultOffsiteRate));
                parameters.Add(new SqlParameter("@ProjectOnsiteRate", ProjectOnsiteRate));
                parameters.Add(new SqlParameter("@ProjectOffsiteRate", ProjectOffsiteRate));
                parameters.Add(new SqlParameter("@MiscOnsiteRate", MiscOnsiteRate));
                parameters.Add(new SqlParameter("@MiscOffsiteRate", MiscOffsiteRate));
                parameters.Add(new SqlParameter("@Override", Override));
                parameters.Add(new SqlParameter("@IsRoundingRecord", IsRoundingRecord));
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                DataAccessLayer.Instance.ExecuteQuery(@"Update UserRate set ValidFrom=@ValidFrom,DefaultOnsiteRate=@DefaultOnsiteRate,DefaultOffsiteRate=@DefaultOffsiteRate,ProjectOnsiteRate=@ProjectOnsiteRate,ProjectOffsiteRate=@ProjectOffsiteRate,MiscOnsiteRate=@MiscOnsiteRate,MiscOffsiteRate=@MiscOffsiteRate,Override=@Override,IsRoundingRecord=@IsRoundingRecord,CompanyID=@CompanyID where RateID=@RateID", parameters.ToArray());
            }
        }

        protected void GridUserRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 0)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton Delete = e.Row.FindControl("imgDeleteRecordRate") as ImageButton;
                    Delete.CommandArgument = e.Row.Cells[0].Text;
                    
                }
                e.Row.Cells[0].Visible = false;
                
            }
        }

        protected void imgDeleteRecord_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Delete = (ImageButton)sender;

            if (Convert.ToInt32(Delete.CommandArgument) > 0)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CostID", Delete.CommandArgument));

                DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM UserCost WHERE CostID=@CostID", parameters.ToArray());
            }

            RefreshGridUserCost();
        }

        protected void imgDeleteRecordRate_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Delete = (ImageButton)sender;

            if (Convert.ToInt32(Delete.CommandArgument) > 0)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RateID", Delete.CommandArgument));

                DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM UserRate WHERE RateID=@RateID", parameters.ToArray());
            }

            RefreshGridUserRate();
        }

        protected void btnAddUserCost_Click(object sender, EventArgs e)
        {
            if (tbServiceManagerID.Text != "" && txtValidFrom.Text != "" && txtUserCost.Text != "")
            {
                string ValidFrom = txtValidFrom.Text.Split('/')[2] + "-" + txtValidFrom.Text.Split('/')[1] + "-" + txtValidFrom.Text.Split('/')[0];
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserID", tbServiceManagerID.Text));
                parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
                parameters.Add(new SqlParameter("@Cost", txtUserCost.Text));
                DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO UserCost (UserID,ValidFrom,Cost) Values(@UserID,@ValidFrom,@Cost)", parameters.ToArray());

                txtValidFrom.Text = "";
                txtUserCost.Text = "";


                RefreshGridUserCost();

            }

        }
        
        protected void btnAddUserRate_Click(object sender, EventArgs e)
        {
            string ValidFrom = txtValidFromRate.Text.Split('/')[2] + "-" + txtValidFromRate.Text.Split('/')[1] + "-" + txtValidFromRate.Text.Split('/')[0];

            string UserRateDefaultOnsite = txtUserRateDefaultOnsite.Text;
            string UserRateDefaultOffsite = txtUserRateDefaultOffsite.Text;
            string UserRatePROnsite = txtUserRatePROnsite.Text;
            string UserRatePROffsite = txtUserRatePROffsite.Text;
            string UserRateMiscOnsite = txtUserRateMiscOnsite.Text;
            string UserRateMiscOffsite = txtUserRateMiscOffsite.Text;

            if (UserRateDefaultOnsite == "") UserRateDefaultOnsite = "0";
            if (UserRateDefaultOffsite == "") UserRateDefaultOffsite = "0";
            if (UserRatePROnsite == "") UserRatePROnsite = "0";
            if (UserRatePROffsite == "") UserRatePROffsite = "0";
            if (UserRateMiscOnsite == "") UserRateMiscOnsite = "0";
            if (UserRateMiscOffsite == "") UserRateMiscOffsite = "0";


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserID", tbServiceManagerID.Text));
            parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
            parameters.Add(new SqlParameter("@DefaultOnsiteRate", UserRateDefaultOnsite));
            parameters.Add(new SqlParameter("@DefaultOffsiteRate", UserRateDefaultOffsite));
            parameters.Add(new SqlParameter("@ProjectOnsiteRate", UserRatePROnsite));
            parameters.Add(new SqlParameter("@ProjectOffsiteRate", UserRatePROffsite));
            parameters.Add(new SqlParameter("@MiscOnsiteRate", UserRateMiscOnsite));
            parameters.Add(new SqlParameter("@MiscOffsiteRate", UserRateMiscOffsite));
            parameters.Add(new SqlParameter("@Override", cbUserRateOverride.Checked));
            parameters.Add(new SqlParameter("@IsRoundingRecord", cbIncrement.Checked));
            parameters.Add(new SqlParameter("@CompanyID", ddlUserRateCompany.SelectedValue));
            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO UserRate (UserID,ValidFrom,DefaultOnsiteRate,DefaultOffsiteRate,ProjectOnsiteRate,ProjectOffsiteRate,MiscOnsiteRate,MiscOffsiteRate,Override,IsRoundingRecord,CompanyID) Values(@UserID,@ValidFrom,@DefaultOnsiteRate,@DefaultOffsiteRate,@ProjectOnsiteRate,@ProjectOffsiteRate,@MiscOnsiteRate,@MiscOffsiteRate,@Override,@IsRoundingRecord,@CompanyID)", parameters.ToArray());

            RefreshGridUserRate();
        }

        protected void btnAddUserWorkTime_Click(object sender, EventArgs e)
        {
            string ValidFrom = txtValidFromUserWorkTime.Text.Split('/')[2] + "-" + txtValidFromUserWorkTime.Text.Split('/')[1] + "-" + txtValidFromUserWorkTime.Text.Split('/')[0];

            string MonWorkMinutes = txtMonWorkMinutes.Text;
            string TueWorkMinutes = txtTueWorkMinutes.Text;
            string WedWorkMinutes = txtWedWorkMinutes.Text;
            string ThuWorkMinutes = txtThuWorkMinutes.Text;
            string FriWorkMinutes = txtFriWorkMinutes.Text;
            string SatWorkMinutes = txtSatWorkMinutes.Text;
            string SunWorkMinutes = txtSunWorkMinutes.Text;


            if (MonWorkMinutes == "") MonWorkMinutes = "0";
            if (TueWorkMinutes == "") TueWorkMinutes = "0";
            if (WedWorkMinutes == "") WedWorkMinutes = "0";
            if (ThuWorkMinutes == "") ThuWorkMinutes = "0";
            if (FriWorkMinutes == "") FriWorkMinutes = "0";
            if (SatWorkMinutes == "") SatWorkMinutes = "0";
            if (SunWorkMinutes == "") SunWorkMinutes = "0";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserID", tbServiceManagerID.Text));
            parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
            parameters.Add(new SqlParameter("@MonWorkMinutes", MonWorkMinutes));
            parameters.Add(new SqlParameter("@TueWorkMinutes", TueWorkMinutes));
            parameters.Add(new SqlParameter("@WedWorkMinutes", WedWorkMinutes));
            parameters.Add(new SqlParameter("@ThuWorkMinutes", ThuWorkMinutes));
            parameters.Add(new SqlParameter("@FriWorkMinutes", FriWorkMinutes));
            parameters.Add(new SqlParameter("@SatWorkMinutes", SatWorkMinutes));
            parameters.Add(new SqlParameter("@SunWorkMinutes", SunWorkMinutes));
            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO UserWorkTime (UserID,ValidFrom,MonWorkMinutes,TueWorkMinutes,WedWorkMinutes,ThuWorkMinutes,FriWorkMinutes,SatWorkMinutes,SunWorkMinutes) Values(@UserID,@ValidFrom,@MonWorkMinutes,@TueWorkMinutes,@WedWorkMinutes,@ThuWorkMinutes,@FriWorkMinutes,@SatWorkMinutes,@SunWorkMinutes)", parameters.ToArray());

            RefreshGridUserWorkTimes();
        }

        protected void GridUserWorkTimes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int WorkTimeID = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, 0);
            DateTime ValidFrom = this.GridUserWorkTimes.GetNewValue<DateTime>(e.RowIndex, "txtDateUserWorkTime");
            int MonWorkMinutes = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, "txtMonWork");
            int TueWorkMinutes = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, "txtTueWork");
            int WedWorkMinutes = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, "txtWedWork");
            int ThuWorkMinutes = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, "txtThuWork");
            int FriWorkMinutes = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, "txtFriWork");
            int SatWorkMinutes = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, "txtSatWork");
            int SunWorkMinutes = this.GridUserWorkTimes.GetNewValue<int>(e.RowIndex, "txtSunWork");

            DateTime oldValidFrom = this.GridUserWorkTimes.GetOldValue<DateTime>(e.RowIndex, "txtDateUserWorkTime");
            int oldMonWorkMinutes = this.GridUserWorkTimes.GetOldValue<int>(e.RowIndex, "txtMonWork");
            int oldTueWorkMinutes = this.GridUserWorkTimes.GetOldValue<int>(e.RowIndex, "txtTueWork");
            int oldWedWorkMinutes = this.GridUserWorkTimes.GetOldValue<int>(e.RowIndex, "txtWedWork");
            int oldThuWorkMinutes = this.GridUserWorkTimes.GetOldValue<int>(e.RowIndex, "txtThuWork");
            int oldFriWorkMinutes = this.GridUserWorkTimes.GetOldValue<int>(e.RowIndex, "txtFriWork");
            int oldSatWorkMinutes = this.GridUserWorkTimes.GetOldValue<int>(e.RowIndex, "txtSatWork");
            int oldSunWorkMinutes = this.GridUserWorkTimes.GetOldValue<int>(e.RowIndex, "txtSunWork");



            if (ValidFrom != oldValidFrom || MonWorkMinutes != oldMonWorkMinutes || TueWorkMinutes != oldTueWorkMinutes || WedWorkMinutes != oldWedWorkMinutes || ThuWorkMinutes != oldThuWorkMinutes || FriWorkMinutes != oldFriWorkMinutes || SatWorkMinutes != oldSatWorkMinutes || SunWorkMinutes != oldSunWorkMinutes)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@WorkTimeID", WorkTimeID));
                parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
                parameters.Add(new SqlParameter("@MonWorkMinutes", MonWorkMinutes));
                parameters.Add(new SqlParameter("@TueWorkMinutes", TueWorkMinutes));
                parameters.Add(new SqlParameter("@WedWorkMinutes", WedWorkMinutes));
                parameters.Add(new SqlParameter("@ThuWorkMinutes", ThuWorkMinutes));
                parameters.Add(new SqlParameter("@FriWorkMinutes", FriWorkMinutes));
                parameters.Add(new SqlParameter("@SatWorkMinutes", SatWorkMinutes));
                parameters.Add(new SqlParameter("@SunWorkMinutes", SunWorkMinutes));

                DataAccessLayer.Instance.ExecuteQuery(@"Update UserWorkTime set ValidFrom=@ValidFrom,MonWorkMinutes=@MonWorkMinutes,TueWorkMinutes=@TueWorkMinutes,WedWorkMinutes=@WedWorkMinutes,ThuWorkMinutes=@ThuWorkMinutes,FriWorkMinutes=@FriWorkMinutes,SatWorkMinutes=@SatWorkMinutes,SunWorkMinutes=@SunWorkMinutes where WorkTimeID=@WorkTimeID", parameters.ToArray());
            }
        }

        protected void GridUserWorkTimes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 0)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton Delete = e.Row.FindControl("imgDeleteUserWorkTime") as ImageButton;
                    Delete.CommandArgument = e.Row.Cells[0].Text;
                }
                e.Row.Cells[0].Visible = false;
            }
        }

        protected void imgDeleteUserWorkTime_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Delete = (ImageButton)sender;

            if (Convert.ToInt32(Delete.CommandArgument) > 0)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@WorkTimeID", Delete.CommandArgument));

                DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM UserWorkTime WHERE WorkTimeID=@WorkTimeID", parameters.ToArray());
            }

            RefreshGridUserWorkTimes();
        }

        protected void btnImportUserCost_Click(object sender, EventArgs e)
        {
            string CheckFile = HostingEnvironment.MapPath(@"~\Exports\" + txtImportUserCost.Text);
            string line;
            

            if (File.Exists(CheckFile))
            {
                StreamReader file = new StreamReader(CheckFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] Splitted = line.Split('\t');
                    
                        if(Splitted[35].Length > 6)
                        {
                            
                           string salary = GetNumbers(Splitted[35]);
                           salary = salary.Substring(0, (salary.Length - 2)) + "." + salary.Substring(salary.Length - 2, 2);
                            string query = "Select * from Users where Lastname='" + Splitted[2].Trim() + "' AND Firstname='" + Splitted[3].Trim() + "'";
                            List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);

                            foreach (UsersEntity user in ThisUser)
                            {
                                string ValidFrom = Splitted[0].Split('/')[2] + "-" + Splitted[0].Split('/')[1] + "-" + Splitted[0].Split('/')[0];
                                List<SqlParameter> parameters = new List<SqlParameter>();
                                parameters.Add(new SqlParameter("@UserID", user.UserID.ToString()));
                                parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
                                parameters.Add(new SqlParameter("@Cost", salary));
                                DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO UserCost (UserID,ValidFrom,Cost) Values(@UserID,@ValidFrom,@Cost)", parameters.ToArray());
                            }
                        
                            


                        }
            
                }
            }
            
            

            
        }
        
        private static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        protected void cbShowActiveUsersOnly_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            string UsersQuery;
            if (cbShowActiveUsersOnly.Checked)
                UsersQuery = "SELECT * FROM Users WHERE Active=1 order by Firstname,Lastname";
            else
                UsersQuery = "SELECT * FROM Users order by Firstname,Lastname";

            List<UsersEntity> ListOfCurrentUsers = DataAccessLayer.Instance.GetEntities<UsersEntity>(UsersQuery);

            lstCurrentUsers.Items.Clear();
            ddlPeerReview.Items.Clear();

            ddlPeerReview.Items.Add(new ListItem("N/A"));

            foreach (UsersEntity CurrentUser in ListOfCurrentUsers)
            {
                lstCurrentUsers.Items.Add(new ListItem(CurrentUser.DisplayName.ToString(), CurrentUser.UserID.ToString()));
                ddlPeerReview.Items.Add(new ListItem(CurrentUser.DisplayName.ToString()));
            }

            
        }




    }
}