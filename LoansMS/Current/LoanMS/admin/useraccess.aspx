<%@ Page Title="User Access" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="useraccess.aspx.vb" Inherits="LoansMS.useraccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manager User Access</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel-group">
        
        <div class="panel panel-info">
            
            <div class="panel-heading" style="text-align:center">
                User Access Management
            </div>

            <div class="panel-body">

                <div class="col-md-3">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align:center">
                            User Roles
                        </div>
                        <div class="panel-body">
                            <asp:TreeView id="tvRoles" ShowCheckBoxes="All"  OnSelectedNodeChanged="BtnLoadAccess" runat="server"></asp:TreeView>
                        </div>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align:center">
                            Reports & Menus
                        </div>
                        <div class="panel-body" style="height:260px; overflow-y:scroll">
                            <asp:TreeView ID="tvReports" runat="server" BorderStyle="None" autopostback="true"  ShowCheckBoxes="All" >
                                <Nodes>
                                    <asp:TreeNode SelectAction="SelectExpand"></asp:TreeNode>
                                </Nodes>
                            </asp:TreeView>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    
                    <div class="panel panel-primary" >
                        <div class="panel-heading" style="text-align:center">
                            Access
                        </div>
                        <div class="panel-body" style="height:260px; overflow-y:scroll">
                            <div class="row form-group" style="padding-left:5%">
                                <div class="col-md-11">
                                <input class="form-control" type="text" name="txtActiveRole" id="txtActiveRole" runat="server" disabled="disabled" value="" Placeholder=""/>
                                </div>
                           </div>
                            <div class="table-responsive">
                                    <asp:GridView ID="gview" Visible="false" EnableViewState="false" runat="server" 
                                        CssClass="table table-bordered table-condensed table-hover" UseAccessibleHeader="true" 
                                        AutoGenerateColumns="False" HeaderStyle-CssClass="" Height="1" >
                                        <Columns>
                                            <asp:BoundField HeaderText="Function" DataField="Description" />
                                            <asp:BoundField HeaderText="ControlName" DataField="Control" Visible="false" />
                                            <asp:BoundField HeaderText="Type" DataField="Type" />
                                            <asp:BoundField HeaderText="Granted" DataField="Access" />
                                        </Columns>
                                    </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row btn-group btn-group-justified" style="padding-top:2%">
                    <div class="col-md-1"></div>
                    <div class="col-md-1">
                        <input type="button" id="btnUpdate" value ="Update" class="btn btn-primary btn-md" runat="server" onserverclick="BtnUpdate_Click"/>
                    </div>
                    <div class="col-md-1"></div>
                    <div class="col-md-1">
                        <input type="button" id="btnCancel" value="Cancel" class="btn btn-danger btn-outline-danger btn-md" runat="server" onserverclick="btnCancel_Click"/>
                    </div>

                </div>

            </div>

        </div>
        
    </div>
    
</asp:Content>
