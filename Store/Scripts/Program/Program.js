function setActionsOnUpdates(url) {
    
    $("input[value=Update]").each(function () {
        $(this).on("click", function () {
            var id = $(this).parent().parent().parent().attr("id").replace('RobotProgram', '');
            $(this).remove();
            makeAjaxToServer(url, id);
        });

    });
}

function setActionsOnRemoves(url) {
    $("input[value=Remove]").each(function () {
        $(this).on("click", function () {
            var id = $(this).parent().parent().parent().attr("id").replace('RobotProgram', '');
            $(this).parent().parent().parent().remove();
            makeAjaxToServer(url, id);
        });
    });
}

function makeAjaxToServer(url, id) {
    $.ajax({
        url: url,
        type: 'POST',
        async: false,
        data: 'programRobotId=' + id,
        dataType: 'json',
        success: function (result) {
            if (result.success) {
                //alert("asked robot to update! \n" +
                //    "success");
            } else {
                //alert("asked robot to update! \n" +
                //   "failed");
            }
        },
        // todo почему-то мы попадаем в ошибку, почему?
        error: function (error) {
            //alert("asked robot to update! \n" +
            //        "failed with " + error.textStatus);
        }
    });
}

