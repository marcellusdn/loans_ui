<%@ Page Title="SMS Push" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="sms.aspx.vb" Inherits="LoansMS.sms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="panel panel-primary">
                    
        <div class="panel-heading">
            <strong>Bulk SMS File Upload</strong>
        </div>

        <div class="panel-body">

            <div class="row" style="margin:0; padding:0;">
                Please select the Excel File to upload containing the List of customer to send text messages to via their respective mobile phone numbers. The file should contain:
                <ol type="1">
                    <li><strong>Mobile Number: </strong>This is the mobile number of the intended recipient for the message to be sent. The Excel file Heading should be in the name <strong>Mobile No</strong></li>
                    <li><strong>Loan Account:</strong> This should be the account of the customer, if it is a new customer. Alternatively, provide the loan account or base number for an existing customer.</li>
                    <li><strong>Customer Name:</strong> This is the name of the intended recepient of the message. It is expected that this is the owner of the Mobile Number.</li>
                    <li><strong>Custom Message</strong> This is the message to be sent to the recipient.</li>
                </ol>
            </div>
            
            <div class="row page-header">
                <label class="col-md-4 control-label" id="lbActionName" runat="server"><strong>Provide the Message File Details</strong></label>            
            </div>
            
            <div class="row form-group" style="padding:1%;">
                        
                <label class="col-md-1 control-label" style="padding-top:1%;" id="lbFileName" runat="server" for="fuMessages"><strong>File:</strong></label>            
                <div class="col-md-3" style="padding:1%">
                    <asp:FileUpload ID="fuMessages" runat="server"  Width="466px" />
                </div>

                <label class="col-md-1 control-label" style="padding-top:0.5%;padding-right:0" id="lbDept" for="lsDepartments" runat="server">Department:</label>
                <div class="col-md-2">
                    <select class="form-control col-md-2" id="lsDepartments" runat="server">
                        <option value=""></option>
                    </select>
                </div>

                <label class="col-md-1 control-label" style="padding-top:0.5%;padding-right:0" id="lbMessageType" for="lsMessageType" runat="server">Type:</label>
                <div class="col-md-2">
                    <select class="form-control col-md-2" id="lsMessageType" runat="server">
                        <option value=""></option>
                    </select>
                </div>
                    
                <div class="col-md-2">
                    <input style="margin-left:5%" class="btn btn-primary" type="button" id="btnUpload" name="btnUpload" value="Upload" runat="server" />
                </div>

            </div>
             
            <div class="row">
                <div class="panel panel-info">                        
                    <div class="row form-group">
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top:2%; text-align:center" id="lbmobilenos" for="txtMobileNos" runat="server">Total Mobile Numbers:</label>
                            <input type="text" name="txtMobileNos" id="txtMobileNos" value="" class="form-control" runat="server"/>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top:2%"  id="lbUniqueMobile" for="txtUniqueMobile" runat="server">Unique Mobile Numbers:</label>
                            <input type="text" name="txtUniqueMobile" id="txtUniqueMobile" value="" class="form-control" runat="server"/>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top:2%" id="lbAccountNos" for="txtAccountNos" runat="server">Account Numbers:</label>
                            <input type="text" name="txtAccountNos" id="txtAccountNos" value="" class="form-control" runat="server"/>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top:2%" id="lbUniqueAccounts" for="txtUniqueAccounts" runat="server">Unique Accounts:</label>
                            <input type="text" name="txtUniqueAccounts" id="txtUniqueAccounts" value="" class="form-control" runat="server"/>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top:2%" id="lbMaxLength" for="txtMaxLength" runat="server">Max. Messsage Length:</label>
                            <input type="text" name="txtMaxLength" id="txtMaxLength" value="" class="form-control" runat="server"/>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top:2%" id="lbCost" for="txtCost" runat="server">Estimated Cost:</label>
                            <input type="text" name="txtCost" id="txtCost" value="" class="form-control" runat="server"/>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-8">
                    <div class="panel-body">
                        <asp:GridView ID="gvTextFile" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="3" GridLines="Horizontal">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:BoundField HeaderText="Mobile No" DataField="Mobile No">
                                    <FooterStyle Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Account Number" DataField="Loan Account">
                                    <ItemStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Customer Name" DataField="Customer Name">
                                    <ItemStyle Width="320px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Custom Message" DataField="Custom Message" />
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
                
                <div class="col-md-4" style ="margin-top:1%">
                    <div class="col-md-1"></div>
                    <div class="col-md-4">
                        <input class="btn btn-success btn-md" type="button" id="btnPush" name="btnPush" value="Send" runat="server" onserverclick="btnSend_Click" />
                    </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <input class="btn btn-danger btn-md" type="reset" id="btnCancel" name="btnCancel" value="Reset" runat="server" />
                    </div>
                    <div class="col-md-1"></div>
                </div>
                
            </div>

        </div>

    </div>

</asp:Content>
