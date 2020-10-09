function displayCurrency(CurrencyRates) {
    $(document).ready(function () {
        var plot1 = $.jqplot('usdchartdiv', [CurrencyRates[0]], {
            title: 'Currency rate USD',
            animate: true,
            animateReplot: true,
            axes: {
                xaxis: {
                    renderer: $.jqplot.DateAxisRenderer
                },
                yaxis: {}
            },
            highlighter: {
                show: true,
                sizeAdjust: 7.5
            },
            cursor: {
                show: false,
                showTooltip: true
            }
        });

        var plot2 = $.jqplot('gbpchartdiv', [CurrencyRates[1]], {
            title: 'Currency rate GBP',
            animate: true,
            animateReplot: true,
            axes: {
                xaxis: {
                    renderer: $.jqplot.DateAxisRenderer
                },
                yaxis: {}
            },
            highlighter: {
                show: true,
                sizeAdjust: 7.5
            },
            cursor: {
                show: false,
                showTooltip: true
            }
        });



    });
}