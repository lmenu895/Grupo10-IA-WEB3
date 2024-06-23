const buttonImage = document.getElementById('buttonImage');
const form = document.getElementById('uploadForm');

/*buttonImage.addEventListener('click', (event) => {
    event.preventDefault();
    const formData = new FormData(form);

    const fileInput = document.getElementById('imageUpload');
    const file = fileInput.files[0];

    formData.append('imagenData', file);

    spinner.style.display = "block";
    realizarAccionFetch('/Home/GenerarImg', formData);
});*/
const fileInput = document.getElementById('imageUpload');

// Agregar evento change al input file
fileInput.addEventListener('change', function() {
    // Verificar si se seleccionó un archivo
    if (fileInput.files.length > 0) {
        const formData = new FormData();
        formData.append('imagenData', fileInput.files[0]); // Agregar el archivo al FormData

        spinner.style.display = "block"; // Mostrar el spinner

        realizarAccionFetch('/Home/GenerarImg', formData);
    }
});
