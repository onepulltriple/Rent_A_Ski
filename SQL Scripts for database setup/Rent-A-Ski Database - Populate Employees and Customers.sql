USE MASTER
USE RENT_A_SKI

INSERT INTO TABLE_EMPLOYEES 
	(FirstName, LastName, Email, Birthday, Username, Password)
	VALUES
	('Chase','Helgenlechner','chase.helgenlechner@rentaski.com','1970-01-31','admin01','skiski')

--DELETE FROM TABLE_EMPLOYEES
SELECT * FROM TABLE_EMPLOYEES

INSERT INTO TABLE_CUSTOMERS 
	(FirstName, LastName, Email, Birthday, Address, City, ZIP, TABLE_EMPLOYEES_ID)
	VALUES
	('Harry','Tasker','harry.tasker@omega.gov','1947-07-30','559 L Street','Des Moines', '68995', 1),
	('Helen','Tasker','helen.tasker@omega.gov','1958-11-22','559 L Street','Des Moines', '68995', 1),
	('Dana','Tasker','danarocks@aol.com','1980-12-30','559 L Street','Des Moines', '68995', 1),
	('Juno','Skinner','suckerfortango@realartimporters.com','1967-01-02','845 Queen St #202','Honolulu', '96813', 1)

--DELETE FROM TABLE_CUSTOMERS
SELECT * FROM TABLE_CUSTOMERS

