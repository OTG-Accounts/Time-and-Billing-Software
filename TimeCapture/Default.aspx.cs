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


public partial class _Default : System.Web.UI.Page
{
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

                RefreshGrid();
                RefreshButtons(false);
        }
        else
        {
            

        }
    }

    protected void grdComment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.BulkEdit = false;
        this.GridView1.PageIndex = e.NewPageIndex;
        RefreshGrid();
    }



    protected void grdComment_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int EntityChangeLogId = this.GridView1.GetNewValue<int>(e.RowIndex, 0);
        string Comment = this.GridView1.GetNewValue<string>(e.RowIndex, "txtcomment");
        bool Completed = this.GridView1.GetNewValue<bool>(e.RowIndex, "cbCompleted");
        DateTime EnteredDate = this.GridView1.GetNewValue<DateTime>(e.RowIndex, "txtDate");
        string StartTime = this.GridView1.GetNewValue<string>(e.RowIndex, "txtStartTime");
        bool Onsite = this.GridView1.GetNewValue<bool>(e.RowIndex,"cbOnsite");
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
        
        this.GridView1.DataSource = DataAccessLayer.Instance.GetEntities<EntriesView>(query);
        this.GridView1.DataBind();
        
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.GridView1.BulkEdit = true;
        
        RefreshGrid();
        RefreshButtons(true);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //Button1.Visible = false;
        this.GridView1.BulkEdit = true;
        RefreshGrid();
        

        this.GridView1.BulkUpdate();

        this.GridView1.BulkEdit = false;
        
        RefreshGrid();
        RefreshButtons(false);
        //Button1.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.GridView1.BulkEdit = false;
        RefreshGrid();
        RefreshButtons(false);
    }

    private void RefreshButtons(bool editMode)
    {
        if (editMode)
        {
          
            //this.Button1.Visible = true;
        }
        else
        {
          
            //this.Button1.Visible = true;
        }
    }

    protected void ddlCompleted_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshGrid();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        
        string query = "SELECT * FROM dbo.Entries WHERE Completed=" + this.ddlCompleted.SelectedValue + "ORDER BY " + e.SortExpression + " " + GetSortDirection();
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

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        RefreshGrid();
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        RefreshGrid();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        RefreshGrid();
    }
}
