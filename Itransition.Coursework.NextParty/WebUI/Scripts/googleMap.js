$(document).ready(function () {
    Initialize();
});

function Initialize() {

    var marker;
    var initLocation;
    var infowindow;
    google.maps.visualRefresh = true;    

    var mapOptions = {
        zoom: 14,        
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };
    var mapCanvas = document.getElementById('map_canvas');
    if (mapCanvas != null) {
        map = new google.maps.Map(mapCanvas, mapOptions);        
        geocoder = new google.maps.Geocoder();

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                initLocation = new google.maps.LatLng(position.coords.latitude,
                                                 position.coords.longitude);
                var addressLabel = document.getElementById('Address');

                var lat = document.getElementById('Latitude');
                var lng = document.getElementById('Longitude');
                lat.value = initLocation.lat();
                lng.value = initLocation.lng();

                geocoder.geocode({ latLng: initLocation }, function (responses) {
                    if (responses && responses.length > 0) {
                        addressLabel.value = responses[0].formatted_address;                        
                    } else {
                        alert('Error: Google Maps could not determine the address of this location.');
                    }
                });

                marker = new google.maps.Marker({
                    map: map,
                    draggable: true,
                    animation: google.maps.Animation.DROP,
                    position: initLocation,
                    title: "Event location"
                });
                marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
                map.setCenter(initLocation);

                infowindow = new google.maps.InfoWindow({
                    content: "<div class='infoDiv'></div>"
                });

                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });

                google.maps.event.addListener(marker, 'click', function () {
                    geocoder.geocode({ latLng: marker.getPosition() }, function (responses) {
                        if (responses && responses.length > 0) {
                            var addressLabel = document.getElementById('Address');
                            addressLabel.value = responses[0].formatted_address;
                            var lat = document.getElementById('Latitude');
                            var lng = document.getElementById('Longitude');
                            lat.value = marker.getPosition().lat()
                            lng.value = marker.getPosition().lng()
                            infowindow.setContent(
                            "<div class=\"infoDiv\">" + responses[0].formatted_address
                            + "<br />"
                            + "Latitude: " + marker.getPosition().lat() + "&nbsp"
                            + "Longitude: " + marker.getPosition().lng() + "</div>"
                            );
                        } else {
                            alert('Error: Google Maps could not determine the address of this location.');
                        }
                    });
                    map.panTo(marker.getPosition());
                });

                google.maps.event.addListener(marker, 'dragstart', function () {
                    infowindow.close(map, marker);
                });

                google.maps.event.addListener(marker, 'dragend', function () {
                    geocoder.geocode({ latLng: marker.getPosition() }, function (responses) {
                        if (responses && responses.length > 0) {
                            var addressLabel = document.getElementById('Address');
                            addressLabel.value = responses[0].formatted_address;
                            var lat = document.getElementById('Latitude');
                            var lng = document.getElementById('Longitude');
                            lat.value = marker.getPosition().lat()
                            lng.value = marker.getPosition().lng()
                        } else {
                            alert('Error: Google Maps could not determine the address of this location.');
                        }
                    });
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
