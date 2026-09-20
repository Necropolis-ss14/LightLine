server-ban-string-infinity = Навсегда
server-ban-no-name = Не найдено. ({ $hwid })
server-time-ban =
    Временный бан на { $mins } { $mins ->
        [one] минуту
        [few] минуты
       *[other] минут
    }.
server-perma-ban = Перманентный бан
server-role-ban =
    Временный джоб-бан на { $mins } { $mins ->
        [one] минуту
        [few] минуты
       *[other] минут
    }.
server-perma-role-ban = Перманентный джоб-бан
server-time-ban-string =
    > **Нарушитель**
    > **Логин:** ``{ $targetName }``
    > **Discord:** { $targetLink }

    > **Администратор**
    > **Логин:** ``{ $adminName }``
    > **Discord:** { $adminLink }

    > **Время**
    > **Выдано:** { $TimeNow }
    > **Истекает:** { $expiresString }

    > **Причина:** { $reason }

    > **Уровень серьёзности:** { $severity }
server-ban-footer = { $server } | Раунд: #{ $round }
server-perma-ban-string =
    > **Нарушитель**
    > **Логин:** ``{ $targetName }``
    > **Discord:** { $targetLink }

    > **Администратор**
    > **Логин:** ``{ $adminName }``
    > **Discord:** { $adminLink }

    > **Время**
    > **Выдано:** { $TimeNow }

    > **Причина:** { $reason }

    > **Уровень серьёзности:** { $severity }
server-role-ban-string =
    > **Нарушитель**
    > **Логин:** ``{ $targetName }``
    > **Discord:** { $targetLink }

    > **Администратор**
    > **Логин:** ``{ $adminName }``
    > **Discord:** { $adminLink }

    > **Время**
    > **Выдано:** { $TimeNow }
    > **Истекает:** { $expiresString }

    > **Роли:** { $roles }

    > **Причина:** { $reason }

    > **Уровень серьёзности:** { $severity }
server-perma-role-ban-string =
    > **Нарушитель**
    > **Логин:** ``{ $targetName }``
    > **Discord:** ``{ $targetLink }``

    > **Администратор**
    > **Логин:** ``{ $adminName }``
    > **Discord:** { $adminLink }

    > **Время**
    > **Выдано:** { $TimeNow }

    > **Роли:** { $roles }

    > **Причина:** { $reason }

    > **Уровень серьёзности:** { $severity }
