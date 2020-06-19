<%@ Page Title="CRB Submission Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="crb.aspx.vb" Inherits="LoansMS.crb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="form-horizontal" style="height:inherit">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>CRB Monthly Submission Reports Selection</h5>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="slUpdates" id="lbReportType" runat="server">Report Type:</label>
                    <div class="col-sm-3 col-md-3">
                        <select class="form-control" id="slUpdates" runat="server">
                        </select>
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportdate" id="lbReportDate" runat="server">Report Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportdate" value="" class="form-control" runat="server" />
                    </div>
                </div>

            </div>

            <div class="panel-footer">

                <div class="row">

                    <div class="col-md-2"></div>

                    <div class="col-sm-1 col-md-2">
                        <input type="button" id="btnExtract" value="Extract" class="btn btn-primary" runat="server" onserverclick="BtnExtractClick" />
                    </div>

                    <div class="col-sm-1 col-md-1">
                        <input type="button" id="btnCancel" value="Cancel" class="btn btn-warning" runat="server" />
                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
