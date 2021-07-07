@echo off
NET STOP iikoNetService
cd C:\Program Files\iiko\Plazius.Pos
start unistall.bat
del /f /s /q "C:\Program Files\iiko\Plazius.Pos"
RD %SystemDrive%\RECYCLER /Q/S
exit