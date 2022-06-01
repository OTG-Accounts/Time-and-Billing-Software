using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class TempClientOverview
{
	private string _CompanyName;
    private double _ProjectCost;
    private double _ProjectInvoiced;
    private double _ProjectMargin;
    private double _ProjectMarginPercent;
    private double _MiscCost;
    private double _MiscInvoiced;
    private double _MiscMargin;
    private double _MiscMarginPercent;
    
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

    public double ProjectCost
    {
        get
        {
            return _ProjectCost;
        }

        set
        {
            _ProjectCost = value;
        }
    }

    public double ProjectInvoiced
    {
        get
        {
            return _ProjectInvoiced;
        }

        set
        {
            _ProjectInvoiced = value;
        }
    }

    public double ProjectMargin
    {
        get
        {
            return _ProjectMargin;
        }

        set
        {
            _ProjectMargin = value;
        }
    }

    public double ProjectMarginPercent
    {
        get
        {
            return _ProjectMarginPercent;
        }

        set
        {
            _ProjectMarginPercent = value;
        }
    }

    public double MiscCost
    {
        get
        {
            return _MiscCost;
        }

        set
        {
            _MiscCost = value;
        }
    }

    public double MiscInvoiced
    {
        get
        {
            return _MiscInvoiced;
        }

        set
        {
            _MiscInvoiced = value;
        }
    }

    public double MiscMargin
    {
        get
        {
            return _MiscMargin;
        }

        set
        {
            _MiscMargin = value;
        }
    }

    public double MiscMarginPercent
    {
        get
        {
            return _MiscMarginPercent;
        }

        set
        {
            _MiscMarginPercent = value;
        }
    }
    

}