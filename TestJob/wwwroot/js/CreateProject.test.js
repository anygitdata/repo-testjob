/*
* Designed for testing the creation dialog or changing the "Project"
*/

const objHtml = ( ($)=> {

    const txtOpen = 'open'
    const txtClose = 'close'

    let indLoading = false
    let indOpenClose = txtOpen

    const txt_btnOpen_panelTest = 'Open test panel '
    const txt_btnClose_panelTest = 'Close test panel '

    let div_detailTest = $('.div-detailTest')
    const div_panelTest = $('#div_panelTest')

    const ul = $('<ul></ul>')
    const li = $('<li></li>')
    let list_group_item
    let spanButtonList

    let btnTest_clear
    let btnTest_getData
    const btnLoad_panelTesting = $('#btnLoad-panelTesting')


    // ------------------------------------
    return {
        indLoading,
        indOpenClose,

        txt_btnOpen_panelTest,
        txt_btnClose_panelTest,
        txtOpen,
        txtClose,

        ul,
        li,
        list_group_item,

        div_detailTest,
        div_panelTest,

        spanButtonList,
        btnTest_clear,
        btnTest_getData,
        btnLoad_panelTesting
    }

})(jQuery);


// object for testing
const procTesting = ( (evObj, objHtml)=> {

    // ------------ Test procedures -------------

    const getData = () => {
        const lsData = evObj.getData()
        const div = objHtml.div_detailTest
        const ul = objHtml.ul.clone().addClass('ml-3')
        const li = objHtml.li

        li.clone().text('projectId: ' + lsData.projectId).appendTo(ul)
        li.clone().text('projectName: ' + lsData.projectName).appendTo(ul)
        li.text('data: ' + lsData.date).appendTo(ul)
        li.clone().text('time: ' + lsData.time).appendTo(ul)

        div.empty().append('<b>-- getData() --</b>').append(ul)
    }

    const verify = ()=> {
        const res = evObj.verify()

        objHtml.div_detailTest.empty().append('<b>-- verify --</b>').append('<br/> verify: ' + res)
    }

    const Get_projectId = ()=> {
        const res = evObj.Get_projectId()
        objHtml.div_detailTest.empty().append('<b>-- projectId --</b>').append('<br/> projectId: ' + res)
    }

    const Get_idUpdate = ()=> {
        const res = evObj.Get_idUpdate()
        objHtml.div_detailTest.empty().append('<b>-- Get_idUpdate --</b>').append('<br/> Get_idUpdate: ' + res)
    }

    const verifyExists = ()=> {
        const res = evObj.verifyExists()
        const div = objHtml.div_detailTest.empty()

        const sTempl = '<b>-- verifyExists --</b>'

        if (res == true)
            div.append(sTempl).append('<br/> Project ID is used')
        else
            div.append(sTempl).append('<br/> ID is free')
    }


    // End of test procedure block

    return {
        getData: getData,
        verify: verify,
        Get_projectId: Get_projectId,
        Get_idUpdate: Get_idUpdate,
        verifyExists: verifyExists
    }

})(EventHandl, objHtml);


// run test
(function (prTest, objHtml) {

    let objectSelTest = '' // Item from dropdown list  


    // --------------- Update button text 
    function updText_btnLoadPanelTesting(arg) {
        switch (arg) {
            case objHtml.txtOpen:
                objHtml.btnLoad_panelTesting.text(objHtml.txt_btnClose_panelTest)
                objHtml.indOpenClose = objHtml.txtOpen
                break

            case objHtml.txtClose:
                objHtml.btnLoad_panelTesting.text(objHtml.txt_btnOpen_panelTest)
                objHtml.indOpenClose = objHtml.txtClose
                break
        }

    }

    function list_group_item() {
        const test = $(this).text()

        objectSelTest = test
        objHtml.spanButtonList.text(test)
    }

    function btnTest_clear() {
        objHtml.div_detailTest.empty()
    }

    // Run selected test 
    function btnTest_getData() {

        const arg = objectSelTest.slice(5).trim()
        switch (arg) {
            case 'getData':
                prTest.getData()
                break
            case 'verify':
                prTest.verify()
                break
            case 'Get_projectId':
                prTest.Get_projectId()
                break
            case 'Get_idUpdate':
                prTest.Get_idUpdate()
                break
            case 'verifyExists':
                prTest.verifyExists()
                break
        }

    }


    // close or open div#div_panelTest -> basePanelTesting
    objHtml.btnLoad_panelTesting.click( (e)=> {

        if (!objHtml.indLoading) {
            $.ajax('/html/testing_dlg_project.html', {
                type: 'GET',
                success: function (data) {

                    objHtml.div_panelTest.empty().html(data)
                    objHtml.indLoading = true

                    objHtml.div_detailTest = $('.div-detailTest')
                    objHtml.list_group_item = $('.list-group-item')
                    objHtml.spanButtonList = $('#spanButtonList')

                    objHtml.btnTest_getData = $('#btnTest-getData')
                    objHtml.btnTest_clear = $('#btnTest-clear')

                    objHtml.btnTest_getData.click(btnTest_getData)
                    objHtml.btnTest_clear.click(btnTest_clear)
                    objHtml.list_group_item.click(list_group_item)

                    updText_btnLoadPanelTesting(objHtml.txtOpen)

                }

            })
        }
        else {
            if (objHtml.indOpenClose == objHtml.txtOpen) {

                updText_btnLoadPanelTesting(objHtml.txtClose)
                objHtml.div_panelTest.hide()
            }
            else {
                updText_btnLoadPanelTesting(objHtml.txtOpen)
                objHtml.div_panelTest.show()
            }
        }

    })

})(procTesting, objHtml);