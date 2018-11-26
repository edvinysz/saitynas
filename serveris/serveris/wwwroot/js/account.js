const uri = "../api/user";
let todos = null;

$(document).ready(function () {
    getData();
});

//GET METODAS
function getData() {
    //var token = document.cookie.replace(/(?:(?:^|.*;\s*)Token\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        headers: { "Authorization": 'Bearer ' + token },
        success: function (data) {
            //get username from cookie
            var user;
            var username = document.cookie.replace(/(?:(?:^|.*;\s*)Username\s*\=\s*([^;]*).*$)|^.*$/, "$1");
            //find the user
            $.each(data, function (key, item) {
                if (username == item.username)
                    user = item;
            });
            //set found data in fields
            document.getElementById("change-to-username").innerHTML = "Hello, " + user.firstName;
            document.getElementById("name").value = user.firstName;
            document.getElementById("lastname").value = user.lastName;
            switch (user.age) {
                case 0:
                    document.getElementById("select").value = "Less than 18";
                    break;
                case 1:
                    document.getElementById("select").value = "18-30";
                    break;
                case 2:
                    document.getElementById("select").value = "30 and more";
                    break;
                default:
                    break;
            }
            /*if (user.visibility)
                document.getElementById("visibility-main").value = true;
            else
                document.getElementById("visibility-main").checked = true;
                */
            alert(document.getElementById("visibility-main").checked);

            document.getElementById("account-balance").innerHTML = user.accountBalance + "$";
            document.getElementById("games-won-a").innerHTML = user.gamesWon + " ";
            document.getElementById("games-lost-a").innerHTML = user.gamesLost;
        }
    });
}

//UPDATE METODAS
function editItem() {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";
    var username = document.cookie.replace(/(?:(?:^|.*;\s*)Username\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    $.each(todos, function (key, item) {
        if (item.username === username) {
            $("#edit-name").val(item.name);
            $("#edit-id").val(item.id);
            $("#edit-isComplete")[0].checked = item.isComplete;
        }
    });
    $("#spoiler").css({ display: "block" });
}

function updateInformation() {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";
    var ages;
    switch (document.getElementById("select").value) {
        case "Less than 18":
            ages = 0;
            break;
        case "18-30":
            ages = 1;
            break;
        case "30 and more":
            ages = 2;
            break;
        default:
            ages = 0;
            break;
    }

    findCurrentUserById();

    Id = document.cookie.replace(/(?:(?:^|.*;\s*)Id\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    const item = {
        FirstName: $("#name").val(),
        LastName: $("#lastname").val(),
        Username: document.cookie.replace(/(?:(?:^|.*;\s*)Username\s*\=\s*([^;]*).*$)|^.*$/, "$1"),
        AccountBalance: $("account-balance").val(),
        GamesWon: $("games-won-a").val(),
        GamesLost: $("games-lost-a").val(),
        Age: ages,
        Visibility: $("#visibility-main").val()
    };

    $.ajax({
        url: uri + "/" + Id,
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        headers: { "Authorization": 'Bearer ' + token },
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
        }
    });
    
    return false;
}

function findCurrentUserById() {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        headers: { "Authorization": 'Bearer ' + token },
        success: function (data) {
            //get username from cookie
            var username = document.cookie.replace(/(?:(?:^|.*;\s*)Username\s*\=\s*([^;]*).*$)|^.*$/, "$1");
            //find the user
            $.each(data, function (key, item) {
                if (username == item.username)
                    document.cookie = "Id=" + item.id;
            });
        }
    });
}