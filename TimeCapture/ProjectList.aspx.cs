using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeCapture
{
    public partial class ProjectList : System.Web.UI.Page
    {
        public static string QueryUserList = "";
       


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                string query = "SELECT * FROM Users where active=1 ORDER BY DisplayName";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);
                
                foreach (UsersEntity CurrentUser in ThisUser)
                    ddlUserList.Items.Add(new ListItem(CurrentUser.DisplayName.ToString().TrimEnd(' '), CurrentUser.UserID));

               
           

                query = "SELECT * FROM dbo.viewProjectList WHERE Status='Active' order by IncidentID";

                RefreshGrid(query);


            }
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {

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

        protected void GridProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnMessageBoxOK_Click(object sender, EventArgs e)
        {
          
        }

        protected void btnRates_Click(object sender, EventArgs e)
        {

        }

        protected void GridProjectList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label IncidentID = (Label)GridProjectList.Rows[index].FindControl("lblIncidentID");

            string queryIncident = "SELECT EntityID FROM Incident WHERE IncidentID='" + IncidentID.Text + "'";
            EntityID.Text = DataAccessLayer.Instance.ExecuteSimpleQuery(queryIncident);
       
            switch (e.CommandName)
            {
                case "DisplayRates":
                    
                    lblMessageBoxTitle.Text = IncidentID.Text + " - Daily Rates";
                    string QueryUserList = "Select * from viewProjectRate where IncidentID='" + IncidentID.Text + "' order by DisplayName";
                    RefreshGridProjectRates(QueryUserList);
                    mpeMessageBox.Show();
                    break;
                case "DisplayDetails":
                    lblDetails.Text = IncidentID.Text + " - Details";
                    mpeProjectDetails.Show();
                    break;
            }

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            
            

           
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", ddlUserList.SelectedValue.ToString()));
            parameters.Add(new SqlParameter("@ProjectId", EntityID.Text));
            parameters.Add(new SqlParameter("@OnsiteRate", txtOnsiteRate.Text));
            parameters.Add(new SqlParameter("@OffsiteRate", txtOffsiteRate.Text));
            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO ProjectRate (UserId,ProjectId,OnsiteRate,OffsiteRate) VALUES (@UserID,@ProjectID,@OnsiteRate,@OffsiteRate)", parameters.ToArray());

            

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

        protected void btnCloseDetails_Click(object sender, EventArgs e)
        {

        }
    }
}