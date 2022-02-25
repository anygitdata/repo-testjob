const elHtml = (($) => {

    /*
     * Attributes:
     * FOR divSelected:
     * filename = "..."
     * divid = "Guid"
     * divsel="on"
     * 
     * FOR btnNavigates:
     * in -> div class="col-auto
     * attr btnSeleted
     * id = "Guid"
     */

    let use_VerfData = 'on'  // on -> frondEnd verify data else backEnd
    const Debug = $('#anyData_Debug').val()

    const TaskId = $('#anyData_TaskId').val()
    const maxSizeFile = $('#anyData_maxSizeFile').val()

    const btn_dlg_save = $('#btn-dlg-save')
    const btn_dlg_open = $('#btn-dlg-open')
    const btn_close = $('.btn-close')

    const btn_userIntf_add = $('#btn-userIntf-add')
    const btn_userIntf_upd = $('#btn-userIntf-upd')
    const btn_userIntf_del = $('#btn-userIntf-del')

    let btn_sel_default = $('.btn-sel-default'); // button navigator


    const lb_cont_type = $('.lb-cont-type')
    const ContentType = $('#ContentType')
    const IdComment = $('#IdComment')

    let postedFile = $('#postedFile')
    const sId_postedFile = '#postedFile'

    const Content = $('#Content')

    const div_testarea = $('.div-testarea');
    const div_postedFile = $('.div-postedFile')
    const div_message_err = $('.message-err')
    const div_dlg_strFile = $('.div-dlg-strFile')
    const div_comment = $('.div-comment')
    const div_content_type = $('.div-content-type')
    const div_Modal = $('#div-Modal')    
    const div_modal_footer = $('.modal-footer')
    const div_main_comments = $('.main-comments')
    const div_btn_navig = $('.div-btn-navig')


    const span_dlg_strFile = $('.span-dlg-strFile')


    function AddClass_Disabled(arg) {

        if (arg == true) {
            btn_userIntf_upd.addClass('disabled')
            btn_userIntf_del.addClass('disabled')
        }
        else {
            btn_userIntf_upd.removeClass('disabled')
            btn_userIntf_del.removeClass('disabled')
        }
    }
    

    // -----------------------------
    return {
        use_VerfData, 

        AddClass_Disabled,

        IdComment, TaskId, maxSizeFile,

        btn_dlg_save, btn_dlg_open, btn_close,

        btn_sel_default,
        btn_userIntf_add, btn_userIntf_upd, btn_userIntf_del,
        div_Modal, div_btn_navig,
        div_main_comments,

        span_dlg_strFile,

        lb_cont_type,
        ContentType,
        postedFile, sId_postedFile,
        Content,

        div_testarea, div_postedFile,
        div_message_err, div_dlg_strFile,
        div_comment, div_content_type,        
        div_modal_footer,

        Debug
    }

})(jQuery); 
// elHtml


const buttonFunctions = (($, el) => {

    function CheckData() {
        const check = el.ContentType

        if (check.is(':checked'))
            return true
        else
            return false
    }

    function Get_idFromForm() {
        const idData = $('form').attr('idData')

        return idData
    }

    function Get_firstButtonNavigator() {

        if (el.div_btn_navig.children().length > 1) {
            return el.div_btn_navig.children().first().find('button')
        }

        el.AddClass_Disabled(true)


        return null
    }

    // --------------------------------------

    el.btn_userIntf_add.click(() => {

        if (CheckData() == false)
            el.ContentType.click()


        el.div_message_err.empty()

        el.div_postedFile.hide()
        el.div_dlg_strFile.hide()

        el.div_content_type.show()
        el.div_testarea.show()

        el.div_modal_footer.find('span').text('Add comment')
        el.div_testarea.removeClass('disabled')

        el.Content.val('')
        el.span_dlg_strFile.text('')

        $('form').attr('idData', '').attr('typeOper', 'add')

        el.div_Modal.modal('show')
        el.TypeOperations = ''
    })


    el.btn_userIntf_upd.click(() => {

        const div = $("[divsel='on']")
        const id = div.attr('divid')
        const fileName = div.attr('fileName')
        const content = div.children('.div-comment').text()


        el.btn_userIntf_add.click()

        el.postedFile.val('')

        $('form').attr('idData', id).attr('typeOper','upd')


        if (fileName != '') {
            if (CheckData() == true)
                el.ContentType.click()

            el.div_dlg_strFile.show()
            el.span_dlg_strFile.text(fileName)
        }
        else {
            if (CheckData() == false)
                el.ContentType.click()

            el.div_dlg_strFile.hide()
        }

        el.div_content_type.hide()
        el.div_postedFile.hide();
        el.div_testarea.show()
        el.div_modal_footer.find('span').text('Update comment')

        el.Content.val(content)
    })


    el.btn_userIntf_del.click(() => {
        el.btn_userIntf_upd.click()
        el.div_testarea.addClass('disabled')
         
        const id = Get_idFromForm()

        $('form').attr('idData', id).attr('typeOper', 'del')

        el.div_modal_footer.find('span').text('Remove comment')
    })


    function Click_default(e) {
        const divCur = $("div[divsel='on']")
        if (divCur.length > 0) {
            el.btn_sel_default.removeClass('btn-sel')
            divCur.attr('divsel', 'off').addClass('div-comn-hide')
        }


        const btnNav = $(e.currentTarget).removeClass('btn-not-sel').addClass('btn-sel')

        const id = btnNav.prop('id')

        const divid = "div[divid='##']".replace('##', id)
        $(divid).attr('divsel', 'on').removeClass('div-comn-hide')

    }
    

    el.ContentType.click(() => {

        el.div_dlg_strFile.hide()
        el.div_content_type.show()

        if (CheckData() == true) {
            el.div_postedFile.hide()
            el.div_testarea.show()
            el.lb_cont_type.text('Into file')

        }
        else {
            el.div_postedFile.show()
            el.div_testarea.hide()
            el.lb_cont_type.text('Into database')
        }
    })


    // initBase state  
    el.ContentType.click()



    // ----------------------------
    return {
        CheckData, Get_idFromForm,
        Get_firstButtonNavigator, Click_default
    }

})(jQuery, elHtml);


// data processing
const ProcBlock = (($, el, bf, bv) => {

    const typeFile = 'text/plain'
    const maxSizeFile = 400

    const Err_NotTextFile = 'This is not a text file'
    const Err_BigFile = 'big file'
    const Err_NotData_forContent = 'Not data for Content'
    const Err_PostFile_notData = 'postedFile not data'

    // ------------------------------

    function MessageErr(err) {

        const div = el.div_message_err.empty()

        if (el.Debug == 'on')
            console.log(err)

        div.append(err)
    }

    function SelectedItem(arg) {

        const attr_divsel = "div[divsel='on']"
        const divSel = $(attr_divsel)

        if (arg == 'div')
            return divSel
        else
            return $('#' + divSel.attr('divid'))
    }

    function GetData() {

        const typeOper = $('form').attr('typeOper')
        const checkData = bf.CheckData()
        
        if (typeOper == 'del') {
            return {
                TypeId: el.TaskId,
                IdComment : bf.Get_idFromForm()
            }
        }

        if (typeOper == 'upd') {
            const selDiv = SelectedItem('div')

            return {
                IdComment : bf.Get_idFromForm(),
                Content: el.Content.val(),
                ContentType: selDiv.attr('filename').length>0 ? false : true
            }
        }


        if (typeOper == 'add') {

            if (checkData == false) {
                const file = $(el.sId_postedFile)[0].files[0] // el.postedFile[0].files[0]
                if (file == undefined)
                    el.Content.val('')
                else
                    el.Content.val(file.name)
            }
                        

            res = new FormData($('form')[0])
            res.append('TaskId', el.TaskId)

            res.set('ContentType', checkData)

            if (checkData == true)
                res.delete('postedFile')


            return res
        }

    }

    function VerfData() {

        if (el.use_VerfData == 'off')  // Disabling verification
            return bv.ok


        el.div_message_err.empty()

        const typeOper = $('form').attr('typeOper')


        if (typeOper == 'del')
            return bv.ok


        if (typeOper == 'upd') {

            if (el.Content.val().trim() == '') {
                MessageErr(Err_NotData_forContent)
                return bv.error
            }

            return bv.ok
        }

        if (typeOper == 'add') {

            if (el.TaskId == '') {
                MessageErr('Not data for TaskId')
                return bv.error
            }

            if (bf.CheckData() == false) {
                if (el.postedFile.val() == '') {
                    MessageErr(Err_PostFile_notData)
                    return bv.error
                }

                const file = el.postedFile[0].files[0]
                if (file.type != typeFile) {
                    MessageErr(Err_NotTextFile)
                    return bv.error
                }

                if (file.size > maxSizeFile) {
                    MessageErr(Err_BigFile)
                    return bv.error
                }
            }
            else {

                if (el.Content.val().trim() == '') {
                    MessageErr(Err_NotData_forContent)
                    return bv.error
                }
            }

            return bv.ok
        }

    }


    el.btn_dlg_save.click(() => {

        const typeOper = $('form').attr('typeOper')

        if (typeOper == undefined || typeOper == '')
            return

        switch (typeOper) {
            case 'add':
                AddItem()
                break
            case 'upd':
                UpdItem()
                break
            case 'del':
                DelItem()
                break
        }


    })


    // block ajax handlers 
    function AddItem() {
        // in VerfData -> if (el.use_VerfData == 'off') 

        if (VerfData() == bv.error)
            return

        const data = GetData()
        //const form = $('form')[0]
        //const fd = new FormData(form)
        //fd.append('TaskId', el.TaskId)
        
        const url = '/api/descr'

        $.ajax(url, {
            data: data,
            method: 'POST',

            processData: false,
            contentType: false,

            success: function (data) {
                After_responseAdd(data)
                }
            }
        )
    }


    function UpdItem() {
        if (VerfData() == bv.error)
            return

        const url = '/api/descr'

        const data = GetData()

        $.ajax(url, {
            data: data,
            method: 'PUT',
            success: function (data) {
                After_responseUpd(data)
                }
            }
        )
    }


    function DelItem() {

        if (VerfData() == bv.error)
            return


        const id = bf.Get_idFromForm()
        //const url = '/deldescr/' + id

        const url = '/api/descr/' + id

        $.ajax(url, {
            method: 'DELETE',
            success: function (data) {
                After_responseDel(data)
                }
            }
        )
    }

    // end block ajax handlers 


    // block response ajax
    function After_responseAdd(data) {

        if (data.result == bv.error) {
            if (el.Debug == 'on') {
                console.group('------- Error After_responseAdd -------')
                console.log(data.idComment)
                console.log('result:', data.result)
                console.log('message:', data.message)
                console.groupEnd()
            }

            MessageErr(data.message)

            return
        }


        const div = $('<div style="background: #f8dd91;"></div>')
            .addClass('col-10 div-comn-hide border-top border-bottom border-dark p-2')
            .attr('fileName', data.strFileName)
            .attr('divid', data.idComment)
            .attr('divsel', 'off')

        if (data.strFileName != '' ) {
            const templSpan = '<span><b>fileName</b>: ##file</span><div class="py-1"></div>'
                    .replace('##file', data.strFileName)
            div.append($(templSpan))
        }

        const templDivComn = '<div class="div-comment">##comment</div>'.replace('##comment', data.content)
        const divComn = $(templDivComn)
        div.append(divComn)


        const divBtn = $('<div class="col-auto px-1"></div>')
        const numCount = el.div_btn_navig.children().length
        const btn = $('<button class="btn-sel-default btn-sm border border-info rounded-circle">##</button>'
            .replace('##', numCount)).attr('id', data.idComment)

        divBtn.append(btn)


        el.div_main_comments.append(div)
        divBtn.insertBefore('.last-column')

        el.div_Modal.modal('hide')
        el.btn_sel_default = $('.btn-sel-default')  // btn array update !!!

        btn.click()


        if (el.Debug == 'on') {
            console.group('------- After_responseAdd -------')
            console.log(data.idComment)
            console.log('result:', data.result)
            console.log('message:', data.message)
            console.groupEnd()
        }

    }


    function After_responseUpd(data) {

        if (data.result == bv.error) {
            if (el.Debug == 'on' && bf.CheckData() == false) {
                console.group('------- After_responseUpd -------')
                console.log(data.idComment)
                console.log('result:', data.result)
                console.log('message:', data.message)
                console.groupEnd()

                MessageErr(data.message)
                return
            }

            // обработка данные тестирования
            if (el.Debug == 'on' && bf.CheckData() == true) {
                data.result = 'ok'
                MessageErr(data.message)
            }
        }


        const div = SelectedItem('div')
        div.find('.div-comment').text(data.content)

        el.div_Modal.modal('hide')


        if (el.Debug == 'on') {
            console.group('------- After_responseUpd -------')
            console.log(data.idComment)
            console.log('result:', data.result)
            console.log('message:', data.message)
            console.groupEnd()
        }
    }


    function After_responseDel(data) {

        if (data.result == bv.error) {

            if (el.Debug == 'on') {
                console.group('------- After_responseDel -------')
                console.log(data.idComment)
                console.log('result:', data.result)
                console.log('message:',data.message)
                console.groupEnd()
            }

            MessageErr(data.message)

            return
        }


        el.div_Modal.modal('hide')

        SelectedItem('btn').parent().remove()
        SelectedItem('div').remove()

        const btnNav = bf.Get_firstButtonNavigator()

        if (btnNav != null)
            btnNav.click()

        if (el.Debug == 'on') {
            console.group('------- After_responseDel -------')
            console.log(data.idComment)
            console.log('result:', data.result)
            console.log('message:', data.message)
            console.groupEnd()
        }

    }

    // End of ajax response block 


    // ---------------------------------------

    return {
        GetData, VerfData, typeFile,
        maxSizeFile, MessageErr,
        Err_NotTextFile, Err_BigFile,
        Err_NotData_forContent, Err_PostFile_notData,

        After_responseAdd, After_responseUpd, After_responseDel
    }

})(jQuery, elHtml, buttonFunctions, baseValues);



// settings for start
((el, bf) => {

    $('.div-btn-navig').on('click', '.btn-sel-default', bf.Click_default);

    // base settings
    $('#div-Modal').modal('hide')


    const btnNav = bf.Get_firstButtonNavigator()
    if (btnNav != null)
        btnNav.click()
    

})(elHtml, buttonFunctions);

