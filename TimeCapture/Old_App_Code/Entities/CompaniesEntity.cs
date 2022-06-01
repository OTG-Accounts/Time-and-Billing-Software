using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class CompaniesEntity
{
	private string _CompanyID;
    private string _CompanyName;
    private string _TranslateTo;

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

    public string CompanyName
    {
        get
        {
            return _CompanyName;
        }

        set
        {
            _CompanyName = value;
        }
    }

    public string TranslateTo
    {
        get
        {
            return _TranslateTo;
        }

        set
        {
            _TranslateTo = value;
        }
    }


}