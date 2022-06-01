using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class UserCostEntity
{
	private int _CostID;
    private string _UserID;
    private string _ValidFrom;
    private double _Cost;

    public int CostID
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
    

}