;(function (global) {
    global.userEdit = {};
    $.extend(global.userEdit,
        {
            variables: {},

            initialize: function () {
                var self = this;

                $('.lock-user-link-edit').click(function () {
                    var clickedLink = this;
                    var isLock = isTrue(clickedLink.dataset.isLock);

                    $.ajax({
                        type: 'POST',
                        url: self.variables.userLockUrl,
                        data: { Id: clickedLink.dataset.id, IsLock: isLock },
                        success: function () {
                            clickedLink.dataset.isLock = !isLock;
                            clickedLink.innerHTML = isLock ? 'Lock' : 'Unlock';
                        }
                    });
                });

                $('.edit-user-submit').click(function () {
                    var valuesForSave = $('.edit-user-form').serialize();

                    $.ajax({
                        type: 'POST',
                        url: self.variables.userEditUrl,
                        data: valuesForSave,
                        success: function () {
                            $('.info-message').html('Account updated!');
                        }
                    });
                });
            }
        });
})(window);