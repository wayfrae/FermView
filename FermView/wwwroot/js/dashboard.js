

function registerDevice() {
    console.log($('#registrationCode').val());
    console.log($('#controllerName').val());
    $.ajax({
        url: "/Home/RegisterDevice",
        data: {
            "setupCode": $('#registrationCode').val(),
            "name": $('#controllerName').val(),
            "owner": "@UserManager.GetUserName(User)"
        }
    }).done(function () {
        alert("Success!");
    });
}

function getProfiles() {
    $.ajax({
        url: "/Home/GetProfiles",
        data: { "userId": "@UserManager.GetUserId(User)" }
    }).done(function (response) {
        $("#profileTable").html("");
        $.each(response, function (i) {
            $("#profileTable").append(
                "<tr><td>" + response[i].name +
                "</td><td>" + response[i].description +
                "</td><td>" + response[i].hearts +
                "</td><td><div class='btn-group'><button class='btn btn-secondary' onclick='editProfile(" + response[i].id + ")'>Edit</button>" +
                "<button class='btn btn-danger' onclick='deleteProfile(" + response[i].id + ")'>&times;</button></td></tr>")

        });
        showProfileTable($("#profileTable").html() == "");
    });
}

function getDevices() {
    $.ajax({
        url: "/Home/GetDevices",
        data: { "userId": "@UserManager.GetUserName(User)" }
    }).done(function (response) {
        $("#profileTable").html("");
        $.each(response, function (i) {
	        $("#profileTable").append("<tr><td>" + response[i].name + "</td></tr>");
        });
        showProfileTable($("#profileTable").html() == "");
    });
}

function showProfileTable(isVisible) {
    if (isVisible) {
        $("#emptyProfiles").show();
        $("#profileTableFull").hide();
    } else {
        $("#emptyProfiles").hide();
        $("#profileTableFull").show();
    }
}
function createProfile() {
    var points = [];

    var temps = $('[name="temp"]').get();
    console.log(temps);
    var durations = $('[name="duration"]').get();
    for (var i = 0; i < temps.length; i++) {
        var obj = {};
        obj[temps[i].value] = durations[i].value;
        points.push(obj);
    }
    console.log(JSON.stringify(points));
    $.ajax({
        url: "/Home/CreateProfile",
        data: {
            "userId": "@UserManager.GetUserId(User)",
            "profileName": $('#profileName').val(),
            "description": $('#profileDescription').val(),
            "json": JSON.stringify(points)
        }
    }).done(function () {
        getProfiles();
        $("#modalAddProfile").modal("hide");
    }).fail(function () {
        $("#profileMessage").text = "There was an error saving the profile. Check your inputs.";
    });

}

function addTempRow() {
    $("#profileForm").append('<div class="form-row"><div class="col"><input type="number" class="form-control" name="temp" step="0.1" max="90" min="32" placeholder="Temperature"></div><div class="col"><input type="number" class="form-control" name="duration" step="0.01" data-toggle="tooltip" data-placement="top" placeholder="Duration (in hours)"></div></div>');
}