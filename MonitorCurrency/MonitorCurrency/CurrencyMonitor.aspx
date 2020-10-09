<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrencyMonitor.aspx.cs" Inherits="currencyMonitor.CurrencyMonitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/jqPlot/jquery.jqplot.min.js"></script>

    <script type="text/javascript" src="Scripts/jqPlot/plugins/jqplot.highlighter.min.js"></script>
    <script type="text/javascript" src="Scripts/jqPlot/plugins/jqplot.cursor.min.js"></script>
    <script type="text/javascript" src="Scripts/jqPlot/plugins/jqplot.dateAxisRenderer.min.js"></script>

    <script type="text/javascript" src="Scripts/displayCurrency.js"></script>

    <link rel="stylesheet" type="text/css" href="Content/jquery.jqplot.min.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Currency rates</h1>
        <div id="usdchartdiv" style="height:400px;width:800px; "></div>
        <br />
        <div id="gbpchartdiv" style="height:400px;width:800px; "></div>
    </div>
    </form>
</body>
</html>
