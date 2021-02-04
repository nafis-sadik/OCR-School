﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const scanRequest = {
    use_asprise_dialog: true, // Whether to use Asprise Scanning Dialog
    show_scanner_ui: false, // Whether scanner UI should be shown
    twain_cap_setting: {
        // Optional scanning settings
        ICAP_PIXELTYPE: "TWPT_RGB", // Color
    },
    output_settings: [
        {
            type: "return-base64",
            format: "jpg",
        },
    ],
};

let scan = () => {
    // Triggers the scan
    scanner.scan(displayImagesOnPage, scanRequest);
    $('.scanned').addClass('col-md-4');
}

let displayImagesOnPage = (successful, mesg, response) => {    
    // Handler
    let scannedImages = scanner.getScannedImages(response, true, false); // returns an array of ScannedImage
    //for (let i = 0; scannedImages instanceof Array && i < scannedImages.length; i++) {
    for (let i = 0; i < scannedImages.length; i++) {
        let scannedImage = scannedImages[i];
        let elementImg = scanner.createDomElementFromModel({
            name: "img",
            attributes: { class: "scanned", src: scannedImage.src }
        });
        //(document.getElementById("images") ? document.getElementById("images") : document.body).appendChild(elementImg);
        $('#images>.row').append(elementImg);
    }
};

let submitScan = () => {
    let _image = $('#images>img').attr('src');
    _image = _image.substring(23, _image.length);
    Controller('/Scanner/UploadScannedImage', 'POST', { image: _image });
};

const Controller = (url, method, _data, Viewport = "#Viewport") => {
    if (Viewport[0] != '#') { Viewport = '#' + Viewport; }

    $.ajax({
        url: url,
        type: method,
        data: _data,
        success: (result) => {
            $(Viewport).append(result);
        },
        error: (result) => {
            alert("Exception from JS Controller\nWatch Debug Console of this browser");
            console.error(result);
        }
    });
}

$('#SubmitMarksheet').click(() => {
    
});