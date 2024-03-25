/* 
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/JSP_Servlet/JavaScript.js to edit this template
 */


window.onload = function () {
    const stage = document.querySelector(".popup-box__stage");
    let slider = -1400;
    let direction = 1;

    setInterval(() => {
        slider += direction;
        if (Math.abs(slider) > 1400) {
            direction = -direction;
        }
        stage.style.transform = `translateX(${slider}px)`;
    }, 1);
}