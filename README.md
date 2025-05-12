EnterTask WebAPI
📦 Описание
Приложение представляет собой API для управления событиями и участниками. Реализовано с использованием ASP.NET Core и базы данных MS SQL, развёрнутой в Docker.

🚀 Запуск проекта
📌 Требования
.NET 7 SDK

Docker

SQL-клиент (например, SQL Server Management Studio)

📂 Шаг 1: Запуск MS SQL в Docker
Используем образ mcr.microsoft.com/mssql/server:2022-latest:

bash
Copy
Edit
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=ModsenEnterTask!1' -p 1433:1433 -d --name sqlserver mcr.microsoft.com/mssql/server:2022-latest
При использовании другого имени контейнера, обязательно обновите строку подключения в appsettings.json.

📂 Шаг 2: Восстановление базы данных
Откройте SQL Server Management Studio.

Подключитесь к серверу:

pgsql
Copy
Edit
Server name: localhost
Authentication: SQL Server Authentication
Login: sa
Password: ModsenEnterTask!1
Trust server certificate: True
Загрузите файл ModsenEnterTask.Database.bak в контейнер:

bash
Copy
Edit
docker cp ./StartUp/ModsenEnterTask.Database.bak sqlserver:/var/opt/mssql/data
Восстановите базу данных из бэкапа в SSMS.

⚙️ Шаг 3: Сборка и запуск API
Соберите и запустите WebAPI-проект.

Убедитесь, что приложение и база данных подключены к одной Docker-сети (пример — в изображении в папке StartUp).

Перейдите по адресу:

bash
Copy
Edit
http://localhost:{порт}/swagger
🔐 Авторизация
Для доступа к защищённым методам API необходим JWT токен.

Данные для входа:
Роль	Логин	Пароль
Admin	sa	1111
User	user	2222

Получите токен через AuthController.

Добавьте токен в Swagger (кнопка Authorize) для выполнения авторизованных запросов.

📘 Контроллеры
EventsController — управление событиями.

ParticipantsController — управление участниками.

📁 Стартовые данные
База данных, восстановленная из .bak, уже содержит тестовые записи для работы.