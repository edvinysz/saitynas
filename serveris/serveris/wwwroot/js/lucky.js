const uri = "../api/luckynumber";
let todos = null;
function getCount(data) {
    const el = $("#counter");
    let text = "item1";
    if (data) {
        if (data > 1) {
            text = "items";
        }
        el.text(data + " " + text);
    } else {
        el.text("No " + text);
    }
}

$(document).ready(function () {
    getData();
});

//GET METODAS
function getData() {
    //var token = document.cookie.replace(/(?:(?:^|.*;\s*)Token\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDM0OTEzMTAsImV4cCI6MTU0NDA5NjExMCwiaWF0IjoxNTQzNDkxMzEwfQ.t0H6saKWr8KPyEfXgiUBiipTFgWl5N-NsjQ5mCefo5s";
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        headers: { "Authorization": 'Bearer ' + token },
        success: function (data) {
            //document.getElementById('grid-div').innerHTML = '<ul class="cards">';
            var html = '<ul class="cards">';
            var i = 1;
            $.each(data, function (key, item) {
                html = html + '<li><h2>' + 'Lucky Number #' + item.id + '</h2><h6>' + item.date + '</h6><p>' + item.number + '</p>' + '<input name="submit" src="https://www.freeiconspng.com/uploads/blue-delete-button-png-2.png" type="image" class="btn btn-delete" onclick="deleteItem(' + item.id + ');" /></input>' + '</li>';
            });
            html = html + '</ul>';
            document.getElementById('grid-div').innerHTML = html;
        }
    });
}

//CREATE NEW METODAS
function updateInformation() {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";

    findCurrentUserById();

    Id = document.cookie.replace(/(?:(?:^|.*;\s*)Id\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        url: uri + "/" + Id,
        type: "POST",
        accepts: "application/json",
        contentType: "application/json",
        headers: { "Authorization": 'Bearer ' + token },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            location.reload();//parent.location.reload();
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

//DELETE METODAS
function deleteItem(id) {
    //var token = document.cookie.replace(/(?:(?:^|.*;\s*)Token\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDM0OTEzMTAsImV4cCI6MTU0NDA5NjExMCwiaWF0IjoxNTQzNDkxMzEwfQ.t0H6saKWr8KPyEfXgiUBiipTFgWl5N-NsjQ5mCefo5s";
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        headers: { "Authorization": 'Bearer ' + token },
        success: function (result) {
            location.reload();
        }
    });
}