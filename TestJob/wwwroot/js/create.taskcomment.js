
const elHtml = (($) => {

    //const btn_dlg_close = $('#btn-dlg-close')

    const Debug = $('#anyData_Debug').val()
    const TaskId = $('#anyData_TaskId').val()
    const TypeOperations = $('#TypeOperations')
    const maxSizeFile = $('#anyData_maxSizeFile').val()

    const btn_dlg_save = $('#btn-dlg-save')
    const btn_dlg_open = $('#btn-dlg-open')
    const btn_close = $('.btn-close')

    const btn_userIntf_add = $('#btn-userIntf-add')
    const btn_userIntf_upd = $('#btn-userIntf-upd')
    const btn_userIntf_del = $('#btn-userIntf-del')


    const btn_sel_default = $('.btn-sel-default'); // button navigator


    const lb_cont_type = $('.lb-cont-type')
    const ContentType = $('#ContentType')
    const IdComment = $('#IdComment')

    const postedFile = $('#postedFile')
    const Content = $('#Content')

    const div_testarea = $('.div-testarea');
    const div_postedFile = $('.div-postedFile')
    const div_message_err = $('.message-err')
    const div_dlg_strFile = $('.div-dlg-strFile')
    const div_comment = $('.div-comment')
    const div_content_type = $('.div-content-type')
    const div_Modal = $('#div-Modal')    
    const div_modal_footer = $('.modal-footer')
    const div_comments = $('.div-comments')
    const div_btn_navig = $('.div-btn-navig')


    const span_dlg_strFile = $('.span-dlg-strFile')


    // -----------------------------
    return {
        IdComment, TaskId, TypeOperations, maxSizeFile,

        btn_dlg_save, btn_dlg_open, btn_close,

        btn_sel_default,
        btn_userIntf_add, btn_userIntf_upd, btn_userIntf_del,
        div_Modal, div_comments, div_btn_navig,

        span_dlg_strFile,

        lb_cont_type,
        ContentType,
        postedFile,
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
        const id = idData.substring(0, idData.length)

        return id
    }

    function Get_firstButtonNavigator() {

        return el.div_btn_navig.children().first().find('button')
    }



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
    })


    el.btn_userIntf_upd.click(() => {

        const div = $("[idSelComn='on']")
        const divId = div.prop('id')
        const id = divId.substring(4, divId.length)
        const fileName = div.attr('fileName')
        const content = div.children('.div-comment').text()


        el.btn_userIntf_add.click() // base settings

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


    // button navigator
    el.btn_sel_default.bind('click', (e) => {
        const btn = $(e.currentTarget)
        const btn_id = btn.prop('id')
        const id = btn.prop('id').substring(4, btn_id.length)

        const idDiv = 'div-' + id;
        const divComn = $('#' + idDiv);

        el.btn_sel_default.removeClass('btn-sel')
        btn.removeClass('btn-not-sel')

        btn.addClass('btn-sel')

        // div-72511B9B-45BE-445F-9635-1B62C6DAF625

        const div = $("[idSelComn='on']").attr('idSelComn', 'off')
        div.addClass('div-comn-hide')

        divComn.attr('idSelComn', 'on')
        divComn.removeClass('div-comn-hide')

    })
    

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


    return {
        CheckData, Get_idFromForm, Get_firstButtonNavigator
    }

})(jQuery, elHtml);


// data processing
;const ProcBlock = (($, el, bf, bv) => {

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

    el.btn_dlg_save.click(SaveData)


    function GetData() {

        const typeOper = $('form').attr('typeOper')
                
        if (typeOper == 'del') {
            return {
                TypeId: el.TaskId,
                IdComment : bf.Get_idFromForm()
            }
        }

        if (typeOper == 'upd') {
            return {
                TypeId: el.TaskId,
                IdComment : bf.Get_idFromForm(),
                Content: el.Content.val()
            }
        }

        if (typeOper == 'add') {

            const res = {
                TypeId: el.TaskId,                
                IdComment: '',
                ContentType: bf.CheckData()
            }

            if (bf.CheckData() == false) {
                const files = el.postedFile[0].files

                if (files.length > 0) {
                    res.Content = files[0].name
                }
            }
            else
                res.Content = el.Content.val()

            return res
        }

    }


    function VerfData() {

        el.div_message_err.empty()

        const typeOper = $('form').attr('typeOper')


        if (typeOper == 'del')
            return bv.ok

        if (el.TaskId == '') {
            MessageErr('Not data for TaskId')
            return bv.error
        }

        if (el.Content.val().trim() == '') {
            MessageErr(Err_NotData_forContent)
            return bv.error
        }


        if (typeOper == 'upd') {
            return bv.ok
        }


        if (typeOper == 'add') {

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

            return bv.ok
        }

    }


    function AddItem() {
        const data = GetData()

    }


    function DelItem() {

    }


    function updItem() {

    }


    function SaveData(e) {
        if (VerfData())
            return

    }


    return {
        GetData, VerfData, SaveData, typeFile,
        maxSizeFile, MessageErr,
        Err_NotTextFile, Err_BigFile,
        Err_NotData_forContent, Err_PostFile_notData
    }

})(jQuery, elHtml, buttonFunctions, baseValues);



// settings for start
;((el, bf) => {

    // base settings
    $('#div-Modal').modal('hide')

    if (el.div_btn_navig.children().length > 0) {
        bf.Get_firstButtonNavigator().click()
    }
    else {
        el.btn_userIntf_upd.addClass('disabled')
        el.btn_userIntf_del.addClass('disabled')
    }

})(elHtml, buttonFunctions);

