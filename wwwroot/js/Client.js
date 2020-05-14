"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/measurementHub").build();

connection.start().catch(function (e) {
});

connection.on("ReceiveMeasurement", function (time,name,lat,lon,temp,humid,pressure) {
    var m = "Time :" + time + "," +
        "Name: " + name + "," +
        "Lat: " + lat + "," +
        "Lon: " + lon + "," +
        "Temperature: " + temp + "," +
        "Humidity: " + humid + "," +
        "Pressure: " + pressure + ",";
    var li = document.createElement("li");
    li.textContent = m;
    document.getElementById("measurementList").appendChild(li);

});

