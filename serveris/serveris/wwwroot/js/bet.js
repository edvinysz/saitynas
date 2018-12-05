const uri = "../api/bet";
let todos = null;

$(document).ready(function () {
    getData();
});

//GET METODAS
function getData() {
    //var token = document.cookie.replace(/(?:(?:^|.*;\s*)Token\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";
    findCurrentUserById();
    var userid = document.cookie.replace(/(?:(?:^|.*;\s*)Id\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        headers: { "Authorization": 'Bearer ' + token },
        success: function (data) {
            //document.getElementById('grid-div').innerHTML = '<ul class="cards">';
            var html = '<ul class="cards">';
            $.each(data, function (key, item) {
                if (item.personId == userid) {
                    if (item.hasWon == 0)
                        html = html + '<li><h2>Game ID:' + item.gameId + '</h2><h6>Your bet: ' + item.betMoney + '$ <br><br> You can win! ' + item.possibleWinMoney + '$</h6><p>' +
                            '<div class="form-group row" id="submit-button-main"><div class="offset-4 col-8"><input name="submit2" src="http://pluspng.com/img-png/delete-button-png-delete-button-png-image-28560-600.png" type="image" class="btn btn-bet-1" onclick="deleteItem(' + item.id + ');" /></input></div></div></form>'
                            + '</p></li>';
                    else
                        html = html + '<li>' +
                            '<h2>Game ID:' + item.gameId + '</h2><h6>Your bet: ' + item.betMoney + '$ <br><br> You can win! ' + item.possibleWinMoney + '$</h6><p>' +
                            '<svg height="60" width="200"><ellipse cx="100" cy="30" rx="100" ry="30" style="fill:darkblue;stroke:white;stroke-width:2" /></svg><a>COMPLETED</a>'
                            + '</p></li>';
                }
            });
            html = html + '</ul>';
            document.getElementById('grid-div').innerHTML = html;
        }
    });
}

//delete bet
//DELETE METODAS
function deleteItem(id) {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";

    $.ajax({
        url: uri + "/" + id,
        headers: { "Authorization": 'Bearer ' + token },
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function findCurrentUserById() {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";
    $.ajax({
        type: "GET",
        url: "../api/user",
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