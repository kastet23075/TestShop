;(function (global) {
    global.shopIndex = {};
    $.extend(global.shopIndex,
        {
            variables: {},

            initialize: function () {
                var self = this;

                var linkAddToCartList = document.getElementsByClassName('shop-index-product-add-to-cart');
                var btnMinusList = document.getElementsByClassName('button-minus');
                var btnPlusList = document.getElementsByClassName('button-plus');

                for (let i = 0; i < btnPlusList.length; i++) {
                    btnPlusList[i].addEventListener('click', function (event) {
                        var parent = this.parentNode;
                        var countValue = parent.getElementsByTagName('input')[0].value;

                        if (countValue < 9999) {
                            parent.getElementsByTagName('input')[0].value = ++countValue;
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
                        }

                        event.preventDefault();
                    });
                }

                for (let i = 0; i < linkAddToCartList.length; i++) {
                    linkAddToCartList[i].addEventListener('click', function () {
                        let clickedLink = this;
                        let productId = clickedLink.dataset.id;
                        let neighborTd = clickedLink.parentNode.parentNode.previousElementSibling;
                        let countProducts = neighborTd.querySelector('.shop-count-product-mask').value;

                        $.ajax({
                            type: 'POST',
                            url: self.variables.addToCartUrl,
                            data: { productId: productId, count: countProducts },
                            success: function () {
                                var infoMessage = document.querySelector('.info-message');
                                infoMessage.classList.add('alert', 'alert-success');
                                infoMessage.innerHTML = 'Product added to cart!';

                                updateCountProducts(self.variables.getProductsCountUrl);
                                setTimeout(function() {
                                    infoMessage.classList.remove('alert-success', 'alert');
                                    infoMessage.innerHTML = '';
                                }, 2000);
                            }
                        });

                        return false;
                    });
                }
            }
        });
})(window);

function updateCountProducts(url) {
    $.ajax({
        type: 'POST',
        url: url,
        success: function (count) {
            if (count !== 0) {
                document.querySelector('.count-products-cart').innerHTML = count;
            } else {
                document.querySelector('.count-products-cart').innerHTML = '';
            }
        }
    });
}