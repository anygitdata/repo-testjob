const elHtml = (($, bv) => {

    const projectId = $('#projectId').val()
    const projectName = $("#projectName").val()
    const debug = $("#debug").val()

    const date = $('#Date')
    const time = $('#Time')

    const dateExt = $('#DateExt')
    const timeExt = $('#TimeExt')

    const TaskName = $('#TaskName');

    const divMessage = $('.divMessage')

    const btn_save = $('#btn-save')


    bv.initDatePicker(date)
    bv.initTimePicker(time)

    bv.initDatePicker(dateExt)
    bv.initTimePicker(timeExt)


    // -------------------------

    return {
        projectId, projectName, debug,
        dateExt, date, timeExt, time, 
        divMessage,
        TaskName, btn_save,
    }

})(jQuery, baseValues);


const userInterf = (($, el, bv) => {

    function Message(mes) {
        const div = el.divMessage.empty()

        div.append(mes);
    }

    function VerData() {
        if (el.TaskName.val() == '') {
            Message('Fill in the task field ')
            return bv.error
        }

        if (el.date.val() == '' || el.time.val() == '') {
            Message('Date or time fields not filled')
            return bv.error
        }

        if (el.dateExt.val() == '' || el.timeExt.val() == '') {
            Message('DateTime fields begin task not filled')
            return bv.error
        }

        return bv.ok
    }


    function getData() {

        let res = {
            Date: el.date.val(),
            Time: el.time.val(),
            DateExt: el.dateExt.val(),
            TimeExt: el.timeExt.val(),
            projectId: el.projectId,
            TaskName: el.TaskName.val()
        }

        return res
    }


    el.btn_save.click((e) => {
        e.preventDefault();

        $.ajax('/api/tasks', {
            data: getData(),
            type: 'POST',
            success: function (data) {
                console.log('Result: ', data.result)
            }
        })

    })


})(jQuery, elHtml, baseValues);