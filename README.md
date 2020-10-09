# .net-monitoring-currency
Solution to query currency rates on Europe Central Bank, save them in local DB and display at request


Solution consists of 2 parts.
1) 'updateCurrency' obtains currency rates from European Central bank API and save data in local DB
2) 'MonitorCurrency' contains ASP.NET page that queries local DB and displays data to client

In solution is used my local DB 'currency' with table 'rates' that contains date field 'date' and 2 decimal fields for currencies: 'usd', 'gbp'.
Connection string can be replaced in config file.

## 1 part
In folder `./updateCurrency` there is console application that requests currency rates from ECB:
https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml
Then necessary currencies are extracted from the xml and stored in local DB.

If any error occurs, it's stored in file
`C:\ProgramData\updateCurrency\ErrorLog.txt`

In order to execute this process daily, this application after build must be registered in windows Task Scheduler.

## 2 part
In folder `./MonitorCurrency` there is ASP.NET web application with single web form CurrencyMonitor.aspx
This page querries currency data from local DB on page load at server, then prepares them for display and pass them to client side as javascript object. On client side 
function 'displayCurrency' pass these data to jqPlot library.


### 3rd party libraries used
jqPlot javascript chart library
http://www.jqplot.com/
