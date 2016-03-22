/*
    Тут используются cookie
    про них хорошо написано здесь - https://learn.javascript.ru/cookie
*/

/*
Выделить кнопку цветом
*/
function setItemSelected(id) {
    $("#leftMenuItem" + id).addClass("btn-primary");
}

function deleteAllCookies() {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function onClickCategory(id) {
    $("#leftMenuItem" + id).click(function () {
        deleteAllCookies();
        /*
            Важно, что path определяет адрес, где доступны cookie
            я поставил, чтобы были доступны везде
        */
        document.cookie = "selectedMenuItemId=" + id + ";path=/";
    });
}

$(document).ready(function () {
    var id = getCookie("selectedMenuItemId");
    setItemSelected(id);
});