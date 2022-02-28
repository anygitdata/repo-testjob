const elHtml = (($, bv) => {

    const projName_selected = $('#spanSelProject').text()
    const projId = $('#projectId').val()
    const numItem = $('#numItem').val()
    const debug = $('#debug').val()
    const idUpdate = $('#idUpdate').val()

    const strTypeOperAdd = 'insert'
    const strTypeOperUpd = 'update'

    const projName = $('#ProjectName')

    const date = $('#Date')
    const time = $('#Time')
    const dateUpd = $('#DateUpd')
    const timeUpd = $('#TimeUpd')

    const typeOperation = $('#typeOperations')

    const btn_AddProject = $('#btn-AddProject')
    const btn_UpdProject = $('#btn-updProject')

    const btn_closeDialog = $('#btn-closeDialog')
    const btn_iconClose = $('button.close')

    const btn_saveData = $('#btn-save-changes')

    const formDialog = $('#formDialog')

    const div_contentTable = $('.contentTableJob')
    const div_dateTime_addProj = $('.fm-add-proj')
    const div_dateTime_updProj = $('.fm-upd-proj')
    const div_mesError = $('.dataError')


    const Dlg_titleLabel = $('#dlg_titleLabel')


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


    // Initial settings 
    if (numItem > 0) {
        bv.initDatePicker(date)
        bv.initDatePicker(dateUpd)

        bv.initTimePicker(time)
        bv.initTimePicker(timeUpd)
    }

    if (idUpdate == 'on' || numItem == 0)
    {
        btn_AddProject.addClass('disabled')
        btn_UpdProject.addClass('disabled')
    }


    // --------------------------

    return {
        MesError, 
        projName_selected, projId, projName, 
        date, time, dateUpd, timeUpd,
        typeOperation, strTypeOperAdd, strTypeOperUpd,
        TrigFormDialog,

        btn_AddProject, btn_UpdProject, btn_closeDialog,
        btn_iconClose, btn_saveData,

        formDialog,
    }

})(jQuery, baseValues)


// Button Handlers 
;(($, el) => {
    el.btn_iconClose.click(() => {
        el.btn_closeDialog.click()
    })

    el.btn_closeDialog.click(() => {
        el.TrigFormDialog(false, '')
    })


    el.btn_UpdProject.click((e) => {
        el.TrigFormDialog(true, el.strTypeOperUpd)
        el.typeOperation.val()
    })

    el.btn_AddProject.click((e) => {
        el.TrigFormDialog(true, el.strTypeOperAdd)
    })

})(jQuery, elHtml)


const addUpdProj = (($, el, bv) => {

    function VerifyData() {
        const data = GetData()

        if (data.ProjectName == '') {
            el.MesError('No projectName')
            return bv.error
        }

        if (el.typeOperation.val() == el.strTypeOperAdd) {
            if (el.date.val() == '' || el.time.val() == '') {
                el.MesError('Date or time fields not filled')
                return bv.error
            }

        }
        else {
            if (el.dateUpd.val() == '' || el.timeUpd.val() == '') {
                el.MesError('Date or time fields not filled')
                return bv.error
            }
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

    el.btn_saveData.click((e) => {

        e.preventDefault()

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

                el.MesError(data.message)
            }
        })
    }

    function UpdProject() {
        console.log(GetData())
    }


})(jQuery, elHtml, baseValues)

