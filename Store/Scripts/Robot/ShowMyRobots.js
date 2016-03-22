/*
Функция по клику для кнопки Installed Programs
*/

function openRobotProgramsById(id, url) {
    $("#robot" + id).click(function () {
        window.location.href = url;
    });
}