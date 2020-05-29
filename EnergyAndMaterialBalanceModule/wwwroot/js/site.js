// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#resources').change(function () {

    window.location.href = location.protocol + '//' +
        location.host + "/Main/Index/" + $(this).val();
});



$(document).ready(function () {
    $("#tableView tbody tr").click(function () {
        var selected = $(this).hasClass("highlight");

        $("#tableView tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });
});