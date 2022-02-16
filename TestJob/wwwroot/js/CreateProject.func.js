/*
 * Function block
 */
;(function ($, evObj) {

    let idModf = 'false';


    // Modifying User Content 
    function set_idModf(arg, start = false) {

        evObj.removeDisabled(evObj.projectName)
        evObj.div_dataError.empty()

        // CreateNewproject
        if (arg == 'false') {

            evObj.createDate.val('')
            evObj.createTime.val('')
            evObj.projectName.val('')

            evObj.updateDate.val('')
            evObj.updateTime.val('')

            evObj.removeDisabled(evObj.createDate)
            evObj.removeDisabled(evObj.createTime)

            evObj.addDisabled(evObj.updateDate)
            evObj.addDisabled(evObj.updateTime)

            evObj.exampleModalLabel.text('Create project')
        }

        // ModifyProject
        else {

            evObj.updateDate.val('')
            evObj.updateTime.val('')

            evObj.addDisabled(evObj.createDate)
            evObj.addDisabled(evObj.createTime)

            evObj.removeDisabled(evObj.updateDate)
            evObj.removeDisabled(evObj.updateTime)


            const ajaxRoute = evObj.ajaxRoute

            if (ajaxRoute == undefined) {

                const projectId = Get_projectId()
                const url = '/api/Rest/dataproject/' + projectId

                // Loading detail 
                $.ajax(url, {
                    type: 'GET',
                    success: function (data) {
                        if (data.result == 'ok') {

                            // Buffer creation ajaxRoute
                            $('body').data('ajaxRoute', data)
                            evObj.ajaxRoute = $('body').data('ajaxRoute')

                            set_id_uploadData_ajaxRoute()
                        }
                    }

                })
            }
            else {
                set_id_uploadData_ajaxRoute()
            }
        }

        idModf = arg

        if (!start) {
            $('.contentTableJob').hide()
            $('#formDialog').show(100)
            $('#btn-AddProject').hide()
        }

    }

    // Detail of the project used 
    function set_id_uploadData_ajaxRoute() {

        const ajaxRoute = evObj.ajaxRoute

        evObj.projectName.val(ajaxRoute.projectName)
        evObj.createDate.val(ajaxRoute.date)
        evObj.createTime.val(ajaxRoute.time)

        evObj.exampleModalLabel.text('Modify project')
    }

    function getData() {

        let obj = new Object()

        obj.projectName = evObj.projectName.val()

        if (idModf == 'false') {
            obj.date = evObj.createDate.val()
            obj.time = evObj.createTime.val()
            obj.projectId = ''
        }
        else {
            obj.projectId = evObj.projectId.val()
            obj.date = evObj.updateDate.val()
            obj.time = evObj.updateTime.val()
        }

        return obj
    }

    function verifyExists() {

        const prName = evObj.projectName.val()

        if (prName == '') {
            evObj.div_dataError.empty()
            showMessageError('Not data for projectName')
            return true
        }

        showMessageError('')
        const url = '/api/Rest/checkNameproject'
        const data = { strParam: prName }

        let result

        $.ajax(url, {
            type: 'GET',
            async: false,
            data: data,
            success: function (data) {
                if (data.response == 'exist')
                    result = true
                else
                    result = false
            }
        })

        return result
    }

    function verify() {

        const data = getData()

        $('.dataError').empty()

        if (get_idModf() == 'false') {
            if (data.projectName == '') {
                showMessageError('Not data for projectName')
                return "err"
            }

            if (data.date == '') {
                showMessageError('Not data for createDate')
                return "err"
            }

            if (data.time == '') {
                showMessageError('Not data for createTime')
                return "err"
            }


            let resExists = verifyExists()

            if (resExists == true) {
                showMessageError('project id is used')
                return "err"
            }

        }
        else {
            if (data.date == '' || data.time == '' || data.projectName == '') {
                showMessageError('Fill in the form fields')
                return "err"
            }

            const dataAjax = evObj.ajaxRoute
            if (dataAjax.date == data.date && dataAjax.time == data.time) {
                showMessageError('Data has not changed')
                return "err"
            }

        }

        return "ok"
    }

    function showMessageError(err) {
        const div = evObj.div_dataError.empty()
        const ul = $('<ul></ul>')
        const li = $('<li></li>')

        if (err != '') {
            li.text(err)

            ul.append(li)
            div.append(ul)
        }

    }

    function Get_projectId() {
        return evObj.projectId.val()
    }

    function get_idModf() {
        return idModf// $('#btnDropdown-mentu').attr('idModf')
    }

    // Project Close ID 
    function Get_idUpdate() {
        return $('#content_TableModel_idUpdate').val()
    }

    // UI update 
    function updateAfterSave() {

        const index = $('.dropdown-item').length + 1
        const projName = evObj.projectName.val()
        const a = $('<a class="dropdown-item " href="/Home/index/' + index + '">' + projName + '</a > ')

        $('.dropdown-menu').append(a)
    }

    function updateAfterModf() {
        // only projectName
        const modfProjectName = evObj.projectName.val()
        $('#spanSelProject').val(modfProjectName)
    }

    // ------------------------------------

    evObj.set_idModf = set_idModf
    evObj.get_idModf = get_idModf
    evObj.getData = getData
    evObj.verify = verify
    evObj.verifyExists = verifyExists
    evObj.showMessageError = showMessageError
    evObj.Get_projectId = Get_projectId
    evObj.Get_idUpdate = Get_idUpdate
    evObj.updateAfterSave = updateAfterSave
    evObj.updateAfterModf = updateAfterModf

}(jQuery, EventHandl));