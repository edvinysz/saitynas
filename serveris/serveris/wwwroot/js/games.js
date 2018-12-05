const uri = "../api/game";
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
            //document.getElementById('grid-div').innerHTML = '<ul class="cards">';
            var html = '<ul class="cards">';
            $.each(data, function (key, item) {
                if(!item.isComplete)
                html = html + '<li><h2>' + item.firstTeamId + ' VS ' + item.secondTeamId + '</h2><h6>' + item.firstkof + ' : ' + item.secondkof + '</h3><p>' +
                    '<form id="form1"><div class="form-group row" ><input id="bet1" name="bet1" placeholder="Input your bet" class="form-control here" required="required" type="text"></div></div>' +
                    '<div class="form-group row" id="submit-button-main"><div class="offset-4 col-8"><input name="submit1" src="https://www.freepngimg.com/thumb/submit_button/25395-2-submit-button-thumb.png" type="image" class="btn btn-bet-1" onclick="betone(' + item.id + ', ' + item.firstkof + ');" /></input></div></div></form>' + 
                '<form id="form2"><div class="form-group row" ><input id="bet2" name="bet2" placeholder="Input your bet" class="form-control here" required="required" type="text"></div></div>' +
                    '<div class="form-group row" id="submit-button-main"><div class="offset-4 col-8"><input name="submit2" src="https://www.freepngimg.com/thumb/submit_button/25395-2-submit-button-thumb.png" type="image" class="btn btn-bet-1" onclick="bet2(' + item.id + ', ' + item.firstkof + ');" /></input></div></div></form>'
                    +'</p></li>';
            });
            html = html + '</ul>';
            document.getElementById('grid-div').innerHTML = html;
        }
    });
}

function betone(id, kof) {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDMxNTY5ODcsImV4cCI6MTU0Mzc2MTc4NywiaWF0IjoxNTQzMTU2OTg3fQ.1phhpO6n_UD6nngP6EEXXss9fwSnth-_B1KME1cKzqI";
    findCurrentUserById();

    Id = document.cookie.replace(/(?:(?:^|.*;\s*)Id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var bet = document.getElementById("bet1").value;
    //alert(id + " - " + Id + " - " + bet + " - " + kof);
    var win = bet * kof;

    const item = {
        GameId: id,
        PersonId: Id,
        ChosenId: 1,
        BetMoney: bet,
        PossibleWinMoney: win,
        HasWon: 0
    };

    $.ajax({
        url: "../api/bet",
        type: "POST",
        accepts: "application/json",
        contentType: "application/json",
        headers: { "Authorization": 'Bearer ' + token },
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            alert("You succesfully bet")
        }
    });

    return false;
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