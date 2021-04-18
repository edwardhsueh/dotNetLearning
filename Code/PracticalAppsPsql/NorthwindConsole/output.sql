CREATE TABLE "Categories" (
    "CategoryID" bigint NOT NULL,
    "CategoryName" varchar (15) NOT NULL,
    "Description" text NULL,
    "Picture" bytea NULL,
    CONSTRAINT "PK_Categories" PRIMARY KEY ("CategoryID")
);


CREATE TABLE "Customers" (
    "CustomerID" char (5) NOT NULL,
    "CompanyName" varchar (40) NOT NULL,
    "ContactName" varchar (30) NULL,
    "ContactTitle" varchar (30) NULL,
    "Address" varchar (60) NULL,
    "City" varchar (15) NULL,
    "Region" varchar (15) NULL,
    "PostalCode" varchar (10) NULL,
    "Country" varchar (15) NULL,
    "Phone" varchar (24) NULL,
    "Fax" varchar (24) NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY ("CustomerID")
);


CREATE TABLE "Employees" (
    "EmployeeID" bigint NOT NULL,
    "LastName" varchar (20) NOT NULL,
    "FirstName" varchar (10) NOT NULL,
    "Title" varchar (30) NULL,
    "TitleOfCourtesy" varchar (25) NULL,
    "BirthDate" date NULL,
    "HireDate" date NULL,
    "Address" varchar (60) NULL,
    "City" varchar (15) NULL,
    "Region" varchar (15) NULL,
    "PostalCode" varchar (10) NULL,
    "Country" varchar (15) NULL,
    "HomePhone" varchar (24) NULL,
    "Extension" varchar (4) NULL,
    "Photo" bytea NULL,
    "Notes" text NULL,
    "ReportsTo" integer NULL,
    "PhotoPath" varchar (255) NULL,
    CONSTRAINT "PK_Employees" PRIMARY KEY ("EmployeeID")
);


CREATE TABLE "EmployeeTerritories" (
    "EmployeeID" integer NOT NULL,
    "TerritoryID" varchar NOT NULL
);


CREATE TABLE "Shippers" (
    "ShipperID" bigint NOT NULL,
    "CompanyName" varchar (40) NOT NULL,
    "Phone" varchar (24) NULL,
    CONSTRAINT "PK_Shippers" PRIMARY KEY ("ShipperID")
);


CREATE TABLE "Suppliers" (
    "SupplierID" bigint NOT NULL,
    "CompanyName" varchar (40) NOT NULL,
    "ContactName" varchar (30) NULL,
    "ContactTitle" varchar (30) NULL,
    "Address" varchar (60) NULL,
    "City" varchar (15) NULL,
    "Region" varchar (15) NULL,
    "PostalCode" varchar (10) NULL,
    "Country" varchar (15) NULL,
    "Phone" varchar (24) NULL,
    "Fax" varchar (24) NULL,
    "HomePage" text NULL,
    CONSTRAINT "PK_Suppliers" PRIMARY KEY ("SupplierID")
);


CREATE TABLE "Territories" (
    "TerritoryID" varchar NOT NULL,
    "TerritoryDescription" char NOT NULL,
    "RegionID" integer NOT NULL
);


CREATE TABLE "Orders" (
    "OrderID" bigint NOT NULL,
    "CustomerID" char (5) NULL,
    "EmployeeID" integer NULL,
    "OrderDate" date NULL,
    "RequiredDate" date NULL,
    "ShippedDate" date NULL,
    "ShipVia" integer NULL,
    "Freight" money NULL DEFAULT (0),
    "ShipName" varchar (40) NULL,
    "ShipAddress" varchar (60) NULL,
    "ShipCity" varchar (15) NULL,
    "ShipRegion" varchar (15) NULL,
    "ShipPostalCode" varchar (10) NULL,
    "ShipCountry" varchar (15) NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("OrderID"),
    CONSTRAINT "FK_Orders_Customers_CustomerID" FOREIGN KEY ("CustomerID") REFERENCES "Customers" ("CustomerID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Orders_Employees_EmployeeID" FOREIGN KEY ("EmployeeID") REFERENCES "Employees" ("EmployeeID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Orders_Shippers_ShipVia" FOREIGN KEY ("ShipVia") REFERENCES "Shippers" ("ShipperID") ON DELETE RESTRICT
);


CREATE TABLE "Products" (
    "ProductID" bigint NOT NULL,
    "ProductName" varchar (40) NOT NULL,
    "SupplierID" integer NULL,
    "CategoryID" integer NULL,
    "QuantityPerUnit" varchar (20) NULL,
    "UnitPrice" money NULL DEFAULT (0),
    "UnitsInStock" smallint NULL DEFAULT (0),
    "UnitsOnOrder" smallint NULL DEFAULT (0),
    "ReorderLevel" smallint NULL DEFAULT (0),
    "Discontinued" boolean NOT NULL DEFAULT (false),
    CONSTRAINT "PK_Products" PRIMARY KEY ("ProductID"),
    CONSTRAINT "FK_Products_Categories_CategoryID" FOREIGN KEY ("CategoryID") REFERENCES "Categories" ("CategoryID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Products_Suppliers_SupplierID" FOREIGN KEY ("SupplierID") REFERENCES "Suppliers" ("SupplierID") ON DELETE RESTRICT
);


CREATE TABLE "Order Details" (
    "OrderID" integer NOT NULL,
    "ProductID" integer NOT NULL,
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



