<%@ Page Title="CRB Requests" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="crbrequests.aspx.vb" Inherits="LoansMS.crbrequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-info">

        <div class="panel-heading">
            <strong>CRB Query Search Criteria</strong>
        </div>

        <div class="panel-body">

            <div class="row form-group">

                <div class="col-md-2">
                    <input id="txtNationalID" type="text" class="form-control" name="txtNationalID" value="" placeholder="National ID" runat="server" />
                </div>

                <div class="col-md-2">
                    <input id="txtName" type="text" class="form-control" name="Customer Name" value="" placeholder="Customer Name" runat="server" />
                </div>

                <div class="col-md-2">
                    <input id="txtMobile" type="text" class="form-control" name="Mobile No" value="" placeholder="Mobile No" runat="server" />
                </div>
                
                <div class="col-md-2">
                    <select class="form-control" id="lsClientType" runat="server">
                        <option value=""></option>
                        <option value="PostBank">PostBank</option>
                        <option value="Consumer">Consumer</option>
                    </select>
                </div>

                <div class="col-md-2">
                    <input id="txtQueryDate" type="Date" class="form-control" name="txtQueryDate" value="" placeholder="Query Date" runat="server" />
                </div>

                <div class="col-md-1">
                    <input class="btn btn-primary" type="button" name="btnSearch" id="btnSearch" value="Search" runat="server" onserverclick="getCRB" />
                </div>

                <div class="col-md-1">
                    <input class="btn btn-warning " type="button" name="btnReset" id="btnReset" value="Refresh" runat="server" onserverclick="reset"  />
                </div>

            </div>
            
        </div>

        <div class="panel-heading">
            <strong>Search Results</strong>
        </div>

        <div class="table-responsive">
            
            <asp:GridView ID="gview" runat="server" CssClass="table table-striped"
                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" BackColor="White" BorderColor="#336666" 
                    BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                    <Columns>
                        <asp:BoundField HeaderText="National ID" DataField="NationalID"/>
                        <asp:BoundField HeaderText="Mobile No" DataField="MobileNo"/>
                        <asp:BoundField HeaderText="Name" DataField="Customer"/>
                        <asp:BoundField HeaderText="RequestDate" DataField="RequestDate" DataFormatString="{0:d}"/>
                        <asp:BoundField HeaderText="Response Date" DataField="ResponseDate" DataFormatString="{0:d}"/>
                        <asp:BoundField HeaderText="CRB Check" DataField="CRBQuery"/>
                        <asp:BoundField HeaderText="CRB Response" DataField="CRBAssessment"/>
                        <asp:BoundField HeaderText="NPA Records" DataField="Non-PerformingAccounts" DataFormatString="{0:N0}"/>
                        <asp:BoundField HeaderText="PA Records" DataField="OpenPerformingAccounts" DataFormatString="{0:N0}"/>
                        <asp:BoundField HeaderText="Type" DataField="LoanScheme"/>
                        <asp:BoundField HeaderText="Loan Account" DataField="LoanAccount"/>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <%--<RowStyle BackColor="White" ForeColor="#333333" />--%>
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
            
        </div>
    
    </div>

</asp:Content>
