# Smart House - "Система" умного дома

**Задача:** Есть данные от датчиков температуры умного дома (дата-время, комната, температура). 
Необходимо по запросу вычислять среднюю температуру за день/месяц/год по дому/комнате.

Разработать программу, реализующую работу с базой данных и решающую задачу над этой базой данных.

Создать таблицу/таблицы (базу) данных, состоящую из записей соответствующего класса. 
Записи таблиц хранятся в файле. Предусмотреть возможность дополнять таблицу данных новыми записями и 
корректировать старые записи. 
При вводе записей таблицу данных производить весь возможный контроль правильности ввода. 
Для ввода/вывода записей использовать функции работы с файлами.


При программировании объектов обязательно использовать следующие классы:
  * Объект-запись БД – описание строки данных;
  * База данных – список записей БД;
  * Манипуляция с файлом, хранилищем БД
  * Слой данных;
  * Слой бизнес-логики;
  * Слой юзер-логики;
  
При разработке программы необходимо учитывать следующее:
  * БД описывается классом, хранящим список записей БД, которые умеют сериализовываться и десереализовываться;
  * Непосредственная манипуляция с файлом-хранилищем БД осуществляется специальным классом;
  * Все действия с БД производятся через класс слоя данных, который обязательно имеет четыре метода, манипулирующих одной строкой (create, read, update, delete), а также индексатор для быстрого получения данных по первичному ключу. Дополнительные методы, если необходимы, реализуются самостоятельно;
  * Решение по условию задачи осуществляются классом бизнес-логики, имеющим методы для работы с данными и решения поставленной задачи;
  * Между слоем данных и слоем бизнес-логики данные передаются через объекты, инкапсулирующие сущности данных;
  * Ни слой бизнес-логики, ни слой данных не имеют возможности интерактивно общаться с пользователем;
  * Слой юзер-логики имеет право взаимодействовать только с пользователем с одной стороны и слоем бизнес-логики – с другой;
  * Слой бизнес-логики имеет право взаимодействовать только со слоем юзер-логики с одной стороны и слоем дата-логики – с другой;
  * Слой дата-логики имеет право взаимодействовать только с физическим файлом, хранящим базу данных с одной стороны и слоем бизнес-логики – с другой;
  * Интерфейс пользователя должен предлагать следующую функциональность: добавить, удалить, отредактировать запись; просмотреть конкретную запись; просмотреть все записи; показать решение задачи;
  * Текст программы снабдить подробными комментариями.
