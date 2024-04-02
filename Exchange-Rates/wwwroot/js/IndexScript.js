setInterval(function () {
    updateFavList();
}, 60*60*1000);
var favCurrencies = JSON.parse(localStorage.getItem('favList'));
var result = favCurrencies == null ? {} : JSON.parse(localStorage.getItem('favList')) ;
var addToFav = function (name, rate) {
    
    if (!result.hasOwnProperty(name)) {
        document.getElementById("star_" + name).classList.remove("bi-star");
        document.getElementById("star_" + name).classList.add("bi-star-fill");
        result[name] = rate;
        localStorage.setItem('favList', JSON.stringify(result));
        addToFavTable(name, rate);
    } else {
        document.getElementById("star_" + name).classList.remove("bi-star-fill");
        document.getElementById("star_" + name).classList.add("bi-star");
        var favJSON = localStorage.getItem('favList');
        var favObj = JSON.parse(favJSON);
        delete favObj[name];
        var updatedFavJSON = JSON.stringify(favObj);
        localStorage.setItem('favList', updatedFavJSON);
    }
}


var updateFavList = function(){
    if (result && Object.keys(result).length > 0) {
        var array = Object.keys(result);
        sendRequest(array);
    }
    location.reload();
    //console.log(localStorage.getItem('favList'))
}
function sendRequest(cArray) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "https://localhost:7019/Home/GetUpdatedInfo", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var response = JSON.parse(xhr.responseText);
            const convertedPayload = {};
            for (const item of response) {
                const { currency, rate } = item;
                convertedPayload[currency] = rate.toString();
            }
            localStorage.clear();
            localStorage.setItem('favList', JSON.stringify(convertedPayload));
            //console.log(convertedPayload);
        }
    };
    xhr.send(JSON.stringify(cArray));
}
if (result != null) {
    for (item in result) {
        addToFavTable(item, result[item]);
    }
    var elements = document.querySelectorAll('i');
    elements.forEach(function (e) {
        for (post in result) {
            var nameId = "star_" + post;
            if (e.id == nameId) {
                e.classList.remove("bi-star");
                e.classList.add("bi-star-fill");
            }
        }
    });
}
function addToFavTable(item, value) {
    var newRow = document.createElement("tr");
    var table = document.getElementById("add");

    var cell1 = createRow(item);
    newRow.appendChild(cell1);

    var cell2 = createRow(value);
    newRow.appendChild(cell2);

    table.querySelector("tbody").appendChild(newRow);

}
function createRow(data) {
    var cell = document.createElement("td");
    cell.textContent = data;
    return cell;
}

