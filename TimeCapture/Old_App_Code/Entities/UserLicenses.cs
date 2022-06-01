using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommentsEntity
/// </summary>
public class UserLicensesEntity
{
	private int _ID;
    private string _System;
    private string _Date;
    private string _Username;
    private string _Firstname;
    private string _Lastname;
    private string _Company;
    private bool _UserDisabled;
    private string _LicenseCode;

    public int ID
    {
        get
        {
            return _ID;
        }

        set
        {
            _ID = value;
        }
    }

    public string System
    {
        get
        {
            return _System;
        }

        set
        {
             _System = value;
        }
    }

    public string Date
    {
        get
        {
            return _Date;
        }

        set
        {
            _Date = value;
        }
    }

    public string Username
    {
        get
        {
            return _Username;
        }

        set
        {
             _Username= value;
        }
    }

    public string Firstname
    {
        get
        {
            return _Firstname;
        }

        set
        {
            _Firstname = value;
        }
    }

    
    public string Lastname
    {
        get
        {
            return _Lastname;
        }

        set
        {
            _Lastname = value;
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

    public bool UserDisabled
    {
        get
        {
            return _UserDisabled;
        }

        set
        {
            _UserDisabled = value;
        }
    }


    public string LicenseCode
    {
        get
        {
            return _LicenseCode;
        }

        set
        {
            _LicenseCode = value;
        }
    }

}