
const elHtml = (($, bv) => {

    const date = $('#Date')
    const time = $('#Time')


    bv.initDatePicker(date)
    bv.initTimePicker(time)


    return {

    }

})(jQuery, baseValues)

