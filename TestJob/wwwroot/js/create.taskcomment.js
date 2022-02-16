
const elHtml = (($) => {

    //const btn_dlg_close = $('#btn-dlg-close')

    const Debug = $('#Debug').val()
    const TaskId = $('#TaskId').val()
    const TypeOperations = $('#TypeOperations').val()

    const btn_dlg_save = $('#btn-dlg-save')
    const btn_dlg_open = $('#btn-dlg-open')
    const btn_close = $('.btn-close')

    const btn_drm_getdata = $('#btn-drm-getdata')
    const btn_drm_verfdata = $('#btn-drm-verfdata')
    const btn_drm_save = $('#btn-drm-save')
    const btn_drm_postFile_empty = $('#btn-drm-postFile-empty')
    const btn_drm_base_param = $('#btn-drm-base-param')


    const lb_cont_type = $('.lb-cont-type')
    const ContentType = $('#ContentType')
    const IdComment = $('#IdComment')

    const postedFile = $('#postedFile')
    const Content = $('#Content')

    const div_testarea = $('.div-testarea');
    const div_postedFile = $('.div-postedFile')
    const div_message_err =  $('.message-err')

    // -----------------------------
    return {
        IdComment, TaskId, TypeOperations,

        btn_dlg_save, btn_dlg_open, btn_close,

        btn_drm_getdata, btn_drm_verfdata, btn_drm_save,
        btn_drm_postFile_empty, btn_drm_base_param,

        lb_cont_type,
        ContentType,
        postedFile,
        Content,
        div_testarea, div_postedFile, div_message_err,

        Debug
    }

})(jQuery);


const buttonFunctions = (($, el) => {

    // base setting
    //if (el.ContentType.is(':checked')) {
    //    el.div_testarea.show()
    //    el.lb_cont_type.text('Into file')
    //}
    //else {
    //    el.div_testarea.hide()
    //    el.lb_cont_type.text('Into database')
    //}

    // -------------------------------------

    function CheckData() {
        const check = el.ContentType

        if (check.is(':checked'))
            return true
        else
            return false
    }


    el.btn_dlg_open.click((e) => {
        //console.log('click for btn_dlg_open')
    });

    

    el.btn_close.click((e) => {
        //console.log('click for btn_dlg_close')
    });

    el.ContentType.click(() => {
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


    return { CheckData}

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

        if (el.Debug == 'True')
            console.log(err)

        div.append(err)
    }

    el.btn_dlg_save.click(SaveData)


    function GetData() {

        const files = el.postedFile[0].files
        let file = null

        if (files.length > 0)
            file = files[0].name

        const res = {
            IdComment: el.IdComment.val(),
            TypeOperations: el.TypeOperations,
            ContentType: el.ContentType.val(),
            Content:""
        }

        if (bf.CheckData() == true)
            res.Content = el.Content.val()
        else {
            if (file != null) {
                res.Content = file
                res.postedFile = el.postedFile.val()
            }
        }

        return res
    }


    function VerfData() {

        el.div_message_err.empty()

        if (bf.CheckData() == true && el.Content.val().trim() == '') {
            MessageErr(Err_NotData_forContent)
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

        return bv.ok
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

