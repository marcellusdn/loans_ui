<%@ Page Title="Portfolios" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="portfolio.aspx.vb" Inherits="LoansMS.portfolio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="panel panel-info">

        <div class="panel-heading">
            <strong>Portfolio Search Criteria</strong>
        </div>

        <div class="panel-body">

            <div class="row form-group">

                <div class="col-md-2">
                    <input class="form-control" type="text" name="txtAccountNo" id="txtAccountNo" value="" placeholder="Account No" runat="server" />
                </div>
            
                <div class="col-md-3">
                    <input class="form-control" type="text" name="txtCustomer" id="txtCustomer" value="" placeholder="Customer" runat="server" />
                </div>

                <div class="col-md-2">
                    <input class="form-control" type="text" name="txtBaseNo" id="txtBaseNo" value="" placeholder="Base No" runat="server" />
                </div>

                <div class="col-md-2">
                    <input class="form-control" type="Date" name="txtStartDate" id="txtStartDate" value="" placeholder="Start Date" runat="server" />
                </div>

                <div class="col-md-2">
                    <input class="form-control" type="Date" name="txtEndDate" id="txtEndDate" value="" placeholder="End Date" runat="server" />
                </div>
            
            </div>

            <div class="row form-group">

                <div class="col-md-2">
                    <input class="form-control" type="text" name="txtMinAmt" id="txtMinAmt" value="" placeholder="Min Amount" runat="server" />
                </div>

                <div class="col-md-2">
                    <input class="form-control" type="text" name="txtMaxAmt" id="txtMaxAmt" value="" placeholder="Max Amount" runat="server" />
                </div>

                <div class="col-md-2">
                    <select class="form-control" runat="server" id="lsSector" placeholder ="Sector">
                        <option value=""></option>
                        <option value=""></option>
                    </select>
                </div>

                <div class="col-md-3">
                    <select class="form-control" runat="server" id="lsProduct" placeholder ="Product">
                        <option value=""></option>
                        <option value=""></option>
                    </select>
                </div>

                <div class="col-md-1">
                    <input class="btn btn-primary" type="button" name="btnSearch" id="btnSearch" value="Search" runat="server" onserverclick="LoadPortfolio" />
                </div>

                <div class="col-md-1">
                    <input class="btn btn-warning " type="button" name="btnReset" id="btnReset" value="Refresh" runat="server" onserverclick="Reset" />
                </div>

            </div>
        
        </div>

        <div class="panel-heading">
            <strong>Search Results</strong>
        </div>

        <div class="table-responsive" >
            <asp:GridView CssClass="table table-condensed table-hover table-bordered" style="border-radius:5px; " ID="gvPortfolio" runat="server" 
                AutoGenerateColumns="False" AllowPaging="True" PageSize="30" 
                 OnPageIndexChanging ="gvPortfolio_PageIndexChanged" AllowSorting="True" >
                <Columns>
                    <asp:BoundField HeaderText="Loan Account" DataField="LoanAccount">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Customer" DataField="Customer">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Loan Amount" datafield="LoanAmount"/>
                    <asp:BoundField HeaderText="Principal" datafield="PrincipalBalance" DataFormatString="{0:N}" NullDisplayText="0.00" >
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Interest" datafield="InterestBalance" DataFormatString="{0:N}">
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Loan Balance" datafield="LoanBalance" DataFormatString="{0:N}" NullDisplayText="0.00" >
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Loan Arrears" datafield="LoanArrears" DataFormatString="{0:N}" NullDisplayText="0.00" >
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DPD" datafield="DPD"/>
                    <asp:BoundField HeaderText="Overdue Date" datafield="OverdueDate" DataFormatString="{0:d}"/>
                    <asp:BoundField HeaderText="Product" datafield="Product"/>
                    <asp:BoundField HeaderText="Sector" datafield="Sector"/>
                    <asp:BoundField HeaderText="Classification" datafield="Classification" />
                </Columns>
                <HeaderStyle BackColor="#99CCFF" BorderStyle="Solid" CssClass="table table-stripped" />
                <PagerSettings Position="TopAndBottom" />
            </asp:GridView>
        </div>

    </div>

</asp:Content>
