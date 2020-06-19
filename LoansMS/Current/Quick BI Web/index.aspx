<%@ Page Title="Quick BI" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="index.aspx.vb" Inherits="LoansMS.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <%--<div class="container" style="width:inherit">--%>

            <div class="panel panel-primary" style="font-size:medium">

                <div class="panel-heading">
                    <strong>Welcome to Quick BI</strong>
                </div>

                <div class="panel-body">
                    
                    <div class="row">
                        <p></p>
                    </div>

                    <div class="row" style="padding:2%">
                        This is a web application designed to facilitate sending of bulk text messages via Short Messaging Services <strong>(SMS)</strong> to clients. 
                        It rides on the existing services provided by the PRSP, Africa's Talking. The application also has the capability of loading airtime 
                        through the PRSP to customer mobile lines. These functionalities are available under the <strong><i>Services Menus</i></strong>.
                    </div>
                    
                    <div class="row">
                        <p></p>
                    </div>
                    
                    <div class="row" style="padding:2%">
                        The application also has administrative features that allow for addition of users who can access the application and initiate the granted functionality.
                        These functionalities are available under the <strong><i>System Menus</i></strong>.
                    </div>
                    
                    <div class="row">
                        <p></p>
                    </div>

                    <div class="row" style="padding:2%">
                        The application also has provides for extraction and generation of reports based on customised system queries.
                        These functionalities are available under the <strong><i>Report & View Menus</i></strong>.
                    </div>

                </div>
            
            </div>

    <%--</div>--%>

</asp:Content>
