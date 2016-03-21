function openRobotProgramsById(id) {
    $("#robot" + id).click(function () {
        window.location.href = 'RedirectToProgramms?robotId=' + id;
    });
}