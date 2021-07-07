chcp 65001
@echo off
cd %programdata%/CLEAR_bat
mkdir temp
cd ./temp
mkdir update_%date%
cd ./update_%date%"
%programdata%/CLEAR_bat/bin/curl.exe -LJO "https://github.com/ru2mix/ru2mix/raw/main/update_c.zip"
mkdir temp
cd %programdata%/CLEAR_bat/temp/update_%date%/temp"
%programdata%/CLEAR_bat/bin/unzip.exe "%programdata%/CLEAR_bat/temp/update_%date%/update_c.zip"
xcopy "%programdata%/CLEAR_bat/temp/update_%date%/temp" "%programdata%/CLEAR_bat" /E/I/Y
cd "%programdata%/CLEAR_bat/"
rmdir /S/Q "%programdata%/CLEAR_bat/temp/update_%date%/"
ECHO "Обновлено успешно!"
exit