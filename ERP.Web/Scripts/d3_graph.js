var Dummy = {
    gender: {
        "data": [
            { 'value': '10', 'label': 'Male', 'color': '#F8F8F8' },
            { 'value': '10', 'label': 'Female', 'color': '#F8F8F8' }
        ],
        "tooltip": false
    },
    age: {
        "data": [
            { 'value': '10', 'label': '<20', 'color': '#F8F8F8', 'tooltip': false },
            { 'value': '10', 'label': '20 - 30', 'color': '#F8F8F8', 'tooltip': false },
            { 'value': '10', 'label': '30 - 40', 'color': '#F8F8F8', 'tooltip': false },
            { 'value': '10', 'label': 'Others', 'color': '#F8F8F8', 'tooltip': false }
        ],
        "tooltip": false
    },
    degree: {
        "data": [
            { 'value': '10', 'label': 'Masters', 'color': '#F8F8F8', 'tooltip': false },
            { 'value': '10', 'label': 'Bachelor/Honors', 'color': '#F8F8F8', 'tooltip': false },
            { 'value': '10', 'label': 'Diploma', 'color': '#F8F8F8', 'tooltip': false },
            { 'value': '10', 'label': 'Doctoral', 'color': '#F8F8F8', 'tooltip': false }
        ],
        "tooltip": false
    },
    university: {
        "data": [
            { 'value': '100', 'label': 'University 1' },
            { 'value': '80', 'label': 'University 2' },
            { 'value': '60', 'label': 'University 3' },
            { 'value': '40', 'label': 'University 4' },
            { 'value': '20', 'label': 'University 5' }
        ],
        "color": "#F8F8F8",
        "tooltip": false
    },
    map: [
        { 'hc-key': 'bd-da', 'tooltipTitle': 'Dhaka', 'legend': '', 'value': '0' }
        , { 'hc-key': 'bd-cg', 'tooltipTitle': 'Chittagong', 'legend': '', 'value': '0' }
        , { 'hc-key': 'bd-rj', 'tooltipTitle': 'Rajshahi', 'legend': '', 'value': '0' }
        , { 'hc-key': 'bd-kh', 'tooltipTitle': 'Khulna', 'legend': '', 'value': '0' }
        , { 'hc-key': 'bd-rp', 'tooltipTitle': 'Rangpur', 'legend': '', 'value': '0' }
        , { 'hc-key': 'bd-sy', 'tooltipTitle': 'Sylhet', 'legend': '', 'value': '0' }
        , { 'hc-key': 'bd-ba', 'tooltipTitle': 'Barisal', 'legend': '', 'value': '0' }
        , { 'hc-key': 'Outside of Bangladesh', 'tooltipTitle': 'Outside of Bangladesh', 'legend': 'Outside of Bangladesh', 'value': '0' }
    ]
}
$.fn.DrawBar = function (alldata, legend) {
    'use strict';
    var data = alldata;
    var isMobile = false;
    if (Object.keys(data).indexOf("deviceType") > -1) {
        if (data.deviceType.toLowerCase() == "mobile") {
            isMobile = true;
        }
    }
    var islegend = legend;
    var colored = data.colored ? true : false, originalData = data, tempData = [], hyScale, isTooltip = data.tooltip;
    if (colored) {
        data = data.data;
        data.reverse();
        $.each(data, function (i, d) {
            d.total = d.data.reduce(function (value, obj) {
                return value + parseFloat(obj.value);
            }, 0);
            d.data.reduce(function (value, obj) {
                obj.under = d.under
                , obj.original = obj.value
                , value += parseFloat(obj.value)
               , obj.percentage = (parseFloat(obj.value) * 100) / parseFloat(d.total)
                , obj.value = value;
                return value;
            }, 0);

            tempData = tempData.concat(d.data.sort(function (a, b) {
                return b.value - a.value;
            }));
        });
        var tdata = [];
        $.each(tempData, function (v, ob) {
            if (parseFloat(ob.original) > 0)
                tdata.push(ob);
        });
        data = tdata;
    }

    var id = $(this).attr("id"),
        margins = {
            top: colored ? 0 : 12,
            left: colored ? 50 : 150,
            right: colored ? islegend ? 150 : 20 : 50,
            bottom: colored ? 7 : 24
        };

    if (colored) {
        if (isMobile) {
            if (islegend)
                margins.right = 105;
        }
    }
    var width = colored ? $(this).width() - margins.right : $(this).width() - margins.left - margins.right
      , height = $(this).attr("height") - margins.top - margins.bottom
      , dataset = [{ data: colored ? data : data.data }];
    dataset = dataset.map(function (d) {
        return d.data.map(function (o, i) {
            return { y: parseInt(o.value), x: colored ? o.under : o.label, val: o.original };
        });
    });

    var stack = d3.layout.stack(dataset);
    stack(dataset);
    dataset = dataset.map(function (group) {
        return group.map(function (d) { return { x: d.y, y: d.x, x0: d.y0, val: d.original }; });
    });
    var isscroll = originalData.scroll;
    var originalWidth = width;
    width = isscroll ? (width * 5) : width;
    var svg = d3.select('#' + id)
        .append('svg')
        .attr("style", 'font-family: Arial,sans-serif;cursor: default;')
        .attr("xmlns", "http://www.w3.org/2000/svg")
        .attr("version", "1.1")
        .attr("id", id + "-hor-bar")
        .attr('width', function () {
            return colored ? width : width + margins.left + margins.right;
        })
        .attr('height', function () {
            return colored ? height + margins.top + margins.bottom + 15 : height + margins.top + margins.bottom;
        })
        .append('g')
        .attr('transform', 'translate(' + margins.left + ',' + margins.top + ')');
    var xMax = d3.max(dataset, function (group) {
        return d3.max(group, function (d) { return d.x + d.x0; });
    });
    var xScale = d3.scale.linear()
        .domain(colored ? [xMax + (xMax / height), 0] : [0, xMax + (xMax / width)])
        .range([0, colored ? height : width]);
    var labels = dataset[0].map(function (d) {
        return d.y;
    })
        , yScale = d3.scale.ordinal()
        .domain(labels)
        .rangeRoundBands([0, colored ? width : height], .6)
        , hScale = d3.scale.linear()
        .domain([0, xMax + (xMax / height)])
        .range([0, height]);
    var xAxis = d3.svg.axis()
        .scale(xScale)
        .orient(colored ? 'left' : 'bottom')
        , yAxis = d3.svg.axis()
        .scale(yScale)
        .orient('left');
    var groups = svg.selectAll('g')
        .data(dataset)
        .enter()
        .append('g')
        .attr("class", "rect-box");
    groups.selectAll('rect')
        .data(function (d) {
            return d;
        })
        .enter()
        .append('rect')
        .attr('x', function (d) {
            return colored ? yScale(d.y) : xScale(d.x0);
        })
        .attr('y', function (d, i) {
            return colored ? 0 : yScale(d.y);
        })
        .attr("stroke", function (d) {
            return isTooltip ? "" : "#DDDDDD";
        })
        .attr('height', function (d) {
            return colored ? hScale(d.x) : yScale.rangeBand();
        })
        .attr('width', function (d) {
            return colored ? yScale.rangeBand() : xScale(d.x);
        })
        .attr("box-text", function (d, i) {
            return d.x;
        })
        .attr('fill', function (d, i) {
            return colored ? data[i].color : data.color;
        })
        .on("mousemove", function (d, i) {
            if (isTooltip) {
                $("div.d3-tooltip").html("<span class='d3-tooltip-label'>" + (colored ? data[i].label : d.y) + "</span><br/><span class='d3-tooltip-value'>" + originalData.tooltipTitle + ": " + (colored ? islegend ? numberWithComma(data[i].original.toString()) + "(" + data[i].percentage.toFixed(1) + "%)" : numberWithComma(data[i].original.toString()) : numberWithComma(d.x.toString())) + "</span>");
                $("div.d3-tooltip")
                    .css("top", d3.event.pageY - 120)
                    .css("left", d3.event.pageX - 240)
                    .css("display", "block");
            }
        })
        .on("mouseout", function (d, i) {
            $("div.d3-tooltip").html("").css("display", "none");
        });
    if (colored) {
        hyScale = d3.scale.ordinal()
            .domain(labels)
            .rangeRoundBands([width, 0], .6)
        , yAxis = d3.svg.axis()
            .scale(hyScale)
            .orient('bottom');
    }
    svg.append('g')
            .attr('class', 'axis-x')
            .attr('transform', 'translate(0,' + height + ')')
            .call(colored ? yAxis : xAxis);
    svg.append('g')
        .attr('class', 'axis-y')
        .call(colored ? xAxis : yAxis);

    if (colored) {
        if (islegend) {
            var size = 14, font = "12px", leng = 12, rx = 20;
            if (isMobile) {
                originalWidth = originalWidth + 20;
                size = 9;
                font = "9px";
                leng = 8;
                rx = 12;
            }
            var boxes = d3.select("#" + id + " svg")
                    .append("svg:g")
                    .attr("transform", "translate(" + (originalWidth + 15) + ",70)");
            var dts = originalData.data[0].data;
            boxes.selectAll("rect")
                .data(dts)
                .enter()
                .append("rect")
                .attr("class", "color-box-rect")
                .attr("x", function (d, i) {
                    return 0;
                })
                .attr("y", function (d, i) {
                    return i * 25;
                })
                .attr("fill", function (d, i) {
                    return d.color;
                })
                .attr("rx", "2")
                .attr("height", size)
                .attr("width", size);
            boxes.selectAll("text")
                .data(dts)
                .enter()
                .append("text")
                .style("font-size", font)
                .attr("class", "color-box-text")
                .attr("x", function (d, i) {
                    return rx;
                })
                .attr("y", function (d, i) {
                    return i * 25 + leng;
                })
                .text(function (d, i) {
                    return d.label;
                });
            $("#" + id + " g>text.color-box-text").each(function (i, d) {
                var text = replaceAll(this.__data__.label, "&amp;", "&"), maximum = 20;
                if (isMobile)
                    maximum = 12;
                if (text.length > maximum) {
                    var ext = "", maxLength = text.length;
                    if (maxLength > maximum) {
                        maxLength = maximum * 2;
                        if (text.length > maximum * 2) {
                            maxLength -= 2;
                            ext = "...";
                        }
                    }
                    d3.select(d).text("");
                    var yy = parseInt($(this).attr("y"));
                    var tspan1 = d3.select(d)
                        .append("tspan")
                        .attr("x", $(this).attr("x"))
                        .attr("y", yy);
                    var txt1 = text.slice(0, maximum).trim().split(" ");
                    $.each(txt1, function (num, ob) {
                        tspan1.append("tspan").text(ob);
                        if (num < txt1.length - 1) {
                            tspan1.append("tspan").text(" ");
                        }
                    });
                    yy = parseInt($(this).attr("y")) + 15;
                    var tspan2 = d3.select(d)
                        .append("tspan")
                        .attr("x", $(this).attr("x"))
                        .attr("y", yy);
                    var txt2 = (text.slice(maximum, maxLength).trim() + ext).split(" ");
                    $.each(txt2, function (num, ob) {
                        tspan2.append("tspan").text(ob);
                        if (num < txt2.length - 1) {
                            tspan2.append("tspan").text(" ");
                        }
                    });
                    $("#" + id + " g>text.color-box-text").each(function (j, e) {
                        if (j > i) {
                            $(e).attr("y", parseInt($(this).attr("y")) + 10);
                        }
                    });
                    $("#" + id + " g>rect.color-box-rect").each(function (j, e) {
                        if (j > i) {
                            $(e).attr("y", parseInt($(this).attr("y")) + 10);
                        }
                    });
                } else {
                    d3.select(d).text("");
                    var tspan0 = d3.select(d)
                        .append("tspan");
                    var txt0 = text.trim().split(" ");
                    $.each(txt0, function (num, ob) {
                        tspan0.append("tspan").text(ob);
                        if (num < txt0.length - 1) {
                            tspan0.append("tspan").text(" ");
                        }
                    });
                }
            });
        }
        d3.select("#" + id + " svg")
            .attr("height", parseInt($("#" + id + " svg").attr("height")) + (islegend ? 50 : 0))
            .attr("width", parseInt($("#" + id + " svg").attr("width")) + margins.right);
        d3.select("#" + id + " svg>g")
            .attr("transform", "translate(" + margins.left + "," + (islegend ? 30 : 5) + ")");
    }
    $("[class^=axis] path, [class^=axis] line").css("fill", "none").css("stroke", "#999999").css("shape-rendering", "crispEdges");
    $("[class^=axis] text").css("font-size", "11px").css("fill", "#666666").css("font-weight", "bold");
    $(".axis-y text").css("font-size", "11px");
    var className = colored ? ".axis-x" : ".axis-y";
    var isExceed = false;
    $("#" + id + " " + className + " text").each(function (i, d) {
        var text = replaceAll(this.__data__, "&amp;", "&"), maximum = 20;
        //if (isMobile)
        //    maximum = 18;
        var maxText = maximum;
        if (text.length > maximum) {
            var ext = "", maxLength = text.length;
            if (maxLength > maximum) {
                maxLength = maximum * 2;
                if (text.length > maximum * 2) {
                    maxLength -= 2;
                    ext = "...";
                }
                if (text[maximum] != " ") {
                    if (text.slice(0, maximum).trim().indexOf(" ") == -1) {
                        maximum = text.slice(0, maximum).lastIndexOf("/") + 1;
                    } else {
                        maximum = text.slice(0, maximum).lastIndexOf(" ") + 1;
                    }
                }
                isExceed = true;
            }
            d3.select(d).text("");
            var yy = colored ? parseInt($(this).attr("y")) : (parseInt($(this).attr("y")) - 10);
            var tspan1 = d3.select(d)
                .append("tspan")
                .attr("x", $(this).attr("x"))
                .attr("y", yy);
            var txt1 = text.slice(0, maximum).trim().split(" ");
            $.each(txt1, function (num, ob) {
                tspan1.append("tspan").text(ob);
                if (num < txt1.length - 1) {
                    tspan1.append("tspan").text(" ");
                }
            });
            yy = colored ? (parseInt($(this).attr("y")) + 20) : (parseInt($(this).attr("y")) + 10);
            var tspan2 = d3.select(d)
                .append("tspan")
                .attr("x", $(this).attr("x"))
                .attr("y", yy);
            var txt2 = (text.slice(maximum, maximum + maxText).trim() + ext).split(" ");
            $.each(txt2, function (num, ob) {
                tspan2.append("tspan").text(ob);
                if (num < txt2.length - 1) {
                    tspan2.append("tspan").text(" ");
                }
            });
        } else {
            d3.select(d).text("");
            var tspan0 = d3.select(d)
                .append("tspan");
            var txt0 = text.trim().split(" ");
            $.each(txt0, function (num, ob) {
                tspan0.append("tspan").text(ob);
                if (num < txt0.length - 1) {
                    tspan0.append("tspan").text(" ");
                }
            });
        }
    });
    className = colored ? ".axis-y" : ".axis-x";
    $("#" + id + " " + className + " text").each(function (i, d) {
        var text = xMax.toString().length > 4 ? abbreviateNumber(parseInt(replaceAll(this.__data__, ",", ""))) : numberWithComma(this.__data__.toString());
        d3.select(d).text("");
        var tspan0 = d3.select(d)
            .append("tspan");
        var txt0 = text.toString().trim().split(" ");
        $.each(txt0, function (num, ob) {
            tspan0.append("tspan").text(ob);
            if (num < txt0.length - 1) {
                tspan0.append("tspan").text(" ");
            }
        });
        if ($("#" + id + " " + className + " text").length > 10) {
            if (i % 2 != 0) $(d).hide();
        }
    });
    if (colored)
        d3.select("g.rect-box").attr("transform", "rotate(180) translate(-" + width + ",-" + height + ")");
    else {
        if (isTooltip) {
            $("#" + id + " .rect-box rect").each(function (i, d) {
                d3.select("#" + id)
                    .select("svg")
                    .select(".rect-box")
                    .append("text")
                    .attr("style", 'fill: #666666;font-size:12px;')
                    .attr("class", "color-field-text")
                    .attr("id", id + "-rect-text-" + i)
                    .text(xMax.toString().length > 4 ? abbreviateNumber(parseInt(replaceAll($(d).attr("box-text"), ",", ""))) : numberWithComma($(d).attr("box-text")))
                    .attr("x", parseInt($(d).attr("x")) + parseInt($(d).attr("width")) + 5)
                    .attr("y", parseInt($(d).attr("y")) + 15);
            });
            if (xMax.toString().length > 4) {
                $("#" + id + " .color-field-text").each(function (i, d) {
                    var text = abbreviateNumber(this.__data__[i].x);
                    d3.select(d).text("");
                    var tspan0 = d3.select(d)
                        .append("tspan");
                    var txt0 = text.trim().split(" ");
                    $.each(txt0, function (num, ob) {
                        tspan0.append("tspan").text(ob);
                        if (num < txt0.length - 1) {
                            tspan0.append("tspan").text(" ");
                        }
                    });
                });
            }
        }
    }

    if (colored) {
        if (isMobile) {
            $("#" + id + " .axis-x text").each(function (i, d) {
                $(this).attr("transform", "rotate(-90)").css("text-anchor", "end");
                if ($(this).children().length > 0) {
                    $(this).children().each(function (k, l) {
                        var hhy;
                        if ($(l).parent().children().length > 1) {
                            hhy = typeof $(l).attr("y") === 'undefined' ? 0 : parseInt($(l).attr("y"));
                            hhy -= 21;
                        } else {
                            hhy = typeof $(l).attr("y") === 'undefined' ? 0 : parseInt($(l).attr("y"));
                            hhy -= 5;
                        }
                        $(l).attr("y", hhy).attr("x", "-10").css("font-size", "10px");
                        $(l).find("tspan").removeAttr("y");
                    });
                } else {
                    $(this).attr("y", parseInt($(this).attr("y")) - 15).attr("x", "-10").css("font-size", "10px");
                }
            });
            $("#" + id + " svg").attr("height", parseInt($("#" + id + " svg").attr("height")) + (isExceed ? 120 : 60));
            $("[class^=axis] path, [class^=axis] line").css("stroke-width", "2");
        }
    }
    if (!document.getElementsByClassName("d3-tooltip")[0])
        d3.select("body").append("div").attr("class", "d3-tooltip");
};

$.fn.DrawPie = function (alldata, donut) {
    'use strict';
    var data = alldata;
    var originalData = data, legendPosition = "top", isMobile = false;
    if (Object.keys(data).indexOf("legendPosition") > -1) {
        if (data.legendPosition.toLowerCase() == "right")
            legendPosition = "right";
    }
    if (Object.keys(data).indexOf("deviceType") > -1) {
        if (data.deviceType.toLowerCase() == "mobile") {
            isMobile = true;
        }
    }
    if (data.data) {
        var hasData = false;
        $.each(data.data, function (i, d) {
            hasData = parseInt(d.value) > 0 ? true : hasData;
        });
        if (!hasData) data = Dummy.age;
    }
    var r = $(this).attr("radius"),
        w = $(this).width(),
        h = $(this).attr("height"),
        id = $(this).attr("id"),
        isTooltip = data.tooltip;
    data = data.data;
    if (data.length == 5) {
        var top = data.slice(0, 3);
        top.push({ value: (parseInt(data[3].value) + parseInt(data[4].value)).toString(), label: 'Others', color: data[3].color });
        data = top;
    }
    data.total = data.reduce(function (value, d) {
        return value + parseInt(d.value);
    }, 0);
    var largestData = [], smallestData = [];
    $.each(data, function (i, d) {
        var index = 0, label = replaceAll(d.label, " ", ""), tempPer;
        if (["<20", "Male", "Diploma"].indexOf(label) > -1)
            index = 1;
        if (["20-30", "Female", "Bachelor/Honors"].indexOf(label) > -1)
            index = 2;
        if (["30-40", "Masters"].indexOf(label) > -1)
            index = 3;
        if (["40-50", "Doctoral"].indexOf(label) > -1)
            index = 4;
        if ([">50", "Others"].indexOf(label) > -1)
            index = 5;
        d.serial = index;
        tempPer = ((parseInt(d.value) * 100) / data.total).toFixed(1);
        d.percentage = tempPer.toString()[tempPer.toString().indexOf(".") + 1] == "0" ? Math.round(tempPer) : tempPer;
        if ((i + 1) <= Math.round(data.length / 2))
            largestData.push(d);
        else
            smallestData.push(d);
    });
    var isCont = true, ind = 0, ind1 = 0;
    while (isCont) {
        if (!largestData[ind] && !smallestData[ind])
            isCont = false;
        else {
            if (largestData[ind]) {
                data[ind1] = largestData[ind];
                ind1++;
            }
            if (smallestData[ind]) {
                data[ind1] = smallestData[ind];
                ind1++;
            }
            ind++;
        }
    }
    var vis = d3.select('#' + id)
        .append("svg:svg")
        .attr("style", 'font-family: Arial,sans-serif;font-size:12px;cursor: default;')
        .attr("xmlns", "http://www.w3.org/2000/svg")
        .attr("version", "1.1")
        .attr("class", "pie")
        .attr("overflow", "visible")
        .attr("width", w)
        .attr("height", h)
        .append("svg:g")
        .attr("class", "pie-container")
        .attr("transform", "translate(" + (w / 2) + "," + (parseInt(r) + 40) + ")");
    var pie = d3.layout.pie()
        .value(function (d) {
            return d.value;
        }).sort(null);
    var arc = d3.svg.arc()
        .innerRadius(function () {
            return donut ? r - Math.round(r / 3) - 7 : 0;
        })
        .outerRadius(r);
    var arcs = vis.selectAll("g.slice")
        .data(pie(data))
        .enter()
        .append("svg:g")
        .attr("class", "slice");
    arcs.append("svg:path")
        .attr("fill", function (d, i) {
            return d.data.color;
        })
        .attr("stroke", function (d) {
            return isTooltip ? "" : "#DDDDDD";
        })
        .attr("d", function (d) {
            return arc(d);
        })
        .attr("class", "color-divided-field")
        .on("mousemove", function (d, i) {
            if (isTooltip) {
                $("div.d3-tooltip")
                    .html("<span class='d3-tooltip-label'>" + d.data.label + "</span><br/><span class='d3-tooltip-value'>" + originalData.tooltipTitle + ": " + numberWithComma(d.data.value) + "</span>");
                $("div.d3-tooltip").css("top", d3.event.pageY).css("left", d3.event.pageX + 15).css("display", "block");
            }
        })
        .on("mouseout", function () {
            $("div.d3-tooltip").html('').css("display", "none");
        });
    if (isTooltip) {
        arcs.append("svg:text")
            .attr("transform", function (d) {
                d.innerRadius = 0, d.outerRadius = r;
                return "translate(" + arc.centroid(d) + ")";
            })
            .attr("text-anchor", "middle")
            .text(function (d, i) {
                return d.data.percentage == 0 ? "" : d.data.percentage + "%";
            })
            .attr("class", "color-field-text")
            .attr("style", "fill:#FFFFFF;font-size:12px;")
            .on("mousemove", function (d, i) {
                $("div.d3-tooltip")
                    .html("<span class='d3-tooltip-label'>" + d.data.label + "</span><br/><span class='d3-tooltip-value'>" + originalData.tooltipTitle + ": " + numberWithComma(d.data.value) + "</span>");
                $("div.d3-tooltip").css("top", d3.event.pageY).css("left", d3.event.pageX + 15).css("display", "block");
            })
            .on("mouseout", function () {
                $("div.d3-tooltip").html('').css("display", "none");
            });
    }
    var svg = d3.select('#' + id)
        .select("svg.pie");
    var cord = { x: 0, y: 12 };
    data.sort(function (a, b) { return a.serial - b.serial; });
    if (legendPosition == "right") {
        var boxes = d3.select("#" + id + " svg")
            .append("svg:g")
            .attr("transform", "translate(" + (w - 125) + ",70)");
        boxes.selectAll("rect")
            .data(data)
            .enter()
            .append("rect")
            .attr("class", "color-box-rect")
            .attr("x", function (d, i) {
                return 0;
            })
            .attr("y", function (d, i) {
                return i * 25;
            })
            .attr("fill", function (d, i) {
                return d.color;
            })
            .attr("rx", "2")
            .attr("height", "14")
            .attr("width", "14");
        boxes.selectAll("text")
            .data(data)
            .enter()
            .append("text")
            .style("font-size", "12px")
            .attr("class", "color-box-text")
            .attr("x", function (d, i) {
                return 20;
            })
            .attr("y", function (d, i) {
                return i * 25 + 12;
            })
            .text(function (d, i) {
                return d.label;
            });
        $("#" + id + " g>text.color-box-text").each(function (i, d) {
            var text = replaceAll(this.__data__.label, "&amp;", "&"), maximum = 20;
            if (text.length > maximum) {
                var ext = "", maxLength = text.length;
                if (maxLength > maximum) {
                    maxLength = maximum * 2;
                    if (text.length > maximum * 2) {
                        maxLength -= 2;
                        ext = "...";
                    }
                }
                d3.select(d).text("");
                var yy = parseInt($(this).attr("y"));
                var tspan1 = d3.select(d)
                    .append("tspan")
                    .attr("x", $(this).attr("x"))
                    .attr("y", yy);
                var txt1 = text.slice(0, maximum).trim().split(" ");
                $.each(txt1, function (num, ob) {
                    tspan1.append("tspan").text(ob);
                    if (num < txt1.length - 1) {
                        tspan1.append("tspan").text(" ");
                    }
                });
                yy = parseInt($(this).attr("y")) + 15;
                var tspan2 = d3.select(d)
                    .append("tspan")
                    .attr("x", $(this).attr("x"))
                    .attr("y", yy);
                var txt2 = (text.slice(maximum, maxLength).trim() + ext).split(" ");
                $.each(txt2, function (num, ob) {
                    tspan2.append("tspan").text(ob);
                    if (num < txt2.length - 1) {
                        tspan2.append("tspan").text(" ");
                    }
                });
                $("#" + id + " g>text.color-box-text").each(function (j, e) {
                    if (j > i) {
                        $(e).attr("y", parseInt($(this).attr("y")) + 10);
                    }
                });
                $("#" + id + " g>rect.color-box-rect").each(function (j, e) {
                    if (j > i) {
                        $(e).attr("y", parseInt($(this).attr("y")) + 10);
                    }
                });
            } else {
                d3.select(d).text("");
                var tspan0 = d3.select(d)
                    .append("tspan");
                var txt0 = text.trim().split(" ");
                $.each(txt0, function (num, ob) {
                    tspan0.append("tspan").text(ob);
                    if (num < txt0.length - 1) {
                        tspan0.append("tspan").text(" ");
                    }
                });
            }
        });
    } else {
        var he = 14, font = '12px', xx = 18;
        if (isMobile) {
            he = 10;
            font = '10px';
            cord.y = 9;
            xx = 12;
        }
        $.each(data, function (j, obj) {
            svg.append("svg:rect")
                .attr("x", cord.x)
                .attr("fill", obj.color)
                .attr("class", "small-color-box")
                .attr("stroke", function () {
                    return isTooltip ? "" : "#DDDDDD";
                })
                .attr({ rx: 2, ry: 2, height: he, width: he });
            cord.x += xx;
            if (obj.label.indexOf(" ") > -1) {
                var arr1 = obj.label.split(" "), str1 = "";
                var txt1 = svg.append("svg:text")
                    .attr("transform", "translate(" + cord.x + "," + cord.y + ")")
                    .attr("class", "color-box-text")
                    .style('font-size', font);
                $.each(arr1, function (num, ob) {
                    txt1.append("tspan").text(ob);
                    if (num < arr1.length - 1) {
                        txt1.append("tspan").text(" ");
                    }
                });
            } else if (obj.label.indexOf("-") > -1) {
                var arr = obj.label.split("-"), str = "";
                var txt = svg.append("svg:text")
                    .attr("transform", "translate(" + cord.x + "," + cord.y + ")")
                    .attr("class", "color-box-text")
                    .style('font-size', font);
                $.each(arr, function (num, ob) {
                    txt.append("tspan").text(ob);
                    if (num < arr.length - 1) {
                        txt.append("tspan").text("-");
                    }
                });
            } else {
                svg.append("svg:text")
                    .attr("transform", "translate(" + cord.x + "," + cord.y + ")")
                    .attr("class", "color-box-text")
                    .style('font-size', font)
                    .append("tspan")
                    .text(function (d, i) {
                        return obj.label;
                    });
            }
            var leng = 7;
            if (isMobile)
                leng = 5;
            cord.x += ((obj.label.length + 1) * leng);
        });
    }
    var allPaths = d3.selectAll("#" + id + " g.slice path"), allTexts = d3.selectAll("#" + id + " g.slice text");
    allPaths.order();
    allTexts.order();
    if (legendPosition == "right") {
        $("#" + id + " svg>g.pie-container").attr("transform", "translate(" + (w / 2) + "," + (parseInt(r) + 10) + ")");
        $("#" + id + " svg").attr("height", parseInt(h) - 40);
    }
    if (!document.getElementsByClassName("d3-tooltip")[0])
        d3.select("body").append("div").attr("class", "d3-tooltip");
};

$.fn.DrawGroupedVerticalBar = function (alldata, islegend) {
    'use strict';
    var data = alldata;
    var isLegend = islegend;
    var isMobile = false;
    if (Object.keys(data).indexOf("deviceType") > -1) {
        if (data.deviceType.toLowerCase() == "mobile") {
            isMobile = true;
        }
    }
    var originalData = data;
    var xMax = 0;
    data = data.data;
    var id = $(this).attr('id'), margin = { top: 20, right: 20, bottom: 30, left: 47 };
    var increase = 50;
    var width = $(this).width() - margin.left - margin.right - increase
        , height = $(this).attr("height") - margin.top - margin.bottom;
    if (isMobile)
        width = width + 45;
    $.each(data, function (i, d) {
        d.total = Object.keys(d).reduce(function (value, obj) {
            xMax = xMax < parseInt(d[obj]) ? parseInt(d[obj]) : xMax;
            return (obj != "under" && obj != "labels") ? value + parseInt(d[obj]) : value;
        }, 0);
    });
    var x0 = d3.scale.ordinal()
        .rangeRoundBands([0, width], .6)
        , x1 = d3.scale.ordinal()
        , y = d3.scale.linear().range([height, 0])
        , color = d3.scale.ordinal().range(["#1ca2d5", "#ed5d67", "#66BB6A"])
        , xAxis = d3.svg.axis().scale(x0).orient("bottom")
        , yAxis = d3.svg.axis().scale(y).orient("left");
    var svg = d3.select("#" + id)
        .append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
    var labelNames = d3.keys(data[0])
        .filter(function (key) {
            return key !== "under" && key !== "total" && key !== "labels";
        });
    data.forEach(function (d) {
        d.labels = labelNames.map(function (name) {
            return { name: name, value: +d[name], percentage: (+d[name] * 100) / (+d.total) };
        });
    });
    x0.domain(data.map(function (d) { return d.under; }));
    x1.domain(labelNames).rangeRoundBands([0, x0.rangeBand()]);
    y.domain([0, d3.max(data, function (d) {
        return d3.max(d.labels, function (o) { return o.value; });
    })]);
    svg.append("g")
        .attr("class", "axis-x")
        .attr("transform", "translate(0," + height + ")")
        .call(xAxis);
    svg.append("g")
        .attr("class", "axis-y")
        .call(yAxis);
        //.append("text")
        //.attr("transform", "rotate(-90)")
        //.attr("y", "-47")
        //.attr("x", "-80")
        //.attr("dy", ".71em")
        //.style("text-anchor", "end")
        //.text("Applicant");
    var under = svg.selectAll(".under")
        .data(data)
        .enter()
        .append("g")
        .attr("class", "under rect-box")
        .attr("transform", function (d) {
            return "translate(" + x0(d.under) + ",0)";
        });
    under.selectAll("rect")
        .data(function (d) {
            return d.labels;
        })
        .enter()
        .append("rect")
        .attr("width", x1.rangeBand())
        .attr("x", function (d) {
            return x1(d.name);
        })
        .attr("y", function (d) {
            return y(d.value);
        })
        .attr("height", function (d) {
            return height - y(d.value);
        })
        .style("fill", function (d) {
            return color(d.name);
        })
        .on("mousemove", function (d, i) {
            $("div.d3-tooltip")
                .html("<span class='d3-tooltip-under'>" + d.name + "</span><br/><span class='d3-tooltip-value'>" + originalData.tooltipTitle + ":" + d.value + "(" + d.percentage.toFixed(1) + "%)</span>");
            $("div.d3-tooltip")
                .css("top", d3.event.pageY)
                .css("left", d3.event.pageX + 15)
                .css("display", "block");
        })
        .on("mouseout", function (d, i) {
            $("div.d3-tooltip").html("").css("display", "none");
        });
    var size = 14, font = "12px", leng = 8, rx = 30;
    if (isMobile) {
        size = 9;
        font = "9px";
        leng = 5;
        rx = 38;
    }
    var legend = svg.selectAll(".legend")
        .data(labelNames.slice().reverse())
        .enter()
        .append("g")
        .attr("class", "legend")
        .attr("transform", function (d, i) {
            return "translate(32," + (i * 20 + 40) + ")";
        });
    legend.append("rect")
        .attr("x", width - 50)
        .attr("width", size)
        .attr("height", size)
        .style("fill", color)
        .attr("rx", "2");
    legend.append("text")
        .attr("class", "color-box-text")
        .attr("x", width - rx)
        .attr("y", leng)
        .attr("dy", ".35em")
        .style("text-anchor", "start")
        .style("font-size", font)
        .text(function (d) {
            return d;
        });
    d3.select("#" + id + " svg")
        .attr("width", parseInt($("#" + id + " svg").attr("width")) + increase);
    //}
    var isExceed = false;
    $("#" + id + " .axis-x text").each(function (i, d) {
        var text = replaceAll(this.__data__, "&amp;", "&"), maximum = 20;
        if (isMobile)
            maximum = 18;
        if (text.length > maximum) {
            var ext = ""
                , maxLength = text.length;
            if (maxLength > maximum) {
                maxLength = maximum * 2;
                if (text.length > maximum * 2) {
                    maxLength -= 2;
                    ext = "...";
                }
            }
            d3.select(d).text("");
            d3.select(d)
                .append("tspan")
                .text(text.slice(0, maximum).trim())
                .attr("x", $(this).attr("x"))
                .attr("y", parseInt($(this).attr("y")));
            d3.select(d)
                .append("tspan")
                .text(text.slice(maximum, maxLength).trim() + ext)
                .attr("x", $(this).attr("x"))
                .attr("y", parseInt($(this).attr("y")) + 20);
            isExceed = true;
        }
    });
    $("#" + id + " .axis-y text").each(function (i, d) {
        //if (i < $("#" + id + " .axis-y text").length - 1) {
            var text = xMax.toString().length > 4 ? abbreviateNumber(parseInt(replaceAll(this.__data__, ",", ""))) : numberWithComma(this.__data__.toString());
            d3.select(d).text("");
            var tspan0 = d3.select(d)
                .append("tspan");
            var txt0 = text.toString().trim().split(" ");
            $.each(txt0, function (num, ob) {
                tspan0.append("tspan").text(ob);
                if (num < txt0.length - 1) {
                    tspan0.append("tspan").text(" ");
                }
            });
            if ($("#" + id + " .axis-y text").length > 10) {
                if (i % 2 != 0) $(d).hide();
            }
        //}
    });
    if (isMobile) {
        $("#" + id + " .axis-x text").each(function (i, d) {
            $(this).attr("transform", "rotate(270)").css("text-anchor", "end");
            if ($(this).find("tspan").length > 0) {
                $(this).find("tspan").each(function (k, l) {
                    $(l).attr("y", parseInt($(l).attr("y")) - 15).attr("x", "-10");
                });
            } else {
                $(this).attr("y", parseInt($(this).attr("y")) - 15).attr("x", "-10");
            }
        });
        $("#" + id + " svg").attr("height", parseInt($("#" + id + " svg").attr("height")) + (isExceed ? 120 : 60));
        $("[class^=axis] path, [class^=axis] line").css("stroke-width", "2");
    }
    $("[class^=axis] path, [class^=axis] line").css("fill", "none").css("stroke", "#999999").css("shape-rendering", "crispEdges");
    $("[class^=axis] text").css("font-size", "11px").css("fill", "#666666").css("font-weight", "bold");
    $(".axis-y text").css("font-size", "11px");
    if (!document.getElementsByClassName("d3-tooltip")[0])
        d3.select("body").append("div").attr("class", "d3-tooltip");
};

$.fn.DrawMap = function (data) {
    var color = "#1ca2d5", isDummy = false;
    var id = $(this).attr("id");
    if (data.length == 0) {
        data = Dummy.map;
        color = "#F8F8F8";
        isDummy = true;
    }
    if (isDummy == false) {
        data = data.sort(function (a, b) { return b.value - a.value; });
        var total = data.reduce(function (value, obj) {
            return value + parseInt(obj.value);
        }, 0);
        var indx = -1;
        $.each(data, function (index, d) {
            if (d.legend.toString() == "Outside of Bangladesh") {
                indx = index;
            }
            d.value = parseInt(d.value);
            d.label = numberWithComma(d.value.toString());
            d.color = color;
            d.opacity = index != 0 ? ((d.value) / data[1].value).toFixed(2) : 1;
            d.percentage = ((d.value / total) * 100).toFixed(1);
            d.percentage = d.percentage.toString()[d.percentage.toString().indexOf(".") + 1] == "0" ? Math.round(d.percentage) : d.percentage;
            if (d.percentage != 0) d.percentage += "%";
        });
        if (indx != -1) {
            var abroad = data[indx];
            data.splice(indx, 1);
            data.push(abroad);
        }

        $("#" + id).highcharts('Map', {
            series: [
                {
                    data: data,
                    mapData: Highcharts.maps['countries/bd/bd-all'],
                    joinBy: 'hc-key',
                    name: 'Applicant',
                    dataLabels: {
                        enabled: true,
                        format: '{point.legend}<br />{point.percentage}'
                    },
                    tooltip: {
                        pointFormat: '{point.tooltipTitle}: {point.label}'
                    }
                }
            ]
        });
        var points = $('#' + id).highcharts().series[0].data;
        for (var i = 0; i < points.length; i++) {
            points[i].pointAttr.hover.fill = points[i].color;
        }
        $("#" + id + " .highcharts-series path").each(function (index, d) {
            $(this).css("opacity", index == 1 ? points[index].opacity - 0.25 : points[index].opacity);
        });
        $("#" + id + " .highcharts-data-labels g").each(function (index, d) {
            $(this).find("text").css("color", "#666666").css("fill", "#666666").css('text-shadow', '').css("font-size", "12px").css("font-weight", "500").css("opacity", "1");
            var width = $("#" + id).width();
            if (index == 7) {
                $(this).find("text").find("tspan").attr("x", width - 100).attr("dy", "14");
                $(this).find("text").find("tspan").next().attr("x", width - 38).attr("text-anchor", "middle");
            } else {
                $(this).find("text").find("tspan").attr("x", "15").attr("text-anchor", "middle").attr("dy", "5");
            }

        });
        $("#" + id + " div").css("margin-top", "-10px");
        $("#" + id + " .highcharts-title,#" + id + " .highcharts-button,#" + id + " .highcharts-grid").remove();
        $("#" + id + " .highcharts-text-shadow").remove();
    } else {
        $.each(data, function (index, d) {
            d.color = color;
        });
        $("#" + id).highcharts('Map', {
            series: [
                {
                    data: data,
                    mapData: Highcharts.maps['countries/bd/bd-all'],
                    joinBy: 'hc-key',
                    name: '',
                    dataLabels: {
                        enabled: false
                    }
                }
            ]
        });
        var p = $('#' + id).highcharts().series[0].data;
        for (var l = 0; l < p.length; l++) {
            p[l].pointAttr.hover.fill = p[l].color;
        }
        $("#" + id + " div").css("margin-top", "-10px");
        $("#" + id + " .highcharts-title,#" + id + " .highcharts-button,#" + id + " .highcharts-grid,#" + id + " .highcharts-tooltip").remove();
        $("#" + id + " .highcharts-text-shadow").remove();
    }
};
function isnumeric(x) {
    return !isNaN(x);
}
function replaceAll(str, find, re) {
    return str.toString().replace(new RegExp(find, 'g'), re);
}
function numberWithComma(intConvertNumber) {
    var convertNumber = "";
    if (isnumeric(intConvertNumber)) {
        intConvertNumber = replaceAll(intConvertNumber, ",", "");
        var dotVal = "";
        if (intConvertNumber.toString().indexOf(".") > -1) {
            var dotFound = intConvertNumber.toString().indexOf(".");
            var intval = intConvertNumber.toString().slice(0, dotFound - 1);
            dotVal = intConvertNumber.toString().slice(dotFound + 1);
            intConvertNumber = intval;
        }
        if (intConvertNumber.length > 3) {
            var convertKilo = intConvertNumber.slice(intConvertNumber.length - 3);
            var lenNumber = intConvertNumber.length;
            var gtCconvertKilo = intConvertNumber.slice(0, lenNumber - 3);
            for (; ;) {
                if (gtCconvertKilo.length >= 2) {
                    var separateNumber = gtCconvertKilo.slice(gtCconvertKilo.length - 2);
                    convertNumber = separateNumber + "," + convertNumber;
                    gtCconvertKilo = intConvertNumber.slice(0, gtCconvertKilo.length - 2);
                } else {
                    if (gtCconvertKilo != "")
                        convertNumber = gtCconvertKilo + "," + convertNumber;
                    break;
                }
            }
            convertNumber = convertNumber + convertKilo + dotVal;
        } else
            convertNumber = intConvertNumber + dotVal;
        return convertNumber;
    }
    return intConvertNumber;
}

function abbreviateNumber(value) {
    var newValue = value;
    if (isnumeric(value) && value > 0) {
        var shortVal = (value / 1000).toFixed(1);
        newValue = (shortVal.toString()[shortVal.toString().indexOf(".") + 1] == "0" ? Math.round(shortVal) : shortVal) + " k";
    }
    return newValue;
}
