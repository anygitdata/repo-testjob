const elHtml = (($, bv) => {

    // dislocation index.cshtml 
    // as hidden
    const numItem = $('#numItem').val()
    const debug = $('#debug').val()
    const idUpdate = $('#idUpdate').val()

    const selProjectId = $('#selProjectId').val()
    const selProjectName = $('#selProjectName').val()

    // user interface
    const btn_AddProject = $('#btn-AddProject')
    const btn_UpdProject = $('#btn-updProject')

    const btn_closeDialog = $('#btn-closeDialog')
    const btn_iconClose = $('button.close')
    const btn_saveData = $('#btn-save-changes')

    const div_contentTable = $('.contentTableJob')  // <div class="div_contentTable" 


    // dislocation _formDialog_add_upd_project.cshtml
    const test = $('#test').val()

    const typeOperation = $('#typeOperations')  // hidden change in TrigFormDialog(...)
    const projId = $('#projectId').val()        // hidden

    const projName = $('#dlgPr-projectName')

    const date = $('#dlgPr-date')
    const time = $('#dlgPr-time')
    const dateUpd = $('#dlgPr-dateUpd')
    const timeUpd = $('#dlgPr-timeUpd')

    const formDialog = $('#formDialog') // Main container  <div class="container"

    const div_dateTime_addProj = $('.fm-add-proj')  // div for addProject
    const div_dateTime_updProj = $('.fm-upd-proj')  // div for updProject
    const div_mesError = $('.dataError')            // div for messageError
    const Dlg_titleLabel = $('#dlg_titleLabel')     // modal-title


    // ------------ Switching constants -----------
    const strTypeOperAdd = 'insert'     // settings btn_AddProject.click
    const strTypeOperUpd = 'update'     // settings btn_UpdProject.click


    function MesError(arg) {
        div_mesError.empty().append(arg)

        if (debug == 'on')
            console.log(arg)
    }


    // Interface settings trigger
    function TrigFormDialog(arg, typeOper) {

        // reset visibility options 
        typeOperation.val(typeOper)
        div_dateTime_addProj.hide()
        div_dateTime_updProj.hide()

        // dialog display 
        if (arg == true) {
            formDialog.show()
            div_contentTable.hide()
        }
        else {
            formDialog.hide()
            div_contentTable.show()
        }

        // form element settings 
        if (typeOper == strTypeOperAdd) {
            div_dateTime_addProj.show()
            Dlg_titleLabel.text('Create project')

            if (projName.val().length > 0)
                projName.val('')

        }
        else {
            if (projName.val == '')
                projName.val(projName_selected)

            div_dateTime_updProj.show()
            Dlg_titleLabel.text('Update project')
        }
    }


    bv.initDatePicker(date)
    bv.initTimePicker(time)

    // Initial settings
    if (idUpdate == 'on' || numItem == 0) {
        btn_UpdProject.addClass('disabled')
    }
    else {        
        bv.initDatePicker(dateUpd)
        bv.initTimePicker(timeUpd)
    }


    btn_iconClose.click(() => {
        btn_closeDialog.click()
    })

    btn_closeDialog.click(() => {
        TrigFormDialog(false, '')
    })

    btn_UpdProject.click((e) => {
        TrigFormDialog(true, strTypeOperUpd)
        typeOperation.val()

        projName.val(selProjectName)

        date.val('')
        time.val('')
    })

    btn_AddProject.click((e) => {
        projName.val('')
        date.val('')
        time.val('')
        TrigFormDialog(true, strTypeOperAdd)
    })
   
    // --------------------------

    return {
        MesError,
        projId, projName,
        date, time, dateUpd, timeUpd,
        typeOperation, strTypeOperAdd, strTypeOperUpd,
        TrigFormDialog,

        btn_AddProject, btn_UpdProject, btn_closeDialog,
        btn_iconClose, btn_saveData,

        formDialog, test, selProjectId, selProjectName
    }

})(jQuery, baseValues);


const addUpdProj = (($, el, bv) => {

    function VerifyData() {
        const data = GetData()

        if (data.ProjectName == '') {
            el.MesError('No projectName')
            return bv.error
        }

        if (data.Date == '' || data.Time == '') {
            el.MesError('Date or time fields not filled')
            return bv.error
        }

        return bv.ok
    }

    function GetData() {
        if (el.typeOperation.val() == el.strTypeOperAdd) {

            return {
                ProjectId : el.projId,
                ProjectName: el.projName.val(),
                Date : el.date.val(),
                Time : el.time.val(),
                TypeOperations : el.typeOperation.val()
            }
        }
        else {
            return {
                ProjectId: el.projId,
                ProjectName: el.projName.val(),
                Date: el.dateUpd.val(),
                Time: el.timeUpd.val(),
                TypeOperations: el.typeOperation.val()
            }
        }
    }

    // ----------------------------

    function TestData() {
        const data = GetData()
        let keys = Object.keys(data), k = 0

        console.group('------ getData -------')
        for (k; k < keys.length; k++) {
            console.log(keys[k] + ': ', data[keys[k]])
        }
        console.groupEnd()

        console.log('VerifyData: ', VerifyData())
    }


    el.btn_saveData.click((e) => {

        e.preventDefault()

        if (el.test == 'on') {
            TestData()
            return;
        }

        if (VerifyData() == bv.error)
            return


        if (el.typeOperation.val() == el.strTypeOperAdd)
            AddProject()
        else
            UpdProject()
    })

    function AddProject() {

        $.ajax('/api/projects', {
            type: 'POST',
            data: GetData(),
            success: function (data) {
                if (data.result == bv.error) {
                    el.MesError(data.message)

                    return
                }

                el.btn_closeDialog.click()
            }
        })
    }

    function UpdProject() {

        $.ajax('/api/projects', {
            type: 'PUT',
            data: GetData(),
            success: function (data) {
                if (data.result == bv.error) {
                    el.MesError(data.message)

                    return
                }

                el.btn_closeDialog.click()
            }
        })

    }


})(jQuery, elHtml, baseValues)

