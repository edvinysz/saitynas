const uri = "../api/news";
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
                html = html + '<li><h2>' + item.title + '</h2><h6>' + item.date + '</h3><p>' + item.text + '</p></li>';
            });
            html = html + '</ul>';
            document.getElementById('grid-div').innerHTML = html;
        }
    });
}

/*//POST METODAS
function addItem() {
    const item = {
        name: $("#add-name").val(),
        isComplete: false
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            getData();
            $("#add-name").val("");
        }
    });
}

//DELETE METODAS
function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

//UPDATE METODAS
function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $("#edit-name").val(item.name);
            $("#edit-id").val(item.id);
            $("#edit-isComplete")[0].checked = item.isComplete;
        }
    });
    $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function () {
    const item = {
        name: $("#edit-name").val(),
        isComplete: $("#edit-isComplete").is(":checked"),
        id: $("#edit-id").val()
    };

    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}*/