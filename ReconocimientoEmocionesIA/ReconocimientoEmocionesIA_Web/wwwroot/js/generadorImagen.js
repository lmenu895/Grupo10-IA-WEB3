const buttonImage = document.getElementById('buttonImage');
const form = document.getElementById('uploadForm');
const fileInput = document.getElementById('imageUpload');

fileInput.addEventListener('change', function() {
    if (fileInput.files.length > 0) {
        const formData = new FormData();
        formData.append('imagenData', fileInput.files[0]);

        spinner.style.display = "block";

        realizarAccionFetch('/Home/GenerarImg', formData);
    }
});
