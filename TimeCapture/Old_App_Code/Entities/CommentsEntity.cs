/// <summary>
/// Summary description for CommentsEntity
/// </summary>
public class CommentsEntity
{
	private int _EntityChangeLogId;
    private string _IncidentEntityID;
    private string _UserID;
    private string _EnteredDate;
    private string _StartTime;
    private int _TimeInMinutes;
    private string _Comment;
    private bool _OnSite;
    private bool _Completed;
    private bool _PeerReview;
    private bool _ManagementReview;
    private bool _Billable;
    private bool _Error;
    private bool _NotToBeInvoiced;
    private bool _Invoiced;
    private string _CompletedDate;
    private string _PeerReviewDate;
    private string _ManagementReviewDate;
    private string _InvoicedDate;
    private bool _AccountsLock;
    private string _AccountsLockDate;
    private bool _Export;
    private string _ExportDate;
    private bool _PurchaseExport;
    private string _PurchaseExportDate;
    private bool _VITRExport;
    private string _VITRExportDate;
    private bool _AHS;

    


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

    public string IncidentEntityID
    {
        get
        {
            return _IncidentEntityID;
        }

        set
        {
             _IncidentEntityID = value;
        }
    }

    public string UserID
    {
        get
        {
            return _UserID;
        }

        set
        {
            _UserID = value;
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
             _EnteredDate= value;
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
           _PeerReview  = value;
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
           _ManagementReview  = value;
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

    public string CompletedDate
    {
        get
        {
            return _CompletedDate;
        }

        set
        {
            _CompletedDate = value;
        }
    }

    public string PeerReviewDate
    {
        get
        {
            return _PeerReviewDate;
        }

        set
        {
            _PeerReviewDate = value;
        }
    }

    public string ManagementReviewDate
    {
        get
        {
            return _ManagementReviewDate;
        }

        set
        {
            _ManagementReviewDate = value;
        }
    }

    public string InvoicedDate
    {
        get
        {
            return _InvoicedDate;
        }

        set
        {
            _InvoicedDate = value;
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

    public string AccountsLockDate
    {
        get
        {
            return _AccountsLockDate;
        }

        set
        {
            _AccountsLockDate = value;
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

    public string ExportDate
    {
        get
        {
            return _ExportDate;
        }

        set
        {
            _ExportDate = value;
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

    public string PurchaseExportDate
    {
        get
        {
            return _PurchaseExportDate;
        }

        set
        {
            _PurchaseExportDate = value;
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

    public string VITRExportDate
    {
        get
        {
            return _VITRExportDate;
        }

        set
        {
            _VITRExportDate = value;
        }
    }
}