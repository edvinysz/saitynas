// Cookies for login
$(document).ready(function () {
    /*Cookies in .net
     * HttpCookie UserCookie = new HttpCookie("UserSettings");
    if (Request.Cookies["UserSettings"] != null)
        document.getElementById('login-link-js').click();
        */

    // promt to login always if not logged in
    if (document.cookie == "")
        document.getElementById('login-link-js').click();
    else // remove login button if logged in
        document.getElementById("login-link-js").remove();
    
});

function authorize() {
    var username = document.getElementById('username-placeholder').value;
    var password = document.getElementById('password-placeholder').value;
    postAuth(username, password);
}

//post to /authenticate to get JWT token
function postAuth(user, pass) {
    const item = {
        Username: user,
        Password: pass
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: "authenticate",
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Something went wrong!");
        },
        success: function (result) {
            //put cookies
            document.cookie = "Username=" + user;
            document.cookie = "Password=" + pass;
            //get token from json string
            const data = JSON.stringify(result);
            const token = JSON.parse(data);
            document.cookie = "Token=" + token.token;

            //refresh page
            location.reload();
        }
    });
}

//set iframe source by onclick in html
function changeIframe(url) {
    document.getElementById('iframe-page-target').setAttribute('src', url);//.attr('src', url);
    calcHeightOfIframe();
    /*document.getElementById('iframe-page-target').style.height =
        document.getElementById('iframe-page-target').contentWindow.document.body.offsetHeight + 'px';
    alert($('iframe').height($('iframe').contents().height()));*/
}

function calcHeightOfIframe() {
        //find the height of the internal page
    var the_height = document.getElementById('iframe-page-target').contentWindow.
            document.body.scrollHeight;

        //change the height of the iframe
    document.getElementById('iframe-page-target').height =
            the_height + 100;
}
