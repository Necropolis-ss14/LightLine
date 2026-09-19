character-requirement-desc = Требования:

## Job
character-job-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть одной из этих ролей: {$jobs}

character-department-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть в одном из этих отделов: {$departments}

character-antagonist-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть антагонистом

character-mindshield-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} иметь чип защиты разума

character-timer-department-insufficient = Требуется ещё [color=yellow]{TOSTRING($time, "0")}[/color] минут игры за [color={ $departmentColor }]{ $department }[/color].
character-timer-department-too-high = Требуется на [color=yellow]{TOSTRING($time, "0")}[/color] меньше минут игры за [color={ $departmentColor }]{ $department }[/color]. (Вы пытаетесь играть за роль для новичков?)

character-timer-overall-insufficient = Требуется ещё [color=yellow]{TOSTRING($time, "0")}[/color] минут общего игрового времени.
character-timer-overall-too-high = Требуется на [color=yellow]{TOSTRING($time, "0")}[/color] меньше минут общего игрового времени. (Вы пытаетесь играть за роль для новичков?)

character-timer-role-insufficient = Требуется ещё [color=yellow]{TOSTRING($time, "0")}[/color] минут игры в качестве [color={ $departmentColor }]{ $job }[/color] для этой роли.
character-timer-role-too-high = Требуется на [color=yellow]{TOSTRING($time, "0")}[/color] меньше минут игры в качестве [color={ $departmentColor }]{ $job }[/color] для этой роли. (Вы пытаетесь играть за роль для новичков?)


## Logic
character-logic-and-requirement-listprefix = {""}
    {$indent}[color=gray]&[/color]{" "}
character-logic-and-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} соответствовать [color=red]всему[/color] из [color=gray]этого[/color]: {$options}

character-logic-or-requirement-listprefix = {""}
    {$indent}[color=white]O[/color]{" "}
character-logic-or-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} соответствовать [color=red]хотя бы одному[/color] из [color=white]этого[/color]: {$options}

character-logic-xor-requirement-listprefix = {""}
    {$indent}[color=white]X[/color]{" "}
character-logic-xor-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} соответствовать [color=red]только одному[/color] из [color=white]этого[/color]: {$options}


## Profile
character-age-requirement-range = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть в промежутке между [color=yellow]{$min}[/color] и [color=yellow]{$max}[/color] лет

character-age-requirement-minimum-only = Вам неоходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть хотя бы [color=yellow]{$min}[/color] лет

character-age-requirement-maximum-only = Вам необходимо{$inverted ->
    [true]{""}
    *[other]{" "}не
} быть старше чем [color=yellow]{$max}[/color] лет

character-backpack-type-requirement = Вам необходимо {$inverted ->
    [true] не использовать
    *[other] использовать
} [color=brown]{$type}[/color] в качестве вашего рюкзака

character-clothing-preference-requirement = Вы должны {$inverted ->
    [true] не одевать
    *[other] одеть
} [color=white]{$type}[/color]

character-gender-requirement = Вы должны {$inverted ->
    [true] не иметь
    *[other] иметь
} местоимения [color=white]{$gender}[/color]

character-sex-requirement = You must{$inverted ->
    [true]{" "}not
    *[other]{""}
} be [color=white]{$sex ->
    [None] unsexed
    *[other] {$sex}
}[/color]
character-species-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть {$species}

character-height-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть {$min ->
    [-2147483648]{$max ->
        [2147483648]{""}`
        *[other] ниже чем [color={$color}]{$max}[/color]см
    }
    *[other]{$max ->
        [2147483648] выше чем [color={$color}]{$min}[/color]см
        *[other] между [color={$color}]{$min}[/color] и [color={$color}]{$max}[/color]см в высоту
    }
}

character-width-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть {$min ->
    [-2147483648]{$max ->
        [2147483648]{""}
        *[other] худее чем [color={$color}]{$max}[/color]см
    }
    *[other]{$max ->
        [2147483648] шире чем [color={$color}]{$min}[/color]см
        *[other] между [color={$color}]{$min}[/color] и [color={$color}]{$max}[/color]см в ширину
    }
}

character-weight-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть {$min ->
    [-2147483648]{$max ->
        [2147483648]{""}
        *[other] легче чем [color={$color}]{$max}[/color]кг
    }
    *[other]{$max ->
        [2147483648] тяжелее чем [color={$color}]{$min}[/color]кг
        *[other] между [color={$color}]{$min}[/color] и [color={$color}]{$max}[/color]кг
    }
}


character-trait-requirement = Вам необходимо {$inverted ->
    [true] не иметь
    *[other] иметь
} одну из этих черт: {$traits}

character-loadout-requirement = Вам необходимо {$inverted ->
    [true] не иметь
    *[other] иметь
} один из этих лодаутов: {$loadouts}


character-item-group-requirement = Вам необходимо {$inverted ->
    [true] иметь более {$max}
    *[other] не иметь более {$max}
} вещей из категории [color=white]{$group}[/color]


## Whitelist
character-whitelist-requirement = Вам необходимо{$inverted ->
    [true]{" "}не
    *[other]{""}
} быть в вайтлисте

## CVar

character-cvar-requirement =
    The server must{$inverted ->
    [true]{" "}not
    *[other]{""}
} have [color={$color}]{$cvar}[/color] set to [color={$color}]{$value}[/color].
