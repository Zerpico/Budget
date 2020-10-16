$(function () {
    var
        $table = $('#tree-table'),
        rows = $table.find('tr');

    rows.each(function (index, row) {
        var
            $marg = 10,
            $row = $(row),
            level = $row.data('level'),
            id = $row.data('id'),
            $columnName = $row.find('td[data-column="name"]'),
            children = $table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            var expander = $columnName.find('b')
                .css("margin-left", parseInt(15 * level + $marg) + 'px')
                .css("margin-right", '16px')
                .css("height", '16px')
                .prepend('' +
                '<i class="treegrid-expander"></i>' +
                '');
            $columnName.find('b').addClass('fa fa-chevron-down');
            $marg = 0;
            children.show();

            $columnName.find('b').on('click', function (e) {
                var $target = $(e.target);
                if ($target.hasClass('fa-chevron-right')) {
                    $target
                        .removeClass('fa-chevron-right')
                        .addClass('fa-chevron-down');

                    children.show();
                } else {
                    $target
                        .removeClass('fa-chevron-down')
                        .addClass('fa-chevron-right');

                    reverseHide($table, $row);
                }
            });
                      
        }

        $columnName.find('b')
            .css("margin-left", parseInt(15 * level + $marg) + 'px')
            .css("margin-right", '16px')
            .css("height", '16px');
      /*  $columnName.find('b').prepend('' +
            '<i class="treegrid-indent" style="margin-left:' + parseInt(15 * level + $marg) + 'px"></i>' +
            '');*/
    });

    // Reverse hide all elements
    reverseHide = function (table, element) {
        var
            $element = $(element),
            id = $element.data('id'),
            children = table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            children.each(function (i, e) {
                reverseHide(table, e);
            });

            $element
                .find('.fa-chevron-down')
                .removeClass('fa fa-chevron-down')
                .addClass('fa fa-chevron-right');

            children.hide();
        }
    };
});
