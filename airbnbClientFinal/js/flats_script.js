$(document).ready(function () {
    server = `https://proj.ruppin.ac.il/cgroup64/test2/tar1/`
    //let port = 7021;
    //server = `https://localhost:${port}/`
    $("#city").on("blur", checkCity);
    $("#newFlat").submit(submitFunc);
    getFlats();
    loged = sessionStorage.getItem('user');
})

function checkCity() {
    let check = false;
    let v = $("#city").val();//chosen value
    let datalistCities = $("#cityList").find("option");
    datalistCities.each(function () {
        if ($(this).val() == v) {
            check = true;
            $("#city")[0].validity.valid = true;
            $("#city")[0].setCustomValidity('');
        }
    })
    if (!check) {
        $("#city")[0].validity.valid = false;
        $("#city")[0].setCustomValidity('The city should be chosen from a datalist');
    }
}

function submitFunc() {
    postFlat();
    return false;
}

function postFlat() {
    // createa a JSON object called Flat
    flat = {
        City: $("#city").val(),
        Address: $("#flatAdd").val(),
        NumOfRooms: $("#numOfRooms").val(),
        Price: $("#price").val()
    }
    let api = server + `api/Flats`
    ajaxCall("POST", api, JSON.stringify(flat), postSCB, postECB);
}

function postSCB(status) {
    if (status) {
        swal("The flat was successfully saved!", "Great Job", "success");
        getFlats();
    }
    else
        alert("This id is already exists");
}

function postECB(err) {
    console.log(err);
}

function getFlats() {
    let api = server + `api/Flats`
    ajaxCall("GET", api, "", getSCB, getECB);
}

function getSCB(flatList) {
    str = `<div class="row">`;
    for (var i = 0; i < flatList.length; i++) {
        str += `<div class="col-md-3 divF">
                        <p><u>id:</u> ${flatList[i].id}</br></p>
                        <p><u>city:</u> ${flatList[i].city}</br></p>
                        <p><u>address:</u> ${flatList[i].address}</br></p>
                        <p><u>number of rooms:</u> ${flatList[i].numOfRooms}</br></p>
                        <p><u>price:</u> ${flatList[i].price}</br></p>
                        <button class="orderBtn" onclick="moveToVacay(${flatList[i].id})">Order vacation</button>
                </div>`
    }
    str += `</div>`;
    $("#Flats").html(str);
    if (!loged) {
        const flatBtn = document.querySelectorAll(".orderBtn");
        flatBtn.forEach((btn) => {
            btn.disabled = true;
            console.log(btn.disabled);
        });
    }
}

function getECB(err) {
    console.log(err);
}

function moveToVacay(id) {
    window.location.href = "vacations.html?id=" + id;
}