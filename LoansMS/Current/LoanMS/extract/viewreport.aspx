<%@ Page Title="Report Data" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="viewreport.aspx.vb" Inherits="LoansMS.viewreport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        Report Data
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    <asp:ScriptManager ID="smMain" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-success">
       <rsweb:reportviewer ID="rvMain" runat="server" Width="100%">

        </rsweb:reportviewer>
    </div>
    
</asp:Content>
