async function Category() {
    debugger
    let url = `https://localhost:44327/api/Category/GetAllCategories`;
    let response = await fetch(url);
    let data = await response.json();
    let priceItem = document.getElementById("Batool");
    data.forEach((Categories) => {
    priceItem.innerHTML += `
<div class="project-block col-lg-3 col-md-6">
    <div class="inner-box">
    <div class="image-box">
    <figure class="image overlay-anim"><a href="shop-products.html"><img src="${Categories.categoryImage}" width="575px" height="575px" alt="${Categories.categoryImage} (Image not found) "></a></figure>
    <figure class="image-2"><a href="shop-products.html"><img src="images/resource/projec1-2.png" alt></a></figure>
    </div>
    <div class="content-box">
    <span>${Categories.categoryName}</span>
    <h6 class="title"><a href="shop-products.html">المزيد</a></h6>
    </div>
    </div>
    </div>
    `;
});
    console.log(data);
}
Category();




async function FeedBack() {
    debugger
    let url = `https://localhost:44327/api/FeedBack/GetAllFeedback`;
    let response = await fetch(url);
    let data = await response.json();
    let priceItem = document.getElementById("feedback");
    data.forEach((feedbackk) => {
    priceItem.innerHTML += `
<div class="testimonial-block col-md-6">
<div class="inner-box">
<div class="icon-box">
<i class="flaticon-quote-1"></i>
</div>
<div class="content-box">
<div class="text"> ${feedbackk.message} </div>
<div class="auther-info">
<img src=" ${feedbackk.userFeedbackImage} " width="70px" height="70px" alt=" ${feedbackk.userFeedbackImage}  (Image not found) ">
<div class="info-box">
<h6 class="title"> ${feedbackk.name} </h6>
<span> ${feedbackk.profession} </span>
</div>
</div>
</div>
</div>
</div>
    `;
});
    console.log(data);
}
FeedBack();
///////////
async function ProjectData() {
    let url = `https://localhost:44327/api/Project/GetLatestProjects`;
    let response = await fetch(url);
    let data = await response.json();
    let priceItem = document.getElementById("Projects");

    data.forEach((Project) => {
        // تحويل التاريخ إلى تنسيق YYYY-MM-DD بدون الوقت
        let formattedDate = new Date(Project.projectDate).toLocaleDateString();

        priceItem.innerHTML += `
<div class="news-block col-lg-4 col-md-6">
    <div class="inner-box">
        <div class="image-box">
            <figure class="image">
                <a href="shop-product-details.html">
                    <img src="${Project.projectImage}" alt="${Project.projectImage} (Image not found)">
                </a>
            </figure>
        </div>
        <div class="content-box">
            <ul class="post">
                <li>
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 14 14" fill="none">
                        <path opacity="0.8" d="M4.9 0V1.4H9.1V0H10.5V1.4H13.3C13.6866 1.4 14 1.7134 14 2.1V13.3C14 13.6866 13.6866 14 13.3 14H0.7C0.313404 14 0 13.6866 0 13.3V2.1C0 1.7134 0.313404 1.4 0.7 1.4H3.5V0H4.9ZM12.6 7H1.4V12.6H12.6V7ZM3.5 2.8H1.4V5.6H12.6V2.8H10.5V4.2H9.1V2.8H4.9V4.2H3.5V2.8Z" fill="#F94A29" />
                    </svg>
                    ${formattedDate}
                </li>
                <li>
                    <svg xmlns="http://www.w3.org/2000/svg" width="10" height="14" viewBox="0 0 10 14" fill="none">
                        <path opacity="0.8" d="M0.625 0H9.375C9.72019 0 10 0.303636 10 0.678183V13.6608C10 13.8481 9.86006 14 9.6875 14C9.62881 14 9.57125 13.982 9.5215 13.9481L5 10.8722L0.478494 13.9481C0.332269 14.0476 0.139412 13.9997 0.0477311 13.841C0.0165436 13.787 0 13.7246 0 13.6608V0.678183C0 0.303636 0.279825 0 0.625 0ZM8.75 1.35637H1.25V11.8224L5 9.27123L8.75 11.8224V1.35637Z" fill="#F94A29" />
                    </svg>
                    ${Project.projectType}
                </li>
            </ul>
            <h6 class="title"><a href="shop-product-details.html">${Project.projectName}</a></h6>
        </div>
    </div>
</div>
        `;
    });

    console.log(data);
}

ProjectData();
///////////
