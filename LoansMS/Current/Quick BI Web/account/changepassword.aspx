<%@ Page Title="Change Password" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="changepassword.aspx.vb" Inherits="LoansMS.changepassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="panel panel-info">

        <div class="panel-heading" style="text-align:center">
            <strong>Change User Password</strong> 
        </div>

        <div class="panel-body">

            <div class="col-md-4 col-md-offset-4">

                <div class="row">
                    <p>Please provide the current and new passwords to change your password.</p>
                </div>

                <div class="row form-group">
                    <div class="col-md-8">
                        <input class="form-control" type="password" name="txtCurrentPassword" id="txtCurrentPassword" runat="server" Placeholder ="Current Password"/>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-8">
                        <input class="form-control" type="password" name="txtNewPassword" id="txtNewPassword" runat="server" Placeholder ="New Password"/>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-8">
                        <input class="form-control" type="password" name="txtConfirmPassword" id="txtConfirmPassword" runat="server" Placeholder ="Confirm Password"/>
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-4">
                        <input class="btn btn-primary" type="button" name="btnChange" id="btnChange" value="Change" runat="server" onserverclick="ChangeClick"/>
                    </div>

                    <div class="col-md-4">
                        <input class="btn btn-danger" type="button" name="btnCancel" id="btnCancel" value="Cancel" runat="server" onserverclick="CancelClick" />
                    </div>

                </div>

            </div>

        </div>
        
    </div>

</asp:Content>
