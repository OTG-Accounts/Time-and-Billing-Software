using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EntriesView
/// </summary>
public class WorkedHoursView
{
    private string _EnteredBy;
    private string _EnteredDate;
    private int _TotalTime;
    
    
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

    
    public int TotalTime
    {
        get
        {
            return _TotalTime;
        }

        set
        {
            _TotalTime = value;
        }
    }

    
 
}