<%@ Page Title="Manage Clients" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="clients.aspx.vb" Inherits="LoansMS.clients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="container-fluid" style="padding: 0; margin: 0;">

        <div class="panel panel-primary" style="padding: 0; margin: 0;">

            <div class="panel-heading">
                <strong>Update/Create Client</strong>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="control-label col-md-2" value="Mobile Num" id="lbMobileNo" for="txtMobileNo" runat="server">Mobile No:</label>
                    <div class="col-md-2">
                        <input class="form-control" type="text" id="txtMobileNo" name="txtMobileNo" placeholder="Mobile No" runat="server" value="" />
                    </div>
                    <div class="col-md-1"></div>
                    <div class="col-md-1">
                        <input class="btn btn-default btn-success" type="button" name="btnGet" id="btnGet" value="GET" runat="server" onserverclick="GetClientClick" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="control-label col-md-2" value="Base No" id="lbBaseNo" for="txtBaseNo" runat="server">Base No:</label>
                    <div class="col-md-2">
                        <input class="form-control" type="text" id="txtClientNo" name="txtBaseNo" placeholder="Base No" runat="server" value="" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="control-label col-md-2" value="Customer Name" id="lbCustomerName" for="txtCustomerName" runat="server">Customer Name:</label>
                    <div class="col-md-4">
                        <input class="form-control" type="text" id="txtCustomerName" name="txtCustomerName" placeholder="Customer Name" runat="server" value="" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="control-label col-md-2" value="Customer Limit" id="lbLimit" for="txtLimit" runat="server">Customer Limit:</label>
                    <div class="col-md-2">
                        <input class="form-control" type="text" id="txtLimit" name="txtLimit" placeholder="Customer Limit" runat="server" value="" />
                    </div>
                    <label class="control-label col-md-2" value="Mobile No" id="lbMobileNum" for="txtMobileNum" runat="server">Mobile No:</label>
                    <div class="col-md-2">
                        <input class="form-control" type="text" id="txtMobileNum" name="txtMobileNum" placeholder="Mobile No" runat="server" value="" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="control-label col-md-2" value="National ID" id="lbNationalID" for="txtCustomerID" runat="server">National ID:</label>
                    <div class="col-md-2">
                        <input class="form-control" type="text" id="txtCustomerID" name="txtCustomerID" placeholder="National ID" runat="server" value="" />
                    </div>
                    <label class="control-label col-md-2" value="Date of Birth" id="lbDOB" for="txtDOB" runat="server">Date of Birth:</label>
                    <div class="col-md-2">
                        <input class="form-control" type="text" id="txtDOB" name="txtDOB" placeholder="Date of Birth" runat="server" value="" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="control-label col-md-2" value="CustomerType" id="lbCustomerType" for="lsCustomerType" runat="server">Type:</label>
                    <div class="col-md-2">
                        <select class="form-control" id="lsCustomerType" name="lsCustomerType" runat="server">
                        <option value="PostBank">PostBank</option>
                        <option value="Consumer">Consumer</option>
                    </select>
                    </div>
                    <label class="control-label col-md-2" value="Account No" id="lbAccountNo" for="txtAccountNo" runat="server">Account No:</label>
                    <div class="col-md-2">
                        <input class="form-control" type="text" id="txtAccountNo" name="txtAccountNo" placeholder="Account No" runat="server" value="" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="control-label col-md-2" value="ActivateMessages" id="lbActivateMessage" for="txtCustomerID" runat="server">Activate:</label>
                    <div class="col-md-1">
                        <input class="form-control" type="checkbox" id="chkActivateMessage" name="chkActivateMessage" runat="server" value="" />
                    </div>
                </div>

                <div>
                    <div class="row">
                        <div class="col-md-2">
                            <input class="btn btn-default btn-primary btn-block" type="button" name="btnNew" id="btnNew" value="New" runat="server" onserverclick="RecordClick" />
                        </div>
                        <div class="col-md-2 col-offset-md-2">
                            <input class="btn btn-info btn-block" type="button" name="btnEdit" id="btnEdit" value="Edit" runat="server" />
                        </div>
                        <div class="col-md-2 col-offset-md-2">
                            <input class="btn btn-warning btn-block" type="button" name="btnDeactivate" id="btnDeactivate" value="Deactivate" runat="server" onserverclick="DeactivateClick" />
                        </div>
                        <div class="col-md-2 col-offset-md-2">
                            <input class="btn btn-danger btn-block" type="button" name="btnCancel" id="btnCancel" value="Reset" runat="server" onserverclick="ResetClick" />
                        </div>
                    </div>
                </div>

            </div>

        </div>

    </div>
        
</asp:Content>
