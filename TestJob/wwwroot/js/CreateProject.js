
// Initial settings
(function (evObj) {

    evObj.formDialog.hide()

    evObj.updateTime.timePicker()
    evObj.createTime.timePicker()

    evObj.updateDate.datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
    })

    evObj.createDate.datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
    })

    evObj.set_idModf('false', true)

    if (evObj.Get_projectId() == ''
        || evObj.Get_idUpdate() == 'true') {

        evObj.addDisabled(evObj.btn_modfProject)
        evObj.addDisabled(evObj.btn_deleteProject)

    }

}(EventHandl));


// Basic event handlers 
(function ($, evObj) {

    evObj.btn_closeFormDialog.click( (e)=> {
        evObj.formDialog.hide(100);
        evObj.contentTableJob.show(100);
        evObj.btn_AddProject.show();
    });

    evObj.button_Close.click( (e)=> {
        evObj.closeFormDialog();
    });

    evObj.btn_AddProject.click( (e)=> {
        evObj.set_idModf('false');
    });

    evObj.btn_modfProject.click( (e)=> {
        evObj.set_idModf('true');
    });


    // --------------- modal dialog delete project ----------------

    // Delete project
    $('#btn-modalDlgSave').click( (e)=> {
        evObj.resultDelete_project.text('Пробное сообщение');
    });

    // Заполнение контента модального диалога
    $('#modal-delProject').on('show.bs.modal', function (e) {
        const selProject = $('#spanSelProject').text();
        evObj.span_modal_delete.text(selProject);
    })

}(jQuery, EventHandl));


// save data project
(function ($, evObj) {

    $('#btn-save-changes').click( (e)=> {

        $('.dataError').empty();
        evObj.exampleModalLabel.text('Create project');

        if (evObj.verify() == "err") {
            e.preventDefault();
            return;
        }

        const idModf = evObj.get_idModf();

        const data = evObj.getData();
        const url = idModf == 'false' ? '/newproject' : '/modfroject';
        const type = idModf == 'false' ? 'POST' : 'PUT';

        $.ajax(url, {
            data: data,
            type: type,
            success: function (data) {

                if (data.result == 'ok') {

                    if (idModf == "false")
                        evObj.updateAfterSave();
                    else
                        evObj.updateAfterModf();

                    evObj.closeFormDialog();

                }
                else {

                    evObj.showMessageError(data.message);

                }
            },
            error: function (jqXHR, status, errorMes) {
                console.log(errorMes);
            }

        });

    });

}(jQuery, EventHandl));

