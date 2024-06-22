const buttonImage = document.getElementById('buttonImage');
const form = document.getElementById('uploadForm');

buttonImage.addEventListener('click', (event) => {
    event.preventDefault();
    const formData = new FormData(form);

    const fileInput = document.getElementById('imageUpload');
    const file = fileInput.files[0];

    formData.append('imagenData', file);

    spinner.style.display = "block";
    realizarAccionFetch('/Home/GenerarImg', formData);
});