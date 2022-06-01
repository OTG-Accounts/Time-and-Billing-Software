using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EntriesView
/// </summary>
public class EntriesViewForCosts
{
    private string _EnteredBy;
    private string _EnteredYearMonth;
    private int _TimeInMinutes;
    private bool _OnSite;
    private string _Category;
    
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

    public string EnteredYearMonth
    {
        get
        {
            return _EnteredYearMonth;
        }

        set
        {
            _EnteredYearMonth = value;
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

    
}