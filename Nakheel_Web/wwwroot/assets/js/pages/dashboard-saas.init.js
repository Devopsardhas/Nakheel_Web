function getChartColorsArray(e) {
    if (null !== document.getElementById(e)) {
        var r = document.getElementById(e).getAttribute("data-colors");
        return (r = JSON.parse(r)).map(function (e) {
            var r = e.replace(" ", "");
            if (-1 == r.indexOf("--")) return r;
            var t = getComputedStyle(document.documentElement).getPropertyValue(r);
            return t || void 0
        })
    }
}
var barchartColors = getChartColorsArray("chart-sparkline1"),
    sparklineoptions1 = {
        series: [{
            data: [12, 14, 2, 47, 42, 15, 47, 75, 65, 19, 14]
        }],
        chart: {
            type: "area",
            width: 85,
            height: 30,
            sparkline: {
                enabled: !0
            }
        },
        fill: {
            type: "gradient",
            gradient: {
                shadeIntensity: 1,
                inverseColors: !1,
                opacityFrom: .45,
                opacityTo: .05,
                stops: [20, 100, 100, 100]
            }
        },
        stroke: {
            curve: "smooth",
            width: 2
        },
        colors: barchartColors,
        tooltip: {
            fixed: {
                enabled: !1
            },
            x: {
                show: !1
            },
            y: {
                title: {
                    formatter: function (e) {
                        return ""
                    }
                }
            },
            marker: {
                show: !1
            }
        }
    },
    sparklinechart1 = new ApexCharts(document.querySelector("#chart-sparkline1"), sparklineoptions1);
sparklinechart1.render();
var sparklineoptions2 = {
    series: [{
        data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
    }],
    chart: {
        type: "area",
        width: 85,
        height: 30,
        sparkline: {
            enabled: !0
        }
    },
    fill: {
        type: "gradient",
        gradient: {
            shadeIntensity: 1,
            inverseColors: !1,
            opacityFrom: .45,
            opacityTo: .05,
            stops: [20, 100, 100, 100]
        }
    },
    stroke: {
        curve: "smooth",
        width: 2
    },
    colors: barchartColors = getChartColorsArray("chart-sparkline2"),
    tooltip: {
        fixed: {
            enabled: !1
        },
        x: {
            show: !1
        },
        y: {
            title: {
                formatter: function (e) {
                    return ""
                }
            }
        },
        marker: {
            show: !1
        }
    }
},
    sparklinechart2 = new ApexCharts(document.querySelector("#chart-sparkline2"), sparklineoptions2);
sparklinechart2.render();
var sparklineoptions3 = {
    series: [{
        data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
    }],
    chart: {
        type: "area",
        width: 85,
        height: 30,
        sparkline: {
            enabled: !0
        }
    },
    fill: {
        type: "gradient",
        gradient: {
            shadeIntensity: 1,
            inverseColors: !1,
            opacityFrom: .45,
            opacityTo: .05,
            stops: [20, 100, 100, 100]
        }
    },
    stroke: {
        curve: "smooth",
        width: 2
    },
    colors: barchartColors = getChartColorsArray("chart-sparkline3"),
    tooltip: {
        fixed: {
            enabled: !1
        },
        x: {
            show: !1
        },
        y: {
            title: {
                formatter: function (e) {
                    return ""
                }
            }
        },
        marker: {
            show: !1
        }
    }
},
    sparklinechart3 = new ApexCharts(document.querySelector("#chart-sparkline3"), sparklineoptions3);
sparklinechart3.render();
var sparklineoptions4 = {
    series: [{
        data: [12, 14, 2, 47, 42, 15, 47, 75, 65, 19, 14]
    }],
    chart: {
        type: "area",
        width: 85,
        height: 30,
        sparkline: {
            enabled: !0
        }
    },
    fill: {
        type: "gradient",
        gradient: {
            shadeIntensity: 1,
            inverseColors: !1,
            opacityFrom: .45,
            opacityTo: .05,
            stops: [20, 100, 100, 100]
        }
    },
    stroke: {
        curve: "smooth",
        width: 2
    },
    colors: barchartColors = getChartColorsArray("chart-sparkline4"),
    tooltip: {
        fixed: {
            enabled: !1
        },
        x: {
            show: !1
        },
        y: {
            title: {
                formatter: function (e) {
                    return ""
                }
            }
        },
        marker: {
            show: !1
        }
    }
},
    sparklinechart4 = new ApexCharts(document.querySelector("#chart-sparkline4"), sparklineoptions4);
sparklinechart4.render();
var map = new jsVectorMap({
    map: "world_merc",
    selector: "#world-map-markers",
    zoomOnScroll: !1,
    zoomButtons: !1,
    regionStyle: {
        initial: {
            fill: "#fff",
            fillOpacity: .1
        },
        hover: {
            fillOpacity: .2
        }
    },
    markerStyle: {
        initial: {
            fill: "#f56e6e",
            fillOpacity: 1
        },
        hover: {
            fill: "#f56e6e",
            fillOpacity: .8
        }
    },
    markers: [{
        name: "Greenland",
        coords: [72, -42]
    }, {
        name: "Canada",
        coords: [56.1304, -106.3468]
    }, {
        name: "Brazil",
        coords: [-14.235, -51.9253]
    }, {
        name: "Egypt",
        coords: [26.8206, 30.8025]
    }, {
        name: "Russia",
        coords: [61, 105]
    }, {
        name: "China",
        coords: [35.8617, 104.1954]
    }, {
        name: "United States",
        coords: [37.0902, -95.7129]
    }, {
        name: "Norway",
        coords: [60.472024, 8.468946]
    }, {
        name: "Ukraine",
        coords: [48.379433, 31.16558]
    }],
    lines: [{
        from: "Canada",
        to: "Egypt"
    }, {
        from: "Russia",
        to: "Egypt"
    }, {
        from: "Greenland",
        to: "Egypt"
    }, {
        from: "Brazil",
        to: "Egypt"
    }, {
        from: "United States",
        to: "Egypt"
    }, {
        from: "China",
        to: "Egypt"
    }, {
        from: "Norway",
        to: "Egypt"
    }, {
        from: "Ukraine",
        to: "Egypt"
    }],
    lineStyle: {
        stroke: "#fff",
        animation: !0,
        strokeDasharray: "6 3 6"
    }
}),
    swiper = new Swiper(".swiper-location-widget", {
        direction: "vertical",
        spaceBetween: 24,
        slidesPerView: 3,
        loop: !0,
        autoplay: {
            delay: 2e3,
            disableOnInteraction: !1
        }
    }),
    options = {
        series: [{
            name: "Completed Trips",
            type: "area",
            data: [10, 20, 30, 50, 10, 20, 80, 20, 10]
        }, {
            name: "Cancelled Trips",
            data: [2, 5, 3, 8, 4, 6, 3, 7, 10],
            type: "line"
        }],
        chart: {
            type: "line",
            height: 350,
            toolbar: {
                show: !1
            }
        },
        dataLabels: {
            enabled: !1
        },
        stroke: {
            show: !0,
            curve: "smooth",
            width: [0, 4]
        },
        forecastDataPoints: {
            count: 7
        },
        colors: barchartColors = getChartColorsArray("chart-area"),
        xaxis: {
            categories: ["Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct"]
        },
        yaxis: {
            labels: {
                formatter: function (e) {
                    return e 
                }
            },
            tickAmount: 4
        },
        legend: {
            show: !1
        },
        fill: {
            type: "gradient",
            gradient: {
                shade: "light",
                gradientToColors: [barchartColors[2], barchartColors[3]],
                shadeIntensity: 1,
                type: "horizontal",
                opacityFrom: [.75, 1],
                opacityTo: [.75, 1],
                stops: [0, 100, 100, 100]
            }
        },
        markers: {
            size: 3,
            strokeWidth: 3,
            hover: {
                size: 6
            }
        },
        grid: {
            show: !0,
            xaxis: {
                lines: {
                    show: !0
                }
            },
            yaxis: {
                lines: {
                    show: !1
                }
            }
        }
    },
    chart = new ApexCharts(document.querySelector("#chart-area"), options);
chart.render();

options = {
    series: [44, 55, 67],
    chart: {
        height: 323,
        type: "radialBar"
    },
    plotOptions: {
        radialBar: {
            offsetY: 0,
            startAngle: 0,
            endAngle: 270,
            dataLabels: {
                name: {
                    show: !1
                },
                value: {
                    show: !1
                }
            },
            hollow: {
                margin: 7,
                size: "20%"
            },
            track: {
                strokeWidth: "60%",
                opacity: 1,
                margin: 16
            }
        }
    },
    fill: {
        type: "gradient",
        gradient: {
            shade: "light",
            type: "horizontal",
            shadeIntensity: .5,
            inverseColors: !0,
            opacityFrom: 1,
            opacityTo: 1,
            stops: [0, 100]
        }
    },
    stroke: {
        lineCap: "round"
    },
    colors: barchartColors = getChartColorsArray("chart-radialBar"),
    labels: ["Cash", "Card", "Wallet"],
    legend: {
        show: !0,
        floating: !0,
        fontSize: "16px",
        position: "left",
        offsetX: -24,
        offsetY: 15,
        labels: {
            useSeriesColors: !0
        },
        markers: {
            size: 0
        },
        formatter: function (e, r) {
            return e + ":  " + r.w.globals.series[r.seriesIndex]
        },
        itemMargin: {
            vertical: 3
        }
    },
    responsive: [{
        breakpoint: 480,
        options: {
            legend: {
                show: !1
            }
        }
    }]
};
(chart = new ApexCharts(document.querySelector("#chart-radialBar"), options)).render();