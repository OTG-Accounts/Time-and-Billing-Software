using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IncidentEntity
/// </summary>
public class IncidentEntity
{
    private string _EntityID;
    private string _IncidentID;
    private string _Title;
    private string _Description;
    private int _TotalTimeInMinutes;
    private string _Category;
    private string _Status;
    private string _AffectedUser;
    private string _CompanyID;
    private string _ResolutionCategory;
    private string _ResolutionDescription;
    private string _Escalated;
    private string _CreatedDate;
    private string _ResolvedDate;
    private string _ClosedDate;
    private string _FirstAssignedDate;
    private string _FirstResponseDate;
    private string _ActualStartDate;
    private string _ActualEndDate;
    private string _TargetResolutionTime;
    private int _Priority;
    private string _Impact;
    private string _Urgency;
    private string _Source;
    private string _SubCategory;
    private string _ClientReference;
    private bool _FixedCostProject;
    private int _DailyRate;
    private int _TotalDaysAllocated;
    private string _CreatedBy;
    private string _AssignedTo;
    private string _ResolvedBy;
    private string _ClosedBy;
    private double _TotalHoursAllocated;
    private double _EstimatedRevenue;
    private string _SubCategory2;
    private string _SubCategory3;


    public string EntityID
    {
        get
        {
            return _EntityID;
        }

        set
        {
            _EntityID = value;
        }
    }

    public string IncidentID
    {
        get
        {
            return _IncidentID;
        }

        set
        {
            _IncidentID = value;
        }
    }

    public string Title
    {
        get
        {
            return _Title;
        }

        set
        {
            _Title = value;
        }
    }

    public string Description
    {
        get
        {
            return _Description;
        }

        set
        {
            _Description = value;
        }
    }

    public int TotalTimeInMinutes
    {
        get
        {
            return _TotalTimeInMinutes;
        }

        set
        {
            _TotalTimeInMinutes = value;
        }
    }

    public string Category
    {
        get
        {
            return _Category;
        }

        set
        {
            _Category = value;
        }
    }

    public string Status
    {
        get
        {
            return _Status;
        }

        set
        {
            _Status = value;
        }
    }

    public string AffectedUser
    {
        get
        {
            return _AffectedUser;
        }

        set
        {
            _AffectedUser = value;
        }
    }

    public string CompanyID
    {
        get
        {
            return _CompanyID;
        }

        set
        {
            _CompanyID = value;
        }
    }

    public string ResolutionCategory
    {
        get
        {
            return _ResolutionCategory;
        }

        set
        {
            _ResolutionCategory = value;
        }
    }

    public string ResolutionDescription
    {
        get
        {
            return _ResolutionDescription;
        }

        set
        {
            _ResolutionDescription = value;
        }
    }

    public string Escalated
    {
        get
        {
            return _Escalated;
        }

        set
        {
            _Escalated = value;
        }
    }

    public string CreatedDate
    {
        get
        {
            return _CreatedDate;
        }

        set
        {
            _CreatedDate = value;
        }
    }

    public string ResolvedDate
    {
        get
        {
            return _ResolvedDate;
        }

        set
        {
            _ResolvedDate = value;
        }
    }

    public string ClosedDate
    {
        get
        {
            return _ClosedDate;
        }

        set
        {
            _ClosedDate = value;
        }
    }

    public string FirstAssignedDate
    {
        get
        {
            return _FirstAssignedDate;
        }

        set
        {
            _FirstAssignedDate = value;
        }
    }

    public string FirstResponseDate
    {
        get
        {
            return _FirstResponseDate;
        }

        set
        {
            _FirstResponseDate = value;
        }
    }

    public string ActualStartDate
    {
        get
        {
            return _ActualStartDate;
        }

        set
        {
            _ActualStartDate = value;
        }
    }

    public string ActualEndDate
    {
        get
        {
            return _ActualEndDate;
        }

        set
        {
            _ActualEndDate = value;
        }
    }

    public string TargetResolutionTime
    {
        get
        {
            return _TargetResolutionTime;
        }

        set
        {
            _TargetResolutionTime = value;
        }
    }

    public int Priority
    {
        get
        {
            return _Priority;
        }

        set
        {
            _Priority = value;
        }
    }

    public string Impact
    {
        get
        {
            return _Impact;
        }

        set
        {
            _Impact = value;
        }
    }

    public string Urgency
    {
        get
        {
            return _Urgency;
        }

        set
        {
            _Urgency = value;
        }
    }

    public string Source
    {
        get
        {
            return _Source;
        }

        set
        {
            _Source = value;
        }
    }

    public string SubCategory
    {
        get
        {
            return _SubCategory;
        }

        set
        {
            _SubCategory = value;
        }
    }

    public string ClientReference
    {
        get
        {
            return _ClientReference;
        }

        set
        {
            _ClientReference = value;
        }
    }

    public bool FixedCostProject
    {
        get
        {
            return _FixedCostProject;
        }

        set
        {
            _FixedCostProject = value;
        }
    }

    public int DailyRate
    {
        get
        {
            return _DailyRate;
        }

        set
        {
            _DailyRate = value;
        }
    }

    public int TotalDaysAllocated
    {
        get
        {
            return _TotalDaysAllocated;
        }

        set
        {
            _TotalDaysAllocated = value;
        }
    }

    public string CreatedBy
    {
        get
        {
            return _CreatedBy;
        }

        set
        {
            _CreatedBy = value;
        }
    }

    public string AssignedTo
    {
        get
        {
            return _AssignedTo;
        }

        set
        {
            _AssignedTo = value;
        }
    }

    public string ResolvedBy
    {
        get
        {
            return _ResolvedBy;
        }

        set
        {
            _ResolvedBy = value;
        }
    }

    public string ClosedBy
    {
        get
        {
            return _ClosedBy;
        }

        set
        {
            _ClosedBy = value;
        }
    }

    public double TotalHoursAllocated
    {
        get
        {
            return _TotalHoursAllocated;
        }

        set
        {
            _TotalHoursAllocated = value;
        }
    }

    public double EstimatedRevenue
    {
        get
        {
            return _EstimatedRevenue;
        }

        set
        {
            _EstimatedRevenue = value;
        }
    }

    public string SubCategory2
    {
        get
        {
            return _SubCategory2;
        }

        set
        {
            _SubCategory2 = value;
        }
    }


    public string SubCategory3
    {
        get
        {
            return _SubCategory3;
        }

        set
        {
            _SubCategory3 = value;
        }
    }
}