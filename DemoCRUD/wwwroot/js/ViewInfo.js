var slideIndex = 1;
function plusSlide(n) {
    slideShow(slideIndex += n);
}
function slideShow(n) {
    var slides = document.getElementsByClassName("img-house");
    if (n > slides.length) {
        slideIndex = 1;
    }
    if (n < 1) {
        slideIndex = slides.length;
    }
    for (var i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slideIndex - 1].style.display = "block";
}
//Confirm before delete
$("body").on("submit", "#form-delete", function () {
    return confirm("Do you want to delete?");
});