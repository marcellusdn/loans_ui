<%@ Page Title="Change Password" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="unlockuser.aspx.vb" Inherits="LoansMS.unlockuser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="panel panel-info">

        <div class="panel-heading" style="text-align:center">
            <strong>Unlock User</strong> 
        </div>

        <div class="panel-body">

            <div class="col-md-4 col-md-offset-4">

                <div class="row">
                    <p>Please provide the login id for the user to be unlocked.</p>
                </div>

                <div class="row form-group">
                    <div class="col-md-4">
                        <input class="form-control" type="text" name="txtLoginID" id="txtLoginID" runat="server" autopostback="true" onblur="CheckUser" onchange="CheckUser" Placeholder ="Login ID"/>
                    </div>
                    <div class="col-md-1">
                        <input class="btn btn-success" type="button" name="btnCheck" id="btnCheck" value="Go" runat="server" onserverclick="CheckUser"/>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-10">
                        <input class="form-control" type="text" disabled="disabled" name="txtUserName" id="txtUserName" runat="server" Placeholder ="User Name"/>
                    </div>
                </div>
                  
                <div class="row">

                    <div class="col-md-4">
                        <input class="btn btn-primary" type="button" name="btnUnlock" id="btnUnlock" value="Unlock" runat="server" onserverclick="ChangeClick"/>
                    </div>

                    <div class="col-md-4">
                        <input class="btn btn-warning" type="button" name="btnCancel" id="btnCancel" value="Cancel" runat="server" onserverclick="CancelClick" />
                    </div>

                </div>

            </div>

        </div>
        
    </div>

</asp:Content>
