$(document).ready(function () {
    server = `https://proj.ruppin.ac.il/cgroup64/test2/tar1/`
    //let port = 7021;
    //server = `https://localhost:${port}/`
    //get the id from the query string
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get("id");
    //add the id to the form
    $("#flatGetId").append(`<input type="number" class="form-control" id="flatId" value="${id}" readonly />`);
    loged = JSON.parse(sessionStorage.getItem('user'));
    $("#userGetEmail").append(`<input type="text" class="form-control" id="userEmail" value="${loged.email}" readonly />`);
    $("#newVac").submit(submitFunc);
    $("#getMyVac").on("click", getVac);
})

function submitFunc() {
    if (checkDates()) {
        postVac();
        return false;
    }
    return false;
}

function postVac() {
    // createa a JSON object called vacation
    vacation = {
        UserEmail: $("#userEmail").val(),
        FlatId: $("#flatId").val(),
        StartDate: $("#startDate").val(),
        EndDate: $("#endDate").val()
    }
    let api = server + `api/Vacations`
    ajaxCall("POST", api, JSON.stringify(vacation), postSCB, postECB);
}

function postSCB(status) {
    if (status) {
        swal("The vacation was successfully saved!", "Enjoy", "success");
    }
    else
        alert("This flat is unavailable on those dates");
}

function postECB(err) {
    console.log(err);
}

function getVac() {
    let api = server + `api/Vacations`
    ajaxCall("GET", api, "", getSCB, getECB);
}

function getSCB(vacList) {
    //get only the user vacations
    let user = $("#userEmail").val()
    str = `<div class="row">`;
    for (var i = 0; i < vacList.length; i++) {
        if (vacList[i].userEmail == user) {
            str += `<div class="col-md-3 divF">
                        <p><u>Flat id:</u> ${vacList[i].flatId}<br></p>
                        <p><u>Vacation id:</u> ${vacList[i].id}</br></p>
                        <p><u>User email:</u> ${vacList[i].userEmail}</br></p>
                        <p><u>Start date</u> ${vacList[i].startDate}</br></p>
                        <p><u>End date</u> ${vacList[i].endDate}</br></p>
                        </div>`
        }
    }
    str += `</div>`;
    $("#myVacations").html(str);
}

function getECB(err) {
    console.log(err);
}

function checkDates() {
    if ($("#startDate").val() >= $("#endDate").val()) {
        alert("The end date must be greater than the start date");
        return false;
    }
    else {
        const endDateObj = new Date($("#endDate").val());
        const startDateObj = new Date($("#startDate").val());
        Diff = endDateObj - startDateObj;
        dayDiff = Math.floor(Diff / (1000 * 60 * 60 * 24));
        if (dayDiff > 20) {
            alert('The vacation cannot exceed 20 days');
            return false
        }
        else
            return true;
    }
}