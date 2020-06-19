<%@ Page Title="Collection Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports.Master" CodeBehind="clientstatement.aspx.vb" Inherits="LoansMS.clientstatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">

    <div class="form-horizontal">

        <div class="panel panel-info">

            <div class="panel-heading">
                <h5>PostBank Client Statement</h5>
            </div>

            <div class="panel-body">

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtAccountNo" id="lbAccountNo" >Account No:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="text" id="txtAccountNo" value="" class="form-control" runat="server" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtstartdate" id="lbStartDate">Start Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtstartdate" value="" class="form-control" runat="server" />
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-sm-1 col-md-2 control-label" for="txtenddate" id="lbEndDate">End Date:</label>
                    <div class="col-sm-1 col-md-3">
                        <input type="date" id="txtenddate" value="" class="form-control" runat="server" />
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
