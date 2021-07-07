@echo off
chcp 65001

taskkill /IM "iikoFront.Net.exe"

taskkill /IM "IIKOCustomerDisplay.Front.exe"

NET STOP iikoCard5POS

NET STOP iikoNetService

net stop wuauserv
sc config wuauserv start=disabled

timeout /t 5


net start w32time
Запускаю синхронизацию
w32tm /resync



del /f /s /q "%appdata%\iiko\CashServer\Logs"


del /f /s /q "C:\Users\iikoCard5POS\AppData\Roaming\iiko\iikoCard5"
del /f /s /q "C:\Windows\ServiceProfiles\iikoCard5POS\AppData\Roaming\iiko\iikoCard5\"

netsh http add urlacl url=http://+:7001/ user=Everyone
netsh http add urlacl url=http://+:7001/ user=Все


net localgroup "Администраторы" "NT SERVICE\iikoCard5POS" /add
net localgroup "Administrators" "NT SERVICE\iikoCard5POS" /add

del /f /s /q "C:\ProgramData\iiko\CustomerDisplayScreen"

del %Temp%\*.* /S /F /Q
del C:\Windows\Temp\*.* /S /F /Q
SET Path2Del=C:\Windows\Temp\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
SET Path2Del=%Temp%\
for /R "%Path2Del%" %%F in (.) DO IF NOT "%%F"=="%Path2Del%." (RD /S /Q "%%F") ELSE (Del /F /S /Q "%Path2Del%*")
del c:\Windows\SoftwareDistribution\Download\*.* /f /s /q

cd C:\Program Files\iiko\Plazius.Pos
del /f /s /q "C:\Program Files\iiko\Plazius.Pos"

RD %SystemDrive%\RECYCLER /Q/S

NET START iikoCard5POS

cd "C:\Program Files\iiko\iikoRMS\Front.Net\"
start iikoFront.Net.exe
exit