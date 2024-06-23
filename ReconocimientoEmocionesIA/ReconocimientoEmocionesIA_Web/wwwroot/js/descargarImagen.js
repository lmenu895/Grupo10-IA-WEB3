document.addEventListener('DOMContentLoaded', (event) => {
    console.log("DOM completamente cargado y parseado");

    
    const observer = new MutationObserver((mutationsList, observer) => {
        for (const mutation of mutationsList) {
            if (mutation.type === 'childList') {
                const finalImage = document.getElementById('finalImage');
                if (finalImage) {
                    console.log("Imagen generada detectada en el DOM");
                    document.getElementById('downloadImage').disabled = false;
                    observer.disconnect();
                }
            }
        }
    });

    
    observer.observe(document.body, { childList: true, subtree: true });

    document.getElementById('downloadImage').addEventListener('click', function () {
        const image = document.getElementById('finalImage');
        if (image) {
            const link = document.createElement('a');
            link.href = image.src;

            
            const now = new Date();
            const dateString = now.toISOString().split('T')[0];
            link.download = `meme_${dateString}.png`;

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        } else {
            console.error("No se encontró la imagen para descargar.");
        }
    });
});
