<%@ Page Title="Register Roles" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="createrole.aspx.vb" Inherits="LoansMS.createrole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        Manage User Roles
    </title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-primary">

        <div class="panel-heading" style="text-align:center;">
            System Role Management
        </div>

        <div class="panel-body">

            <div class="container" style="padding-top:1%;">

                <div class="col-md-6 col-md-offset-3 form-horizontal">

                    <div class="row form-group">
                        <label class="control-label col-md-1 col-offset-3" for="txtCode" runat="server"><strong>Code:</strong></label>
                        <div class="col-md-4">
                            <input type="text" class="form-control" id="txtCode" placeholder="Enter Role Code" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-1" for="txtName" runat="server"><strong>Name:</strong></label>
                        <div class="col-md-6">
                            <input type="text" class="form-control" id="txtName" placeholder="Enter Role Description/Name" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-1" for="lstRoles" runat="server"><strong>Status:</strong></label>
                        <div class="col-md-4">
                            <select id="lstRoles" class="form-control" runat="server">
                                <option value="">Select Status</option>
                                <option value="Active">Active</option>
                                <option value="Suspended">Suspended</option>
                            </select>
                        </div>
                    </div>

                    <div class="row btn-block btn-group-justified">
                       <div class="col-md-2">
                           <input type="button" id="btnNew" class="btn btn-primary btn-md" runat="server" title="New" value="New" onserverclick="SaveOnClick"/>
                       </div>
                       <div class="col-md-2">
                           <input type="button" id="btnEdit" class="btn btn-md" runat="server" value="Edit" onserverclick="EditOnClick"/>
                       </div>
                       <div class="col-md-2">
                           <input type="button" id="btnCancel" class="btn btn-danger btn-outline-danger btn-md" runat="server" value="Cancel" onserverclick="CancelOnClick" />
                       </div>
                   </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
