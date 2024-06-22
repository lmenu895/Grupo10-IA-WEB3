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
    realizarAccionFetch('/Home/Capturar', JSON.stringify({ imageData }), "application/json");
});