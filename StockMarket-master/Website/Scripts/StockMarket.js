﻿/// <reference path="../scripts/jquery-1.6.4.js" />
/// <reference path="../scripts/jquery.signalr.js" />

/*!
    SignalR Stock Ticker Sample
*/

// Crockford's supplant method (poor man's templating)
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

// A simple background color flash effect that uses jQuery Color plugin
jQuery.fn.flash = function (color, duration) {
    var current = this.css('backgroundColor');
    this.animate({ backgroundColor: 'rgb(' + color + ')' }, duration / 2)
        .animate({ backgroundColor: current }, duration / 2);
}

$(function () {

    var ticker = $.connection.stockTicker, // the generated client-side hub proxy
        up = '▲',
        down = '▼',
        $stockTable = $('#stockTable'),
        $stockTableBody = $stockTable.find('tbody'),
        rowTemplate = '<tr data-symbol="{Symbol}"><td>{Symbol}</td><td>{Price}</td><td>{DayOpen}</td><td>{DayHigh}</td><td>{DayLow}</td><td><span class="dir {DirectionClass}">{Direction}</span> {Change}</td><td>{PercentChange}</td></tr>',
        $stockTicker = $('#stockTicker'),
        $stockTickerUl = $stockTicker.find('ul'),
        liTemplate = '<li data-symbol="{Symbol}"><span class="symbol">{Symbol}</span> <span class="price">{Price}</span> <span class="change"><span class="dir {DirectionClass}">{Direction}</span> {Change} ({PercentChange})</span></li>';

    function formatStock(stock) {
        return $.extend(stock, {
            Price: stock.Price.toFixed(2),
            PercentChange: (stock.PercentChange * 100).toFixed(2) + '%',
            Direction: stock.Change === 0 ? '' : stock.Change >= 0 ? up : down,
            DirectionClass: stock.Change === 0 ? 'even' : stock.Change >= 0 ? 'up' : 'down'
        });
    }

    function scrollTicker() {
        var w = $stockTickerUl.width();
        $stockTickerUl.css({ marginLeft: w });
        $stockTickerUl.animate({ marginLeft: -w }, 15000, 'linear', scrollTicker);
    }

    function stopTicker() {
        $stockTickerUl.stop();
    }

    function init() {
        return ticker.getAllStocks()
            .done(function (stocks) {
                $stockTableBody.empty();
                $stockTickerUl.empty();
                $.each(stocks, function () {
                    var stock = formatStock(this);
                    $stockTableBody.append(rowTemplate.supplant(stock));
                    $stockTickerUl.append(liTemplate.supplant(stock));
                });
            });
    }

    // Add client-side hub methods that the server will call
    ticker.updateStockPrice = function (stock) {
        var displayStock = formatStock(stock),
            $row = $(rowTemplate.supplant(displayStock)),
            $li = $(liTemplate.supplant(displayStock)),
            bg = stock.LastChange === 0
                ? '255,216,0' // yellow
                : stock.LastChange > 0
                    ? '154,240,117' // green
                    : '255,148,148'; // red

        $stockTableBody.find('tr[data-symbol=' + stock.Symbol + ']')
            .replaceWith($row);
        $stockTickerUl.find('li[data-symbol=' + stock.Symbol + ']')
            .replaceWith($li);

        $row.flash(bg, 1000);
        $li.flash(bg, 1000);
    };

    ticker.addStock = function(stock) {
        $stockTableBody.append('<tr data-symbol=' + stock.Symbol + '/>');
        $stockTickerUl.append('<li data-symbol=' + stock.Symbol + '/>');

        ticker.updateStockPrice(stock);
    };

    ticker.updateMarketState = function (state) {
        if (state === 'Closed')
            stopTicker();
        else
            scrollTicker();

        if (state == 'Reset') {
            ticker.marketReset();
            return;
        }

        $("#market-status").attr('class', state);
        $("#market-status").attr('title', 'Market is '+state);
    };

    ticker.marketReset = function () {
        init();
    };

    // Start the connection
    $.connection.hub.start(function () {
        init().done(function () {
            ticker.getMarketState()
                .done(ticker.updateMarketState);
        });
    });

    // Wire up the buttons
    $("#open").click(function () {
        ticker.openMarket();
    });

    $("#closed").click(function () {
        ticker.closeMarket();
    });

    $("#reset").click(function () {
        ticker.reset();
    });
});

function priceUpdateComplete(data) {
    alert("Updated " + data.Symbol + " price to " + data.Price);
}
function priceUpdateFailed() {
    alert("Price update failed");
}
