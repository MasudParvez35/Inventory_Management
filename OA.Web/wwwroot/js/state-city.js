﻿$(document).ready(function () {
    $("#city").attr('disabled', true);
    $("#area").attr('disabled', true);
    LoadStates();
});

$("#state").change(function () {
    var stateId = $(this).val();
    if (stateId > 0) {
        LoadCities();
    }
});
$("#city").change(function () {
    var cityId = $(this).val();
    if (cityId > 0) {
        LoadAreas();
    }
});
function LoadStates() {
    $('#state').empty();

    $.ajax({
        url: "/Account/GetStates",
        type: "GET",
        success: function (response) {
            if (response && response.length > 0) {
                $('#state').attr('disabled', false);
                $('#state').append('<option>--select state--</option>');
                $('#city').append('<option>--select city--</option>');
                $('#area').append('<option>--select area--</option>');
                $.each(response, function (i, data) {
                    $('#state').append('<option value="' + data.id + '">' + data.name + '</option>');
                });
            }
        },
        error: function (error) {
            // Handle error if needed
        }
    });
}

function LoadCities() {
    $('#city').empty();

    var stateId = $('#state').val();
    $.ajax({
        url: "/Account/GetCities",
        type: "GET",
        data: { stateId: stateId },
        success: function (response) {
            if (response && response.length > 0) {
                $('#city').attr('disabled', false);
                $('#city').append('<option>--select city--</option>');
                $.each(response, function (i, data) {
                    $('#city').append('<option value="' + data.id + '">' + data.name + '</option>');
                });
            }
            else {
                $('#city').attr('disabled', true);
                $('#city').append('<option>--cities not available--</option>');
            }
        },
        error: function (error) {
            // Handle error if needed
        }
    });
}

function LoadAreas() {
    $('#area').empty();

    var cityId = $('#city').val();
    console.log("Selected City ID:", cityId);
    $.ajax({
        url: "/Account/GetAreas",
        type: "GET",
        data: { cityId: cityId },
        success: function (response) {
            if (response && response.length > 0) {
                $('#area').attr('disabled', false);
                $('#area').append('<option>--select area--</option>');
                $.each(response, function (i, data) {
                    $('#area').append('<option value="' + data.id + '">' + data.name + '</option>');
                });
            }
            else {
                $('#area').attr('disabled', true);
                $('#area').append('<option>--areas not available--</option>');
            }
        },
        error: function (error) {
            // Handle error if needed
        }
    });
}
