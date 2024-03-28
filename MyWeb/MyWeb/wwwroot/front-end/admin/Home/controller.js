var ctxfolderHome="/front-end/admin/Home"
var app = angular.module("MyApp", ["ngRoute"]);

app.controller("Ctrl_Main",function(){

});

app.factory('APIs', function ($http) {
    $http.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }
    var submitFormUpload = function (url, data, callback) {
        var req = {
            method: 'POST',
            url: url,
            headers: {
                'Content-Type': undefined
            },
            data: data
        }
        $http(req).then(callback);
    };
    return {
        GetAllTypeMarker:function(callback){
            $http.get("/api/Marker/GetAllTypeMarker").then(callback);
        },
    }
});
app.config(function ($routeProvider){
    $routeProvider.when('/', {
        templateUrl: ctxfolderHome + '/index.html',
        controller: 'index'
    })
});


app.controller("index",function($scope,APIs){
    $scope.model={
        "id": 0,
        "x": 0,
        "y": 0,
        "z": 0,
        "dgr": 1,
        "typeid": 0
      };
    const container = document.getElementById("popup");
    const content = document.getElementById("popup-content");
    const closer = document.getElementById("popup-closer");
    const overlay = new ol.Overlay({
        element: container,
        autoPan: {
          animation: {
            duration: 250,
          },
        },
        offset: [0, -30],
      });
    const map=new ol.Map({
        layers: [
          new ol.layer.Tile({
            source: new ol.source.OSM(),
          }),
        ],
        overlays: [overlay],
        target: "map",
        view: new ol.View({
          center: ol.proj.fromLonLat([106.0607825247514,9.330767250397628]),
          zoom: 15,
        }),
    });

    closer.onclick = function () {
        overlay.setPosition(undefined);
        closer.blur();
        return false;
    };
    // Create a vector source and layer to add points
    const vectorSource = new ol.source.Vector();

    map.on("click", function (evt) {
        // Xóa marker cũ nếu chưa lưu
        if ($scope.marker)
            if (!$scope.marker.getId())
            vectorSource.removeFeature($scope.marker);

        
        $scope.marker = map.forEachFeatureAtPixel(
          evt.pixel,
          (feature) => feature
        );

        if ($scope.marker) {
            $scope.coordinate = $scope.marker.getGeometry().getCoordinates();
            content.innerHTML =
                "<p>Coordinates:</p><code>" +
                ol.proj.toLonLat($scope.coordinate) +
                "</code>";
            overlay.setPosition($scope.coordinate);
        } else {
            //Lấy vị trí marker
            $scope.coordinate = evt.coordinate;
            //Tạo đối tượng marker
            $scope.marker = new ol.Feature({
                geometry: new ol.geom.Point($scope.coordinate)
            });
            //Set style marker
            var Icon=$scope.listType.filter(function(item) {
                return item.id === $scope.typeid;
            })
            if(Icon.length>0){
                $scope.marker.setStyle(new ol.style.Style({
                    image: Icon[0].StyleIcon
                }));
            }
            //Popup thông tin marker
            content.innerHTML =
                "<p>Coordinates:</p><code>" +
                ol.coordinate.toStringHDMS(ol.proj.toLonLat($scope.coordinate)) +
                "</code>";
            overlay.setPosition($scope.coordinate);
            //Thêm marker vảo bản đồ
            vectorSource.addFeature($scope.marker);
        }
        $scope.model.x=$scope.coordinate[0]
        $scope.model.y=$scope.coordinate[1]
        $scope.$apply()
    });
    $scope.id=0
    $scope.Save=function(){
        if($scope.marker){
            $scope.marker.setId($scope.id)
            $scope.id++;
        }
        console.log($scope.model);
    }
    $scope.setIcon=function(id){
        if($scope.marker){
            var Icon=$scope.listType.filter(function(item) {
                return item.id === $scope.typeid;
            })
            if(Icon.length>0){
                $scope.marker.setStyle(new ol.style.Style({
                    image: Icon[0].StyleIcon
                }));
            }
        }
    }
    $scope.init=function(){
        APIs.GetAllTypeMarker(function(rs){
            rs=rs.data;
            console.log(rs);
            $scope.listType=rs;
            $scope.listType.forEach(function(item){
                item.StyleIcon = new ol.style.Icon({
                    anchor: [0.5, 1],
                    src: item.icon, // Đường dẫn đến hình ảnh mặc định
                    scale: 0.8
                });
            })
            var vectorLayer = new ol.layer.Vector({
                source: vectorSource,
                style: new ol.style.Style({
                image: $scope.listType[0].StyleIcon,
                }),
            });
        
            map.addLayer(vectorLayer);
            $scope.typeid=1;
            $scope.setIcon(1);
        })
        // $scope.listType=[{
        //     id:1,
        //     title:"Character",
        //     Link:"/Image/TypeMarker/Character.png"
        // },
        // {
        //     id:2,
        //     title:"Background",
        //     Link:"/Image/TypeMarker/Background.png"
        // }]
        
        
        
    }

    $scope.init()
});