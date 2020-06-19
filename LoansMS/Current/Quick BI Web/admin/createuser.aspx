<%@ Page Title="Register User" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="createuser.aspx.vb" Inherits="LoansMS.createuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manager Users</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-primary">

        <div class="panel-heading" style="text-align:center;">
            System User Management
        </div>

        <div class="panel-body">

            <div class="container" style="padding-top:1%;">

                <div class="col-md-10 col-md-offset-3 form-horizontal">

                    <div class = "row form-group">
                        <div class = "col-md-6">
                            Provide the details of the new user to be created
                        </div>
                    </div>

                    <div class="row form-group">
                         <label class="control-label col-md-1" for="txtLoginID"><strong>User ID:</strong></label>
                         <div class="col-md-2">
                             <input type="text" class="form-control" id="txtLoginID" placeholder="Enter Login ID" runat="server" />
                         </div>
                        <div class="col-md-1">
                            <input class="btn btn-success" type="button" name="btnCheck" id="btnCheck" value="Go" runat="server" onserverclick="CheckUser"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-1" for="txtName"><strong>Name:</strong></label>
                        <div class="col-md-4">
                            <input type="text" class="form-control" id="txtName" placeholder="Enter User Name" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-1" for="txtEMail"><strong>EMail:</strong></label>
                        <div class="col-md-4">
                            <input type="text" class="form-control" id="txtEMail" placeholder="Enter User EMal" runat ="server"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-1" for="lstRoles"><strong>Role:</strong></label>
                        <div class="col-md-4">
                            <select id="lstRoles" runat="server"  class="form-control">
                                <option value="">Select Role</option>
                            </select>
                       </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-1" for="cboStatus"><strong>Status:</strong></label>
                        <div class="col-md-2">
                            <select class="form-control" id="cboStatus" runat="server">
                                <option value="">Select Status</option>
                                <option value="Active">Active</option>
                                <option value="Suspended">Suspended</option>
                            </select>
                       </div>
                    </div>

                    <div class="row">
                       <div class="col-md-1">
                            <input type="button" id="btnNew" value ="New" class="btn btn-primary btn-md" runat="server" onserverclick="btnNew_Click"/>
                       </div>
                        <div class="col-md-1">
                            <input type="button" id="btnEdit" value ="Edit" class="btn btn-md" runat="server" onserverclick="btnEdit_Click"/>
                        </div>
                        <div class="col-md-2">
                            <input type="button" id="btnResetPassword" value="Reset" class="btn btn-outline-danger btn-md" runat="server" onserverclick="btnResetPassword_Click"/>
                        </div>
                        <div class="col-md-1">
                            <input type="button" id="btnCancel" value="Cancel" class="btn btn-danger btn-outline-danger btn-md" runat="server" onserverclick="btnCancel_Click"/>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    
    </div>

</asp:Content>
