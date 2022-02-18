
/* test functionality
 * 
 */
;const FuncTest = (($, el, bf, pb, bv) => {

    el.btn_drm_getdata.click(() => {

        const typeOper = $('form').attr('typeOper')

        const AdditionalInf = {}
        const data = pb.GetData()

        if (typeOper == 'add') {

            if (bf.CheckData() == false) {

                const files = el.postedFile[0].files

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


    el.btn_drm_verfdata.click(() => {
        const res = pb.VerfData()
        if (res == bv.ok)
            console.log('verfData: ', res);
    })


    el.btn_drm_save.click(() => {
        pb.SaveData()
    })


    el.btn_drm_postFile_empty.click(() => {
        el.postedFile.val('')
        console.log('el.postedFile: ', el.postedFile.val())
    })


    el.btn_drm_base_param.click(() => {

        console.group('-------- BaseParam --------')

        console.log('Debug:', el.Debug)
        console.log('TaskId:', el.TaskId)        
        console.log('maxSizeFile:', el.maxSizeFile)

        console.groupEnd()
    })


})(jQuery, elHtml, buttonFunctions, ProcBlock, baseValues);