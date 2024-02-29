$(document).ready(function () {
     server = `https://proj.ruppin.ac.il/cgroup64/test2/tar1/`
   /* let port = 7021;*/
    /*server = `https://localhost:${port}/`;*/

    //initialize
    let loged = sessionStorage.getItem('user');
    if (loged) {
        let logedUser = JSON.parse(loged);
        logedUserChanges();
        initialUser(logedUser);
    }
    else {
        logoutChanges();
    }

    //modal script
    $('#loginBtn').on('click',()=> {
        $('#loginModal').css("display","block");
    });
    $('.close').on('click', ()=> {
        $('#loginModal').css("display", "none");
    });

    //functions
    $('#loginForm').submit(submitLogin);
    $('#regForm').submit((e) => {
        if ($('#submitBtn').val() === 'Register') {
            submitReg();
        } else {
            submitUpdate();
        }
        e.preventDefault();
    });

    $('#logoutBtn').on('click', logoutChanges); 
})

function submitReg() {
    postUser()
    return false;
}

function postUser() {
    // createa a JSON object called Ueser
    user = {
        FirstName: $("#firstName").val(),
        FamilyName: $("#familyName").val(),
        Email: $("#email").val(),
        Password: $("#password").val(),
    }
    //check if user with this email exists
    let api = server + `api/Users`
    ajaxCall("POST", api, JSON.stringify(user), postSCB, postECB);
}

function postSCB(status) {
    swal("You have successfully registered!", "Great Job", "success");
}

function postECB(err) {
    alert("This email is already exists");
    console.log(err);
}


function submitLogin() {
    // createa a JSON object called Ueser
    user = {
        FirstName: "",
        FamilyName: "",
        Email: $("#emailLogin").val(),
        Password: $("#passwordLogin").val(),
    }
    let api = server + `api/Users/Login`
    ajaxCall("POST", api, JSON.stringify(user), LoginSCB, LoginECB);
    return false;
}

function LoginSCB(user) {
    if (user.email != null) {
        $('#incorrect').html('');
        // Close the modal after login attempt
        $('#loginModal').css("display", "none");
        //reset the login form
        $('#loginForm :input').not('#loginBtnModal').val('');
        swal("You've logged in successfully", "Great Job", "success");
        sessionStorage.setItem('user', JSON.stringify(user));
        initialUser(user);
        logedUserChanges()
    }
    else {
        $('#incorrect').html('incorrect details!')
    }
}
function initialUser(user) {
    $('#firstName').val(user.firstName);
    $('#familyName').val(user.familyName);
    $('#email').val(user.email);
    $('#email').prop('readonly', true);
    $('#password').val(user.password);
}
function LoginECB(err) {
    console.log(err);
}

function submitUpdate() {
    // createa a JSON object called Ueser
    user = {
        FirstName: $("#firstName").val(),
        FamilyName: $("#familyName").val(),
        Email: $("#email").val(),
        Password: $("#password").val(),
    }
    let api = server + `api/Users`
    ajaxCall("PUT", api, JSON.stringify(user), updateSCB, updateECB);
    return false;
}

function updateSCB(user) {
    sessionStorage.setItem('user', JSON.stringify(user))
    initialUser(user);
    swal("Your details have been successfully updated", "Great Job", "success");
}
function updateECB() {
    console.log(err);
}
function logedUserChanges() {
    $('#regTitle').html('Edit details');
    $('#submitBtn').val('Update details');
    $('#logoutBtn').show();
    $('#loginBtn').hide();
    //$('#regForm').submit(submitUpdate);
    //$('#regForm').off('submit', submitReg);
}
function logoutChanges() {
    $('#regTitle').html('Registration');
    $('#submitBtn').val('Register');
    //$('#submitRegUser').show();
    //$('#updateDetails').hide();
    $('#logoutBtn').hide();
    $('#loginBtn').show();
    $('#regForm :input').not('#submitBtn').val('');
    //$('#regForm').submit(submitReg);
    //$('#regForm').off('submit', submitUpdate);
    $('#email').prop('readonly', false);
    sessionStorage.clear();
}
