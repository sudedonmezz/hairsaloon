const menu = document.querySelector(".menu")
const button = document.getElementById("toggle");

const navbar = document.getElementById('navbar')

button.addEventListener("click", function () {
  menu.classList.toggle("Show_menu")
})



// Navbar 
window.addEventListener('scroll', function () {
  var scroll = window.scrollY;
  if (scroll > 100) {
    navbar.classList.add("sticky");
  }
  else {
    navbar.classList.remove("sticky");
  }
})




document.addEventListener('DOMContentLoaded', function () {
  new Splide('.Testimonials_Splide', {
    type   : 'loop',
    drag   : 'free',
    focus  : 'center',
    gap:'2rem',
    perPage: 2,
    pagination:false,
    autoScroll: {
      speed: 0.7,
    },
    breakpoints:{
      1200:{
        gap:'1rem'
      },
      900:{
        perPage:1.5
      },
      500:{
        perPage:1
      }
    }
  }).mount( window.splide.Extensions );
});