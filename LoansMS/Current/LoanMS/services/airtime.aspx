<%@ Page Title="Airtime Topup" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="airtime.aspx.vb" Inherits="LoansMS.airtime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-primary">

        <div class="panel-heading">
            <strong>Bulk Airtime Topup</strong>
        </div>

        <div class="panel-body">

            <div class="row" style="margin:0; padding:0;">
                Please select the Excel File to upload containing the List of customers to top-up airtime to their respective mobile phone numbers. The file should contain:
                <ol type="1">
                    <li><strong>Mobile Number: </strong>This is the mobile number of the intended recipient for the airtime to be loaded. The Excel file Heading should be in the name <strong>Mobile No</strong></li>
                    <li><strong>Loan Account:</strong> This should be the loan account funded and warranting the airtime incentive.</li>
                    <li><strong>Customer Name:</strong> This is the name of the intendend recepient of the airtime. It is expected that this is the owner of the Mobile Number.</li>
                    <li><strong>Loan Amount:</strong> This is the loan amount granted to the client to warrant the top-up to the recipient.</li>
                    <li><strong>Airtime Value:</strong> This is the airtime value to be loaded to the recipient's mobile number.</li>
                </ol>
            </div>

            <div class="row page-header">
                <label class="col-md-4 control-label" id="lbActionName" runat="server"><strong>Provide the Airtime File Details</strong></label>
            </div>

            <div class="row form-group" style="padding-top:1%">

                <label class="col-md-1 control-label" for="fuMessages" id="lbFileName" runat="server"><strong>File Name</strong></label>
                <div class="col-md-5">
                    <asp:FileUpload ID="fuMessages" runat="server" />
                </div>

                <label class="col-md-1 control-label" style="padding-top: 0.5%; padding-right: 0" id="lbDept" for="lsDepartments" runat="server">Department:</label>
                <div class="col-md-2">
                    <select class="form-control col-md-2" id="lsDepartments" runat="server">
                        <option value=""></option>
                    </select>
                </div>

                <div class="col-md-3">
                    <input style="margin-left: 5%" class="btn btn-primary" type="button" id="btnUpload" name="btnUpload" value="Upload" runat="server" />
                </div>

            </div>

            <div class="row">
                <div class="panel panel-info">
                    <div class="row form-group">
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top: 2%; text-align: center" id="lbmobilenos" for="txtMobileNos" runat="server">Total Mobile Numbers:</label>
                            <input type="text" name="txtMobileNos" id="txtMobileNos" value="" disabled="disabled" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top: 2%" id="lbUniqueMobile" for="txtUniqueMobile" runat="server">Unique Mobile Numbers:</label>
                            <input type="text" name="txtUniqueMobile" id="txtUniqueMobile" value="" disabled="disabled" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top: 2%" id="lbAccountNos" for="txtAccountNos" runat="server">Account Numbers:</label>
                            <input type="text" name="txtAccountNos" id="txtAccountNos" value="" disabled="disabled" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top: 2%" id="lbUniqueAccounts" for="txtUniqueAccounts" runat="server">Unique Accounts:</label>
                            <input type="text" name="txtUniqueAccounts" id="txtUniqueAccounts" disabled="disabled" value="" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" style="padding-top: 2%" id="Label1" for="txtMinAirtime" runat="server">Minimum:</label>
                            <input type="text" name="txtMinAirtime" id="txtMinAirtime" disabled="disabled" value="" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" style="padding-top: 2%" id="lbMaxLength" for="txtMaxAirtime" runat="server">Maximum:</label>
                            <input type="text" name="txtMaxAirtime" id="txtMaxAirtime" disabled="disabled" value="" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top: 2%" id="lbCost" for="txtCost" runat="server">Estimated Cost:</label>
                            <input type="text" name="txtCost" id="txtCost" value="" disabled="disabled" class="form-control" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row" style ="margin-top:1%">
                <div class="col-md-8">
                    <div class="panel-body">
                        <asp:GridView ID="gvairtime" runat="server" Visible="false" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField HeaderText="Mobile No" DataField="Mobile No"/>
                                <asp:BoundField HeaderText="Customer Name" DataField="Customer Name"/>
                                <asp:BoundField HeaderText="Account Number" DataField="Loan Account"/>                                       
                                <asp:BoundField HeaderText="Loan Amount" DataField="Loan Amount" />
                                <asp:BoundField HeaderText="Airtime" DataField="Airtime Value" />
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <SortedAscendingCellStyle BackColor="#F4F4FD" />
                            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                            <SortedDescendingCellStyle BackColor="#D8D8F0" />
                            <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-4">
                        <input class="btn btn-success btn-md" type="button" id="btnPush" name="btnPush" value="Send" runat="server" onserverclick="btnSend_click" />
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <input class="btn btn-danger btn-md" type="button" id="btnCancel" name="btnCancel" value="Reset" runat="server" onserverclick="btnCancel_Click" />
                    </div>
                    <div class="col-md-2"></div>
                </div>
            </div>
            
        </div>
            
    </div>

</asp:Content>
