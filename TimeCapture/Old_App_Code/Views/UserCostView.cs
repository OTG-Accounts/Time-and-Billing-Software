using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class UserCostView
{
    private double _CostID;
	private string _DisplayName;
    private string _ValidFrom;
    private double _Cost;
    private string _Group;

    public double CostID
    {
        get
        {
            return _CostID;
        }

        set
        {
            _CostID = value;
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

    public double Cost
    {
        get
        {
            return _Cost;
        }

        set
        {
            _Cost = value;
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

}