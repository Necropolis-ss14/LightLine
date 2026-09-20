entity-effect-guidebook-modify-solution-reagent =
    { $chance ->
        [1] { $deltasign ->
                [1] Добавляет
                *[-1] Удаляет
            }
        *[other]
            { $deltasign ->
                [1] добавить
                *[-1] удалить
            }
    } { NATURALFIXED($amount, 2) }u { $reagent } { $deltasign ->
        [1] в
        *[-1] из
    } раствор { $solution }
entity-effect-guidebook-regrow-doll-shell =
    { $chance ->
        [1] Восстанавливает
        *[other] восстановить
    } один кусок оболочки
