command-list-langs-desc = Список языков, на которых ваша текущая сущность может говорить в данный момент.
command-list-langs-help = Использование: {$command}
command-saylang-desc = Отправить сообщение на определённом языке. Чтобы выбрать язык, можно использовать либо название языка, либо его номер в списке языков.
command-saylang-help = Использование: {$command} <language id> <message>. Пример: {$command} GalacticCommon "Hello World!". Пример: {$command} 1 "Hello World!"
command-language-select-desc = Выбрать текущий разговорный язык вашей сущности. Можно использовать либо название языка, либо его номер в списке языков.
command-language-select-help = Использование: {$command} <language id>. Пример: {$command} 1. Пример: {$command} GalacticCommon
command-language-spoken = Разговорные:
command-language-understood = Понимаемые:
command-language-current-entry = {$id}. {$language} - {$name} (текущий)
command-language-entry = {$id}. {$language} - {$name}
command-language-invalid-number = Номер языка должен быть от 0 до {$total}. Либо используйте название языка.
command-language-invalid-language = Язык {$id} не существует или вы не можете на нём говорить.
# Toolshed

command-description-language-add = Добавляет новый язык указанной сущности. Два последних аргумента указывают, должен ли он говориться/пониматься. Пример: 'self language:add "Canilunzt" true true'
command-description-language-rm = Удаляет язык у указанной сущности. Работает аналогично language:add. Пример: 'self language:rm "GalacticCommon" true true'.
command-description-language-lsspoken = Показывает все языки, на которых сущность может говорить. Пример: 'self language:lsspoken'
command-description-language-lsunderstood = Показывает все языки, которые сущность может понимать. Пример: 'self language:lssunderstood'
command-description-translator-addlang = Добавляет новый целевой язык указанной сущности-переводчику. Подробности см. в language:add.
command-description-translator-rmlang = Удаляет целевой язык у указанной сущности-переводчика. См. language:rm для подробностей.
command-description-translator-addrequired = Добавляет новый обязательный язык указанной сущности-переводчику. Пример: 'ent 1234 translator:addrequired "GalacticCommon"'
command-description-translator-rmrequired = Удаляет обязательный язык у указанной сущности-переводчика. Пример: 'ent 1234 translator:rmrequired "GalacticCommon"'
command-description-translator-lsspoken = Показывает все разговорные языки указанной сущности-переводчика. Пример: 'ent 1234 translator:lsspoken'
command-description-translator-lsunderstood = Показывает все понимаемые языки указанной сущности-переводчика. Пример: 'ent 1234 translator:lssunderstood'
command-description-translator-lsrequired = Показывает все обязательные языки указанной сущности-переводчика. Пример: 'ent 1234 translator:lsrequired'
command-language-error-this-will-not-work = Это не сработает.
command-language-error-not-a-translator = Сущность {$entity} не является переводчиком.
