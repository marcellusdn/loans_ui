<%@ Page Title="Messages" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="msgview.aspx.vb" Inherits="LoansMS.msgview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-info">

        <div class="panel-heading">
            <strong>Messages Selection Criteria</strong>
        </div>

        <div class="panel-body">

            <div class="row">
                <div class="col-md-2">
                    <input id="txtAccount" type="text" class="form-control" name="txtloanaccount" value="" placeholder="Account No" runat="server" />
                </div>
                <div class="col-md-2">
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
                <div class="col-md-1">                    
                    <input id="btnSearch" type="button" class="btn btn-info" name="btnSearch" value="Search" runat="server" onserverclick="BtnSearchClick" />
                </div>
                <div class="col-md-1">                    
                    <input id="btnReset" type="button" class="btn btn-warning" name="btnReset" value="Reset" runat="server" onserverclick="Clear" />
                </div>
            </div>
            
        </div>

        <div class="panel-heading">
            <strong>Message Search Results</strong>
        </div>

        <div class="panel-body">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-stripped table-condensed table-hover table-bordered" style="border-radius:5px; "
                    ID="gview" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" GridLines="None"
                    OnPageIndexChanging ="gview_PageIndexChanged" AllowSorting="True" DataKeyNames="mobileno" 
                    Height="100%" Width="100%" EmptyDataText="There are no data records to display." 
                    ShowHeaderWhenEmpty="true" >
                    <Columns>
                        <asp:BoundField HeaderText="Mobile No" DataField="mobileno" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Customer" DataField="customer" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Account No" Datafield="accountno" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Delivery Date" DataField="senddate" DataFormatString="{0:d}" >
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="state" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Text Message" DataField="message" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#99CCFF" BorderStyle="Solid" CssClass="table table-stripped" />
                    <PagerSettings Position="TopAndBottom" />
                </asp:GridView>
            </div>
        </div>
    
    </div>

</asp:Content>
