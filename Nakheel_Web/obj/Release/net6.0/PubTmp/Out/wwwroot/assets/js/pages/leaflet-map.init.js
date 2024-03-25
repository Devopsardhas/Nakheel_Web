


var mymap = L.map("leaflet-map").setView([11.1271, 78.6569], 13);
L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw", {
    maxZoom: 18,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    id: "mapbox/streets-v11",
    tileSize: 512,
    zoomOffset: -1
}).addTo(mymap);
var markermap = L.map("leaflet-map-marker").setView([11.1271, 78.6569], 13);

L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw", {
    maxZoom: 18,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    id: "mapbox/streets-v11",
    tileSize: 512,
    zoomOffset: -1
}).addTo(markermap), L.marker([51.5, -.09]).addTo(markermap), L.circle([51.508, -.11], {
    color: "#82D173",
    fillColor: "#82D173",
    fillOpacity: .5,
    radius: 500
}).addTo(markermap), L.polygon([
    [51.509, -.08],
    [51.503, -.06],
    [51.51, -.047]
], {
    color: "#3b76e1",
    fillColor: "#3b76e1"
}).addTo(markermap);
var popupmap = L.map("leaflet-map-popup").setView([11.1271, 78.6569], 13);
L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw", {
    maxZoom: 18,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    id: "mapbox/streets-v11",
    tileSize: 512,
    zoomOffset: -1
}).addTo(popupmap), L.marker([51.5, -.09]).addTo(popupmap).bindPopup("<b>Hello world!</b><br />I am a popup.").openPopup(), L.circle([51.508, -.11], 500, {
    color: "#E54B4B",
    fillColor: "#E54B4B",
    fillOpacity: .5
}).addTo(popupmap).bindPopup("I am a circle."), L.polygon([
    [51.509, -.08],
    [51.503, -.06],
    [51.51, -.047]
], {
    color: "#3b76e1",
    fillColor: "#3b76e1"
}).addTo(popupmap).bindPopup("I am a polygon.");
var popup = L.popup(),
    customiconsmap = L.map("leaflet-map-custom-icons").setView([11.1271, 78.6569], 13);
L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(customiconsmap);
var LeafIcon = L.Icon.extend({
    options: {
        iconSize: [45, 95],
        iconAnchor: [22, 94],
        popupAnchor: [-3, -76]
    }
}),
    greenIcon = new LeafIcon({
        iconUrl: "assets/images/logo-light-sm.png"
    });
L.marker([51.5, -.09], {
    icon: greenIcon
}).addTo(customiconsmap);
var interactivemap = L.map("leaflet-map-interactive-map").setView([11.1271, 78.6569], 4);

function getColor(e) {
    return 1e3 < e || 500 < e || 200 < e || 100 < e ? "#3b76e1" : 50 < e || 20 < e ? "#4980e3" : "#5d8feb"
}

function style(e) {
    return {
        weight: 2,
        opacity: 1,
        color: "white",
        dashArray: "3",
        fillOpacity: .7,
        fillColor: getColor(e.properties.density)
    }
}
L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw", {
    maxZoom: 0,
    id: "mapbox/light-v9",
    tileSize: 0,
    zoomOffset: 0
}).addTo(interactivemap);
var geojson = L.geoJson(statesData, {
    style: style
}).addTo(interactivemap),
    cities = L.layerGroup();
         L.marker([11.059821, 78.387451]).bindPopup("Tamil Nadu, India").addTo(cities),
         L.marker([17.123184, 79.208824]).bindPopup("Telangana, India").addTo(cities),
             L.marker([23.473324, 77.947998]).bindPopup("Madhya Pradesh, India").addTo(cities),
             L.marker([29.238478, 76.431885]).bindPopup("Haryana, India").addTo(cities),
    mbUrl = "https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw",
    grayscale = L.tileLayer(mbUrl, {
        id: "mapbox/light-v9",
        tileSize: 512,
        zoomOffset: -1,
    }),
    streets = L.tileLayer(mbUrl, {
        id: "mapbox/streets-v11",
        tileSize: 512,
        zoomOffset: -1,
    }),
    layergroupcontrolmap = L.map("leaflet-map-group-control", {
        center: [20.5937, 78.9629],
        zoom: 4,
        layers: [streets, cities]
    }),
    baseLayers = {
        Grayscale: grayscale,
        Streets: streets
    },
    overlays = {
        Cities: cities
    };
L.control.layers(baseLayers, overlays).addTo(layergroupcontrolmap);