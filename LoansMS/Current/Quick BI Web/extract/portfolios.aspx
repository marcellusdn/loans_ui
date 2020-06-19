<%@ Page Title="Portfolio Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="portfolios.aspx.vb" Inherits="LoansMS.portfolios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <title>
        Portfolio  Reports
    </title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="form-horizontal">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>Portfolio Reports Selection</h5>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="slPortfolio" id="lbReportName" runat="server">Report Type:</label>
                    <div class="col-sm-3 col-md-3">
                        <select id="slPortfolio" class="form-control" runat="server">
                        </select>
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportdate" id="lbReportDate" runat="server" >Report Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportdate" value="" class="form-control" runat="server" />
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
