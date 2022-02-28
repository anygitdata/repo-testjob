const elHtml = (($, bv) => {

    const projName_selected = $('#spanSelProject').text()
    const numItem = $('#numItem').val()

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


    const formDialog = $('#formDialog')

    const div_contentTable = $('.contentTableJob')
    const div_dateTime_addProj = $('.fm-add-proj')
    const div_dateTime_updProj = $('.fm-upd-proj')

    const Dlg_titleLabel = $('#dlg_titleLabel')

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


    if (numItem>0) {
        bv.initDatePicker(date)
        bv.initDatePicker(dateUpd)

        bv.initTimePicker(time)
        bv.initTimePicker(timeUpd)
    }
    else {
        btn_AddProject.addClass('disabled')
        btn_UpdProject.addClass('disabled')
    }


    // --------------------------

    return {
        projName_selected,
        date, time, dateUpd, timeUpd, 
        typeOperation, strTypeOperAdd, strTypeOperUpd,
        TrigFormDialog, 
        btn_AddProject, btn_UpdProject, btn_closeDialog, btn_iconClose,

        formDialog,
    }

})(jQuery, baseValues)


const userInterface = (($, el) => {

    el.btn_iconClose.click(() => {
        el.btn_closeDialog.click()
    })

    el.btn_closeDialog.click(() => {
        el.TrigFormDialog(false, '')
    })

    // ---------------------------

    el.btn_UpdProject.click((e) => {
        el.TrigFormDialog(true, el.strTypeOperUpd)
        el.typeOperation.val()
    })

    el.btn_AddProject.click((e) => {
        el.TrigFormDialog(true, el.strTypeOperAdd)
    })




})(jQuery, elHtml)

