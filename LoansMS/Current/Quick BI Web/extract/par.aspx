<%@ Page Title="PAR Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="par.aspx.vb" Inherits="LoansMS.par" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="form-horizontal">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>Portfolio at Risk (PAR) Reports Selection</h5>
            </div>

            <div class="panel-body form-horizontal">

                <div class="row form-group">

                    <label class="col-sm-1 col-md-2 control-label" for="slPortfolio" id="lbReportName" >Report Name</label>
                    <div class="col-sm-3 col-md-3">
                        <select id="slPAR" class="form-control" runat="server" multiple="false">
                        </select>
                    </div>

                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtreportdate" id="lbReportDate" >Report Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtreportdate" value="" class="form-control" runat="server" />
                    </div>
                </div>

            </div>

            <div class="panel-footer">

                <div class="row btn-group btn-group-justified">

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
