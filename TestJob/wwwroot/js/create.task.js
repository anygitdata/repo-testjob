
class userIntf {
    static btnSelProj = document.querySelector('.dropdown-item')


    bindEvents() {
        btnSelProj.addEventListener('click', (e) => {
            console.log('click')
        })
    }
    
}

    