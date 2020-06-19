<%@ Page Title="Manage Password Rules" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="managepasswords.aspx.vb" Inherits="LoansMS.managepasswords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        Manage Password Complexity
    </title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="id_index" runat="server">
       
    <div class="panel panel-primary form-horizontal">

        <div class ="panel-heading" style="text-align:center">
            Manage Password Complexity Rules
        </div>

        <div class="panel-body">

            <div class="container">

                <div class="col-md-6 col-md-offset-4">

                    <div class="row form-group">
                        <label class="control-label col-md-3" id="lbMinLength" runat="server" for="txtMinLength">Mininum Length:</label>
                        <div class="col-md-2">
                            <input class="form-control" runat="server" type="text" name="txtMinLength" id="txtMinLength" value="" Placeholder="Min"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-3" runat="server" id="lbMaxLength" for="txtMaxLength">Mininum Length:</label>
                        <div class="col-md-2">
                            <input class="form-control" runat="server" type="text" name="txtMaxLength" id="txtMaxLength" value="" Placeholder="Max"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-3" id="lbPasswordAge" runat="server" for="txtDuration">Password Age:</label>
                        <div class="col-md-2">
                            <input class="form-control" runat="server" type="text" name="txtDuration" id="txtDuration" value="" Placeholder="Age"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-3" id="lbSpecialChar" runat="server" for="txtSpecialChar">Special Characters:</label>
                        <div class="col-md-2">
                            <input class="form-control" runat="server" type="text" name="txtDuration" id="txtSpecialChar" value="" Placeholder="Special"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-3" id="lbUpperCase" for="txtUpperCase" runat="server">Upper Case:</label>
                        <div class="col-md-2">
                            <input class="form-control" type="text" name="txtUpperCase" id="txtUpperCase" runat="server" value="" Placeholder="Upper"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-3" id="lbNumbers" for="txtUpperCase" runat="server">Numerics:</label>
                        <div class="col-md-2">
                            <input class="form-control" type="text" name="txtNumerics" id="txtNumerics" runat="server" value="" Placeholder="Numbers"/>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="control-label col-md-3" id="lbEnforceHistory" for="chkEnforeHistory" runat="server">Enforce History:</label>
                        <div class="col-md-2">
                            <input type="checkbox" class="form-control" id="chkEnforeHistory" runat="server" />
                        </div>
                    </div>

                    <div class="row btn-group-justified">
                         
                        <div class="col-md-1"></div>

                        <div class="col-md-2">
                            <input class="btn btn-primary" type="button" name="btnUpdate" id="btnUpdate" runat="server" value="Update" onserverclick="UpdateClick" />
                        </div>

                        <div class="col-md-1"></div>

                        <div class="col-md-2">
                            <input class="btn btn-danger" type="button" name="btnCancel" id="btnCancel" runat="server" value="Cancel" onserverclick="CancelClick" />
                        </div>

                    </div>

                    <div>
                        <label id="lbResponse" runat="server" visible="false"></label>
                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
