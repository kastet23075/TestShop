$(function() {
    $('.input-price-mask').inputmask({
        alias: 'decimal',
        integerDigit: 10,
        radixPoint: '.',
        digits: 2,
        max: '99999999.99',
        min: '0',
        placeholder: '',
        rightAlign: false
    });

    $('.shop-count-product-mask').inputmask({
        alias: 'numeric',
        integerDigit: 4,
        max: '9999',
        min: '1',
        rightAlign: false
    });
});
