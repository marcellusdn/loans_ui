<%@ Page Title="Airtime Topup Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="topup.aspx.vb" Inherits="LoansMS.topup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="form-horizontal">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>Airtime Top Up Report Selection</h5>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportname" id="lbReportName">Report Name:</label>
                    <div class="col-sm-3 col-md-3">
                        <select class="form-control" runat="server" id="lsReportName">
                            <option value=""></option>
                        </select>
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txttopupstartdate" id="lbMessageDate" runat="server">Start Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txttopupstartdate" value="" class="form-control" runat="server" />
                    </div>
                    <label class="col-sm-1 col-md-2 control-label" for="txttopupenddate" id="Label1" runat="server">End Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txttopupenddate" value="" class="form-control" runat="server" />
                    </div>
                </div>
                
                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtCustomerName" id="lbCustomerName" runat="server">Customer Name:</label>
                    <div class="col-sm-3 col-md-5">
                        <input type="text" id="txtCustomerName" value="" class="form-control" runat="server" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtMobileNo" id="lbMobileNo" runat="server">Mobile No:</label>
                    <div class="col-sm-3 col-md-3">
                        <input type="text" id="txtMobileNo" value="" class="form-control" runat="server" />
                    </div>
                </div>

            </div>

            <div class="panel-footer">

                <div class="row">

                    <div class="col-md-2"></div>

                    <div class="col-sm-1 col-md-2">
                        <input type="button" id="btnExtract" value="Extract" class="btn btn-primary" runat="server" onserverclick="btnExtractClick" />
                    </div>

                    <div class="col-sm-1 col-md-2">
                        <input type="button" id="btnCancel" value="Cancel" class="btn btn-warning" />
                    </div>
                
                </div>

            </div>

        </div>

    </div>
        
   
</asp:Content>
