using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EntriesView
/// </summary>
public class ExportView
{
    private string _EnteredBy;
    private string _EnteredDate;
    private int _TimeInMinutes;
    private string _Comment;
    private bool _OnSite;
    private bool _Billable;
    private string _IncidentID;
    private string _Title;
    private string _Category;
    private string _Company;
    private string _SubCategory;
    private bool _AHS;


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


}