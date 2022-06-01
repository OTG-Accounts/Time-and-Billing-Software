using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class TempWorkedHours
{
    private string _Name;
    private string _HoursWorked;
    private double _DaysWorked;
    private double _DaysRequired;
    private string _DailyAverage;
    private string _HoursWorkedWeekend;
    private int _OutstandingPeerEntries;
    private string _TotalHoursWorked;

    public string Name
    {
        get
        {
            return _Name;
        }

        set
        {
            _Name = value;
        }
    }

    public string HoursWorked
    {
        get
        {
            return _HoursWorked;
        }

        set
        {
            _HoursWorked = value;
        }
    }

    public string TotalHoursWorked
    {
        get
        {
            return _TotalHoursWorked;
        }

        set
        {
            _TotalHoursWorked = value;
        }
    }

    public double DaysWorked
    {
        get
        {
            return _DaysWorked;
        }

        set
        {
            _DaysWorked = value;
        }
    }

    public double DaysRequired
    {
        get
        {
            return _DaysRequired;
        }

        set
        {
            _DaysRequired = value;
        }
    }

    public string DailyAverage
    {
        get
        {
            return _DailyAverage;
        }

        set
        {
            _DailyAverage = value;
        }
    }

    public string HoursWorkedWeekend
    {
        get
        {
            return _HoursWorkedWeekend;
        }

        set
        {
            _HoursWorkedWeekend = value;
        }
    }

    public int OutstandingPeerEntries
    {
        get
        {
            return _OutstandingPeerEntries;
        }

        set
        {
            _OutstandingPeerEntries = value;
        }
    }
    
}