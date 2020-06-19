<%@ Page Title="Mobile Requests" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="mobrequests.aspx.vb" Inherits="LoansMS.mobrequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-info" style="max-height:400px">

        <div class="panel-heading">
            <strong>Mobile Loan Requests Search Criteria</strong>
        </div>

        <div class="panel-body">

            <div class="row form-group" style="padding:0; margin:0">

                <div class="col-md-2">
                    <input id="txtMobile" type="text" class="form-control" name="Mobile No" value="" placeholder="Mobile No" runat="server" />
                </div>
                
                <div class="col-md-2"></div>

                <div class="col-md-2">
                    <input id="txtStartDate" type="Date" class="form-control" name="txtStartDate" value="" placeholder="Start Date" runat="server" />
                </div>

                <div class="col-md-2">
                    <input id="txtEndDate" type="Date" class="form-control" name="txtEndDate" value="" placeholder="End Date" runat="server" />
                </div>

                <div class="col-md-1">
                    <input class="btn btn-primary" type="button" name="btnSearch" id="btnSearch" value="Search" runat="server" onserverclick="GetMobile" />
                </div>

                <div class="col-md-1">
                    <input class="btn btn-warning " type="button" name="btnReset" id="btnReset" value="Refresh" runat="server" onserverclick="Reset"  />
                </div>

            </div>
            
        </div>

        <div class="panel-heading">
            <strong>Search Results</strong>
        </div>
                
        <div class="panel-body" >
            <div class="container" style="max-height:240px; max-width:100%; overflow-y:scroll; padding:0; margin:0;">
                <div class="table-responsive">
                    <asp:GridView ID="gview" runat="server" EmptyDataText="There are no data records to display." 
                        CssClass="table table-stiped table-bordered table-hover table-condensed" EnableViewState="false"
                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" UseAccessibleHeader="true" 
                        HeaderStyle-CssClass="" Height="1">
                        <Columns>
                            <asp:BoundField HeaderText="Mobile No" DataField="MobileNo"/>
                            <asp:BoundField HeaderText="Transaction" DataField="Type"/>
                            <asp:BoundField HeaderText="Eclectics Mobile Request" DataField="Request"/>
                            <asp:BoundField HeaderText="FinMFI Response" DataField="Response" />
                            <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString=""/>
                        </Columns>
                        <FooterStyle CssClass="footerStyle" />
                        <HeaderStyle CssClass="headerStyle" />
                        <RowStyle CssClass="rowStyle" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                        <HeaderStyle BackColor="LightGray" ForeColor="Black" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    
    </div>

</asp:Content>
