window.canvasFunctions = {
    drawHangman: function (canvasId, step) {
        const canvas = document.getElementById(canvasId);
        const context = canvas.getContext("2d");
        const centerX = canvas.width / 2;
        const centerY = canvas.height / 2;

        // Funktion zum Zeichnen des Hangman
        function drawHangmanParts(step) {
            context.clearRect(0, 0, canvas.width, canvas.height);
            context.beginPath();

            // Zeichnen des Galgenpfostens
            if (step >= 1) {
                context.moveTo(centerX - 20, centerY + 50);
                context.lineTo(centerX - 20, centerY - 50);
            }

            // Zeichnen des Querbalkens
            if (step >= 2) {
                context.moveTo(centerX - 20, centerY - 50);
                context.lineTo(centerX + 50, centerY - 50);
            }

            // Zeichnen des Seils
            if (step >= 3) {
                context.moveTo(centerX + 50, centerY - 50);
                context.lineTo(centerX + 50, centerY - 40);
            }

            // Zeichnen des Kopfes
            if (step >= 4) {
                context.arc(centerX + 50, centerY - 30, 10, 0, 2 * Math.PI);
            }

            // Zeichnen des Körpers
            if (step >= 5) {
                context.moveTo(centerX + 50, centerY - 20);
                context.lineTo(centerX + 50, centerY + 10);
            }

            // Zeichnen des linken Arms
            if (step >= 6) {
                context.moveTo(centerX + 50, centerY - 10);
                context.lineTo(centerX + 40, centerY);
            }

            // Zeichnen des rechten Arms
            if (step >= 7) {
                context.moveTo(centerX + 50, centerY - 10);
                context.lineTo(centerX + 60, centerY);
            }

            // Zeichnen des linken Beins
            if (step >= 8) {
                context.moveTo(centerX + 50, centerY + 10);
                context.lineTo(centerX + 40, centerY + 20);
            }

            // Zeichnen des rechten Beins
            if (step >= 9) {
                context.moveTo(centerX + 50, centerY + 10);
                context.lineTo(centerX + 60, centerY + 20);
            }

            context.stroke();
        }

        drawHangmanParts(step);
    }
};
