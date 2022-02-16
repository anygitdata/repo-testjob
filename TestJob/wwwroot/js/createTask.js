
const elHtml = (($, bv) => {
        
    const projectId = $('#projectId')
    const TaskId = $('#TaskId')
    const OperTask = $('#OperTask')

    const TypeOperModf = $('#TypeOperModf')

    const dateCreate = $('#dateCreate')
    const timeCreate = $("[name='timeCreate']")

    const startDate = $('#StartDate')
    const startTime = $('#StartTime')

    const TaskName = $('#TaskName');

    const divMessage = $('.divMessage');
    const div_btn_details = $('#div-btn-details')
    const div_detailsStart = $('#divDetails-start')


    const divCont_create_task = $('#divCont-create-task')
    const div_short_data_task = $('#div-short-data-task')
    const div_shortData_start = $('.shortData-start')


    const spanShortData_nameTask = $('#div-short-data-task .nameTask')
    const spanShortData_taskData = $('#div-short-data-task .taskData')
    const spanShortData_taskTime = $('#div-short-data-task .taskTime')

    const spanShortData_tasStartkData = $('#div-short-data-task .taskStartData')
    const spanShortData_tasStartkTime = $('#div-short-data-task .taskStartTime')

    

    const btn_save = $('#btn-save')
    const btn_filling_details = $('#btn-filling-details')


    bv.initDatePicker(dateCreate)
    bv.initTimePicker(timeCreate)

    bv.initDatePicker(startDate)
    bv.initTimePicker(startTime)

    // -----------------------------
    return {
        projectId, TaskId, 

        TypeOperModf, TaskName, OperTask,
        spanShortData_nameTask, spanShortData_taskData, spanShortData_taskTime,
        spanShortData_tasStartkData, spanShortData_tasStartkTime,

        dateCreate,
        timeCreate,
        startDate,
        startTime,
        
        divMessage,
        div_btn_details,
        div_detailsStart,
        divCont_create_task,
        div_short_data_task,
        div_shortData_start,

        btn_save,
        btn_filling_details
    }


})(jQuery, baseValues);


const userInterf = (($, el, bv) => {

    function Message(mes) {
        const div = el.divMessage.empty()

        div.append(mes);
    }

    function verData_Create() {
        if (el.TaskName.val() == '') {
            Message('Fill in the task field ')
            return bv.error
        }

        if (el.dateCreate.val() == '' || el.timeCreate.val() == '') {
            Message('Date or time fields not filled ')
            return bv.error
        }

        return bv.ok
    }

    function verData_Start() {
        if (el.startDate.val() == '' || el.startTime.val() == '') {
            Message('Date or time fields not filled ')
            return bv.error
        }
    }

    function verData() {

        let res = bv.error;

        switch (el.OperTask.val()) {
            case bv.enTsk_create:
                res = verData_Create();
                break;
            case bv.enTsk_start:
                res = verData_Start()
                break;
        }

        return res; 
    }

    function getData() {

        let res = {
            dateCreate: el.dateCreate.val(),
            timeCreate: el.timeCreate.val(),
            projectId: el.projectId.val(),
            OperTask: el.OperTask.val()
        }

        if (el.OperTask.val() == bv.enTsk_create)
            res.TaskName = el.TaskName.val();

        if (el.OperTask.val() == bv.enTsk_start) {
            res.taskId = el.TaskId.val()
            res.startDate = el.startDate.val()
            res.startTime = el.startTime.val()
        }

        return res
    }

    function afterSave(data) {

        switch (data) {
            case bv.enTsk_start:
                el.div_btn_details.show()
                el.divCont_create_task.hide()

                el.spanShortData_nameTask.text(el.TaskName.val())
                el.spanShortData_taskData.text(el.dateCreate.val())
                el.spanShortData_taskTime.text(el.timeCreate.val())
                break

            case bv.enTsk_detail:
                el.spanShortData_tasStartkData.text(el.startDate.val())
                el.spanShortData_tasStartkTime.text(el.startTime.val())

                el.div_detailsStart.hide()
                el.div_shortData_start.show()
                break

        }

        el.div_short_data_task.show()

        el.OperTask.val(data)
        
    }


    // --------------------------------------------
    return { Message, verData, getData, afterSave }

})(jQuery, elHtml, baseValues);


// Event handlers 
(($, el, bv, uInt) => {
    el.btn_filling_details.click((e) => {
        e.preventDefault()

        el.div_btn_details.hide()
        el.div_detailsStart.show()

    });

    el.btn_save.click((e) => {
        e.preventDefault();

        if (uInt.verData() == bv.error)
            return

        const data = uInt.getData()

        $.ajax('/createtask', {
            type: 'POST',
            data: data,
            success: (data) => {
                if (data.result != bv.ok) {
                    uInt.Message(data.message)
                    return;
                }

                el.TaskId.val(data.taskId)

                uInt.Message('Task created')
                uInt.afterSave(data.str_operTask)
                
            }
        });


    });

})(jQuery, elHtml, baseValues, userInterf);