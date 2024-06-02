const video = document.getElementById('video');
const canvas = document.getElementById('canvas');
const captureButton = document.getElementById('capture');
const context = canvas.getContext('2d');

// Solicitar acceso a la webcam
navigator.mediaDevices.getUserMedia({ video: true })
    .then(stream => {
        video.srcObject = stream;
        video.play();
    })
    .catch(err => {
        console.error(`Error al acceder a la webcam: ${err}`);
    });

// Capturar la imagen al hacer clic en el botón
captureButton.addEventListener('click', () => {
    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    const imageData = canvas.toDataURL('image/png');

    // Enviar la imagen al servidor
    fetch('/Home/Capturar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ imageData })
    }).then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.json();
    })
        .then(data => {
            window.location.href = `/Home/Index/?Imagen=${data.imagen}`;
        })
        .catch(error => {
            // Maneja cualquier error que ocurra
            console.error('There was a problem with the fetch operation:', error);
        });
});