using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;

/// <summary>
/// Summary description for JSON
/// </summary>
public class JSON
{
    public JSON()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string ToJSONString(Object pObject )
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        StringBuilder sb = new StringBuilder();

        js.Serialize(pObject, sb);

        return sb.ToString();
    }

    public string Serialize(Object pObject)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        StringBuilder sb = new StringBuilder();

        js.Serialize(pObject, sb);

        return sb.ToString();
    }

    public T Deserialize<T>(string pstrJSON)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Deserialize<T>(pstrJSON);
    }
}