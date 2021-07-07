@echo off
chcp 65001
Echo "Удаление обновлений Windows мешающих работе QR кодов на принтерах"
wusa /uninstall /kb:5000803
wusa /uninstall /kb:5000802
ECHO "Готово"
exit