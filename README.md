Приложение на ASP.NET Список контактов

В приложении настроено Code First посредством EntityFramework

В Web.config можно подключить базу данных или файл Microsoft SQL Server 



Для запуска сайта необходима предустановленная версия .Net Framework 4.7.2

Для подключение к базе данных Sql server нужно в web.config заменить 
data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Database1.mdf
на data source=ИмяСервера;Initial Catalog=ИмяБазыДанных
Если базы данных нет, то она создастся автоматически

Можно использовать файл базы данных, то необходимо установленная программа Microsoft SQL Server 2008+ LocalDB. В файле web.config уже прописано подключение.

