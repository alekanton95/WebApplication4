Приложение на ASP.NET Список контактов

В приложении настроено Code First посредством EntityFramework

В Web.config нужно подключить базу данных или файл Microsoft SQL Server 



Для запуска сайта необходима предустановленная версия .Net Framework 4.7.2

Для подключение к базе данных Sql server нужно в web.config заменить 
data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Database1.mdf
на data source=ИмяСервера;Initial Catalog=ИмяБазыДанных
Если базы данных нет, то она создастся автоматически

Можно использовать файл базы данных, то необходимо установленная программа Microsoft SQL Server 2008+ LocalDB. В файле web.config уже прописано подключение.

В приложении не произведена настройка логирования ошибок, возможны экстренное завершение приложения.

Настроено валидация клиентской и серверной стороны. При возникновении ошибки валидации в серверной стороне произойдет возврат на страницу с записью с описанием ошибки.
При проверке валидации клиентской части, пока не будут устранены ошибки, нельзя отправить данные. Кнопка Сохранения неактивна, до устранения ошибки валидации.

Для разворачивания на сервере IIS можно воспользоваться инструкцией на сайте https://metanit.com/sharp/mvc/13.2.php

Опубликованная версия не сформирована в данном репозитории, то есть необходимо Visual Studio

Приложение писалось на Visual Studio 2017, также произведена проверка на 2015 версии.
