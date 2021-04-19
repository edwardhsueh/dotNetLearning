SET DATESTYLE TO SQL,MDY;
COPY public."Categories"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\categories.csv'
DELIMITER ','
CSV HEADER;

COPY public."Customers"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\customers.csv'
DELIMITER ','
CSV HEADER;

COPY public."Employees"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\Employees.csv'
DELIMITER ','
CSV HEADER;

COPY public."Shippers"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\Shippers.csv'
DELIMITER ','
CSV HEADER;

COPY public."Suppliers"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\Suppliers.csv'
DELIMITER ','
CSV HEADER;

COPY public."Orders"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\orders.csv'
DELIMITER ','
CSV HEADER;

COPY public."Products"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\Products.csv'
DELIMITER ','
CSV HEADER;

COPY public."Order Details"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\Order_Details.csv'
DELIMITER ','
CSV HEADER;

COPY public."EmployeeTerritories"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\EmployeeTerritories.csv'
DELIMITER ','
CSV HEADER;

COPY public."Territories"
From 'D:\Github\dotNetLearning\Code\PracticalAppsPsql\csv\Territories.csv'
DELIMITER ','
CSV HEADER;