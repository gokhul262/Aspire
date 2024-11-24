Create database CompanyDB;

CREATE TABLE EmployeeTable (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),    
	FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Salary DECIMAL(10, 2),
    Department VARCHAR(100)
);

SP_COLUMNS 'EmployeeTable';

INSERT INTO EmployeeTable (FirstName, LastName, Salary, Department )
VALUES
('jack', 'daniel',  75000.00,  'Engineering'),
('ram', 'saran', 95000.00,  'Product'),
('Mark', 'antony', 70000.00, 'Human Resources'),
('jonny', 'dep',  65000.00, 'Marketing'),
('Michael', 'jackson',  85000.00, 'Engineering');

--To list the employees in the table.
Select * From EmployeeTable;

--to retrive only employees name and salary from the table. 
SELECT FirstName, Salary
FROM EmployeeTable;

--TO list all the employees salary above 50000.
SELECT * FROM EmployeeTable Where Salary> 50000;

ALTER TABLE EmployeeTable
ADD JoinDate DATE;

UPDATE EmployeeTable
SET Location = 'New York'
WHERE FirstName = 'Alan' AND LastName = 'Mama';

--To List all employees who joined the company in the year 2020.

SELECT FirstName, LastName, Salary, Department, JoinDate
FROM EmployeeTable
WHERE YEAR(JoinDate) = 2020;

INSERT INTO EmployeeTable (FirstName, LastName)
VALUES ('Alan', 'Mama');

--To display the employee name start with 'A'.
SELECT FirstName
FROM EmployeeTable
WHERE (FirstName) LIKE 'A%';


----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------

-- to find the average salary of the employee 
SELECT AVG(Salary) AS AverageSalary
FROM EmployeeTable;

--to find the total number of the employees in the company
SELECT COUNT(*) AS TotalEmployees
FROM EmployeeTable;

--Write a query to find the highest salary in the employees table.
SELECT MAX(Salary) AS HighestSalary
FROM EmployeeTable;

--Calculate the total salary paid by the company for all employees.
SELECT SUM(Salary) AS TotalSalaryPaid
FROM EmployeeTable;


--Find the count of employees in each department.
SELECT Department, COUNT(*) AS EmployeeCount
FROM EmployeeTable
GROUP BY Department;

CREATE TABLE DepartmentTable (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName VARCHAR(100) UNIQUE
);


INSERT INTO DepartmentTable (DepartmentName)
SELECT DISTINCT Department
FROM EmployeeTable;

ALTER TABLE EmployeeTable
ADD DepartmentID INT;

UPDATE e
SET e.Department = d.DepartmentID
FROM EmployeeTable e
JOIN DepartmentTable d
ON e.Department = d.DepartmentName;

--Write a query to retrieve employee names along with their department names (using employees and departments tables).
SELECT e.FirstName, e.LastName, d.DepartmentName
FROM EmployeeTable e
INNER JOIN DepartmentTable d
    ON e.Department = d.DepartmentID;



ALTER TABLE EmployeeTable
ADD Location Varchar;


SELECT e.FirstName AS EmployeeFirstName, 
       e.LastName AS EmployeeLastName, 
       m.FirstName AS ManagerFirstName, 
       m.LastName AS ManagerLastName
FROM EmployeeTable e
INNER JOIN EmployeeTable m
    ON e.ManagerID = m.EmployeeID;



-- Create the ProjectTable
CREATE TABLE ProjectTable (
    ProjectID INT PRIMARY KEY IDENTITY(1,1),
    ProjectName VARCHAR(100)
);

-- Create the EmployeeProjectTable to map employees to projects
CREATE TABLE EmployeeProjectTable (
    EmployeeID INT,
    ProjectID INT,
    FOREIGN KEY (EmployeeID) REFERENCES EmployeeTable(EmployeeID),
    FOREIGN KEY (ProjectID) REFERENCES ProjectTable(ProjectID),
    PRIMARY KEY (EmployeeID, ProjectID)
);




INSERT INTO ProjectTable (ProjectName)
VALUES 
('Project Alpha'),
('Project Beta'),
('Project Gamma'),
('Project Delta');


-- Assign employees to projects in EmployeeProjectTable
INSERT INTO EmployeeProjectTable (EmployeeID, ProjectID)
VALUES 
(1, 1),  
(1, 2), 
(2, 3),
(3, 1),  
(3, 2),  
(4, 3), 
(5, 4), 
(6, 1),  
(7, 4), 
(8, 2), 
(9, 3),  
(10, 1),
(10, 4); 


--Find the names of employees who are working on multiple projects (using employees and projects tables).
SELECT e.FirstName, e.LastName
FROM EmployeeTable e
INNER JOIN EmployeeProjectTable ep
    ON e.EmployeeID = ep.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
HAVING COUNT(ep.ProjectID) > 1;

--Write a query to display all projects and the employees assigned to them.
SELECT p.ProjectName, e.FirstName, e.LastName
FROM ProjectTable p
LEFT JOIN EmployeeProjectTable ep ON p.ProjectID = ep.ProjectID
LEFT JOIN EmployeeTable e ON ep.EmployeeID = e.EmployeeID
ORDER BY p.ProjectName, e.LastName, e.FirstName;


;
--Retrieve the names of employees who do not belong to any department.
SELECT FirstName, LastName
FROM EmployeeTable
WHERE Department IS NULL;

--Write a query to find the employees with the second-highest salary.
SELECT FirstName, LastName, Salary
FROM EmployeeTable
WHERE Salary = (
    SELECT MAX(Salary)
    FROM EmployeeTable
    WHERE Salary < (SELECT MAX(Salary) FROM EmployeeTable)
);


--Retrieve the names of employees whose salary is above the department average salary.
SELECT e.FirstName, e.LastName, e.Salary, e.Department
FROM EmployeeTable e
WHERE e.Salary > (
    SELECT AVG(Salary)
    FROM EmployeeTable
    WHERE Department = e.Department
)
ORDER BY e.Department, e.Salary DESC;


--Find employees who earn more than the average salary of the entire company.
SELECT FirstName, LastName, Salary
FROM EmployeeTable
WHERE Salary > (
    SELECT AVG(Salary)
    FROM EmployeeTable
)
ORDER BY Salary DESC;


--Write a query to find the department with the highest number of employees.
SELECT TOP 1 Department, COUNT(*) AS EmployeeCount
FROM EmployeeTable
GROUP BY Department
ORDER BY EmployeeCount DESC;






--List all employees who work in a department located in 'New York'.
SELECT FirstName, LastName, Department
FROM EmployeeTable
WHERE Department IN (
    SELECT DepartmentID
    FROM DepartmentTable
    WHERE Location = 'New York'
);

--Write a query to find employees who work in either the 'HR' or 'Finance' department.
SELECT FirstName, LastName, Department
FROM EmployeeTable
WHERE Department IN ('HR' , 'Finance');

--Write a query to get all unique job titles across all departments.
SELECT DISTINCT  Department
FROM EmployeeTable;


--Create a new table departments_backup with the same structure as the departments table.
SELECT * INTO departments_backup
FROM DepartmentTable
WHERE 1 = 0; 


--Drop the temporary_data table from the database.
DROP TABLE temporary_data;



