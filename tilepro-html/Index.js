async function Category() {
    let url = `https://localhost:44327/api/Category/GetAllCategories`;
    let response = await fetch(url);
    let data = await response.json();
    let priceItem = document.getElementById("Batool");

    data.forEach((category) => {
        priceItem.innerHTML += `
<div class="project-block col-lg-3 col-md-6">
    <div class="inner-box">
    <div class="image-box">
    <figure class="image overlay-anim">
        <a href="#" onclick="save(${category.categoryId});">
            <img src="https://localhost:44327/api/Category/images/${category.categoryImage}" width="575px" height="575px" alt="${category.categoryName} (Image not found)">
        </a>
    </figure>
    <figure class="image-2">
        <a href="#" onclick="save(${category.categoryId});">
            <img src="images/resource/projec1-2.png" alt="Alternative Image">
        </a>
    </figure>
    </div>
    <div class="content-box">
    <span>${category.categoryName}</span>
    <h6 class="title"><a href="#" onclick="save(${category.categoryId});">المزيد</a></h6>
    </div>
    </div>
    </div>
    `;
    });

    console.log(data);
}

function save(categoryId) {
    localStorage.setItem("categoryId", categoryId);
    window.location.href = "shop-products2.html";
}

Category();

// Contact Us
document.addEventListener("DOMContentLoaded", function() {
    const form = document.getElementById("contact-form");

    form.addEventListener("submit", async function(event) {
        event.preventDefault();

        const formData = new FormData(form); // Use FormData directly

        try {
            const response = await fetch("https://localhost:44327/api/ContactUs/AddContact", {
                method: "POST",
                body: formData // Send FormData object directly
            });

            if (response.ok) {
                alert("تم إرسال رسالتك بنجاح، سوف يتم التواصل معك عبر البريد الإلكتروني أو عبر رقم الهاتف");
            } else {
                alert("يوجد خطأ في أرسال رسالتك، يرجى التأكد من بياناتك قبل الإرسال");
            }
        } catch (error) {
            console.error("Error:", error);
            alert("يوجد خطأ في إرسال البيانات. يرجى المحاولة مرة أخرى لاحقًا.");
        }
    });
});


async function FeedBack() {
    
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
<img src=" https://localhost:44327/api/Category/images/${feedbackk.userFeedbackImage} " width="70px" height="70px" alt=" ${feedbackk.userFeedbackImage}  (Image not found) ">
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
                    <img src="https://localhost:44327/api/Category/images/${Project.projectImage}" alt="${Project.projectImage} (Image not found)">
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
async function GetAllNews() {
    // debugger
    let url = `https://localhost:44327/api/News/GetLatestNews`;
    let response = await fetch(url);
    let data = await response.json();
    let Neewws = document.getElementById("newsReels");

    data.sort((a, b) => new Date(b.newsDate) - new Date(a.newsDate));

    data.forEach((AllNews) => {
        Neewws.innerHTML += `
        <div class="news-block col-lg-4 col-md-6">
            <div class="inner-box">
                <div class="image-box">
                    <iframe width="374" height="262.8" src="${AllNews.youtubeUrl}" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                </div>
                <div class="content-box">
                    <ul class="post">
                        <li>
                            <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 14 14" fill="none">
                                <path opacity="0.8" d="M4.9 0V1.4H9.1V0H10.5V1.4H13.3C13.6866 1.4 14 1.7134 14 2.1V13.3C14 13.6866 13.6866 14 13.3 14H0.7C0.313404 14 0 13.6866 0 13.3V2.1C0 1.7134 0.313404 1.4 0.7 1.4H3.5V0H4.9ZM12.6 7H1.4V12.6H12.6V7ZM3.5 2.8H1.4V5.6H12.6V2.8H10.5V4.2H9.1V2.8H4.9V4.2H3.5V2.8Z" fill="#F94A29" />
                            </svg> ${AllNews.newsDate}
                        </li>
                        <li>
                            <svg xmlns="http://www.w3.org/2000/svg" width="10" height="14" viewBox="0 0 10 14" fill="none">
                                <path opacity="0.8" d="M0.625 0H9.375C9.72019 0 10 0.303636 10 0.678183V13.6608C10 13.8481 9.86006 14 9.6875 14C9.62881 14 9.57125 13.982 9.5215 13.9481L5 10.8722L0.478494 13.9481C0.332269 14.0476 0.139412 13.9997 0.0477311 13.841C0.0165436 13.787 0 13.7246 0 13.6608V0.678183C0 0.303636 0.279825 0 0.625 0ZM8.75 1.35637H1.25V11.8224L5 9.27123L8.75 11.8224V1.35637Z" fill="#F94A29" />
                            </svg> أخر الأخبار
                        </li>
                    </ul>
                    <h6 class="title"><p>${AllNews.newsName}</p></h6>
                </div>  
            </div>
        </div>
        `;
    });
    console.log(data);
}
GetAllNews();


// الوصول إلى زر التسجيل
let authBtn = document.getElementById("auth-btn");

// التحقق من حالة تسجيل الدخول عند تحميل الصفحة
window.onload = function() {
    if (localStorage.getItem('jwtToken')) {
        authBtn.textContent = "خروج"; // تغيير النص إلى خروج إذا كان المستخدم مسجل الدخول
        authBtn.href = "#"; // منع الانتقال إلى صفحة تسجيل الدخول
    }
};

// تسجيل الخروج عند الضغط على زر "خروج"
authBtn.addEventListener("click", function(event) {
    if (authBtn.textContent === "خروج") {
        localStorage.removeItem('jwtToken'); // حذف رمز JWT من LocalStorage
        authBtn.textContent = "تسجيل"; // إعادة النص إلى تسجيل
        authBtn.href = "Login.html"; // إعادة الرابط إلى صفحة تسجيل الدخول
        alert("تم تسجيل الخروج بنجاح");
        window.location.href = "index.html"; // إعادة توجيه إلى الصفحة الرئيسية
    }
});






    // document.getElementById("moreLink").addEventListener("click", function(event) {
    //     event.preventDefault(); 
    // });



    // Function to update the cart item count
    function updateCartItemCount() {
        // Retrieve cart items from localStorage
        let cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
        
        // Get the cart item count
        let itemCount = cartItems.length;

        // Update the cart item count in the badge
        document.querySelector(".cart-btn .badge").textContent = itemCount;
    }

    // Call the function to update the cart item count on page load
    updateCartItemCount();
