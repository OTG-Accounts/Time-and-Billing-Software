using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyEntity
/// </summary>
public class ConfigurationEntity
{
	private string _ParameterName;
    private string _ParameterValue;

    public string ParameterName
    {
        get
        {
            return _ParameterName;
        }

        set
        {
            _ParameterName = value;
        }
    }

    public string ParameterValue
    {
        get
        {
            return _ParameterValue;
        }

        set
        {
            _ParameterValue = value;
        }
    }

    

}