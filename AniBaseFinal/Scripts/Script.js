function myFunction() {
    var x = document.getElementById("navDemo");
    if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
    } else { 
        x.className = x.className.replace(" w3-show", "");
    }
}


      (function() {
      var quotes = [
        {
          text: "Live a Life",
        },
        {
          text: "Reach out for those who are in need",
        },
        {
          text: "Tears don't judge the love you have for that person",
        }
      ];
      var quote = quotes[Math.floor(Math.random() * quotes.length)];
      document.getElementById("quote").innerHTML =
        '<p>' + quote.text + '</p>' ;
})();

var finished_rendering = function() {
  console.log("finished rendering plugins");
  var spinner = document.getElementById("spinner");
  spinner.removeAttribute("style");
  spinner.removeChild(spinner.childNodes[0]);
}
FB.Event.subscribe('xfbml.render', finished_rendering);

var slideIndex = 0;
showSlides();

