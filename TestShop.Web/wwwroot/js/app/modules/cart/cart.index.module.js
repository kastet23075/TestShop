updateTotalPrice();
var inputCountList = document.getElementsByClassName('shop-count-product-mask');
var btnMinusList = document.getElementsByClassName('button-minus');
var btnPlusList = document.getElementsByClassName('button-plus');

for (let i = 0; i < btnPlusList.length; i++) {
    btnPlusList[i].addEventListener('click', function () {
        var parent = this.parentNode;
        var countValue = parent.getElementsByTagName('input')[0].value;

        if (countValue < 9999) {
            parent.getElementsByTagName('input')[0].value = ++countValue;
            updateTotalPrice();
        }

        event.preventDefault();
    });
}

for (let i = 0; i < btnMinusList.length; i++) {
    btnMinusList[i].addEventListener('click', function () {
        var parent = this.parentNode;
        var countValue = parent.getElementsByTagName('input')[0].value;

        if (countValue > 1) {
            parent.getElementsByTagName('input')[0].value = --countValue;
            updateTotalPrice();
        }

        event.preventDefault();
    });
}

for (let i = 0; i < inputCountList.length; i++) {
    inputCountList[i].addEventListener('blur', function () {
        updateTotalPrice();
    });
}

function updateTotalPrice() {
    var totalPrice = 0;
    let table = document.querySelector('.cart-index-products-table');

    for (let i = 1; i < table.rows.length; i++) {
        let price = table.rows[i].querySelector('.cart-index-product-price').innerHTML;
        let count = table.rows[i].querySelector('.shop-count-product-mask').value;
        totalPrice += price * count;
    }

    document.querySelector('.cart-index-total-price').innerHTML = totalPrice.toFixed(2);
    return false;
}