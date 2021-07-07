@echo off
chcp 65001
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
exit