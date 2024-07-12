# Library Management System

## Overview
This project is a Library Management System developed in C#. It utilizes Entity Framework Core for ORM (Object-Relational Mapping) and SQL Server for database management, with SQL Server Management Studio (SSMS) as the database administration tool. The system allows you to manage books and members, facilitating operations like adding, viewing, loaning, and returning books, as well as adding, viewing, and removing members.

## Features
- **Books Management**: Add, view, loan, return, and search books by genre.
- **Members Management**: Add, view, and remove members.
- **Loan Management**: Track book loans and returns.

## Prerequisites
- .NET SDK
- SQL Server
- SQL Server Management Studio (SSMS)

## Setup

### Database Setup
1. **Create Database and Tables**:
    - Open SQL Server Management Studio (SSMS).
    - Connect to your SQL Server instance.
    - Open the provided `database_setup.sql` script.
    - Execute the script to create the necessary database schema and tables.

### Configuration
1. **Connection String**:
    - Open the `appsettings.json` file in the project.
    - Update the connection string under the `ConnectionStrings` section with your SQL Server connection details.

```json
{
  "ConnectionStrings": {
    "LibraryDatabase": "YourConnectionStringHere"
  }
}
