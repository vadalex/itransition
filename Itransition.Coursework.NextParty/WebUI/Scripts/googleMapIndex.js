$(document).ready(function () {
    Initialize();
});

function Initialize() {

    var markers = new Array();
    var initLocation;    
    google.maps.visualRefresh = true;

    var mapOptions = {
        zoom: 14,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };
    var mapCanvas = document.getElementById('map_canvas');
    if (mapCanvas != null) {
        map = new google.maps.Map(mapCanvas, mapOptions);        
                       
        $.getJSON('Home/GetFutureEvents', function (data) {
            $.each(data, function (i, item) {
                markers[i] = new google.maps.Marker({
                    map: map,
                    draggable: false,
                    animation: google.maps.Animation.DROP,
                    position: new google.maps.LatLng(item.Longitude, item.Latitude),
                    title: item.Title
                });
                markers[i].setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png')

                var infowindow = new google.maps.InfoWindow({
                    content: "<div class='infoDiv'><h2>Title " + item.Title + "</h2><div><h4>Datetime: "
                        + item.DatetimeOfStart + "</h4></div><div><h4>Address: " + item.Address + "</h4></div></div>"
                });

                google.maps.event.addListener(markers[i], 'click', function () {
                    infowindow.open(map, markers[i]);
                });
            })
        });

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                initLocation = new google.maps.LatLng(position.coords.latitude,
                                                 position.coords.longitude);
                map.setCenter(initLocation);

               var marker = new google.maps.Marker({
                    map: map,
                    draggable: false,
                    animation: google.maps.Animation.DROP,
                    position: initLocation,
                    title: "You"
                });
                marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png')

                var infowindow = new google.maps.InfoWindow({
                    content: "<div><h2>It's you</h2></div>"
                });

                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
            }, function () {
                handleNoGeolocation(true);
            });
        } else {
            // Browser doesn't support Geolocation
            handleNoGeolocation(false);
        }

        function handleNoGeolocation(errorFlag) {
            if (errorFlag == true) {
                alert("Geolocation service failed.");
                initialLocation = newyork;
            } else {
                alert("Your browser doesn't support geolocation. We've placed you in Siberia.");
                initialLocation = siberia;
            }
        }
    }
}
