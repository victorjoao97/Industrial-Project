// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// getting all bgroups by selected Resource

var selectedResult = null;

async function GetBGroups(resourceId) {
    const response = await fetch("main/getBGroup/" + resourceId, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const result = await response.json();
        selectedResult = result;
        fillSelect(result.selectedResource.resourceId);
        fillTreeView(result.bgroups);
        clearTableView();
    }
}

// getting all points by selected BGroup

async function GetPoints(bgroupId) {
    const response = await fetch("main/getPoints/" + bgroupId, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const result = await response.json();
        selectedResult = result;
        disableUpdateBgroupBtn(false);
        clearTableView();
        fillTableView(result.points);
        fillValidDisbalance(result.selectedBGroup.validDisbalance);
    }
}

// function for setting selected Resource

function fillSelect(resourceId) {
    selectElement('resources', resourceId);
}

// creating tree view component with BGroups

function fillTreeView(bgroups) {
    var tree = $('#tree').tree({
        uiLibrary: 'bootstrap4',
        primaryKey: 'bgroupId',
        textField: 'bgroupName',
        childrenField: "inverseBgroupIdparentNavigation",
        select: function (e, node, id) {
            GetPoints(id);
            $("#add_bgroup_id_parent").val(id);
            $("#edit_bgroup_id").val(id);
        },
        unselect: function (e, node, id) {
            clearTableView();
            clearValidDisbalance();
        }
    });

    tree.render(bgroups);
}

// setting selected Resource

function selectElement(id, valueToSelect) {
    var element = document.getElementById(id);
    element.value = valueToSelect;
}

// setting valid disbalance for selected BGroup

function fillValidDisbalance(data) {
    $('#validDisbalance').text(data);
}

// creating table view component (tbody) with Points

function fillTableView(points) {
    var tableView = document.getElementById("tableView");
    var rows = tableView.getElementsByTagName('tbody')[0];
    points.forEach((point, i) => {
        rows.append(row(point, i));
    });
}

// deleting table view component 

function clearTableView() {
    $('#tableView tbody > tr').remove();
}

// deleting valid disbalance 

function clearValidDisbalance() {
    fillValidDisbalance("");
}

// deleting tree view component 

function clearTreeView() {
    fillTreeView([]);
}

//setting points' data in table view component

function row(point, i) {

    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", point.pointId);
    tr.className = "d-flex";

    const idTd = document.createElement("td");
    idTd.append(i + 1);
    idTd.className = "col-1";
    tr.append(idTd);

    const nameTd = document.createElement("td");
    nameTd.append(point.pointName);
    nameTd.className = "col-3";
    tr.append(nameTd);

    const directionTd = document.createElement("td");
    directionTd.append(point.direction);
    directionTd.className = "col-2";
    tr.append(directionTd);

    const tagTd = document.createElement("td");
    tagTd.append(point.tagname);
    tagTd.className = "col-1";
    tr.append(tagTd);

    const periodTd = document.createElement("td");
    periodTd.append(point.period.periodName);
    periodTd.className = "col-2";
    tr.append(periodTd);

    const validMistakeTd = document.createElement("td");
    validMistakeTd.append(point.validMistake);
    validMistakeTd.className = "col-1";
    tr.append(validMistakeTd);

    const sourceTd = document.createElement("td");
    sourceTd.append(point.source.sourceName);
    sourceTd.className = "col-2";
    tr.append(sourceTd);

    return tr;
}

function disableCreateBgroupBtn(status) {
    $('#btnCreateBgroupModal').prop('disabled', status);
}

function disableUpdateBgroupBtn(status) {
    $('#btnUpdateBgroupModal').prop('disabled', status);
}

function showCreateBgroupModal() {
    $('#add_resource_id').val(selectedResult.selectedResource.resourceId);

    if (selectedResult.selectedBGroup != null)
        $('#add_bgroup_id_parent').val(selectedResult.selectedBGroup.bgroupId);
}

function showUpdateBgroupModal() {
    $('#edit_bgroup_id').val(selectedResult.selectedBGroup.bgroupId);
    $('#edit_bgroup_name').val(selectedResult.selectedBGroup.bgroupName);
    $('#edit_valid_disbalance').val(selectedResult.selectedBGroup.validDisbalance);
}

async function CreateBgroups() {
    bGroupNameVal = $("#add_bgroup_name").val();
    validDisbalanceVal = $("#add_valid_disbalance").val();
    resourceIdVal = $("#add_resource_id").val();
    bGroupIdParentVal = $("#add_bgroup_id_parent").val();

    $.post("/main/CreateBgroups/", {
        bgroupName: bGroupNameVal,
        validDisbalance: validDisbalanceVal,
        resourceId: resourceIdVal,
        bGroupIdParent: bGroupIdParentVal
    }).done(function (result) {
        console.log(result);
        $('#createBGroupModal').modal('hide');
        GetBGroups(resourceIdVal);
    }).fail(function (result) {
        console.log("error!")
    });
}

async function UpdateBgroups() {
    bGroupNameVal = $("#edit_bgroup_name").val();
    validDisbalanceVal = $("#edit_valid_disbalance").val();
    bgroupsIdVal = $("#edit_bgroup_id").val();

    console.log(bgroupsIdVal);
    $.post("/Main/UpdateBgroups/", {
        bgroupId: bgroupsIdVal,
        bgroupName: bGroupNameVal,
        validDisbalance: validDisbalanceVal
    }).done(function (result) {
        console.log(result);
        $('#updateBGroupModal').modal('hide');
        GetBGroups(result.selectedBGroup.resourceId);
    }).fail(function (result) {
        console.log("error!")
    });
}

$(document).ready(function () {
    $("#tableView").delegate("tr", "click", function () {
        var selected = $(this).hasClass("highlight");

        $("#tableView tr").removeClass("highlight");
        if (!selected)
            $(this).addClass("highlight");
    });

    $('#resources').change(function () {
        if ($(this).val() === "") {
            clearTableView();
            clearTreeView();
            disableCreateBgroupBtn(true);
        }
        else {
            GetBGroups($(this).val());
            $("#add_resource_id").val($(this).val());
            disableCreateBgroupBtn(false);
        }
        clearTableView();
        clearValidDisbalance();
    });

    $('#btnCreateBgroupModal').click(function () {
        showCreateBgroupModal();
    });

    $('#btnUpdateBgroupModal').click(function () {
        showUpdateBgroupModal();
    });

    $('#add_bgroups_form').on('submit', function (e) {
        console.log("SUBMIT");
        e.preventDefault();
        CreateBgroups();
    });

    $('#edit_bgroups_form').on('submit', function (e) {
        console.log("Submit Update");
        e.preventDefault();
        UpdateBgroups();
    });
});
    
