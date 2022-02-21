
/* test functionality
 * 
 */


;const htmlTest = (($, el) => {

    const Debug = el.Debug
    const TaskId = el.TaskId
    const maxSizeFile = el.maxSizeFile
    const postedFile = el.postedFile



    const btn_drm_getdata = $('#btn-drm-getdata')
    const btn_drm_verfdata = $('#btn-drm-verfdata')
    const btn_drm_save = $('#btn-drm-save')
    const btn_drm_postFile_empty = $('#btn-drm-postFile-empty')
    const btn_drm_base_param = $('#btn-drm-base-param')

    const btn_drm_data_ajaxAdd = $('#btn-drm-data_ajaxAdd')
    const btn_drm_data_ajaxUpd = $('#btn-drm-data_ajaxUpd')
    const btn_drm_data_ajaxDel = $('#btn-drm-data_ajaxDel')


    const after_response_add = $('#btn-drm-response-add')
    const after_response_upd = $('#btn-drm-response-upd')
    const after_response_del = $('#btn-drm-response-del')

    // --------------------
    return {

        Debug, TaskId, maxSizeFile, postedFile,

        btn_drm_getdata,
        btn_drm_verfdata,
        btn_drm_save,
        btn_drm_postFile_empty,
        btn_drm_base_param,

        btn_drm_data_ajaxAdd,
        btn_drm_data_ajaxUpd,
        btn_drm_data_ajaxDel,

        after_response_add, after_response_upd, after_response_del
    }

})(jQuery, elHtml)


const objTest = ((el, bf) => {

    function Get_data_ajaxAdd(strFileName, content) {

        const res = {
            idComment: "E0ABAE77-E3F2-4F63-AD59-9EAEB49AD282",
            Content: content,
            strFileName: strFileName
        }

        res.contentType = ! strFileName != ''

        return res
    }

    function Get_data_ajaxUpd(content) {

        const id = bf.Get_idFromForm()

        const res = {
            idComment: id,
            Content: content,            
        }

        return res
    }

    function Get_data_ajaxDel() {

        const id = bf.Get_idFromForm()

        const res = {
            idComment: id,
        }

        return res
    }

    // -------------------------------------

    return {
        Get_data_ajaxAdd, Get_data_ajaxUpd, Get_data_ajaxDel,
    }

})(elHtml, buttonFunctions)


;const FuncTest = (($, ht, bf, pb, bv, ot) => {

    ht.btn_drm_getdata.click(() => {

        const typeOper = $('form').attr('typeOper')

        const AdditionalInf = {}
        const data = pb.GetData()

        console.log(data)

        if (typeOper == 'add') {

            if (bf.CheckData() == false) {

                const files = ht.postedFile[0].files

                if (files.length > 0) {
                    AdditionalInf.size = files[0].size
                    AdditionalInf.type = files[0].type

                    if (AdditionalInf.type != pb.typeFile)
                        AdditionalInf.errorPostFile = pb.Err_NotTextFile
                    else
                        if (AdditionalInf.size > pb.maxSizeFile)
                            AdditionalInf.errorSize = pb.Err_BigFile
                }
            }

            if (bf.CheckData() == true && data.Content == '')
                AdditionalInf.errContent = pb.Err_NotData_forContent
            
        }

        let keys = Object.keys(data), k = 0, i=0

        console.group('------ getData -------')
        for (k; k < keys.length; k++) {
            console.log(keys[k] + ': ', data[keys[k]])
        }
        console.groupEnd()

        keys = Object.keys(AdditionalInf)

        if (keys.length > 0) {

            console.group('------ AdditionalInf -------')
            for (i; i < keys.length; i++ ) {
                console.log(keys[i] + ': ', AdditionalInf[keys[i]])
            }

            console.groupEnd()
        }

    })

    ht.btn_drm_verfdata.click(() => {
        const res = pb.VerfData()
        if (res == bv.ok)
            console.log('verfData: ', res);
    })

    ht.btn_drm_base_param.click(() => {

        console.group('-------- BaseParam --------')

        console.log('Debug:', ht.Debug)
        console.log('TaskId:', ht.TaskId)        
        console.log('maxSizeFile:', ht.maxSizeFile)

        console.groupEnd()
    })


    // ------------------------------ 
    ht.btn_drm_data_ajaxAdd.click(() => {
        const data = ot.Get_data_ajaxAdd('taskComment.txt', 'comment from testing procedure')
        const keys = Object.keys(data)

        console.group('-------- data_ajaxAdd --------')
        
        for (k = 0; k < keys.length; k++) {
            console.log(keys[k] + ': ', data[keys[k]])
        }

        console.groupEnd()

    })

    ht.btn_drm_data_ajaxUpd.click(() => {
        const data = ot.Get_data_ajaxUpd('comment modifed')
        const keys = Object.keys(data)

        console.group('-------- data_ajaxUpd --------')
        for (k = 0; k < keys.length; k++) {
            console.log(keys[k] + ': ', data[keys[k]])
        }
        console.groupEnd()
    })

    ht.btn_drm_data_ajaxDel.click(() => {

        const data = ot.Get_data_ajaxDel()
        const keys = Object.keys(data)

        console.group('-------- data_ajaxDel --------')
        for (k = 0; k < keys.length; k++) {
            console.log(keys[k] + ': ', data[keys[k]])
        }
        console.groupEnd()
    })


    // ------------------------
    ht.after_response_add.click((e) => {

        e.preventDefault()

        if ($('form').attr('typeOper') != 'add') {
            pb.MessageErr('Open add dialog ')

            return
        }

        const data = {
            result: 'ok',
            strFileName: 'Testing_file.txt',
            idComment: '79540886-EFDD-4CE0-988C-A9E5B2751221',
            content: 'Описание задачи из файла',
            message: 'ok'
        }

        pb.After_responseAdd(data)


        const data2 = {
            result: 'ok',
            strFileName:'',
            idComment: '2793071A-9759-46D5-8162-4BFA8F845391',
            content: 'Comment by procTesting',
            message: 'ok'
        }

        pb.After_responseAdd(data2)

    })

    ht.after_response_upd.click((e) => {

        e.preventDefault()

        const id = bf.Get_idFromForm()

        if (id == '' || $('form').attr('typeOper') != 'upd') {
            pb.MessageErr('Open update dialog ')

            return
        }
        
        const data = {
            result: 'ok',
            idComment: id,
            content: 'Update comment by procTesting',
            message: 'ok'
        }

        pb.After_responseUpd(data)
    })

    ht.after_response_del.click((e) => {

        e.preventDefault()

        const id = bf.Get_idFromForm()

        if (id == '' || $('form').attr('typeOper') != 'del') {
            pb.MessageErr('Open delete dialog ')

            return
        }

        const data = {
            idComment: id,
            result: 'ok',
            message: 'ok'
        }

        pb.After_responseDel(data)

    })


})(jQuery, htmlTest, buttonFunctions, ProcBlock, baseValues, objTest);