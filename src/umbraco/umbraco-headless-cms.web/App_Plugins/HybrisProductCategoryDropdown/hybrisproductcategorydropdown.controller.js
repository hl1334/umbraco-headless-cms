angular.module("umbraco").controller("HybrisProductCategoryDropdown", function ($scope, hybrisProductCategoryResource) {
  $scope.onload = function () {
    hybrisProductCategoryResource.getAll().then(function (response) {
      var jsonResponse = response.Data;

      if (jsonResponse.Error) {
        $scope.error = true;
        $scope.errorMessage = jsonResponse.ErrorMessage;
      } else {
        $scope.error = false;
        $scope.hybrisProductCategories = jsonResponse;
        $scope.model.selectedHybrisProductCategory = $scope.model.value;
      }
    });
  };

  $scope.hybrisProductCategoryChange = function () {
    $scope.model.value = $scope.model.selectedHybrisProductCategory;
  };

  $scope.onload();
});
