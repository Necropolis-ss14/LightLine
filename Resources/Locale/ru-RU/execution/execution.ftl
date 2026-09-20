execution-verb-name = Казнить
execution-verb-message = Используйте своё оружие, чтобы казнить кого-то.

# All the below localisation strings have access to the following variables
# attacker (the person committing the execution)
# victim (the person being executed)
# weapon (the weapon used for the execution)

execution-popup-melee-initial-internal = Вы прикладываете { $weapon } к горлу { $victim }.
execution-popup-melee-initial-external = { CAPITALIZE($attacker) } прикладывает свой { $weapon } к горлу { $victim }.
execution-popup-melee-complete-internal = Вы перерезаете горло { $victim }!
execution-popup-melee-complete-external = { CAPITALIZE($attacker) } перерезает горло { $victim }!

execution-popup-self-initial-internal = Вы прикладываете { $weapon } к своему горлу.
execution-popup-self-initial-external = { CAPITALIZE($attacker) } прикладывает свой { $weapon } к своему горлу.
execution-popup-self-complete-internal = Вы перерезаете себе горло!
execution-popup-self-complete-external = { CAPITALIZE($attacker) } перерезает себе горло!

suicide-verb-name = Суицид
suicide-verb-message = Используйте оружие, чтобы покончить с собой.
execution-popup-gun-initial-internal = Вы наставляете дуло {THE($weapon)} на голову {THE($victim)}.
execution-popup-gun-initial-external  = { CAPITALIZE(THE($attacker)) } наставляет дуло {POSS-ADJ($attacker)} {$weapon} на голову {THE($victim)}.
execution-popup-gun-complete-internal = Вы стреляете {THE($victim)} в голову!
execution-popup-gun-complete-external = { CAPITALIZE(THE($attacker)) } стреляет {THE($victim)} в голову!
execution-popup-gun-clumsy-internal = Вы промахиваетесь мимо головы {THE($victim)} и вместо этого стреляете себе в ногу!
execution-popup-gun-clumsy-external = { CAPITALIZE(THE($attacker)) } промахивается мимо {THE($victim)} и стреляет себе в ногу!
execution-popup-gun-empty = { CAPITALIZE(THE($weapon)) } щёлкает.
execution-popup-self-melee-initial-internal = Вы приставляете {THE($weapon)} к собственному горлу.
execution-popup-self-gun-initial-internal = Вы засовываете дуло {THE($weapon)} себе в рот.
execution-popup-self-melee-initial-external = { CAPITALIZE(THE($attacker)) } приставляет {POSS-ADJ($attacker)} {$weapon} к собственному горлу.
execution-popup-self-gun-initial-external = { CAPITALIZE(THE($attacker)) } засовывает дуло {POSS-ADJ($attacker)} {$weapon} себе в рот.
execution-popup-self-melee-complete-internal = Вы перерезаете себе горло!
execution-popup-self-gun-complete-internal = Вы стреляете себе в голову!
execution-popup-self-melee-complete-external = { CAPITALIZE(THE($attacker)) } перерезает себе горло!
execution-popup-self-gun-complete-external = { CAPITALIZE(THE($attacker)) } стреляет себе в голову!
