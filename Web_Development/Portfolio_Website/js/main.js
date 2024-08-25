// Website dark/light theme

// Selektiere das DOM-Element, das den Theme-Umschalt-Button repräsentiert
const themeBtn = document.querySelector(".theme-btn");

// Füge einen Event-Listener hinzu, um auf das Klicken des Theme-Umschalt-Buttons zu reagieren
themeBtn.addEventListener("click", () => {
    // Toggle die Klasse 'dark-theme' am <body>-Element
    document.body.classList.toggle("dark-theme");
    // Toggle die Klasse 'sun' am Theme-Umschalt-Button
    themeBtn.classList.toggle("sun");

    // Speichere den aktuellen Theme-Status und Icon-Status im lokalen Speicher
    localStorage.setItem("saved-theme", getCurrentTheme());
    localStorage.setItem("saved-icon", getCurrentIcon());
});

// Prüft, welches theme gespeichert ist
const getCurrentTheme = () => document.body.classList.contains("dark-theme") ? "dark" : "light";

// Prüft, welches Icon gespeichert ist
const getCurrentIcon = () => themeBtn.classList.contains("sun") ? "sun" : "moon";

// Lese die zuvor gespeicherten Theme- und Icon-Einstellungen aus dem lokalen Speicher
const savedTheme = localStorage.getItem("saved-theme");
const savedIcon = localStorage.getItem("saved-icon");

// Überprüfe, ob zuvor gespeicherte Einstellungen vorhanden sind, und wende sie an
if (savedTheme) {
    // Füge die Klasse 'dark-theme' hinzu oder entferne sie basierend auf der gespeicherten Theme-Einstellung
    document.body.classList[savedTheme === "dark" ? "add" : "remove"]("dark-theme");
    // Füge die Klasse 'sun' hinzu oder entferne sie basierend auf der gespeicherten Icon-Einstellung
    themeBtn.classList[savedIcon === "sun" ? "add" : "remove"]("sun");
}


// Scroll to top button

// Selektiere das DOM-Element, das den "Scroll-to-Top"-Button repräsentiert
const scrollTopBtn = document.querySelector(".scrollToTop-btn");

// Button zum hoch scrollen
window.addEventListener("scroll", function() {
    // Toggle die Klasse 'active' basierend auf der vertikalen Scroll-Position (window.scrollY)
    // Wenn die vertikale Scroll-Position größer als 500 Pixel ist, wird die Klasse 'active' hinzugefügt, ansonsten entfernt
    scrollTopBtn.classList.toggle("active", window.scrollY > 500);
});

// Füge einen Event-Listener hinzu, um auf das Klicken des "Scroll-to-Top"-Buttons zu reagieren
scrollTopBtn.addEventListener("click", () => {
    // Setze die vertikale Scroll-Position des Dokuments auf 0, um zum oberen Rand der Seite zu scrollen
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
});


// Navigation menu items active on page scroll
window.addEventListener("scroll", () => {
    // Selektiere alle Abschnitte (sections) auf der Seite
    const sections = document.querySelectorAll("section");

    // Holt sich die aktuelle vertikale Scroll-Position der Seite
    const scrollY = window.pageYOffset;

    // Iteriert über jeden Abschnitt und aktualisiert die aktive Navigation
    sections.forEach(current => {
        // Holt sich die Höhe und den oberen Rand des aktuellen Abschnitts
        let sectionHeight = current.offsetHeight;
        let sectionTop = current.offsetTop - 200; // 200 ist ein Offset-Wert, um die Aktivierung etwas vor dem eigentlichen Abschnittsbeginn auszulösen

        // Holt sich die ID des aktuellen Abschnitts
        let id = current.getAttribute("id");
        
        // Überprüfen, ob der sichtbare Bereich die Höhe des aktuellen Abschnitts umfasst
        if (scrollY > sectionTop && scrollY <= sectionTop + sectionHeight) {
            // Füge die Klasse 'active' zum entsprechenden Navigationslink hinzu
            document.querySelector(".nav-items a[href*=" + id + "]").classList.add("active");
        } else {
            // Entferne die Klasse 'active' vom Navigationslink, wenn der Abschnitt nicht sichtbar ist
            document.querySelector(".nav-items a[href*=" + id + "]").classList.remove("active");
        }
    });
});


// Navigations fenster

// Auswahl der DOM-Elemente
const menuBtn = document.querySelector(".nav-menu-btn"); // Schaltfläche für Menü öffnen
const closeBtn = document.querySelector(".nav-close-btn"); // Schaltfläche für Menü schließen
const navigation = document.querySelector(".navigation"); // Navigationscontainer
const navItems = document.querySelectorAll(".nav-items a"); // Alle Navigationslinks

// Event-Listener für das Öffnen des Menüs
menuBtn.addEventListener("click", () => {
    navigation.classList.add("active"); // Füge die Klasse 'active' hinzu, um das Menü zu öffnen
});

// Event-Listener für das Schließen des Menüs
closeBtn.addEventListener("click", () => {
    navigation.classList.remove("active"); // Entferne die Klasse 'active', um das Menü zu schließen
});

// Event-Listener für das Schließen des Menüs bei Klick auf einen Navigationslink
navItems.forEach((navItem) => {
    navItem.addEventListener("click", () => {
        navigation.classList.remove("active"); // Entferne die Klasse 'active', um das Menü zu schließen
    });
});


//Einstellungen für reveal animationen
ScrollReveal({
    distance: '60px',
    duration: 2500,
    delay: 100
});

//Reveal Animationen
ScrollReveal().reveal('.home .info h2, .section-title-01, .section-title-02', { delay: 500, origin: 'left' });
ScrollReveal().reveal('.home .info h3, .home .info p, .about-info .btn', { delay: 600, origin: 'right' });
ScrollReveal().reveal('.home .info .btn', { delay: 700, origin: 'bottom' });
ScrollReveal().reveal('.media-icons i, .contact-left li', { delay: 500, origin: 'left', interval: 200 });
ScrollReveal().reveal('.home-img, .about-img', { delay: 500, origin: 'bottom' });
ScrollReveal().reveal('.about .description, .contact-right', { delay: 600, origin: 'right' });
ScrollReveal().reveal('.about .professional-list li', { delay: 500, origin: 'right', interval: 200 });
ScrollReveal().reveal('.experience-description, .services-description, .contact-card, .client-swiper, .contact-left h2', { delay: 700, origin: 'left' });
ScrollReveal().reveal('.education, .portfolio .img-card', { delay: 800, origin: 'bottom', interval: 200 });
ScrollReveal().reveal('footer .group', { delay: 500, origin: 'top', interval: 200 });
ScrollReveal().reveal('.imprint', { delay: 600, origin: 'right' });

// E-Mail fenster öffnen
function openEmailWindow() {
    var emailAdresse = 'michael.epper@ksb-sg.ch';
    
    // Öffne das E-Mail-Fenster mit der E-Mail-Adresse
    window.location.href = 'mailto:' + emailAdresse;
}