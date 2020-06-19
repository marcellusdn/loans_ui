<%@ Page Title="Topups" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="airview.aspx.vb" Inherits="LoansMS.airview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-info">

        <div class="panel-heading">
            <strong>Airtime Selection Criteria</strong>
        </div>

        <div class="panel-body">

            <div class="row form-group">
                <div class="col-md-2">
                    <input id="txtAccount" type="text" class="form-control" name="txtloanaccount" value="" placeholder="Account No" runat="server" />
                </div>
                <div class="col-md-3">
                    <input id="txtName" type="text" class="form-control" name="Customer Name" value="" placeholder="Customer Name" runat="server" />
                </div>
                <div class="col-md-2">
                    <input id="txtMobile" type="text" class="form-control" name="Mobile No" value="" placeholder="Mobile No" runat="server" />
                </div>
                <div class="col-md-2">
                    <input id="txtStartDate" type="date" class="form-control" name="Start Date" value="" placeholder="Start Date" runat="server" />
                </div>
                <div class="col-md-2">
                    <input id="txtEndDate" type="date" class="form-control" name="Last Date" value="" placeholder="End Date" runat="server" />
                </div>
                
            </div>

            <div class="row form-group">
                <div class="col-md-2">
                    <select class="form-control" name="lsResponse" id="lsResponse" runat="server">
                        <option value=""></option>
                    </select>
                </div>
                <%--<div class="col-md-2">
                    <input id="txtMinAmount" type="text" class="form-control" name="txtMinAmount" value="" placeholder="Min Airtime" runat="server" />
                </div>
                <div class="col-md-2">
                    <input id="txtMaxAmount" type="text" class="form-control" name="txtMaxAmount" value="" placeholder="Max Airtime" runat="server" />
                </div>--%>
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

            <div class="table-responsive">

                <asp:GridView ID="gview" Visible="false" runat="server" DataKeyNames="mobileno" UseAccessibleHeader="true"
                    CssClass="table table-bordered table-condensed table-hover" AutoGenerateColumns="False" Height="100%" 
                    Width="100%" HorizontalAlign="Justify" ShowHeaderWhenEmpty="true" AllowPaging="True" PageSize="10" 
                    OnPageIndexChanging ="gview_PageIndexChanged" AllowSorting="True"
                    EmptyDataText="There are no data records to display.">
                    <Columns>
                        <asp:BoundField HeaderText="Mobile No" DataField="mobileno" > 
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                         </asp:BoundField>
                        <asp:BoundField HeaderText="Customer" DataField="customer" > 
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                         </asp:BoundField>
                        <asp:BoundField HeaderText="Loan Account" DataField="accountno" > 
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                         </asp:BoundField>
                        <asp:BoundField HeaderText="Delivery Date" DataField="senddate" DataFormatString="{0:d}" > 
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                         </asp:BoundField>
                        <asp:BoundField HeaderText="Status"  DataField="response" > 
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                         </asp:BoundField>
                        <asp:BoundField HeaderText="Airtime" DataField="airtimeamount" > 
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                         </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footerStyle" />
                    <HeaderStyle CssClass="panel-heading" BackColor="DarkGray" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    
                </asp:GridView>

            </div>

        </div>
    
    </div>

</asp:Content>
