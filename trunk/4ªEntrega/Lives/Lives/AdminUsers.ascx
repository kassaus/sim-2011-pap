<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminUsers.ascx.cs" Inherits="Lives.AdminUsers" %>

<div style="text-align: left; margin-left:10px; margin-top:10px; margin-bottom:10px;">
<hr class="hrLinha"/>
    <asp:Image ID="imgAmigo" runat="server" Width="80px" ImageAlign="Top" />
   

&nbsp;
    <asp:LinkButton ID="btnAmigo" runat="server" CssClass="letraSiteMedia"
        Font-Overline="false">label</asp:LinkButton>
        <div style="position:absolute; top:120px; left:100px">
   <asp:Label ID="Label1" runat="server" Text="Ultima publicação:" style="font-weight: 700"></asp:Label>
            &nbsp;
            <asp:Label ID="lblUltimaPublicacao" runat="server" Text="Label"></asp:Label>
   </div>


</div>