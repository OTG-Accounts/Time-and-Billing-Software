using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ProjectRateView
{
    private double _Id;
    private string _IncidentID;
    private string _DisplayName;
    private double _OnsiteRate;
    private double _OffsiteRate;


    public double Id
    {
        get
        {
            return _Id;
        }
        set
        {
            _Id = value;
        }
    }
    public string IncidentID
    {
        get
        {
            return _IncidentID;
        }

        set
        {
            _IncidentID = value;
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

    public double OnsiteRate
    {
        get
        {
            return _OnsiteRate;
        }
        set
        {
            _OnsiteRate = value;
        }
    }

    public double OffsiteRate
    {
        get
        {
            return _OffsiteRate;
        }
        set
        {
            _OffsiteRate = value;
        }
    }

}
