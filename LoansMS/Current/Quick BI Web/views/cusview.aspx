<%@ Page Title="Customer Listing" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="cusview.aspx.vb" Inherits="LoansMS.cusview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-info">

        <div class="panel-heading">
            <strong>Client Selection Criteria</strong>
        </div>

        <div class="panel-body">

            <div class="row form-group">

                <div class="col-md-2">
                    <input id="txtClientNo" type="text" class="form-control" name="txtClientNo" value="" placeholder="Base No" runat="server" />
                </div>

                <div class="col-md-2">
                    <input id="txtName" type="text" class="form-control" name="Customer Name" value="" placeholder="Customer Name" runat="server" />
                </div>

                <div class="col-md-2">
                    <input id="txtMobile" type="text" class="form-control" name="Mobile No" value="" placeholder="Mobile No" runat="server" />
                </div>

                <div class="col-md-2">
                    <input id="txtNationalID" type="text" class="form-control" name="txtNationalID" value="" placeholder="National ID" runat="server" />
                </div>

                <div class="col-md-2">
                    <select class="form-control" id="lsClientType" runat="server">
                        <option value=""></option>
                    </select>
                </div>

                <div class="col-md-1">
                    <input class="btn btn-primary" type="button" name="btnSearch" id="btnSearch" value="Search" runat="server" onserverclick="getClients" />
                </div>

                <div class="col-md-1">
                    <input class="btn btn-warning " type="button" name="btnReset" id="btnReset" value="Refresh" runat="server" onserverclick="reset"  />
                </div>

            </div>
            
        </div>

        <div class="panel-heading">
            <strong>Search Results</strong>
        </div>

        <div class="table-responsive" style="max-height:100%; max-width:100%; overflow:scroll">
            
            <asp:GridView ID="gview" runat="server" CssClass="table table-striped table-bordered table-hover"
                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="false" BackColor="White" BorderColor="#336666" 
                    BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                    <Columns>
                        <asp:BoundField HeaderText="Client No" DataField="ClientNo"/>
                        <asp:BoundField HeaderText="Mobile No" DataField="MobileNo"/>
                        <asp:BoundField HeaderText="Name" DataField="Name"/>
                        <asp:BoundField HeaderText="National ID" DataField="NationalID"/>
                        <asp:BoundField HeaderText="Loan Limit" DataField="Limit" DataFormatString="{0:N}"/>
                        <asp:BoundField HeaderText="Status" DataField="Status"/>
                        <asp:BoundField HeaderText="Messaging Status" DataField="Messaging"/>
                        <asp:BoundField HeaderText="Age" DataField="Age" DataFormatString="{0:N0}"/>
                        <asp:BoundField HeaderText="Type" DataField="Grouping"/>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                </asp:GridView>
            
        </div>
    
    </div>

</asp:Content>
