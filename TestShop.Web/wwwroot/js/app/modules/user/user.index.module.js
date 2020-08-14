;(function (global) {
    global.userIndex = {};
    $.extend(global.userIndex,
        {
            variables: {},

            initialize: function () {
                var self = this;

                $('.lock-user-link-index').click(function () {
                    var clickedLink = this;
                    var isLock = isTrue(clickedLink.dataset.isLock);

                    $.ajax({
                        type: 'POST',
                        url: self.variables.userLockUrl,
                        data: { Id: clickedLink.dataset.id, IsLock: isLock },
                        success: function () {
                            clickedLink.dataset.isLock = !isLock;
                            clickedLink.classList.toggle('fa-lock');
                            clickedLink.classList.toggle('fa-unlock');
                        }
                    });
                });
            }
        });
})(window);
