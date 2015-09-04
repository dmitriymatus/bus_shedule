

//обработчик события выбора автобуса
function selectNumber()
{
    var busNumber = document.getElementById("busNumber");
    if (busNumber.value != "") {
        $("#nodes").text("");
        $.getJSON("/Home/getStopsNames" + "?busNumber=" + encodeURIComponent(busNumber.value), null, getData);
    }
    else {
        $("#nodes").text("");
        $("#stopName").text("");
        $("#endStop").text("");
        $("#days").text("");
        if (document.getElementById("busContainer").hasAttribute("hidden") != true) {
            document.getElementById("busContainer").setAttribute("hidden", "")
        }
        if (document.getElementById("nodesContainer").hasAttribute("hidden") != true) {
            document.getElementById("nodesContainer").setAttribute("hidden", "")
        }
    }
}
function getData(result) {
    $("#nodes").text("");
    $("#stopName").text("");
    $("#stopName").append("<option>" + "</option>")
    $("#endStop").text("");
    $("#endStop").append("<option>" + "</option>")
    $("#days").text("");
    $("#days").append("<option>" + "</option>")
    if (document.getElementById("stopsContainer").hasAttribute("hidden") != true) {
        document.getElementById("stopsContainer").setAttribute("hidden");
    }
    if (document.getElementById("otherBusContainer").hasAttribute("hidden") != true) {
        document.getElementById("otherBusContainer").setAttribute("hidden");
    }
    $.each(result.Stops, function (i) { $("#stopName").append("<option>" + this + "</option>") })
    //$.each(result.Days, function (i) { $("#days").append("<option>" + this + "</option>") })
}

//---------------------------------------------------------------------------------------------
function selectAll() {
    var busNumber = document.getElementById("busNumber");
    var stopName = document.getElementById("stopName");
    var endStopName = document.getElementById("endStop");
    var days = document.getElementById("days");

    if (busNumber.value != "" & stopName.value != "" & endStopName.value != "" & days.value != "") {
        $.getJSON("/Home/getStops" + "?busNumber=" + encodeURIComponent(busNumber.value) + "&stopName=" + encodeURIComponent(stopName.value) + "&endStopName=" + encodeURIComponent(endStopName.value) + "&days=" + encodeURIComponent(days.value), null, GetNodes);
        if (document.getElementById("stopsContainer").hasAttribute("hidden") == true) {
            document.getElementById("stopsContainer").removeAttribute("hidden");
        }
    }
    else {
        if (document.getElementById("stopsContainer").hasAttribute("hidden") != true) {
            document.getElementById("stopsContainer").setAttribute("hidden");
        }
        $("#nodes").text("");
    }

}

function GetNodes(nodes) {
    $("#nodes").text("");
    if (nodes.length != 0) {
        $.each(nodes, function (i) { $("#nodes").append("<span class='breaks'>" + this + "</span> ") })
    }
    else {
        $("#nodes").append("<span class='breaks'> Нет рейсов</span>")
    }
}

//-----------------------------------------------------------------------------------------------

//обработчик события выбора остановки
function selectStop() {
    var stopName = document.getElementById("stopName");
    var busNumber = document.getElementById("busNumber");
    if (stopName.value != "") {
        $.getJSON("/Home/GetOtherBuses" + "?stopName=" + encodeURIComponent(stopName.value) + "&busNumber=" + encodeURIComponent(busNumber.value), null, GetOtherBuses);
        $.getJSON("/Home/GetFinalStops" + "?stopName=" + encodeURIComponent(stopName.value) + "&busNumber=" + encodeURIComponent(busNumber.value), null, GetFinalStops);
    }
    else {
        if (document.getElementById("busContainer").hasAttribute("hidden") == false) {
            document.getElementById("busContainer").setAttribute("hidden", "")
        }
    }
}

function GetFinalStops(stops) {

    $("#endStop").text("");
    $("#endStop").append("<option>" + "</option>")
    $.each(stops, function (i) { $("#endStop").append("<option>" + this + "</option>") })
}



function GetOtherBuses(buses) {
    if (buses != 0) {
        if (document.getElementById("otherBusContainer").hasAttribute("hidden") == true) {
            document.getElementById("otherBusContainer").removeAttribute("hidden");
        }

        $("#otherBuses").text("");
        $.each(buses, function (i) {
            var elem = document.createElement('span');
            elem.setAttribute('class', 'breaks');
            elem.innerHTML = this;
            elem.setAttribute("onclick", "selectOtherBusOnThisStop(" + "'" + this + "'" + ")");
            $("#otherBuses").append(elem);
            $("#otherBuses").append(" ");
        })
    }
    else {
        if (document.getElementById("otherBusContainer").hasAttribute("hidden") == false) {
            document.getElementById("otherBusContainer").setAttribute("hidden");
        }
    }
}


function selectFinalStop()
{
    var stopName = document.getElementById("stopName");
    var busNumber = document.getElementById("busNumber");
    var finalStopName = document.getElementById("endStop");
    $.getJSON("/Home/GetDays" + "?stopName=" + encodeURIComponent(stopName.value) + "&busNumber=" + encodeURIComponent(busNumber.value) + "&endStop=" + encodeURIComponent(finalStopName.value), null, GetDays);
}



function GetDays(days)
{
    $("#days").text("");
    $("#days").append("<option>" + "</option>")
    $.each(days, function (i) { $("#days").append("<option>" + this + "</option>") })
}
//---------------------------------------------------------------------------


//обработчик выбора другого автобуса на этой остановке
function selectOtherBusOnThisStop(_busNumber) {
    var val = this.value;
    $.getJSON("/Home/getBreaksNames" + "?busNumber=" + encodeURIComponent(_busNumber), null, GetNewData);
    var busNumber = document.getElementById("busNumber");
    for (i = 0; i < busNumber.length; i++) {
        if (busNumber.options[i].value == busNumber) {
            busNumber.options.selectedIndex = i;
        }
    }
}


function GetNewData(result) {
    $("#nodes").text("");
    var stopName = document.getElementById("stopName").value;
    $("#stopName").text("");
    $("#stopName").append("<option>" + "</option>")
    $("#endStop").text("");
    $("#endStop").append("<option>" + "</option>")
    $("#days").text("");
    $("#days").append("<option>" + "</option>")
    if (document.getElementById("nodesContainer").hasAttribute("hidden") != true) {
        document.getElementById("nodesContainer").setAttribute("hidden", "")
    }
    if (document.getElementById("busContainer").hasAttribute("hidden") != true) {
        document.getElementById("busContainer").setAttribute("hidden", "")
    }
    $.each(result.Stops, function (i) { $("#stopName").append("<option>" + this + "</option>") })
    //$.each(result.FinalBreak, function (i) { $("#endStop").append("<option>" + this + "</option>") })
    //$.each(result.Days, function (i) { $("#days").append("<option>" + this + "</option>") })
    var ttt = document.getElementById("stopName");
    for (i = 0; i < ttt.length; i++) {
        var eee = ttt.options[i].value;
        if (eee == stopName) {
            ttt.options.selectedIndex = i;
        }
    }
    selectStop();
}