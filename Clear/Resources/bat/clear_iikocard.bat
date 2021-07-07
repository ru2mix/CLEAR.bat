@echo off
taskkill /IM "iikoFront.Net.exe"
NET STOP iikoCard5POS
del /f /s /q "C:\Users\iikoCard5POS\AppData\Roaming\iiko\iikoCard5"
del /f /s /q "C:\Windows\ServiceProfiles\iikoCard5POS\AppData\Roaming\iiko\iikoCard5\"
netsh http add urlacl url=http://+:7001/ user=Everyone
netsh http add urlacl url=http://+:7001/ user=Все
net localgroup "Администраторы" "NT SERVICE\iikoCard5POS" /add
net localgroup "Administrators" "NT SERVICE\iikoCard5POS" /add
RD %SystemDrive%\RECYCLER /Q/S
NET START iikoCard5POS
exit