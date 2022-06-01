using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Principal;
using System.Collections.Specialized;

namespace TimeCapture
{
    public partial class PeriodReview : System.Web.UI.Page
    {
        public static int MinutesINOnsite = 0;
        public static int MinutesINOffsite = 0;
        public static int MinutesRTOnsite = 0;
        public static int MinutesRTOffsite = 0;
        public static int MinutesPROnsite = 0;
        public static int MinutesPROffsite = 0;
        public static int MinutesDVOnsite = 0;
        public static int MinutesDVOffsite = 0;
        public static int MinutesIROnsite = 0;
        public static int MinutesIROffsite = 0;
        public static int TotalMinutesWorked = 0;
        public static List<TotalUserCosts> AllUserCosts = new List<TotalUserCosts>();
        public static int ListIndex = 0; //Variable used as index in the TotalUserCosts list to store values in the right area
        public static string LastRecordDate = "";
        public static string PreviousUser = "";
        public static string CurrentUser = "";

        public static string MyUser;
        protected static string UserGroup;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);
            if (!this.IsPostBack)
            {
                InitializeVariables();

                

                txtStartDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                string StartDate = "01/" + txtStartDate.Text.Split('/')[1] + "/" + txtStartDate.Text.Split('/')[2];
                string EndDate = DateTime.DaysInMonth(Convert.ToInt32(txtEndDate.Text.Split('/')[2]), Convert.ToInt32(txtEndDate.Text.Split('/')[1])).ToString() + "/" + txtEndDate.Text.Split('/')[1] + "/" + txtEndDate.Text.Split('/')[2];

                txtStartDate.Text = StartDate;
                txtEndDate.Text = EndDate;

                NameValueCollection HTTPVariables = Request.QueryString;
                string[] strVariables = HTTPVariables.AllKeys;

                for (int i = 0; i < strVariables.Length; i++)
                {
                    switch (strVariables[i])
                    {
                        case "StartDate":
                            txtStartDate.Text = HTTPVariables.GetValues(strVariables[i])[0];
                            break;
                        case "EndDate":
                            txtEndDate.Text = HTTPVariables.GetValues(strVariables[i])[0];
                            break;
                    }
                }

                CalculateTotals();
            }
        }

        private void UpdateInvoicedDisplay(double TotalIRInvoicedOnsite, double TotalIRInvoicedOffsite, double TotalPRInvoicedOnsite, double TotalPRInvoicedOffsite)
        {
            txtTotalIRInvoicedOnsite.Text = TotalIRInvoicedOnsite.ToString("$0.00");
            txtTotalIRInvoicedOffsite.Text = TotalIRInvoicedOffsite.ToString("$0.00");
            txtTotalIRInvoiced.Text = (TotalIRInvoicedOnsite + TotalIRInvoicedOffsite).ToString("$0.00");
            txtTotalPRInvoicedOnsite.Text = TotalPRInvoicedOnsite.ToString("$0.00");
            txtTotalPRInvoicedOffsite.Text = TotalPRInvoicedOffsite.ToString("$0.00");
            txtTotalPRInvoiced.Text = (TotalPRInvoicedOnsite + TotalPRInvoicedOffsite).ToString("$0.00");
            txtTotalInvoicedOnsite.Text = (TotalIRInvoicedOnsite + TotalPRInvoicedOnsite).ToString("$0.00");
            txtTotalInvoicedOffsite.Text = (TotalIRInvoicedOffsite + TotalPRInvoicedOffsite).ToString("$0.00");
            txtTotalInvoiced.Text = (TotalIRInvoicedOnsite + TotalPRInvoicedOnsite + TotalIRInvoicedOffsite + TotalPRInvoicedOffsite).ToString("$0.00");

            txtDailyAvgIROnsite.Text = (TotalIRInvoicedOnsite / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");
            txtDailyAvgIROffsite.Text = (TotalIRInvoicedOffsite / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");
            txtDailyAvgIR.Text = ((TotalIRInvoicedOnsite + TotalIRInvoicedOffsite) / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");
            txtDailyAvgPROnsite.Text = (TotalPRInvoicedOnsite / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");
            txtDailyAvgPROffsite.Text = (TotalPRInvoicedOffsite / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");
            txtDailyAvgPR.Text = ((TotalPRInvoicedOnsite + TotalPRInvoicedOffsite) / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");

            txtDailyAvgOnsite.Text = ((TotalPRInvoicedOnsite + TotalIRInvoicedOnsite) / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");
            txtDailyAvgOffsite.Text = ((TotalPRInvoicedOffsite + TotalIRInvoicedOffsite) / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");
            txtDailyAvg.Text = ((TotalPRInvoicedOnsite + TotalIRInvoicedOnsite + TotalPRInvoicedOffsite + TotalIRInvoicedOffsite) / Convert.ToDouble(txtNumberDays.Text)).ToString("$0.00");

        }

        private void UpdateCostsDisplay(bool Empty)
        {
            double IRCostOnsite = 0;
            double IRCostOffsite = 0;
            double IRTimeOnsite = 0;
            double IRTimeOffsite = 0;
            double IRCost = 0;
            double IRTime = 0;
            double HourlyIRCost = 0;

            double PRCostOnsite = 0;
            double PRCostOffsite = 0;
            double PRTimeOnsite = 0;
            double PRTimeOffsite = 0;
            double PRCost = 0;
            double PRTime = 0;
            double HourlyPRCost = 0;

            double DVCostOnsite = 0;
            double DVCostOffsite = 0;
            double DVTimeOnsite = 0;
            double DVTimeOffsite = 0;
            double DVCost = 0;
            double DVTime = 0;
            double HourlyDVCost = 0;

            double INCostOnsite = 0;
            double INCostOffsite = 0;
            double INTimeOnsite = 0;
            double INTimeOffsite = 0;
            double INCost = 0;
            double INTime = 0;
            double HourlyINCost = 0;

            double RTCostOnsite = 0;
            double RTCostOffsite = 0;
            double RTTimeOnsite = 0;
            double RTTimeOffsite = 0;
            double RTCost = 0;
            double RTTime = 0;
            double HourlyRTCost = 0;

            double CostOffsite = 0;
            double CostOnsite = 0;
            double Cost = 0;

            double HourlyOffsiteCost = 0;
            double HourlyOnsiteCost = 0;
            double HourlyCost = 0;
            double TimeOffsite = 0;
            double TimeOnsite = 0;
            double Time = 0;

            if (!Empty)
            {
                for (int i = 0; i <= ListIndex; i++)
                {
                    IRCostOnsite += AllUserCosts[i].IROnsiteCost;
                    IRCostOffsite += AllUserCosts[i].IROffsiteCost;
                    IRTimeOnsite += AllUserCosts[i].IROnsiteTime;
                    IRTimeOffsite += AllUserCosts[i].IROffsiteTime;
                    PRCostOnsite += AllUserCosts[i].PROnsiteCost;
                    PRCostOffsite += AllUserCosts[i].PROffsiteCost;
                    PRTimeOnsite += AllUserCosts[i].PROnsiteTime;
                    PRTimeOffsite += AllUserCosts[i].PROffsiteTime;
                    DVCostOnsite += AllUserCosts[i].DVOnsiteCost;
                    DVCostOffsite += AllUserCosts[i].DVOffsiteCost;
                    DVTimeOnsite += AllUserCosts[i].DVOnsiteTime;
                    DVTimeOffsite += AllUserCosts[i].DVOffsiteTime;
                    INCostOnsite += AllUserCosts[i].INOnsiteCost;
                    INCostOffsite += AllUserCosts[i].INOffsiteCost;
                    INTimeOnsite += AllUserCosts[i].INOnsiteTime;
                    INTimeOffsite += AllUserCosts[i].INOffsiteTime;
                    RTCostOnsite += AllUserCosts[i].RTOnsiteCost;
                    RTCostOffsite += AllUserCosts[i].RTOffsiteCost;
                    RTTimeOnsite += AllUserCosts[i].RTOnsiteTime;
                    RTTimeOffsite += AllUserCosts[i].RTOffsiteTime;

                }

                IRCost = IRCostOffsite + IRCostOnsite;
                PRCost = PRCostOffsite + PRCostOnsite;
                DVCost = DVCostOffsite + DVCostOnsite;
                INCost = INCostOffsite + INCostOnsite;
                RTCost = RTCostOffsite + RTCostOnsite;

                CostOnsite = IRCostOnsite + PRCostOnsite + DVCostOnsite + INCostOnsite + RTCostOnsite;
                CostOffsite = IRCostOffsite + PRCostOffsite + DVCostOffsite + INCostOffsite + RTCostOffsite;
                Cost = CostOnsite + CostOffsite;

                IRTime = IRTimeOnsite + IRTimeOffsite;
                PRTime = PRTimeOnsite + PRTimeOffsite;
                DVTime = DVTimeOnsite + DVTimeOffsite;
                INTime = INTimeOnsite + INTimeOffsite;
                RTTime = RTTimeOnsite + RTTimeOffsite;

                if (IRTime > 0) HourlyIRCost = (IRCost / IRTime) * 60;
                if (PRTime > 0) HourlyPRCost = (PRCost / PRTime) * 60;
                if (DVTime > 0) HourlyDVCost = (DVCost / DVTime) * 60;
                if (INTime > 0) HourlyINCost = (INCost / INTime) * 60;
                if (RTTime > 0) HourlyRTCost = (RTCost / RTTime) * 60;

                TimeOffsite = IRTimeOffsite + PRTimeOffsite + DVTimeOffsite + INTimeOffsite + RTTimeOffsite;
                TimeOnsite = IRTimeOnsite + PRTimeOnsite + DVTimeOnsite + INTimeOnsite + RTTimeOnsite;
                Time = IRTimeOffsite + PRTimeOffsite + DVTimeOffsite + INTimeOffsite + RTTimeOffsite + IRTimeOnsite + PRTimeOnsite + DVTimeOnsite + INTimeOnsite + RTTimeOnsite;

                if (TimeOffsite > 0) HourlyOffsiteCost = (CostOffsite / TimeOffsite) * 60;
                if (TimeOnsite > 0) HourlyOnsiteCost = (CostOnsite / TimeOnsite) * 60;
                if (Time > 0) HourlyCost = (Cost / Time) * 60;
            }

            txtTotalIRCostOffsite.Text = IRCostOffsite.ToString("$0.00");
            txtTotalIRCostOnsite.Text = IRCostOnsite.ToString("$0.00");
            txtTotalIRCost.Text = IRCost.ToString("$0.00");
            txtTotalPRCostOffsite.Text = PRCostOffsite.ToString("$0.00");
            txtTotalPRCostOnsite.Text = PRCostOnsite.ToString("$0.00");
            txtTotalPRCost.Text = PRCost.ToString("$0.00");
            txtTotalDVCostOffsite.Text = DVCostOffsite.ToString("$0.00");
            txtTotalDVCostOnsite.Text = DVCostOnsite.ToString("$0.00");
            txtTotalDVCost.Text = DVCost.ToString("$0.00");
            txtTotalINCostOffsite.Text = INCostOffsite.ToString("$0.00");
            txtTotalINCostOnsite.Text = INCostOnsite.ToString("$0.00");
            txtTotalINCost.Text = INCost.ToString("$0.00");
            txtTotalRTCostOffsite.Text = RTCostOffsite.ToString("$0.00");
            txtTotalRTCostOnsite.Text = RTCostOnsite.ToString("$0.00");
            txtTotalRTCost.Text = RTCost.ToString("$0.00");
            txtTotalOnsiteCost.Text = CostOnsite.ToString("$0.00");
            txtTotalOffsiteCost.Text = CostOffsite.ToString("$0.00");
            txtTotalCost.Text = Cost.ToString("$0.00");

            txtTotalIRTimeOnsite.Text = ConvertMinutesToHHMM(IRTimeOnsite); 
            txtTotalIRTimeOffsite.Text = ConvertMinutesToHHMM(IRTimeOffsite);
            txtTotalIRTime.Text = ConvertMinutesToHHMM(IRTime);
            txtTotalPRTimeOnsite.Text = ConvertMinutesToHHMM(PRTimeOnsite); 
            txtTotalPRTimeOffsite.Text = ConvertMinutesToHHMM(PRTimeOffsite);
            txtTotalPRTime.Text = ConvertMinutesToHHMM(PRTime);
            txtTotalDVTimeOnsite.Text = ConvertMinutesToHHMM(DVTimeOnsite);
            txtTotalDVTimeOffsite.Text = ConvertMinutesToHHMM(DVTimeOffsite);
            txtTotalDVTime.Text = ConvertMinutesToHHMM(DVTime);
            txtTotalINTimeOnsite.Text = ConvertMinutesToHHMM(INTimeOnsite);
            txtTotalINTimeOffsite.Text = ConvertMinutesToHHMM(INTimeOffsite);
            txtTotalINTime.Text = ConvertMinutesToHHMM(INTime);
            txtTotalRTTimeOnsite.Text = ConvertMinutesToHHMM(RTTimeOnsite);
            txtTotalRTTimeOffsite.Text = ConvertMinutesToHHMM(RTTimeOffsite);
            txtTotalRTTime.Text = ConvertMinutesToHHMM(RTTime);
            txtTotalTimeOnsite.Text = ConvertMinutesToHHMM(TimeOnsite);
            txtTotalTimeOffsite.Text = ConvertMinutesToHHMM(TimeOffsite);
            txtTotalTime.Text = ConvertMinutesToHHMM(Time);

            txtHourlyIRCost.Text = HourlyIRCost.ToString("$0.00");
            txtHourlyPRCost.Text = HourlyPRCost.ToString("$0.00");
            txtHourlyDVCost.Text = HourlyDVCost.ToString("$0.00");
            txtHourlyINCost.Text = HourlyINCost.ToString("$0.00");
            txtHourlyRTCost.Text = HourlyRTCost.ToString("$0.00");

            txtHourlyOffsiteCost.Text = HourlyOffsiteCost.ToString("$0.00");
            txtHourlyOnsiteCost.Text = HourlyOnsiteCost.ToString("$0.00");
            txtHourlyCost.Text = HourlyCost.ToString("$0.00");
             
            
            if(IRCostOnsite > 0) IRCostOnsite = (IRCostOnsite / IRTimeOnsite) * 60;
            if(IRCostOffsite > 0) IRCostOffsite = (IRCostOffsite / IRTimeOffsite) * 60;
            if(PRCostOnsite > 0) PRCostOnsite = (PRCostOnsite / PRTimeOnsite) * 60;
            if(PRCostOffsite > 0) PRCostOffsite = (PRCostOffsite / PRTimeOffsite) * 60;
            if(DVCostOnsite > 0) DVCostOnsite = (DVCostOnsite / DVTimeOnsite) * 60;
            if(DVCostOffsite > 0) DVCostOffsite = (DVCostOffsite / DVTimeOffsite) * 60;
            if(INCostOnsite > 0) INCostOnsite = (INCostOnsite / INTimeOnsite) * 60;
            if(INCostOffsite > 0) INCostOffsite = (INCostOffsite / INTimeOffsite) * 60;
            if(RTCostOnsite > 0) RTCostOnsite = (RTCostOnsite / RTTimeOnsite) * 60;
            if(RTCostOffsite > 0) RTCostOffsite = (RTCostOffsite / RTTimeOffsite) * 60;

            txtHourlyIRCostOnsite.Text = IRCostOnsite.ToString("$0.00");
            txtHourlyIRCostOffsite.Text = IRCostOffsite.ToString("$0.00");
            txtHourlyPRCostOnsite.Text = PRCostOnsite.ToString("$0.00");
            txtHourlyPRCostOffsite.Text = PRCostOffsite.ToString("$0.00");
            txtHourlyDVCostOnsite.Text = DVCostOnsite.ToString("$0.00");
            txtHourlyDVCostOffsite.Text = DVCostOffsite.ToString("$0.00");
            txtHourlyINCostOnsite.Text = INCostOnsite.ToString("$0.00");
            txtHourlyINCostOffsite.Text = INCostOffsite.ToString("$0.00");
            txtHourlyRTCostOnsite.Text = RTCostOnsite.ToString("$0.00");
            txtHourlyRTCostOffsite.Text = RTCostOffsite.ToString("$0.00");

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

        private double GetHourlyRateOrIncrement(string CompanyName, string Username, string IncidentID, string SearchDate, List<ProjectRateView> viewProjectRates, List<UserRateView> viewUserRates, List<CompanyRateView> viewCompanyRates, List<UserRateView> viewUserPerCompanyRates, bool Onsite, string Category, bool isIncrement)
        {
            double HourlyRateOrIncrement = 0;

            if (SearchDate.Length == 5)
                SearchDate = SearchDate.Substring(0, 4) + "0" + SearchDate.Substring(4, 1);

            // looking up the project rate table to see if there is a price override

            if (!isIncrement)
            {
                ProjectRateView FoundProjectRate = viewProjectRates.FindLast(project => (project.DisplayName == Username) && (project.IncidentID == IncidentID));

                if (FoundProjectRate != null)
                {
                    if (Onsite) return (FoundProjectRate.OnsiteRate / 8);
                    else return (FoundProjectRate.OffsiteRate / 8);
                }
            }



            // looking up the user rate table to see if there is a price override
            UserRateView FoundUserRate = viewUserRates.FindLast(user => (user.DisplayName == Username) && (Convert.ToInt32(user.ValidFrom.Split(' ')[0].Split('/')[2] + user.ValidFrom.Split(' ')[0].Split('/')[1]) <= Convert.ToInt32(SearchDate)) && (user.IsRoundingRecord == isIncrement));

            if (FoundUserRate != null)
            {
                if (FoundUserRate.Override)
                {
                    //found a price override
                    if (Category == "Project")
                    {
                        if (Onsite)
                        {
                            if (FoundUserRate.ProjectOnsiteRate > 0) HourlyRateOrIncrement = FoundUserRate.ProjectOnsiteRate;
                            else HourlyRateOrIncrement = FoundUserRate.DefaultOnsiteRate;
                        }
                        else
                        {
                            if (FoundUserRate.ProjectOffsiteRate > 0) HourlyRateOrIncrement = FoundUserRate.ProjectOffsiteRate;
                            else HourlyRateOrIncrement = FoundUserRate.DefaultOffsiteRate;
                        }
                    }
                    else
                    {
                        if (Category != "Development" && Category != "Internal" && Category != "RITs")
                        {
                            if (Onsite)
                            {
                                if (FoundUserRate.MiscOnsiteRate > 0) HourlyRateOrIncrement = FoundUserRate.MiscOnsiteRate;
                                else HourlyRateOrIncrement = FoundUserRate.DefaultOnsiteRate;
                            }
                            else
                            {
                                if (FoundUserRate.MiscOffsiteRate > 0) HourlyRateOrIncrement = FoundUserRate.MiscOffsiteRate;
                                else HourlyRateOrIncrement = FoundUserRate.DefaultOffsiteRate;
                            }
                        }
                        else
                        {
                            if (Onsite) HourlyRateOrIncrement = FoundUserRate.DefaultOnsiteRate;
                            else HourlyRateOrIncrement = FoundUserRate.DefaultOffsiteRate;
                        }
                    }
                    return HourlyRateOrIncrement;
                }
            }

            //no rate override at the user level, checking if there is a specific company rate for a specific user
            UserRateView FoundUserPerCompanyRate = viewUserPerCompanyRates.FindLast(user => (user.DisplayName == Username) && (Convert.ToInt32(user.ValidFrom.Split(' ')[0].Split('/')[2] + user.ValidFrom.Split(' ')[0].Split('/')[1]) <= Convert.ToInt32(SearchDate)) && (user.IsRoundingRecord == isIncrement));
            if (FoundUserPerCompanyRate != null)
            {
                if (Category == "Project")
                {
                    if (Onsite)
                    {
                        if (FoundUserPerCompanyRate.ProjectOnsiteRate > 0) HourlyRateOrIncrement = FoundUserPerCompanyRate.ProjectOnsiteRate;
                        else HourlyRateOrIncrement = FoundUserPerCompanyRate.DefaultOnsiteRate;
                    }
                    else
                    {
                        if (FoundUserPerCompanyRate.ProjectOffsiteRate > 0) HourlyRateOrIncrement = FoundUserPerCompanyRate.ProjectOffsiteRate;
                        else HourlyRateOrIncrement = FoundUserPerCompanyRate.DefaultOffsiteRate;
                    }
                }
                else
                {
                    if (Category != "Development" && Category != "Internal" && Category != "RITs")
                    {
                        if (Onsite)
                        {
                            if (FoundUserPerCompanyRate.MiscOnsiteRate > 0) HourlyRateOrIncrement = FoundUserPerCompanyRate.MiscOnsiteRate;
                            else HourlyRateOrIncrement = FoundUserPerCompanyRate.DefaultOnsiteRate;
                        }
                        else
                        {
                            if (FoundUserPerCompanyRate.MiscOffsiteRate > 0) HourlyRateOrIncrement = FoundUserPerCompanyRate.MiscOffsiteRate;
                            else HourlyRateOrIncrement = FoundUserPerCompanyRate.DefaultOffsiteRate;
                        }
                    }
                    else
                    {
                        if (Onsite) HourlyRateOrIncrement = FoundUserPerCompanyRate.DefaultOnsiteRate;
                        else HourlyRateOrIncrement = FoundUserPerCompanyRate.DefaultOffsiteRate;
                    }
                }
                return HourlyRateOrIncrement;
            }

            //no rate/ override at the user level, so looking in the company rate table what rate to use.
            CompanyRateView FoundCompanyRate = viewCompanyRates.FindLast(Company => (Company.CompanyName == CompanyName) && (Convert.ToInt32(Company.ValidFrom.Split(' ')[0].Split('/')[2] + Company.ValidFrom.Split(' ')[0].Split('/')[1]) <= Convert.ToInt32(SearchDate)) && (Company.IsRoundingRecord == isIncrement));
            if (FoundCompanyRate != null)
            {
                if (Category == "Project")
                {
                    if (Onsite)
                    {
                        if (FoundCompanyRate.ProjectOnsite > 0) HourlyRateOrIncrement = FoundCompanyRate.ProjectOnsite;
                        else HourlyRateOrIncrement = FoundCompanyRate.DefaultOnsite;
                    }
                    else
                    {
                        if (FoundCompanyRate.ProjectOffsite > 0) HourlyRateOrIncrement = FoundCompanyRate.ProjectOffsite;
                        else HourlyRateOrIncrement = FoundCompanyRate.DefaultOffsite;
                    }
                }
                else
                {
                    if (Category != "Development" && Category != "Internal" && Category != "RITs")
                    {
                        if (Onsite)
                        {
                            if (FoundCompanyRate.MiscOnsite > 0) HourlyRateOrIncrement = FoundCompanyRate.MiscOnsite;
                            else HourlyRateOrIncrement = FoundCompanyRate.DefaultOnsite;
                        }
                        else
                        {
                            if (FoundCompanyRate.MiscOffsite > 0) HourlyRateOrIncrement = FoundCompanyRate.MiscOffsite;
                            else HourlyRateOrIncrement = FoundCompanyRate.DefaultOffsite;
                        }
                    }
                    else
                    {
                        if (Onsite) HourlyRateOrIncrement = FoundCompanyRate.DefaultOnsite;
                        else HourlyRateOrIncrement = FoundCompanyRate.DefaultOffsite;
                    }
                }
                return HourlyRateOrIncrement;
            }

            CompanyRateView FoundDefaultRate = viewCompanyRates.FindLast(Company => (Company.CompanyName == "Default") && (Company.IsRoundingRecord == isIncrement));
            if (FoundDefaultRate != null)
            {
                if (Category == "Project")
                {
                    if (Onsite)
                    {
                        if (FoundDefaultRate.ProjectOnsite > 0) HourlyRateOrIncrement = FoundDefaultRate.ProjectOnsite;
                        else HourlyRateOrIncrement = FoundDefaultRate.DefaultOnsite;
                    }
                    else
                    {
                        if (FoundDefaultRate.ProjectOffsite > 0) HourlyRateOrIncrement = FoundDefaultRate.ProjectOffsite;
                        else HourlyRateOrIncrement = FoundDefaultRate.DefaultOffsite;
                    }
                }
                else
                {
                    if (Category != "Development" && Category != "Internal" && Category != "RITs")
                    {
                        if (Onsite)
                        {
                            if (FoundDefaultRate.MiscOnsite > 0) HourlyRateOrIncrement = FoundDefaultRate.MiscOnsite;
                            else HourlyRateOrIncrement = FoundDefaultRate.DefaultOnsite;
                        }
                        else
                        {
                            if (FoundDefaultRate.MiscOffsite > 0) HourlyRateOrIncrement = FoundDefaultRate.MiscOffsite;
                            else HourlyRateOrIncrement = FoundDefaultRate.DefaultOffsite;
                        }
                    }
                    else
                    {
                        if (Onsite) HourlyRateOrIncrement = FoundDefaultRate.DefaultOnsite;
                        else HourlyRateOrIncrement = FoundDefaultRate.DefaultOffsite;
                    }
                }
            }


            return HourlyRateOrIncrement;
        }

        protected void CalculateUserTotalCosts(int CurrentIndex)
        {
            AllUserCosts[CurrentIndex].CostPerMinute = AllUserCosts[CurrentIndex].TotalCost / AllUserCosts[CurrentIndex].TotalMinutesWorked;
            AllUserCosts[CurrentIndex].INOnsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].INOnsiteTime;
            AllUserCosts[CurrentIndex].INOffsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].INOffsiteTime;
            AllUserCosts[CurrentIndex].IROnsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].IROnsiteTime;
            AllUserCosts[CurrentIndex].IROffsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].IROffsiteTime;
            AllUserCosts[CurrentIndex].RTOnsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].RTOnsiteTime;
            AllUserCosts[CurrentIndex].RTOffsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].RTOffsiteTime;
            AllUserCosts[CurrentIndex].DVOnsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].DVOnsiteTime;
            AllUserCosts[CurrentIndex].DVOffsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].DVOffsiteTime;
            AllUserCosts[CurrentIndex].PROnsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].PROnsiteTime;
            AllUserCosts[CurrentIndex].PROffsiteCost = AllUserCosts[CurrentIndex].CostPerMinute * AllUserCosts[CurrentIndex].PROffsiteTime;
            
        }

        protected void AddTimes(EntriesViewForCosts CurrentEntry,List<UserCostView> UserCost)
        {
            int TempMonth = 0;
            int SearchDate = 0;

            AllUserCosts[ListIndex].Username = CurrentUser;
            AllUserCosts[ListIndex].YearMonth = CurrentEntry.EnteredYearMonth;
            TempMonth = Convert.ToInt32(CurrentEntry.EnteredYearMonth.Substring(4, CurrentEntry.EnteredYearMonth.Length - 4));
            if (TempMonth >= 10) SearchDate = Convert.ToInt32(CurrentEntry.EnteredYearMonth + "01");
            else SearchDate = Convert.ToInt32(CurrentEntry.EnteredYearMonth.Substring(0, 4) + "0" + CurrentEntry.EnteredYearMonth.Substring(4, CurrentEntry.EnteredYearMonth.Length - 4) + "01");

            UserCostView FoundUserCost2 = UserCost.FindLast(user => (user.DisplayName == PreviousUser) && (Convert.ToInt32(user.ValidFrom.Split(' ')[0].Split('/')[2] + user.ValidFrom.Split(' ')[0].Split('/')[1] + "01" ) <= SearchDate));
            AllUserCosts[ListIndex].TotalCost = FoundUserCost2.Cost;

            AllUserCosts[ListIndex].TotalMinutesWorked += CurrentEntry.TimeInMinutes;
            switch (CurrentEntry.Category)
            {
                case "Internal":
                    if (CurrentEntry.OnSite) AllUserCosts[ListIndex].INOnsiteTime = CurrentEntry.TimeInMinutes;
                    else AllUserCosts[ListIndex].INOffsiteTime = CurrentEntry.TimeInMinutes;
                    break;
                case "Development":
                    if (CurrentEntry.OnSite) AllUserCosts[ListIndex].DVOnsiteTime = CurrentEntry.TimeInMinutes;
                    else AllUserCosts[ListIndex].DVOffsiteTime = CurrentEntry.TimeInMinutes;
                    break;
                case "RITs":
                    if (CurrentEntry.OnSite) AllUserCosts[ListIndex].RTOnsiteTime = CurrentEntry.TimeInMinutes;
                    else AllUserCosts[ListIndex].RTOffsiteTime = CurrentEntry.TimeInMinutes;
                    break;
                case "Project":
                    if (CurrentEntry.OnSite) AllUserCosts[ListIndex].PROnsiteTime = CurrentEntry.TimeInMinutes;
                    else AllUserCosts[ListIndex].PROffsiteTime = CurrentEntry.TimeInMinutes;
                    break;
                default:
                    if (CurrentEntry.OnSite) AllUserCosts[ListIndex].IROnsiteTime += CurrentEntry.TimeInMinutes;
                    else AllUserCosts[ListIndex].IROffsiteTime += CurrentEntry.TimeInMinutes;
                    break;
            }
        }

        protected void InitializeVariables()
        {
            AllUserCosts.Clear();
            MinutesINOnsite = 0;
            MinutesINOffsite = 0;
            MinutesRTOnsite = 0;
            MinutesRTOffsite = 0;
            MinutesPROnsite = 0;
            MinutesPROffsite = 0;
            MinutesDVOnsite = 0;
            MinutesDVOffsite = 0;
            MinutesIROnsite = 0;
            MinutesIROffsite = 0;
            TotalMinutesWorked = 0;
            ListIndex = 0; //Variable used as index in the TotalUserCosts list to store values in the right area
            LastRecordDate = "";
            PreviousUser = "";
            CurrentUser = "";
            

        }

        protected void UpdateTempClientOverviewTable(string CompanyName, double PRCost, double PRInvoiced, double IRCost, double IRInvoiced)
        {
            double PRMargin = PRInvoiced-PRCost;
            double IRMargin = IRInvoiced - IRCost;
            double PRMarginPercent = 0;
            double IRMarginPercent = 0; 

            if (PRInvoiced > 0) PRMarginPercent = ((PRInvoiced - PRCost) / PRInvoiced) * 100;
            if (IRInvoiced > 0) IRMarginPercent = ((IRInvoiced - IRCost) / IRInvoiced) * 100;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CompanyName", CompanyName));
            parameters.Add(new SqlParameter("@ProjectCost", PRCost));
            parameters.Add(new SqlParameter("@ProjectInvoiced", PRInvoiced));
            parameters.Add(new SqlParameter("@ProjectMargin", PRMargin));
            parameters.Add(new SqlParameter("@ProjectMarginPercent", PRMarginPercent));
            parameters.Add(new SqlParameter("@MiscCost", IRCost));
            parameters.Add(new SqlParameter("@MiscInvoiced", IRInvoiced));
            parameters.Add(new SqlParameter("@MiscMargin", IRMargin));
            parameters.Add(new SqlParameter("@MiscMarginPercent", IRMarginPercent));

            DataAccessLayer.Instance.ExecuteQuery(@"INSERT INTO TempClientOverview VALUES(@CompanyName,@ProjectCost,@ProjectInvoiced,@ProjectMargin,@ProjectMarginPercent,@MiscCost,@MiscInvoiced,@MiscMargin,@MiscMarginPercent)", parameters.ToArray());
        }

        private int RoundUpMinutes(int MinutesWorked, int Increment)
        {

            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Convert.ToDouble(MinutesWorked) / Convert.ToDouble(Increment))) * Convert.ToDouble(Increment));
            
            
        }

        private void RefreshClientTotalsGrid()
        {
            string Query = "SELECT * FROM TempClientOverview";

            this.GridClientTotals.DataSource = DataAccessLayer.Instance.GetEntities<TempClientOverview>(Query);
            this.GridClientTotals.DataBind();


        }

        private void InitialiseUserCosts(TotalUserCosts UserCosts)
        {
            UserCosts.CostPerMinute = 0;
            UserCosts.DVOffsiteCost = 0;
            UserCosts.DVOffsiteTime = 0;
            UserCosts.DVOnsiteCost = 0;
            UserCosts.DVOnsiteTime = 0;
            UserCosts.INOffsiteCost = 0;
            UserCosts.INOffsiteTime = 0;
            UserCosts.INOnsiteCost = 0;
            UserCosts.INOnsiteTime = 0;
            UserCosts.IROffsiteCost = 0;
            UserCosts.IROffsiteTime = 0;
            UserCosts.IROnsiteCost = 0;
            UserCosts.IROnsiteTime = 0;
            UserCosts.YearMonth = "";
            UserCosts.PROffsiteCost = 0;
            UserCosts.PROffsiteTime = 0;
            UserCosts.PROnsiteCost = 0;
            UserCosts.PROnsiteTime = 0;
            UserCosts.RTOffsiteCost = 0;
            UserCosts.RTOffsiteTime = 0;
            UserCosts.RTOnsiteCost = 0;
            UserCosts.RTOnsiteTime = 0;
            UserCosts.TotalCost = 0;
            UserCosts.TotalMinutesWorked = 0;
            UserCosts.Username = "";
        }

        protected void GridClientTotals_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
            {
                GridViewRow gvr = e.Row;

                if (gvr.RowType == DataControlRowType.Header)
                {
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = "";
                    cell.BorderStyle = BorderStyle.None;
                    cell.BorderWidth = 0;
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 4;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Text = "PROJECTS";
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.FromName("#dddddd");
                    cell.BackColor = Color.FromName("#3C454F");
                    cell.ForeColor = Color.FromName("#EEEEEE");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 4;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.FromName("#dddddd");
                    cell.BackColor = Color.FromName("#3C454F");
                    cell.ForeColor = Color.FromName("#EEEEEE");
                    cell.Text = "MISCELLANEOUS";
                    row.Cells.Add(cell);

                    GridClientTotals.Controls[0].Controls.AddAt(0, row);
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label ProjectCost = e.Row.FindControl("lblProjectCost") as Label;
                    Label ProjectInvoiced = e.Row.FindControl("lblProjectInvoiced") as Label;
                    Label ProjectMargin = e.Row.FindControl("lblProjectMargin") as Label;
                    Label ProjectMarginPercent = e.Row.FindControl("lblProjectMarginPercent") as Label;
                    Label MiscCost = e.Row.FindControl("lblMiscCost") as Label;
                    Label MiscInvoiced = e.Row.FindControl("lblMiscInvoiced") as Label;
                    Label MiscMargin = e.Row.FindControl("lblMiscMargin") as Label;
                    Label MiscMarginPercent = e.Row.FindControl("lblMiscMarginPercent") as Label;

                    string QueryConfiguration = "SELECT * from Configuration WHERE ParameterName='RevenueThreshold'";
                    List<ConfigurationEntity> Configuration = DataAccessLayer.Instance.GetEntities<ConfigurationEntity>(QueryConfiguration);

                    //Color PR cells in the client overview tables depending on their values
                    if (Convert.ToDouble(ProjectCost.Text) > 0)
                    {
                        if (Convert.ToDouble(ProjectCost.Text) > Convert.ToDouble(ProjectInvoiced.Text))
                        {
                            ProjectCost.BackColor = Color.IndianRed;
                            ProjectInvoiced.BackColor = Color.IndianRed;
                            ProjectMargin.BackColor = Color.IndianRed;
                            ProjectMarginPercent.BackColor = Color.IndianRed;
                        }
                        else
                        {
                            if (Convert.ToDouble(ProjectCost.Text) > (Convert.ToDouble(ProjectInvoiced.Text) - (Convert.ToDouble(ProjectCost.Text) * (Convert.ToDouble(Configuration[0].ParameterValue)/100))))
                            {
                                ProjectCost.BackColor = Color.LightYellow;
                                ProjectInvoiced.BackColor = Color.LightYellow;
                                ProjectMargin.BackColor = Color.LightYellow;
                                ProjectMarginPercent.BackColor = Color.LightYellow;
                            }
                            else
                            {
                                ProjectCost.BackColor = Color.ForestGreen;
                                ProjectInvoiced.BackColor = Color.ForestGreen;
                                ProjectMargin.BackColor = Color.ForestGreen;
                                ProjectMarginPercent.BackColor = Color.ForestGreen;
                            }
                        }   
                    }
                    else
                    {
                        ProjectCost.BackColor = Color.Transparent;
                        ProjectInvoiced.BackColor = Color.Transparent;
                        ProjectMargin.BackColor = Color.Transparent;
                        ProjectMarginPercent.BackColor = Color.Transparent;
                    }

                    //Color IR cells in the client overview tables depending on their values

                    if (Convert.ToDouble(MiscCost.Text) > 0)
                    {
                        if (Convert.ToDouble(MiscCost.Text) > Convert.ToDouble(MiscInvoiced.Text))
                        {
                            MiscCost.BackColor = Color.IndianRed;
                            MiscInvoiced.BackColor = Color.IndianRed;
                            MiscMargin.BackColor = Color.IndianRed;
                            MiscMarginPercent.BackColor = Color.IndianRed;
                        }
                        else
                        {
                            if (Convert.ToDouble(MiscCost.Text) > (Convert.ToDouble(MiscInvoiced.Text) - (Convert.ToDouble(MiscCost.Text) * (Convert.ToDouble(Configuration[0].ParameterValue) / 100))))
                            {
                                MiscCost.BackColor = Color.LightYellow;
                                MiscInvoiced.BackColor = Color.LightYellow;
                                MiscMargin.BackColor = Color.LightYellow;
                                MiscMarginPercent.BackColor = Color.LightYellow;
                            }
                            else
                            {
                                MiscCost.BackColor = Color.ForestGreen;
                                MiscInvoiced.BackColor = Color.ForestGreen;
                                MiscMargin.BackColor = Color.ForestGreen;
                                MiscMarginPercent.BackColor = Color.ForestGreen;
                            }
                        }
                    }
                    else
                    {
                        MiscCost.BackColor = Color.Transparent;
                        MiscInvoiced.BackColor = Color.Transparent;
                        MiscMargin.BackColor = Color.Transparent;
                        MiscMarginPercent.BackColor = Color.Transparent;
                    }

                                        
                    ProjectMargin.Text = "$" + ProjectMargin.Text;
                    ProjectCost.Text = "$" + ProjectCost.Text;
                    ProjectInvoiced.Text = "$" + ProjectInvoiced.Text;
                    ProjectMarginPercent.Text = ProjectMarginPercent.Text + "%";

                    MiscMargin.Text = "$" + MiscMargin.Text;
                    MiscCost.Text = "$" + MiscCost.Text;
                    MiscInvoiced.Text = "$" + MiscInvoiced.Text;
                    MiscMarginPercent.Text = MiscMarginPercent.Text + "%";
                }
            }
        }

        private void CalculateTotals()
        {
            InitializeVariables();

            string PreviousCompanyName = "";
            string CurrentCompanyName = "";
            int PreviousMonth = 0;
            int CurrentMonth = 0;
            string[] MonthList = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (txtStartDate.Text == "") txtStartDate.Text = DateTime.Now.ToShortDateString();
            if (txtEndDate.Text == "") txtEndDate.Text = DateTime.Now.ToShortDateString();

            //temporary variables that will store the total minutes worked for a user per category;
            string StartDate = txtStartDate.Text;
            string EndDate = txtEndDate.Text;


            StartDate = "01/" + txtStartDate.Text.Split('/')[1] + "/" + txtStartDate.Text.Split('/')[2];
            EndDate = DateTime.DaysInMonth(Convert.ToInt32(txtEndDate.Text.Split('/')[2]), Convert.ToInt32(txtEndDate.Text.Split('/')[1])).ToString() + "/" + txtEndDate.Text.Split('/')[1] + "/" + txtEndDate.Text.Split('/')[2];

            int StartMonth = Convert.ToDateTime(txtStartDate.Text).Month;
            int EndMonth = Convert.ToDateTime(txtEndDate.Text).Month;

            if (StartMonth != EndMonth) lblPeriod.Text = MonthList[StartMonth - 1] + " " + txtStartDate.Text.Split('/')[2] + " - " + MonthList[EndMonth - 1] + " " + txtEndDate.Text.Split('/')[2];
            else lblPeriod.Text = MonthList[StartMonth - 1] + " " + txtStartDate.Text.Split('/')[2];

            //Delete all records from the temporary table used to display the values on the screen in the GRID
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fake", "fake"));
            DataAccessLayer.Instance.ExecuteQuery("DELETE FROM TempClientOverview", parameters.ToArray());

            //Retrieve the company rates (for billables)
            string QueryCompanyRate = "SELECT * from viewCompanyRate WHERE ValidFrom<=CONVERT(datetime,'" + EndDate + "',103) ORDER BY ValidFrom";
            List<CompanyRateView> viewCompanyRates = DataAccessLayer.Instance.GetEntities<CompanyRateView>(QueryCompanyRate);

            //Retrieve the user rates (for billables)
            string QueryUserRate = "SELECT * from viewUserRate WHERE ValidFrom<=CONVERT(datetime,'" + EndDate + "',103) AND (CompanyID IS NULL OR CompanyID = '')  ORDER BY ValidFrom";
            List<UserRateView> viewUserRates = DataAccessLayer.Instance.GetEntities<UserRateView>(QueryUserRate);

            //Retrieve the user rates (for billables)
            string QueryUserPerCompanyRate = "SELECT * from viewUserRate WHERE ValidFrom<=CONVERT(datetime,'" + EndDate + "',103) AND CompanyID IS NOT NULL AND CompanyID <> '' ORDER BY ValidFrom";
            List<UserRateView> viewUserPerCompanyRates = DataAccessLayer.Instance.GetEntities<UserRateView>(QueryUserPerCompanyRate);

            string QueryUserPerProjectRate = "SELECT * from viewProjectRate";
            List<ProjectRateView> viewUserPerProjectRates = DataAccessLayer.Instance.GetEntities<ProjectRateView>(QueryUserPerProjectRate);

            //Retrieve the user costs
            string QueryUserCost = "SELECT * from viewUserCost WHERE ValidFrom<=CONVERT(datetime,'" + EndDate + "',103) ORDER BY ValidFrom";
            List<UserCostView> UserCost = DataAccessLayer.Instance.GetEntities<UserCostView>(QueryUserCost);

            //Retrieve all timesheet entries using the 'Entries' SQL view, sorted by Users to calculate the costs per user for the desired period
            string QueryUserCostForPeriod;
            if (cbManagementReview.Checked)
                QueryUserCostForPeriod = "SELECT EnteredBy,Convert(varchar,datepart(year,EnteredDate))+Convert(varchar,datepart(month,EnteredDate)) as EnteredYearMonth,Category,SUM(TimeInMinutes) as TimeInMinutes,OnSite from Entries WHERE EnteredDate>=CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate + "',103) AND Error=0 AND ManagementReview=" + Convert.ToInt32(cbManagementReview.Checked) + " GROUP By EnteredBy,Convert(varchar,datepart(year,EnteredDate))+Convert(varchar,datepart(month,EnteredDate)),Category,OnSite ORDER BY 1,2";
            else
                QueryUserCostForPeriod = "SELECT EnteredBy,Convert(varchar,datepart(year,EnteredDate))+Convert(varchar,datepart(month,EnteredDate)) as EnteredYearMonth,Category,SUM(TimeInMinutes) as TimeInMinutes,OnSite from Entries WHERE EnteredDate>=CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate + "',103) AND Error=0 GROUP By EnteredBy,Convert(varchar,datepart(year,EnteredDate))+Convert(varchar,datepart(month,EnteredDate)),Category,OnSite ORDER BY 1,2";
            List<EntriesViewForCosts> AllEntriesForCost = DataAccessLayer.Instance.GetEntities<EntriesViewForCosts>(QueryUserCostForPeriod);

            

            if (AllEntriesForCost.Count > 0)
            {

                PreviousUser = AllEntriesForCost[0].EnteredBy;
                PreviousMonth = Convert.ToInt32(AllEntriesForCost[0].EnteredYearMonth);

                AllUserCosts.Add(new TotalUserCosts());
                InitialiseUserCosts(AllUserCosts[0]);

                //Loop through all timesheet entries to calculate costs
                foreach (EntriesViewForCosts TimeEntry in AllEntriesForCost)
                {
                    CurrentUser = TimeEntry.EnteredBy;
                    CurrentMonth = Convert.ToInt32(TimeEntry.EnteredYearMonth);

                    if (PreviousUser == CurrentUser && PreviousMonth == CurrentMonth)
                    {
                        AddTimes(TimeEntry, UserCost);
                    }
                    else
                    {
                        //Found a new user. Adding a new totalusercosts class to the existing list and incrementing the index
                        AllUserCosts.Add(new TotalUserCosts());
                        ListIndex++;
                        InitialiseUserCosts(AllUserCosts[ListIndex]);
                        PreviousUser = CurrentUser;
                        PreviousMonth = CurrentMonth;
                        AddTimes(TimeEntry, UserCost);
                    }

                }

                for (int i = 0; i <= ListIndex; i++)
                    CalculateUserTotalCosts(i);

                //Retrieve all timesheet entries using the 'Entries' SQL view 
                string query;
                string QueryNegativeTimes;
                if (cbManagementReview.Checked)
                {
                    query = "SELECT * from Entries WHERE EnteredDate>=CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate + "',103) AND ManagementReview=" + Convert.ToInt32(cbManagementReview.Checked) + " AND Error=0 AND TimeInMinutes > 0 AND (Category='Incident' OR Category='Project' OR Category='Service Request') ORDER BY Company,EnteredDate,EnteredBy";
                    QueryNegativeTimes = "SELECT * from Entries WHERE EnteredDate>=CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate + "',103) AND ManagementReview=" + Convert.ToInt32(cbManagementReview.Checked) + " AND Error=0 AND TimeInMinutes < 0 AND (Category='Incident' OR Category='Project'  OR Category='Service Request') ORDER BY EntityChangeLogId";
                }
                else
                {
                    query = "SELECT * from Entries WHERE EnteredDate>=CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate + "',103) AND Error=0 AND TimeInMinutes > 0 AND (Category='Incident' OR Category='Project' OR Category='Service Request') ORDER BY Company,EnteredDate,EnteredBy";
                    QueryNegativeTimes = "SELECT * from Entries WHERE EnteredDate>=CONVERT(datetime,'" + StartDate + "',103) AND EnteredDate<=CONVERT(datetime,'" + EndDate + "',103) AND Error=0 AND TimeInMinutes < 0 AND (Category='Incident' OR Category='Project' OR Category='Service Request') ORDER BY EntityChangeLogId";
                }

                List<EntriesView> AllNegativeEntries = DataAccessLayer.Instance.GetEntities<EntriesView>(QueryNegativeTimes);
                List<EntriesView> AllEntries = DataAccessLayer.Instance.GetEntities<EntriesView>(query);

                double PRCost = 0;
                double IRCost = 0;
                double PRInvoiced = 0;
                double IRInvoiced = 0;
                double TotalIRInvoicedOnsite = 0;
                double TotalIRInvoicedOffsite = 0;
                double TotalPRInvoicedOnsite = 0;
                double TotalPRInvoicedOffsite = 0;

                if (AllEntries.Count > 0)
                {
                    PreviousCompanyName = AllEntries[0].Company.ToString();
                    PreviousMonth = Convert.ToInt32(AllEntries[0].EnteredDate.Split(' ')[0].Split('/')[1]);
                    PreviousUser = AllEntries[0].EnteredBy;





                    //Loop through the timesheet entries to calculate total costs and invoiced for each companies
                    foreach (EntriesView TimeEntry in AllEntries)
                    {
                        CurrentCompanyName = TimeEntry.Company;
                        CurrentMonth = Convert.ToInt32(TimeEntry.EnteredDate.Split(' ')[0].Split('/')[1]);
                        CurrentUser = TimeEntry.EnteredBy;
                        int TimeSpent = TimeEntry.TimeInMinutes;

                        List<EntriesView> FoundNegativeTime = AllNegativeEntries.FindAll(NegativeEntry => (NegativeEntry.Comment == TimeEntry.EntityChangeLogId.ToString()));
                        foreach (EntriesView ThisNegativeTime in FoundNegativeTime)
                        {
                            if (TimeSpent > ThisNegativeTime.TimeInMinutes)
                                TimeSpent += ThisNegativeTime.TimeInMinutes;
                            else
                                TimeSpent = 0;
                        }


                        if (PreviousCompanyName != TimeEntry.Company)
                        {
                            UpdateTempClientOverviewTable(PreviousCompanyName, PRCost, PRInvoiced, IRCost, IRInvoiced);
                            IRCost = 0;
                            PRCost = 0;
                            IRInvoiced = 0;
                            PRInvoiced = 0;
                            PreviousCompanyName = CurrentCompanyName;
                        }



                        string SearchDate = Convert.ToInt32(TimeEntry.EnteredDate.Split(' ')[0].Split('/')[2]).ToString() + Convert.ToInt32(TimeEntry.EnteredDate.Split(' ')[0].Split('/')[1]).ToString();
                        TotalUserCosts FoundAllUserCost = AllUserCosts.FindLast(user => (user.Username == TimeEntry.EnteredBy) && (user.YearMonth == SearchDate));

                        int Increment = Convert.ToInt32(GetHourlyRateOrIncrement(TimeEntry.Company, TimeEntry.EnteredBy, TimeEntry.IncidentID, SearchDate, viewUserPerProjectRates, viewUserRates, viewCompanyRates, viewUserPerCompanyRates, TimeEntry.OnSite, TimeEntry.Category, true));

                        double HourlyRate = 0;
                        if (TimeEntry.SubCategory != "PS - T&M PrePaid" & TimeEntry.SubCategory != "PS - Fixed Price")
                            HourlyRate = GetHourlyRateOrIncrement(TimeEntry.Company, TimeEntry.EnteredBy, TimeEntry.IncidentID, SearchDate, viewUserPerProjectRates, viewUserRates, viewCompanyRates, viewUserPerCompanyRates, TimeEntry.OnSite, TimeEntry.Category, false);

                        if (TimeEntry.Category == "Project")
                        {
                            PRCost += FoundAllUserCost.CostPerMinute * TimeSpent;
                            if (TimeEntry.Billable)
                            {
                                double TimeInMinutes = RoundUpMinutes(TimeSpent, Increment);
                                double TimeInHours = TimeInMinutes / 60;
                                PRInvoiced += (TimeInHours * HourlyRate);
                                if (TimeEntry.OnSite) TotalPRInvoicedOnsite += (TimeInHours * HourlyRate);
                                else TotalPRInvoicedOffsite += (TimeInHours * HourlyRate);
                            }
                        }
                        else
                        {
                            if (TimeEntry.Category != "Internal" && TimeEntry.Category != "RITs" && TimeEntry.Category != "Development")
                            {
                                IRCost += FoundAllUserCost.CostPerMinute * TimeSpent;
                                if (TimeEntry.Billable)
                                {
                                    double TimeInMinutes = RoundUpMinutes(TimeSpent, Increment);
                                    double TimeInHours = TimeInMinutes / 60;
                                    IRInvoiced += TimeInHours * HourlyRate;
                                    if (TimeEntry.OnSite) TotalIRInvoicedOnsite += (TimeInHours * HourlyRate);
                                    else TotalIRInvoicedOffsite += (TimeInHours * HourlyRate);
                                }
                            }
                        }

                    }

                    UpdateTempClientOverviewTable(PreviousCompanyName, PRCost, PRInvoiced, IRCost, IRInvoiced);
                    UpdateCostsDisplay(false);
                    UpdateInvoicedDisplay(TotalIRInvoicedOnsite, TotalIRInvoicedOffsite, TotalPRInvoicedOnsite, TotalPRInvoicedOffsite);
                }

            }
            else
            {
                UpdateInvoicedDisplay(0, 0, 0, 0);
                UpdateCostsDisplay(true);
            }

            RefreshClientTotalsGrid();
            
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {

            CalculateTotals();

        }

        protected void GridClientTotals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridClientTotals.PageIndex = e.NewPageIndex;
            RefreshClientTotalsGrid();
        }

        protected void btnCompanyName_Click(object sender, EventArgs e)
        {
            Button CompanyName = (Button)sender;

            string StartDate = "01/" + txtStartDate.Text.Split('/')[1] + "/" + txtStartDate.Text.Split('/')[2];
            string EndDate = DateTime.DaysInMonth(Convert.ToInt32(txtEndDate.Text.Split('/')[2]), Convert.ToInt32(txtEndDate.Text.Split('/')[1])).ToString() + "/" + txtEndDate.Text.Split('/')[1] + "/" + txtEndDate.Text.Split('/')[2];

            string URL = "TimeReview.aspx?Company=";
            URL += CompanyName.Text;
            URL += "&StartDate=";
            URL += StartDate;
            URL += "&EndDate=";
            URL += EndDate;

            Response.Redirect(URL);
        }
        
    }
}