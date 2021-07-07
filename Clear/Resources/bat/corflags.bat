@echo off
cd C:/iiko_Distr
Corflags.exe /32bit+ "C:\Program Files\iiko\iikoRMS\Front.Net\iikoFront.Net.exe"
copy CorFlags.exe "C:\Program Files\iiko\iikoRMS\Front.Net"
cd C:\Program Files\iiko\iikoRMS\Front.Net
Corflags.exe /32bit+ iikoFront.Net.exe
exit