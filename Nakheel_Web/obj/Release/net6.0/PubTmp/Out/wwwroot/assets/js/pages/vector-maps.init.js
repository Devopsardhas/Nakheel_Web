var worldlinemap = new jsVectorMap({
    map: "world_merc",
    selector: "#world-map-line-markers",
    zoomOnScroll: !1,
    zoomButtons: !1,
    selectedMarkers: [0, 2, 4],
    markers: [{
        name: "Greenland",
        coords: [72, -42]
    }, {
        name: "Canada",
        coords: [56.1304, -106.3468]
    }, {
        name: "Brazil",
        coords: [-14.235, -51.9253]
    },{
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
        },
        {
            name: "India",
            coords: [20.5937, 78.9629]
        }    ],
    lines: [{
        from: "Canada",
        to: "India"
    }, {
        from: "Russia",
        to: "India"
    }, {
        from: "Greenland",
        to: "India"
    }, {
        from: "Brazil",
        to: "India"
    }, {
        from: "United States",
        to: "India"
    }, {
        from: "China",
        to: "India"
    }, {
        from: "Norway",
        to: "India"
    }, {
        from: "Ukraine",
        to: "India"
        }, {
        from: "India",
            to: "Egypt"
        }],
    lineStyle: {
        animation: !0,
        strokeDasharray: "6 3 6"
    },
    markerStyle: {
        initial: {
            fill: "green"
        },
        selected: {
            fill: "red"
        },
    },
    labels: {
        markers: {
            render: function (e) {
                return e.name
            }
        }
    }

}),
    worldemapmarkers = new jsVectorMap({
        map: "world_merc",
        selector: "#world-map-markers",
        zoomOnScroll: !1,
        zoomButtons: !1,
        selectedMarkers: [0, 2],
        markersSelectable: !0,
        markers: [{
            name: "Palestine",
            coords: [31.9474, 35.2272]
        }, {
            name: "Russia",
            coords: [61.524, 105.3188]
        }, {
            name: "Canada",
            coords: [56.1304, -106.3468]
        }, {
            name: "Greenland",
            coords: [71.7069, -42.6043]
        }],
        markerStyle: {
            initial: {
                fill: "#00FF00"
            },
            selected: {
                fill: "red"
            }
        },
        labels: {
            markers: {
                render: function (e) {
                    return e.name
                }
            }
        }
    }),
    worldemapmarkersimage = new jsVectorMap({
        map: "world_merc",
        selector: "#world-map-markers-image",
        zoomOnScroll: !1,
        zoomButtons: !1,
        selectedMarkers: [0, 2],
        markersSelectable: !0,
        markers: [{
            name: "Palestine",
            coords: [31.9474, 35.2272]
        }, {
            name: "Russia",
            coords: [61.524, 105.3188]
        }, {
            name: "Canada",
            coords: [56.1304, -106.3468]
        }, {
            name: "Greenland",
            coords: [71.7069, -42.6043]
        }],
        markerStyle: {
            initial: {
                image: "assets/images/logo-dark-sm.png"
            }
        },
        labels: {
            markers: {
                render: function (e) {
                    return e.name
                }
            }
        }
    }),
    usmap = new jsVectorMap({
        map: "us_merc_en",
        selector: "#usa-vectormap",
        zoomOnScroll: !1,
        zoomButtons: !1
    }),
    canadamap = new jsVectorMap({
        map: "canada",
        selector: "#canada-vectormap",
        zoomOnScroll: !1,
        zoomButtons: !1
    });