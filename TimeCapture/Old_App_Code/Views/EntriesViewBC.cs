/// <summary>
/// Summary description for EntriesView
/// </summary>
public class EntriesViewBC
{
    private int _EntityChangeLogId;
    private string _EnteredBy;
    private string _EnteredDate;
    private string _StartTime;
    private int _TimeInMinutes;
    private string _Comment;
    private bool _OnSite;
    private bool _Completed;
    private bool _Billable;
    private string _IncidentID;
    private string _Title;
    private bool _ManagementReview;
    private bool _PeerReview;
    private string _Category;
    private string _Company;
    private string _SubCategory;
    private bool _Error;
    private bool _NotToBeInvoiced;
    private bool _Invoiced;
    private bool _AccountsLock;
    private bool _Export;
    private bool _PurchaseExport;
    private bool _VITRExport;
    private bool _AHS;
    private string _CompanyID;
    private string _EntityID;
    private string _ClientReference;
    private string _Group;
    private string _BCCompanyID;


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

    public int EntityChangeLogId
    {
        get
        {
            return _EntityChangeLogId;
        }

        set
        {
            _EntityChangeLogId = value;
        }
    }

    public string EnteredBy
    {
        get
        {
            return _EnteredBy;
        }

        set
        {
            _EnteredBy = value;
        }
    }

    public string EnteredDate
    {
        get
        {
            return _EnteredDate;
        }

        set
        {
            _EnteredDate = value;
        }
    }

    public string StartTime
    {
        get
        {
            return _StartTime;
        }

        set
        {
            _StartTime = value;
        }
    }

    public int TimeInMinutes
    {
        get
        {
            return _TimeInMinutes;
        }

        set
        {
            _TimeInMinutes = value;
        }
    }

    
    public string Comment
    {
        get
        {
            return _Comment;
        }

        set
        {
            _Comment = value;
        }
    }

    public string Company
    {
        get
        {
            return _Company;
        }

        set
        {
            _Company = value;
        }
    }

    public bool ManagementReview
    {
        get
        {
            return _ManagementReview;
        }

        set
        {
            _ManagementReview = value;
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

    public bool OnSite
    {
        get
        {
            return _OnSite;
        }

        set
        {
            _OnSite = value;
        }
    }

    public bool Completed
    {
        get
        {
            return _Completed;
        }

        set
        {
            _Completed = value;
        }
    }

    public bool PeerReview
    {
        get
        {
            return _PeerReview;
        }

        set
        {
            _PeerReview = value;
        }
    }
    
    public bool Billable
    {
        get
        {
            return _Billable;
        }

        set
        {
            _Billable = value;
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

    public bool Error
    {
        get
        {
            return _Error;
        }

        set
        {
            _Error = value;
        }
    }

    public bool NotToBeInvoiced
    {
        get
        {
            return _NotToBeInvoiced;
        }

        set
        {
            _NotToBeInvoiced = value;
        }
    }

    public bool Invoiced
    {
        get
        {
            return _Invoiced;
        }

        set
        {
            _Invoiced = value;
        }
    }

    public bool AccountsLock
    {
        get
        {
            return _AccountsLock;
        }

        set
        {
            _AccountsLock = value;
        }
    }

    public bool Export
    {
        get
        {
            return _Export;
        }

        set
        {
            _Export = value;
        }
    }


    public bool PurchaseExport
    {
        get
        {
            return _PurchaseExport;
        }

        set
        {
            _PurchaseExport = value;
        }
    }

    public bool VITRExport
    {
        get
        {
            return _VITRExport;
        }

        set
        {
            _VITRExport = value;
        }
    }

    public bool AHS
    {
        get
        {
            return _AHS;
        }

        set
        {
            _AHS = value;
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

    public string Group
    {
        get
        {
            return _Group;
        }

        set
        {
            _Group = value;
        }
    }

    public string BCCompanyID
    {
        get
        {
            return _BCCompanyID;
        }

        set
        {
            _BCCompanyID = value;
        }
    }
}