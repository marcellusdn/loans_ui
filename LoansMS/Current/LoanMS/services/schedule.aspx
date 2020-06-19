<%@ Page Title="Schedule SMS" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="schedule.aspx.vb" Inherits="LoansMS.schedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

   <div class="panel panel-primary">

        <div class="panel-heading">
           <strong>Schedule Messages</strong>
        </div>
        
        <div class="panel-body">

            <div class="row" style="margin:0; padding:0;">
                Please select the Excel File to upload containing the list of customers to send text messages to at a scheduled date and time. The file should contain:
                <ol type="1">
                    <li><strong>Mobile Number: </strong>This is the mobile number of the intended recipient for the message to be sent. The Excel file Heading should be in the name <strong>Mobile No</strong></li>
                    <li><strong>Loan Account:</strong> This should be the account of the customer, if it is a new customer. Alternatively, provide the loan account or base number for an existing customer.</li>
                    <li><strong>Customer Name:</strong> This is the name of the intendend recepient of the message. It is expected that this is the owner of the Mobile Number.</li>
                    <li><strong>Custom Message</strong> This is the message to be sent to the recipient.</li>
                </ol>
            </div>

            <div class="row page-header">
                <strong>Select Schedule Details</strong>
            </div>

            <div class="row form-group">
                
                <div class="col-md-2">
                    <label id="lbTime" class="control-label" style="padding-top:0.5%;" text="Time" for="txtTime" runat="server">Scheduled Time:</label>
                    <input class="form-control" type="text" id="txtTime" name="txtTime" value="" placeholder="Schedule Time" runat="server" />
                </div>
                
                <div class="col-md-2">
                    <label id="lbDate" class="control-label" style="padding-top:0.5%;" text="Date" for="txtDate" runat="server">Scheduled Date:</label>
                    <input class="form-control" type="Date" id="txtDate" name="txtDate" value="" placeholder="Schedule Date" runat="server" />
                </div>
                
                <div class="col-md-2">
                    <label id="lbDept" class="col-md-1 control-label" style="padding-top:0.5%;" text="Department" for="lsDept" runat="server">Department:</label>
                    <select class="form-control" runat="server" id="lsDept">
                        <option value=""></option>
                    </select>
                </div>

                <div class="col-md-2">
                    <label id="lbMsgs" class="col-md-1 control-label" style="padding-top:0.5%;" text="Message Type" for="lsMsgs" runat="server">Messages:</label>
                    <select class="form-control" runat="server" id="lsMsgs">
                        <option value=""></option>
                    </select>
                </div>
                
                <div class="col-md-3">
                    <label id="lbFileUpload" class ="control-label" runat="server" style="margin-top:2%" ><strong>File Name:</strong> </label>
                    <asp:FileUpload ID="fuMessages" runat="server" BackColor="Transparent" />
                </div>

                <div class="col-md-1" style="padding-top:1%">
                    <input type="button" id="btnUpload" UseSubmitBehavior="False" class="btn btn-primary" style="margin-top:10%" name="btnUpload" value="Upload" runat="server" onserverclick="loadfile" />
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
                            <input type="text" name="txtUniqueAccounts" id="txtUniqueAccounts" value="" disabled="disabled" class="form-control" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" style="padding-top: 2%" id="lbMaxLength" for="txtMaxLength" runat="server">Max. Messsage Length:</label>
                            <input type="text" name="txtMaxLength" id="txtMaxLength" value="" disabled="disabled" class="form-control" runat="server" />
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
               
                <div class="col-md-4">
                    <div class ="btn-block btn-group-justified">
                        <div class="col-md-4">
                            <input class="btn btn-success" type="button" id="btnPush" name="btnPush" value="Send" runat="server" onserverclick="btnSend_Click" />
                        </div>
                        <div class="col-md-2"></div>
                        <div class="col-md-4">
                            <input type="button"  class="btn btn-danger" id="btnCancel" name="btnCancel" value="Reset" runat="server" onserverclick="BtnCancel_Click" />
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                </div>
                
            </div>

        </div>

    </div>

</asp:Content>
