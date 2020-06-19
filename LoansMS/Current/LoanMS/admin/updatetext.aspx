<%@ Page Title="Update Settings" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="updatetext.aspx.vb" Inherits="LoansMS.updatetext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        Customer Text Messages
    </title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel-group">

        <div class="panel panel-primary">

            <div class="panel-heading" style="text-align:center">
                <strong>Manage Customer Message Texts</strong>
            </div>
        
            <div class="panel-body">

                <div class="col-md-8">

                    <div class="row form-group">

                        <div class="col-md-3">
                            <select id="slSearchGroup" class="form-control" runat="server" name="slSearchGroup">
                                <option value="">Select Customer Group</option>
                            </select>
                        </div>

                        <div class="col-md-3">
                            <select id="slSearchType" class="form-control" runat="server" name="slSearchType">
                                <option value="">Select Customer Type</option>
                                <option value="PostBank">PostBank</option>
                            </select>
                        </div>

                        <div class="col-md-3">
                            <select id="slSearchStatus" class="form-control" runat="server" name="slSearchStatus">
                                <option value="">Select Status</option>
                                <option value="Active">Active</option>
                                <option value="Disabled">Disabled</option>
                            </select>
                        </div>
                        
                        <div class="col-md-1"></div>

                        <div class="col-md-2">
                            <input class="btn btn-info btn-block" type="button" id="Button1" name="btnSearch" value="Search" runat="server" onserverclick="SearchClick" />
                        </div>

                    </div>

                    <div class="panel panel-info">
                        <div class="panel-heading" style="text-align:center;font-weight:bold">
                            Customer Texts
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gview" Visible="false" EnableViewState="false" runat="server" 
                                    CssClass="table table-bordered table-condensed table-hover" UseAccessibleHeader="true" 
                                    AutoGenerateColumns="False" HeaderStyle-CssClass="table-dark" Height="1" >
                                    <Columns>
                                        <asp:ButtonField HeaderText="Select Message" Text="Select" CommandName="Select"/>
                                        <asp:BoundField HeaderText="Record No" DataField="RecordID" />
                                        <asp:BoundField HeaderText="Customer Group" DataField="CustomerGroup" />
                                        <asp:BoundField HeaderText="Message" DataField="MessageText" />
                                        <asp:BoundField HeaderText="Customer Type" DataField="CustomerType" />
                                        <asp:BoundField HeaderText="Status" DataField="MessageStatus" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="col-md-4 form-horizontal">

                    <div class="row form-group">
                        <div class="col-md-4">
                            <input class="form-control" type="text" id="txtRecordNo" name="txtRecordNo" value="" disabled="disabled" placeholder="Record No" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-8">
                            <input class="form-control" type="text" id="txtCustomerGroup" name="txtCustomerGroup" value="" disabled="disabled" placeholder="Customer Group" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-12">
                            <textarea rows="5" cols="6" class="col-md-6 form-control" type="text" id="txtMessage" name="txtMessage" disabled="disabled" value="" placeholder="Message Text" runat="server"></textarea>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-8">
                            <input class="form-control" type="text" id="txtCustomerType" name="txtCustomerType" value="" disabled="disabled" placeholder="Customer Type" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-6">
                            <select class="form-control" runat="server" disabled="disabled" name="slStatusMsgs" id="slStatusMsgs">
                                <option value="">Select Message Status</option>
                                <option value="Active">Active</option>
                                <option value="Disabled">Disabled</option>
                            </select>
                        </div>
                    </div>

                    <div class="row form-group">
                                                        
                        <div class="btn-block btn-group-justified">

                            <div class="col-md-4" style="padding-top: 1%">
                                <input class="btn btn-success btn-block" type="button" id="btnEdit" name="btnEdit" value="Update" runat="server" onserverclick="UpdateClick" />
                            </div>

                            <div class="col-md-2"></div>

                            <div class="col-md-4" style="padding-top: 1%">
                                <input type="button" class="btn btn-info btn-block btn-warning" id="btnCancel" name="btnCancel" value="Cancel" runat="server" onserverclick="BtnCancel_Click" />
                            </div>

                        </div>
                    
                    </div>

                </div>
                
            </div>

        </div>
    
    </div>

</asp:Content>
