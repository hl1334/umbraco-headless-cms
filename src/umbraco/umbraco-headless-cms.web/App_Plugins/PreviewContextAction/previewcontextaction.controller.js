angular.module("umbraco").controller("PreviewContextAction", function($scope, $window, editorState) {

    var dialogOptions = $scope.dialogOptions;
    var metaData = dialogOptions.currentAction.metaData;   
    $scope.websiteRootUrl = metaData.websiteRootUrl;
    $scope.pageName = editorState.current.name;

    $scope.previewDate = {
        model: null,
        existingValue: null,
        hasValue: false
    };

    function buildDateTimePickerModel(alias, label, description) {
        return {
            editor: "Umbraco.DateTime",
            label: label,
            description: description,
            hideLabel: false,
            view: "datepicker",
            alias: alias,
            value: null,
            validation: {
                mandatory: false,
                pattern: ""
            },
            config: {
                format: "YYYY-MM-DD",
                pickDate: true,
                pickTime: false,
                useSeconds: false
            }
        };
    };

    $scope.previewDate.model =
        buildDateTimePickerModel("previewDate", "Preview Date", "Select an optional preview date");

    $scope.openPreviewWindow = function() {
        var url = metaData.websiteRootUrl + "/" + metaData.pageId + "?preview=true";
        if ($scope.previewDate.model.value) {
            url += "&date=" + $scope.previewDate.model.value;
        }
        url += "&token=" + metaData.previewToken;
        $window.open(url, "_blank");
    };
});