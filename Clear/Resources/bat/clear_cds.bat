@echo off
taskkill /IM "iikoFront.Net.exe"
taskkill /IM "IIKOCustomerDisplay.Front.exe"
del /f /s /q "C:\ProgramData\iiko\CustomerDisplayScreen"
RD %SystemDrive%\RECYCLER /Q/S
exit