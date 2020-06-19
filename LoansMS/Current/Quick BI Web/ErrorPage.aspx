<%@ Page Title="Quick BI" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="ErrorPage.aspx.vb" Inherits="LoansMS.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-danger" style="font-size:medium">

        <div class="panel-heading" style="text-align:center">
            <strong>Error</strong>
        </div>

        <div class="panel-body">
                    
            <div class="row" style="padding:2%">
                <div class="col-md-5 col-md-offset-3">
                    Oops! We seem to have encountered an error. Please check the functionality selected or the data provided. 
                </div>
            </div>
            <div class="row" style="padding:2%">
                <div class="col-md-5 col-md-offset-3">
                    Contact the System Administrator for Support!
                </div>
            </div>
                    
        </div>
            
    </div>

</asp:Content>
