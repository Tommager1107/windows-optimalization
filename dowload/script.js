document.getElementById("download-btn").addEventListener("click", function() {
    var windowsVersion = document.getElementById("windows-version").value;
    if (windowsVersion === "windows10") {
        
        window.location.href = "https://github.com/Tommager1107/windows-optimalization/releases/download/1.0.0/Windows10-OptimalizationSoftware.exe";
    } else if (windowsVersion === "windows11") {
        
        window.location.href = "win11.html";
    }
});
