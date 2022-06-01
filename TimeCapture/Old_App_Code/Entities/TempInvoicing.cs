using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class TempInvoicing
{
    private string _CompanyID;
	private string _CompanyName;
    private bool _InvoiceFileExist;
    private string _InvoiceList;
    private string _ContactName;
    private string _ContactEmail;


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

    public bool InvoiceFileExist
    {
        get
        {
            return _InvoiceFileExist;
        }

        set
        {
            _InvoiceFileExist = value;
        }
    }

    public string InvoiceList
    {
        get
        {
            return _InvoiceList;
        }

        set
        {
            _InvoiceList = value;
        }
    }

    public string ContactName
    {
        get
        {
            return _ContactName;
        }

        set
        {
            _ContactName = value;
        }
    }

    public string ContactEmail
    {
        get
        {
            return _ContactEmail;
        }

        set
        {
            _ContactEmail = value;
        }
    }
}