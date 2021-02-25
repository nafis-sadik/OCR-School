// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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