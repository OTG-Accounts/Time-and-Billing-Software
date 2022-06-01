using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyRate
/// </summary>
public class UserRateView
{
    private double _RateID;
    private string _DisplayName;
    private string _ValidFrom;
    private double _DefaultOnsiteRate;
    private double _DefaultOffsiteRate;
    private double _ProjectOnsiteRate;
    private double _ProjectOffsiteRate;
    private double _MiscOnsiteRate;
    private double _MiscOffsiteRate;
    private bool _Override;
    private bool _IsRoundingRecord;
    private string _CompanyID;


    public double RateID
    {
        get
        {
            return _RateID;
        }

        set
        {
            _RateID = value;
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

    public double DefaultOffsiteRate
    {
        get
        {
            return _DefaultOffsiteRate;
        }

        set
        {
            _DefaultOffsiteRate = value;
        }
    }

    public double DefaultOnsiteRate
    {
        get
        {
            return _DefaultOnsiteRate;
        }

        set
        {
            _DefaultOnsiteRate = value;
        }
    }

    public double ProjectOnsiteRate
    {
        get
        {
            return _ProjectOnsiteRate;
        }

        set
        {
            _ProjectOnsiteRate = value;
        }
    }

    public double ProjectOffsiteRate
    {
        get
        {
            return _ProjectOffsiteRate;
        }

        set
        {
            _ProjectOffsiteRate = value;
        }
    }

    public double MiscOnsiteRate
    {
        get
        {
            return _MiscOnsiteRate;
        }

        set
        {
            _MiscOnsiteRate = value;
        }
    }

    public double MiscOffsiteRate
    {
        get
        {
            return _MiscOffsiteRate;
        }

        set
        {
            _MiscOffsiteRate = value;
        }
    }

    public bool Override
    {
        get
        {
            return _Override;
        }

        set
        {
            _Override = value;
        }
    }

    public bool IsRoundingRecord
    {
        get
        {
            return _IsRoundingRecord;
        }

        set
        {
            _IsRoundingRecord = value;
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
}