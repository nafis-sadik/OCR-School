// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const _Router = {
    ScannedImageSubmission: "/Scanner/UploadScannedImage",
    PTS: "https://ptsv2.com/t/5n5fj-1612777669/post",
};

const Controller = (_url, _method, _data, _viewport = "#Viewport", _cleanViewPort = true, _viewLoadingScreen = true) => {
    if (_viewport[0] != "#")
        _viewport = "#" + _viewport;

    $.ajax({
        url: _url,
        type: _method,
        data: _data,
        processData: false,
        contentType: false,
        success: (result) => {
            if (_viewLoadingScreen)
                $("#LoadingScreen").hide();

            if (_cleanViewPort)
                $(_viewport).empty();

            $(_viewport).append(result);
        },
        error: (result) => {
            if (_viewLoadingScreen)
                $("#LoadingScreen").hide();

            if (_cleanViewPort)
                $(_viewport).empty();

            $(_viewport).append(result);
        },
    });
};

$(document).ready(() => {
    $("#LoadingScreen").hide();

    $('#SubmitMarksheet').click(() => {
        Controller('/Scanner/UserSubmit', 'POST', new FormData($('#FinalizedMarkSheet').get(0)), '#Viewport', false, true);
    });
});