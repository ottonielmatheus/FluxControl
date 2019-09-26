﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/serialHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("VehicleArrived", function (status)
{
    var encodedMsg = status.statusCode + ": " + status.data;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start()
    .then(function ()
    {
        document.getElementById("sendButton").disabled = false;
    })
    .catch(function (err)
    {
        return console.error(err.toString());
    });

document.getElementById("sendButton").addEventListener("click", function (event) {

    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", message)
        .catch(function (err) {
            return console.error(err.toString());
        });

    event.preventDefault();
});