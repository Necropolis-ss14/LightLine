moff-blade-server-rack-window-title = Стойка блейд-серверов
moff-blade-server-rack-window-footer-flavor = ПРОШИВКА УСТРОЙСТВА © 2125 NANOSOFT

moff-blade-server-rack-slot-status = Слот {$index}: {$content}

moff-blade-server-rack-slot-entity-unknown = неизвестно
moff-blade-server-rack-slot-empty = свободен

moff-blade-server-rack-slot-eject = Извлечь
moff-blade-server-rack-slot-insert = Вставить
moff-blade-server-rack-slot-power-toggle = Переключить питание

moff-blade-server-rack-slot-locked-fail = Заблокировано!
moff-blade-server-rack-slot-whitelist-fail = Это не подходит!

moff-blade-server-rack-examine-empty = Внутри [color=#1f8ab2]нет блейдов[/color].
moff-blade-server-rack-examine-single = Внутри только {$slot}.
moff-blade-server-rack-examine-multiple-start = Внутри
moff-blade-server-rack-examine-multiple-slot-line = - {$slot}
moff-blade-server-rack-examine-slot = { INDEFINITE($name) } [color=#1f8ab2]{ CAPITALIZE($name) }[/color] в слоте {$index}
moff-blade-server-rack-examine-distant =
    Внутри [color=#1f8ab2]{$numBlades} { $numBlades ->
        [1] блейд
        *[other] блейдов
    }[/color], но отсюда не разобрать, { $numBlades ->
        [1] что это
        *[other] что это
    }.

moff-blade-server-frame-incompatible-board = Похоже, эта плата несовместима с каркасом...
moff-blade-server-board-compatible-hint = Может использоваться для создания [color=#1f8ab2]блейд-сервера[/color]
