
export const obj = (() => {
    function log() {
        console.log('testing loading modul')
    }

    return { log }
})()

export const objExt = {
    log: function () {
        console.log('Message from objExt: log')
    }
}


