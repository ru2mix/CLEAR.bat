@echo off
taskkill /IM "iikoFront.Net.exe"
del /f /s /q "%appdata%\iiko\CashServer\Logs"
RD %SystemDrive%\RECYCLER /Q/S
cd %programdata%/cclear/
exit