using System;
using System.Drawing;
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
using System.Web.Hosting;

namespace TimeCapture
{
    public partial class UserLicenses : System.Web.UI.Page
    {
        const UInt32 BIT_MASK_MAILBOX = 0x1;
        const UInt32 BIT_MASK_OFFICE365 = 0x2;
        const UInt32 BIT_MASK_WORKSPACE = 0x4;
        const UInt32 BIT_MASK_MSOFFICE = 0x8;
        const UInt32 BIT_MASK_MSPROJECT = 0x10;
        const UInt32 BIT_MASK_MSVISIO = 0x20;
        const UInt32 BIT_MASK_MSCRM = 0x40;
        const UInt32 BIT_MASK_MSSHAREPOINT = 0x80;
        const UInt32 BIT_MASK_THIRDPARTY = 0x100;
        const UInt32 BIT_MASK_MEHS = 0x200;
        const UInt32 BIT_MASK_INTUNE = 0x400;
        const UInt32 BIT_MASK_NFB = 0x80000000;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            string GetDaysQuery = "SELECT SUM(cnt) from ( select COUNT(DISTINCT([Date])) as cnt from UserLicenses where [Date]>=CONVERT(datetime,'" + Convert.ToDateTime(txtStartDate.Text).ToShortDateString() + "',103) AND [Date]<=CONVERT(datetime,'" + Convert.ToDateTime(txtEndDate.Text).ToShortDateString() + "',103) Group by [Date]) as NumberofDays";
            double DaysInPeriod = Convert.ToDouble(DataAccessLayer.Instance.ExecuteSimpleQuery(GetDaysQuery));

            double DateDifference = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).TotalDays + 1;

            DeleteRecordsFromTempUserLicensesChanges();

            if(DateDifference == DaysInPeriod)
            {
                pnlUserLicenses.Visible = true;
                pnlError.Visible = false;

                
                string LicenseQuery = "SELECT * FROM UserLicenses WHERE [Date]>=CONVERT(datetime,'" + Convert.ToDateTime(txtStartDate.Text).ToShortDateString() + "',103) AND [Date]<=CONVERT(datetime,'" + Convert.ToDateTime(txtEndDate.Text).ToShortDateString() + "',103) order by Company,Username,[Date]";
                List<UserLicensesEntity> AllLicenses = DataAccessLayer.Instance.GetEntities<UserLicensesEntity>(LicenseQuery);

                UserLicensesEntity PreviousUser;

                DateTime FirstDateUserFound = Convert.ToDateTime("01-01-1900");
                DateTime LastDateUserFound = Convert.ToDateTime("01-01-2200"); 

                PreviousUser = AllLicenses[0];
                FirstDateUserFound = Convert.ToDateTime(AllLicenses[0].Date);
                LastDateUserFound = Convert.ToDateTime(AllLicenses[0].Date);
                UInt32 PreviousMask = Convert.ToUInt32(AllLicenses[0].LicenseCode);
                bool PreviousDisabled = AllLicenses[0].UserDisabled;

                foreach (UserLicensesEntity License in AllLicenses)
                {
                    if (PreviousUser.Username != License.Username)
                    {
                        //Check user created or deleted
                        if(Convert.ToDateTime(txtStartDate.Text).ToString("dd-MM-yyyy") != FirstDateUserFound.ToString("dd-MM-yyyy"))
                        {
                            //User was created on "FirstDateUserFound"
                            UpdateTempUserLicensesChanges(PreviousUser.System, FirstDateUserFound.ToString("dd-MM-yyyy"), PreviousUser.Username, PreviousUser.Firstname + " " + PreviousUser.Lastname, PreviousUser.Company, "created", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                        }
                        if (Convert.ToDateTime(txtEndDate.Text).ToString("dd-MM-yyyy") != LastDateUserFound.ToString("dd-MM-yyyy"))
                        {
                            //User was deleted on "LastDateUserFound" + 1
                            UpdateTempUserLicensesChanges(PreviousUser.System, LastDateUserFound.ToString("dd-MM-yyyy"), PreviousUser.Username, PreviousUser.Firstname + " " + PreviousUser.Lastname, PreviousUser.Company, "deleted", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        }

                        FirstDateUserFound = Convert.ToDateTime(License.Date);
                        PreviousUser = License;
                        PreviousDisabled = License.UserDisabled;
                        PreviousMask = Convert.ToUInt32(License.LicenseCode);
                    }
                    else
                    {
                        LastDateUserFound = Convert.ToDateTime(License.Date);
                        if(PreviousDisabled != License.UserDisabled)
                        {
                            //User has been enabled or disabled
                            if(!License.UserDisabled)
                                UpdateTempUserLicensesChanges(License.System, Convert.ToDateTime(License.Date).AddDays(-1).ToString("dd-MM-yyyy"), License.Username, License.Firstname + " " + License.Lastname, License.Company, "disabled", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            else
                                UpdateTempUserLicensesChanges(License.System, Convert.ToDateTime(License.Date).AddDays(-1).ToString("dd-MM-yyyy"), License.Username, License.Firstname + " " + License.Lastname, License.Company, "enabled", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                            PreviousDisabled = License.UserDisabled;
                        }
                        if(PreviousMask != Convert.ToUInt32(License.LicenseCode))
                        {
                            int Mailbox = 0;
                            int Office365 = 0;
                            int Workspace = 0;
                            int OfficePro = 0;
                            int Project = 0;
                            int Visio = 0;
                            int CRM = 0;
                            int Sharepoint = 0;
                            int ThirdParty = 0;
                            int MEHS = 0;
                            int InTune = 0;
                            int NotForBilling = 0;
                            
                            if ((PreviousMask & BIT_MASK_MAILBOX) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MAILBOX))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MAILBOX))
                                    Mailbox = 1;
                                else
                                    Mailbox = -1;
                            }

                                
                            if ((PreviousMask & BIT_MASK_OFFICE365) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_OFFICE365))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_OFFICE365))
                                    Office365 = 1;
                                else
                                    Office365 = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_WORKSPACE) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_WORKSPACE))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_WORKSPACE))
                                    Workspace = 1;
                                else
                                    Workspace = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_MSOFFICE) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSOFFICE))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSOFFICE))
                                    OfficePro = 1;
                                else
                                    OfficePro = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_MSPROJECT) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSPROJECT))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSPROJECT))
                                    Project = 1;
                                else
                                    Project = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_MSVISIO) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSVISIO))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSVISIO))
                                    Visio = 1;
                                else
                                    Visio = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_MSCRM) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSCRM))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSCRM))
                                    CRM = 1;
                                else
                                    CRM = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_MSSHAREPOINT) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSSHAREPOINT))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MSSHAREPOINT))
                                    Sharepoint = 1;
                                else
                                    Sharepoint = -1;
                            }

                            if ((PreviousMask & BIT_MASK_THIRDPARTY) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_THIRDPARTY))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_THIRDPARTY))
                                    ThirdParty = 1;
                                else
                                    ThirdParty = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_MEHS) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MEHS))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_MEHS))
                                    MEHS = 1;
                                else
                                    MEHS = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_INTUNE) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_INTUNE))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_INTUNE))
                                    InTune = 1;
                                else
                                    InTune = -1;
                            }
                                
                            if ((PreviousMask & BIT_MASK_NFB) != (Convert.ToUInt32(License.LicenseCode) & BIT_MASK_NFB))
                            {
                                if (Convert.ToBoolean(Convert.ToUInt32(License.LicenseCode) & BIT_MASK_NFB))
                                    NotForBilling = 1;
                                else
                                    NotForBilling = -1;
                            }
                                
                            
                            
                            UpdateTempUserLicensesChanges(License.System, Convert.ToDateTime(License.Date).AddDays(-1).ToString("dd-MM-yyyy"), License.Username, License.Firstname + " " + License.Lastname, License.Company, "License",
                                Mailbox,
                                Office365,
                                Workspace,
                                OfficePro,
                                Project,
                                Visio,
                                CRM,
                                Sharepoint,
                                ThirdParty,
                                MEHS,
                                InTune,
                                NotForBilling);

                            PreviousMask = Convert.ToUInt32(License.LicenseCode);
                        }
                    }

                    
                    
                }
                RefreshUserLicensesChanges();
            }
            else
            {
                pnlUserLicenses.Visible = false;
                pnlError.Visible = true;
                lblDaysError.Text = "Error in the day count! Please use the manual spreadsheet";
                
            }

            

            
        }

        private void UpdateTempUserLicensesChanges(string System, string Date, string Username, string DisplayName, string Company, string Status, int Mailbox, int Office365, int Workspace, int OfficePro, int Project, int Visio, int CRM, int Sharepoint, int ThirdParty, int MEHS, int InTune, int NotForBilling)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@System", System));
            parameters.Add(new SqlParameter("@Date", Date));
            parameters.Add(new SqlParameter("@Username", Username));
            parameters.Add(new SqlParameter("@DisplayName", DisplayName));
            parameters.Add(new SqlParameter("@Company", Company));
            parameters.Add(new SqlParameter("@Status", Status));
            parameters.Add(new SqlParameter("@Mailbox", Mailbox));
            parameters.Add(new SqlParameter("@Office365", Office365));
            parameters.Add(new SqlParameter("@Workspace", Workspace));
            parameters.Add(new SqlParameter("@OfficePro", OfficePro));
            parameters.Add(new SqlParameter("@Project", Project));
            parameters.Add(new SqlParameter("@Visio", Visio));
            parameters.Add(new SqlParameter("@CRM", CRM));
            parameters.Add(new SqlParameter("@Sharepoint", Sharepoint));
            parameters.Add(new SqlParameter("@ThirdParty", ThirdParty));
            parameters.Add(new SqlParameter("@MEHS", MEHS));
            parameters.Add(new SqlParameter("@InTune", InTune));
            parameters.Add(new SqlParameter("@NotForBilling", NotForBilling));

            

            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO TempUserLicensesChanges VALUES(@System,@Date,@Username,@DisplayName,@Company,@Status,@Mailbox,@Office365,@Workspace,@OfficePro,@Project,@Visio,@CRM,@Sharepoint,@ThirdParty,@MEHS,@InTune,@NotForBilling)", parameters.ToArray());
        }

        private void DeleteRecordsFromTempUserLicensesChanges()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Dummy", ""));
            DataAccessLayer.Instance.ExecuteQuery("DELETE FROM TempUserLicensesChanges", parameters.ToArray());
            

        }

        private void RefreshUserLicensesChanges()
        {
            string Query = "SELECT * FROM TempUserLicensesChanges WHERE Status='created' ORDER BY System, Company, DisplayName";

            this.GridUsersCreated.DataSource = DataAccessLayer.Instance.GetEntities<TempUserLicensesChanges>(Query);
            this.GridUsersCreated.DataBind();

            Query = "SELECT * FROM TempUserLicensesChanges WHERE Status='deleted' ORDER BY System, Company, DisplayName";

            this.GridUsersDeleted.DataSource = DataAccessLayer.Instance.GetEntities<TempUserLicensesChanges>(Query);
            this.GridUsersDeleted.DataBind();

            Query = "SELECT * FROM TempUserLicensesChanges WHERE Status='enabled' ORDER BY System, Company, DisplayName";

            this.GridUsersEnabled.DataSource = DataAccessLayer.Instance.GetEntities<TempUserLicensesChanges>(Query);
            this.GridUsersEnabled.DataBind();

            Query = "SELECT * FROM TempUserLicensesChanges WHERE Status='disabled' ORDER BY System, Company, DisplayName";

            this.GridUserDisabled.DataSource = DataAccessLayer.Instance.GetEntities<TempUserLicensesChanges>(Query);
            this.GridUserDisabled.DataBind();

            Query = "SELECT * FROM TempUserLicensesChanges WHERE Status='License' ORDER BY System, Company, DisplayName";

            this.GridUserLicense.DataSource = DataAccessLayer.Instance.GetEntities<TempUserLicensesChanges>(Query);
            this.GridUserLicense.DataBind();


        }

        protected void GridUsersCreated_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
            {
                GridViewRow gvr = e.Row;

                if (gvr.RowType == DataControlRowType.Header)
                {
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 5;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = "USERS CREATED";
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.FromName("#dddddd");
                    cell.BackColor = Color.FromName("#3C454F");
                    cell.ForeColor = Color.FromName("#EEEEEE");
                    row.Cells.Add(cell);

                    GridUsersCreated.Controls[0].Controls.AddAt(0, row);
                }

            }
        }

        protected void GridUsersDeleted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
            {
                GridViewRow gvr = e.Row;

                if (gvr.RowType == DataControlRowType.Header)
                {
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 5;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = "USERS DELETED";
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.FromName("#dddddd");
                    cell.BackColor = Color.FromName("#3C454F");
                    cell.ForeColor = Color.FromName("#EEEEEE");
                    row.Cells.Add(cell);

                    GridUsersDeleted.Controls[0].Controls.AddAt(0, row);
                }

            }
        }

        protected void GridUsersEnabled_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
            {
                GridViewRow gvr = e.Row;

                if (gvr.RowType == DataControlRowType.Header)
                {
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 5;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = "USERS ENABLED";
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.FromName("#dddddd");
                    cell.BackColor = Color.FromName("#3C454F");
                    cell.ForeColor = Color.FromName("#EEEEEE");
                    row.Cells.Add(cell);

                    GridUsersEnabled.Controls[0].Controls.AddAt(0, row);
                }

            }
        }

        protected void GridUserDisabled_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
            {
                GridViewRow gvr = e.Row;

                if (gvr.RowType == DataControlRowType.Header)
                {
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 5;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = "USERS DISABLED";
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.FromName("#dddddd");
                    cell.BackColor = Color.FromName("#3C454F");
                    cell.ForeColor = Color.FromName("#EEEEEE");
                    row.Cells.Add(cell);

                    GridUserDisabled.Controls[0].Controls.AddAt(0, row);
                }

            }
        }

        protected void GridUserLicense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
            {
                GridViewRow gvr = e.Row;

                if (gvr.RowType == DataControlRowType.Header)
                {
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 17;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = "LICENSE CHANGES";
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.FromName("#dddddd");
                    cell.BackColor = Color.FromName("#3C454F");
                    cell.ForeColor = Color.FromName("#EEEEEE");
                    row.Cells.Add(cell);

                    GridUserLicense.Controls[0].Controls.AddAt(0, row);
                }

                
                

            }

            if (e.Row.Cells.Count >= 3)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblMailbox = e.Row.FindControl("lblMailbox") as Label;
                    Label lblOffice365 = e.Row.FindControl("lblOffice365") as Label;
                    Label lblWorkspace = e.Row.FindControl("lblWorkspace") as Label;
                    Label lblOfficePro = e.Row.FindControl("lblOfficePro") as Label;
                    Label lblProject = e.Row.FindControl("lblProject") as Label;
                    Label lblVisio = e.Row.FindControl("lblVisio") as Label;
                    Label lblCRM = e.Row.FindControl("lblCRM") as Label;
                    Label lblSharepoint = e.Row.FindControl("lblSharepoint") as Label;

                    Label lblMEHS = e.Row.FindControl("lblMEHS") as Label;
                    Label lblInTune = e.Row.FindControl("lblInTune") as Label;
                    Label lblNotForBilling = e.Row.FindControl("lblNotForBilling") as Label;


                    Label lblThirdParty = e.Row.FindControl("lblThirdParty") as Label;

                    string value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lblThirdParty"));
                    if (value == "1")
                    {
                        lblThirdParty.ForeColor = Color.White;
                        lblThirdParty.BackColor = Color.IndianRed;
                    }

                }
            }
        }
    }
}