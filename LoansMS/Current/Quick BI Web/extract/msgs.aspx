<%@ Page Title="SMS Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="msgs.aspx.vb" Inherits="LoansMS.msgs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="form-horizontal">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>Messages Report Selection</h5>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="slReportName" id="lbReportName" runat="server" >Report Name:</label>
                    <div class="col-sm-1 col-md-3">
                        <select class="form-control" id="slReportName" runat="server">
                            <option value=""></option>
                        </select>
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtmessagestartdate" id="lbMessageDate" >Start Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtmessagestartdate" value="" class="form-control" runat="server" />
                    </div>
                    <label class="col-sm-1 col-md-2 control-label" for="txtmessageenddate" id="lbMessageEndDate" >End Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtmessageenddate" value="" class="form-control" runat="server" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtCustomerName" id="lbCustomerName" >Customer Name:</label>
                    <div class="col-sm-3 col-md-5">
                        <input type="text" id="txtCustomerName" name="txtCustomerName" value="" class="form-control" runat="server" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtMobileNo" id="lbMobileNo">Mobile No:</label>
                    <div class="col-sm-3 col-md-3">
                        <input type="text" id="txtMobileNo" name="txtMobileNo" value="" class="form-control" runat="server" />
                    </div>
                </div>

            </div>

            <div class="panel-footer">  

                <div class="row">

                    <div class="col-md-2"></div>

                    <div class="col-sm-1 col-md-2">
                        <input type="button" id="Button1" value="Extract" class="btn btn-primary" runat="server" onserverclick="btnExtractClick" />
                    </div>

                    <div class="col-sm-1 col-md-2">
                        <input type="button" id="btnCancel" name="btnCancel" value="Cancel" class="btn btn-warning" runat="server" onserverclick="btnCancelClick" />
                    </div>

                </div>

            </div>

        </div>

    </div>
        
   
</asp:Content>
