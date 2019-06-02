angular.module('umbraco.resources').factory('hybrisProductCategoryResource',
  function ($q, $http, umbRequestHelper) {
    return {
      getAll: function () {
        return umbRequestHelper.resourcePromise(
          $http.get("backoffice/CustomEditors/HybrisProductCategoryApi/GetAll")
        );
      }
    };
  }
);
