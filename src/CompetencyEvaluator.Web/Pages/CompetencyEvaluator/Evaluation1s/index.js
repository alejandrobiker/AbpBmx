$(function () {
    var l = abp.localization.getResource("CompetencyEvaluator");
	
	var evaluation1Service = window.competencyEvaluator.evaluation1s.evaluation1;
	
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
        viewUrl: abp.appPath + "CompetencyEvaluator/Evaluation1s/CreateModal",
        scriptUrl: abp.appPath + "Pages/CompetencyEvaluator/Evaluation1s/createModal.js",
        modalClass: "evaluation1Create"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CompetencyEvaluator/Evaluation1s/EditModal",
        scriptUrl: abp.appPath + "Pages/CompetencyEvaluator/Evaluation1s/editModal.js",
        modalClass: "evaluation1Edit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            criterio_1_R1Min: $("#Criterio_1_R1FilterMin").val(),
			criterio_1_R1Max: $("#Criterio_1_R1FilterMax").val(),
			criterio_1_R2Min: $("#Criterio_1_R2FilterMin").val(),
			criterio_1_R2Max: $("#Criterio_1_R2FilterMax").val(),
			criterio_2_R1Min: $("#Criterio_2_R1FilterMin").val(),
			criterio_2_R1Max: $("#Criterio_2_R1FilterMax").val(),
			criterio_2_R2Min: $("#Criterio_2_R2FilterMin").val(),
			criterio_2_R2Max: $("#Criterio_2_R2FilterMax").val(),
			criterio_3_R1Min: $("#Criterio_3_R1FilterMin").val(),
			criterio_3_R1Max: $("#Criterio_3_R1FilterMax").val(),
			criterio_3_R2Min: $("#Criterio_3_R2FilterMin").val(),
			criterio_3_R2Max: $("#Criterio_3_R2FilterMax").val(),
			criterio_4_R1Min: $("#Criterio_4_R1FilterMin").val(),
			criterio_4_R1Max: $("#Criterio_4_R1FilterMax").val(),
			criterio_4_R2Min: $("#Criterio_4_R2FilterMin").val(),
			criterio_4_R2Max: $("#Criterio_4_R2FilterMax").val(),
			resultado_R1Min: $("#Resultado_R1FilterMin").val(),
			resultado_R1Max: $("#Resultado_R1FilterMax").val(),
			resultado_R2Min: $("#Resultado_R2FilterMin").val(),
			resultado_R2Max: $("#Resultado_R2FilterMax").val(),
			athleteId: $("#AthleteIdFilter").val()
        };
    };
    
    //<suite-custom-code-block-1>
    //</suite-custom-code-block-1>

    var dataTable = $("#Evaluation1sTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(evaluation1Service.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('CompetencyEvaluator.Evaluation1s.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.evaluation1.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('CompetencyEvaluator.Evaluation1s.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    evaluation1Service.delete(data.record.evaluation1.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "evaluation1.criterio_1_R1" },
			{ data: "evaluation1.criterio_1_R2" },
			{ data: "evaluation1.criterio_2_R1" },
			{ data: "evaluation1.criterio_2_R2" },
			{ data: "evaluation1.criterio_3_R1" },
			{ data: "evaluation1.criterio_3_R2" },
			{ data: "evaluation1.criterio_4_R1" },
			{ data: "evaluation1.criterio_4_R2" },
			{ data: "evaluation1.resultado_R1" },
			{ data: "evaluation1.resultado_R2" },
            {
                data: "athlete.name",
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

    $("#NewEvaluation1Button").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        evaluation1Service.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/competency-evaluator/evaluation1s/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'criterio_1_R1Min', value: input.criterio_1_R1Min },
                            { name: 'criterio_1_R1Max', value: input.criterio_1_R1Max },
                            { name: 'criterio_1_R2Min', value: input.criterio_1_R2Min },
                            { name: 'criterio_1_R2Max', value: input.criterio_1_R2Max },
                            { name: 'criterio_2_R1Min', value: input.criterio_2_R1Min },
                            { name: 'criterio_2_R1Max', value: input.criterio_2_R1Max },
                            { name: 'criterio_2_R2Min', value: input.criterio_2_R2Min },
                            { name: 'criterio_2_R2Max', value: input.criterio_2_R2Max },
                            { name: 'criterio_3_R1Min', value: input.criterio_3_R1Min },
                            { name: 'criterio_3_R1Max', value: input.criterio_3_R1Max },
                            { name: 'criterio_3_R2Min', value: input.criterio_3_R2Min },
                            { name: 'criterio_3_R2Max', value: input.criterio_3_R2Max },
                            { name: 'criterio_4_R1Min', value: input.criterio_4_R1Min },
                            { name: 'criterio_4_R1Max', value: input.criterio_4_R1Max },
                            { name: 'criterio_4_R2Min', value: input.criterio_4_R2Min },
                            { name: 'criterio_4_R2Max', value: input.criterio_4_R2Max },
                            { name: 'resultado_R1Min', value: input.resultado_R1Min },
                            { name: 'resultado_R1Max', value: input.resultado_R1Max },
                            { name: 'resultado_R2Min', value: input.resultado_R2Min },
                            { name: 'resultado_R2Max', value: input.resultado_R2Max }, 
                            { name: 'athleteId', value: input.athleteId }
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
