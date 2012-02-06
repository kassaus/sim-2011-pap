<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayList.aspx.cs" Inherits="Lives.PlayList" %>

<%   
    
    Response.ContentType =  "video/x-ms-asf";

    var id = System.Web.HttpContext.Current.Request["id"];

    if (id == null)
    {
        id = "";
    }
    
    if (System.Web.HttpContext.Current.Session["PlayList" + id] != null)
        Response.Write(System.Web.HttpContext.Current.Session["PlayList" + id].ToString());
    else
        Response.Write("<ASX version = \"3.0\"></ASX>");
%>