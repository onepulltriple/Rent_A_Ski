USE MASTER
DROP DATABASE RENT_A_SKI

CREATE DATABASE RENT_A_SKI
GO

USE RENT_A_SKI


--DROP TABLE TABLE_STATUS
CREATE TABLE TABLE_STATUS (
	id int PRIMARY KEY IDENTITY(1,1),
	Description varchar(50) NOT NULL UNIQUE
	)

INSERT INTO TABLE_STATUS (Description) VALUES 
	('Available'),
	('Rented'),
	('Maintenance required'),
	('Out for maintenance'),
	('Repair required'),
	('Out for repair'),
	('Sold'),
	('Retired')

SELECT * FROM TABLE_STATUS ORDER BY 'id'


--DROP TABLE TABLE_CATEGORY
CREATE TABLE TABLE_CATEGORY (
	id int PRIMARY KEY IDENTITY(1,1),
	Name varchar(50) NOT NULL UNIQUE
	)

SELECT * FROM TABLE_CATEGORY ORDER BY 'id'

INSERT INTO TABLE_CATEGORY (Name) VALUES
	('Ski'), 
	('Snowboard'), 
	('Ski Boots'), 
	('Snowboard Boots'),
	('Poles'), 
	('Helmet'), 
	('Goggles')

SELECT * FROM TABLE_CATEGORY ORDER BY 'id'


--DROP TABLE TABLE_ARTICLES
CREATE TABLE TABLE_ARTICLES (
	id int PRIMARY KEY IDENTITY(1,1),
	SerialNumber varchar(100) NOT NULL,
	Name varchar(100) NOT NULL,
	DateAdded date DEFAULT GETDATE(),
	PricePerDay decimal(10,2) NOT NULL,
	Counter int DEFAULT 0,
	MaintenanceInterval int DEFAULT 10,
	TABLE_STATUS_ID int NOT NULL DEFAULT 1,
	TABLE_CATEGORY_ID int NOT NULL,
	FOREIGN KEY (TABLE_STATUS_ID) REFERENCES TABLE_STATUS(id),
	FOREIGN KEY (TABLE_CATEGORY_ID) REFERENCES TABLE_CATEGORY(id)
	)

SELECT * FROM TABLE_ARTICLES ORDER BY 'id'


--DROP TABLE TABLE_EMPLOYEES
CREATE TABLE TABLE_EMPLOYEES (
	id int PRIMARY KEY IDENTITY(1,1),
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	Email varchar(100) NOT NULL UNIQUE,
	Birthday date NOT NULL,
	Address varchar(100) NOT NULL,
	City varchar(50) NOT NULL,
	ZIP varchar(15) NOT NULL,
	Username varchar(100) NOT NULL UNIQUE,
	Password varchar(255) NOT NULL
	)

SELECT * FROM TABLE_EMPLOYEES ORDER BY 'id'


--DROP TABLE TABLE_CUSTOMERS
CREATE TABLE TABLE_CUSTOMERS (
	id int PRIMARY KEY IDENTITY(1,1),
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	Email varchar(100) NOT NULL UNIQUE,
	Birthday date NOT NULL,
	Address varchar(100) NOT NULL,
	City varchar(50) NOT NULL,
	ZIP varchar(15) NOT NULL,
	TABLE_EMPLOYEES_ID int NOT NULL,
	FOREIGN KEY (TABLE_EMPLOYEES_ID) REFERENCES TABLE_EMPLOYEES(id)  -- employee who registered this customer
	)

SELECT * FROM TABLE_CUSTOMERS ORDER BY 'id'


CREATE TABLE TABLE_RENTALS (
	id int PRIMARY KEY IDENTITY(1,1),
	OutgoingDate datetime NOT NULL,
	IncomingDate datetime,
	TABLE_ARTICLES_ID int NOT NULL,
	TABLE_CUSTOMERS_ID int NOT NULL,
	TABLE_EMPLOYEES_ID int NOT NULL,
	FOREIGN KEY (TABLE_ARTICLES_ID) REFERENCES TABLE_ARTICLES(id),
	FOREIGN KEY (TABLE_CUSTOMERS_ID) REFERENCES TABLE_CUSTOMERS(id),
	FOREIGN KEY (TABLE_EMPLOYEES_ID) REFERENCES TABLE_EMPLOYEES(id)  -- employee who created this rental
	)

SELECT * FROM TABLE_RENTALS ORDER BY 'id'

SELECT TABLE_ARTICLES.*, TABLE_STATUS.Description AS status_id, TABLE_CATEGORY.Name AS category_id FROM TABLE_ARTICLES
	INNER JOIN TABLE_STATUS ON TABLE_ARTICLES.TABLE_STATUS_ID = TABLE_STATUS.id
	INNER JOIN TABLE_CATEGORY ON TABLE_ARTICLES.TABLE_CATEGORY_ID = TABLE_CATEGORY.id