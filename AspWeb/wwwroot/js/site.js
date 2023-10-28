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
    if (movingDiv != null) {
        if (distance > 5) {
            movingDiv.classList.add('detail_page_div_overlap');
        } else {
            movingDiv.classList.remove('detail_page_div_overlap');
        }
    }
});

const parallaxImage = document.querySelector('.parallax-image');
if (parallaxImage != null) {
    parallaxImage.style.height = `${document.body.scrollHeight}px`;
    parallaxImage.style.top = `${-document.body.scrollHeight}px`;
    window.addEventListener('scroll', () => {
        // Adjust the speed factor for the parallax effect.
        const speed = 0.4;
        parallaxImage.style.transform = `translateY(${(window.scrollY) * speed}px)`;
    });
}

const hoverDiv1 = document.querySelector('.timeeventdiv1');
const hoverDiv2 = document.querySelector('.timeeventdiv2');
const hoverDiv3 = document.querySelector('.timeeventdiv3');
const hoverDiv4 = document.querySelector('.timeeventdiv4');
const hoverDiv5 = document.querySelector('.timeeventdiv5');
const hoverDiv6 = document.querySelector('.timeeventdiv6');
const hoverDiv7 = document.querySelector('.timeeventdiv7');
const textParagraph = document.getElementById('text-paragraph');
if (hoverDiv1 != null) {
    hoverDiv1.addEventListener('mouseover', () => {
        textParagraph.textContent = "中国共产党第一次全国代表大会于1921年7月23日至8月初在上海法租界望志路106号（现兴业路76号）" +
            "和浙江嘉兴召开。上海的李达、李汉俊，北京的张国焘、刘仁静，武汉的董必武、陈潭秋，长沙的毛泽东、何叔衡，广州的陈公博，济南的王尽美、邓恩铭，旅日的周佛海，以及由陈独秀指定的代表包惠僧出席会议，代表全国50多名党员。共产国际代表马林和尼克尔斯基也出席了大会。";
    });
    hoverDiv1.addEventListener('mouseout', () => {
        textParagraph.textContent = "";
        textParagraph.style.color = 'black';
        textParagraph.style.fontWeight = 'normal';
    });
}
if (hoverDiv2 != null) {
    hoverDiv2.addEventListener('mouseover', () => {
        textParagraph.textContent = "1925年1月11日至22日，中国共产党第四次全国代表大会在上海召开。出席大会的有陈独秀、蔡和森、瞿秋白、谭平山、周恩来、彭述之、张太雷、陈潭秋、李维汉、李立三、王荷波、项英、向警予等20人，代表着全国994名党员。";
    });
    hoverDiv2.addEventListener('mouseout', () => {
        textParagraph.textContent = "";
        textParagraph.style.color = 'black';
        textParagraph.style.fontWeight = 'normal';
    });
}
if (hoverDiv3 != null) {
    hoverDiv3.addEventListener('mouseover', () => {
        textParagraph.textContent = "红军反“围剿”战争一般是指1930年-1934年，中国工农红军反击反革命军事力量对以中央革命根据地为重点的各根据地的五次“围剿”的作战。"
            + "在中国共产党的领导下，红军经过三年艰苦曲折的游击战争，粉碎了国民党反动派的多次“进剿”与“会剿”，至1930年夏，中国工农红军已发展到约10万多人，在十余个省先后开辟了大小十多块革命根据地。";
    });
    hoverDiv3.addEventListener('mouseout', () => {
        textParagraph.textContent = "";
        textParagraph.style.color = 'black';
        textParagraph.style.fontWeight = 'normal';
    });
}
if (hoverDiv4 != null) {
    hoverDiv4.addEventListener('mouseover', () => {
        textParagraph.textContent = "1937年7月15日，中共中央将《中国共产党为公布国共合作宣言》送交国民党。8月中旬，蒋介石被迫同意将陕北的中央红军改编为国民革命军第八路军。" +
            "8月22日，国民政府军事委员会发布命令，将红军改编为八路军，任命朱德为总指挥、彭德怀为副总指挥。9月22日，国民党中央通讯社发表《中共中央为公布国共合作宣言》。";
    });
    hoverDiv4.addEventListener('mouseout', () => {
        textParagraph.textContent = "";
        textParagraph.style.color = 'black';
        textParagraph.style.fontWeight = 'normal';
    });
}
if (hoverDiv5 != null) {
    hoverDiv5.addEventListener('mouseover', () => {
        textParagraph.textContent = "百团大战，是抗日战争时期，八路军在华北敌后发动的一次大规模进攻和反“扫荡”的战役，由于参战兵力达105个团，故称“百团大战”。" +
            "百团大战是抗日战争相持阶段八路军在华北地区发动的一次规模最大、持续时间最长的战役。";
    });
    hoverDiv5.addEventListener('mouseout', () => {
        textParagraph.textContent = "";
        textParagraph.style.color = 'black';
        textParagraph.style.fontWeight = 'normal';
    });
}
if (hoverDiv6 != null) {
    hoverDiv6.addEventListener('mouseover', () => {
        textParagraph.textContent = "解放战争（英文：War of Liberation），亦称第三次国内革命战争 [1]，是1946年6月至1950年6月 [37]中国人民解放军在中国共产党的领导下，为反对国民党蒋介石集团发动的反共反人民内战，" +
            "推翻帝国主义、封建主义、官僚资本主义的反动统治，建立人民民主专政的政权而进行的战争 。是一场事关中国前途命运的决战。";
    });
    hoverDiv6.addEventListener('mouseout', () => {
        textParagraph.textContent = "";
        textParagraph.style.color = 'black';
        textParagraph.style.fontWeight = 'normal';
    });
}
if (hoverDiv7 != null) {
    hoverDiv7.addEventListener('mouseover', () => {
        textParagraph.textContent = "1949年10月1日，中华人民共和国中央人民政府成立。此前，中国人民政治协商会议第一届全体会议于9月21日至30日举行。会议通过起临时宪法作用的《中国人民政治协商会议共同纲领》。10月1日下午2时，中央人民政府委员会举行第一次会议。" +
            "下午3时，庆祝中华人民共和国中央人民政府成立典礼在北京天安门广场隆重举行。毛泽东主席宣告中央人民政府成立。";
    });
    hoverDiv7.addEventListener('mouseout', () => {
        textParagraph.textContent = "";
        textParagraph.style.color = 'black';
        textParagraph.style.fontWeight = 'normal';
    });
}

const items = document.querySelectorAll('.item');
const checkbox1 = document.getElementById('eventfilter');
const checkbox2 = document.getElementById('peoplefilter');
if (checkbox1 != null) {
    checkbox1.addEventListener('change', () => {
        const filterClass = checkbox1.id;

        items.forEach(item => {
            if (checkbox1.checked) {
                if (item.classList.contains(filterClass)) {
                    item.style.display = 'flex';
                }
            } else {
                if (item.classList.contains(filterClass)) {
                    item.style.display = 'none';
                }
            }
        });
    });
}
if (checkbox2 != null) {
    checkbox2.addEventListener('change', () => {
        const filterClass = checkbox2.id;

        items.forEach(item => {
            if (checkbox2.checked) {
                if (item.classList.contains(filterClass)) {
                    item.style.display = 'flex';
                }
            } else {
                if (item.classList.contains(filterClass)) {
                    item.style.display = 'none';
                }
            }
        });
    });
}
