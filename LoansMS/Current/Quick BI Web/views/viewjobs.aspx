<%@ Page Title="Scheduled Jobs" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="viewjobs.aspx.vb" Inherits="LoansMS.viewjobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-info">

        <div class="panel-heading">
            <strong>Scheduled Jobs Selection Criteria</strong>
        </div>

        <div class="panel-body">

            <div class="row form-group">

                <div class="col-md-2">
                    <select class="form-control" name="lsResponse" id="slUser" runat="server">
                        <option value=""></option>
                    </select>
                </div>
                <div class="col-md-2">
                    <select class="form-control" name="lsResponse" id="slDepartment" runat="server">
                        <option value=""></option>
                    </select>
                </div>
                <div class="col-md-2">
                    <select class="form-control" name="lsResponse" id="slMessageType" runat="server">
                        <option value=""></option>
                    </select>
                </div>
                <div class="col-md-2">
                    <input id="txtStartDate" type="date" class="form-control" name="Start Date" value="" placeholder="Start Date" runat="server" />
                </div>
                <div class="col-md-2">
                    <input id="txtEndDate" type="date" class="form-control" name="Last Date" value="" placeholder="End Date" runat="server" />
                </div>
                <div class="col-md-1">
                    <input id="btnSearch" type="button" class="btn btn-success btn-md" value="Search" runat="server" onserverclick="SearchClick" />
                </div>
                <div class="col-md-1">
                    <input id="btnCancel" type="button" class="btn btn-danger btn-md" value="Cancel" runat="server" onserverclick="CancelClick" />
                </div>

            </div>

        </div>

        <div class="panel-heading">
            <strong>Search Results</strong>
        </div>

        <div class="panel-body">
            <div class="container" style="max-height:240px; max-width:100%; overflow-y:scroll; padding:0; margin:0;">
                <div class="table-responsive">
                <asp:GridView ID="gview" runat="server" Height="185px" Width="1011px" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                    BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                    <Columns>
                        <asp:BoundField HeaderText="Job No" DataField="recid" />
                        <asp:BoundField HeaderText="Job Date" DataField="jdate" />
                        <asp:BoundField HeaderText="Time" DataField ="jtime" />
                        <asp:BoundField HeaderText="Department" DataField="jdep" />
                        <asp:BoundField HeaderText="Type" DataField="jtype" />
                        <asp:BoundField HeaderText="Status" DataField="jstatus" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                </div>
            </div>
        </div>
    
    </div>

</asp:Content>
