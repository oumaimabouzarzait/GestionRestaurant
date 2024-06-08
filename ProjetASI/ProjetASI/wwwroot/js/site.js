// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var connectionProject = new signalR.HubConnectionBuilder().withUrl("/Hubs/Project").build();

connectionProject.on("updateTableIndex", () => {
    if (window.location.pathname === "/Tables") {
        location.reload();
    }
});

connectionProject.on("updateCommandeIndex", () => {
    if (window.location.pathname === "/Commandes") {
        location.reload();
    }
});

connectionProject.on("updateFactureIndex", () => {
    if (window.location.pathname === "/Factures") {
        location.reload();
    }
});

connectionProject.on("updateEncaissementIndex", () => {
    if (window.location.pathname === "/Encaissements") {
        location.reload();
    }
});

connectionProject.start();