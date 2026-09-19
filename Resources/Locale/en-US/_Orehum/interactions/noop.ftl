interaction-LookAt-name = Смотреть на
interaction-LookAt-description = Пялиться на пустоту и увидеть, как оно пялится на вас.
interaction-LookAt-success-self-popup = Вы смотрите на {THE($target)}.
interaction-LookAt-success-target-popup = Вы чувствуете, что {THE($user)} смотрит на вас...
interaction-LookAt-success-others-popup = {THE($user)} смотрит на {THE($target)}.

interaction-Hug-name = Обнять
interaction-Hug-description = Одно объятие в день избавляет от психологических ужасов, недоступных вашему пониманию.
interaction-Hug-success-self-popup = Вы обнимаете {THE($target)}.
interaction-Hug-success-target-popup = {THE($user)} обнимает вас.
interaction-Hug-success-others-popup = {THE($user)} обнимает {THE($target)}.

interaction-Pet-name = Гладить
interaction-Pet-description = Погладьте своего коллегу, что бы снять с него стресс.
interaction-Pet-success-self-popup = Вы гладите {THE($target)} по {POSS-ADJ($target)} голове.
interaction-Pet-success-target-popup = {THE($user)} гладит вас.
interaction-Pet-success-others-popup = {THE($user)} гладит {THE($target)}.

interaction-PetAnimal-name = {interaction-Pet-name}
interaction-PetAnimal-description = Погладьте животное.
interaction-PetAnimal-success-self-popup = {interaction-Pet-success-self-popup}
interaction-PetAnimal-success-target-popup = {interaction-Pet-success-target-popup}
interaction-PetAnimal-success-others-popup = {interaction-Pet-success-others-popup}

interaction-KnockOn-name = Стукнуть
interaction-KnockOn-description = Постучите по цели, что бы привлечь к себе внимание.
interaction-KnockOn-success-self-popup = Вы стучите по {THE($target)}.
interaction-KnockOn-success-target-popup = {THE($user)} стучит по вам.
interaction-KnockOn-success-others-popup = {THE($user)} стучит по {THE($target)}.

interaction-Rattle-name = Грохотать
interaction-Rattle-success-self-popup = Вы грохочете {THE($target)}.
interaction-Rattle-success-target-popup = {THE($user)} грохочет вам.
interaction-Rattle-success-others-popup = {THE($user)} грохочет {THE($target)}.

# The below includes conditionals for if the user is holding an item
interaction-WaveAt-name = Махать рукой
interaction-WaveAt-description = Помашите своей цели. Если у вас в руках предмет, вы помашите им.
interaction-WaveAt-success-self-popup = Вы машете {$hasUsed ->
    [false] около {THE($target)}.
    *[true] ваш {$used} около {THE($target)}.
}
interaction-WaveAt-success-target-popup = {THE($user)} машет {$hasUsed ->
    [false] вам.
    *[true] {POSS-PRONOUN($user)} {$used} вам.
}
interaction-WaveAt-success-others-popup = {THE($user)} машет {$hasUsed ->
    [false] около {THE($target)}.
    *[true] {POSS-PRONOUN($user)} {$used} около {THE($target)}.
}
