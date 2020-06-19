<%@ Page Title="CRB Update Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="updates.aspx.vb" Inherits="LoansMS.updates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="form-horizontal">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>CRB Updates Report Selection</h5>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportname" id="lbReportName" runat="server" >Report Name:</label>
                    <div class="col-sm-3 col-md-3">
                        <select id="slUpdate" class="form-control" runat="server">
                        </select>
                    </div>
                </div>

                <%--<div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportname" id="lbReportType" runat="server" >Report Type:</label>
                    <div class="col-sm-3 col-md-3">
                        <select id="slType" class="form-control" runat="server">
                            <option value="" id="option1"></option>
                            <option value="Individuals" id="indiv">Individuals</option>
                            <option value="Corporates" id="biz">Corporates</option>
                        </select>
                    </div>
                </div>--%>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportstartdate" id="lbReportStartDate">Start Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportstartdate" value="" class="form-control" runat="server" />
                    </div>
                </div>

                <div class="row form-group">

                    <label class="col-sm-1 col-md-2 control-label" for="txtreportstartdate" id="lbReportEndDate">End Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportenddate" value="" class="form-control" runat="server" />
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
