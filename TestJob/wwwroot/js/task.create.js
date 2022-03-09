const elHtml = (($, bv) => {

    const projectId = $('#projectId')
    const projectName = $("#projectName").val()
    const debug = $("#debug").val()

    const date = $('#Date')
    const time = $('#Time')

    const dateExt = $('#DateExt')
    const timeExt = $('#TimeExt')

    const TaskName = $('#TaskName');

    const divMessage = $('.divMessage')

    const btn_save = $('#btn-save')
    const btn_dropdownProj = $('.dropdown-toggle')
    const btn_dropdownItem  = $('.dropdown-item')


    bv.initDatePicker(date)
    bv.initTimePicker(time)

    bv.initDatePicker(dateExt)
    bv.initTimePicker(timeExt)

    btn_dropdownItem.click((e) => {
        const el = e.currentTarget

        $('#spanSelProject').text(el.innerText)
        projectId.val(el.id)
    })


    // -------------------------

    return {
        projectId, projectName, debug,
        dateExt, date, timeExt, time, 
        divMessage,
        TaskName, btn_save, btn_dropdownProj,
    }

})(jQuery, baseValues);


const userInterf = ((el, bv) => {

    function Message(mes) {
        const div = el.divMessage.empty()

        div.append(mes);
    }

    function VerData() {

        const data = getData()

        if (data.projectId == '') {
            Message('Choose a project')
            return bv.error
        }

        if (data.TaskName == '') {
            Message('Fill in the task field')
            return bv.error
        }

        if (data.Date == '' || data.Time == '') {
            Message('Date or time fields not filled')
            return bv.error
        }

        if (data.DateExt == '' || data.TimeExt == '') {
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
            projectId: el.projectId.val(),
            TaskName: el.TaskName.val()
        }

        return res
    }


    el.btn_save.click((e) => {
        e.preventDefault();

        if (VerData() == bv.error)
            return


        $.ajax('/api/tasks', {
            data: getData(),
            type: 'POST',
            success: function (data) {

                if (data.result == bv.ok && data.redirect != '')
                    window.location.replace(data.redirect);
                else                
                    console.log('Result: ', data.result)
            }
        })

    })



})(elHtml, baseValues);