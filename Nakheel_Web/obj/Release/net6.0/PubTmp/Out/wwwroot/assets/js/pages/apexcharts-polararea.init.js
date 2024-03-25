function getChartColorsArray(e) {
    if (null !== document.getElementById(e)) {
        var r = document.getElementById(e).getAttribute("data-colors");
        return (r = JSON.parse(r)).map(function (e) {
            var r = e.replace(" ", "");
            if (-1 == r.indexOf("--")) return r;
            var o = getComputedStyle(document.documentElement).getPropertyValue(r);
            return o || void 0
        })
    }
}
var options = {
    series: [4, 0, 26, 7, 5],
    chart: {
        type: "polarArea",
        width: 300
    },
    labels: ["Fire Incidents", "Property damage", "Near miss", "First aid", "Vehicle Accident"],
    stroke: {
        colors: ["#fff"]
    },
    fill: {
        opacity: .8
    },
    legend: {
        position: "bottom"
    },
    colors: barchartColors = getChartColorsArray("basic_polar_area")
},
    chart = new ApexCharts(document.querySelector("#basic_polar_area"), options);
chart.render();
var barchartColors = getChartColorsArray("monochrome_polar_area"),
    options = {
        series: [42, 47, 52, 58, 65],
        chart: {
            width: 400,
            type: "polarArea"
        },
        labels: ["Rose A", "Rose B", "Rose C", "Rose D", "Rose E"],
        fill: {
            opacity: 1
        },
        stroke: {
            width: 1,
            colors: void 0
        },
        yaxis: {
            show: !1
        },
        legend: {
            position: "bottom"
        },
        plotOptions: {
            polarArea: {
                rings: {
                    strokeWidth: 0
                },
                spokes: {
                    strokeWidth: 0
                }
            }
        },
        theme: {
            mode: "light",
            palette: "palette1",
            monochrome: {
                enabled: !0,
                shadeTo: "light",
                color: "#038edc",
                shadeIntensity: .6
            }
        }
    };
(chart = new ApexCharts(document.querySelector("#monochrome_polar_area"), options)).render();