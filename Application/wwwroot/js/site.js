// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
}

let displayImagesOnPage = (successful, mesg, response) => {
    // Handler
    var scannedImages = scanner.getScannedImages(response, true, false); // returns an array of ScannedImage
    for (let i = 0; scannedImages instanceof Array && i < scannedImages.length; i++) {
        let scannedImage = scannedImages[i];
        var elementImg = scanner.createDomElementFromModel({
            name: "img",
            attributes: { class: "scanned", src: scannedImage.src },
        });
        (document.getElementById("images")
            ? document.getElementById("images")
            : document.body
        ).appendChild(elementImg);
    }
}