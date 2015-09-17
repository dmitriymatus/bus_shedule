        $(document).ready(function () {
            GetMap();
        });
 
    // Функция загрузки
        function GetMap()
        {
 
        var Brest = new google.maps.LatLng(52.09755, 23.68775);
        google.maps.visualRefresh = true;
        // установка основных координат
        if (navigator.geolocation)
        {
            navigator.geolocation.getCurrentPosition(function (x)
            {
                Brest = new google.maps.LatLng(x.coords.latitude, x.coords.longitude);
                SetMap(Brest);
            });
        }
        else
        {
            SetMap(Brest);
        }
        }
        
        function SetMap(Brest)
        {
        // Установка общих параметров отображения карты, как масштаб, центральная точка и тип карты
        var mapOptions = {
            zoom: 15,
            center: Brest,
            mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
        };
 
        // Встраиваем гугл-карты в элемент на странице и получаем объект карты
        var map = new google.maps.Map(document.getElementById("canvas"), mapOptions);
 
        // Настраиваем красный маркер, который будет использоваться для центральной точки
        var myLatlng = Brest;
 
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: 'Остановки автобусов'
        });
 
        // Берем для маркера иконку с сайта google
        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png')
 
        // Получаем данные
        $.getJSON("/Map/GetData", function (data) {
            // Проходим по всем данным и устанавливаем для них маркеры
            $.each(data, function (i, item) {
                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(item.GeoLong, item.GeoLat),
                    map: map,
                    title: item.Name
                });
 
                // Берем для этих маркеров синие иконки с сайта google
                marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')
 
                // Для каждого объекта добавляем доп. информацию, выводимую в отдельном окне
                var infowindow = new google.maps.InfoWindow({
                    content: "<div class='stationInfo'><h2>Станция " + item.Name + "</h2></div>"
                });
 
                // обработчик нажатия на маркер объекта
                google.maps.event.addListener(marker, 'click', function ()
                {
                    var stopNames = $('#stopName option');
                    stopNames.each(function ()
                    {
                        if (this.text == marker.title)
                        {
                            $("#stopName :contains(" + marker.title + ")").attr("selected", "selected");
                           
                            var aaa = google.maps.geometry.spherical.computeDistanceBetween(Brest, marker.position);
                            $("#info").text = "";
                            $("#info").append("Расстояние: " + Math.round(aaa) + " м.");

                            $.getJSON("/Map/GetInfo", GetInfo());

                        }
                    });

                });
            })
        });
        }


        function GetInfo()
        {

        }