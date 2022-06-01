using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsersEntity
/// </summary>
public class UsersEntity
{
    private string _UserID;    
    private string _Username;
    private string _UPN;
    private string _Firstname;
    private string _Lastname;
    private string _Domain;
    private string _DisplayName;
    private string _Email;
    private string _password;
    private string _Group;
    private string _PeerReview;
    private bool _Active;
    

    public string password
    {
        get
        {
            return _password;
        }

        set
        {
            _password = value;
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
            _Username = value;
        }
    }

    public string UPN
    {
        get
        {
            return _UPN;
        }

        set
        {
            _UPN = value;
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

    public string Domain
    {
        get
        {
            return _Domain;
        }

        set
        {
            _Domain = value;
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

    public string Email
    {
        get
        {
            return _Email;
        }

        set
        {
            _Email = value;
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

    public string Group
    {
        get
        {
            return _Group;
        }

        set
        {
            _Group = value;
        }
    }

    public string PeerReview
    {
        get
        {
            return _PeerReview;
        }

        set
        {
            _PeerReview = value;
        }
    }

    public bool Active
    {
        get
        {
            return _Active;
        }

        set
        {
            _Active = value;
        }
    }
    
}