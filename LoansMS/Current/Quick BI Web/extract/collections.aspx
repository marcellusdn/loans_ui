<%@ Page Title="Collection Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="collections.aspx.vb" Inherits="LoansMS.collections" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="form-horizontal">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>Debt Collection Reports Selection</h5>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportname" id="lbReportName" >Report Name:</label>
                    <div class="col-sm-1 col-md-3">
                        <select id="slDebtCollection" class="form-control" runat="server">
                            <option value="ScoreCard" id="scorecard">Score Card</option>
                            <option value="Collections" id="collections">Collections</option>
                        </select>
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportdate" id="lbReportDate">Report Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportdate" value="" class="form-control" runat="server" />
                    </div>
                </div>

                <div class="row form-group">

                    <label class="col-sm-1 col-md-2 control-label" for="txtreportstartdate" id="lbReportStartDate">Start Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportstartdate" value="" class="form-control" runat="server" />
                    </div>

                    <label class="col-sm-1 col-md-2 control-label" for="txtreportstartdate" id="lbReportEndDate">End Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportenddate" value="" class="form-control" runat="server" />
                    </div>

                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="slCreditOfficer" id="lbCrediOfficer" runat="server" >Credit Officer:</label>
                    <div class="col-sm-3 col-md-3 ">
                        <select id="slCreditOfficer" class="form-control" runat="server">
                            <option value="" id="opcreditofficer"></option>
                        </select>
                    </div>
                </div>

                <div class="row form-group">

                    <label class="col-sm-1 col-md-2 control-label" for="txtBranchVisits" id="lbBranchVisits" runat="server">Branch Visits:</label>
                    <div class="col-sm-3 col-md-2">
                        <input type="text" class="form-control" name="txtBranchVisits" id="txtBranchVisits" value="" placeholder="Branch Visits" runat="server" />
                    </div>

                </div>

            </div>

            <div class="panel-footer">

                <div class="row">

                    <div class="col-md-2"></div>

                    <div class="col-sm-1 col-md-2">
                        <input type="button" id="btnExtract" value="Extract" class="btn btn-primary" runat="server" onserverclick="btnExtractClick" />
                    </div>

                    <div class="col-sm-1 col-md-1">
                        <input type="button" id="btnCancel" value="Cancel" class="btn btn-warning" runat="server" />
                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
