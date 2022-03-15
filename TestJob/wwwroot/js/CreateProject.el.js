﻿/*
 * Initializing HTML Elements 
 */

const EventHandl = (($, bv) => {

    const btn_deleteProject = $('#btn-deleteProject')
    const btn_modfProject = $('#btn-modfProject')
    const btn_modalDlgSave = $('#btn-modalDlgSave')

    const contentTableJob = $('.contentTableJob')   // Main Content 

    const projectName = $("[name='projectName']")   // selector project

    const div_dataError = $('.dataError')

    const span_modal_delete = $('#span-modal-delete') // close dialog delete project


    const createDate = $("[name='createDate']")
    const createTime = $("[name='createTime']")
    const updateDate = $("[name='updateDate']")
    const updateTime = $("[name='updateTime']")

    //const projectId = $("#content_TableModel_projectId")
    const exampleModalLabel = $('#exampleModalLabel')

    const formDialog = $('#formDialog')
    const btn_closeFormDialog = $('#closeFormDialog')

    const resultDelete_project = $('#resultDelete-project')

    

    const button_Close = $('button.close')
    const btn_AddProject = $('#btn-AddProject')

    const modal_delProject =  $('#modal-delProject')

    let ajaxRoute = $('body').data('ajaxRoute')


    formDialog.hide()


    //---------- function block 
    const closeFormDialog = function () {
        $('#closeFormDialog').trigger('click')
    }
    const addDisabled = function (el) {
        el.addClass('disabled')
    }
    const removeDisabled = function (el) {
        el.removeClass('disabled')
    }


    // ---------------------------------------

    return {
        removeDisabled,
        addDisabled,
        ajaxRoute,

        projectName,
        createDate,
        createTime,
        updateDate,
        updateTime,
        span_modal_delete,
        div_dataError,
        formDialog,
        contentTableJob,

        btn_deleteProject,
        btn_modfProject,
        btn_modalDlgSave,
        button_Close,
        btn_AddProject,
        btn_closeFormDialog,

        modal_delProject,

        resultDelete_project,

        closeFormDialog,
        exampleModalLabel,

        projectId

    };

})(jQuery, baseValues);
