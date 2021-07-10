# > CLEAR.bat - Утилита по работе с iiko
Полное описание скриптов программы в самой программе во вкладке "Справка".

Так же читайте Wiki:

https://github.com/ru2mix/CLEAR.bat/wiki

Скачать:

BAT >>> v 2.20 Stable - [clear_v2.20.bat](https://github.com/ru2mix/CLEAR.bat/blob/main/clear_v2.20.bat) (Не умеет скачивать плагины, только очистка, последняя стабильная версия в bat формате, для любителей мелких файлов всего 16кб)

EXE >>>  v 5.2 Stable - [CLEAR.bat.exe](https://github.com/ru2mix/CLEAR.bat/raw/main/CLEAR.bat.exe) (Умеет все! Будет обновляться весит 3мб. Нужен .Net 4.5.2

[VirusTotal](https://www.virustotal.com/gui/file/941a061327404485ed25df160096bc327682a29e922392c2d10d61f527cb042c/detection)

Скриншоты:

![](https://github.com/ru2mix/CLEAR.bat/raw/main/IMG/v3/Home.JPG)
![](https://github.com/ru2mix/CLEAR.bat/blob/main/IMG/img_scr/23_1.JPG?raw=true)
![](https://github.com/ru2mix/CLEAR.bat/blob/main/IMG/img_scr/24.JPG?raw=true)
___________________________________________________________________________________________________________

Святая корова! Что же она умеет? А вот что:

1) Чистит систему (Особенно полезно когда у клиента 50гб SSD)

2) Отключает службы windows

3) Пробрасывает порты iikoFront

4) Предотвращает возникновения проблем с iikoCard (Скрипты оптимизации)

5) Умеет самостоятельно скачивать дистрибутивы ([Office: Chain, Back][iikoFront][RMS: iikoRMS, iikoChain]) - с FTP iiko в папку C:/iiko_Distr. (Начиная с 6й версии iiko)

6) Умеет самостоятельно устанавливать плагины LoyaltyPlanet, SBRF, Sberbank.API (полная установка, в т.ч подкидывает файлик в CashServer), DigitalSignature (iikoWeb CDS)

7) Чистит логи фронта, CDS, винды.

и много чего еще)

Программа в стадии активной разработки. 



Ваши предложения и улучшения жду в личку (Роберт Лато), в телегу ( https://t.me/rlato ) или в файлик: https://docs.google.com/spreadsheets/d/1vxJ6OYsSdb1AQHCuQamftnpz6fjmbKjnF_LBQWWz9N0/edit#gid=0

Всем добра! 

/// UPD List

Версия 5.1 чистилки вышла!
После долгих бессоных ночей, я наконец таки рад представить вам новую версию чистилки))
Достаточно много нового, а дальше будет больше!
Что нового:

1) Проверка на наличие новой версии при старте программы.
2) GUI скачивалка с FTP дистрибутивы iiko
- Добавлена возможность скачивать папки серверов.
- Обновление версий с FTP iiko, если вышла новая версия, а я не успел обновить, программа это сделает сама!
- На данный момент, не делал отображения процесса скачивания, по этому просто ждите пока она не отвиснет))
3) Добавил скачку Plugin.Front.AtolFiscalRegister и его установку. Обязательно попробуйте автоматическую установку плагинов, это классно))
- Если использовали предыдущую версию 5.0.1, то перейдите на вкладку Справка и нажмите "Удалить скрипты".
Иначе скрипты не обновятся. В будующих версиях добавлю автоматическое обновление скриптов.
4) Оптимизация скриптов и исправление ошибок.

Если что как всегда пишите мне в телеграм https://t.me/rlato или в личку слака)
Описание всех скриптов есть в самой программе во вкладке "Справка".

//

CLEAR.bat версии 5.2!
Очень много нововведений и работы над скриптами.

1) Перевел автоустановку плагинов на C# теперь все намного удобнее. Можно скачать все плагины кроме тех которые в архиве (не вижу смысла в них, но если нужно будет добавлю).
- Скачка и подкидывание плагина в папку plugins с ftp, возможность сразу запустить скрипт corflags просто поставив галку (скрипт запускает фронт как 32 битный процесс)
2) Допиливание скрипта скачки с ftp к сожалению процент загрузки не смог поставить, но перевел на более быстрый движок, и теперь показывает скорость скачки))
3) Теперь можно из скрипта проверить имя и весрию сервера iiko (Спасибо тебе Егор Бурдаков:D <3 )
4) Скачка различных версий .Net на выбор) Спасибо Антон))
5) Уменьшение веса чистилки с 13мб до 3мб))
6) Изменение иконки (Спасибо Леонид)

И еще куча мелких изменений. Программа стала более стабильной, и удобной)

//

UPD: 525 исправление ошибок и добавление новых фич
Добавлена возможность удалять установленные плагины
Добавлено отображение скачки и установки плагинов
Исправлена ошибка скачки RMS Server в папку чейна
Добавлен запуск exe чейна и рмс после скачки.
Обновляйтесь(smile)

//

UPD: 526 Stable

Добавил функции:

Автоматическое открывание беков по пути /RMS/OfficeXXX - где XXX версия iiko, или iikoChain : /Chain/COfficeXXX (Указывайте путь до папки где пути до чейна и бека).

Суть работы: 
1) Нажимаете настроить: Выбираете путь до беков.
2) Вбиваете сервер iiko в строку, далее если нужно вбиваете порт, и нажимаете запустить iiko. Бек нужной версии запуститься.
3) Бек будет уже с введенным адресом, осталось только ввесли толгин и пароль.
4) Установка лицензий. Без захода в бек спрятана в меню Справка

Hotfix 526:
Исправил ошибку запуска беков, теперь их можно запустить хоть 100 штук)

Пофиксил Corflags.

//

UPD 527
- Добавлен iikoWaiter (Установка плагина)
- Установлена версия по умолчанию: 7.7.6020.0
 
 BUGFIX 527
 - Исправлены множественные ошибки отображения
 - Теперь Обновление лицензий работает корректно и действительно обновляет лицензии, так же вытащил отображение CRMID

//

UPD 530
- Добавлена утилита по работе с беками! Конфигурацией и установкой лицензий!
- Удалены старые скрипты
- Оптимизирована стартовая страница
- Добавлено автоматическое обновление, теперь не нужно скачивать установщик и переносить его в нужную папку, скрипт сделает все за вас!

Огромное спасибо тем кто поддерживает проект! 
