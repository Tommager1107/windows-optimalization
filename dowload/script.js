document.getElementById("download-btn").addEventListener("click", function() {
    var windowsVersion = document.getElementById("windows-version").value;
    if (windowsVersion === "windows10") {
        // Replace 'windows10_download_link' with the actual download link for Windows 10
        window.location.href = "windows10_download_link";
    } else if (windowsVersion === "windows11") {
        // Replace 'windows11_download_link' with the actual download link for Windows 11
        window.location.href = "win11.html";
    }
});
