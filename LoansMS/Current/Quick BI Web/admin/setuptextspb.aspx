<%@ Page Title="Update Settings" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="setuptextspb.aspx.vb" Inherits="LoansMS.setuptextspb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
    
    <div class="panel panel-primary">

        <div class="panel-heading" style="text-align:center;">
            <strong>PostBank Messages Setup</strong>
        </div>

        <div class="panel-body">

            <div class="container" style="padding-top:1%;">

                <div class="col-md-6 col-md-offset-3 form-horizontal">

                    <div class="row form-group">
                        <%--<label class="control-label col-md-4" for="slFrequency" runat="server"><strong>Frequency:</strong></label>--%>
                        <div class="col-md-6">
                            <select id="slFrequency" class="form-control" runat="server">
                                <option value="">Select Frequency</option>
                                <option value="Active">Daily</option>
                                <option value="Weekly">Weekly</option>
                                <option value="Monthly">Monthly</option>
                                <option value="Fortnightly">Fortnightly</option>
                                <option value="Other">Other</option>
                            </select>
                        </div>
                    </div>

                    <div class="row form-group">
                        <%--<label class="control-label col-md-5" for="txtMessageCount" runat="server"><strong>Messsage Count:</strong></label>--%>
                        <div class="col-md-4">
                            <input type="text" class="form-control" id="txtMessageCount" placeholder="No of Messages" runat="server" />
                        </div>
                    </div>

                    <div class="row btn-block btn-group-justified">
                       <div class="col-md-2">
                           <input type="button" id="btnEdit" class="btn btn-primary btn-md" runat="server" value="Update" onserverclick="EditOnClick"/>
                       </div>
                       <div class="col-md-2">
                           <input type="button" id="btnCancel" class="btn btn-danger btn-outline-danger btn-md" runat="server" value="Cancel" onserverclick="CancelOnClick" />
                       </div>
                   </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
