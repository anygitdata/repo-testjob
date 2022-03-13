
const baseValues = (($) => {
    const valFalse = 'False';
    const valTrue = 'True'

    const enTsk_create = 'create';
    const enTsk_start = 'start'
    const enTsk_detail = 'detail'
    const enTsk_update = 'update'
    const enTsk_cancel = 'cancel'

    const error = 'error'
    const ok = 'ok'

    const disabled = 'disabled'

    function initDatePicker(el) {
        el.datetimepicker({
            timepicker: false,
            format: 'Y-m-d',
        })
    }

    function initTimePicker(el) {
        el.timePicker()
    }


    function ObjStruct_intoConsole(data) {

        let keys = Object.keys(data), k = 0

        console.group('------ data -------')
        for (k; k < keys.length; k++) {
            console.log(keys[k] + ': ', data[keys[k]])
        }
        console.groupEnd()
    }

    // ---------------------------
    return {
        ObjStruct_intoConsole,
        enTsk_create, enTsk_start, enTsk_detail,
        enTsk_update, enTsk_cancel,

        valFalse, valTrue, disabled,
        error, ok,

        initDatePicker,
        initTimePicker
    }

})(jQuery);
