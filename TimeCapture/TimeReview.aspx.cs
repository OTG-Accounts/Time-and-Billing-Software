using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeCapture
{
    public partial class TimeReview : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);
            if (!this.IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                ddlEnteredBy.Items.Add(new ListItem("All","%"));
                ddlCompany.Items.Add(new ListItem("All", "%"));
                ddlCompany.Items.Add(new ListItem("All Cubesys", "AllCubesys"));
                ddlCategory.Items.Add(new ListItem("All", "%"));
                ddlSubCategory.Items.Add(new ListItem("All", "%"));

                NameValueCollection HTTPVariables = Request.QueryString;
                string[] strVariables = HTTPVariables.AllKeys;

                for (int i = 0; i < strVariables.Length; i++)
                {
                    switch (strVariables[i])
                    {
                        case "Company":
                            ddlCompany.SelectedValue = HTTPVariables.GetValues(strVariables[i])[0];
                            break;
                        case "StartDate":
                            txtStartDate.Text = HTTPVariables.GetValues(strVariables[i])[0];
                            break;
                        case "EndDate":
                            txtEndDate.Text = HTTPVariables.GetValues(strVariables[i])[0];
                            break;
                    }
                }


                string query = "SELECT * FROM Users ORDER BY DisplayName";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);

                foreach (UsersEntity CurrentUser in ThisUser)
                    ddlEnteredBy.Items.Add(new ListItem(CurrentUser.DisplayName.ToString().TrimEnd(' '), CurrentUser.DisplayName.ToString().TrimEnd(' ')));
                    
                
                List<CompaniesEntity> Companies=DataAccessLayer.Instance.GetEntities<CompaniesEntity>("SELECT * FROM Companies ORDER BY CompanyName");
                foreach (CompaniesEntity Comp in Companies)
                    ddlCompany.Items.Add(new ListItem(Comp.CompanyName.ToString(), Comp.CompanyName.ToString()));

                List<Category> Categories = DataAccessLayer.Instance.GetEntities<Category>("SELECT * From Category WHERE ParentCategoryID=0");
                foreach (Category Categ in Categories)
                        ddlCategory.Items.Add(new ListItem(Categ.CategoryName.ToString(), Categ.DisplayName.ToString()));
                    

                RefreshGrid();
            }
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
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {

            foreach (GridViewRow item in GridView2.Rows)
            {
                HiddenField editStatus = item.FindControl("hdnEditStatus") as HiddenField;
                if (editStatus != null)
                {
                    if (editStatus.Value == "true")
                    {
                        var lblCategoryID = item.FindControl("EntityChangeLogId") as Label;
                        if (lblCategoryID != null)
                        {
                            //DataRow dr = dtDirtyRows.Rows.Add();
                            //dr["EntityChangeLogId"] = lblCategoryID.Text;
                            TextBox lblComment = item.FindControl("txtComment") as TextBox;
							TextBox txtTimeInMinutes = item.FindControl("txtTimeInMinutes") as TextBox;
							CheckBox cbManagementReview = item.FindControl("cbManagementReview") as CheckBox;
                            CheckBox cbBillable = item.FindControl("cbBillable") as CheckBox;
                            CheckBox cbNTBI = item.FindControl("cbNTBI") as CheckBox;
                            //CheckBox cbError = item.FindControl("cbError") as CheckBox;
                            CheckBox cbOnSite = item.FindControl("cbOnSite") as CheckBox;
                            TextBox txtSubCategory = item.FindControl("txtSubCategory") as TextBox;
                            Label lblIncidentID = item.FindControl("lblIncidentID") as Label;



                            if (Convert.ToInt32(txtTimeInMinutes.Text) < 1)
							{
								lblMessageBox.Text = "Entered time cannot be smaller than 1 minute";
								mpeMessageBox.Show();
							}
							else
							{

								List<SqlParameter> parameters = new List<SqlParameter>();
								parameters.Add(new SqlParameter("@EntityChangeLogId", lblCategoryID.Text));
								parameters.Add(new SqlParameter("@Comment", lblComment.Text));
								parameters.Add(new SqlParameter("@TimeInMinutes", txtTimeInMinutes.Text));
								parameters.Add(new SqlParameter("@ManagementReview", cbManagementReview.Checked));
								parameters.Add(new SqlParameter("@Billable", cbBillable.Checked));
								parameters.Add(new SqlParameter("@NTBI", cbNTBI.Checked));
								parameters.Add(new SqlParameter("@OnSite", cbOnSite.Checked));
								//parameters.Add(new SqlParameter("@Error", cbError.Checked));

								if (cbManagementReview.Checked) parameters.Add(new SqlParameter("@ManagementReviewDate", DateTime.Now));
								else parameters.Add(new SqlParameter("@ManagementReviewDate", DBNull.Value));

								DataAccessLayer.Instance.ExecuteQuery(@"Update Comments set Comment=@Comment,Billable=@Billable,TimeInMinutes=@TimeInMinutes,ManagementReview=@ManagementReview,NotToBeInvoiced=@NTBI,ManagementReviewDate=@ManagementReviewDate,OnSite=@OnSite,Error=0 where EntityChangeLogId=@EntityChangeLogId", parameters.ToArray());


                                if (txtSubCategory.Text.ToLower() == "PS - T&M Monthly".ToLower() || txtSubCategory.Text.ToLower() == "PS - T&M End".ToLower() || txtSubCategory.Text.ToLower() == "PS - T&M PrePaid".ToLower() || txtSubCategory.Text.ToLower() == "PS - Fixed Price".ToLower())
                                {
                                    // add the total hours logged values
                                    List<SqlParameter> loggedParameters = new List<SqlParameter>();
                                    loggedParameters.Add(new SqlParameter("@IncidentID", lblIncidentID.Text));

                                    string sqlBilledHoursQuery = @"UPDATE Incident SET TotalHoursBilled=(select SUM((FLOOR((CAST(TimeInMinutes as float) + 29)/30)*30)/60) from entries where incidentID=@IncidentID and Error=0 and ManagementReview=1 and completed=1 and billable=1) WHERE IncidentID=@IncidentID";
                                    DataAccessLayer.Instance.ExecuteQuery(sqlBilledHoursQuery, loggedParameters.ToArray());

                                }
                            }
                        }
                    }
                }
            }

            RefreshGrid();
            


        }
		protected void btnMessageBoxOK_Click(object sender, EventArgs e)
		{
			txtSortExpression.Text = "ORDER BY EnteredDate, StartTime, EntityChangeLogId ASC";
			RefreshGrid();
		}


		protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSortExpression.Text = "ORDER BY EnteredDate, StartTime, EntityChangeLogId ASC";
            RefreshGrid();
        }

    
        private void RefreshGrid()
        {
            string Query = "SELECT * FROM Entries ";
            Query += "WHERE Category like '" + ddlCategory.SelectedValue + "' ";
            Query += "AND SubCategory like '" + ddlSubCategory.SelectedValue + "' ";
            Query += "AND ManagementReview like '" + ddlReviewed.SelectedValue + "' ";
            Query += "AND PeerReview like '" + ddlPeerReview.SelectedValue + "' ";
            Query += "AND Completed like '" + ddlCompleted.SelectedValue + "' ";
            Query += "AND Billable like '" + ddlBillable.SelectedValue + "' ";
            if(txtSearchComment.Text == "")
                Query += "AND (Comment like '' OR Comment is NULL OR Comment is not null) ";
            else
                Query += "AND Comment like '%" + txtSearchComment.Text + "%' ";
            Query += "AND Title like '%" + txtSearchTitle.Text + "%' ";
            Query += "AND OnSite like '" + ddlOnsite.SelectedValue + "' ";
            Query += "AND AHS like '%' ";
            Query += "AND Error=0 ";
            Query += "AND EnteredBy like '" + ddlEnteredBy.SelectedValue + "' ";
            if(ddlCompany.SelectedValue == "AllCubesys")
                Query += "AND ((CompanyID>='051' and CompanyID<='070') or CompanyID='192') ";
            else
                Query += "AND Company like '" + ddlCompany.SelectedValue + "' ";
            Query += "AND EnteredDate>=CONVERT(datetime,'" + txtStartDate.Text + "',103) AND EnteredDate<=CONVERT(datetime,'" + txtEndDate.Text + "',103) ";
            Query += "AND IncidentID like '%" + txtIncidentID.Text + "%'" + txtSortExpression.Text; 
            
            this.GridView2.DataSource = DataAccessLayer.Instance.GetEntities<EntriesView>(Query);
            this.GridView2.DataBind();
         
        }

        
        
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue != "All")
            {
                ddlSubCategory.Enabled = true;
                ddlSubCategory.Items.Clear();
                ddlSubCategory.Items.Add(new ListItem("All", "%"));
                List<Category> Categories = DataAccessLayer.Instance.GetEntities<Category>("SELECT * From Category WHERE ParentCategoryID=(SELECT CategoryID From Category WHERE CategoryName='" + ddlCategory.SelectedValue + "') ORDER BY DisplayName");
                foreach (Category Categ in Categories)
                    ddlSubCategory.Items.Add(new ListItem(Categ.CategoryName.ToString(), Categ.DisplayName.ToString()));
            }
            else
            {
                ddlSubCategory.Enabled = false;
                ddlSubCategory.Items.Clear();
                ddlSubCategory.Items.Add(new ListItem("All", "%"));
            }
        }

        protected void btnReview_Click(object sender, ImageClickEventArgs e)
        {
            string URL = "PeriodReview.aspx?StartDate=";
            URL += txtStartDate.Text;
            URL += "&EndDate=";
            URL += txtEndDate.Text;
            Response.Redirect(URL);
        }

        
        

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string NotDoneYetBackColour = "#FF0000";
            string PeerReviewBackColour = "#FF8000";
            string InvoicedBackColour = "#80FF80";

            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDate = e.Row.FindControl("txtDate") as TextBox;
                TextBox txtStartTime = e.Row.FindControl("txtStartTime") as TextBox;
                TextBox txtTimeInMinutes = e.Row.FindControl("txtTimeInMinutes") as TextBox;
                TextBox txtTitle = e.Row.FindControl("txtTitle") as TextBox;
                TextBox txtComment = e.Row.FindControl("txtComment") as TextBox;
                CheckBox cbOnsite = e.Row.FindControl("cbOnsite") as CheckBox;
                CheckBox cbManagementReview = e.Row.FindControl("cbManagementReview") as CheckBox;
                Label lblIncidentID = e.Row.FindControl("lblIncidentID") as Label;
                //CheckBox cbError = e.Row.FindControl("cbError") as CheckBox;
                CheckBox cbNTBI = e.Row.FindControl("cbNTBI") as CheckBox;
                CheckBox cbBillable = e.Row.FindControl("cbBillable") as CheckBox;
                TextBox txtSubCategory = e.Row.FindControl("txtSubCategory") as TextBox;
                TextBox txtCategory = e.Row.FindControl("txtCategory") as TextBox;
				ImageButton imgBtnSplit = e.Row.FindControl("imgBtnSplitLine") as ImageButton;
				ImageButton imgBtnDelete = e.Row.FindControl("imgBtnDeleteLine") as ImageButton;

				cbManagementReview.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                //cbError.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                cbNTBI.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                cbBillable.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                cbOnsite.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                txtComment.Attributes.Add("onkeyup", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
				txtTimeInMinutes.Attributes.Add("onkeyup", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");


				string value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Completed"));
                if (value == "False")
                {
                    e.Row.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtDate.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtDate.Enabled = false;

                    txtStartTime.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtStartTime.Enabled = false;

                    txtTimeInMinutes.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtTimeInMinutes.Enabled = false;

                    cbOnsite.BackColor = Color.FromName(NotDoneYetBackColour);
                    cbOnsite.Enabled = false;

                    txtTitle.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtTitle.Enabled = false;

                    txtSubCategory.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtSubCategory.Enabled = false;

                    txtCategory.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtCategory.Enabled = false;

                    txtComment.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtComment.Enabled = false;

                    cbManagementReview.BackColor = Color.FromName(NotDoneYetBackColour);
                    cbManagementReview.Enabled = false;

                    cbNTBI.BackColor = Color.FromName(NotDoneYetBackColour);
                    cbNTBI.Enabled = false;

                    cbBillable.BackColor = Color.FromName(NotDoneYetBackColour);
                    cbBillable.Enabled = false;

                    //cbError.BackColor = Color.FromName(NotDoneYetBackColour);
                    //cbError.Enabled = false;

					imgBtnSplit.Enabled = false;
					imgBtnDelete.Enabled = false;

					//PnlFilterHeader.Visible = false;

				}
                else
                {
                    value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PeerReview"));
                    if (value == "False")
                    {
                        e.Row.BackColor = Color.FromName(PeerReviewBackColour);
                        txtDate.BackColor = Color.FromName(PeerReviewBackColour);
                        txtDate.Enabled = false;

                        txtStartTime.BackColor = Color.FromName(PeerReviewBackColour);
                        txtStartTime.Enabled = false;

                        txtTimeInMinutes.BackColor = Color.FromName(PeerReviewBackColour);
                        txtTimeInMinutes.Enabled = false;

                        cbOnsite.BackColor = Color.FromName(PeerReviewBackColour);
                        cbOnsite.Enabled = false;

                        txtTitle.BackColor = Color.FromName(PeerReviewBackColour);
                        txtTitle.Enabled = false;

                        txtSubCategory.BackColor = Color.FromName(PeerReviewBackColour);
                        txtSubCategory.Enabled = false;

                        txtCategory.BackColor = Color.FromName(PeerReviewBackColour);
                        txtCategory.Enabled = false;

                        txtComment.BackColor = Color.FromName(PeerReviewBackColour);
                        txtComment.Enabled = false;

                        cbManagementReview.BackColor = Color.FromName(PeerReviewBackColour);
                        cbManagementReview.Enabled = false;

                        cbNTBI.BackColor = Color.FromName(PeerReviewBackColour);
                        cbNTBI.Enabled = false;

                        cbBillable.BackColor = Color.FromName(PeerReviewBackColour);
                        cbBillable.Enabled = false;

                        //cbError.BackColor = Color.FromName(PeerReviewBackColour);
                        //cbError.Enabled = false;
						//PnlFilterHeader.Visible = false;

						imgBtnSplit.Enabled = false;
						imgBtnDelete.Enabled = false;
					}
                    else
                    {
                        value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AccountsLock"));
                        if (value == "True")
                        {
                            e.Row.BackColor = Color.FromName(InvoicedBackColour);
                            txtDate.BackColor = Color.FromName(InvoicedBackColour);
                            txtDate.Enabled = false;

                            txtStartTime.BackColor = Color.FromName(InvoicedBackColour);
                            txtStartTime.Enabled = false;

                            txtTimeInMinutes.BackColor = Color.FromName(InvoicedBackColour);
                            txtTimeInMinutes.Enabled = false;

                            cbOnsite.BackColor = Color.FromName(InvoicedBackColour);
                            cbOnsite.Enabled = false;

                            txtTitle.BackColor = Color.FromName(InvoicedBackColour);
                            txtTitle.Enabled = false;

                            txtSubCategory.BackColor = Color.FromName(InvoicedBackColour);
                            txtSubCategory.Enabled = false;

                            txtCategory.BackColor = Color.FromName(InvoicedBackColour);
                            txtCategory.Enabled = false;

                            txtComment.BackColor = Color.FromName(InvoicedBackColour);
                            txtComment.Enabled = false;

                            cbManagementReview.BackColor = Color.FromName(InvoicedBackColour);
                            cbManagementReview.Enabled = false;

                            cbNTBI.BackColor = Color.FromName(InvoicedBackColour);
                            cbNTBI.Enabled = false;

                            cbBillable.BackColor = Color.FromName(InvoicedBackColour);
                            cbBillable.Enabled = false;

                            //cbError.BackColor = Color.FromName(InvoicedBackColour);
                            //cbError.Enabled = false;

							imgBtnSplit.Enabled = false;
							imgBtnDelete.Enabled = false;
						}
                    }
                }
                
                
            }

            

        }

        

        protected void btnIncidentSort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY IncidentID " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnCompanySort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY Company " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnAnalystSort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY EnteredBy " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnEnteredDateSort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY EnteredDate " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnStartSort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY StartTime " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnMinutesSort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY TimeInMinutes " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnCategorySort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY Category " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnSubCategorySort_Click(object sender, EventArgs e)
        {
            txtSortExpression.Text = "ORDER BY SubCategory " + GetSortDirection();
            RefreshGrid();
        }

        protected void btnDoneAll_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow item in GridView2.Rows)
            {
                CheckBox cbDone = item.FindControl("cbManagementReview") as CheckBox;
                HiddenField editStatus = item.FindControl("hdnEditStatus") as HiddenField;

                if (cbDone.Checked)
                    cbDone.Checked = false;
                    
                else
                    cbDone.Checked = true;

                editStatus.Value = "true";
            }

            
        }

		protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int index = Convert.ToInt32(e.CommandArgument);
			Label EntityChangeLogId = (Label)GridView2.Rows[index].FindControl("EntityChangeLogId");
			TextBox TimeInMinutes = (TextBox)GridView2.Rows[index].FindControl("txtTimeInMinutes");

			switch (e.CommandName)
			{
				case "Split":
					if (Convert.ToInt32(TimeInMinutes.Text) > 1)
					{
						string currentId = DataAccessLayer.Instance.ExecuteSimpleQuery("SELECT CurrentID from dbo.SplitID");

						//WriteLog("Budo", "click2");
						List<SqlParameter> parameters = new List<SqlParameter>();
						parameters.Add(new SqlParameter("@EntityChangeLogId", EntityChangeLogId.Text));
						string query = @"INSERT INTO dbo.comments (EntityChangeLogId,IncidentEntityID,UserID,EnteredDate,StartTime,TimeInMinutes,Comment,OnSite,Completed,PeerReview,ManagementReview,";
						query += @"Billable,Error,NotToBeInvoiced,Invoiced,CompletedDate,PeerReviewDate,ManagementReviewDate,InvoicedDate,AccountsLock,AccountsLockDate,Export,ExportDate,";
						query += @"VITRExport,VITRExportDate,PurchaseExport,PurchaseExportDate,AHS,CreatedDate) ";
						query += @"SELECT " + currentId + @",IncidentEntityID,UserID,EnteredDate,StartTime,1,Comment,OnSite,Completed,PeerReview,ManagementReview,";
						query += @"Billable,Error,NotToBeInvoiced,Invoiced,CompletedDate,PeerReviewDate,ManagementReviewDate,InvoicedDate,AccountsLock,AccountsLockDate,Export,ExportDate,";
						query += @"VITRExport,VITRExportDate,PurchaseExport,PurchaseExportDate,AHS,CreatedDate ";
						query += @"FROM dbo.comments WHERE EntityChangeLogId=@EntityChangeLogId";
						DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());


						List<SqlParameter> parameters2 = new List<SqlParameter>();
						parameters2.Add(new SqlParameter("@EntityChangeLogId", EntityChangeLogId.Text));
						parameters2.Add(new SqlParameter("@TimeInMinutes", (Convert.ToInt32(TimeInMinutes.Text) - 1).ToString()));
						query = @"UPDATE dbo.Comments set TimeInMinutes=@TimeInMinutes WHERE EntityChangeLogId=@EntityChangeLogId";
						DataAccessLayer.Instance.ExecuteQuery(query, parameters2.ToArray());

						List<SqlParameter> parameters3 = new List<SqlParameter>();
						parameters3.Add(new SqlParameter("@CurrentID", (Convert.ToInt32(currentId) + 1).ToString()));
						query = @"UPDATE dbo.SplitID set CurrentID=@CurrentID";
						DataAccessLayer.Instance.ExecuteQuery(query, parameters3.ToArray());

						txtSortExpression.Text = "ORDER BY EnteredDate, StartTime, EntityChangeLogId ASC";
						RefreshGrid();

					}
					else
					{
						lblMessageBox.Text = "Time must be higher than 1 minute to split the line";
						mpeMessageBox.Show();
					}
					break;
				case "Del":

					DelID.Text = EntityChangeLogId.Text;

					lblConfirmLine1.Text = "Are you sure you want to delete this entry?";
					lblConfirmLine2.Text = "(This action cannot be reversed!)";
					mpeConfirm.Show();

					break;
			}
			
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
			List<SqlParameter> deleteParameters = new List<SqlParameter>();
			deleteParameters.Add(new SqlParameter("@EntityChangeLogId", DelID.Text));

			string deleteQuery = @"DELETE FROM dbo.Comments WHERE EntityChangeLogId=@EntityChangeLogId";
			DataAccessLayer.Instance.ExecuteQuery(deleteQuery, deleteParameters.ToArray());

			RefreshGrid();
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{

		}
	}

    
}