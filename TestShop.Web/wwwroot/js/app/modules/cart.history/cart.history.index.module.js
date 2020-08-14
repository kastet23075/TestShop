var headerOrderList = document.getElementsByClassName('accordeon-head');

for (let i = 0; i < headerOrderList.length; i++) {
    headerOrderList[i].addEventListener('click', function () {
        var hiddenElem = $('.accordeon-head').not($(this)).children('.fa-sort-up');
        hiddenElem.toggleClass('fa-sort-up');
        hiddenElem.toggleClass('fa-sort-down');

        this.querySelector('.fas').classList.toggle('fa-sort-down');
        this.querySelector('.fas').classList.toggle('fa-sort-up');
        $('.accordeon-body').not($(this).next()).slideUp(500);
        $(this).next().slideToggle(500);
    });
}