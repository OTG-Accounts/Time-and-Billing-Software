using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsersEntity
/// </summary>
public class JobsEntity
{
    private int _JobNumber;    
    private string _JobName;
    private string _Company;
    private string _CompanyID;
    

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

    public string JobName
    {
        get
        {
            return _JobName;
        }

        set
        {
            _JobName = value;
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