using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class UserTargetView
{

    private string _DisplayName;
    private string _Category;
    private double _Target;

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

    public string Category
    {
        get
        {
            return _Category;
        }

        set
        {
            _Category = value;
        }
    }

    public double Target
    {
        get
        {
            return _Target;
        }

        set
        {
            _Target = value;
        }
    }
}
