
function generarMeme(fileName)
{
    fetch(`/Home/GenerarMeme/?fileName=${fileName}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.json();
        })
        .then(memeData => {

            console.log(memeData);

            const params = new URLSearchParams();

            params.append('emocionId', memeData.frase.idEmotion);
            params.append('fraseId', memeData.frase.idPhrase);
            params.append('imagen', memeData.imagen);
            params.append('frase', memeData.frase.description);

            memeData.emociones.forEach((emocion, index) => {
                params.append(`Emociones[${index}].Nombre`, emocion.nombre);
                params.append(`Emociones[${index}].Porcentaje`, emocion.porcentaje * 100);
            });

            const queryString = params.toString();

            console.log(queryString);
            window.location.href = 'https://localhost:7218/Home/Index?' + queryString;
            spinner.style.display = "none";
        })
        .catch(error => {
            spinner.style.display = "none";
        });
}

//function realizarAccionFetch(api, imageData, contentType)
//{
//    fetch(api, {
//        method: 'POST',
//        // Asegúrate de que el tipo de contenido sea el correcto para la imagen
//        headers: {
//            'Content-Type': contentType //contentType //'application/json' // descomenta si el API requiere JSON
//        },
//        body: imageData // Asegúrate de que imageData sea lo que esperas
//    })
//        .then(response => {
//            // Log para verificar el estado de la respuesta
//            console.log('Response status:', response.status);
//            console.log('Response status text:', response.statusText);
//            if (!response.ok) {
//                throw new Error('Network response was not ok ' + response.statusText);
//            }
//            return response.json();
//        })
//        .then(data => {
//            // Log para verificar los datos recibidos
//            console.log('Data received:', data);
//            generarMeme(data.fileName);
//        })
//        .catch(error => {
//            console.error('There was a problem with the fetch operation:', error);
//            spinner.style.display = "none";
//        });
//}

function realizarAccionFetch(api, imageData, contentType) {
    const options = {
        method: 'POST',
        body: imageData
    };

    if (contentType) {
        options.headers = {
            'Content-Type': contentType
        };
    }

    fetch(api, options)
        .then(response => {
            console.log('Response status:', response.status);
            console.log('Response status text:', response.statusText);
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.json();
        })
        .then(data => {
            console.log('Data received:', data);
            generarMeme(data.fileName);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            spinner.style.display = "none";
        });
}