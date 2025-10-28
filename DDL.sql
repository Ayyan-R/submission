
CREATE SCHEMA training;
GO


CREATE TABLE training.Users (
    UserId INT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100),
    DOB DATE NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(15),
    Address NVARCHAR(255),
    UserType NVARCHAR(20) CHECK (UserType IN ('NormalUser','BankUser')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);
GO

CREATE TABLE training.Roles (
    RoleId INT IDENTITY PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255)
);
GO


CREATE TABLE training.Permissions (
    PermissionId INT IDENTITY PRIMARY KEY,
    PermissionName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255)
);
GO

CREATE TABLE training.UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    PRIMARY KEY(UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES training.Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES training.Roles(RoleId)
);
GO


CREATE TABLE training.RolePermissions (
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    PRIMARY KEY(RoleId, PermissionId),
    FOREIGN KEY (RoleId) REFERENCES training.Roles(RoleId),
    FOREIGN KEY (PermissionId) REFERENCES training.Permissions(PermissionId)
);
GO

CREATE TABLE training.Banks (
    BankId INT IDENTITY PRIMARY KEY,
    BankName NVARCHAR(100) NOT NULL,
    HeadOfficeAddress NVARCHAR(255),
    SWIFTCode NVARCHAR(20),
    IFSCCode NVARCHAR(20) UNIQUE
);
GO


CREATE TABLE training.Branches (
    BranchId INT IDENTITY PRIMARY KEY,
    BankId INT NOT NULL,
    BranchName NVARCHAR(100),
    BranchCode NVARCHAR(20) UNIQUE,
    Address NVARCHAR(255),
    PhoneNumber NVARCHAR(15),
    FOREIGN KEY (BankId) REFERENCES training.Banks(BankId)
);
GO


CREATE TABLE training.Accounts (
    AccountId INT IDENTITY PRIMARY KEY,
    AccountNumber NVARCHAR(20) UNIQUE NOT NULL,
    UserId INT NOT NULL,
    BranchId INT NOT NULL,
    AccountType NVARCHAR(20) CHECK (AccountType IN ('Savings','Current','TermDeposit')),
    Currency NVARCHAR(10),
    Balance DECIMAL(18,2) DEFAULT 0,
    POAUserId INT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ClosedAt DATETIME NULL,
    Status NVARCHAR(20) DEFAULT 'Active',
    FOREIGN KEY (UserId) REFERENCES training.Users(UserId),
    FOREIGN KEY (BranchId) REFERENCES training.Branches(BranchId),
    FOREIGN KEY (POAUserId) REFERENCES training.Users(UserId)
);
GO

CREATE TABLE training.Transactions (
    TransactionId INT IDENTITY PRIMARY KEY,
    AccountId INT NOT NULL,
    TransactionType NVARCHAR(20),
    Amount DECIMAL(18,2),
    Currency NVARCHAR(10),
    TransactionDate DATETIME DEFAULT GETDATE(),
    PerformedByUserId INT NOT NULL,
    Remarks NVARCHAR(255),
    FOREIGN KEY (AccountId) REFERENCES training.Accounts(AccountId),
    FOREIGN KEY (PerformedByUserId) REFERENCES training.Users(UserId)
);
GO


CREATE TABLE training.AuditLogs (
    AuditId INT IDENTITY PRIMARY KEY,
    EntityName NVARCHAR(100),
    EntityId INT,
    ActionType NVARCHAR(20),
    UserId INT,
    Timestamp DATETIME DEFAULT GETDATE(),
    Details NVARCHAR(255),
    FOREIGN KEY (UserId) REFERENCES training.Users(UserId)
);
GO





