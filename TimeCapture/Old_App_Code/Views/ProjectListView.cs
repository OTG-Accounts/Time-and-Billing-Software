using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ProjectListView
{
    private string _IncidentID;
    private string _Title;
    private string _Category;
    private string _Status;
    private string _CompanyName;
    private string _SubCategory;
    private string _DisplayName;
    private double _TotalHoursAllocated;
    private double _EstimatedRevenue;
    private string _ClientReference;


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

    public string CompanyName
    {
        get
        {
            return _CompanyName;
        }

        set
        {
            _CompanyName = value;
        }
    }

    public string DisplayName
    {
        get
        {
            return _DisplayName;
        }
        set
        {
            _DisplayName = value;
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
}
