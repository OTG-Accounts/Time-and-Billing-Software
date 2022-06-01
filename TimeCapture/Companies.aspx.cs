using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Security.Principal;


namespace TimeCapture
{
    public partial class Companies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!Common.Instance.CheckAccess())
                    Response.Redirect("AccessDenied.aspx", true);

            if (!this.IsPostBack)
            {
                RefreshGridCompanies();
            }
        }

        protected void btnAddCompany_Click(object sender, EventArgs e)
        {
            string CompanyID = txtCompanyID.Text;

            CompanyID = CompanyID.PadLeft(3, '0');

            if (!CompanyIDExist(CompanyID))
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                parameters.Add(new SqlParameter("@CompanyName", txtCompanyNameAdd.Text));
                parameters.Add(new SqlParameter("@TranslateTo", txtTranslateToAdd.Text));
                DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO Companies VALUES(@CompanyID,@CompanyName,@TranslateTo,1,'')", parameters.ToArray());

                if (cbCreateJobs.Checked)
                {
                    parameters.Clear();
                    parameters.Add(new SqlParameter("@JobNumber","1" + CompanyID));
                    parameters.Add(new SqlParameter("@JobName", "RITs - " + txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@Company", txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                    DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO MYOBJobs VALUES(@JobNumber,@JobName,@Company,@CompanyID)", parameters.ToArray());

                    parameters.Clear();
                    parameters.Add(new SqlParameter("@JobNumber", "2" + CompanyID));
                    parameters.Add(new SqlParameter("@JobName", "MISC - " + txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@Company", txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                    DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO MYOBJobs VALUES(@JobNumber,@JobName,@Company,@CompanyID)", parameters.ToArray());

                    parameters.Clear();
                    parameters.Add(new SqlParameter("@JobNumber", "3" + CompanyID));
                    parameters.Add(new SqlParameter("@JobName", "3PS - " + txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@Company", txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                    DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO MYOBJobs VALUES(@JobNumber,@JobName,@Company,@CompanyID)", parameters.ToArray());

                    parameters.Clear();
                    parameters.Add(new SqlParameter("@JobNumber", "4" + CompanyID));
                    parameters.Add(new SqlParameter("@JobName", "HWSW - " + txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@Company", txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                    DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO MYOBJobs VALUES(@JobNumber,@JobName,@Company,@CompanyID)", parameters.ToArray());

                    parameters.Clear();
                    parameters.Add(new SqlParameter("@JobNumber", "5" + CompanyID));
                    parameters.Add(new SqlParameter("@JobName", "PROJ - " + txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@Company", txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                    DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO MYOBJobs VALUES(@JobNumber,@JobName,@Company,@CompanyID)", parameters.ToArray());

                    parameters.Clear();
                    parameters.Add(new SqlParameter("@JobNumber", "6" + CompanyID));
                    parameters.Add(new SqlParameter("@JobName", "Infinitis - " + txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@Company", txtCompanyNameAdd.Text));
                    parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                    DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO MYOBJobs VALUES(@JobNumber,@JobName,@Company,@CompanyID)", parameters.ToArray());
                }

                txtCompanyID.Text = "";
                txtCompanyNameAdd.Text = "";

                RefreshGridCompanies();
            }
        }

        protected void GridCompanies_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int CompanyID = this.GridCompanies.GetNewValue<int>(e.RowIndex, 1);
            string CompanyName = this.GridCompanies.GetNewValue<string>(e.RowIndex, "txtCompanyName");
            string oldCompanyName = this.GridCompanies.GetOldValue<string>(e.RowIndex, "txtCompanyName");
            string TranslateTo = this.GridCompanies.GetNewValue<string>(e.RowIndex, "txtTranslateTo");
            string oldTranslateTo = this.GridCompanies.GetOldValue<string>(e.RowIndex, "txtTranslateTo");

            if(TranslateTo != oldTranslateTo)
            {
                List<SqlParameter> parameters2 = new List<SqlParameter>();
                parameters2.Add(new SqlParameter("@CompanyID", CompanyID));
                parameters2.Add(new SqlParameter("@TranslateTo", TranslateTo));

                DataAccessLayer.Instance.ExecuteQuery(@"Update Companies set TranslateTo=@TranslateTo where CompanyID=@CompanyID", parameters2.ToArray());
            }

            if (CompanyName != oldCompanyName)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                parameters.Add(new SqlParameter("@CompanyName", CompanyName));

                DataAccessLayer.Instance.ExecuteQuery(@"Update Companies set CompanyName=@CompanyName where CompanyID=@CompanyID", parameters.ToArray());

                parameters.Clear();
                parameters.Add(new SqlParameter("@JobNumber", "1" + CompanyID));
                parameters.Add(new SqlParameter("@JobName", "RITs - " + CompanyName));
                parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                DataAccessLayer.Instance.ExecuteQuery(@"Update MYOBJobs set JobName=@JobName,Company=@CompanyName,CompanyID=@CompanyID where JobNumber=@JobNumber", parameters.ToArray());

                parameters.Clear();
                parameters.Add(new SqlParameter("@JobNumber", "2" + CompanyID));
                parameters.Add(new SqlParameter("@JobName", "MISC - " + CompanyName));
                parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                DataAccessLayer.Instance.ExecuteQuery(@"Update MYOBJobs set JobName=@JobName,Company=@CompanyName,CompanyID=@CompanyID where JobNumber=@JobNumber", parameters.ToArray());

                parameters.Clear();
                parameters.Add(new SqlParameter("@JobNumber", "3" + CompanyID));
                parameters.Add(new SqlParameter("@JobName", "3PS - " + CompanyName));
                parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                DataAccessLayer.Instance.ExecuteQuery(@"Update MYOBJobs set JobName=@JobName,Company=@CompanyName,CompanyID=@CompanyID where JobNumber=@JobNumber", parameters.ToArray());

                parameters.Clear();
                parameters.Add(new SqlParameter("@JobNumber", "4" + CompanyID));
                parameters.Add(new SqlParameter("@JobName", "HWSW - " + CompanyName));
                parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                DataAccessLayer.Instance.ExecuteQuery(@"Update MYOBJobs set JobName=@JobName,Company=@CompanyName,CompanyID=@CompanyID where JobNumber=@JobNumber", parameters.ToArray());

                parameters.Clear();
                parameters.Add(new SqlParameter("@JobNumber", "5" + CompanyID));
                parameters.Add(new SqlParameter("@JobName", "PROJ - " + CompanyName));
                parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                DataAccessLayer.Instance.ExecuteQuery(@"Update MYOBJobs set JobName=@JobName,Company=@CompanyName,CompanyID=@CompanyID where JobNumber=@JobNumber", parameters.ToArray());

                parameters.Clear();
                parameters.Add(new SqlParameter("@JobNumber", "6" + CompanyID));
                parameters.Add(new SqlParameter("@JobName", "Infinitis - " + CompanyName));
                parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                DataAccessLayer.Instance.ExecuteQuery(@"Update MYOBJobs set JobName=@JobName,Company=@CompanyName,CompanyID=@CompanyID where JobNumber=@JobNumber", parameters.ToArray());
            }
        }

        protected void GridCompanies_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 0)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton Delete = e.Row.FindControl("imgDeleteRecord") as ImageButton;
                    TextBox CompanyName = e.Row.FindControl("txtCompanyName") as TextBox;
                    Delete.CommandArgument = e.Row.Cells[1].Text + ";" + CompanyName.Text;


                    // Set the hand mouse cursor for the selected row.
                    e.Row.Attributes.Add("OnMouseOver", "this.style.cursor = 'hand';");

                    // The seelctButton exists for ensuring the selection functionality
                    // and bind it with the appropriate event hanlder.
                    LinkButton selectButton = new LinkButton()
                    {
                        CommandName = "Select",
                        Text = e.Row.Cells[0].Text
                    };
                    selectButton.Font.Underline = false;
                    selectButton.ForeColor = System.Drawing.Color.Black;

                    e.Row.Cells[0].Controls.Add(selectButton);
                    //e.Row.Attributes["OnClick"] =
                    //     Page.ClientScript.GetPostBackClientHyperlink(selectButton, "");
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridCompanies, "Select$" + e.Row.RowIndex);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }

        }

        protected void imgDeleteRecord_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Delete = (ImageButton)sender;



            if (Delete.CommandArgument.Length > 0)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@CompanyID", Delete.CommandArgument.Split(';')[0]));

                DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM Companies WHERE CompanyID=@CompanyID", parameters.ToArray());

                parameters.Clear();
                parameters.Add(new SqlParameter("@CompanyName", Delete.CommandArgument.Split(';')[1]));

                DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM MYOBJobs WHERE Company=@CompanyName", parameters.ToArray());
            }

            RefreshGridCompanies();
        }

        private void RefreshGridCompanies()
        {
            string Query = "SELECT * FROM Companies";
            this.GridCompanies.DataSource = DataAccessLayer.Instance.GetEntities<CompaniesEntity>(Query);
            this.GridCompanies.DataBind();
        }

        private void RefreshGridCompanyRate()
        {
            string Query = "SELECT * FROM CompanyRate WHERE CompanyID='" + GridCompanies.SelectedDataKey.Value.ToString() + "'";
            this.GridCompanyRate.DataSource = DataAccessLayer.Instance.GetEntities<CompanyRateEntity>(Query);
            this.GridCompanyRate.DataBind();
        }

        private void RefreshGridCompanyJobs()
        {
            string Query = "SELECT * FROM MYOBJobs WHERE CompanyID='" + GridCompanies.SelectedDataKey.Value.ToString() + "'";
            this.GridCompanyJobs.DataSource = DataAccessLayer.Instance.GetEntities<JobsEntity>(Query);
            this.GridCompanyJobs.DataBind();
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            this.GridCompanies.BulkEdit = true;
            RefreshGridCompanies();
            this.GridCompanies.BulkUpdate();
            this.GridCompanies.BulkEdit = false;
            RefreshGridCompanies();

            this.GridCompanyRate.BulkEdit = true;
            RefreshGridCompanyRate();
            this.GridCompanyRate.BulkUpdate();
            this.GridCompanyRate.BulkEdit = false;
            RefreshGridCompanyRate();
        }

        private bool CompanyIDExist(string CompanyID)
        {
            bool CompanyIDExist = false;

            string query = "SELECT * FROM Companies WHERE CompanyID = '" + CompanyID + "'";
            List<CompaniesEntity> Companies = DataAccessLayer.Instance.GetEntities<CompaniesEntity>(query);

            if (Companies.Count > 0)
                CompanyIDExist = true;

            return CompanyIDExist;
        }

        protected void GridCompanies_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCompanies.PageIndex = e.NewPageIndex;
            RefreshGridCompanies();
        }

        protected void GridCompanies_Sorting(object sender, GridViewSortEventArgs e)
        {
            string query = "SELECT * FROM Companies ORDER BY " + e.SortExpression + " " + GetSortDirection();
            this.GridCompanies.DataSource = DataAccessLayer.Instance.GetEntities<CompaniesEntity>(query);
            this.GridCompanies.DataBind();
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

        protected void GridCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            RefreshGridCompanyRate();
            RefreshGridCompanyJobs();
        }


        protected void GridCompanyRate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int RateID = this.GridCompanyRate.GetNewValue<int>(e.RowIndex, 0);
            DateTime ValidFrom = this.GridCompanyRate.GetNewValue<DateTime>(e.RowIndex, "txtDate");
            int DefaultOnsiteRate = this.GridCompanyRate.GetNewValue<int>(e.RowIndex, "DefaultOnsite");
            int DefaultOffsiteRate = this.GridCompanyRate.GetNewValue<int>(e.RowIndex, "DefaultOffsite");
            int ProjectOnsiteRate = this.GridCompanyRate.GetNewValue<int>(e.RowIndex, "ProjectOnsite");
            int ProjectOffsiteRate = this.GridCompanyRate.GetNewValue<int>(e.RowIndex, "ProjectOffsite");
            int MiscOnsiteRate = this.GridCompanyRate.GetNewValue<int>(e.RowIndex, "MiscOnsite");
            int MiscOffsiteRate = this.GridCompanyRate.GetNewValue<int>(e.RowIndex, "MiscOffsite");
            bool IsRoundingRecord = this.GridCompanyRate.GetNewValue<bool>(e.RowIndex, "cbRounding");

            DateTime oldValidFrom = this.GridCompanyRate.GetOldValue<DateTime>(e.RowIndex, "txtDate");
            int oldDefaultOnsiteRate = this.GridCompanyRate.GetOldValue<int>(e.RowIndex, "DefaultOnsite");
            int oldDefaultOffsiteRate = this.GridCompanyRate.GetOldValue<int>(e.RowIndex, "DefaultOffsite");
            int oldProjectOnsiteRate = this.GridCompanyRate.GetOldValue<int>(e.RowIndex, "ProjectOnsite");
            int oldProjectOffsiteRate = this.GridCompanyRate.GetOldValue<int>(e.RowIndex, "ProjectOffsite");
            int oldMiscOnsiteRate = this.GridCompanyRate.GetOldValue<int>(e.RowIndex, "MiscOnsite");
            int oldMiscOffsiteRate = this.GridCompanyRate.GetOldValue<int>(e.RowIndex, "MiscOffsite");
            bool oldIsRoundingRecord = this.GridCompanyRate.GetOldValue<bool>(e.RowIndex, "cbRounding");




            if (ValidFrom != oldValidFrom || DefaultOnsiteRate != oldDefaultOnsiteRate || DefaultOffsiteRate != oldDefaultOffsiteRate || ProjectOnsiteRate != oldProjectOnsiteRate || ProjectOffsiteRate != oldProjectOffsiteRate || MiscOnsiteRate != oldMiscOnsiteRate || MiscOffsiteRate != oldMiscOffsiteRate || IsRoundingRecord != oldIsRoundingRecord)
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
                parameters.Add(new SqlParameter("@IsRoundingRecord", IsRoundingRecord));
                
                DataAccessLayer.Instance.ExecuteQuery(@"Update CompanyRate set ValidFrom=@ValidFrom,DefaultOnsite=@DefaultOnsiteRate,DefaultOffsite=@DefaultOffsiteRate,ProjectOnsite=@ProjectOnsiteRate,ProjectOffsite=@ProjectOffsiteRate,MiscOnsite=@MiscOnsiteRate,MiscOffsite=@MiscOffsiteRate,IsRoundingRecord=@IsRoundingRecord where RateID=@RateID", parameters.ToArray());
            }
        }

        protected void GridCompanyRate_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void imgDeleteRecordRate_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Delete = (ImageButton)sender;

            if (Delete.CommandArgument.Length > 0)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RateID", Delete.CommandArgument));

                DataAccessLayer.Instance.ExecuteQuery(@"DELETE FROM CompanyRate WHERE RateID=@RateID", parameters.ToArray());
            }

            RefreshGridCompanyRate();
        }

        protected void btnAddCompanyRate_Click1(object sender, EventArgs e)
        {
            string ValidFrom = txtValidFromRate.Text.Split('/')[2] + "-" + txtValidFromRate.Text.Split('/')[1] + "-" + txtValidFromRate.Text.Split('/')[0];

            string CompanyRateDefaultOnsite = txtCompanyRateDefaultOnsite.Text;
            string CompanyRateDefaultOffsite = txtCompanyRateDefaultOffsite.Text;
            string CompanyRatePROnsite = txtCompanyRatePROnsite.Text;
            string CompanyRatePROffsite = txtCompanyRatePROffsite.Text;
            string CompanyRateMiscOnsite = txtCompanyRateMiscOnsite.Text;
            string CompanyRateMiscOffsite = txtCompanyRateMiscOffsite.Text;

            if (CompanyRateDefaultOnsite == "") CompanyRateDefaultOnsite = "0";
            if (CompanyRateDefaultOffsite == "") CompanyRateDefaultOffsite = "0";
            if (CompanyRatePROnsite == "") CompanyRatePROnsite = "0";
            if (CompanyRatePROffsite == "") CompanyRatePROffsite = "0";
            if (CompanyRateMiscOnsite == "") CompanyRateMiscOnsite = "0";
            if (CompanyRateMiscOffsite == "") CompanyRateMiscOffsite = "0";


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ValidFrom", ValidFrom));
            parameters.Add(new SqlParameter("@CompanyID", GridCompanies.SelectedValue.ToString()));
            parameters.Add(new SqlParameter("@DefaultOnsiteRate", CompanyRateDefaultOnsite));
            parameters.Add(new SqlParameter("@DefaultOffsiteRate", CompanyRateDefaultOffsite));
            parameters.Add(new SqlParameter("@ProjectOnsiteRate", CompanyRatePROnsite));
            parameters.Add(new SqlParameter("@ProjectOffsiteRate", CompanyRatePROffsite));
            parameters.Add(new SqlParameter("@MiscOnsiteRate", CompanyRateMiscOnsite));
            parameters.Add(new SqlParameter("@MiscOffsiteRate", CompanyRateMiscOffsite));
            parameters.Add(new SqlParameter("@IsRoundingRecord", cbIncrement.Checked));
            
            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO CompanyRate (ValidFrom,DefaultOnsite,DefaultOffsite,ProjectOnsite,ProjectOffsite,MiscOnsite,MiscOffsite,IsRoundingRecord,CompanyID) Values(@ValidFrom,@DefaultOnsiteRate,@DefaultOffsiteRate,@ProjectOnsiteRate,@ProjectOffsiteRate,@MiscOnsiteRate,@MiscOffsiteRate,@IsRoundingRecord,@CompanyID)", parameters.ToArray());

            RefreshGridCompanyRate();
        }

        protected void btnAddCompanyJob_Click(object sender, EventArgs e)
        {

        }

        protected void GridCompanyJobs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridCompanyJobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        
    }
}