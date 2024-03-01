$(document).ready(function () {
    // server = `https://proj.ruppin.ac.il/cgroup64/test2/tar1/`
    let port = 7021;
    server = `https://localhost:${port}/`;
    $('#logOutAdmin').on('click', () => {
        sessionStorage.clear();
    })
    
    $('#data').hide();
    $('#manageUsers').on('click', renderUsers)
    $("#monthsDropdown").on('change', (e) => renderReport(e));
})

function renderUsers() {
    let api = server + `api/Users`
    ajaxCall("GET", api, "", getSCB, getECB);
}
function getSCB(usersData) {
    users = usersData;
    try {
        tbl = $('#usersTable').DataTable({
            data: usersData,
            pageLength: 5,
            columns: [
                { data: "email" },
                { data: "firstName" },
                { data: "familyName" },
                { data: "password" },
                {
                    render: function (data, type, row, meta) {
                        if (row.isActive == true) {
                            isActivCheckBox = "<input type='checkbox' class='isActive' checked id = '" + row.email + "'/>"
                        }
                        else {
                            isActivCheckBox = "<input type='checkbox' class='isActive' id = '" + row.email + "'/>"
                        }
                        return isActivCheckBox;
                    }
                }
            ],
        });
    }

    catch (err) {
        alert(err);
    }
    $('.isActive').on('change', (e) => changeIsActive(e));
    $('#data').show();
    $('#manageUsers').hide();
}
function getECB(err) {
    console.log(err);
}
function changeIsActive(e) {
    checkedId = e.target.id;
    if (e.target.checked) {
        statusA = true;
    }
    else {
        statusA = false;
    }
    user = {
        FirstName: "",
        FamilyName: "",
        Email: checkedId,
        Password: "",
        IsActive: statusA
    }
    let api = server + `api/Users/isActive`
    ajaxCall("PUT", api, JSON.stringify(user), putSCB, putECB);
}

function putSCB() {
    console.log(users);
}
function putECB(err) {
    console.log(err);
}
function renderReport(e) {
    selectedM = e.target.value;
    let api = server + `api/Vacations`
    ajaxCall("GET", api, selectedM, reportSCB, reportECB);
}
function reportSCB(list) {
    let str = "<p>";
    list.map(rep => {
        str += rep.city + ": " + rep.averagePrice + "</p>";
    })
    $('#results').html(str);
}
function reportECB(err) {
    console.log(err);
}