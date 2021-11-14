// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function changePlanningTab(evt, tabName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tab-content");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tab-button");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
}

$(function() {

    $("#add").click(function(e) {
        e.preventDefault();
        var i = $(".items").length;
        var n = '<label class="control-label"> Title </label>' +
            '<input type="text" class="form-control" name="Goals[' + i + '].Title" />' +
            '<label class="control-label"> Target </label>' +
            '<input type="number" class="form-control" name="Goals[' + i + '].NumericalTarget" />' +
            '<label class="control-label"> Progress </label>' +
            '<input type="number" class="form-control" name="Goals[' + i + '].NumericalProgress" />';
        $("#goal-list").append(n);
    });

});