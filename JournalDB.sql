CREATE DATABASE Journal

use Journal

CREATE TABLE Users
(UserCode int not null identity,
 UserLogin varchar(20) not null,
 UserPassword varchar(20) not null,
 UserSurname varchar(30) null,
 UserName varchar(30) null, 
 UserPatronymic varchar(30) null, 
 UserPhoneNumber varchar(17) not null,
 constraint UserLoginUniq unique (UserLogin),
 constraint UserCode_PK primary key (UserCode),
 --constraint checkName check (UserName like '%[^- а-я]%' OR UserName like '%[^- А-Я]%'),
 --constraint checkSurname check (UserSurname like '%[^-а-яА-Я]%'),
-- constraint checkPartonymic check (UserPatronymic like '%[^-а-яА-Я]%'),
 constraint checkNumber check (UserPhoneNumber like '+375([2-4][3|4|5|9])[0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'))
 INSERT INTO Users (UserLogin, UserPassword, UserSurname, UserName, UserPatronymic, UserPhoneNumber) VALUES ('tatSneg', '194358', 'Снегирева', 'Татьяна', 'Казимировна', '+375(33)784-95-43')
 INSERT INTO Users (UserLogin, UserPassword, UserSurname, UserName, UserPatronymic, UserPhoneNumber) VALUES ('IvDb', '12345', 'Дубов', 'Иван', 'Иванович', '+375(29)712-34-00')
 --drop table Users

 CREATE TABLE Violation
 (ViolationCode int not null identity,
  ViolationName varchar(80) not null,
  ViolationCost float not null,
  constraint ViolationNameUniq unique (ViolationName),
  constraint ViolationCode_PK primary key (ViolationCode))
  INSERT INTO Violation (ViolationName, ViolationCost) VALUES ('Превышение скорости на 20 км/ч', 27.00), ('Парковка на тротуаре', 23.00), ('Невключенный ближний свет днём', 51.00), ('Непристёгнутый ремень безопасности', 27.00), ('Управление ТС без документов', 742.50)
  --drop table Violation

  CREATE TABLE Town
  (TownCode int not null identity,
   TownName varchar (30) not null,
   constraint TownNameUniq unique (TownName),
   constraint TownCode_PK primary key (TownCode))
   INSERT INTO Town (TownName) values ('Минск'), ('Брест'), ('Витебск'), ('Гомель'), ('Гродно'), ('Могилев'), ('Бобруйск'), ('Борисов'), ('Мозырь'), ('Орша'), ('Полоцк'), ('Речица'), ('Слуцк'), ('Быхов'), ('Ветка'),  
   ('Горки'), ('Городок'), ('Дзержинск'), ('Добруш'), ('Дрисса'), ('Дубровно'), ('Жлобин'), ('Калинковичи'), ('Климовичи'), ('Костюковичи'), ('Кричев'), ('Лепель'), ('Мстиславль'), ('Осиповочи'), ('Петриков'), ('Рогачёв'),
   ('Сенно'), ('Старые дороги'), ('Чаусы'), ('Червень'), ('Чериков'), ('Шклов')
   --drop table Town

  CREATE TABLE Street
  (StreetCode int not null identity,
   StreetName varchar (30) not null,
   constraint StreetNameUniq unique (StreetName),
   constraint StreetCode_PK primary key (StreetCode))
   INSERT INTO Street (StreetName) VALUES ('Машерова'), ('Ленина'), ('Притыцкого'), ('Комсомольская'), ('Суворова'), ('Замковая'), ('Пушкинская')
   --drop table Street

   CREATE TABLE CarModel
   (CarModelCode int not null identity,
    CarModelName varchar(50) not null,
	constraint CarModelNameUniq unique (CarModelName),
	constraint CarModelCode_PK primary key (CarModelCode))
	INSERT INTO CarModel (CarModelName) VALUES ('BMW'), ('Mercedes'), ('KIA'), ('Opel'), ('Land Rover'), ('Suzuki'), ('Subaru'),  ('Mitsubishi'), ('Audi'), ('Toyota'), ('Bentley'), ('ВАЗ'), ('Honda'),  ('Geely'), ('Nissan'), ('Citroen'), ('Volvo'), ('Chrysler'),  ('Fiat'),  ('Siat'),  ('Skoda'), ('Volkswagen'), ('Scania'), ('Porsche'), ('Mazda'), ('Peugeot'), ('Lexus'), ('Rover'), ('Lancia'), ('Ford'), ('Lada'), ('Renault'), ('Tesla'), ('Chevrolet'), ('Dodge'), ('Infiniti')

   CREATE TABLE Color
   (ColorCode int not null identity,
   ColorName varchar (50) not null,
   constraint UniqColorName unique (ColorName),
   constraint ColorCode_PK primary key (ColorCode))
   INSERT INTO Color (ColorName) VALUES ('Красный'), ('Желный'), ('Оранжевый'), ('Зеленый'), ('Синий'), ('Розовый'), ('Кориченевый'), ('Белый'), ('Черный'), ('Серый'), ('Фиолетовый'), ('Голубой'), ('Бордовый'), ('Серебристый')     

   CREATE TABLE Violator
  (ViolatorCode int not null identity,
   ViolatorSurname varchar(30) not null,
   ViolatorName varchar(30) not null, 
   ViolatorPatronymic varchar(30) not null, 
   ViolatorPasportNumber varchar(9) not null,
   ViolatorPhoneNumber varchar(17) not null,
   ViolatorHouseNumber int not null,
   ViolatorApartmentNumber int null,
   ViolatorTownCode int not null,
   ViolatorStreetCode int not null,
   --constraint checkViolatorName check (ViolatorName like '%[^АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя]%'),
   --constraint checkViolatorSurname check (ViolatorSurname like '%[^АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя]%'),
   --constraint checkViolatorPartonymic check (ViolatorPatronymic like '%[^АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя]%'),
   constraint ViolatorTownCode_FK foreign key (ViolatorTownCode) references Town (TownCode) on update cascade on delete cascade,
   constraint ViolatorStreetCode_FK foreign key (ViolatorStreetCode) references Street (StreetCode) on update cascade on delete cascade,
   constraint ViolatorCode_PK primary key (ViolatorCode))
   INSERT INTO Violator (ViolatorPasportNumber, ViolatorName, ViolatorSurname, ViolatorPatronymic, ViolatorPhoneNumber, ViolatorTownCode, ViolatorStreetCode, ViolatorHouseNumber, ViolatorApartmentNumber) VALUES ('MP5678374', 'Дмитрий', 'Страхов', 'Андреевич', '+375(29)530-78-67', 4, 2, 63, 56),
   ('AB5182749', 'Алексей', 'Грозный', 'Владимирович', '+375(33)973-27-94', 1, 3, 12, 2), ('HB7259167', 'Александр', 'Назаров', 'Олегович', '+375(44)876-13-00', 8, 5, 41, 90), ('KB9342695', 'Даниил', 'Рогалевич', 'Сергеевич', '+375(25)903-56-12', 11, 4, 31, 25)
   --drop table Violator

 CREATE TABLE ViolatorCar
 (CarCode int not null identity,
  CarModelCode int not null,
  ColorCode int not null,
  CarStatetNumber varchar(8) not null,
  ViolatorCode int not null,
  constraint CarStatetNumberUniq unique (CarStatetNumber),
  constraint ColorCode_FK foreign key (ColorCode) references Color (ColorCode) on update cascade on delete cascade,
  constraint ViolatorCode_FK foreign key (ViolatorCode) references Violator (ViolatorCode) on update cascade on delete cascade,
  constraint CarNameCode_FK foreign key (CarModelCode) references CarModel (CarModelCode) on update cascade on delete cascade,
  constraint CarCode_PK primary key (CarCode))
  INSERT INTO ViolatorCar (CarModelCode, ColorCode, CarStatetNumber, ViolatorCode) VALUES (2, 4, '8359НА-1', 1), (7, 12, '3782ІК-6', 3), (27, 10, '9278КЛ-3', 2),  (30, 9, '3081ВС-7', 1)

  CREATE TABLE ViolationStatus
  (ViolationStatusCode int not null identity,
   ViolationStatusName varchar(12) not null,
   constraint ViolationStatusNameUniq unique (ViolationStatusName),
   constraint ViolationStatusCode_PK primary key (ViolationStatusCode))
   INSERT INTO ViolationStatus (ViolationStatusName) VALUES ('Оплачено'), ('Не оплачено')

  CREATE TABLE ViolationsJournal
  (EntryNumber int not null identity,
   EntryNumberDate date not null,
   EntryNumberTime time not null,
   UserCode int not null,
   ViolationCode int not null, 
   CarCode int not null,
   ViolationStatusCode int not null,
   constraint UserCode_FK foreign key (UserCode) references Users (UserCode) on update cascade on delete cascade,
   constraint ViolationCode_FK foreign key (ViolationCode) references Violation (ViolationCode) on update cascade on delete cascade,
   constraint CarCode_FK foreign key (CarCode) references ViolatorCar (CarCode) on update cascade on delete cascade,
   constraint ViolationStatusCode_FK foreign key (ViolationStatusCode) references ViolationStatus (ViolationStatusCode) on update cascade on delete cascade,
   constraint EntryNumbere_PK primary key (EntryNumber))
   INSERT INTO ViolationsJournal (EntryNumberDate, EntryNumberTime, UserCode, CarCode, ViolationCode, ViolationStatusCode) VALUES ('28.01.2020', '18:32', 2, 1, 1, 2), ('19.11.2019', '14:16', 2, 2, 3, 2), ('20.02.2020', '09:47', 2, 3, 4, 2), ('01.01.2020', '20:03', 1, 4, 5, 2)
   --drop table  ViolationsJournal