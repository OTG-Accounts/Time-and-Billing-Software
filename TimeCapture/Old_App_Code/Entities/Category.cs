using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class Category
{
	private double _CategoryID;
    private string _CategoryName;
    private double _ParentCategoryID;
    private int _JobNumber;
    private bool _ClientLinked;
    private string _DisplayName;
    private int _AccountNumber;

    public double CategoryID
    {
        get
        {
            return _CategoryID;
        }

        set
        {
            _CategoryID = value;
        }
    }

    public string CategoryName
    {
        get
        {
            return _CategoryName;
        }

        set
        {
            _CategoryName = value;
        }
    }

    public double ParentCategoryID
    {
        get
        {
            return _ParentCategoryID;
        }

        set
        {
            _ParentCategoryID = value;
        }
    }

    public int JobNumber
    {
        get
        {
            return _JobNumber;
        }

        set
        {
            _JobNumber = value;
        }
    }

    public bool ClientLinked
    {
        get
        {
            return _ClientLinked;
        }

        set
        {
            _ClientLinked = value;
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

    public int AccountNumber
    {
        get
        {
            return _AccountNumber;
        }

        set
        {
            _AccountNumber = value;
        }
    }
}