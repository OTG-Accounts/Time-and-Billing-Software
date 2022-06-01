using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeCapture
{
    public partial class ProjectManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string User = GetCurrentUser();
                string query = "SELECT * FROM Users where active=1 ORDER BY DisplayName";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);

                foreach (UsersEntity CurrentUser in ThisUser)
                    ddlUserList.Items.Add(new ListItem(CurrentUser.DisplayName.ToString().TrimEnd(' '), CurrentUser.UserID));
       

                
                
                query = "select UserID,Username,UPN,Firstname,Lastname,Domain,DisplayName,Email,[Password],[Group],PeerReview,Active from incident INNER JOIN Users on Incident.AssignedTo=Users.UserID where SubCategory like 'PS%' group by UserID,Username,UPN,Firstname,Lastname,Domain,DisplayName,Email,[Password],[Group],PeerReview,Active order by DisplayName";
                List<UsersEntity> ThisUserFilter = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);

                ddlOwner.Items.Add(new ListItem("All", "%"));

                foreach (UsersEntity CurrentUser in ThisUserFilter)
                    ddlOwner.Items.Add(new ListItem(CurrentUser.DisplayName.ToString().TrimEnd(' '), CurrentUser.DisplayName.ToString().TrimEnd(' ')));

                    ddlOwner.SelectedIndex = ddlOwner.Items.IndexOf(ddlOwner.Items.FindByText(User.ToString()));
                
                query = "select Companies.CompanyID,CompanyName,TranslateTo,Active from incident INNER JOIN Companies on Incident.CompanyID=Companies.CompanyID where SubCategory like 'PS%' and active=1 group by Companies.CompanyID,CompanyName,TranslateTo,Active order by CompanyName";
                List<CompaniesEntity> Companies = DataAccessLayer.Instance.GetEntities<CompaniesEntity>(query);

                ddlCompany.Items.Add(new ListItem("All", "%"));
                foreach(CompaniesEntity company in Companies)
                    ddlCompany.Items.Add(new ListItem(company.CompanyName, company.CompanyName));

                
                query = "SELECT * FROM dbo.viewProjectList WHERE Status='Active' and SubCategory <> 'PS - Internal' and DisplayName like '%" + User + "%' order by IncidentID";

                RefreshGrid(query);


            }

        }


        private void RefreshGrid(string Query)
        {
            this.GridProjectList.DataSource = DataAccessLayer.Instance.GetEntities<ProjectListView>(Query);
            this.GridProjectList.DataBind();
        }

        private void RefreshGridProjectRates(string Query)
        {
            this.GridProjectRates.DataSource = DataAccessLayer.Instance.GetEntities<ProjectRateView>(Query);
            this.GridProjectRates.DataBind();
        }

        protected void GridProjectList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            Label IncidentID = (Label)GridProjectList.Rows[index].FindControl("lblIncidentID");

            string queryIncident = "SELECT EntityID FROM Incident WHERE IncidentID='" + IncidentID.Text + "'";
            EntityID.Text = DataAccessLayer.Instance.ExecuteSimpleQuery(queryIncident);

            //string QueryIncidentDetails = "select * from Incident where IncidentID='" + IncidentID.Text + "'";
            string QueryIncidentRevenue = "select EstimatedRevenue from Incident WHERE IncidentID='" + IncidentID.Text + "'";
            string QueryIncidentHours = "select TotalHoursAllocated from Incident WHERE IncidentID='" + IncidentID.Text + "'";
            //List<IncidentEntity> IncidentList = DataAccessLayer.Instance.GetEntities<IncidentEntity>(QueryIncidentDetails);
            txtEstimatedRevenue.Text = DataAccessLayer.Instance.ExecuteSimpleQuery(QueryIncidentRevenue);
            txtHoursAllocated.Text = DataAccessLayer.Instance.ExecuteSimpleQuery(QueryIncidentHours);


            switch (e.CommandName)
            {
                case "DisplayRates":
                    MyLabelIncidentID.Text = IncidentID.Text;
                    lblMessageBoxTitle.Text = IncidentID.Text + " - Daily Rates";
                    string QueryUserList = "Select * from viewProjectRate where IncidentID='" + IncidentID.Text + "' order by DisplayName";
                    RefreshGridProjectRates(QueryUserList);
                    mpeMessageBox.Show();
                    break;
                case "DisplayDetails":
                    lblIncident.Text = EntityID.Text;
                    lblDetails.Text = IncidentID.Text + " - Details";
                    mpeProjectDetails.Show();
                    break;
            }
        }



        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            string query;
            if (!cbExcludeInternal.Checked)
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' order by IncidentID";
            else
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' AND SubCategory <> 'PS - Internal' order by IncidentID";

            RefreshGrid(query);
        }

        protected void GridProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label HoursBilled = e.Row.FindControl("lblHoursBilled") as Label;
                Label HoursLogged = e.Row.FindControl("lblHoursLogged") as Label;
                Label HoursAllocated = e.Row.FindControl("lblTotalHoursAllocated") as Label;

                string IncidentID = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "IncidentID"));
                HoursBilled.Text = "0";
                string query = "select TotalHoursBilled from Incident where incidentID='" + IncidentID + "'";
                HoursBilled.Text = DataAccessLayer.Instance.ExecuteSimpleQuery(query);

                query = "select TotalHoursLogged from Incident where incidentID='" + IncidentID + "'";
                HoursLogged.Text = DataAccessLayer.Instance.ExecuteSimpleQuery(query);
                if (HoursBilled.Text == "")
                    HoursBilled.Text = "0";

                if (Convert.ToDouble(HoursBilled.Text) > Convert.ToDouble(HoursAllocated.Text))
                {
                    HoursBilled.BackColor = System.Drawing.Color.FromName("#C80000");
                    HoursBilled.ForeColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.White);
                    HoursBilled.Font.Bold = true;
                }
                else
                {
                    if (Convert.ToDouble(HoursBilled.Text) >= (Convert.ToDouble(HoursAllocated.Text) / 100 * 90))
                    {
                        HoursBilled.BackColor = System.Drawing.Color.FromName("#EC7600");
                        HoursBilled.ForeColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.White);
                        HoursBilled.Font.Bold = true;
                    }
                    else
                    {
                        HoursBilled.BackColor = System.Drawing.Color.FromName("#008000");
                        HoursBilled.ForeColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.White);
                        HoursBilled.Font.Bold = true;
                    }
                }

               GridView gvEntries = e.Row.FindControl("gvEntries") as GridView;

                string gvEntriesQuery = "Select * from entries where IncidentID='" + IncidentID + "' and error=0 and completed=1 order by EnteredDate";
                gvEntries.DataSource = DataAccessLayer.Instance.GetEntities<EntriesView>(gvEntriesQuery);
                gvEntries.DataBind();
                
            }
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            mpeNewProject.Show();
        }

        protected void lblIncidentID_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {

            
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", ddlUserList.SelectedValue.ToString()));
            parameters.Add(new SqlParameter("@ProjectId", EntityID.Text));
            parameters.Add(new SqlParameter("@OnsiteRate", txtOnsiteRate.Text));
            parameters.Add(new SqlParameter("@OffsiteRate", txtOffsiteRate.Text));
            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO ProjectRate (UserId,ProjectId,OnsiteRate,OffsiteRate) VALUES (@UserID,@ProjectID,@OnsiteRate,@OffsiteRate)", parameters.ToArray());

            
            lblMessageBoxTitle.Text = MyLabelIncidentID.Text + " - Daily Rates";
            string QueryUserList = "Select * from viewProjectRate where IncidentID='" + MyLabelIncidentID.Text + "' order by DisplayName";
            RefreshGridProjectRates(QueryUserList);

            mpeMessageBox.Show();
    
            //RefreshGridProjectRates(QueryUserList);
        }

        protected void GridProjectRates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label Id = (Label)GridProjectRates.Rows[index].FindControl("lblId");

            switch (e.CommandName)
            {
                case "Del":

                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@Id", Id.Text));

                    DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM ProjectRate Where Id=@Id", parameters.ToArray());

                    break;
            }
        }

        protected void btnMessageBoxOK_Click(object sender, EventArgs e)
        {
            mpeMessageBox.Hide();

            string query;
            if (!cbExcludeInternal.Checked)
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' order by IncidentID";
            else
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' AND SubCategory <> 'PS - Internal' order by IncidentID";

            RefreshGrid(query);
        }

        protected void btnSaveDetails_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EntityID", lblIncident.Text));
            parameters.Add(new SqlParameter("@EstimatedRevenue", txtEstimatedRevenue.Text));
            parameters.Add(new SqlParameter("@TotalHoursAllocated", txtHoursAllocated.Text));
            //parameters.Add(new SqlParameter("@Invoiced", txtInvoiced.Text));
            DataAccessLayer.Instance.ExecuteQuery(@"UPDATE Incident set EstimatedRevenue=@EstimatedRevenue,TotalHoursAllocated=@TotalHoursAllocated WHERE EntityID=@EntityID", parameters.ToArray());

            mpeMessageBox.Hide();

            string query;
            if (!cbExcludeInternal.Checked)
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' order by IncidentID";
            else
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' AND SubCategory <> 'PS - Internal' order by IncidentID";

            RefreshGrid(query);
            
        }

        protected void btnCloseDetails_Click(object sender, EventArgs e)
        {
  

            string query;
            if (!cbExcludeInternal.Checked)
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' order by IncidentID";
            else
                query = "SELECT * FROM dbo.viewProjectList where displayname like '" + ddlOwner.SelectedValue + "' AND Status like '" + ddlStatus.SelectedValue + "' AND CompanyName like '" + ddlCompany.SelectedValue + "' AND SubCategory like '" + ddlProjectType.SelectedValue + "' AND SubCategory <> 'PS - Internal' order by IncidentID";

            RefreshGrid(query);
        }

        private string GetCurrentUser()
        {
            IPrincipal Username = HttpContext.Current.User;
            string MyUser = "";

            if (Username != null)
            {
                string query = "Select * from Users where Username='" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);


                MyUser = ThisUser[0].DisplayName.TrimEnd(' ');
                
            }

            return MyUser;

        }

        public static string ReformatDate(string valueFromDatabase)
        {
            
            DateTime value;
            DateTime.TryParseExact(valueFromDatabase, "d/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
            
            return value.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
        }
    }
}