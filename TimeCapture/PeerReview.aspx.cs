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
using System.Drawing;

namespace TimeCapture
{
    public partial class PeerReview : System.Web.UI.Page
    {
        //public static int CurrentDayOfWeek;
        //public static DateTime DaySelected;
        //public static string MyUser;
        //public static string Peer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);
            if (!this.IsPostBack)
            {
                MondayRedDot.Visible = false;
                TuesdayRedDot.Visible = false;
                WednesdayRedDot.Visible = false;
                ThursdayRedDot.Visible = false;
                FridayRedDot.Visible = false;
                SaturdayRedDot.Visible = false;
                SundayRedDot.Visible = false;

                txtDaySelected.Text = DateTime.Now.ToString();
                CurrentDate.Text = DateTime.Now.ToLongDateString();
                txtCurrentDayOfWeek.Text = GetDayOfTheWeek(DateTime.Now).ToString();

                DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);


                
                


                HighlightDayStatus(Convert.ToInt32(txtCurrentDayOfWeek.Text));


                    CurrentDate.Text = DaySelected.ToLongDateString();
                    

                    RefreshGrid("SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text);

                    lblPeer.Text = " " + GetPeer().ToUpper() + " ";
                    CheckRedDotStatus(DaySelected);
          

            }
        }

        protected void btnNextWeek_Click(object sender, ImageClickEventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            DaySelected = DaySelected.AddDays(7);
            CurrentDate.Text = Convert.ToDateTime(DaySelected).ToLongDateString();
            int CurrentDayOfWeek = GetDayOfTheWeek(DaySelected);

            txtDaySelected.Text = DaySelected.ToString();
            txtCurrentDayOfWeek.Text = CurrentDayOfWeek.ToString();

            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
            
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

            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
            
            RefreshGrid(query);
            CheckRedDotStatus(DaySelected);
        }

        protected void btnPreviousWeek_Click(object sender, ImageClickEventArgs e)
        {
            
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            DaySelected = DaySelected.AddDays(-7);
            CurrentDate.Text = Convert.ToDateTime(DaySelected).ToLongDateString();
            int CurrentDayOfWeek = GetDayOfTheWeek(DaySelected);

            txtDaySelected.Text = DaySelected.ToString();
            txtCurrentDayOfWeek.Text = CurrentDayOfWeek.ToString();

            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
            CheckRedDotStatus(DaySelected);

        }

        protected void btnMonday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(1) + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
        }

        protected void btnTuesday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(2) + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
        }

        protected void btnWednesday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(3) + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
        }

        protected void btnThursday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(4) + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
        }

        protected void btnFriday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(5) + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
        }

        protected void btnSaturday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(6) + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
        }

        protected void btnSunday_Click(object sender, ImageClickEventArgs e)
        {
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + GetSearchDate(7) + "',103) " + txtSortExpression.Text;
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
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate>=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate.ToShortDateString() + "',103) " + txtSortExpression.Text;
            RefreshGrid(query);
        }

        protected void ddlCompleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            if (GetDayOfTheWeek(DaySelected) > 0)
            {
                RefreshGrid("SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text);
            }
            else
            {
                DateTime StartDate = DaySelected.AddDays(1 - GetDayOfTheWeek(DaySelected));
                DateTime EndDate = DaySelected.AddDays(7 - GetDayOfTheWeek(DaySelected));
                RefreshGrid("SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' EnteredDate>=CONVERT(datetime,'" + StartDate.ToShortDateString() + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate.ToShortDateString() + "',103) " + txtSortExpression.Text);
            }
        }

        protected void btnMessageBoxOK_Click(object sender, EventArgs e)
        {
            mpeMessageBox.Hide();
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
                            CheckBox cbPeerReview = item.FindControl("cbPeerReview") as CheckBox;

                            List<SqlParameter> parameters = new List<SqlParameter>();
                            parameters.Add(new SqlParameter("@EntityChangeLogId", lblCategoryID.Text));
                            parameters.Add(new SqlParameter("@PeerReview", cbPeerReview.Checked));
                            if (cbPeerReview.Checked) parameters.Add(new SqlParameter("@PeerReviewDate", DateTime.Now));
                            else parameters.Add(new SqlParameter("@PeerReviewDate", DBNull.Value));

                            DataAccessLayer.Instance.ExecuteQuery(@"Update Comments set PeerReview=@PeerReview,PeerReviewDate=@PeerReviewDate where EntityChangeLogId=@EntityChangeLogId", parameters.ToArray());
                        }
                    }
                }
            }


            
            



            RefreshGrid("SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text);
            CheckRedDotStatus(DaySelected);
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            RefreshGrid("SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) " + txtSortExpression.Text);
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
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + Monday.ToShortDateString() + "',103) and PeerReview like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) MondayRedDot.Visible = true;
            QueryReturn = null;
            parameters = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(1).ToShortDateString() + "',103) and PeerReview like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) TuesdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(2).ToShortDateString() + "',103) and PeerReview like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) WednesdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(3).ToShortDateString() + "',103) and PeerReview like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) ThursdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(4).ToShortDateString() + "',103) and PeerReview like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) FridayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(5).ToShortDateString() + "',103) and PeerReview like '0'";
            QueryReturn = DataAccessLayer.Instance.ExecuteQuery(query, parameters.ToArray());
            if (QueryReturn != null) SaturdayRedDot.Visible = true;
            QueryReturn = null;

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            query = "SELECT * FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + Monday.AddDays(6).ToShortDateString() + "',103) and PeerReview like '0'";
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
                query = "SELECT SUM([TimeInMinutes]) FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate=CONVERT(datetime,'" + DaySelected.AddDays(DayNumber - CurrentDayOfWeek).ToShortDateString() + "',103) and error=0 GROUP BY EnteredBY,EnteredDate";
            }
            else
            {
                query = "SELECT SUM([TimeInMinutes]) FROM [Entries] WHERE EnteredBY='" + GetPeer() + "' and EnteredDate>=CONVERT(datetime,'" + DaySelected.AddDays(1 - CurrentDayOfWeek).ToShortDateString() + "',103) and error=0 AND EnteredDate<=CONVERT(datetime,'" + DaySelected.AddDays(7 - CurrentDayOfWeek).ToShortDateString() + "',103) GROUP BY EnteredBY";
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            /*this.GridView1.BulkEdit = false;
            this.GridView1.PageIndex = e.NewPageIndex;
            RefreshGrid("SELECT * FROM dbo.Entries  WHERE Completed like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + MyUser + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103)");
            */
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            
        }

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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string NotDoneYetBackColour = "#FF0000";
            string ManagementReviewBackColour = "#80FF80";

            if (e.Row.Cells.Count >= 3)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDate = e.Row.FindControl("txtDate") as TextBox;
                Label txtStartTime = e.Row.FindControl("txtStartTime") as Label;
                Label txtTimeInMinutes = e.Row.FindControl("txtTimeInMinutes") as Label;
                TextBox txtTitle = e.Row.FindControl("txtTitle") as TextBox;
                TextBox txtComment = e.Row.FindControl("txtComment") as TextBox;
                CheckBox cbOnsite = e.Row.FindControl("cbOnsite") as CheckBox;
                CheckBox cbCompleted = e.Row.FindControl("cbPeerReview") as CheckBox;
                Label lblIncidentID = e.Row.FindControl("lblIncidentID") as Label;
                Label txtCategory = e.Row.FindControl("txtCategory") as Label;
                //CheckBox cbError = e.Row.FindControl("cbError") as CheckBox;
                Label txtSubCategory = e.Row.FindControl("txtSubCategory") as Label;

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

                   // cbError.BackColor = Color.FromName(NotDoneYetBackColour);
                   // cbError.Enabled = false;

                    txtTitle.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtTitle.Enabled = false;

                    txtCategory.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtCategory.Enabled = false;

                    txtSubCategory.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtSubCategory.Enabled = false;

                    txtComment.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtComment.Enabled = false;

                    cbCompleted.BackColor = Color.FromName(NotDoneYetBackColour);
                    cbCompleted.Enabled = false;

                    //PnlFilterHeader.Visible = false;

                }
                else
                {
                    value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ManagementReview"));
                    if (value == "True")
                    {
                        e.Row.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtDate.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtDate.Enabled = false;

                        txtStartTime.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtStartTime.Enabled = false;

                        txtTimeInMinutes.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtTimeInMinutes.Enabled = false;

                        cbOnsite.BackColor = Color.FromName(ManagementReviewBackColour);
                        cbOnsite.Enabled = false;

                       // cbError.BackColor = Color.FromName(ManagementReviewBackColour);
                        //cbError.Enabled = false;

                        txtTitle.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtTitle.Enabled = false;

                        txtSubCategory.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtSubCategory.Enabled = false;

                        txtCategory.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtCategory.Enabled = false;

                        txtComment.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtComment.Enabled = false;

                        cbCompleted.BackColor = Color.FromName(ManagementReviewBackColour);
                        cbCompleted.Enabled = false;

                        //PnlFilterHeader.Visible = false;
                    }
                }
                

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

        private string GetPeer()
        {

            IPrincipal MyUsername = HttpContext.Current.User;
            string Peer = "";

            if (MyUsername != null)
            {
                string query = "Select * from Users where Username='" + MyUsername.Identity.Name.ToString().TrimEnd(' ') + "'";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);


                foreach (UsersEntity CurrentUser in ThisUser)
                {
                    Peer = CurrentUser.PeerReview.TrimEnd(' ');
                }
            }

            return Peer;
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

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            txtSortExpression.Text = "ORDER BY " + e.SortExpression + " " + GetSortDirection();
            DateTime DaySelected = Convert.ToDateTime(txtDaySelected.Text);

            //string query = "SELECT * FROM dbo.Entries WHERE Completed like " + this.ddlCompleted.SelectedValue + "ORDER BY " + e.SortExpression + " " + GetSortDirection();
            string query = "SELECT * FROM dbo.Entries WHERE PeerReview like '" + this.ddlCompleted.SelectedValue + "' AND EnteredBy='" + GetPeer() + "' AND EnteredDate=CONVERT(datetime,'" + DaySelected.ToShortDateString() + "',103) ORDER BY " + e.SortExpression + " " + GetSortDirection();
            RefreshGrid(query);
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string NotDoneYetBackColour = "#FF0000";
            string ManagementReviewBackColour = "#80FF00";



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDate = e.Row.FindControl("txtDate") as TextBox;
                Label txtStartTime = e.Row.FindControl("txtStartTime") as Label;
                Label txtTimeInMinutes = e.Row.FindControl("txtTimeInMinutes") as Label;
                TextBox txtTitle = e.Row.FindControl("txtTitle") as TextBox;
                TextBox txtComment = e.Row.FindControl("txtComment") as TextBox;
                CheckBox cbOnsite = e.Row.FindControl("cbOnsite") as CheckBox;
                CheckBox cbCompleted = e.Row.FindControl("cbPeerReview") as CheckBox;
                Label lblIncidentID = e.Row.FindControl("lblIncidentID") as Label;
                Label txtCategory = e.Row.FindControl("txtCategory") as Label;
                //CheckBox cbError = e.Row.FindControl("cbError") as CheckBox;
                Label txtSubCategory = e.Row.FindControl("txtSubCategory") as Label;

                cbCompleted.Attributes.Add("onclick", "updateEditStatus(\"" + e.Row.FindControl("hdnEditStatus").ClientID + "\");");
                

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

                    //cbError.BackColor = Color.FromName(NotDoneYetBackColour);
                    //cbError.Enabled = false;

                    txtTitle.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtTitle.Enabled = false;

                    txtCategory.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtCategory.Enabled = false;

                    txtSubCategory.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtSubCategory.Enabled = false;

                    txtComment.BackColor = Color.FromName(NotDoneYetBackColour);
                    txtComment.Enabled = false;

                    cbCompleted.BackColor = Color.FromName(NotDoneYetBackColour);
                    cbCompleted.Enabled = false;

                    //PnlFilterHeader.Visible = false;

                }
                else
                {
                    value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ManagementReview"));
                    if (value == "True")
                    {
                        e.Row.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtDate.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtDate.Enabled = false;

                        txtStartTime.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtStartTime.Enabled = false;

                        txtTimeInMinutes.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtTimeInMinutes.Enabled = false;

                        cbOnsite.BackColor = Color.FromName(ManagementReviewBackColour);
                        cbOnsite.Enabled = false;

                        //cbError.BackColor = Color.FromName(ManagementReviewBackColour);
                        //cbError.Enabled = false;

                        txtTitle.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtTitle.Enabled = false;

                        txtSubCategory.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtSubCategory.Enabled = false;

                        txtCategory.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtCategory.Enabled = false;

                        txtComment.BackColor = Color.FromName(ManagementReviewBackColour);
                        txtComment.Enabled = false;

                        cbCompleted.BackColor = Color.FromName(ManagementReviewBackColour);
                        cbCompleted.Enabled = false;

                        //PnlFilterHeader.Visible = false;
                    }
                }
                

            }
        }

    }



}