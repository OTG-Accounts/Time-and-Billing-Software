using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TimeCapture
{
    public partial class TimeCapture : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);

			//GridView2.EnableViewState = false;

            if (!this.IsPostBack)
            {
                MondayRedDot.Visible = false;
                TuesdayRedDot.Visible = false;
                WednesdayRedDot.Visible = false;
                ThursdayRedDot.Visible = false;
                FridayRedDot.Visible = false;
                SaturdayRedDot.Visible = false;
                SundayRedDot.Visible = false;

                //txtOrginEntityID.Visible = false;
                
                txtDaySelected.Text = DateTime.Now.ToString();
                CurrentDate.Text = DateTime.Now.ToLongDateString();
                txtCurrentDayOfWeek.Text = GetDayOfTheWeek(DateTime.Now).ToString();

                DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

                HighlightDayStatus(Convert.ToInt32(txtCurrentDayOfWeek.Text));

                string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
               // WriteLog(GetCurrentUser(), "Refresh Grid (Page Load) - " + query);
                RefreshGrid(query);
                CheckRedDotStatus(Convert.ToDateTime(txtDaySelected.Text));


                
            }
        }

        private string GetCurrentUser()
        {
            IPrincipal Username = HttpContext.Current.User;
            string MyUser = "";

            if (Username != null)
            {
                string query = "Select * from Users where Username='" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);
            

                foreach (UsersEntity CurrentUser in ThisUser)
                {
                    MyUser = CurrentUser.DisplayName.ToUpper().TrimEnd(' ');
                }
            }

            return MyUser;

        }

        private string GetCurrentUserID()
        {
            IPrincipal Username = HttpContext.Current.User;
            string MyUser = "";

            if (Username != null)
            {
                string query = "Select * from Users where Username='" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);


                foreach (UsersEntity CurrentUser in ThisUser)
                {
                    MyUser = CurrentUser.UserID.ToString();
                }
            }

            return MyUser;

        }

        private void CheckRedDotStatus(DateTime WeekStartDate)
        {
            DateTime Monday = WeekStartDate.AddDays(1 - GetDayOfTheWeek(WeekStartDate));
            string query;
            object QueryReturn;

            MondayRedDot.Visible = false;
            TuesdayRedDot.Visible = false;
            WednesdayRedDot.Visible = false;
            ThursdayRedDot.Visible = false;
            FridayRedDot.Visible = false;
            SaturdayRedDot.Visible = false;
            SundayRedDot.Visible = false;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + Monday.ToShortDateString() + "',103) and Completed like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) MondayRedDot.Visible = true;
            QueryReturn = null;
            parameters = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(1).ToShortDateString() + "',103) and Completed like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) TuesdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(2).ToShortDateString() + "',103) and Completed like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) WednesdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(3).ToShortDateString() + "',103) and Completed like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) ThursdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(4).ToShortDateString() + "',103) and Completed like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) FridayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(5).ToShortDateString() + "',103) and Completed like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) SaturdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(6).ToShortDateString() + "',103) and Completed like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) SundayRedDot.Visible = true;
            QueryReturn = null;
        }

        private int GetDayOfTheWeek(DateTime myDate)
        {
            int TempDay = (int)myDate.DayOfWeek;

            if (TempDay == 0) TempDay = 7;

            return TempDay;
        }

        private void UpdateDailyTime(int DayNumber, Label DayLabel)
        {
            int MinutesWorked;
            string query;
            object QueryReturn;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));

            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            int CurrentDayOfWeek = GetDayOfTheWeek(DaySelected);


            if (DayNumber > 0)
            {
                query = "SELECT SUM([TimeInMinutes]) FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + DaySelected.AddDays(DayNumber - CurrentDayOfWeek).ToShortDateString() + "',103) AND Error=0 GROUP BY EnteredBY,EnteredDate";
            }
            else
            {
                query = "SELECT SUM([TimeInMinutes]) FROM [Entries] WHERE EnteredBY='" + GetCurrentUser() + "' and EnteredDate>=CONVERT(datetime,'" + DaySelected.AddDays(1 - CurrentDayOfWeek).ToShortDateString() + "',103) AND EnteredDate<=CONVERT(datetime,'" + DaySelected.AddDays(7 - CurrentDayOfWeek).ToShortDateString() + "',103) AND Error=0 GROUP BY EnteredBY";
            }

            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null)
            {
                MinutesWorked = (int)QueryReturn;
                
                switch (DayNumber)
                {
                    case 0:
                        if (MinutesWorked < 2250) lbWeekTime.CssClass = "LabelDailyTimeNotEnough";
                        else lbWeekTime.CssClass = "LabelDailyTime";
                        break;
                    case 1:
                        if (MinutesWorked < 450) lbMondayTime.CssClass = "LabelDailyTimeNotEnough";
                        else lbMondayTime.CssClass = "LabelDailyTime";
                        break;
                    case 2:
                        if (MinutesWorked < 450) lbTuesdayTime.CssClass = "LabelDailyTimeNotEnough";
                        else lbTuesdayTime.CssClass = "LabelDailyTime"; 
                        break;
                    case 3:
                        if (MinutesWorked < 450) lbWednesdayTime.CssClass = "LabelDailyTimeNotEnough";
                        else lbWednesdayTime.CssClass = "LabelDailyTime"; 
                        break;
                    case 4:
                        if (MinutesWorked < 450) lbThursdayTime.CssClass = "LabelDailyTimeNotEnough";
                        else lbThursdayTime.CssClass = "LabelDailyTime"; 
                        break;
                    case 5:
                        if (MinutesWorked < 450) lbFridayTime.CssClass = "LabelDailyTimeNotEnough";
                        else lbFridayTime.CssClass = "LabelDailyTime"; 
                        break;
                }
                DayLabel.Text = ConvertMinutesToHours(MinutesWorked);
            }
            else
            {
                switch (DayNumber)
                {
                    case 0:
                        lbWeekTime.CssClass = "LabelDailyTimeNotEnough";
                        break;
                    case 1:
                        lbMondayTime.CssClass = "LabelDailyTimeNotEnough";
                        break;
                    case 2:
                        lbTuesdayTime.CssClass = "LabelDailyTimeNotEnough";
                        break;
                    case 3:
                        lbWednesdayTime.CssClass = "LabelDailyTimeNotEnough";
                        break;
                    case 4:
                        lbThursdayTime.CssClass = "LabelDailyTimeNotEnough";
                        break;
                    case 5:
                        lbFridayTime.CssClass = "LabelDailyTimeNotEnough";
                        break;
                }

                DayLabel.Text = "00:00";
            }

        }

        private string ConvertMinutesToHours(int Minutes)
        {
            string HoursMinutes;

            int Hours = Minutes / 60;
            int tempMinutes = Minutes - (Hours * 60);

            if (Hours < 10)
            {
                HoursMinutes = "0" + Hours.ToString() + ":";
            }
            else
            {
                HoursMinutes = Hours.ToString() + ":";
            }

            if (tempMinutes < 10)
            {
                HoursMinutes = HoursMinutes + "0" + tempMinutes.ToString();
            }
            else
            {
                HoursMinutes = HoursMinutes + tempMinutes.ToString();
            }

           

            return HoursMinutes;
        }

 
        private void WriteChangesToServiceManager(int EntityChangeLogId)
        {
            string SMQuery = "SELECT * FROM [Entries] WHERE IncidentID=(SELECT IncidentID from [Entries] Where EntityChangeLogId=" + EntityChangeLogId.ToString() + ")";
            List<EntriesView> AllEntriesForServiceManager = DataAccessLayer.Instance.GetEntities<EntriesView>(SMQuery);
            string TimesheetLog = "";
            string BaseManagementID = "";

            foreach (EntriesView ThisSMEntry in AllEntriesForServiceManager)
            {
                TimesheetLog = TimesheetLog + "\n";
                TimesheetLog = TimesheetLog + "Date:\t\t" + Convert.ToDateTime(ThisSMEntry.EnteredDate).ToLongDateString() + "\n";
                TimesheetLog = TimesheetLog + "Start Time:\t" + ThisSMEntry.StartTime + "\n";
                TimesheetLog = TimesheetLog + "Time Spent:\t" + ThisSMEntry.TimeInMinutes + "\n";
                TimesheetLog = TimesheetLog + "Analyst:\t\t" + ThisSMEntry.EnteredBy + "\n";
                TimesheetLog = TimesheetLog + "Comment:\n" + ThisSMEntry.Comment + "\n----------------------------------------------------------------------------------\n";
                BaseManagementID = ThisSMEntry.EntityID;
            }

            List<SqlParameter> SMparameters = new List<SqlParameter>();
            SMparameters.Add(new SqlParameter("@BaseManagementEntityId", BaseManagementID));
            SMparameters.Add(new SqlParameter("@TimesheetLog", TimesheetLog));

            DataAccessLayer.Instance.ExecuteQuery(@"Update [ServiceManager].[dbo].[MT_Incident$TimesheetLog] set [TimesheetLog_60B6F933_A83A_AF68_9F3A_6F415BE7D827]=@TimesheetLog where [BaseManagedEntityId]=@BaseManagementEntityId", SMparameters.ToArray());
        }

        private bool CheckIsNumerical(string toCheck)
        {
            string Str = toCheck.Trim();
            double Num;
            bool isNum = double.TryParse(Str, out Num);

            return isNum;
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            //string query = "SELECT * FROM dbo.Entries WHERE Completed like " + this.ddlCompleted.SelectedValue + "ORDER BY " + e.SortExpression + " " + GetSortDirection();
            //string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + e.SortExpression + " " + GetSortDirection();
            //this.GridView1.DataSource = DataAccessLayer.Instance.GetEntities<EntriesView>(query);
            //this.GridView1.DataBind();


            
        }

        /*private void UpdateEntityIDList()
        {
            ddlSelectEntityID.Items.Clear();

            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);
            List<ListItem> items = new List<ListItem>();

            string query = "SELECT * FROM Entries WHERE Completed=0 and EnteredBy='" + GetCurrentUser() + "' and EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) and TimeInMinutes > 0 and Error=0";
            List<EntriesView> CurrentComments = DataAccessLayer.Instance.GetEntities<EntriesView>(query);


            foreach (EntriesView CurrentComment in CurrentComments)
            {
                //string text = string.Format("{0}{1}{2}", CurrentComment.EntityChangeLogId.ToString().PadRight(10, '\u00A0'), CurrentComment.IncidentID.PadRight(10, '\u00A0'), CurrentComment.TimeInMinutes.ToString().PadRight(10, '\u00A0'));
                items.Add(new ListItem(CurrentComment.EntityChangeLogId.ToString(), CurrentComment.EntityChangeLogId.ToString()));
            }

            items.Sort(delegate(ListItem item1, ListItem item2) { return item1.Text.CompareTo(item2.Text); });
            ddlSelectEntityID.Items.AddRange(items.ToArray());
        }
		*/

        private void RefreshGrid(string Query)
        {
            
            
           this.GridView2.DataSource = DataAccessLayer.Instance.GetEntities<EntriesView>(Query);
           this.GridView2.DataBind();

            UpdateDailyTime(1, lbMondayTime);
            UpdateDailyTime(2, lbTuesdayTime);
            UpdateDailyTime(3, lbWednesdayTime);
            UpdateDailyTime(4, lbThursdayTime);
            UpdateDailyTime(5, lbFridayTime);
            UpdateDailyTime(6, lbSaturdaytime);
            UpdateDailyTime(7, lbSundayTime);
            UpdateDailyTime(0, lbWeekTime);

            //UpdateEntityIDList();

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
			DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);
			foreach (GridViewRow item in GridView2.Rows)
            {
				HiddenField editStatus = item.FindControl("hdnEditStatus") as HiddenField;
                if (editStatus != null)
                {
					if (editStatus.Value == "true")
                    {
						var lblCategoryID = item.FindControl("lblEntityChangeLogId") as TextBox;
                        if (lblCategoryID != null)
                        {
							TextBox lblComment = item.FindControl("txtComment") as TextBox;
                            CheckBox cbCompleted = item.FindControl("cbCompleted") as CheckBox;
                            //CheckBox cbError = item.FindControl("cbError") as CheckBox;
                            CheckBox cbAHS = item.FindControl("cbAHS") as CheckBox;
                            CheckBox cbOnSite = item.FindControl("cbOnSite") as CheckBox;
                            TextBox txtStartTime = item.FindControl("txtStartTime") as TextBox;
                            TextBox txtDate = item.FindControl("txtDate") as TextBox;
							TextBox txtTimeInMinutesRO = item.FindControl("txtTimeInMinutesRO") as TextBox;
                            TextBox txtSubCategory = item.FindControl("txtSubCategory") as TextBox;
                            Button btnIncidentID = item.FindControl("btnIncidentID") as Button;

                            List<SqlParameter> parameters = new List<SqlParameter>();
                            parameters.Add(new SqlParameter("@EntityChangeLogId", lblCategoryID.Text));
                            parameters.Add(new SqlParameter("@Comment", lblComment.Text));
                            parameters.Add(new SqlParameter("@Completed", cbCompleted.Checked));
                            parameters.Add(new SqlParameter("@EnteredDate", Convert.ToDateTime(txtDate.Text)));
                            parameters.Add(new SqlParameter("@StartTime", txtStartTime.Text));
							parameters.Add(new SqlParameter("@TimeInMinutes", txtTimeInMinutesRO.Text));
                            parameters.Add(new SqlParameter("@OnSite", cbOnSite.Checked));
                            //parameters.Add(new SqlParameter("@Error", cbError.Checked));
                            parameters.Add(new SqlParameter("@CompletedDate", DBNull.Value));
                            parameters.Add(new SqlParameter("@AHS", cbAHS.Checked));

							string sqlUpdateCommand = @"Update Comments set OnSite=@OnSite,StartTime=@StartTime,TimeInMinutes=@TimeInMinutes,Comment=@Comment,Completed=@Completed,EnteredDate=@EnteredDate,Error=0,CompletedDate=@CompletedDate,AHS=@AHS where EntityChangeLogId=@EntityChangeLogId";
							if (Convert.ToInt32(txtTimeInMinutesRO.Text) < 1)
							{
								lblMessageBox.Text = "Entered time cannot be smaller than 1 minute";
								mpeMessageBox.Show();
							}
							else
							{
								if (cbCompleted.Checked)
								{
									parameters[7].Value = DateTime.Now;
									if (lblComment.Text == "" || lblComment.Text == null)
									{
										lblMessageBox.Text = "Record not saved, Comment blank";
										mpeMessageBox.Show();
									}
									else
									{
										DataAccessLayer.Instance.ExecuteQuery(sqlUpdateCommand, parameters.ToArray());
                                        
                                        
                                        if(txtSubCategory.Text.ToLower() == "PS - T&M Monthly".ToLower() || txtSubCategory.Text.ToLower() == "PS - T&M End".ToLower() || txtSubCategory.Text.ToLower() == "PS - T&M PrePaid".ToLower() || txtSubCategory.Text.ToLower() == "PS - Fixed Price".ToLower())
                                        {
                                            // add the total hours logged values
                                            List<SqlParameter> loggedParameters = new List<SqlParameter>();
                                            loggedParameters.Add(new SqlParameter("@IncidentID", btnIncidentID.Text));

                                            string sqlLoggedHoursQuery = @"UPDATE Incident SET TotalHoursLogged=(select SUM((FLOOR((CAST(TimeInMinutes as float) + 29)/30)*30)/60) from entries where incidentID=@IncidentID and Error=0 and completed=1) WHERE IncidentID=@IncidentID";
                                            DataAccessLayer.Instance.ExecuteQuery(sqlLoggedHoursQuery, loggedParameters.ToArray());
                                            // check if total allocated hours have been exceeded, and if so, send email to owner



                                            // Check if user has rate assigned, and if not, send warning email to owner
                                            List<SqlParameter> checkParameters = new List<SqlParameter>();
                                            checkParameters.Add(new SqlParameter("@fake", "fake"));
                                            string checkProjectRateQuery = "select ProjectRate.ID from ProjectRate inner join Comments on ProjectRate.ProjectId = Comments.IncidentEntityID where Comments.EntityChangeLogId = " + lblCategoryID.Text + " and ProjectRate.userid = '" + GetCurrentUserID() + "'";

                                            object checkProjectRateQueryReturn = DataAccessLayer.Instance.ExecuteQuery(checkProjectRateQuery, checkParameters.ToArray());
                                            if (checkProjectRateQueryReturn == null)
                                            {
                                                string getProjectOwnerQuery = "select email from users inner join Incident on Incident.AssignedTo=Users.UserID where IncidentID='" + btnIncidentID.Text + "'";
                                                string projectOwnerEmail = DataAccessLayer.Instance.ExecuteSimpleQuery(getProjectOwnerQuery);
                                                SendWarningEmail(projectOwnerEmail,btnIncidentID.Text,GetCurrentUser());
                                            }
                                        }
                                        
									}
								}
								else
								{
									DataAccessLayer.Instance.ExecuteQuery(sqlUpdateCommand, parameters.ToArray());
								}
							}
                       
                        }
                    }
                }
            }

            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;

            RefreshGrid(query);

            CheckRedDotStatus(DaySelected);

        }

        private void SendWarningEmail(string emailAddress, string projectID, string whoLoggedTheTime)
        {
            SmtpClient smtp = new SmtpClient("SMTP_IP",25);
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("REPLY_ADDRESS");
            mail.To.Add(new MailAddress(emailAddress));
            mail.Bcc.Add(new MailAddress("WARNING_ADDRESS"));
            mail.Subject = projectID + " - WARNING";
            mail.Body = whoLoggedTheTime + " has added time to the project " + projectID + ".\nThis user does not appear to be part of the team assigned to this project.\n\nPlease check if this time has been added in error or add the appropriate rate for this user in the project rate section.\n\nThank you,\nThe Timesheet Management System";
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Send(mail);
        }
 
        private void HighlightDayStatus(int DayNumber)
        {
            switch (DayNumber)
            {
                case 0:
                    Monday.Attributes["class"] = "MondayDefault";
                    Tuesday.Attributes["class"] = "TuesdayDefault";
                    Wednesday.Attributes["class"] = "WednesdayDefault";
                    Thursday.Attributes["class"] = "ThursdayDefault";
                    Friday.Attributes["class"] = "FridayDefault";
                    Saturday.Attributes["class"] = "SaturdayDefault";
                    Sunday.Attributes["class"] = "SundayDefault";
                    Week.Attributes["class"] = "WeekSelected";
                    break;
                case 1:
                    Monday.Attributes["class"] = "MondaySelected";
                    Tuesday.Attributes["class"] = "TuesdayDefault";
                    Wednesday.Attributes["class"] = "WednesdayDefault";
                    Thursday.Attributes["class"] = "ThursdayDefault";
                    Friday.Attributes["class"] = "FridayDefault";
                    Saturday.Attributes["class"] = "SaturdayDefault";
                    Sunday.Attributes["class"] = "SundayDefault";
                    Week.Attributes["class"] = "WeekDefault";
                    break;
                case 2:
                    Monday.Attributes["class"] = "MondayDefault";
                    Tuesday.Attributes["class"] = "TuesdaySelected";
                    Wednesday.Attributes["class"] = "WednesdayDefault";
                    Thursday.Attributes["class"] = "ThursdayDefault";
                    Friday.Attributes["class"] = "FridayDefault";
                    Saturday.Attributes["class"] = "SaturdayDefault";
                    Sunday.Attributes["class"] = "SundayDefault";
                    Week.Attributes["class"] = "WeekDefault";
                    break;
                case 3:
                    Monday.Attributes["class"] = "MondayDefault";
                    Tuesday.Attributes["class"] = "TuesdayDefault";
                    Wednesday.Attributes["class"] = "WednesdaySelected";
                    Thursday.Attributes["class"] = "ThursdayDefault";
                    Friday.Attributes["class"] = "FridayDefault";
                    Saturday.Attributes["class"] = "SaturdayDefault";
                    Sunday.Attributes["class"] = "SundayDefault";
                    Week.Attributes["class"] = "WeekDefault";
                    break;
                case 4:
                    Monday.Attributes["class"] = "MondayDefault";
                    Tuesday.Attributes["class"] = "TuesdayDefault";
                    Wednesday.Attributes["class"] = "WednesdayDefault";
                    Thursday.Attributes["class"] = "ThursdaySelected";
                    Friday.Attributes["class"] = "FridayDefault";
                    Saturday.Attributes["class"] = "SaturdayDefault";
                    Sunday.Attributes["class"] = "SundayDefault";
                    Week.Attributes["class"] = "WeekDefault";
                    break;
                case 5:
                    Monday.Attributes["class"] = "MondayDefault";
                    Tuesday.Attributes["class"] = "TuesdayDefault";
                    Wednesday.Attributes["class"] = "WednesdayDefault";
                    Thursday.Attributes["class"] = "ThursdayDefault";
                    Friday.Attributes["class"] = "FridaySelected";
                    Saturday.Attributes["class"] = "SaturdayDefault";
                    Sunday.Attributes["class"] = "SundayDefault";
                    Week.Attributes["class"] = "WeekDefault";
                    break;
                case 6:
                    Monday.Attributes["class"] = "MondayDefault";
                    Tuesday.Attributes["class"] = "TuesdayDefault";
                    Wednesday.Attributes["class"] = "WednesdayDefault";
                    Thursday.Attributes["class"] = "ThursdayDefault";
                    Friday.Attributes["class"] = "FridayDefault";
                    Saturday.Attributes["class"] = "SaturdaySelected";
                    Sunday.Attributes["class"] = "SundayDefault";
                    Week.Attributes["class"] = "WeekDefault";
                    break;
                case 7:
                    Monday.Attributes["class"] = "MondayDefault";
                    Tuesday.Attributes["class"] = "TuesdayDefault";
                    Wednesday.Attributes["class"] = "WednesdayDefault";
                    Thursday.Attributes["class"] = "ThursdayDefault";
                    Friday.Attributes["class"] = "FridayDefault";
                    Saturday.Attributes["class"] = "SaturdayDefault";
                    Sunday.Attributes["class"] = "SundaySelected";
                    Week.Attributes["class"] = "WeekDefault";
                    break;
            }
            
        }

        private string GetSearchDate(int DayNumber)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);
            int CurrentDayOfWeek = Convert.ToInt32(txtCurrentDayOfWeek.Text);

            HighlightDayStatus(DayNumber);
            DaySelected = DaySelected.AddDays(DayNumber - GetDayOfTheWeek(DaySelected));

            //DaySelected = Convert.ToDateTime(DateTime.Now.AddDays(DayNumber - CurrentDayOfWeek).ToShortDateString());
            CurrentDate.Text = Convert.ToDateTime(DaySelected).ToLongDateString();
            CurrentDayOfWeek = GetDayOfTheWeek(DaySelected);

            txtDaySelected.Text = DaySelected.ToString();
            txtCurrentDayOfWeek.Text = CurrentDayOfWeek.ToString();

            return DaySelected.ToShortDateString();
        }

        protected void btnMonday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(1) + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Monday Button) - " + query);
            RefreshGrid(query);
        }

        protected void btnTuesday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(2) + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Tuesday Button) - " + query);
            RefreshGrid(query);
            
        }

        protected void btnWednesday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(3) + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Wednesday Button) - " + query);
            RefreshGrid(query);
        }

        protected void btnThursday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(4) + "',103) " + txtSortExpression.Text;
           // WriteLog(GetCurrentUser(), "Refresh Grid (Thursday Button) - " + query);
            RefreshGrid(query);
        }

        protected void btnFriday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(5) + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Friday Button) - " + query);
            RefreshGrid(query);
        }

        protected void btnSaturday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(6) + "',103) " + txtSortExpression.Text;
           // WriteLog(GetCurrentUser(), "Refresh Grid (Saturday Button) - " + query);
            RefreshGrid(query);
        }

        protected void btnSunday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(7) + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Sunday Button) - " + query);
            RefreshGrid(query);
        }

        protected void btnWeek_Click(object sender, ImageClickEventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);
            DateTime EndDate;

            HighlightDayStatus(0);
            DaySelected = DaySelected.AddDays(1 - GetDayOfTheWeek(DaySelected));
            EndDate = DaySelected.AddDays(7 - GetDayOfTheWeek(DaySelected));
            //DaySelected = DateTime.Now.AddDays(7 - CurrentDayOfWeek).ToShortDateString();
            CurrentDate.Text = Convert.ToDateTime(DaySelected).ToLongDateString() + " - " + Convert.ToDateTime(EndDate).ToLongDateString();

            txtDaySelected.Text = DaySelected.ToString();
            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate>=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate.ToShortDateString() + "',103) " + txtSortExpression.Text;
            WriteLog(GetCurrentUser(), "Refresh Grid (Week Button) - " + query);
            RefreshGrid(query);
        }

        protected void btnPreviousWeek_Click(object sender, ImageClickEventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            DaySelected = DaySelected.AddDays(-7);
            CurrentDate.Text = Convert.ToDateTime(DaySelected).ToLongDateString();
            int CurrentDayOfWeek = GetDayOfTheWeek(DaySelected);

            txtDaySelected.Text = DaySelected.ToString();
            txtCurrentDayOfWeek.Text = CurrentDayOfWeek.ToString();

            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Previous week Button) - " + query);
            RefreshGrid(query);
            CheckRedDotStatus(DaySelected);

            
        }

        protected void btnToday_Click(object sender, ImageClickEventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            DaySelected = DateTime.Now;
            CurrentDate.Text = Convert.ToDateTime(DaySelected).ToLongDateString();
            int CurrentDayOfWeek = GetDayOfTheWeek(DaySelected);

            txtDaySelected.Text = DaySelected.ToString();
            txtCurrentDayOfWeek.Text = CurrentDayOfWeek.ToString();

            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Today Button) - " + query);
            RefreshGrid(query);
            CheckRedDotStatus(DaySelected);
            
        }

        protected void btnNextWeek_Click(object sender, ImageClickEventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            DaySelected = DaySelected.AddDays(7);
            CurrentDate.Text = Convert.ToDateTime(DaySelected).ToLongDateString();
            int CurrentDayOfWeek = GetDayOfTheWeek(DaySelected);

            txtDaySelected.Text = DaySelected.ToString();
            txtCurrentDayOfWeek.Text = CurrentDayOfWeek.ToString();

            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
           // WriteLog(GetCurrentUser(), "Refresh Grid (Next Week Button) - " + query);
            RefreshGrid(query);
            CheckRedDotStatus(DaySelected);
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Refresh Button) - " + query);
            RefreshGrid(query);
        }

        protected void ddlCompleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            if ( GetDayOfTheWeek(DaySelected) > 0)
            {
                string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
               // WriteLog(GetCurrentUser(), "Refresh Grid (Filter Completed changed) - " + query);
                RefreshGrid(query);
            }
            else
            {
                DateTime StartDate = DaySelected.AddDays(1 - GetDayOfTheWeek(DaySelected));
                DateTime EndDate = DaySelected.AddDays(7 - GetDayOfTheWeek(DaySelected));

                string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredDate>=CONVERT(datetime,'" + StartDate.ToShortDateString() + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate.ToShortDateString() + "',103) " + txtSortExpression.Text;
               // WriteLog(GetCurrentUser(), "Refresh Grid (Filter Completed changed2) - " + query);
                RefreshGrid(query);
            }
            
        }

        protected void btnMessageBoxOK_Click(object sender, EventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);
            mpeMessageBox.Hide();

            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
           // WriteLog(GetCurrentUser(), "Refresh Grid (MessageBox OK) - " + query);
            RefreshGrid(query);
        }

        protected void btnIncidentID_Click(object sender, EventArgs e)
        {
            
            lblMessageBox.Text = "Incident Details";
            mpeMessageBox.Show();
        }


       

        protected void ddlError_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            if (GetDayOfTheWeek(DaySelected) > 0)
            {
                string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
                //WriteLog(GetCurrentUser(), "Refresh Grid (Filter Completed changed) - " + query);
                RefreshGrid(query);
            }
            else
            {
                DateTime StartDate = DaySelected.AddDays(1 - GetDayOfTheWeek(DaySelected));
                DateTime EndDate = DaySelected.AddDays(7 - GetDayOfTheWeek(DaySelected));

                string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredDate>=CONVERT(datetime,'" + StartDate.ToShortDateString() + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate.ToShortDateString() + "',103) " + txtSortExpression.Text;
               // WriteLog(GetCurrentUser(), "Refresh Grid (Filter Completed changed2) - " + query);
                RefreshGrid(query);
            }
        }

        private void WriteLog(string User, string ToWrite)
        {
            string ExportFile = HostingEnvironment.MapPath(@"~\Exports\Log_" + User + "_" + DateTime.Now.ToShortDateString().Split('/')[0] + DateTime.Now.ToShortDateString().Split('/')[1] + DateTime.Now.ToShortDateString().Split('/')[2] + ".txt");

            System.IO.StreamWriter LogFile = new System.IO.StreamWriter(ExportFile,true);
            
            ToWrite = DateTime.Now.ToString() + " - " + ToWrite;
            LogFile.WriteLine(ToWrite);
            
            LogFile.Flush();
            LogFile.Close();
            LogFile.Dispose();

        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string PeerReviewBackColour = "#EEEEEE";
            string ManagementReviewBackColour = "#CCCCCC";

            if (e.Row.Cells.Count >= 3)
            {
                //e.Row.Cells[0].Visible = false;




                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Button txtIncidentID = e.Row.FindControl("btnIncidentID") as Button;
                    TextBox txtDate = e.Row.FindControl("txtDate") as TextBox;
                    TextBox txtStartTime = e.Row.FindControl("txtStartTime") as TextBox;
                    TextBox txtTimeInMinutesRO = e.Row.FindControl("txtTimeInMinutesRO") as TextBox;
                    TextBox txtTitle = e.Row.FindControl("txtTitle") as TextBox;
                    TextBox txtComment = e.Row.FindControl("txtComment") as TextBox;
                    TextBox txtCategory = e.Row.FindControl("txtCategory") as TextBox;
                    CheckBox cbOnsite = e.Row.FindControl("cbOnsite") as CheckBox;
                    CheckBox cbCompleted = e.Row.FindControl("cbCompleted") as CheckBox;
                    //CheckBox cbError = e.Row.FindControl("cbError") as CheckBox;
                    CheckBox cbAHS = e.Row.FindControl("cbAHS") as CheckBox;
                    TextBox txtSubCategory = e.Row.FindControl("txtSubCategory") as TextBox;
					ImageButton imgBtnSplit = e.Row.FindControl("imgBtnSplitLine") as ImageButton;
					ImageButton imgBtnDelete = e.Row.FindControl("imgBtnDeleteLine") as ImageButton;

					cbOnsite.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                    cbCompleted.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                    //cbError.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                    cbAHS.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                    txtDate.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                    txtDate.Attributes.Add("onkeyup", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                    txtStartTime.Attributes.Add("onkeyup", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                    txtComment.Attributes.Add("onkeyup", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
					txtTimeInMinutesRO.Attributes.Add("onkeyup", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
					//txtComment.Attributes.Add("onfocus", "$find(\"" + this.FindControl("pnlMessageBox").ClientID + "\").show();");

					/*if (Convert.ToInt32(txtTimeInMinutesRO.Text) < 0)
                    {
                        txtComment.Attributes.Add("onclick", "ShowPopup('" + this.mpeNegativeTime.FindControl("txtOrginEntityID").ClientID + "','" + e.Row.FindControl("lblEntityChangeLogId").ClientID + "')");
                        txtComment.Attributes.Add("onfocus", "ShowPopup('" + this.mpeNegativeTime.FindControl("txtOrginEntityID").ClientID + "','" + e.Row.FindControl("lblEntityChangeLogId").ClientID + "')");
                    }*/

                    string value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PeerReview"));
                    if (value == "True")
                    {
                        e.Row.BackColor = Color.FromName(PeerReviewBackColour);
                        txtDate.BackColor = Color.FromName(PeerReviewBackColour);
                        txtDate.Enabled = false;

                        txtStartTime.BackColor = Color.FromName(PeerReviewBackColour);
                        txtStartTime.Enabled = false;


                        txtTimeInMinutesRO.BackColor = Color.FromName(PeerReviewBackColour);
                        txtTimeInMinutesRO.Enabled = false;

                        cbOnsite.BackColor = Color.FromName(PeerReviewBackColour);
                        cbOnsite.Enabled = false;

                        txtTitle.BackColor = Color.FromName(PeerReviewBackColour);
                        txtTitle.Enabled = false;

                        txtComment.BackColor = Color.FromName(PeerReviewBackColour);
                        txtComment.Enabled = false;

                        cbCompleted.BackColor = Color.FromName(PeerReviewBackColour);
                        cbCompleted.Enabled = false;

                        //cbError.BackColor = Color.FromName(PeerReviewBackColour);
                        //cbError.Enabled = false;

                        cbAHS.BackColor = Color.FromName(PeerReviewBackColour);
                        cbAHS.Enabled = false;

                        txtCategory.BackColor = Color.FromName(PeerReviewBackColour);
                        txtCategory.Enabled = false;

                        txtSubCategory.BackColor = Color.FromName(PeerReviewBackColour);
                        txtSubCategory.Enabled = false;

						imgBtnSplit.Enabled = false;
						imgBtnDelete.Enabled = false;

						//PnlFilterHeader.Visible = false;

					}
					value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ManagementReview"));
                    if (value == "True")
                    {
                        e.Row.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtDate.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtDate.Enabled = false;

                        txtStartTime.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtStartTime.Enabled = false;

                        txtTimeInMinutesRO.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtTimeInMinutesRO.Enabled = false;

                        cbOnsite.BackColor = Color.FromName(ManagementReviewBackColour);
                        cbOnsite.Enabled = false;

                        txtTitle.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtTitle.Enabled = false;

                        txtComment.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtComment.Enabled = false;

                        cbCompleted.BackColor = Color.FromName(ManagementReviewBackColour);
                        cbCompleted.Enabled = false;

                        //cbError.BackColor = Color.FromName(ManagementReviewBackColour);
                        //cbError.Enabled = false;

                        cbAHS.BackColor = Color.FromName(ManagementReviewBackColour);
                        cbAHS.Enabled = false;

                        txtCategory.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtCategory.Enabled = false;

                        txtSubCategory.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtSubCategory.Enabled = false;

						imgBtnSplit.Enabled = false;
						imgBtnDelete.Enabled = false;

						//PnlFilterHeader.Visible = false;
					}

				}
            }
        }

        
        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            txtSortExpression.Text = "ORDER BY " + e.SortExpression + " " + GetSortDirection();
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            string query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
            //WriteLog(GetCurrentUser(), "Refresh Grid (Grid Sort) - " + query);
            RefreshGrid(query);
        }

        		

		protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int index = Convert.ToInt32(e.CommandArgument);
			Label EntityChangeLogId = (Label)GridView2.Rows[index].FindControl("lblRecordID");

			switch (e.CommandName)
			{
				case "Split":
					
					TextBox TimeInMinutes = (TextBox)GridView2.Rows[index].FindControl("txtTimeInMinutesRO");

					if (Convert.ToInt32(TimeInMinutes.Text) > 1)
					{
						string currentId = DataAccessLayer.Instance.ExecuteSimpleQuery("SELECT CurrentID from dbo.SplitID");

						List<SqlParameter> splitParameters = new List<SqlParameter>();
						splitParameters.Add(new SqlParameter("@EntityChangeLogId", EntityChangeLogId.Text));
						string query = @"INSERT INTO dbo.comments (EntityChangeLogId,IncidentEntityID,UserID,EnteredDate,StartTime,TimeInMinutes,Comment,OnSite,Completed,PeerReview,ManagementReview,";
						query += @"Billable,Error,NotToBeInvoiced,Invoiced,CompletedDate,PeerReviewDate,ManagementReviewDate,InvoicedDate,AccountsLock,AccountsLockDate,Export,ExportDate,";
						query += @"VITRExport,VITRExportDate,PurchaseExport,PurchaseExportDate,AHS,CreatedDate) ";
						query += @"SELECT " + currentId + @",IncidentEntityID,UserID,EnteredDate,StartTime,1,Comment,OnSite,Completed,PeerReview,ManagementReview,";
						query += @"Billable,Error,NotToBeInvoiced,Invoiced,CompletedDate,PeerReviewDate,ManagementReviewDate,InvoicedDate,AccountsLock,AccountsLockDate,Export,ExportDate,";
						query += @"VITRExport,VITRExportDate,PurchaseExport,PurchaseExportDate,AHS,CreatedDate ";
						query += @"FROM dbo.comments WHERE EntityChangeLogId=@EntityChangeLogId";
						DataAccessLayer.Instance.ExecuteQuery(query, splitParameters.ToArray());


						List<SqlParameter> splitParameters2 = new List<SqlParameter>();
						splitParameters2.Add(new SqlParameter("@EntityChangeLogId", EntityChangeLogId.Text));
						splitParameters2.Add(new SqlParameter("@TimeInMinutes", (Convert.ToInt32(TimeInMinutes.Text) - 1).ToString()));
						query = @"UPDATE dbo.Comments set TimeInMinutes=@TimeInMinutes WHERE EntityChangeLogId=@EntityChangeLogId";
						DataAccessLayer.Instance.ExecuteQuery(query, splitParameters2.ToArray());

						List<SqlParameter> splitParameters3 = new List<SqlParameter>();
						splitParameters3.Add(new SqlParameter("@CurrentID", (Convert.ToInt32(currentId) + 1).ToString()));
						query = @"UPDATE dbo.SplitID set CurrentID=@CurrentID";
						DataAccessLayer.Instance.ExecuteQuery(query, splitParameters3.ToArray());

						DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);
						query = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
						RefreshGrid(query);
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

			DateTime DaySelected2 = Convert.ToDateTime(txtDaySelected.Text);
			string refreshQuery = "SELECT * FROM dbo.Entries WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND Error=0 AND EnteredBy='" + GetCurrentUser() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected2.ToShortDateString() + "',103) " + txtSortExpression.Text;
			RefreshGrid(refreshQuery);
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{

		}
	}
}