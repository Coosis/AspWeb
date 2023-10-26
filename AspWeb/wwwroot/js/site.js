// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//img gallery
const len_img = 4;
const img_container = document.getElementById("img_container");

let page = 0;

function click_prev(){
    page -= 1;
    if(page < 0){
        page = len_img - 1;
    }

    img_container.scrollLeft = page*img_container.offsetWidth;
}

function click_next(){
    page += 1;
    if(page >= len_img){
        page = 0;
    }

    img_container.scrollLeft = page*img_container.offsetWidth;
}

function updateTime() {
    const timeElement = document.getElementById("time");
    const now = new Date();
    timeElement.innerHTML = now.toLocaleTimeString();
}

//time update
//updateTime();
//setInterval(updateTime, 1000);

window.addEventListener('scroll', () => {
    const movingDiv = document.querySelector('.detail_page_div');
    const distance = window.scrollY;

    // Adjust the threshold value (e.g., 200) as needed for when the effect triggers
    if (distance > 5) {
        movingDiv.classList.add('detail_page_div_overlap');
    } else {
        movingDiv.classList.remove('detail_page_div_overlap');
    }
});