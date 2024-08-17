$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Account/GetStates",
        success: function (data) {
            var states = "<option value=''>Select State</option>";
            $.each(data, function (i, state) {
                states += "<option value='" + state.id + "'>" + state.name + "</option>";
            });
            $("#state").html(states);
        }
    });

    $("#state").change(function () {
        var stateId = $("#state").val();
        $.ajax({
            type: "GET",
            url: "/Account/GetCities",
            data: { stateId: stateId },
            success: function (data) {
                var cities = "<option value=''>Select City</option>";
                $.each(data, function (i, city) {
                    cities += "<option value='" + city.id + "'>" + city.name + "</option>";
                });
                $("#city").html(cities);
            }
        });
    });
});