var app = angular.module('myApp', []);
app.controller('myController', ['$scope', '$http', function ($scope, $http) {
    //here http get method for get data from database
    $scope.chartData = [['Name', 'ReportsTo', 'tooltip']];
    $http.get('/employee/getChartData').then(function (response) {
        var newobject = [['Name', 'ReportsTo', 'tooltip']];
        angular.forEach(response.data, function (val) {
            newobject.push(
                [
                    {
                        v: val.EmployeeID.toString(),
                        f: '<div class="customBox"><div>' + (val.FirstName + ' ' + val.LastName) + '</div><div class="title">' + val.Title + '</div></div>'
                    },
                    (val.ReportsTo.toString() == "0" ? "" : val.ReportsTo.toString()),
                    (val.FirstName + ' ' + val.LastName)
                ]
            );

        })
        $scope.chartData = newobject;
    })
}])
app.directive('orgChart', function () {
    function link($scope, element, attrs) {
        var chart = new google.visualization.OrgChart(element[0]);
        $scope.$watch('chartData', function (value, oldvalue) {
            if (!value) {
                return;
            }
            var data = google.visualization.arrayToDataTable(value);
            var options = {
                'title': '',
                'allowHtml': true
            }
            chart.draw(data, options);
        })
    }
    return {
        link: link
    };
})