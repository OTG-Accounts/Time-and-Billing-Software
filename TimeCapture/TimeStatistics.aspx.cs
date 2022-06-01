using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;



namespace TimeCapture
{
    public partial class TimeStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);

            if (!this.IsPostBack)
            {
                txtStartDatePerPerson.Text = DateTime.Now.ToShortDateString();
                txtEndDatePerPerson.Text = DateTime.Now.ToShortDateString();
                

                IPrincipal Username = HttpContext.Current.User;
                string query="";

                if (Username != null)
                {
                    switch(Username.Identity.Name.ToLower().TrimEnd(' '))
                    {
                        default:
                            query = "SELECT * FROM Users WHERE Username = '" + Username.Identity.Name.ToString().TrimEnd(' ') + "'";
                            break;
                    }
                }
                
                List<UsersEntity> AllUsers = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);
                foreach (UsersEntity CurrentUser in AllUsers)
                    ddlEmployee.Items.Add(CurrentUser.DisplayName);

                ddlEmployee.Items.Add("------------");
                ddlEmployee.Items.Add("All");
                ddlEmployee.Items.Add("Service Desk");
                ddlEmployee.Items.Add("Escalations");
                ddlEmployee.Items.Add("Engineers");
                ddlEmployee.Items.Add("Managers");

            }
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            switch (ddlStatSelection.SelectedValue)
            {
                case "LoggedHours":
                    CalculateWorkedHours(txtStartDatePerPerson.Text, txtEndDatePerPerson.Text);
                    break;
                case "Overview":
                    InitialiseTextBoxes();
                    RefreshOverview();
                    break;

            }
            
                
        }

        private void InitialiseTextBoxes()
        {
            tbIncidentCltHourlyCost.Text = "$0";
            tbIncidentIntHourlyCost.Text = "$0";
            tbProjectCltHourlyCost.Text = "$0";
            tbProjectIntHourlyCost.Text = "$0";
            tbInternalHourlyCost.Text = "$0";
            tbDevelopHourlyCost.Text = "$0";
            tbRITSHourlyCost.Text = "$0";
            tbProjectIntTarget.Text = "0";
            tbProjectCltTarget.Text = "0";
            tbIncidentIntTarget.Text = "0";
            
        }

        private void RefreshOverview()
        {
            InitialiseTextBoxes();

            #region Main Variables declaration

            //Variable Declarations
            
            double TotalHrsLog = 0;
            double IncidentCltHrsLog = 0;
            double IncidentIntHrsLog = 0;
            double ProjectCltHrsLog = 0;
            double ProjectIntHrsLog = 0;
            double InternalHrsLog = 0;
            double DevelopHrsLog = 0;
            double RITsHrsLog = 0;
            double TotalDays = 0;
            double TotalYears = 0;
            double TotalWorkingDays = 0;
            double TotalUserCost = 0;
            double IncidentCltBillableHrs = 0;
            double ProjectCltBillableHrs = 0;
            double IncidentIntBillableHrs = 0;
            double ProjectIntBillableHrs = 0;
            double InternalBillableHrs = 0;
            double DevelopBillableHrs = 0;
            double RITsBillableHrs = 0;
            double TotalRITsHours = 0;
            double CompanyCostEmpPerDay = 0;
            double IncidentCltBilled = 0;
            double ProjectCltBilled = 0;
            double IncidentCltPotBilled = 0;
            double ProjectCltPotBilled = 0;
            double IncidentIntBilled = 0;
            double ProjectIntBilled = 0;
            double IncidentIntPotBilled = 0;
            double ProjectIntPotBilled = 0;
            double DevelopBilled = 0;
            
            double DevelopPotBilled = 0;
            double IntAccHrsLog = 0;
            double IntAdmHrsLog = 0;
            double IntBusHrsLog = 0;
            double IntCmsHrsLog = 0;
            double IntCRHrsLog = 0;
            double IntHRHrsLog = 0;
            double IntMgmtHrsLog = 0;
            double IntOpsHrsLog = 0;
            double IntPerHrsLog = 0;
            double IntPrjHrsLog = 0;
            double IntRLHrsLog = 0;
            double IntSMHrsLog = 0;
            double IntTmsHrsLog = 0;
            double IntUnkHrsLog = 0;
            double IntTotalHrsLog = 0;
            double TotalInternalCost = 0;
            double SCHrsLog = 0;
            double STHrsLog = 0;
            double CCHrsLog = 0;
            double CTHrsLog = 0;
            double UCHrsLog = 0;
            double UTHrsLog = 0;
            double OCHrsLog = 0;
            double OTHrsLog = 0;
            double SRHrsLog = 0;
            double RITsWK1HrsLog = 0;
            double RITsWK2HrsLog = 0;
            double RITsWK3HrsLog = 0;
            double RITsWK4HrsLog = 0;
            double RITsSCMHrsLog = 0;
            double RITsSCCMMMHrsLog = 0;
            double RITsDayHrsLog = 0;
            double RITsOthHrsLog = 0;
            double RITsTRSHrsLog = 0;
            double SAMsHrsLog = 0;
            double SAMsBillableHrs = 0;
            double SAMsMonthlyHrsTotal = 0;
            double SAMsSCOMMonHrsLog = 0;
            double SAMsSCOMMMHrsLog = 0;
            double SAMsOthHrsLog =0;
            double TotalSAMsHours = 0;

            double IncidentCltActualRevenue = 0;
            double IncidentCltPotentialRevenue = 0;
            double ProjectCltActualRevenue = 0;
            double ProjectCltPotentialRevenue = 0;
            double IncidentIntActualRevenue = 0;
            double IncidentIntPotentialRevenue = 0;
            double ProjectIntActualRevenue = 0;
            double ProjectIntPotentialRevenue = 0;
            double InternalActualRevenue = 0;
            double InternalPotentialRevenue = 0;
            double DevelopActualRevenue = 0;
            double DevelopPotentialRevenue = 0;
            double RITsBilled = 0;
            double RITsPotBilled = 0;
            double SAMsBilled = 0;
            double SAMsPotBilled = 0;
            double RITSActualRevenue = 0;
            double RITSPotentialRevenue = 0;
            double SAMsActualRevenue = 0;
            double SAMsPotentialRevenue=0;

            //Variables for Targets
            double OPSTarget=0;
            double ACTTarget=0;
            double ADMTarget=0;
            double BUSTarget=0;
            double RLTarget=0;
            double PRJTarget=0;
            double COMTarget=0;
            double HRTarget=0;
            double CRMTarget=0;
            double SMTarget=0;
            double TMSTarget=0;
            double MGTTarget=0;
            double PERTarget=0;
            double DevelopTarget=0;
            double DAYTarget=0;
            double WK1Target=0;
            double WK2Target=0;
            double WK3Target=0;
            double WK4Target=0;
            double SCMTarget=0;
            double TRSTarget=0;
            double OTHTarget=0;
            double CCTarget=0;
            double CTTarget=0;
            double SCTarget=0;
            double STTarget=0;
            double UCTarget=0;
            double UTTarget=0;
            double OCTarget=0;
            double OTTarget=0;
            double ProjectIntTarget=0;
            double ProjectCltTarget=0;
            double IncidentIntTarget=0;
            double ServiceRequestTarget=0;
            double SCOMMonTarget=0;
            double SCOMMMTarget=0;
            double SAMsOthTarget=0;
            double RITsSCCMTarget=0;

            //variables for Chart
            ArrayList ChartMonthlyPct = new ArrayList();
            double MonthlyHrsTotal = 0;
            double InternalMonthlyHrsTotal = 0;
            double IncidentCltMonthlyHrsTotal = 0;
            double IncidentIntMonthlyHrsTotal = 0;
            double ProjectCltMonthlyHrsTotal = 0;
            double ProjectIntMonthlyHrsTotal = 0;
            double RITsMonthlyHrsTotal = 0;
            double DevelopMonthlyHrsTotal = 0;
            DateTime PreviousDate = Convert.ToDateTime("01/01/1900");
            bool FirstRecord = true;


            DateTime PreviousDay = Convert.ToDateTime("01/01/1900");
            double MinutesWorkedDuringDay = 0;
            bool FirstDayRecord = true;

            string[] MonthsName = {"Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"};

            #endregion

            /*
            tbExpensesYear2.Text = String.Format("{0:C0}", Convert.ToDouble(tbExpensesYear.Text));
            tbRITSYear2.Text = String.Format("{0:C0}", Convert.ToDouble(tbRITSYear.Text));
             */

            DateTime StartDate = Convert.ToDateTime(txtStartDateOverview.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDateOverview.Text);

            TotalDays = (EndDate - StartDate).TotalDays + 1;
            TotalYears = TotalDays / 365;
            

            
            tbTotalDays.Text = Math.Round(TotalDays, 2).ToString();
            tbExpensesHour.Text = String.Format("{0:C2}", Math.Round((Convert.ToDouble(tbExpensesYear.Text) / (8 * 5 * 52)), 2));
            tbRITSHour.Text = String.Format("{0:C2}", Math.Round(Convert.ToDouble(tbRITSYear.Text) / (8 * 5 * 52), 2));
            tbExpensesEmployeePerHour.Text = String.Format("{0:C2}", Math.Round((Convert.ToDouble(tbExpensesYear.Text) / 52 / 5 / 8 / Convert.ToDouble(tbNumberOfEmployees.Text)), 2));
            


            tbExpensesMonth.Text = String.Format("{0:C0}", Math.Round((Convert.ToDouble(tbExpensesYear.Text) / 12), 2));
            tbExpensesDay.Text = String.Format("{0:C2}", Math.Round((Convert.ToDouble(tbExpensesYear.Text) / (5 * 52)), 2));
            
            tbExpensesEmployeePerYear.Text = String.Format("{0:C0}", Math.Round((Convert.ToDouble(tbExpensesYear.Text) / Convert.ToDouble(tbNumberOfEmployees.Text)), 2));
            tbExpensesEmployeePerMonth.Text = String.Format("{0:C0}", Math.Round((Convert.ToDouble(tbExpensesYear.Text) / 12 / Convert.ToDouble(tbNumberOfEmployees.Text)), 2));
             
            CompanyCostEmpPerDay = Math.Round((Convert.ToDouble(tbExpensesYear.Text) / 52 / 5 / Convert.ToDouble(tbNumberOfEmployees.Text)), 2);
            
            tbExpensesEmployeePerDay.Text = String.Format("{0:C2}", CompanyCostEmpPerDay);
            
            

            #region Get user targets
            //Get the user targets
            string queryUserTarget = "SELECT * FROM viewUserTarget WHERE DisplayName = '";
            queryUserTarget += ddlEmployee.SelectedValue.ToString();
            queryUserTarget += "'";
            List<UserTargetView> UserTarget = DataAccessLayer.Instance.GetEntities<UserTargetView>(queryUserTarget);

            
            foreach (UserTargetView CurrentTarget in UserTarget)
            {
                switch (CurrentTarget.Category)
                {
                    case "OPS":
                        OPSTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "ACT":
                        ACTTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "ADM":
                        ADMTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "BUS":
                        BUSTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "RL":
                        RLTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "PRJ":
                        PRJTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "COM":
                        COMTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "HR":
                        HRTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "CRM":
                        CRMTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SM":
                        SMTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "TMS":
                        TMSTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "MGT":
                        MGTTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "PER":
                        PERTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SFT":
                        DevelopTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "RITs":
                        break;
                    case "DAY":
                        DAYTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "WK1":
                        WK1Target = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "WK2":
                        WK2Target = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "WK3":
                        WK3Target = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "WK4":
                        WK4Target = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SCM":
                        SCMTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "TRS":
                        TRSTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "OTH":
                        OTHTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "CC":
                        CCTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "CT":
                        CTTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SC":
                        SCTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "ST":
                        STTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "UC":
                        UCTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "UT":
                        UTTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "OC":
                        OCTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "OT":
                        OTTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "Project Int":
                        ProjectIntTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "Project":
                        ProjectCltTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "Incident Int":
                        IncidentIntTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "Service Request":
                        ServiceRequestTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SCOMMon":
                        SCOMMonTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SCOMMM":
                        SCOMMMTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SAMsOther":
                        SAMsOthTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SCCMMM":
                        RITsSCCMTarget = Convert.ToDouble(CurrentTarget.Target);
                        break;
                    case "SAMs":
                        break;
                    
                }
            }

            tbOPSTarget.Text = OPSTarget.ToString();
            tbACTTarget.Text = ACTTarget.ToString();
            tbADMTarget.Text = ADMTarget.ToString();
            tbBUSTarget.Text = BUSTarget.ToString();
            tbRLTarget.Text = RLTarget.ToString();
            tbPRJTarget.Text = PRJTarget.ToString();
            tbCOMTarget.Text = COMTarget.ToString();
            tbHRTarget.Text = HRTarget.ToString();
            tbCRMTarget.Text = CRMTarget.ToString();
            tbSMTarget.Text = SMTarget.ToString();
            tbTMSTarget.Text = TMSTarget.ToString();    
            tbMGTTarget.Text = MGTTarget.ToString();
            tbPERTarget.Text = PERTarget.ToString();
            tbDevelopTarget.Text = DevelopTarget.ToString();
            tbDAYTarget.Text = DAYTarget.ToString();
            tbWK1Target.Text = WK1Target.ToString();
            tbWK2Target.Text = WK2Target.ToString();
            tbWK3Target.Text = WK3Target.ToString();
            tbWK4Target.Text = WK4Target.ToString();
            tbSCMTarget.Text = SCMTarget.ToString();
            tbTRSTarget.Text = TRSTarget.ToString();
            tbOTHTarget.Text = OTHTarget.ToString();
            tbCCTarget.Text = CCTarget.ToString();
            tbCTTarget.Text = CTTarget.ToString();
            tbSCTarget.Text = SCTarget.ToString();
            tbSTTarget.Text = STTarget.ToString();
            tbUCTarget.Text = UCTarget.ToString();
            tbUTTarget.Text = UTTarget.ToString();
            tbOCTarget.Text = OCTarget.ToString();
            tbOTTarget.Text = OTTarget.ToString();
            tbProjectIntTarget.Text = ProjectIntTarget.ToString();
            tbProjectCltTarget.Text = ProjectCltTarget.ToString();
            tbIncidentIntTarget.Text = IncidentIntTarget.ToString();
            tbServiceRequestTarget.Text = ServiceRequestTarget.ToString();
            tbSCOMMonTarget.Text = SCOMMonTarget.ToString();
            tbSCOMMMTarget.Text = SCOMMMTarget.ToString();
            tbSAMsOthTarget.Text = SAMsOthTarget.ToString();
            tbRITsSCCMTarget.Text = RITsSCCMTarget.ToString();


            tbInternalTarget.Text = (Convert.ToDouble(tbOPSTarget.Text) + Convert.ToDouble(tbACTTarget.Text) + Convert.ToDouble(tbADMTarget.Text) + Convert.ToDouble(tbBUSTarget.Text) + Convert.ToDouble(tbRLTarget.Text)
                + Convert.ToDouble(tbPRJTarget.Text) + Convert.ToDouble(tbCOMTarget.Text) + Convert.ToDouble(tbHRTarget.Text) + Convert.ToDouble(tbCRMTarget.Text) + Convert.ToDouble(tbSMTarget.Text)
                + Convert.ToDouble(tbTMSTarget.Text) + Convert.ToDouble(tbMGTTarget.Text) + Convert.ToDouble(tbPERTarget.Text)).ToString();

            tbRITsTarget.Text = (Convert.ToDouble(tbDAYTarget.Text) + Convert.ToDouble(tbWK1Target.Text) + Convert.ToDouble(tbWK2Target.Text) + Convert.ToDouble(tbWK3Target.Text) + Convert.ToDouble(tbWK4Target.Text)
                + Convert.ToDouble(tbSCMTarget.Text) + Convert.ToDouble(tbTRSTarget.Text) + Convert.ToDouble(tbOTHTarget.Text) + Convert.ToDouble(tbRITsSCCMTarget.Text)).ToString();

            tbSAMsTarget.Text = (Convert.ToDouble(tbSCOMMMTarget.Text) + Convert.ToDouble(tbSCOMMonTarget.Text) + Convert.ToDouble(tbSAMsOthTarget.Text)).ToString();

            tbIncidentCltTarget.Text = (Convert.ToDouble(tbCCTarget.Text) + Convert.ToDouble(tbCTTarget.Text) + Convert.ToDouble(tbSCTarget.Text) + Convert.ToDouble(tbSTTarget.Text) + Convert.ToDouble(tbUCTarget.Text)
                + Convert.ToDouble(tbUTTarget.Text) + Convert.ToDouble(tbOCTarget.Text) + Convert.ToDouble(tbOTTarget.Text) + Convert.ToDouble(tbServiceRequestTarget.Text)).ToString();

            

            #endregion

            string SelectStartDate = StartDate.ToString("yyyy-MM-dd");
            string SelectEndDate = EndDate.ToString("yyyy-MM-dd");
            //Get the User costs
            string queryUserCost = "";
            string queryEntries = "";
            string QueryNumberofUsers = "0";
            switch(ddlEmployee.SelectedValue.ToString())
            {
                case "------------":
                case "All":
                    queryUserCost = "SELECT * FROM viewUserCost WHERE ValidFrom >='";
                    queryUserCost += SelectStartDate;
                    queryUserCost += "' AND ValidFrom<='";
                    queryUserCost += SelectEndDate;
                    queryUserCost += "'";

                    queryEntries = "SELECT * FROM Entries WHERE EnteredDate>='";
                    queryEntries += SelectStartDate;
                    queryEntries += "' AND EnteredDate<='";
                    queryEntries += SelectEndDate;
                    queryEntries += "' AND Error=0 ORDER BY EnteredDate";

                    QueryNumberofUsers = "SELECT count(distinct(enteredby)) FROM Entries WHERE EnteredDate>='";
                    QueryNumberofUsers += SelectStartDate;
                    QueryNumberofUsers += "' AND EnteredDate<='";
                    QueryNumberofUsers += SelectEndDate;
                    QueryNumberofUsers += "' AND Error=0";
                    break;
                case "Service Desk":
                case "Escalations":
                case "Engineers":
                    queryUserCost = "SELECT * FROM viewUserCost WHERE [Group]= '";
                    queryUserCost += ddlEmployee.SelectedValue.ToString();
                    queryUserCost += "' AND ValidFrom >='";
                    queryUserCost += SelectStartDate;
                    queryUserCost += "' AND ValidFrom<='";
                    queryUserCost += SelectEndDate;
                    queryUserCost += "'";

                    queryEntries = "SELECT * FROM Entries WHERE EnteredDate>='";
                    queryEntries += SelectStartDate;
                    queryEntries += "' AND EnteredDate<='";
                    queryEntries += SelectEndDate;
                    queryEntries += "' AND [Group]='";
                    queryEntries += ddlEmployee.SelectedValue.ToString();
                    queryEntries += "' AND Error=0 ORDER BY EnteredDate";

                    QueryNumberofUsers = "SELECT count(distinct(enteredby)) FROM Entries WHERE EnteredDate>='";
                    QueryNumberofUsers += SelectStartDate;
                    QueryNumberofUsers += "' AND EnteredDate<='";
                    QueryNumberofUsers += SelectEndDate;
                    QueryNumberofUsers += "' AND [Group]='";
                    QueryNumberofUsers += ddlEmployee.SelectedValue.ToString();
                    QueryNumberofUsers += "' AND Error=0";

                    break;
                case "Managers":
                    queryUserCost = "SELECT * FROM viewUserCost WHERE ([Group]='Managers' OR [Group]='Administrators') AND ValidFrom >='";
                    queryUserCost += SelectStartDate;
                    queryUserCost += "' AND ValidFrom<='";
                    queryUserCost += SelectEndDate;
                    queryUserCost += "'";

                    queryEntries = "SELECT * FROM Entries WHERE EnteredDate>='";
                    queryEntries += SelectStartDate;
                    queryEntries += "' AND EnteredDate<='";
                    queryEntries += SelectEndDate;
                    queryEntries += "' AND ([Group]='Managers' OR [Group]='Administrators') AND Error=0 ORDER BY EnteredDate";

                    QueryNumberofUsers = "SELECT count(distinct(enteredby)) FROM Entries WHERE EnteredDate>='";
                    QueryNumberofUsers += SelectStartDate;
                    QueryNumberofUsers += "' AND EnteredDate<='";
                    QueryNumberofUsers += SelectEndDate;
                    QueryNumberofUsers += "' AND ([Group]='Managers' OR [Group]='Administrators') AND Error=0";
                    break;
                default:
                    queryUserCost = "SELECT * FROM viewUserCost WHERE DisplayName = '";
                    queryUserCost += ddlEmployee.SelectedValue.ToString();
                    queryUserCost += "' AND ValidFrom >='";
                    queryUserCost += SelectStartDate;
                    queryUserCost += "' AND ValidFrom<='";
                    queryUserCost += SelectEndDate;
                    queryUserCost += "'";

                    queryEntries = "SELECT * FROM Entries WHERE EnteredDate>='";
                    queryEntries += SelectStartDate;
                    queryEntries += "' AND EnteredDate<='";
                    queryEntries += SelectEndDate;
                    queryEntries += "' AND EnteredBy='";
                    queryEntries += ddlEmployee.SelectedValue.ToString();
                    queryEntries += "' AND Error=0 ORDER BY EnteredDate";

                    QueryNumberofUsers = "SELECT count(distinct(enteredby)) FROM Entries WHERE EnteredDate>='";
                    QueryNumberofUsers += SelectStartDate;
                    QueryNumberofUsers += "' AND EnteredDate<='";
                    QueryNumberofUsers += SelectEndDate;
                    QueryNumberofUsers += "' AND EnteredBy='";
                    QueryNumberofUsers += ddlEmployee.SelectedValue.ToString();
                    QueryNumberofUsers += "' AND Error=0";
                    break;
            }
        
            
            List<UserCostView> UserCost = DataAccessLayer.Instance.GetEntities<UserCostView>(queryUserCost);
            foreach (UserCostView ThisCost in UserCost)
                TotalUserCost += ThisCost.Cost;

            //Get the total RITs hours for the period across all employees
            string queryRITsEntries = "SELECT * FROM Entries WHERE EnteredDate>='";
            queryRITsEntries += SelectStartDate;
            queryRITsEntries += "' AND EnteredDate<='";
            queryRITsEntries += SelectEndDate;
            queryRITsEntries += "' AND Error=0 AND Category='RITs'";
            List<EntriesView> AllRITsEntries = DataAccessLayer.Instance.GetEntities<EntriesView>(queryRITsEntries);
            foreach (EntriesView CurrentRITsEntry in AllRITsEntries)
                TotalRITsHours += Convert.ToDouble(CurrentRITsEntry.TimeInMinutes) / 60;



            string NumberOfUsers = DataAccessLayer.Instance.ExecuteSimpleQuery(QueryNumberofUsers);

            //Get the total SAMs hours for the period across all employees
            string querySAMsEntries = "SELECT * FROM Entries WHERE EnteredDate>='";
            querySAMsEntries += SelectStartDate;
            querySAMsEntries += "' AND EnteredDate<='";
            querySAMsEntries += SelectEndDate;
            querySAMsEntries += "' AND Error=0 AND Category='SAMs'";
            List<EntriesView> AllSAMsEntries = DataAccessLayer.Instance.GetEntities<EntriesView>(querySAMsEntries);
            foreach (EntriesView CurrentSAMsEntry in AllSAMsEntries)
                TotalSAMsHours += Convert.ToDouble(CurrentSAMsEntry.TimeInMinutes) / 60;
         
            //Get all entries for the selected employee
            

            List<EntriesView> AllEntries = DataAccessLayer.Instance.GetEntities<EntriesView>(queryEntries);

            #region Main loop with data calculation

            foreach (EntriesView CurrentEntry in AllEntries)
            {


                switch (CurrentEntry.Category)
                {
                    case "Internal":
                        InternalHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        InternalMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        switch (CurrentEntry.SubCategory)
                        {
                            case "PRJ":
                                IntPrjHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "CR":
                                IntCRHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "SM":
                                IntSMHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "MGT":
                                IntMgmtHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "TMS":
                                IntTmsHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "BUS":
                                IntBusHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "HR":
                                IntHRHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "COM":
                                IntCmsHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "CRM":
                                IntCRHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "PER":
                                IntPerHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "OPS":
                                IntOpsHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "ACT":
                                IntAccHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "RL":
                                IntRLHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "ADM":
                                IntAdmHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            default:
                                IntUnkHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                        }


                        break;
                    case "RITs":
                        RITsHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        RITsBillableHrs += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        RITsMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        switch (CurrentEntry.SubCategory)
                        {
                            case "WK1":
                                RITsWK1HrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "WK2":
                                RITsWK2HrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "WK3":
                                RITsWK3HrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "WK4":
                                RITsWK4HrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "DAY":
                                RITsDayHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "SCM":
                                RITsSCMHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "SCCMMM":
                                RITsSCCMMMHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "TRS":
                                RITsTRSHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "RITs":
                            case "OTH":
                                RITsOthHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;

                        }
                        break;
                    case "SAMs":
                        SAMsHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        SAMsBillableHrs += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        SAMsMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        switch (CurrentEntry.SubCategory)
                        {
                            case "SCOMMon":
                                SAMsSCOMMonHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "SCOMMM":
                                SAMsSCOMMMHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;
                            case "SAMs":
                            case "SAMsOther":
                                SAMsOthHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                break;

                        }
                        break;
                    case "Development":
                        DevelopHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        DevelopMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                        /*if (chkAdjust.Checked)
                        {
                            DevelopBillableHrs += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            DevelopBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbOffsiteRate.Text));
                            DevelopPotBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbOffsiteRate.Text));
                        }*/
                        break;
                    case "Project":
                        if (CurrentEntry.CompanyID == "INTERNAL_ID")
                        {
                            ProjectCltMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            ProjectIntHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            if (chkAdjust.Checked)
                            {
                                ProjectIntBillableHrs += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                ProjectIntBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbProjectIntRate.Text));
                                ProjectIntPotBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbProjectIntRate.Text));
                            }

                        }
                        else
                        {
                            ProjectCltHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            ProjectIntMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            if (CurrentEntry.Billable)
                            {
                                ProjectCltBillableHrs += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                if (CurrentEntry.OnSite)
                                    ProjectCltBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 60)) / 60 * Convert.ToDouble(tbProjectCltOnsiteRate.Text));
                                else
                                    ProjectCltBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbProjectCltOffsiteRate.Text));
                            }
                            if (CurrentEntry.OnSite)
                                ProjectCltPotBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 60)) / 60 * Convert.ToDouble(tbProjectCltOnsiteRate.Text));
                            else
                                ProjectCltPotBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbProjectCltOffsiteRate.Text));
                        }
                        break;
                    default:
                        if (CurrentEntry.CompanyID == "INTERNAL_ID")
                        {
                            IncidentIntHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            IncidentIntMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            if (chkAdjust.Checked)
                            {
                                IncidentIntBillableHrs += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                IncidentIntBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbIncidentIntRate.Text));
                                IncidentIntPotBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbIncidentIntRate.Text));
                            }



                        }
                        else
                        {
                            IncidentCltHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            IncidentCltMonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                            if (CurrentEntry.Billable)
                            {
                                IncidentCltBillableHrs += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                if (CurrentEntry.OnSite)
                                    IncidentCltBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 60)) / 60 * Convert.ToDouble(tbIncidentCltOnsiteRate.Text));
                                else
                                    IncidentCltBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbIncidentCltOffsiteRate.Text));
                            }
                            if (CurrentEntry.OnSite)
                                IncidentCltPotBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 60)) / 60 * Convert.ToDouble(tbIncidentCltOnsiteRate.Text));
                            else
                                IncidentCltPotBilled += (Convert.ToDouble(RoundUpMinutes(Convert.ToInt32(CurrentEntry.TimeInMinutes), 30)) / 60 * Convert.ToDouble(tbIncidentCltOffsiteRate.Text));

                            switch (CurrentEntry.SubCategory)
                            {
                                case "SC":
                                    SCHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "ST":
                                    STHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "CC":
                                    CCHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "CT":
                                    CTHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "UC":
                                    UCHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "UT":
                                    UTHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "OC":
                                    OCHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "OT":
                                    OTHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                                case "":
                                    SRHrsLog += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                                    break;
                            }


                        }



                        break;
                }


                MonthlyHrsTotal += (Convert.ToDouble(CurrentEntry.TimeInMinutes) / 60);
                if (FirstRecord)
                {
                    PreviousDate = Convert.ToDateTime(CurrentEntry.EnteredDate);
                    FirstRecord = false;
                }

                if (PreviousDate.Month != Convert.ToDateTime(CurrentEntry.EnteredDate).Month)
                {

                    ChartMonthlyPct.Add(new ChartRecord((InternalMonthlyHrsTotal / MonthlyHrsTotal), (IncidentCltMonthlyHrsTotal / MonthlyHrsTotal), (IncidentIntMonthlyHrsTotal / MonthlyHrsTotal), (ProjectCltMonthlyHrsTotal / MonthlyHrsTotal), (ProjectIntMonthlyHrsTotal / MonthlyHrsTotal), (RITsMonthlyHrsTotal / MonthlyHrsTotal), (SAMsMonthlyHrsTotal / MonthlyHrsTotal), (DevelopMonthlyHrsTotal / MonthlyHrsTotal), MonthsName[PreviousDate.Month - 1]));
                    
                    PreviousDate = Convert.ToDateTime(CurrentEntry.EnteredDate);
                    InternalMonthlyHrsTotal = 0;
                    IncidentCltMonthlyHrsTotal = 0;
                    IncidentIntMonthlyHrsTotal = 0;
                    ProjectCltMonthlyHrsTotal = 0;
                    ProjectIntMonthlyHrsTotal = 0;
                    RITsMonthlyHrsTotal = 0;
                    DevelopMonthlyHrsTotal = 0;
                    MonthlyHrsTotal = 0;
                }


                if (FirstDayRecord)
                {
                    PreviousDay = Convert.ToDateTime(CurrentEntry.EnteredDate);
                    FirstDayRecord = false;
                }

                if (PreviousDay != Convert.ToDateTime(CurrentEntry.EnteredDate))
                {

                    MinutesWorkedDuringDay = MinutesWorkedDuringDay / Convert.ToInt32(NumberOfUsers);

                    if (MinutesWorkedDuringDay >= 30 && MinutesWorkedDuringDay <= 150)
                        TotalWorkingDays += 0.25;
                    else
                    {
                        if (MinutesWorkedDuringDay <= 270)
                            TotalWorkingDays += 0.5;
                        else
                        {
                            if (MinutesWorkedDuringDay <= 390)
                                TotalWorkingDays += 0.75;
                            else
                                TotalWorkingDays += 1;
                        }
                    }

                    PreviousDay = Convert.ToDateTime(CurrentEntry.EnteredDate);
                    MinutesWorkedDuringDay = Convert.ToDouble(CurrentEntry.TimeInMinutes); ;
                }
                else
                    MinutesWorkedDuringDay += Convert.ToDouble(CurrentEntry.TimeInMinutes);
    
                
            }

            MinutesWorkedDuringDay = MinutesWorkedDuringDay / Convert.ToInt32(NumberOfUsers);

            if (MinutesWorkedDuringDay >= 30 && MinutesWorkedDuringDay <= 150)
                TotalWorkingDays += 0.25;
            else
            {
                if (MinutesWorkedDuringDay <= 270)
                    TotalWorkingDays += 0.5;
                else
                {
                    if (MinutesWorkedDuringDay <= 390)
                        TotalWorkingDays += 0.75;
                    else
                        TotalWorkingDays += 1;
                }
            }

            
            ChartMonthlyPct.Add(new ChartRecord((InternalMonthlyHrsTotal / MonthlyHrsTotal), (IncidentCltMonthlyHrsTotal / MonthlyHrsTotal), (IncidentIntMonthlyHrsTotal / MonthlyHrsTotal), (ProjectCltMonthlyHrsTotal / MonthlyHrsTotal), (ProjectIntMonthlyHrsTotal / MonthlyHrsTotal), (RITsMonthlyHrsTotal / MonthlyHrsTotal), (SAMsMonthlyHrsTotal / MonthlyHrsTotal), (DevelopMonthlyHrsTotal / MonthlyHrsTotal), MonthsName[PreviousDate.Month - 1]));

            #endregion


            #region Chart drawing

            //Drawing charts
            myChart.DataSource = ChartMonthlyPct;

            myChart.Legends.Add(new Legend("Default") { Docking = Docking.Right });
            

            //if(InternalMonthlyHrsTotal > 0)
            //{
            Series InternalSerie = new Series("Internal", 12);
            
            InternalSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(InternalSerie);
            InternalSerie.YValueMembers = "InternalPercent";
            InternalSerie.XValueMember = "Month";
            InternalSerie.IsValueShownAsLabel = true;
            InternalSerie.Label = "#VAL{P2}";
            InternalSerie.Color = Color.Red;
            InternalSerie.LabelForeColor = Color.White;
            
            //}
            //if (IncidentCltMonthlyHrsTotal > 0)
            //{
            Series IncidentCltSerie = new Series("Incident Client", 12);
            IncidentCltSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(IncidentCltSerie);
            IncidentCltSerie.YValueMembers = "IncidentCltPercent";
            IncidentCltSerie.XValueMember = "Month";
            IncidentCltSerie.IsValueShownAsLabel = true;
            IncidentCltSerie.Label = "#VAL{P2}";
            IncidentCltSerie.Color = Color.Green;
            IncidentCltSerie.LabelForeColor = Color.White;
            //}
            //if (IncidentIntMonthlyHrsTotal > 0)
            //{
            Series IncidentIntSerie = new Series("Incident Internal", 12);
            IncidentIntSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(IncidentIntSerie);
            IncidentIntSerie.YValueMembers = "IncidentIntPercent";
            IncidentIntSerie.XValueMember = "Month";
            IncidentIntSerie.IsValueShownAsLabel = true;
            IncidentIntSerie.Label = "#VAL{P2}";
            IncidentIntSerie.Color = Color.Orange;
            IncidentIntSerie.LabelForeColor = Color.White;
            //}
            //if (ProjectCltMonthlyHrsTotal > 0)
            //{
            Series ProjectCltSerie = new Series("Project Client", 12);
            ProjectCltSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(ProjectCltSerie);
            ProjectCltSerie.YValueMembers = "ProjectIntPercent";
            ProjectCltSerie.XValueMember = "Month";
            ProjectCltSerie.IsValueShownAsLabel = true;
            ProjectCltSerie.Label = "#VAL{P2}";
            ProjectCltSerie.Color = Color.Blue;
            ProjectCltSerie.LabelForeColor = Color.White;

            //}
            //if (ProjectIntMonthlyHrsTotal > 0)
            //{
            Series ProjectIntSerie = new Series("Project Internal", 12);
            ProjectIntSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(ProjectIntSerie);
            ProjectIntSerie.YValueMembers = "ProjectCltPercent";
            ProjectIntSerie.XValueMember = "Month";
            ProjectIntSerie.IsValueShownAsLabel = true;
            ProjectIntSerie.Label = "#VAL{P2}";
            ProjectIntSerie.Color = Color.Gray;
            ProjectIntSerie.LabelForeColor = Color.White;

            //}
            //if (RITsMonthlyHrsTotal > 0)
            //{ 
            Series RITsSerie = new Series("RITs Internal", 12);
            RITsSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(RITsSerie);
            RITsSerie.YValueMembers = "RITsPercent";
            RITsSerie.XValueMember = "Month";
            RITsSerie.IsValueShownAsLabel = true;
            RITsSerie.Label = "#VAL{P2}";
            RITsSerie.Color = Color.Purple;
            RITsSerie.LabelForeColor = Color.White;

            Series SAMsSerie = new Series("SAMs Internal", 12);
            SAMsSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(SAMsSerie);
            SAMsSerie.YValueMembers = "SAMsPercent";
            SAMsSerie.XValueMember = "Month";
            SAMsSerie.IsValueShownAsLabel = true;
            SAMsSerie.Label = "#VAL{P2}";
            SAMsSerie.Color = Color.Black;
            SAMsSerie.LabelForeColor = Color.White;
       
            //}

            //{ 
            Series DevelopSerie = new Series("Dev Internal", 12);
            DevelopSerie.ChartType = SeriesChartType.StackedColumn100;
            myChart.Series.Add(DevelopSerie);
            DevelopSerie.YValueMembers = "DevelopPercent";
            DevelopSerie.XValueMember = "Month";
            if (DevelopMonthlyHrsTotal > 0)
                DevelopSerie.IsValueShownAsLabel = true;
            else
                DevelopSerie.IsValueShownAsLabel = false;
            DevelopSerie.Label = "#VAL{P2}";
            DevelopSerie.Color = Color.Pink;
            DevelopSerie.LabelForeColor = Color.White;

            //}

            #endregion

            
              
            tbTotalWorkingDays.Text = Math.Round(TotalWorkingDays, 2).ToString();
            /*
            tbTotalWorkingWeeks.Text = Math.Round(TotalWorkingDays / 5, 2).ToString();

            */


            #region Costs vs profits


            TotalHrsLog = InternalHrsLog + RITsHrsLog + DevelopHrsLog + ProjectIntHrsLog + ProjectCltHrsLog + IncidentIntHrsLog + IncidentCltHrsLog + SAMsHrsLog;
            tbTotalHrsLog.Text = Math.Round(TotalHrsLog, 2).ToString();
            tbIncidentCltHrsLog.Text = Math.Round(IncidentCltHrsLog, 2).ToString();
            tbIncidentIntHrsLog.Text = Math.Round(IncidentIntHrsLog, 2).ToString();
            tbProjectCltHrsLog.Text = Math.Round(ProjectCltHrsLog, 2).ToString();
            tbProjectIntHrsLog.Text = Math.Round(ProjectIntHrsLog, 2).ToString();
            tbInternalHrsLog.Text = Math.Round(InternalHrsLog, 2).ToString();
            tbDevelopHrsLog.Text = Math.Round(DevelopHrsLog, 2).ToString();
            tbRITSHrsLog.Text = Math.Round(RITsHrsLog, 2).ToString();
            tbSAMsHrsLog.Text = Math.Round(SAMsHrsLog, 2).ToString();

            tbIncidentCltPctLog.Text = String.Format("{0:P2}", (IncidentCltHrsLog / TotalHrsLog));
            
            
            if ((IncidentCltHrsLog / TotalHrsLog * 100) < Convert.ToDouble(tbIncidentCltTarget.Text))
            {
                tbIncidentCltPctLog.ForeColor = Color.White;
                tbIncidentCltPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbIncidentCltPctLog.ForeColor = Color.White;
                tbIncidentCltPctLog.BackColor = Color.ForestGreen;
            }
            
            tbIncidentIntPctLog.Text = String.Format("{0:P2}", (IncidentIntHrsLog / TotalHrsLog));

            if ((IncidentIntHrsLog / TotalHrsLog * 100) > Convert.ToDouble(tbIncidentIntTarget.Text))
            {
                tbIncidentIntPctLog.ForeColor = Color.White;
                tbIncidentIntPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbIncidentIntPctLog.ForeColor = Color.White;
                tbIncidentIntPctLog.BackColor = Color.ForestGreen;
            }
            
            tbProjectCltPctLog.Text = String.Format("{0:P2}", (ProjectCltHrsLog / TotalHrsLog));
            
            if ((ProjectCltHrsLog / TotalHrsLog * 100) < Convert.ToDouble(tbProjectCltTarget.Text))
            {
                tbProjectCltPctLog.ForeColor = Color.White;
                tbProjectCltPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbProjectCltPctLog.ForeColor = Color.White;
                tbProjectCltPctLog.BackColor = Color.ForestGreen;
            }
            
            tbProjectIntPctLog.Text = String.Format("{0:P2}", (ProjectIntHrsLog / TotalHrsLog));
            
            if ((ProjectIntHrsLog / TotalHrsLog * 100) > Convert.ToDouble(tbProjectIntTarget.Text))
            {
                tbProjectIntPctLog.ForeColor = Color.White;
                tbProjectIntPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbProjectIntPctLog.ForeColor = Color.White;
                tbProjectIntPctLog.BackColor = Color.ForestGreen;
            }
            
            tbInternalPctLog.Text = String.Format("{0:P2}", (InternalHrsLog / TotalHrsLog));
            
            if ((InternalHrsLog / TotalHrsLog * 100) > Convert.ToDouble(tbInternalTarget.Text))
            {
                tbInternalPctLog.ForeColor = Color.White;
                tbInternalPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbInternalPctLog.ForeColor = Color.White;
                tbInternalPctLog.BackColor = Color.ForestGreen;
            }
            
            tbDevelopPctLog.Text = String.Format("{0:P2}", (DevelopHrsLog / TotalHrsLog));

            if ((DevelopHrsLog / TotalHrsLog * 100) > Convert.ToDouble(tbDevelopTarget.Text))
            {
                tbDevelopPctLog.ForeColor = Color.White;
                tbDevelopPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbDevelopPctLog.ForeColor = Color.White;
                tbDevelopPctLog.BackColor = Color.ForestGreen;
            }
            
            tbRITSPctLog.Text = String.Format("{0:P2}", (RITsHrsLog / TotalHrsLog));

            if ((RITsHrsLog / TotalHrsLog * 100) > Convert.ToDouble(tbRITsTarget.Text))
            {
                tbRITSPctLog.ForeColor = Color.White;
                tbRITSPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbRITSPctLog.ForeColor = Color.White;
                tbRITSPctLog.BackColor = Color.ForestGreen;
            }
            
            tbSAMsPctLog.Text = String.Format("{0:P2}", (SAMsHrsLog / TotalHrsLog));

            if ((SAMsHrsLog / TotalHrsLog * 100) > Convert.ToDouble(tbSAMsTarget.Text))
            {
                tbSAMsPctLog.ForeColor = Color.White;
                tbSAMsPctLog.BackColor = Color.IndianRed;
            }
            else
            {
                tbSAMsPctLog.ForeColor = Color.White;
                tbSAMsPctLog.BackColor = Color.ForestGreen;
            }
            
            tbIncidentCltHrsDailyLog.Text = Math.Round(IncidentCltHrsLog / TotalWorkingDays, 2).ToString();
            tbIncidentIntHrsDailyLog.Text = Math.Round(IncidentIntHrsLog / TotalWorkingDays, 2).ToString();
            tbProjectCltHrsDailyLog.Text = Math.Round(ProjectCltHrsLog / TotalWorkingDays, 2).ToString();
            tbProjectIntHrsDailyLog.Text = Math.Round(ProjectIntHrsLog / TotalWorkingDays, 2).ToString();
            tbInternalHrsDailyLog.Text = Math.Round(InternalHrsLog / TotalWorkingDays, 2).ToString();
            tbDevelopHrsDailyLog.Text = Math.Round(DevelopHrsLog / TotalWorkingDays, 2).ToString();
            tbRITSHrsDailyLog.Text = Math.Round(RITsHrsLog / TotalWorkingDays, 2).ToString();
            tbSAMsHrsDailyLog.Text = Math.Round(SAMsHrsLog / TotalWorkingDays, 2).ToString();
            tbTotalHrsDailyLog.Text = Math.Round(TotalHrsLog / TotalWorkingDays, 2).ToString();


            if (Math.Round(TotalHrsLog / TotalWorkingDays, 2) >= 7.5)
            {
                tbTotalHrsDailyLog.ForeColor = Color.White;
                tbTotalHrsDailyLog.BackColor = Color.ForestGreen;
            }
            else
            {
                tbTotalHrsDailyLog.ForeColor = Color.White;
                tbTotalHrsDailyLog.BackColor = Color.IndianRed;
            }

            if (chkInlcudeExpenses.Checked)
                //TotalUserCost += (CompanyCostEmpPerDay * TotalWorkingDays * Convert.ToDouble(ddlTimeEmployed.SelectedValue.ToString()));
                TotalUserCost += (CompanyCostEmpPerDay * TotalWorkingDays);

            tbTotalHourlyCost.Text = String.Format("{0:C2}", Math.Round(TotalUserCost / TotalHrsLog, 2));
            tbTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost, 0));

            tbIncidentCltTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (IncidentCltHrsLog / TotalHrsLog), 0));
            tbIncidentIntTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (IncidentIntHrsLog / TotalHrsLog), 0));
            tbProjectCltTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (ProjectCltHrsLog / TotalHrsLog), 0));
            tbProjectIntTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (ProjectIntHrsLog / TotalHrsLog), 0));
            tbInternalTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (InternalHrsLog / TotalHrsLog), 0));
            TotalInternalCost = TotalUserCost * (InternalHrsLog / TotalHrsLog);
            tbDevelopTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (DevelopHrsLog / TotalHrsLog), 0));
            tbRITSTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (RITsHrsLog / TotalHrsLog), 0));
            tbSAMsTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalUserCost * (SAMsHrsLog / TotalHrsLog), 0));

            if (IncidentCltHrsLog > 0)
                tbIncidentCltHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) / IncidentCltHrsLog, 2));
            if (IncidentIntHrsLog > 0)
                tbIncidentIntHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (IncidentIntHrsLog / TotalHrsLog)) / IncidentIntHrsLog, 2));
            if (ProjectCltHrsLog > 0)
                tbProjectCltHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (ProjectCltHrsLog / TotalHrsLog)) / ProjectCltHrsLog, 2));
            if (ProjectIntHrsLog > 0)
                tbProjectIntHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (ProjectIntHrsLog / TotalHrsLog)) / ProjectIntHrsLog, 2));
            if (InternalHrsLog > 0)
                tbInternalHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (InternalHrsLog / TotalHrsLog)) / InternalHrsLog, 2));
            if (DevelopHrsLog > 0)
                tbDevelopHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (DevelopHrsLog / TotalHrsLog)) / DevelopHrsLog, 2));
            if (RITsHrsLog > 0)
                tbRITSHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) / RITsHrsLog, 2));
            if (SAMsHrsLog > 0)
                tbSAMsHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalUserCost * (SAMsHrsLog / TotalHrsLog)) / SAMsHrsLog, 2));



            tbIncidentCltHrsBilled.Text = Math.Round(IncidentCltBillableHrs, 2).ToString();
            tbProjectCltHrsBilled.Text = Math.Round(ProjectCltBillableHrs, 2).ToString();
            tbIncidentIntHrsBilled.Text = Math.Round(IncidentIntBillableHrs, 2).ToString();
            tbProjectIntHrsBilled.Text = Math.Round(ProjectIntBillableHrs, 2).ToString();
            tbInternalHrsBilled.Text = Math.Round(InternalBillableHrs, 2).ToString();
            tbDevelopHrsBilled.Text = Math.Round(DevelopBillableHrs, 2).ToString();
            tbRITSHrsBilled.Text = Math.Round(RITsBillableHrs, 2).ToString();
            tbSAMsHrsBilled.Text = Math.Round(SAMsBillableHrs, 2).ToString();
            tbTotalHrsBilled.Text = Math.Round(IncidentCltBillableHrs + RITsBillableHrs + ProjectCltBillableHrs + IncidentIntBillableHrs + ProjectIntBillableHrs + InternalBillableHrs + DevelopBillableHrs + SAMsBillableHrs, 2).ToString();

            tbIncidentCltPctBilled.Text = String.Format("{0:P2}", (IncidentCltBillableHrs / TotalHrsLog));
            tbProjectCltPctBilled.Text = String.Format("{0:P}", (ProjectCltBillableHrs / TotalHrsLog));
            tbIncidentIntPctBilled.Text = String.Format("{0:P2}", (IncidentIntBillableHrs / TotalHrsLog));
            tbProjectIntPctBilled.Text = String.Format("{0:P2}", (ProjectIntBillableHrs / TotalHrsLog));
            tbInternalPctBilled.Text = String.Format("{0:P2}", (InternalBillableHrs / TotalHrsLog));
            tbDevelopPctBilled.Text = String.Format("{0:P2}", (DevelopBillableHrs / TotalHrsLog));
            tbRITSPctBilled.Text = String.Format("{0:P2}", (RITsBillableHrs / TotalHrsLog));
            tbSAMsPctBilled.Text = String.Format("{0:P2}", (SAMsBillableHrs / TotalHrsLog));
            tbTotalPctBilled.Text = String.Format("{0:P2}", ((IncidentCltBillableHrs + RITsBillableHrs + ProjectCltBillableHrs + IncidentIntBillableHrs + ProjectIntBillableHrs + InternalBillableHrs + DevelopBillableHrs + SAMsBillableHrs) / TotalHrsLog));


            IncidentCltActualRevenue = IncidentCltBilled - (TotalUserCost * (IncidentCltHrsLog / TotalHrsLog));
            IncidentCltPotentialRevenue = IncidentCltPotBilled - (TotalUserCost * (IncidentCltHrsLog / TotalHrsLog));
            ProjectCltActualRevenue = ProjectCltBilled - (TotalUserCost * (ProjectCltHrsLog / TotalHrsLog));
            ProjectCltPotentialRevenue = ProjectCltPotBilled - (TotalUserCost * (ProjectCltHrsLog / TotalHrsLog));
            IncidentIntActualRevenue = IncidentIntBilled - (TotalUserCost * (IncidentIntHrsLog / TotalHrsLog));
            IncidentIntPotentialRevenue = IncidentIntPotBilled - (TotalUserCost * (IncidentIntHrsLog / TotalHrsLog));
            ProjectIntActualRevenue = ProjectIntBilled - (TotalUserCost * (ProjectIntHrsLog / TotalHrsLog));
            ProjectIntPotentialRevenue = ProjectIntPotBilled - (TotalUserCost * (ProjectIntHrsLog / TotalHrsLog));
            InternalActualRevenue = 0 - (TotalUserCost * (InternalHrsLog / TotalHrsLog));
            InternalPotentialRevenue = 0 - (TotalUserCost * (InternalHrsLog / TotalHrsLog));
            DevelopActualRevenue = DevelopBilled - (TotalUserCost * (DevelopHrsLog / TotalHrsLog));
            DevelopPotentialRevenue = DevelopPotBilled - (TotalUserCost * (DevelopHrsLog / TotalHrsLog));
            if(TotalRITsHours > 0)
            {
                RITsBilled = (RITsBillableHrs / TotalRITsHours) * (Convert.ToDouble(tbRITSYear.Text) * TotalYears);
                RITsPotBilled = (RITsBillableHrs / TotalRITsHours) * (Convert.ToDouble(tbRITSYear.Text) * TotalYears);
            }
            if(TotalSAMsHours > 0)
            {
                SAMsBilled = (SAMsBillableHrs / TotalSAMsHours) * (Convert.ToDouble(tbSAMsYear.Text) * TotalYears);
                SAMsPotBilled = (SAMsBillableHrs / TotalSAMsHours) * (Convert.ToDouble(tbSAMsYear.Text) * TotalYears);
            }
            RITSActualRevenue = RITsBilled - (TotalUserCost * (RITsHrsLog / TotalHrsLog));
            RITSPotentialRevenue = RITsPotBilled - (TotalUserCost * (RITsHrsLog / TotalHrsLog));
            SAMsActualRevenue = SAMsBilled - (TotalUserCost * (SAMsHrsLog / TotalHrsLog));
            SAMsPotentialRevenue = SAMsPotBilled - (TotalUserCost * (SAMsHrsLog / TotalHrsLog));

            tbIncidentCltActualBilled.Text = String.Format("{0:C0}", Math.Round(IncidentCltBilled, 0));
            tbIncidentCltPotentialBilled.Text = String.Format("{0:C0}", Math.Round(IncidentCltPotBilled, 0));
            tbProjectCltActualBilled.Text = String.Format("{0:C0}", Math.Round(ProjectCltBilled, 0));
            tbProjectCltPotentialBilled.Text = String.Format("{0:C0}", Math.Round(ProjectCltPotBilled, 0));
            tbIncidentIntActualBilled.Text = String.Format("{0:C0}", Math.Round(IncidentIntBilled, 0));
            tbIncidentIntPotentialBilled.Text = String.Format("{0:C0}", Math.Round(IncidentIntPotBilled, 0));
            tbProjectIntActualBilled.Text = String.Format("{0:C0}", Math.Round(ProjectIntBilled, 0));
            tbProjectIntPotentialBilled.Text = String.Format("{0:C0}", Math.Round(ProjectIntPotBilled, 0));
            tbDevelopActualBilled.Text = String.Format("{0:C0}", Math.Round(DevelopBilled, 0));
            tbDevelopPotentialBilled.Text = String.Format("{0:C0}", Math.Round(DevelopPotBilled, 0));
            tbRITSActualBilled.Text = String.Format("{0:C0}", Math.Round(RITsBilled, 0));
            tbSAMsActualBilled.Text = String.Format("{0:C0}", Math.Round(SAMsBilled, 0));
            tbRITSPotentialBilled.Text = String.Format("{0:C0}", Math.Round(RITsPotBilled, 0));
            tbSAMsPotentialBilled.Text = String.Format("{0:C0}", Math.Round(SAMsPotBilled, 0));
            tbTotalActualBilled.Text = String.Format("{0:C0}", Math.Round(IncidentCltBilled + ProjectCltBilled + IncidentIntBilled + ProjectIntBilled + DevelopBilled + RITsBilled + SAMsBilled, 0));
            tbTotalPotentialBilled.Text = String.Format("{0:C0}", Math.Round(IncidentCltPotBilled + ProjectCltPotBilled + IncidentIntPotBilled + ProjectIntPotBilled + DevelopPotBilled + RITsPotBilled + SAMsPotBilled, 0));

            

            tbIncidentCltActualRevenue.Text = String.Format("{0:C0}", Math.Round(IncidentCltActualRevenue, 0));
            
            tbIncidentCltPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(IncidentCltPotentialRevenue, 0));
            tbProjectCltActualRevenue.Text = String.Format("{0:C0}", Math.Round(ProjectCltActualRevenue, 0));
            tbProjectCltPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(ProjectCltPotentialRevenue, 0));
            tbIncidentIntActualRevenue.Text = String.Format("{0:C0}", Math.Round(IncidentIntActualRevenue, 0));
            tbIncidentIntPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(IncidentIntPotentialRevenue, 0));
            tbProjectIntActualRevenue.Text = String.Format("{0:C0}", Math.Round(ProjectIntActualRevenue, 0));
            tbProjectIntPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(ProjectIntPotentialRevenue, 0));
            tbInternalActualRevenue.Text = String.Format("{0:C0}", Math.Round(InternalActualRevenue, 0));
            tbInternalPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(InternalPotentialRevenue, 0));
            tbDevelopActualRevenue.Text = String.Format("{0:C0}", Math.Round(DevelopActualRevenue, 0));
            tbDevelopPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(DevelopPotentialRevenue, 0));
            tbRITSActualRevenue.Text = String.Format("{0:C0}", Math.Round(RITSActualRevenue, 0));
            tbRITSPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(RITSPotentialRevenue, 0));
            tbSAMsActualRevenue.Text = String.Format("{0:C0}", Math.Round(SAMsActualRevenue, 0));
            tbSAMsPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(SAMsPotentialRevenue, 0));

            tbTotalActualRevenue.Text = String.Format("{0:C0}", Math.Round(IncidentCltActualRevenue + ProjectCltActualRevenue + IncidentIntActualRevenue + ProjectIntActualRevenue + InternalActualRevenue + DevelopActualRevenue + RITSActualRevenue + SAMsActualRevenue, 0));

            tbTotalPotentialRevenue.Text = String.Format("{0:C0}", Math.Round(IncidentCltPotentialRevenue + ProjectCltPotentialRevenue + IncidentIntPotentialRevenue + ProjectIntPotentialRevenue + InternalPotentialRevenue + DevelopPotentialRevenue + RITSPotentialRevenue + SAMsPotentialRevenue, 0));

            if (IncidentCltActualRevenue < 0)
            {
                tbIncidentCltActualRevenue.ForeColor = Color.White;
                tbIncidentCltActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbIncidentCltActualRevenue.ForeColor = Color.White;
                tbIncidentCltActualRevenue.BackColor = Color.ForestGreen;
            }
            
            if (IncidentCltPotentialRevenue < 0)
            {
                tbIncidentCltPotentialRevenue.ForeColor = Color.White;
                tbIncidentCltPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbIncidentCltPotentialRevenue.ForeColor = Color.White;
                tbIncidentCltPotentialRevenue.BackColor = Color.ForestGreen;
            }

            if (ProjectCltActualRevenue < 0)
            {
                tbProjectCltActualRevenue.ForeColor = Color.White;
                tbProjectCltActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbProjectCltActualRevenue.ForeColor = Color.White;
                tbProjectCltActualRevenue.BackColor = Color.ForestGreen;
            }


            if (ProjectCltPotentialRevenue < 0)
            {
                tbProjectCltPotentialRevenue.ForeColor = Color.White;
                tbProjectCltPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbProjectCltPotentialRevenue.ForeColor = Color.White;
                tbProjectCltPotentialRevenue.BackColor = Color.ForestGreen;
            }


            if (IncidentIntActualRevenue < 0)
            {
                tbIncidentIntActualRevenue.ForeColor = Color.White;
                tbIncidentIntActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbIncidentIntActualRevenue.ForeColor = Color.White;
                tbIncidentIntActualRevenue.BackColor = Color.ForestGreen;
            }


            if (IncidentIntPotentialRevenue < 0)
            {
                tbIncidentIntPotentialRevenue.ForeColor = Color.White;
                tbIncidentIntPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbIncidentIntPotentialRevenue.ForeColor = Color.White;
                tbIncidentIntPotentialRevenue.BackColor = Color.ForestGreen;
            }

            if (ProjectIntActualRevenue < 0)
            {
                tbProjectIntActualRevenue.ForeColor = Color.White;
                tbProjectIntActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbProjectIntActualRevenue.ForeColor = Color.White;
                tbProjectIntActualRevenue.BackColor = Color.ForestGreen;
            }

            if (ProjectIntPotentialRevenue < 0)
            {
                tbProjectIntPotentialRevenue.ForeColor = Color.White;
                tbProjectIntPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbProjectIntPotentialRevenue.ForeColor = Color.White;
                tbProjectIntPotentialRevenue.BackColor = Color.ForestGreen;
            }


            if (InternalActualRevenue < 0)
            {
                tbInternalActualRevenue.ForeColor = Color.White;
                tbInternalActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbInternalActualRevenue.ForeColor = Color.White;
                tbInternalActualRevenue.BackColor = Color.ForestGreen;
            }


            if (InternalPotentialRevenue < 0)
            {
                tbInternalPotentialRevenue.ForeColor = Color.White;
                tbInternalPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbInternalPotentialRevenue.ForeColor = Color.White;
                tbInternalPotentialRevenue.BackColor = Color.ForestGreen;
            }


            if (DevelopActualRevenue < 0)
            {
                tbDevelopActualRevenue.ForeColor = Color.White;
                tbDevelopActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbDevelopActualRevenue.ForeColor = Color.White;
                tbDevelopActualRevenue.BackColor = Color.ForestGreen;
            }


            if (DevelopPotentialRevenue < 0)
            {
                tbDevelopPotentialRevenue.ForeColor = Color.White;
                tbDevelopPotentialRevenue.BackColor = Color.IndianRed;
            }

            else
            {
                tbDevelopPotentialRevenue.ForeColor = Color.White;
                tbDevelopPotentialRevenue.BackColor = Color.ForestGreen;
            }


            if (RITSActualRevenue < 0)
            {
                tbRITSActualRevenue.ForeColor = Color.White;
                tbRITSActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbRITSActualRevenue.ForeColor = Color.White;
                tbRITSActualRevenue.BackColor = Color.ForestGreen;
            }


            if (RITSPotentialRevenue < 0)
            {
                tbRITSPotentialRevenue.ForeColor = Color.White;
                tbRITSPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {

                tbRITSPotentialRevenue.ForeColor = Color.White;
                tbRITSPotentialRevenue.BackColor = Color.ForestGreen;
            }

            if (SAMsActualRevenue < 0)
            {
                tbSAMsActualRevenue.ForeColor = Color.White;
                tbSAMsActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbSAMsActualRevenue.ForeColor = Color.White;
                tbSAMsActualRevenue.BackColor = Color.ForestGreen;
            }


            if (SAMsPotentialRevenue < 0)
            {
                tbSAMsPotentialRevenue.ForeColor = Color.White;
                tbSAMsPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {

                tbSAMsPotentialRevenue.ForeColor = Color.White;
                tbSAMsPotentialRevenue.BackColor = Color.ForestGreen;
            }


            if ((IncidentCltActualRevenue + ProjectCltActualRevenue + IncidentIntActualRevenue + ProjectIntActualRevenue + InternalActualRevenue + DevelopActualRevenue + RITSActualRevenue) < 0)
            {
                tbTotalActualRevenue.ForeColor = Color.White;
                tbTotalActualRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbTotalActualRevenue.ForeColor = Color.White;
                tbTotalActualRevenue.BackColor = Color.ForestGreen;
            }


            if ((IncidentCltPotentialRevenue + ProjectCltPotentialRevenue + IncidentIntPotentialRevenue + ProjectIntPotentialRevenue + InternalPotentialRevenue + DevelopPotentialRevenue + RITSPotentialRevenue) < 0)
            {
                tbTotalPotentialRevenue.ForeColor = Color.White;
                tbTotalPotentialRevenue.BackColor = Color.IndianRed;
            }
            else
            {
                tbTotalPotentialRevenue.ForeColor = Color.White;
                tbTotalPotentialRevenue.BackColor = Color.ForestGreen;
            }

            #endregion

            #region Internal Breakdown

            tbIntAccHrsLog.Text = Math.Round(IntAccHrsLog, 2).ToString();
            tbIntAdmHrsLog.Text = Math.Round(IntAdmHrsLog, 2).ToString();
            tbIntBusHrsLog.Text = Math.Round(IntBusHrsLog, 2).ToString();
            tbIntCmsHrsLog.Text = Math.Round(IntCmsHrsLog, 2).ToString();
            tbIntCRHrsLog.Text = Math.Round(IntCRHrsLog, 2).ToString();
            tbIntHRHrsLog.Text = Math.Round(IntHRHrsLog, 2).ToString();
            tbIntMgmtHrsLog.Text = Math.Round(IntMgmtHrsLog, 2).ToString();
            tbIntOpsHrsLog.Text = Math.Round(IntOpsHrsLog, 2).ToString();
            tbIntPerHrsLog.Text = Math.Round(IntPerHrsLog, 2).ToString();
            tbIntPrjHrsLog.Text = Math.Round(IntPrjHrsLog, 2).ToString();
            tbIntRLHrsLog.Text = Math.Round(IntRLHrsLog, 2).ToString();
            tbIntSMHrsLog.Text = Math.Round(IntSMHrsLog, 2).ToString();
            tbIntTmsHrsLog.Text = Math.Round(IntTmsHrsLog, 2).ToString();
            tbIntUnkHrsLog.Text = Math.Round(IntUnkHrsLog, 2).ToString();

             

            IntTotalHrsLog = IntAccHrsLog + IntAdmHrsLog + IntBusHrsLog + IntCmsHrsLog + IntCRHrsLog + IntHRHrsLog + IntMgmtHrsLog + IntOpsHrsLog + IntPerHrsLog + IntPrjHrsLog + IntRLHrsLog + IntSMHrsLog + IntTmsHrsLog + IntUnkHrsLog;

            
            tbIntAccPctLog.Text = String.Format("{0:P2}", (IntAccHrsLog / IntTotalHrsLog));
            tbIntAdmPctLog.Text = String.Format("{0:P2}", (IntAdmHrsLog / IntTotalHrsLog));
            tbIntBusPctLog.Text = String.Format("{0:P2}", (IntBusHrsLog / IntTotalHrsLog));
            tbIntCmsPctLog.Text = String.Format("{0:P2}", (IntCmsHrsLog / IntTotalHrsLog));
            tbIntCRPctLog.Text = String.Format("{0:P2}", (IntCRHrsLog / IntTotalHrsLog));
            tbIntHRPctLog.Text = String.Format("{0:P2}", (IntHRHrsLog / IntTotalHrsLog));
            tbIntMgmtPctLog.Text = String.Format("{0:P2}", (IntMgmtHrsLog / IntTotalHrsLog));
            tbIntOpsPctLog.Text = String.Format("{0:P2}", (IntOpsHrsLog / IntTotalHrsLog));
            tbIntPerPctLog.Text = String.Format("{0:P2}", (IntPerHrsLog / IntTotalHrsLog));
            tbIntPrjPctLog.Text = String.Format("{0:P2}", (IntPrjHrsLog / IntTotalHrsLog));
            tbIntRLPctLog.Text = String.Format("{0:P2}", (IntRLHrsLog / IntTotalHrsLog));
            tbIntSMPctLog.Text = String.Format("{0:P2}", (IntSMHrsLog / IntTotalHrsLog));
            tbIntTmsPctLog.Text = String.Format("{0:P2}", (IntTmsHrsLog / IntTotalHrsLog));
            tbIntUnkPctLog.Text = String.Format("{0:P2}", (IntUnkHrsLog / IntTotalHrsLog));

            tbIntAccPctLogTot.Text = String.Format("{0:P2}", (IntAccHrsLog / TotalHrsLog));
            tbIntAdmPctLogTot.Text = String.Format("{0:P2}", (IntAdmHrsLog / TotalHrsLog));
            tbIntBusPctLogTot.Text = String.Format("{0:P2}", (IntBusHrsLog / TotalHrsLog));
            tbIntCmsPctLogTot.Text = String.Format("{0:P2}", (IntCmsHrsLog / TotalHrsLog));
            tbIntCRPctLogTot.Text = String.Format("{0:P2}", (IntCRHrsLog / TotalHrsLog));
            tbIntHRPctLogTot.Text = String.Format("{0:P2}", (IntHRHrsLog / TotalHrsLog));
            tbIntMgmtPctLogTot.Text = String.Format("{0:P2}", (IntMgmtHrsLog / TotalHrsLog));
            tbIntOpsPctLogTot.Text = String.Format("{0:P2}", (IntOpsHrsLog / TotalHrsLog));
            tbIntPerPctLogTot.Text = String.Format("{0:P2}", (IntPerHrsLog / TotalHrsLog));
            tbIntPrjPctLogTot.Text = String.Format("{0:P2}", (IntPrjHrsLog / TotalHrsLog));
            tbIntRLPctLogTot.Text = String.Format("{0:P2}", (IntRLHrsLog / TotalHrsLog));
            tbIntSMPctLogTot.Text = String.Format("{0:P2}", (IntSMHrsLog / TotalHrsLog));
            tbIntTmsPctLogTot.Text = String.Format("{0:P2}", (IntTmsHrsLog / TotalHrsLog));
            tbIntUnkPctLogTot.Text = String.Format("{0:P2}", (IntUnkHrsLog / TotalHrsLog));

            tbIntTotalPctLogTot.Text = tbInternalPctLog.Text;

            tbIntAccHrsDailyLog.Text = Math.Round(IntAccHrsLog / TotalWorkingDays, 2).ToString();
            tbIntAdmHrsDailyLog.Text = Math.Round(IntAdmHrsLog / TotalWorkingDays, 2).ToString();
            tbIntBusHrsDailyLog.Text = Math.Round(IntBusHrsLog / TotalWorkingDays, 2).ToString();
            tbIntCmsHrsDailyLog.Text = Math.Round(IntCmsHrsLog / TotalWorkingDays, 2).ToString();
            tbIntCRHrsDailyLog.Text = Math.Round(IntCRHrsLog / TotalWorkingDays, 2).ToString();
            tbIntHRHrsDailyLog.Text = Math.Round(IntHRHrsLog / TotalWorkingDays, 2).ToString();
            tbIntMgmtHrsDailyLog.Text = Math.Round(IntMgmtHrsLog / TotalWorkingDays, 2).ToString();
            tbIntOpsHrsDailyLog.Text = Math.Round(IntOpsHrsLog / TotalWorkingDays, 2).ToString();
            tbIntPerHrsDailyLog.Text = Math.Round(IntPerHrsLog / TotalWorkingDays, 2).ToString();
            tbIntPrjHrsDailyLog.Text = Math.Round(IntPrjHrsLog / TotalWorkingDays, 2).ToString();
            tbIntRLHrsDailyLog.Text = Math.Round(IntRLHrsLog / TotalWorkingDays, 2).ToString();
            tbIntSMHrsDailyLog.Text = Math.Round(IntSMHrsLog / TotalWorkingDays, 2).ToString();
            tbIntTmsHrsDailyLog.Text = Math.Round(IntTmsHrsLog / TotalWorkingDays, 2).ToString();
            tbIntUnkHrsDailyLog.Text = Math.Round(IntUnkHrsLog / TotalWorkingDays, 2).ToString();

            tbIntAccTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntAccHrsLog / IntTotalHrsLog), 0));
            tbIntAdmTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntAdmHrsLog / IntTotalHrsLog), 0));
            tbIntBusTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntBusHrsLog / IntTotalHrsLog), 0));
            tbIntCmsTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntCmsHrsLog / IntTotalHrsLog), 0));
            tbIntCRTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntCRHrsLog / IntTotalHrsLog), 0));
            tbIntHRTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntHRHrsLog / IntTotalHrsLog), 0));
            tbIntMgmtTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntMgmtHrsLog / IntTotalHrsLog), 0));
            tbIntOpsTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntOpsHrsLog / IntTotalHrsLog), 0));
            tbIntPerTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntPerHrsLog / IntTotalHrsLog), 0));
            tbIntPrjTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntPrjHrsLog / IntTotalHrsLog), 0));
            tbIntRLTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntRLHrsLog / IntTotalHrsLog), 0));
            tbIntSMTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntSMHrsLog / IntTotalHrsLog), 0));
            tbIntTmsTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntTmsHrsLog / IntTotalHrsLog), 0));
            tbIntUnkTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost * (IntUnkHrsLog / IntTotalHrsLog), 0));

            if (IntAccHrsLog > 0)
                tbIntAccHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntAccHrsLog / IntTotalHrsLog)) / IntAccHrsLog, 2));
            if (IntAdmHrsLog > 0)
                tbIntAdmHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntAdmHrsLog / IntTotalHrsLog)) / IntAdmHrsLog, 2));
            if (IntBusHrsLog > 0)
                tbIntBusHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntBusHrsLog / IntTotalHrsLog)) / IntBusHrsLog, 2));
            if (IntCmsHrsLog > 0)
                tbIntCmsHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntCmsHrsLog / IntTotalHrsLog)) / IntCmsHrsLog, 2));
            if (IntCRHrsLog > 0)
                tbIntCRHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntCRHrsLog / IntTotalHrsLog)) / IntCRHrsLog, 2));
            if (IntHRHrsLog > 0)
                tbIntHRHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntHRHrsLog / IntTotalHrsLog)) / IntHRHrsLog, 2));
            if (IntMgmtHrsLog > 0)
                tbIntMgmtHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntMgmtHrsLog / IntTotalHrsLog)) / IntMgmtHrsLog, 2));
            if (IntOpsHrsLog > 0)
                tbIntOpsHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntOpsHrsLog / IntTotalHrsLog)) / IntOpsHrsLog, 2));
            if (IntPerHrsLog > 0)
                tbIntPerHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntPerHrsLog / IntTotalHrsLog)) / IntPerHrsLog, 2));
            if (IntPrjHrsLog > 0)
                tbIntPrjHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntPrjHrsLog / IntTotalHrsLog)) / IntPrjHrsLog, 2));
            if (IntRLHrsLog > 0)
                tbIntRLHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntRLHrsLog / IntTotalHrsLog)) / IntRLHrsLog, 2));
            if (IntSMHrsLog > 0)
                tbIntSMHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntSMHrsLog / IntTotalHrsLog)) / IntSMHrsLog, 2));
            if (IntTmsHrsLog > 0)
                tbIntTmsHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntTmsHrsLog / IntTotalHrsLog)) / IntTmsHrsLog, 2));
            if (IntUnkHrsLog > 0)
                tbIntUnkHourlyCost.Text = String.Format("{0:C2}", Math.Round((TotalInternalCost * (IntUnkHrsLog / IntTotalHrsLog)) / IntUnkHrsLog, 2));

            tbIntTotalHrsLog.Text = Math.Round(IntTotalHrsLog, 2).ToString();
            tbIntTotalPctLog.Text = "100%";
            tbIntTotalHrsDailyLog.Text = Math.Round(IntTotalHrsLog / TotalWorkingDays, 2).ToString();
            tbIntTotalCost.Text = String.Format("{0:C0}", Math.Round(TotalInternalCost, 0));
            tbIntTotalHourlyCost.Text = String.Format("{0:C2}", Math.Round(TotalInternalCost / IntTotalHrsLog, 2));

            #endregion

            #region RITs Breakdown
            //RITs Breakdown
            double TotalRITsHrsLog = RITsWK1HrsLog + RITsWK2HrsLog + RITsWK3HrsLog + RITsWK4HrsLog + RITsDayHrsLog + RITsTRSHrsLog + RITsSCMHrsLog + RITsOthHrsLog;

            tbRITsWK1HrsLog.Text = Math.Round(RITsWK1HrsLog, 2).ToString();
            tbRITsWK2HrsLog.Text = Math.Round(RITsWK2HrsLog, 2).ToString();
            tbRITsWK3HrsLog.Text = Math.Round(RITsWK3HrsLog, 2).ToString();
            tbRITsWK4HrsLog.Text = Math.Round(RITsWK4HrsLog, 2).ToString();
            tbRITsDayHrsLog.Text = Math.Round(RITsDayHrsLog, 2).ToString();
            tbRITsTRSHrsLog.Text = Math.Round(RITsTRSHrsLog, 2).ToString();
            tbRITsSCMHrsLog.Text = Math.Round(RITsSCMHrsLog, 2).ToString();
            tbRITsOthHrsLog.Text = Math.Round(RITsOthHrsLog, 2).ToString();

            if (TotalRITsHrsLog>0)
            {
                tbRITsWK1PctLog.Text = String.Format("{0:P2}", (RITsWK1HrsLog / TotalRITsHrsLog));
                tbRITsWK2PctLog.Text = String.Format("{0:P2}", (RITsWK2HrsLog / TotalRITsHrsLog));
                tbRITsWK3PctLog.Text = String.Format("{0:P2}", (RITsWK3HrsLog / TotalRITsHrsLog));
                tbRITsWK4PctLog.Text = String.Format("{0:P2}", (RITsWK4HrsLog / TotalRITsHrsLog));
                tbRITsDayPctLog.Text = String.Format("{0:P2}", (RITsDayHrsLog / TotalRITsHrsLog));
                tbRITsSCMPctLog.Text = String.Format("{0:P2}", (RITsSCMHrsLog / TotalRITsHrsLog));
                tbRITsOthPctLog.Text = String.Format("{0:P2}", (RITsOthHrsLog / TotalRITsHrsLog));
                tbRITsTRSPctLog.Text = String.Format("{0:P2}", (RITsTRSHrsLog / TotalRITsHrsLog));

                tbRITsWK1TotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK1HrsLog / TotalRITsHrsLog), 0));
                tbRITsWK2TotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK2HrsLog / TotalRITsHrsLog), 0));
                tbRITsWK3TotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK3HrsLog / TotalRITsHrsLog), 0));
                tbRITsWK4TotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK4HrsLog / TotalRITsHrsLog), 0));
                tbRITsSCMTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsSCMHrsLog / TotalRITsHrsLog), 0));
                tbRITsDayTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsDayHrsLog / TotalRITsHrsLog), 0));
                tbRITsTRSTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsTRSHrsLog / TotalRITsHrsLog), 0));
                tbRITsOthTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsOthHrsLog / TotalRITsHrsLog), 0));
            }
            
            tbRITsWK1PctLogTot.Text = String.Format("{0:P2}", (RITsWK1HrsLog / TotalHrsLog));
            tbRITsWK2PctLogTot.Text = String.Format("{0:P2}", (RITsWK2HrsLog / TotalHrsLog));
            tbRITsWK3PctLogTot.Text = String.Format("{0:P2}", (RITsWK3HrsLog / TotalHrsLog));
            tbRITsWK4PctLogTot.Text = String.Format("{0:P2}", (RITsWK4HrsLog / TotalHrsLog));
            tbRITsDayPctLogTot.Text = String.Format("{0:P2}", (RITsDayHrsLog / TotalHrsLog));
            tbRITsSCMPctLogTot.Text = String.Format("{0:P2}", (RITsSCMHrsLog / TotalHrsLog));
            tbRITsOthPctLogTot.Text = String.Format("{0:P2}", (RITsOthHrsLog / TotalHrsLog));
            tbRITsTRSPctLogTot.Text = String.Format("{0:P2}", (RITsTRSHrsLog / TotalHrsLog));

            tbRITsWK1HrsDailyLog.Text = Math.Round(RITsWK1HrsLog / TotalWorkingDays, 2).ToString();
            tbRITsWK2HrsDailyLog.Text = Math.Round(RITsWK2HrsLog / TotalWorkingDays, 2).ToString();
            tbRITsWK3HrsDailyLog.Text = Math.Round(RITsWK3HrsLog / TotalWorkingDays, 2).ToString();
            tbRITsWK4HrsDailyLog.Text = Math.Round(RITsWK4HrsLog / TotalWorkingDays, 2).ToString();
            tbRITsDayHrsDailyLog.Text = Math.Round(RITsDayHrsLog / TotalWorkingDays, 2).ToString();
            tbRITsSCMHrsDailyLog.Text = Math.Round(RITsSCMHrsLog / TotalWorkingDays, 2).ToString();
            tbRITsOthHrsDailyLog.Text = Math.Round(RITsOthHrsLog / TotalWorkingDays, 2).ToString();
            tbRITsTRSHrsDailyLog.Text = Math.Round(RITsTRSHrsLog / TotalWorkingDays, 2).ToString();



            if (RITsWK1HrsLog > 0)
                tbRITsWK1HourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK1HrsLog / TotalRITsHrsLog)) / RITsWK1HrsLog, 2));
            if (RITsWK2HrsLog > 0)
                tbRITsWK2HourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK2HrsLog / TotalRITsHrsLog)) / RITsWK2HrsLog, 2));
            if (RITsWK3HrsLog > 0)
                tbRITsWK3HourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK3HrsLog / TotalRITsHrsLog)) / RITsWK3HrsLog, 2));
            if (RITsWK4HrsLog > 0)
                tbRITsWK4HourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsWK4HrsLog / TotalRITsHrsLog)) / RITsWK4HrsLog, 2));
            if (RITsDayHrsLog > 0)
                tbRITsDayHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsDayHrsLog / TotalRITsHrsLog)) / RITsDayHrsLog, 2));
            if (RITsSCMHrsLog > 0)
                tbRITsSCMHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsSCMHrsLog / TotalRITsHrsLog)) / RITsSCMHrsLog, 2));
            if (RITsTRSHrsLog > 0)
                tbRITsTRSHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsTRSHrsLog / TotalRITsHrsLog)) / RITsTRSHrsLog, 2));
            if (RITsOthHrsLog > 0)
                tbRITsOthHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (RITsHrsLog / TotalHrsLog)) * (RITsOthHrsLog / TotalRITsHrsLog)) / RITsOthHrsLog, 2));

            tbRITsTotalHrsLog.Text = tbRITSHrsLog.Text;
            tbRITsTotalPctLog.Text = "100%";
            tbRITsTotalPctLogTot.Text = tbRITSPctLog.Text;
            tbRITsTotalHrsDailyLog.Text = tbRITSHrsDailyLog.Text;
            tbRITsTotalCostDet.Text = tbRITSTotalCost.Text;
            tbRITsTotalHourlyCost.Text = tbTotalHourlyCost.Text;

            #endregion

            #region SAMs Breakdown
            // SAMs Breakdown
            double TotalSAMsHrsLog = SAMsSCOMMonHrsLog + SAMsSCOMMMHrsLog + SAMsOthHrsLog;

            tbSAMsSCOMMonHrsLog.Text = Math.Round(SAMsSCOMMonHrsLog, 2).ToString();
            tbSAMsSCOMMMHrsLog.Text = Math.Round(SAMsSCOMMMHrsLog, 2).ToString();
            tbSAMsOthHrsLog.Text = Math.Round(SAMsOthHrsLog, 2).ToString();
            if(TotalSAMsHrsLog>0)
            {
                tbSAMsSCOMMMPctLog.Text = String.Format("{0:P2}", (SAMsSCOMMMHrsLog / TotalSAMsHrsLog));
                tbSAMsSCOMMonPctLog.Text = String.Format("{0:P2}", (SAMsSCOMMonHrsLog / TotalSAMsHrsLog));
                tbSAMsOthPctLog.Text = String.Format("{0:P2}", (SAMsOthHrsLog / TotalSAMsHrsLog));
                tbSAMsSCOMMMTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (SAMsHrsLog / TotalHrsLog)) * (SAMsSCOMMMHrsLog / TotalSAMsHrsLog), 0));
                tbSAMsSCOMMonTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (SAMsHrsLog / TotalHrsLog)) * (SAMsSCOMMonHrsLog / TotalSAMsHrsLog), 0));
                tbSAMsOthTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (SAMsHrsLog / TotalHrsLog)) * (SAMsOthHrsLog / TotalSAMsHrsLog), 0));

            }
            

            tbSAMsSCOMMMPctLogTot.Text = String.Format("{0:P2}", (SAMsSCOMMMHrsLog / TotalHrsLog));
            tbSAMsSCOMMonPctLogTot.Text = String.Format("{0:P2}", (SAMsSCOMMonHrsLog / TotalHrsLog));
            tbSAMsOthPctLogTot.Text = String.Format("{0:P2}", (SAMsOthHrsLog / TotalHrsLog));

            tbSAMsSCOMMMHrsDailyLog.Text = Math.Round(SAMsSCOMMMHrsLog / TotalWorkingDays, 2).ToString();
            tbSAMsSCOMMonHrsDailyLog.Text = Math.Round(SAMsSCOMMonHrsLog / TotalWorkingDays, 2).ToString();
            tbSAMsOthHrsDailyLog.Text = Math.Round(SAMsOthHrsLog / TotalWorkingDays, 2).ToString();



            if (SAMsSCOMMMHrsLog > 0)
                tbSAMsSCOMMMHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (SAMsHrsLog / TotalHrsLog)) * (SAMsSCOMMMHrsLog / TotalSAMsHrsLog)) / SAMsSCOMMMHrsLog, 2));
            if (SAMsSCOMMonHrsLog > 0)
                tbSAMsSCOMMonHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (SAMsHrsLog / TotalHrsLog)) * (SAMsSCOMMonHrsLog / TotalSAMsHrsLog)) / SAMsSCOMMonHrsLog, 2));
            if (SAMsOthHrsLog > 0)
                tbSAMsOthHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (SAMsHrsLog / TotalHrsLog)) * (SAMsOthHrsLog / TotalSAMsHrsLog)) / SAMsOthHrsLog, 2));

            tbSAMsTotalHrsLog.Text = tbSAMsHrsLog.Text;
            tbSAMsTotalPctLog.Text = "100%";
            tbSAMsTotalPctLogTot.Text = tbSAMsPctLog.Text;
            tbSAMsTotalHrsDailyLog.Text = tbSAMsHrsDailyLog.Text;
            tbSAMsTotalCostDet.Text = tbSAMsTotalCost.Text;
            tbSAMsTotalHourlyCost.Text = tbTotalHourlyCost.Text;
 
            #endregion

            #region Incident Breakdown
            //Incident Breakdown
            tbSCHrsLog.Text = Math.Round(SCHrsLog, 2).ToString();
            tbSTHrsLog.Text = Math.Round(STHrsLog, 2).ToString();
            tbCCHrsLog.Text = Math.Round(CCHrsLog, 2).ToString();
            tbCTHrsLog.Text = Math.Round(CTHrsLog, 2).ToString();
            tbUCHrsLog.Text = Math.Round(UCHrsLog, 2).ToString();
            tbUTHrsLog.Text = Math.Round(UTHrsLog, 2).ToString();
            tbOCHrsLog.Text = Math.Round(OCHrsLog, 2).ToString();
            tbOTHrsLog.Text = Math.Round(OTHrsLog, 2).ToString();
            tbSRHrsLog.Text = Math.Round(SRHrsLog, 2).ToString();

            tbSCPctLog.Text = String.Format("{0:P2}", (SCHrsLog / IncidentCltHrsLog));
            tbSTPctLog.Text = String.Format("{0:P2}", (STHrsLog / IncidentCltHrsLog));
            tbCCPctLog.Text = String.Format("{0:P2}", (CCHrsLog / IncidentCltHrsLog));
            tbCTPctLog.Text = String.Format("{0:P2}", (CTHrsLog / IncidentCltHrsLog));
            tbUCPctLog.Text = String.Format("{0:P2}", (UCHrsLog / IncidentCltHrsLog));
            tbUTPctLog.Text = String.Format("{0:P2}", (UTHrsLog / IncidentCltHrsLog));
            tbOCPctLog.Text = String.Format("{0:P2}", (OCHrsLog / IncidentCltHrsLog));
            tbOTPctLog.Text = String.Format("{0:P2}", (OTHrsLog / IncidentCltHrsLog));
            tbSRPctLog.Text = String.Format("{0:P2}", (SRHrsLog / IncidentCltHrsLog));


            tbSCPctLogTot.Text = String.Format("{0:P2}", (SCHrsLog / TotalHrsLog));
            tbSTPctLogTot.Text = String.Format("{0:P2}", (STHrsLog / TotalHrsLog));
            tbCCPctLogTot.Text = String.Format("{0:P2}", (CCHrsLog / TotalHrsLog));
            tbCTPctLogTot.Text = String.Format("{0:P2}", (CTHrsLog / TotalHrsLog));
            tbUCPctLogTot.Text = String.Format("{0:P2}", (UCHrsLog / TotalHrsLog));
            tbUTPctLogTot.Text = String.Format("{0:P2}", (UTHrsLog / TotalHrsLog));
            tbOCPctLogTot.Text = String.Format("{0:P2}", (OCHrsLog / TotalHrsLog));
            tbOTPctLogTot.Text = String.Format("{0:P2}", (OTHrsLog / TotalHrsLog));
            tbSRPctLogTot.Text = String.Format("{0:P2}", (SRHrsLog / TotalHrsLog));

            tbSCHrsDailyLog.Text = Math.Round(SCHrsLog / TotalWorkingDays, 2).ToString();
            tbSTHrsDailyLog.Text = Math.Round(STHrsLog / TotalWorkingDays, 2).ToString();
            tbCCHrsDailyLog.Text = Math.Round(CCHrsLog / TotalWorkingDays, 2).ToString();
            tbCTHrsDailyLog.Text = Math.Round(CTHrsLog / TotalWorkingDays, 2).ToString();
            tbUCHrsDailyLog.Text = Math.Round(UCHrsLog / TotalWorkingDays, 2).ToString();
            tbUTHrsDailyLog.Text = Math.Round(UTHrsLog / TotalWorkingDays, 2).ToString();
            tbOCHrsDailyLog.Text = Math.Round(OCHrsLog / TotalWorkingDays, 2).ToString();
            tbOTHrsDailyLog.Text = Math.Round(OTHrsLog / TotalWorkingDays, 2).ToString();
            tbSRHrsDailyLog.Text = Math.Round(SRHrsLog / TotalWorkingDays, 2).ToString();

            tbSCTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (SCHrsLog / IncidentCltHrsLog), 0));
            tbSTTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (STHrsLog / IncidentCltHrsLog), 0));
            tbCCTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (CCHrsLog / IncidentCltHrsLog), 0));
            tbCTTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (CTHrsLog / IncidentCltHrsLog), 0));
            tbUCTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (UCHrsLog / IncidentCltHrsLog), 0));
            tbUTTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (UTHrsLog / IncidentCltHrsLog), 0));
            tbOCTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (OCHrsLog / IncidentCltHrsLog), 0));
            tbOTTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (OTHrsLog / IncidentCltHrsLog), 0));
            tbSRTotalCost.Text = String.Format("{0:C0}", Math.Round((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (SRHrsLog / IncidentCltHrsLog), 0));

            if (SCHrsLog > 0)
                tbSCHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (SCHrsLog / IncidentCltHrsLog)) / SCHrsLog, 2));
            if (STHrsLog > 0)
                tbSTHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (STHrsLog / IncidentCltHrsLog)) / STHrsLog, 2));
            if (CCHrsLog > 0)
                tbCCHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (CCHrsLog / IncidentCltHrsLog)) / CCHrsLog, 2));
            if (CTHrsLog > 0)
                tbCTHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (CTHrsLog / IncidentCltHrsLog)) / CTHrsLog, 2));
            if (UCHrsLog > 0)
                tbUCHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (UCHrsLog / IncidentCltHrsLog)) / UCHrsLog, 2));
            if (UTHrsLog > 0)
                tbUTHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (UTHrsLog / IncidentCltHrsLog)) / UTHrsLog, 2));
            if (OCHrsLog > 0)
                tbOCHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (OCHrsLog / IncidentCltHrsLog)) / OCHrsLog, 2));
            if (OTHrsLog > 0)
                tbOTHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (OTHrsLog / IncidentCltHrsLog)) / OTHrsLog, 2));
            if (SRHrsLog > 0)
                tbSRHourlyCost.Text = String.Format("{0:C2}", Math.Round(((TotalUserCost * (IncidentCltHrsLog / TotalHrsLog)) * (SRHrsLog / IncidentCltHrsLog)) / SRHrsLog, 2));


            tbIncidentTotalHrsLog.Text = tbIncidentCltHrsLog.Text;
            tbIncidentTotalPctLogTot.Text = tbIncidentCltPctLog.Text;
            tbIncidentTotalHrsDailyLog.Text = tbIncidentCltHrsDailyLog.Text;
            tbIncidentTotalCost.Text = tbIncidentCltTotalCost.Text;
            tbIncidentTotalHourlyCost.Text = tbIncidentCltHourlyCost.Text;

            #endregion


        }

        protected void txtStartDatePerPerson_TextChanged(object sender, EventArgs e)
        {
                
            
            
        }

        protected void txtEndDatePerPerson_TextChanged(object sender, EventArgs e)
        {
            
           
        }

        protected void txtCaseNumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlStatSelection.SelectedValue)
            {
                case "LoggedHours":
                    pnlOverviewHeader.Visible = pnlOverviewBody.Visible = false;
                    pnlLoggedHoursHeader.Visible = pnlLoggedHoursBody.Visible = true;
                    break;
                case "Overview":
                    pnlOverviewHeader.Visible = pnlOverviewBody.Visible = true;
                    pnlLoggedHoursHeader.Visible = pnlLoggedHoursBody.Visible = false;
                    break;

            }
        }

        protected void GridWorkedHours_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDailyAverage = e.Row.FindControl("txtDailyAverage") as TextBox;
                TextBox txtDaysRequired = e.Row.FindControl("txtDaysRequired") as TextBox;
                TextBox txtHoursWorked = e.Row.FindControl("txtHoursWorked") as TextBox;
                TextBox txtDaysWorked = e.Row.FindControl("txtDaysWorked") as TextBox;
                TextBox txtOutstandingPeerEntries = e.Row.FindControl("txtOutstandingPeerEntries") as TextBox;

                double RequiredTime = Convert.ToDouble(txtDaysRequired.Text) * 480;
                int HoursWorked = ConvertHHMMToMinutes(txtHoursWorked.Text);

                if (Convert.ToInt32(txtOutstandingPeerEntries.Text) > 0)
                    txtOutstandingPeerEntries.ForeColor = System.Drawing.Color.IndianRed;
                else
                    txtOutstandingPeerEntries.ForeColor = System.Drawing.Color.FromName("#333333");
                
                if (RequiredTime > HoursWorked)
                {
                    txtHoursWorked.ForeColor = System.Drawing.Color.IndianRed;
                    txtDailyAverage.ForeColor = System.Drawing.Color.IndianRed;
                    txtDaysWorked.ForeColor = System.Drawing.Color.IndianRed;
                }
                else
                {
                    txtHoursWorked.ForeColor = System.Drawing.Color.FromName("#333333");
                    txtDailyAverage.ForeColor = System.Drawing.Color.FromName("#333333");
                    txtDaysWorked.ForeColor = System.Drawing.Color.FromName("#333333");
                }


            }
        }

        private void CalculateWorkedHours(string StartDate, string EndDate)
        {
            List<SqlParameter> parameters2 = new List<SqlParameter>();
            parameters2.Add(new SqlParameter("@fake", "fake"));
            DataAccessLayer.Instance.ExecuteQuery("DELETE FROM TempWorkedHours", parameters2.ToArray());

            string query = "SELECT EnteredBy,EnteredDate,SUM(TimeInMinutes) as TotalTime FROM Entries WHERE EnteredDate >= CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate <= CONVERT(datetime,'" + EndDate + "',103) AND Error=0 AND Completed=1 GROUP BY EnteredBy,EnteredDate ORDER BY 1";
            string PreviousUser = "";
            int TotalMinutes = 0;
            int TotalMinutesWeekends = 0;
            int OutstandingPeerEntries = 0;
            double RequiredDaysWorked = 0;
            double DaysWorked = 0;
            string DailyAverage = "00:00";

            List<WorkedHoursView> AllEntries = DataAccessLayer.Instance.GetEntities<WorkedHoursView>(query);
            if(AllEntries.Count > 0)
                PreviousUser = AllEntries[0].EnteredBy;

            foreach (WorkedHoursView ThisEntry in AllEntries)
            {

                if (PreviousUser != ThisEntry.EnteredBy)
                {
                    
                    OutstandingPeerEntries = GetPeerReviewCount(PreviousUser,StartDate,EndDate);

                    RequiredDaysWorked = GetRequiredWorkedHours(Convert.ToDateTime(StartDate), Convert.ToDateTime(EndDate), PreviousUser);

                    DailyAverage = "00:00";
                    if (RequiredDaysWorked > 0)
                        DailyAverage = ConvertMinutesToHHMM(TotalMinutes / RequiredDaysWorked);

                    InsertUserRecord(PreviousUser, ConvertMinutesToHHMM(TotalMinutes), RequiredDaysWorked, DailyAverage, OutstandingPeerEntries, ConvertMinutesToHHMM(TotalMinutesWeekends), DaysWorked,ConvertMinutesToHHMM(TotalMinutes+TotalMinutesWeekends) );

                    PreviousUser = ThisEntry.EnteredBy;
                    TotalMinutes = 0;
                    RequiredDaysWorked = 0;
                    DaysWorked = 0;
                    TotalMinutesWeekends = 0;
                    OutstandingPeerEntries = 0;        
                }

                if (IsWorkingDay(ThisEntry.EnteredDate.ToString(), ThisEntry.EnteredBy))
                {
                    TotalMinutes += ThisEntry.TotalTime;
                    DaysWorked += ((Convert.ToDouble(ThisEntry.TotalTime)/60) /8);
                }
                else TotalMinutesWeekends += ThisEntry.TotalTime;
                
            }

            OutstandingPeerEntries = GetPeerReviewCount(PreviousUser, StartDate, EndDate);

            RequiredDaysWorked = GetRequiredWorkedHours(Convert.ToDateTime(StartDate), Convert.ToDateTime(EndDate), PreviousUser);

            DailyAverage = "00:00";
            if (RequiredDaysWorked > 0)
                DailyAverage = ConvertMinutesToHHMM(TotalMinutes / RequiredDaysWorked);

            InsertUserRecord(PreviousUser, ConvertMinutesToHHMM(TotalMinutes), RequiredDaysWorked, DailyAverage, OutstandingPeerEntries, ConvertMinutesToHHMM(TotalMinutesWeekends), DaysWorked, ConvertMinutesToHHMM(TotalMinutes + TotalMinutesWeekends));

            RefreshGridWorkedHours();
            
        }

        private int GetPeerReviewCount(string User, string StartDate, string EndDate)
        {
            string Peer = GetPeer(User);
            List<SqlParameter> PeerCount = new List<SqlParameter>();
            PeerCount.Add(new SqlParameter("@EnteredBy",Peer));
            string PeerCountQuery = @"SELECT COUNT(*) FROM Entries WHERE EnteredBy=@EnteredBy AND EnteredDate >= CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate <= CONVERT(datetime,'" + EndDate + "',103) AND PeerReview=0";
            object PeerReviewCount = DataAccessLayer.Instance.ExecuteQuery(PeerCountQuery, PeerCount.ToArray());

            if(PeerReviewCount != null) return Convert.ToInt32(PeerReviewCount);
            else return 0;
        }

        private void InsertUserRecord(string User, string HoursWorked, double TotalRequiredDaysWorked, string DailyAverage, int OutstandingPeerEntries, string HoursWorkedWeekend, double TotalDaysWorked, string TotalHoursWorked)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", User));
            parameters.Add(new SqlParameter("@HoursWorked", HoursWorked));
            parameters.Add(new SqlParameter("@DaysRequired", TotalRequiredDaysWorked));
            parameters.Add(new SqlParameter("@DaysWorked", TotalDaysWorked));
            parameters.Add(new SqlParameter("@DailyAverage", DailyAverage));
            parameters.Add(new SqlParameter("@OutstandingPeerEntries", OutstandingPeerEntries));
            parameters.Add(new SqlParameter("@HoursWorkedWeekend", HoursWorkedWeekend));
            parameters.Add(new SqlParameter("@TotalHoursWorked", TotalHoursWorked));
            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO TempWorkedHours (Name,HoursWorked,DaysRequired,DaysWorked,DailyAverage,OutstandingPeerEntries,HoursWorkedWeekend,TotalHoursWorked) VALUES(@Name,@HoursWorked,@DaysRequired,@DaysWorked,@DailyAverage,@OutstandingPeerEntries,@HoursWorkedWeekend,@TotalHoursWorked)", parameters.ToArray());

        }

        private bool IsWorkingDay(string Date, string User)
        {
            bool IsWorkingDay = false;

            string QueryUserWorkingDaily = "SELECT * from viewUserWorkTime WHERE ValidFrom<=CONVERT(datetime,'" + Date + "',103) AND DisplayName='" + User + "' ORDER BY ValidFrom";
            List<UserWorkTimeView> viewUserWorkTime = DataAccessLayer.Instance.GetEntities<UserWorkTimeView>(QueryUserWorkingDaily);

            switch (Convert.ToDateTime(Date).DayOfWeek.ToString())
            {
                case "Monday": //Monday
                    if (viewUserWorkTime[viewUserWorkTime.Count - 1].MonWorkMinutes > 0) IsWorkingDay = true;
                    else IsWorkingDay = false;
                    
                    break;
                case "Tuesday": //Tuesday
                    if (viewUserWorkTime[viewUserWorkTime.Count - 1].TueWorkMinutes > 0) IsWorkingDay = true;
                    else IsWorkingDay = false;
                    break;
                case "Wednesday": //Wednesday
                    if (viewUserWorkTime[viewUserWorkTime.Count - 1].WedWorkMinutes > 0) IsWorkingDay = true;
                    else IsWorkingDay = false;
                    break;
                case "Thursday": //Thursday
                    if (viewUserWorkTime[viewUserWorkTime.Count - 1].ThuWorkMinutes > 0) IsWorkingDay = true;
                    else IsWorkingDay = false;
                    break;
                case "Friday": //Friday
                    if (viewUserWorkTime[viewUserWorkTime.Count - 1].FriWorkMinutes > 0) IsWorkingDay = true;
                    else IsWorkingDay = false;
                    break;
                case "Saturday": //Saturday
                    if (viewUserWorkTime[viewUserWorkTime.Count - 1].SatWorkMinutes > 0) IsWorkingDay = true;
                    else IsWorkingDay = false;
                    break;
                case "Sunday": //Sunday
                    if (viewUserWorkTime[viewUserWorkTime.Count - 1].SunWorkMinutes > 0) IsWorkingDay = true;
                    else IsWorkingDay = false;
                    break;
            }

            return IsWorkingDay;
        }

        private double GetRequiredWorkedHours(DateTime StartDate, DateTime EndDate , string User)
        {
            double WorkDay = 0;

            while (StartDate <= EndDate)
            {
                string QueryUserWorkingDaily = "SELECT * from viewUserWorkTime WHERE ValidFrom<=CONVERT(datetime,'" + StartDate.ToShortDateString() + "',103) AND DisplayName='" + User + "' ORDER BY ValidFrom";
                List<UserWorkTimeView> viewUserWorkTime = DataAccessLayer.Instance.GetEntities<UserWorkTimeView>(QueryUserWorkingDaily);

                switch (StartDate.DayOfWeek.ToString())
                {
                    case "Monday": //Monday
                        WorkDay += (Convert.ToDouble(viewUserWorkTime[viewUserWorkTime.Count - 1].MonWorkMinutes) / 60) / 8;
                        break;
                    case "Tuesday": //Tuesday
                        WorkDay += (Convert.ToDouble(viewUserWorkTime[viewUserWorkTime.Count - 1].TueWorkMinutes) / 60) / 8;
                        break;
                    case "Wednesday": //Wednesday
                        WorkDay += (Convert.ToDouble(viewUserWorkTime[viewUserWorkTime.Count - 1].WedWorkMinutes) / 60) / 8;
                        break;
                    case "Thursday": //Thursday
                        WorkDay += (Convert.ToDouble(viewUserWorkTime[viewUserWorkTime.Count - 1].ThuWorkMinutes) / 60) / 8;
                        break;
                    case "Friday": //Friday
                        WorkDay += (Convert.ToDouble(viewUserWorkTime[viewUserWorkTime.Count - 1].FriWorkMinutes) / 60) / 8;
                        break;
                    case "Saturday": //Saturday
                        WorkDay += (Convert.ToDouble(viewUserWorkTime[viewUserWorkTime.Count - 1].SatWorkMinutes) / 60) / 8;
                        break;
                    case "Sunday": //Sunday
                        WorkDay += (Convert.ToDouble(viewUserWorkTime[viewUserWorkTime.Count - 1].SunWorkMinutes) / 60) / 8;
                        break;
                }

                StartDate=StartDate.AddDays(1);
            }

            

            return WorkDay;
        }

        private void RefreshGridWorkedHours()
        {
            string Query = "SELECT * FROM TempWorkedHours";
            this.GridWorkedHours.DataSource = DataAccessLayer.Instance.GetEntities<TempWorkedHours>(Query);
            this.GridWorkedHours.DataBind();
        }

        private string ConvertMinutesToHHMM(double Minutes)
        {
            string HHMM = "00:00";

            int TempHour = Convert.ToInt32(Math.Floor(Minutes / 60));
            int TempMinutes = Convert.ToInt32(Minutes) - (TempHour * 60);

            if (TempHour < 10) HHMM = "0" + TempHour.ToString();
            else HHMM = TempHour.ToString();

            if (TempMinutes < 10) HHMM += ":0" + TempMinutes;
            else HHMM += ":" + TempMinutes;


            return HHMM;
        }

        private int ConvertHHMMToMinutes(string HHMM)
        {
            int TimeInMinutes = 0;

            if (HHMM.Length == 5)
            {
                TimeInMinutes = Convert.ToInt32(HHMM.Substring(0, 2)) * 60;
                TimeInMinutes += Convert.ToInt32(HHMM.Substring(3, 2));
            }
            else
            {
                if (HHMM.Length == 6)
                {
                    TimeInMinutes = Convert.ToInt32(HHMM.Substring(0, 3)) * 60;
                    TimeInMinutes += Convert.ToInt32(HHMM.Substring(4, 2));
                }
                else
                {
                    if (HHMM.Length == 7)
                    {
                        TimeInMinutes = Convert.ToInt32(HHMM.Substring(0, 4)) * 60;
                        TimeInMinutes += Convert.ToInt32(HHMM.Substring(5, 2));
                    }
                }
            }

            return TimeInMinutes;
        }
        
        private int RoundUpMinutes(int MinutesWorked, int Increment)
        {
            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Convert.ToDouble(MinutesWorked) / Convert.ToDouble(Increment))) * Convert.ToDouble(Increment));
        }

        private string GetPeer(string MyUsername)
        {

            
            string Peer = "";

            if (MyUsername != null)
            {
                string query = "Select * from Users where DisplayName='" + MyUsername + "'";
                List<UsersEntity> ThisUser = DataAccessLayer.Instance.GetEntities<UsersEntity>(query);


                foreach (UsersEntity CurrentUser in ThisUser)
                {
                    Peer = CurrentUser.PeerReview.TrimEnd(' ');
                }
            }

            return Peer;
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            UpdateTarget(ddlEmployee.SelectedItem.ToString(),"ACT",tbACTTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "ADM", tbADMTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "BUS", tbBUSTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "COM", tbCOMTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "CRM", tbCRMTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "HR", tbHRTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "MGT", tbMGTTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "OPS", tbOPSTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "PER", tbPERTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "PRJ", tbPRJTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "RL", tbRLTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "SM", tbSMTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "TMS", tbTMSTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "WK1", tbWK1Target.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "WK2", tbWK2Target.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "WK3", tbWK3Target.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "WK4", tbWK4Target.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "DAY", tbDAYTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "TRS", tbTRSTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "SCM", tbSCMTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "OTH", tbOTHTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "SC", tbSCTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "ST", tbSTTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "CC", tbCCTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "CT", tbCTTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "UC", tbUCTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "UT", tbUTTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "OC", tbOCTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "OT", tbOTTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "Service Request", tbServiceRequestTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "Project", tbProjectCltTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "Incident Int", tbIncidentIntTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "Project Int", tbProjectIntTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "SCOMMM", tbSCOMMMTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "SCOMMon", tbSCOMMonTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "SAMsOther", tbSAMsOthTarget.Text.ToString());
            UpdateTarget(ddlEmployee.SelectedItem.ToString(), "SCCMMM", tbRITsSCCMTarget.Text.ToString());
            


            tbInternalTarget.Text = (Convert.ToDouble(tbOPSTarget.Text) + Convert.ToDouble(tbACTTarget.Text) + Convert.ToDouble(tbADMTarget.Text) + Convert.ToDouble(tbBUSTarget.Text) + Convert.ToDouble(tbRLTarget.Text)
                + Convert.ToDouble(tbPRJTarget.Text) + Convert.ToDouble(tbCOMTarget.Text) + Convert.ToDouble(tbHRTarget.Text) + Convert.ToDouble(tbCRMTarget.Text) + Convert.ToDouble(tbSMTarget.Text)
                + Convert.ToDouble(tbTMSTarget.Text) + Convert.ToDouble(tbMGTTarget.Text) + Convert.ToDouble(tbPERTarget.Text)).ToString();

            tbRITsTarget.Text = (Convert.ToDouble(tbDAYTarget.Text) + Convert.ToDouble(tbWK1Target.Text) + Convert.ToDouble(tbWK2Target.Text) + Convert.ToDouble(tbWK3Target.Text) + Convert.ToDouble(tbWK4Target.Text)
                + Convert.ToDouble(tbSCMTarget.Text) + Convert.ToDouble(tbTRSTarget.Text) + Convert.ToDouble(tbOTHTarget.Text) + Convert.ToDouble(tbRITsSCCMTarget.Text)).ToString();

            tbSAMsTarget.Text = (Convert.ToDouble(tbSCOMMMTarget.Text) + Convert.ToDouble(tbSCOMMonTarget.Text) + Convert.ToDouble(tbSAMsOthTarget.Text)).ToString();

            tbIncidentCltTarget.Text = (Convert.ToDouble(tbCCTarget.Text) + Convert.ToDouble(tbCTTarget.Text) + Convert.ToDouble(tbSCTarget.Text) + Convert.ToDouble(tbSTTarget.Text) + Convert.ToDouble(tbUCTarget.Text)
                + Convert.ToDouble(tbUTTarget.Text) + Convert.ToDouble(tbOCTarget.Text) + Convert.ToDouble(tbOTTarget.Text) + Convert.ToDouble(tbServiceRequestTarget.Text)).ToString();

            

            RefreshOverview();

        }

        private void UpdateTarget(string DisplayName, string CategoryName, string Value)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Target", Value));

            string sqlUpdateCommand = @"Update viewUserTarget set Target=@Target WHERE DisplayName='" + DisplayName + "' and Category='" + CategoryName + "'";
            DataAccessLayer.Instance.ExecuteQuery(sqlUpdateCommand, parameters.ToArray());
        }

        


    }

    public class ChartRecord
    {
        double Internalpercent;
        double IncidentCltpercent;
        double IncidentIntpercent;
        double ProjectCltpercent;
        double ProjectIntpercent;
        double RITspercent;
        double SAMspercent;
        double Developpercent;
        string month;

        public ChartRecord(double Internalpercent, double IncidentCltpercent, double IncidentIntpercent, double ProjectCltpercent, double ProjectIntpercent, double RITspercent, double SAMspercent, double Developpercent, string month)
        {
            this.Internalpercent = Internalpercent;
            this.IncidentCltpercent = IncidentCltpercent;
            this.IncidentIntpercent = IncidentIntpercent;
            this.ProjectCltpercent = ProjectCltpercent;
            this.ProjectIntpercent = ProjectIntpercent;
            this.RITspercent = RITspercent;
            this.SAMspercent = SAMspercent;
            this.Developpercent = Developpercent;
            this.month = month;
        }
        public double InternalPercent
        {
            get { return Internalpercent; }
            set { Internalpercent = value; }
        }
        public double IncidentCltPercent
        {
            get { return IncidentCltpercent; }
            set { IncidentCltpercent = value; }
        }
        public double IncidentIntPercent
        {
            get { return IncidentIntpercent; }
            set { IncidentIntpercent = value; }
        }
        public double ProjectCltPercent
        {
            get { return ProjectCltpercent; }
            set { ProjectCltpercent = value; }
        }
        public double ProjectIntPercent
        {
            get { return ProjectIntpercent; }
            set { ProjectIntpercent = value; }
        }
        public double RITsPercent
        {
            get { return RITspercent; }
            set { RITspercent = value; }
        }
        public double SAMsPercent
        {
            get { return SAMspercent; }
            set { SAMspercent = value; }
        }
        public double DevelopPercent
        {
            get { return Developpercent; }
            set { Developpercent = value; }
        }
        public string Month
        {
            get { return month; }
            set { month = value; }
        }
    }
}