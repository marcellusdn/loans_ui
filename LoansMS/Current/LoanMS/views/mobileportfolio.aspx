<%@ Page Title="Mobile Portfolio" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="mobileportfolio.aspx.vb" Inherits="LoansMS.mobileportfolio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
       
    <div class="panel panel-info">
        
        <div class="panel-heading">
            <strong>Mobile Loans Search Criteria</strong> 
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

                <div class="col-md-3">
                    <input id="txtEmployer" type="text" class="form-control" name="Employer Name" value="" placeholder="Employer Name" runat="server" />
                </div>

                <div class="col-md-1">
                    <input id="btnSearch" type="button" class="btn btn-success btn-md" value="Search" runat="server" />
                </div>

                <div class="col-md-1">
                    <input id="btnCancel" type="button" class="btn btn-danger btn-md" value="Cancel" runat="server" onserverclick="cancel" />
                </div>

            </div>
           
        </div>
        
        <div class="panel-heading">
            <strong>Search Results</strong> 
        </div>

        <div class="panel-body">
            
            <div class="table-responsive">
                    
                <asp:GridView ID="gview" runat="server" DataKeyNames="mobileno" UseAccessibleHeader="true" 
                    CssClass="table table-condensed table-bordered table-hover" Height="100%" Width="100%" 
                    AutoGenerateColumns="False" HorizontalAlign="Justify" AllowPaging="false" PageSize="10" >
                    <Columns>
                        <asp:BoundField HeaderText="Mobile No" DataField="mobileno" SortExpression="futurebalance"/>
                        <asp:BoundField HeaderText="Customer" DataField="customer" SortExpression="futurebalance" />
                        <asp:BoundField HeaderText="Loan Account" DataField="loanaccount" SortExpression="futurebalance" />
                        <asp:BoundField HeaderText="Loan Amount" DataField="loanamount" SortExpression="futurebalance" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Loan Balance" DataField="loanbalance" SortExpression="futurebalance" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Loan Arrears" DataField="overdueamount" SortExpression="futurebalance" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField HeaderText="Employer" SortExpression="futurebalance" DataField="employer" />
                        <asp:BoundField HeaderText="DPD" SortExpression="futurebalance" DataField="dpd" />
                        <asp:BoundField HeaderText="Future Balance" DataField="futurebalance" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" SortExpression="futurebalance" />
                    </Columns>
                    <HeaderStyle CssClass="panel-heading" BackColor="DarkGray" />
                    <PagerSettings Position="TopAndBottom" />
                </asp:GridView>
            
            </div>

        </div>

    </div>

</asp:Content>
