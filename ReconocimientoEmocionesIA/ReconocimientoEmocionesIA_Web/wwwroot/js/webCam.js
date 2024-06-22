const video = document.getElementById('video');
const canvas = document.getElementById('canvas');
const captureButton = document.getElementById('capture');
const context = canvas.getContext('2d');
const spinner = document.getElementById('overlay');

spinner.style.display = "none";


navigator.mediaDevices.getUserMedia({ video: true })
    .then(stream => {
        video.srcObject = stream;
        video.play();
    })
    .catch(err => {
        console.error(`Error al acceder a la webcam: ${err}`);
    });

captureButton.addEventListener('click', () => {
    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    const imageData = canvas.toDataURL('image/png');

    spinner.style.display = "block";


    fetch('/Home/Capturar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ imageData })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.json();
        })
        .then(data => {
            fetch(`/Home/GenerarMeme/?fileName=${data.fileName}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok ' + response.statusText);
                    }
                    return response.json();
                })
                .then(memeData => {

                    console.log(memeData);


                    //preparando parametros
                    const params = new URLSearchParams();

                    // Añadir propiedades básicas al query string
                    params.append('emocionId', memeData.frase.idEmotion);
                    params.append('fraseId', memeData.frase.idPhrase);
                    params.append('imagen', memeData.imagen);
                    params.append('frase', memeData.frase.description);
                    

                    // Añadir elementos del array 'emociones'
                    //memeData.emociones.forEach((emocion, index) => {
                    //    for (const [key, value] of Object.entries(emocion)) {
                    //        params.append(`emociones[${index}][${key}]`, value);
                    //    }
                    //});

                    memeData.emociones.forEach((emocion, index) => {
                        params.append(`Emociones[${index}].Nombre`, emocion.nombre);
                        params.append(`Emociones[${index}].Porcentaje`, emocion.porcentaje * 100);
                    });

                    // Convertir a query string
                    const queryString = params.toString();

                    console.log(queryString);
                    window.location.href = 'https://localhost:7218/Home/Index?' + queryString;
                    spinner.style.display = "none";
                })
                .catch(error => {
                    spinner.style.display = "none";
                });
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            spinner.style.display = "none";
        });
});
function objectToQueryString(obj) {
    const params = new URLSearchParams();
    for (const key in obj) {
        if (Array.isArray(obj[key])) {
            for (const item of obj[key]) {
                params.append(`${key}[]`, `${item.Nombre}=${item.Porcentaje}`);
            }
        } else {
            params.append(key, obj[key]);
        }
    }
    return params.toString();
}