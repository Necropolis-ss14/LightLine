entity-condition-guidebook-unknown-reagent = неизвестный реагент

entity-condition-guidebook-blood-reagent-threshold =
    { $max ->
        [2147483648] в крови не менее { NATURALFIXED($min, 2) }u { $reagent }
        *[other] { $min ->
                    [0] в крови не более { NATURALFIXED($max, 2) }u { $reagent }
                    *[other] в крови от { NATURALFIXED($min, 2) }u до { NATURALFIXED($max, 2) }u { $reagent }
                 }
    }

entity-condition-guidebook-has-components =
    у цели { $shouldhave ->
                    [true] есть
                    *[false] нет
               } компонент { $name }
