-- Active: 1723581670576@@blm4uncgjd627yhhfi53-mysql.services.clever-cloud.com@3306
CREATE TABLE Users (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Names  VARCHAR(100) NOT NULL,
    Document INT NOT NULL UNIQUE,
    Address VARCHAR(255),
    Phone  VARCHAR(50),
    Email  VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL
);

-- Crear la tabla Factura
CREATE TABLE Invoices (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    InvoiceNumber VARCHAR(50) NOT NULL UNIQUE,
    PeriodInvoicing VARCHAR(50) NOT NULL,
    InvoicedAmount INT NOT NULL,
    AmountPaid  INT NOT NULL,
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Crear la tabla Plataforma
CREATE TABLE Platforms (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name  VARCHAR(50) NOT NULL UNIQUE
);

-- Crear la tabla Transaccion
CREATE TABLE Transactions (
    Id VARCHAR(10) PRIMARY KEY,
    DateTimeTransaction DATETIME NOT NULL,
    Amount INT NOT NULL,
    State ENUM('Pendiente', 'Fallida', 'Completada') NOT NULL,
    Type  VARCHAR(50) NOT NULL,
    PlatformId INT,
    InvoiceId INT,
    FOREIGN KEY (PlatformId) REFERENCES Platforms(Id),
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id)
);

CREATE TABLE Roles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name  VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE UserRoles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    UserId INT,
    RoleId INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

INSERT INTO Roles (Name) VALUES ('Administrador'), ('Cliente');

INSERT INTO Users (Names, Document, Address, Phone, Email, Password)
VALUES ('Robinson Cortes', 123456789, '123 Main St', '555-1234', 'robinson.cortes@riwi.io', 'password');

INSERT INTO UserRoles (UserId, RoleId) VALUES (1, 1);
