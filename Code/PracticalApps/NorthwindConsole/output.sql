CREATE TABLE "Categories" (
    "CategoryID" INTEGER NOT NULL CONSTRAINT "PK_Categories" PRIMARY KEY,
    "CategoryName" nvarchar (15) NOT NULL,
    "Description" ntext NULL,
    "Picture" image NULL
);


CREATE TABLE "Customers" (
    "CustomerID" nchar (5) NOT NULL CONSTRAINT "PK_Customers" PRIMARY KEY,
    "CompanyName" nvarchar (40) NOT NULL,
    "ContactName" nvarchar (30) NULL,
    "ContactTitle" nvarchar (30) NULL,
    "Address" nvarchar (60) NULL,
    "City" nvarchar (15) NULL,
    "Region" nvarchar (15) NULL,
    "PostalCode" nvarchar (10) NULL,
    "Country" nvarchar (15) NULL,
    "Phone" nvarchar (24) NULL,
    "Fax" nvarchar (24) NULL
);


CREATE TABLE "Employees" (
    "EmployeeID" INTEGER NOT NULL CONSTRAINT "PK_Employees" PRIMARY KEY,
    "LastName" nvarchar (20) NOT NULL,
    "FirstName" nvarchar (10) NOT NULL,
    "Title" nvarchar (30) NULL,
    "TitleOfCourtesy" nvarchar (25) NULL,
    "BirthDate" datetime NULL,
    "HireDate" datetime NULL,
    "Address" nvarchar (60) NULL,
    "City" nvarchar (15) NULL,
    "Region" nvarchar (15) NULL,
    "PostalCode" nvarchar (10) NULL,
    "Country" nvarchar (15) NULL,
    "HomePhone" nvarchar (24) NULL,
    "Extension" nvarchar (4) NULL,
    "Photo" image NULL,
    "Notes" ntext NULL,
    "ReportsTo" int NULL,
    "PhotoPath" nvarchar (255) NULL
);


CREATE TABLE "EmployeeTerritories" (
    "EmployeeID" int NOT NULL,
    "TerritoryID" nvarchar NOT NULL
);


CREATE TABLE "Shippers" (
    "ShipperID" INTEGER NOT NULL CONSTRAINT "PK_Shippers" PRIMARY KEY,
    "CompanyName" nvarchar (40) NOT NULL,
    "Phone" nvarchar (24) NULL
);


CREATE TABLE "Suppliers" (
    "SupplierID" INTEGER NOT NULL CONSTRAINT "PK_Suppliers" PRIMARY KEY,
    "CompanyName" nvarchar (40) NOT NULL,
    "ContactName" nvarchar (30) NULL,
    "ContactTitle" nvarchar (30) NULL,
    "Address" nvarchar (60) NULL,
    "City" nvarchar (15) NULL,
    "Region" nvarchar (15) NULL,
    "PostalCode" nvarchar (10) NULL,
    "Country" nvarchar (15) NULL,
    "Phone" nvarchar (24) NULL,
    "Fax" nvarchar (24) NULL,
    "HomePage" ntext NULL
);


CREATE TABLE "Territories" (
    "TerritoryID" nvarchar NOT NULL,
    "TerritoryDescription" nchar NOT NULL,
    "RegionID" int NOT NULL
);


CREATE TABLE "Orders" (
    "OrderID" INTEGER NOT NULL CONSTRAINT "PK_Orders" PRIMARY KEY,
    "CustomerID" nchar (5) NULL,
    "EmployeeID" int NULL,
    "OrderDate" datetime NULL,
    "RequiredDate" datetime NULL,
    "ShippedDate" datetime NULL,
    "ShipVia" int NULL,
    "Freight" money NULL DEFAULT (0),
    "ShipName" nvarchar (40) NULL,
    "ShipAddress" nvarchar (60) NULL,
    "ShipCity" nvarchar (15) NULL,
    "ShipRegion" nvarchar (15) NULL,
    "ShipPostalCode" nvarchar (10) NULL,
    "ShipCountry" nvarchar (15) NULL,
    CONSTRAINT "FK_Orders_Customers_CustomerID" FOREIGN KEY ("CustomerID") REFERENCES "Customers" ("CustomerID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Orders_Employees_EmployeeID" FOREIGN KEY ("EmployeeID") REFERENCES "Employees" ("EmployeeID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Orders_Shippers_ShipVia" FOREIGN KEY ("ShipVia") REFERENCES "Shippers" ("ShipperID") ON DELETE RESTRICT
);


CREATE TABLE "Products" (
    "ProductID" INTEGER NOT NULL CONSTRAINT "PK_Products" PRIMARY KEY,
    "ProductName" nvarchar (40) NOT NULL,
    "SupplierID" int NULL,
    "CategoryID" int NULL,
    "QuantityPerUnit" nvarchar (20) NULL,
    "UnitPrice" money NULL DEFAULT (0),
    "UnitsInStock" smallint NULL DEFAULT (0),
    "UnitsOnOrder" smallint NULL DEFAULT (0),
    "ReorderLevel" smallint NULL DEFAULT (0),
    "Discontinued" bit NOT NULL DEFAULT (0),
    CONSTRAINT "FK_Products_Categories_CategoryID" FOREIGN KEY ("CategoryID") REFERENCES "Categories" ("CategoryID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Products_Suppliers_SupplierID" FOREIGN KEY ("SupplierID") REFERENCES "Suppliers" ("SupplierID") ON DELETE RESTRICT
);


CREATE TABLE "Order Details" (
    "OrderID" int NOT NULL,
    "ProductID" int NOT NULL,
    "UnitPrice" money NOT NULL DEFAULT (0),
    "Quantity" smallint NOT NULL DEFAULT (1),
    "Discount" real NOT NULL,
    CONSTRAINT "PK_Order Details" PRIMARY KEY ("OrderID", "ProductID"),
    CONSTRAINT "FK_Order Details_Orders_OrderID" FOREIGN KEY ("OrderID") REFERENCES "Orders" ("OrderID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Order Details_Products_ProductID" FOREIGN KEY ("ProductID") REFERENCES "Products" ("ProductID") ON DELETE RESTRICT
);


CREATE INDEX "CategoryName" ON "Categories" ("CategoryName");


CREATE INDEX "City" ON "Customers" ("City");


CREATE INDEX "CompanyNameCustomers" ON "Customers" ("CompanyName");


CREATE INDEX "PostalCodeCustomers" ON "Customers" ("PostalCode");


CREATE INDEX "Region" ON "Customers" ("Region");


CREATE INDEX "LastName" ON "Employees" ("LastName");


CREATE INDEX "PostalCodeEmployees" ON "Employees" ("PostalCode");


CREATE INDEX "OrderID" ON "Order Details" ("OrderID");


CREATE INDEX "OrdersOrder_Details" ON "Order Details" ("OrderID");


CREATE INDEX "ProductID" ON "Order Details" ("ProductID");


CREATE INDEX "ProductsOrder_Details" ON "Order Details" ("ProductID");


CREATE INDEX "CustomerID" ON "Orders" ("CustomerID");


CREATE INDEX "CustomersOrders" ON "Orders" ("CustomerID");


CREATE INDEX "EmployeeID" ON "Orders" ("EmployeeID");


CREATE INDEX "EmployeesOrders" ON "Orders" ("EmployeeID");


CREATE INDEX "OrderDate" ON "Orders" ("OrderDate");


CREATE INDEX "ShippedDate" ON "Orders" ("ShippedDate");


CREATE INDEX "ShippersOrders" ON "Orders" ("ShipVia");


CREATE INDEX "ShipPostalCode" ON "Orders" ("ShipPostalCode");


CREATE INDEX "CategoriesProducts" ON "Products" ("CategoryID");


CREATE INDEX "CategoryID" ON "Products" ("CategoryID");


CREATE INDEX "ProductName" ON "Products" ("ProductName");


CREATE INDEX "SupplierID" ON "Products" ("SupplierID");


CREATE INDEX "SuppliersProducts" ON "Products" ("SupplierID");


CREATE INDEX "CompanyNameSuppliers" ON "Suppliers" ("CompanyName");


CREATE INDEX "PostalCodeSuppliers" ON "Suppliers" ("PostalCode");



