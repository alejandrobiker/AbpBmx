$(function () {
    var l = abp.localization.getResource("CompetencyEvaluator");
	
	var athleteService = window.competencyEvaluator.athletes.athlete;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: abp.appPath + "Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CompetencyEvaluator/Athletes/CreateModal",
        scriptUrl: abp.appPath + "Pages/CompetencyEvaluator/Athletes/createModal.js",
        modalClass: "athleteCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CompetencyEvaluator/Athletes/EditModal",
        scriptUrl: abp.appPath + "Pages/CompetencyEvaluator/Athletes/editModal.js",
        modalClass: "athleteEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			dateOfBirthMin: $("#DateOfBirthFilterMin").val(),
			dateOfBirthMax: $("#DateOfBirthFilterMax").val(),
			genderId: $("#GenderIdFilter").val(),			categoryId: $("#CategoryIdFilter").val()
        };
    };
    
    //<suite-custom-code-block-1>
    //</suite-custom-code-block-1>

    var dataTable = $("#AthletesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(athleteService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('CompetencyEvaluator.Athletes.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.athlete.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('CompetencyEvaluator.Athletes.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    athleteService.delete(data.record.athlete.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "athlete.name" },
            {
                data: "athlete.dateOfBirth",
                render: function (dateOfBirth) {
                    if (!dateOfBirth) {
                        return "";
                    }
                    
					var date = Date.parse(dateOfBirth);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            {
                data: "gender.name",
                defaultContent : ""
            },
            {
                data: "category.name",
                defaultContent : ""
            }
        ]
    }));
    
    //<suite-custom-code-block-2>
    //</suite-custom-code-block-2>

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewAthleteButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        athleteService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/competency-evaluator/athletes/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'name', value: input.name },
                            { name: 'dateOfBirthMin', value: input.dateOfBirthMin },
                            { name: 'dateOfBirthMax', value: input.dateOfBirthMax }, 
                            { name: 'genderId', value: input.genderId }
, 
                            { name: 'categoryId', value: input.categoryId }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
    //<suite-custom-code-block-3>
    //</suite-custom-code-block-3>
    
    
});
