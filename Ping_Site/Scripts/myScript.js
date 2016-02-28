window.dataForChart = 0, window.chart = 0, window.i = 0, window.max = 0, window.min = 0, window.midl = 0;
window.temp = 0, window.flag = 0;
window.responsTime = [];
function PingSite(path) {
    window.flag = 1;
    $.ajax({
        type: "POST",
        url: "/home/Post/",
        data: { path: path },
        dataType: "json",
        success: function (data) {
            if (data >= 0) {
                $("#textout").val(data);
                window.responsTime.push([window.i, data])
                window.temp = data;
                window.i++;
                window.midl += data;
                window.dataForChart = [window.responsTime];
                $(document).ready(function () {
                    window.chart = $.plot($("#chart"), window.dataForChart);
                });
                if (window.flag) {
                    PingSite(path);
                }
            }
            else {
                alert("Url not valid!!! Enter please correct url. For example: vk.com or www.facebook.com");
                clearInterval(window.timerId)
                return;
            }
        },
        error: function () {
            alert("Something went wrong, please try again later.");
        }
    })
    if (1 == i) {
        window.max = window.temp;
        window.min = window.temp;
    }
    else {
        if (window.max < window.temp) {
            window.max = window.temp;
        }
        if (window.min > window.temp) {
            window.min = window.temp;
        }
    }
}
function StopPing(path) {
    window.flag = 0;
    window.midl = window.midl / (i + 1);
    var m = parseInt(window.midl);
    $.ajax({
        type: "POST",
        url: "/home/PostSave/",
        data: { path: path, maximum: window.max, minimum: window.min, mid: m },
        dataType: "text",
        success: function (data) {
            clearInterval(window.timerId)
            window.dataForChart = 0, window.chart = 0, window.i = 0;
            window.responsTime = [];
            alert(data);
        },
        error: function () {
            alert("The data could not be saved, please try again later.");
        }
    });
    window.dataForChart = 0, window.chart = 0, window.i = 0;
    window.responsTime = [];
}
function ShowTop() {
    $.ajax({
        type: "GET",
        url: "/DataSites/DataSitesView/",
        data: {},
        dataType: "html",
        success: function (data) {
            $("#siteTable").html(data);
        }
    });
}
function ClearDb() {
    $.ajax({
        type: "GET",
        url: "/home/ClearDb/",
        dataType: "text",
        success: function (data) {
            $("#siteTable").html("");
            alert(data);
        },
        error: function () {
            alert("The data could not be deleted, please try again later.");
        }
    });
}