<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayList.aspx.cs" Inherits="Lives.PlayList" %>

<%   
    
    Response.ContentType =  "video/x-ms-asf";

   
    
    if (System.Web.HttpContext.Current.Session["PlayList"] != null)
        Response.Write(System.Web.HttpContext.Current.Session["PlayList"].ToString());
    else
        Response.Write("<ASX version = \"3.0\"></ASX>");
%>