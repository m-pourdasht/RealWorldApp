function OpenBootstrapModal(idmodal) {
    var myModalAlternative = new bootstrap.Modal(document.getElementById(idmodal));
    myModalAlternative.show();
}
window.getLocation = () => {
    return new Promise((resolve) => {
        navigator.geolocation.getCurrentPosition((position) => {
            resolve(`Latitude: ${position.coords.latitude}, Longitude: ${position.coords.longitude}`);
        });
    });
}
