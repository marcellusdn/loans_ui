<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="LoansMS.Login" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<%--<html xmlns="http://www.w3.org/1999/xhtml"> --%>
<html>
<head runat="server">

    <title>Welcome to Quick BI</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css?parameter=1" rel="stylesheet"/>
    <script type="text/jscript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/jscript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <link rel="shortcut icon" href="~/QuickBI.ico" type="image/x-icon" />
    <link rel="icon" type="image/ico" href="~/QuickBI.ico" />
    <link rel="apple-touch-icon" type="image/ico" href="/~/QuickBI.ico" />

    <style type="text/css">
        .style1
        {
            width: 162px;
            vertical-align:middle;
        }
        .a
        {
            color:white;
        }
        .a hover
        {
            color:black;
        }
    </style>

</head>

<body>
    
    <form id="form1" runat="server">
    
        <div class="container" style="padding:10% 0 0 0">

            <div class="row">

                <div class="col-md-4 col-md-offset-4">

                    <div class="panel panel-primary">

                        <div class="panel-heading" style="color:white">

                            <div class="row">
                                <h3 style="text-align:center;color:white">Welcome to Quick BI</h3>
                            </div>
                            <div class="row">
                                <h4 style="text-align:center;"><small style="color:white;">Login to Proceed</small></h4>
                            </div>
                        
                        </div>

                        <div class="panel-body">

                            <div class="row">
                                <div class="col-md-3 form-group" style="width:100%;">
                                    <input type="text" class="form-control input-lg" id="txtLoginID" placeholder="Login ID" runat="server"/>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3 form-group" style="width:100%;">
                                    <input type="password" class="form-control input-lg" id="txtPassword" placeholder="Password" runat="server" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3 form-group" style="width:100%;background-color:wheat; color:teal;font-style:italic;text-align:center;">
                                    <label id="vallogin"  class="control-label" runat="server">Invalid login credentials</label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-2">
                                    <input class="btn btn-primary btn-lg" type="button" id="btnLogin" name="btnLogin" value="Login" runat="server" onserverclick="btnLogin_Click"/>
                                </div>
                                <div class="col-md-4"></div>
                                <div class="col-md-2">
                                    <input class="btn btn-danger btn-lg" type="button" id="btnExit" name="btnExit" value="EXIT" runat="server" onserverclick="btnExit_Click"/>
                                </div>
                            </div>
                        
                        </div>

                    </div>

                </div>

            </div>

        </div>

    </form>

</body>
</html>
