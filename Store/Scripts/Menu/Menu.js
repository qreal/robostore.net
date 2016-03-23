/*
    Тут используются cookie
    про них хорошо написано здесь - https://learn.javascript.ru/cookie
*/

/*
Выделить кнопку(ссылку) цветом
*/
function setItemSelected(id) {
    $("#leftMenuItem" + id).addClass("btn-primary");
}

$(document).ready(function () {
    setItemSelected(getCookie("selectedMenuItemId"));

    /*
        По клику ссылки будут перезаписывать в cookie выбранную категорию
    */
    var links = $('a[id^="leftMenuItem"]');
    links.each(function () {
        var number = (this.id).replace('leftMenuItem', '');
        $(this).on("click", function () {
            deleteAllCookies();
            document.cookie = "selectedMenuItemId=" + number + ";path=/";
        });
    });
});