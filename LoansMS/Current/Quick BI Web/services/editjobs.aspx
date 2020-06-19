<%@ Page Title="Update Scheduled Jobs" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="editjobs.aspx.vb" Inherits="LoansMS.editjobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel-group">

        <div class="panel panel-primary">

            <div class="panel-heading" style="text-align:center">
                <strong>Manage Schedule Jobs</strong>
            </div>
        
            <div class="panel-body">

                <div class="col-md-8">
                    <div class="panel panel-info">
                        <div class="panel-heading" style="text-align:center;font-weight:bold">
                            Scheduled Jobs
                        </div>
                        <div class="panel-body" style="min-height:150px; max-height:250px; overflow-y:scroll">
                            <div class="table-responsive">
                                <asp:GridView ID="gview" Visible="false" EnableViewState="false" runat="server" 
                                    CssClass="table table-bordered table-condensed table-hover" UseAccessibleHeader="true" 
                                    AutoGenerateColumns="False" HeaderStyle-CssClass="table-dark" Height="1" >
                                    <Columns>
                                        <asp:ButtonField HeaderText="Select Job" Text="Select" CommandName="Select"/>
                                        <asp:BoundField HeaderText="Job No" DataField="jobno" />
                                        <asp:BoundField HeaderText="Records" DataField="records" />
                                        <asp:BoundField HeaderText="Job Date" DataField="jobdate" />
                                        <asp:BoundField HeaderText="Time" DataField="jobtime" />
                                        <asp:BoundField HeaderText="User" DataField="logger" />
                                        <asp:BoundField HeaderText="Status" DataField="jobstatus" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 form-horizontal">

                    <div class="row form-group">
                        <label id="lbJobNo" class="control-label col-md-4" style="padding-top:0.5%;" for="txtJobNo" runat="server">Job No:</label>
                        <div class="col-md-6">
                            <input class="form-control" type="text" id="txtJobNo" name="txtJobNo" value="" placeholder="Job No" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label id="lbTime" class="control-label col-md-4" style="padding-top:0.5%;" for="txtTime" runat="server">Scheduled Time:</label>
                        <div class="col-md-6">
                            <input class="form-control" type="text" id="txtTime" name="txtTime" value="" placeholder="Schedule Time" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label id="lbDate" class="col-md-4 control-label" style="padding-top:0.5%;" for="txtDate" runat="server">Scheduled Date:</label>
                        <div class="col-md-6">
                            <input class="col-md-6 form-control" type="Date" id="txtDate" name="txtDate" value="" placeholder="Schedule Date" runat="server" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label id="lbDept" class="col-md-4 control-label" style="padding-top:0.5%;" for="lsDept" runat="server">Department:</label>
                        <div class="col-md-6">
                            <select class="form-control" runat="server" id="lsDept">
                                <option value=""></option>
                            </select>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label id="lbMsgs" class="col-md-4 control-label" style="padding-top:0.5%;" for="lsMsgs" runat="server">Messages:</label>
                        <div class="col-md-6">
                            <select class="form-control" runat="server" id="lsMsgs">
                                <option value=""></option>
                            </select>
                        </div>
                    </div>

                    <div class="row form-group">
                                                        
                        <div class="btn-block btn-group-justified">

                            <div class="col-md-2" style="padding-top: 1%">
                                <input class="btn btn-success" type="button" id="btnEdit" name="btnEdit" value="Update" runat="server" onserverclick="UpdateClick" />
                            </div>

                            <div class="col-md-2"></div>

                            <div class="col-md-2" style="padding-top: 1%">
                                <input class="btn btn-danger" type="button" id="btnDelete" name="btnDelete" value="Delete" runat="server" onserverclick="DeleteClick" />
                            </div>

                            <div class="col-md-2"></div>

                            <div class="col-md-2" style="padding-top: 1%">
                                <input type="button" class="btn btn-info" id="btnCancel" name="btnCancel" value="Cancel" runat="server" onserverclick="BtnCancel_Click" />
                            </div>

                        </div>
                    
                    </div>

                </div>
                
            </div>

        </div>
    
    </div>

</asp:Content>
