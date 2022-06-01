using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;

/// <summary>
/// Summary description for DataAccessLayer
/// </summary>
public class DataAccessLayer
{
    private static DataAccessLayer _instance;

    public static DataAccessLayer Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DataAccessLayer();

            return _instance;
        }
    }

	private DataAccessLayer()
	{

	}

    public string ExecuteSimpleQuery(string query)
    {
        string retVal = "";

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetConnectionString"].ConnectionString))
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(query, cnn);
            retVal = cmd.ExecuteScalar().ToString();
        }

        return retVal;
    }

    public object ExecuteQuery(string query, SqlParameter[] parameters)
    {
        object retVal = null;

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetConnectionString"].ConnectionString))
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.AddRange(parameters);
            retVal = cmd.ExecuteScalar();
        }

        return retVal;
    }

    public List<T> GetEntities<T>(string query)
    {
        List<T> entities = new List<T>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetConnectionString"].ConnectionString))
        {
            cnn.Open();

            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            while (reader.Read())
            {
                T entity = Activator.CreateInstance<T>();

                foreach (PropertyInfo property in properties)
                {
                    int index = reader.GetOrdinal(property.Name);

                    if (index != -1)
                    {
                        object value = ConvertValue(reader.GetValue(index), property.PropertyType);
                        property.SetValue(entity, value, null);
                    }
                    else
                    {
                        throw new Exception(string.Format("Fieldname {0} not found!", property.Name));
                    }
                }

                entities.Add(entity);
            }
        }

        return entities;
    }

    public List<T> GetEntitiesSM<T>(string query)
    {
        List<T> entities = new List<T>();

        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceManagerConnectionString"].ConnectionString))
        {
            cnn.Open();

            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            while (reader.Read())
            {
                T entity = Activator.CreateInstance<T>();

                foreach (PropertyInfo property in properties)
                {
                    int index = reader.GetOrdinal(property.Name);

                    if (index != -1)
                    {
                        object value = ConvertValue(reader.GetValue(index), property.PropertyType);
                        property.SetValue(entity, value, null);
                    }
                    else
                    {
                        throw new Exception(string.Format("Fieldname {0} not found!", property.Name));
                    }
                }

                entities.Add(entity);
            }
        }

        return entities;
    }

    private object ConvertValue(object value, Type type)
    {
        object retVal = null;

        if (value != null)
        {
            if (type == typeof(string))
            {
                retVal = Convert.ToString(value);
            }
            else if (type == typeof(int))
            {
                retVal = Convert.ToInt32(value);
            }
            else if (type == typeof(double))
            {
                retVal = Convert.ToDouble(value);
            }
            else if (type == typeof(bool))
            {
                retVal = Convert.ToBoolean(value);
            }
            else if (type == typeof(DateTime))
            {
                retVal = Convert.ToDateTime(value);
            }
        }

        return retVal;
    }


    public  DataTable GetDataTable(string query)
    {
        var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetConnectionString"].ConnectionString);
        var cmd = new SqlCommand(query, conn);
        conn.Open();
        var dr = cmd.ExecuteReader();
        var dt = new DataTable();
        dt.Load(dr);
        conn.Close();
        conn.Dispose();
        return dt;
    }

    public  DataTable GetDataTable(string query, SqlParameter[] parameters)
    {
        var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetConnectionString"].ConnectionString);
        var cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddRange(parameters);
        conn.Open();
        var dr = cmd.ExecuteReader();
        var dt = new DataTable();
        dt.Load(dr);
        conn.Close();
        conn.Dispose();
        return dt;
    }
}