@echo off
chcp 65001
:m1
Echo
Echo  #************************************#
Echo  *          Очистка диска       v 2.20*
Echo  *------------------------------------*
Echo  *             ВНИМАНИЕ!              *
Echo  *------------------------------------*
@echo off
goto check_Permissions
:check_Permissions
    net session >nul 2>&1
    if %errorLevel% == 0 (
        echo  *          ГОТОВ К РАБОТЕ            *
    ) else (
        echo  *  ЗАПУСКАТЬ ОТ ИМЕНИ АДМИНИСТРАТОРА *	
    )
Echo  *------------------------------------*
Echo  *  Очистка всего это удаление:       *
Echo  *  БД+Plazius, БД iikoCard5, Логи:   *
Echo  *  Фронта+CDS, Windows (Temp+Update) *
Echo  *------------------------------------*
Echo  *  Так же синхронизация времени и    *
Echo  *отключение службы обновления windows*
Echo  *------------------------------------*
Echo  * Удалить все -------------------  0 *
Echo  * Чистка всего без Plazius ------  1 *
Echo  * Удалить логи фронта -----------  2 *
Echo  * Удалить логи CDS --------------  3 *
Echo  * Удалить Plazius (БД+Service)---  4 *
Echo  * Удалить системные файлы -------  5 *
Echo  * Почистить iikoCard5 -----------  6 *
Echo  * Отключить службы Windows ------  7 *
Echo  * Ошибки синхронизации времени --  8 *
Echo  * Запустить iikoFront------------  9 *
Echo  * QR нетмонет (WIN UPD) --------- 10 *
Echo  * CorFlags ---------------------- 11 *
Echo  * Проброс портов iikoFront ------ 12 *
Echo  *------------------------------------*
Echo  *  Robert Lato           t.me/rlato  *
Echo  #************************************#

set /p var="Ваш выбор: "
if "%var%"=="0" goto 0
if "%var%"=="1" goto 1
if "%var%"=="2" goto 2
if "%var%"=="3" goto 3
if "%var%"=="4" goto 4
if "%var%"=="5" goto 5
if "%var%"=="6" goto 6
if "%var%"=="7" goto 7
if "%var%"=="8" goto 8
if "%var%"=="9" goto 9
if "%var%"=="10" goto 10
if "%var%"=="11" goto 11
if "%var%"=="12" goto 12
goto m1

:0
ECHO Удалем все.
ECHO "Останавливаю iikoFront"
taskkill /IM "iikoFront.Net.exe"

ECHO "Останавливаю CDS"
taskkill /IM "IIKOCustomerDisplay.Front.exe"

ECHO "Останавливаю службу iikoCard5POS"
NET STOP iikoCard5POS

ECHO "Останавливаю службу iikoNetService"
NET STOP iikoNetService

ECHO "Отключаю обновления windows"
net stop wuauserv
sc config wuauserv start=disabled

timeout /t 5

ECHO "Обновляю время с сервера windows"
ECHO Запускаю службу
net start w32time
Запускаю синхронизацию
w32tm /resync
echo Синхронизация завершена
ECHO "Готово"

ECHO "Чищу логи iikoFront"
del /f /s /q "%appdata%\iiko\CashServer\Logs"

ECHO "Чищу БД iikoCard5POS"
del /f /s /q "C:\Users\iikoCard5POS\AppData\Roaming\iiko\iikoCard5"
del /f /s /q "C:\Windows\ServiceProfiles\iikoCard5POS\AppData\Roaming\iiko\iikoCard5\"

ECHO "Добавляю порт 7001 в исключения"
netsh http add urlacl url=http://+:7001/ user=Everyone
netsh http add urlacl url=http://+:7001/ user=Все

ECHO "Добавляю iikoCard5POS в группу Администраторы"
net localgroup "Администраторы" "NT SERVICE\iikoCard5POS" /add
net localgroup "Administrators" "NT SERVICE\iikoCard5POS" /add

ECHO "Чищу логи CDS"
del /f /s /q "C:\ProgramData\iiko\CustomerDisplayScreen"

ECHO "Чищу Windows (Обновления и файлы кеша)"
del %Temp%\*.* /S /F /Q
del C:\Windows\Temp\*.* /S /F /Q
SET Path2Del=C:\Windows\Temp\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
SET Path2Del=%Temp%\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
del c:\Windows\SoftwareDistribution\Download\*.* /f /s /q

ECHO "Удаляю Plazius POS"
cd C:\Program Files\iiko\Plazius.Pos
start unistall.bat
del /f /s /q "C:\Program Files\iiko\Plazius.Pos"
ECHO "Plazius POS removed"
ECHO "Чищу корзину"
RD %SystemDrive%\RECYCLER /Q/S
ECHO "Запускаю службу iikoCard5POS"
NET START iikoCard5POS
ECHO "Запускаю iikoFront"
cd "C:\Program Files\iiko\iikoRMS\Front.Net\"
start iikoFront.Net.exe
ECHO "Готово"
goto m1

:1
Echo  Чистка всего без Plazius
ECHO Удалем все.
ECHO "Останавливаю iikoFront"
taskkill /IM "iikoFront.Net.exe"
ECHO "Останавливаю CDS"
taskkill /IM "IIKOCustomerDisplay.Front.exe"
ECHO "Останавливаю службу iikoCard5POS"
NET STOP iikoCard5POS
ECHO "Отключаю обновления windows"
net stop wuauserv
sc config wuauserv start=disabled
timeout /t 5
ECHO "Обновляю время с сервера windows"
ECHO Запускаю службу
net start w32time
Запускаю синхронизацию
w32tm /resync
echo Синхронизация завершена
ECHO "Готово"
ECHO "Чищу логи iikoFront"
del /f /s /q "%appdata%\iiko\CashServer\Logs"
ECHO "Чищу БД iikoCard5POS"
del /f /s /q "C:\Users\iikoCard5POS\AppData\Roaming\iiko\iikoCard5"
del /f /s /q "C:\Windows\ServiceProfiles\iikoCard5POS\AppData\Roaming\iiko\iikoCard5\"
ECHO "Добавляю порт 7001 в исключения"
netsh http add urlacl url=http://+:7001/ user=Everyone
netsh http add urlacl url=http://+:7001/ user=Все
ECHO "Добавляю iikoCard5POS в группу Администраторы"
net localgroup "Администраторы" "NT SERVICE\iikoCard5POS" /add
net localgroup "Administrators" "NT SERVICE\iikoCard5POS" /add
ECHO "Чищу логи CDS"
del /f /s /q "C:\ProgramData\iiko\CustomerDisplayScreen"
ECHO "Чищу Windows (Обновления и файлы кеша)"\
del %Temp%\*.* /S /F /Q
del C:\Windows\Temp\*.* /S /F /Q
SET Path2Del=C:\Windows\Temp\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
SET Path2Del=%Temp%\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
del c:\Windows\SoftwareDistribution\Download\*.* /f /s /q
ECHO "Чищу корзину"
RD %SystemDrive%\RECYCLER /Q/S
ECHO "Запускаю службу iikoCard5POS"
NET START iikoCard5POS
ECHO "Запускаю iikoFront"
cd "C:\Program Files\iiko\iikoRMS\Front.Net\"
start iikoFront.Net.exe
ECHO "Готово"
goto m1

:2
Echo  Удалить логи фронта
ECHO "Останавливаю iikoFront"
taskkill /IM "iikoFront.Net.exe"
ECHO "Clear folder %appdata%\iiko\CashServer\Logs"
del /f /s /q "%appdata%\iiko\CashServer\Logs"
ECHO "Чищу корзину"
RD %SystemDrive%\RECYCLER /Q/S
ECHO "Готово"
goto m1

:3
Echo  Удалить логи CDS
ECHO "Останавливаю iikoFront"
taskkill /IM "iikoFront.Net.exe"
taskkill /IM "IIKOCustomerDisplay.Front.exe"
ECHO "Clear folder C:\ProgramData\iiko\CustomerDisplayScreen"
del /f /s /q "C:\ProgramData\iiko\CustomerDisplayScreen"
ECHO "Чищу корзину"
RD %SystemDrive%\RECYCLER /Q/S
ECHO "Готово"
goto m1

:4
Echo  Удалить Plazius (БД+Service)
ECHO "Останавливаю службу iikoNetService"
NET STOP iikoNetService
ECHO "Remove Plazius POS"
cd C:\Program Files\iiko\Plazius.Pos
start unistall.bat
del /f /s /q "C:\Program Files\iiko\Plazius.Pos"
ECHO "Plazius POS removed"
ECHO "Чищу корзину"
RD %SystemDrive%\RECYCLER /Q/S
ECHO "Готово"
goto m1

:5
Echo  Удалить системные файлы
ECHO "Чищу Windows (Обновления и файлы кеша)"
del %Temp%\*.* /S /F /Q
del C:\Windows\Temp\*.* /S /F /Q
SET Path2Del=C:\Windows\Temp\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
SET Path2Del=%Temp%\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
del c:\Windows\SoftwareDistribution\Download\*.* /f /s /q
ECHO "Готово"
goto m1

:6
Echo  Почистить iikoCard5

ECHO "Останавливаю iikoFront"
taskkill /IM "iikoFront.Net.exe"
ECHO "Останавливаю службу iikoCard5POS"
NET STOP iikoCard5POS
ECHO "Чищу БД iikoCard5POS"
del /f /s /q "C:\Users\iikoCard5POS\AppData\Roaming\iiko\iikoCard5"
del /f /s /q "C:\Windows\ServiceProfiles\iikoCard5POS\AppData\Roaming\iiko\iikoCard5\"
ECHO "Добавляю порт 7001 в исключения"
netsh http add urlacl url=http://+:7001/ user=Everyone
netsh http add urlacl url=http://+:7001/ user=Все
ECHO "Добавляю iikoCard5POS в группу Администраторы"
net localgroup "Администраторы" "NT SERVICE\iikoCard5POS" /add
net localgroup "Administrators" "NT SERVICE\iikoCard5POS" /add
ECHO "Чищу корзину"
RD %SystemDrive%\RECYCLER /Q/S
ECHO "Запускаю службу iikoCard5POS"
NET START iikoCard5POS
ECHO "Готово"
goto m1

:7
Echo Отключить обновление Windows
net stop wuauserv
sc config wuauserv start=disabled
:checkPrivileges
NET FILE 1>NUL 2>NUL
if '%errorlevel%' == '0' ( goto gotPrivileges ) else ( goto getPrivileges )

:getPrivileges
if '%1'=='ELEV' (echo ELEV & shift /1 & goto gotPrivileges)

setlocal DisableDelayedExpansion
set "batchPath=%~0"
setlocal EnableDelayedExpansion
ECHO Set UAC = CreateObject^("Shell.Application"^) > "%temp%\OEgetPrivileges.vbs"
ECHO args = "ELEV " >> "%temp%\OEgetPrivileges.vbs"
ECHO For Each strArg in WScript.Arguments >> "%temp%\OEgetPrivileges.vbs"
ECHO args = args ^& strArg ^& " "  >> "%temp%\OEgetPrivileges.vbs"
ECHO Next >> "%temp%\OEgetPrivileges.vbs"
ECHO UAC.ShellExecute "!batchPath!", args, "", "runas", 1 >> "%temp%\OEgetPrivileges.vbs"
"%SystemRoot%\System32\WScript.exe" "%temp%\OEgetPrivileges.vbs" %*
exit /B

:gotPrivileges
if '%1'=='ELEV' shift /1
setlocal & pushd .
cd /d %~dp0

:Start
reg add HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\ /v NoAutoUpdate /t REG_DWORD /d 1 /f
reg add HKLM\SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\WindowsUpdate\AU\ /v NoAutoUpdate /t REG_DWORD /d 1 /f
"%~dp0SetACL.exe" -on "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU" -ot reg -actn setowner -ownr "n:%USERNAME%"
"%~dp0SetACL.exe" -on "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU" -ot reg -actn ace -ace "n:%USERNAME%;p:full"
"%~dp0SetACL.exe" -on "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU" -ot reg -actn ace -ace "n:SYSTEM;p:read"
reg add HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\ /v NoAutoUpdate /t REG_DWORD /d 1 /f
reg add HKLM\SOFTWARE\WOW6432Node\Policies\Microsoft\Windows\WindowsUpdate\AU\ /v NoAutoUpdate /t REG_DWORD /d 1 /f

Echo Отключаю брандмауер
netsh advfirewall set domainprofile state off
netsh advfirewall set privateprofile state off
netsh advfirewall set publicprofile state off
netsh advfirewall set  currentprofile state off
netsh advfirewall set  allprofiles state off
netsh firewall set opmode mode=DISABLE
reg add "HKLM\SOFTWARE\Policies\Microsoft\Windows Defender" /v "DisableAntiSpyware" /t "REG_DWORD" /d "1" /f
netsh firewall set notifications mode = disable profile = allprofiles
netsh advfirewall firewall set notification mode = disable profile = all
goto m1

:8
Исправление неполадок синхронизации времени
cd C:/Windows/System32
ECHO Останавливаем службу времени, если запущена.
net stop w32time
ECHO Отключаем режим отладки
w32tm /debug /disable
ECHO Удаляем регистрацию службы
w32tm /unregister
ECHO По новому регистрируем службу
w32tm /register
ECHO Запускаю службу
net start w32time
Запускаю синхронизацию
w32tm /resync
echo Синхронизация завершена
ECHO "Готово"
goto m1

:9
ECHO "Запускаю iikoFront"
cd "C:\Program Files\iiko\iikoRMS\Front.Net\"
start iikoFront.Net.exe
ECHO "Готово"
goto m1

:10
Echo "Удаление обновлений Windows мешающих работе QR кодов на принтерах"
wusa /uninstall /kb:5000803
wusa /uninstall /kb:5000802
ECHO "Готово"
goto m1

:11
ECHO "Создаю директорию C:/iiko_temp"
cd C:/
mkdir iiko_Temp
cd "C:/iiko_Temp"
ECHO "Скачиваю необходимое ПО"
curl -LJO "https://github.com/ru2mix/ru2mix/raw/main/vcredist_x64.exe" 
curl -LJO "https://github.com/ru2mix/ru2mix/raw/main/vcredist_x86.exe" 
ECHO "Установка"
vcredist_x64.exe /install /quiet
vcredist_x86.exe /install /quiet
ECHO "Скачиваю необходимое Corflags"
curl -O "https://ru.iiko.help/resources/Storage/knowlege-base/CorFlags.exe" 
ECHO "Выполнение скриптов"
Corflags.exe /32bit+ "C:\Program Files\iiko\iikoRMS\Front.Net\iikoFront.Net.exe"
copy CorFlags.exe "C:\Program Files\iiko\iikoRMS\Front.Net"
cd C:\Program Files\iiko\iikoRMS\Front.Net
Corflags.exe /32bit+ iikoFront.Net.exe
ECHO "Готово"
goto m1

:12
Echo Проброс портов iikoFront
Echo Добавляю порты для RU Windows
netsh http add urlacl url=http://+:8100/ user=Все
netsh http add urlacl url=http://+:8080/ user=Все
netsh http add urlacl url=https://+:443/ user=Все
netsh http add urlacl url=http://+:8506/ user=Все
netsh http add urlacl url=http://+:9100/ user=Все
netsh http add urlacl url=http://+:9000/ user=Все
netsh http add urlacl url=http://+:9001/ user=Все
netsh http add urlacl url=http://+:9002/ user=Все
netsh http add urlacl url=http://+:9003/ user=Все
netsh http add urlacl url=http://+:9004/ user=Все
netsh http add urlacl url=http://+:5060/ user=Все
netsh http add urlacl url=http://+:8510/ user=Все
netsh http add urlacl url=http://+:8000/ user=Все
netsh http add urlacl url=http://+:8001/ user=Все
netsh http add urlacl url=http://+:8002/ user=Все
netsh http add urlacl url=http://+:9000/ user=Все

Echo Добавляю порты для EU Windows
netsh http add urlacl url=http://+:8100/ user=Everyone
netsh http add urlacl url=http://+:8080/ user=Everyone
netsh http add urlacl url=https://+:443/ user=Everyone
netsh http add urlacl url=http://+:8506/ user=Everyone
netsh http add urlacl url=http://+:9100/ user=Everyone
netsh http add urlacl url=http://+:9000/ user=Everyone
netsh http add urlacl url=http://+:9001/ user=Everyone
netsh http add urlacl url=http://+:9002/ user=Everyone
netsh http add urlacl url=http://+:9003/ user=Everyone
netsh http add urlacl url=http://+:9004/ user=Everyone
netsh http add urlacl url=http://+:5060/ user=Everyone
netsh http add urlacl url=http://+:8510/ user=Everyone
netsh http add urlacl url=http://+:8000/ user=Everyone
netsh http add urlacl url=http://+:8001/ user=Everyone
netsh http add urlacl url=http://+:8002/ user=Everyone
netsh http add urlacl url=http://+:9000/ user=Everyone
goto m1