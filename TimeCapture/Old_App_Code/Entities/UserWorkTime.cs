using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class UserWorkTimeEntity
{
	private int _WorkTimeID;
    private string _UserID;
    private string _ValidFrom;
    private int _MonWorkMinutes;
    private int _TueWorkMinutes;
    private int _WedWorkMinutes;
    private int _ThuWorkMinutes;
    private int _FriWorkMinutes;
    private int _SatWorkMinutes;
    private int _SunWorkMinutes;


    public int WorkTimeID
    {
        get
        {
            return _WorkTimeID;
        }

        set
        {
            _WorkTimeID = value;
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

    public string ValidFrom
    {
        get
        {
            return _ValidFrom;
        }

        set
        {
            _ValidFrom = value;
        }
    }

    public int MonWorkMinutes
    {
        get
        {
            return _MonWorkMinutes;
        }

        set
        {
            _MonWorkMinutes = value;
        }
    }

    public int TueWorkMinutes
    {
        get
        {
            return _TueWorkMinutes;
        }

        set
        {
            _TueWorkMinutes = value;
        }
    }

    public int WedWorkMinutes
    {
        get
        {
            return _WedWorkMinutes;
        }

        set
        {
            _WedWorkMinutes = value;
        }
    }

    public int ThuWorkMinutes
    {
        get
        {
            return _ThuWorkMinutes;
        }

        set
        {
            _ThuWorkMinutes = value;
        }
    }

    public int FriWorkMinutes
    {
        get
        {
            return _FriWorkMinutes;
        }

        set
        {
            _FriWorkMinutes = value;
        }
    }

    public int SatWorkMinutes
    {
        get
        {
            return _SatWorkMinutes;
        }

        set
        {
            _SatWorkMinutes = value;
        }
    }

    public int SunWorkMinutes
    {
        get
        {
            return _SunWorkMinutes;
        }

        set
        {
            _SunWorkMinutes = value;
        }
    }
}