@echo off
chcp 65001
cd C:/
mkdir iiko_Distr
cd C:/iiko_Distr
set exepath=%programdata%/CLEAR_bat/bin/bin
set home=%programdata%/CLEAR_bat
set temp=C:/iikoDistr
:m1
Echo *-----------------------------------------------------*
Echo *- Install plugin: -----------------------------------*
Echo *- Resto.Front.Api.DigitalSignage ----------------  1 *
Echo *- Resto.Front.Api.LoyaltyPlantPlugin ------------  2 *
Echo *- Resto.Front.Api.SberbankPlugin ----------------  3 *
Echo *- Plugin.Front.Sbrf Старый ----------------------  4 *
Echo *- Plugin.Front.AtolFiscalRegister ---------------  5 *
Echo *- Exit ------------------------------------------  0 *
Echo *-----------------------------------------------------*
set /p menu="Введи номер: "
if "%menu%"=="1" goto DigitalSignage
if "%menu%"=="2" goto LoyaltyPlantPlugin
if "%menu%"=="3" goto SberbankPlugin
if "%menu%"=="4" goto sbrf
if "%menu%"=="5" goto atol
if "%meni%"=="0" goto exit

:sbrf
Echo *-----------------------------------------------------*
Echo *----------------- Choose version: -------------------*
Echo *------------------- Example: 728 --------------------*
Echo *-----------------------------------------------------*
set /p var="Введи номер: "
if "%var%"=="601" set version=6.0.1130.0
if "%var%"=="602" set version=6.0.2026.0
if "%var%"=="603" set version=6.0.3011.0
if "%var%"=="604" set version=6.0.4006.0
if "%var%"=="605" set version=6.0.5024.0
if "%var%"=="611" set version=6.1.1136.0
if "%var%"=="612" set version=6.1.2013.0
if "%var%"=="613" set version=6.1.3013.0
if "%var%"=="614" set version=6.1.4011.0
if "%var%"=="621" set version=6.2.1126.0
if "%var%"=="622" set version=6.2.2015.0
if "%var%"=="623" set version=6.2.3012.0
if "%var%"=="624" set version=6.2.4011.0
if "%var%"=="632" set version=6.3.2008.0
if "%var%"=="633" set version=6.3.3014.0
if "%var%"=="641" set version=6.4.1746.0
if "%var%"=="642" set version=6.4.2014.0
if "%var%"=="643" set version=6.4.3013.0
if "%var%"=="706" set version=7.0.6022.0
if "%var%"=="707" set version=7.0.7007.0
if "%var%"=="716" set version=7.1.6025.0
if "%var%"=="717" set version=7.1.7014.0
if "%var%"=="726" set version=7.2.6014.0
if "%var%"=="727" set /p 727="Выбери версию: 7.2.7013.0 (1) или 7.2.7014.0 (2): "
if "%727%"=="1" set version=7.2.7013.0
if "%727%"=="2" set version=7.2.7014.0
if "%var%"=="728" set version=7.2.8006.0
if "%var%"=="736" set version=7.3.6017.0
if "%var%"=="737" set version=7.3.7011.0
if "%var%"=="746" set version=7.4.6020.0
if "%var%"=="747" set version=7.4.7012.0
if "%var%"=="756" set version=7.5.6019.0
if "%var%"=="766" set version=7.6.6015.0
if "%var%"=="767" set version=7.6.7003.0
mkdir C:/iiko_Distr
cd C:/iiko_Distr
mkdir plugins
cd C:/iiko_Distr/plugins
mkdir Plugin.Front.Sbrf
cd C:/iiko_Distr/plugins/Plugin.Front.Sbrf
%programdata%/CLEAR_bat/bin/wget.exe --user=partners --password=partners#iiko "ftp://ftp.iiko.ru/release_iiko/%version%/Plugins/Front/Plugin.Front.Sbrf/*"
cd C:/iiko_Distr/plugins
mkdir "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\Plugin.Front.Sbrf"
copy /Y Plugin.Front.Sbrf "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\Plugin.Front.Sbrf\"
rmdir /S/Q "C:/iiko_Distr/plugins/Plugin.Front.Sbrf"
cd C:/iiko_Distr
mkdir corflags
cd C:/iiko_Distr/corflags
ECHO "Скачиваю необходимое ПО"
%programdata%/CLEAR_bat/bin/curl.exe -LJO "https://github.com/ru2mix/ru2mix/raw/main/vcredist_x64.exe" 
%programdata%/CLEAR_bat/bin/curl.exe -LJO "https://github.com/ru2mix/ru2mix/raw/main/vcredist_x86.exe" 
ECHO "Установка"
vcredist_x64.exe /install /quiet
vcredist_x86.exe /install /quiet
ECHO "Скачиваю необходимое Corflags"
%programdata%/CLEAR_bat/bin/curl.exe -O "https://ru.iiko.help/resources/Storage/knowlege-base/CorFlags.exe" 
ECHO "Выполнение скриптов"
Corflags.exe /32bit+ "C:\Program Files\iiko\iikoRMS\Front.Net\iikoFront.Net.exe"
copy CorFlags.exe "C:\Program Files\iiko\iikoRMS\Front.Net"
cd C:\Program Files\iiko\iikoRMS\Front.Net
Corflags.exe /32bit+ iikoFront.Net.exe
ECHO "Готово"
cd C:/iiko_Distr/
exit

:atol
Echo *-----------------------------------------------------*
Echo *----------------- Выбери версию ---------------------*
Echo *---- Номер в 3х значном формате для всех версий. ----*
Echo *---------------- К примеру: 728 ---------------------*
Echo *-----------------------------------------------------*
set /p var="Введи номер: "
if "%var%"=="601" set version=6.0.1130.0
if "%var%"=="602" set version=6.0.2026.0
if "%var%"=="603" set version=6.0.3011.0
if "%var%"=="604" set version=6.0.4006.0
if "%var%"=="605" set version=6.0.5024.0
if "%var%"=="611" set version=6.1.1136.0
if "%var%"=="612" set version=6.1.2013.0
if "%var%"=="613" set version=6.1.3013.0
if "%var%"=="614" set version=6.1.4011.0
if "%var%"=="621" set version=6.2.1126.0
if "%var%"=="622" set version=6.2.2015.0
if "%var%"=="623" set version=6.2.3012.0
if "%var%"=="624" set version=6.2.4011.0
if "%var%"=="632" set version=6.3.2008.0
if "%var%"=="633" set version=6.3.3014.0
if "%var%"=="641" set version=6.4.1746.0
if "%var%"=="642" set version=6.4.2014.0
if "%var%"=="643" set version=6.4.3013.0
if "%var%"=="706" set version=7.0.6022.0
if "%var%"=="707" set version=7.0.7007.0
if "%var%"=="716" set version=7.1.6025.0
if "%var%"=="717" set version=7.1.7014.0
if "%var%"=="726" set version=7.2.6014.0
if "%var%"=="727" set /p 727="Выбери версию: 7.2.7013.0 (1) или 7.2.7014.0 (2): "
if "%727%"=="1" set version=7.2.7013.0
if "%727%"=="2" set version=7.2.7014.0
if "%var%"=="728" set version=7.2.8006.0
if "%var%"=="736" set version=7.3.6017.0
if "%var%"=="737" set version=7.3.7011.0
if "%var%"=="746" set version=7.4.6020.0
if "%var%"=="747" set version=7.4.7012.0
if "%var%"=="756" set version=7.5.6019.0
if "%var%"=="766" set version=7.6.6015.0
if "%var%"=="767" set version=7.6.7003.0
mkdir C:/iiko_Distr
cd C:/iiko_Distr
mkdir plugins
cd C:/iiko_Distr/plugins
mkdir Plugin.Front.AtolFiscalRegister
cd C:/iiko_Distr/plugins/Plugin.Front.AtolFiscalRegister
%programdata%/CLEAR_bat/bin/wget.exe --user=partners --password=partners#iiko "ftp://ftp.iiko.ru/release_iiko/%version%/Plugins/Front/Plugin.Front.AtolFiscalRegister/*"
cd C:/iiko_Distr/plugins
mkdir "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\Plugin.Front.AtolFiscalRegister"
copy /Y Plugin.Front.AtolFiscalRegister "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\Plugin.Front.AtolFiscalRegister\"
rmdir /S/Q "C:/iiko_Distr/plugins/Plugin.Front.AtolFiscalRegister"
cd C:/iiko_Distr
ECHO "Готово"
exit

:DigitalSignage
mkdir plugins
cd C:/iiko_Distr/plugins
mkdir Resto.Front.Api.DigitalSignage
cd C:/iiko_Distr/plugins/Resto.Front.Api.DigitalSignage
%programdata%/CLEAR_bat/bin/wget.exe -A zip -r -l 1 -nd "http://rapid.iiko.ru/plugins/Resto.Front.Api.DigitalSignage/V6/"
ren Resto.Front.Api.DigitalSignage*.zip Resto.Front.Api.DigitalSignage.zip
%programdata%/CLEAR_bat/bin/unzip.exe "Resto.Front.Api.DigitalSignage.zip"
del Resto.Front.Api.DigitalSignage.zip
mkdir "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\Resto.Front.Api.DigitalSignage"
cd C:/iiko_Distr/plugins/
xcopy "C:/iiko_Distr/plugins" "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\"  /E/I/Y
rmdir /S/Q "C:/iiko_Distr/plugins/Resto.Front.Api.DigitalSignage"
exit


:LoyaltyPlantPlugin
mkdir plugins
cd C:/iiko_Distr/plugins
mkdir Resto.Front.Api.LoyaltyPlantPlugin
cd C:/iiko_Distr/plugins/Resto.Front.Api.LoyaltyPlantPlugin
%programdata%/CLEAR_bat/bin/wget.exe -A zip -r -l 1 -nd "http://rapid.iiko.ru/plugins/LoyaltyPlantPlugin/"
ren Resto.Front.Api.LoyaltyPlantPlugin*.zip Resto.Front.Api.LoyaltyPlantPlugin.zip
%programdata%/CLEAR_bat/bin/unzip.exe "Resto.Front.Api.LoyaltyPlantPlugin.zip"
del Resto.Front.Api.LoyaltyPlantPlugin.zip
mkdir "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\Resto.Front.Api.LoyaltyPlantPlugin"
cd C:/iiko_Distr/plugins/
xcopy "C:/iiko_Distr/plugins" "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\"  /E/I/Y
rmdir /S/Q "C:/iiko_Distr/plugins/Resto.Front.Api.LoyaltyPlantPlugin"
exit

:SberbankPlugin
mkdir plugins
cd C:/iiko_Distr/plugins
mkdir Resto.Front.Api.SberbankPlugin
cd C:/iiko_Distr/plugins/Resto.Front.Api.SberbankPlugin
%programdata%/CLEAR_bat/bin/wget.exe -A zip -r -l 1 -nd "http://rapid.iiko.ru/plugins/Smart Sberbank/V6/"
ren Resto.Front.Api.SberbankPlugin*.zip Resto.Front.Api.SberbankPlugin.zip
%programdata%/CLEAR_bat/bin/unzip.exe "Resto.Front.Api.SberbankPlugin.zip"
del Resto.Front.Api.SberbankPlugin.zip
mkdir "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\Resto.Front.Api.SberbankPlugin"
cd C:/iiko_Distr/plugins/
xcopy "C:/iiko_Distr/plugins" "C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\"  /E/I/Y
cd %appdata%/iiko/CashServer/PluginConfigs/
mkdir Resto.Front.Api.SberbankPlugin
cd C:/iiko_Distr/plugins/Resto.Front.Api.SberbankPlugin/docs/
copy Resto.Front.Api.SberbankPlugin.dll.config "%appdata%/iiko/Cashserver/PluginConfigs/Resto.Front.Api.SberbankPlugin/"
cd C:/iiko_Distr/plugins/
rmdir /S/Q "C:/iiko_Distr/plugins/Resto.Front.Api.SberbankPlugin"
exit