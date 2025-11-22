# Budget App API
Backend API and database schema for the budget app.
>This repo contains the backend and database code.  
>The React frontend lives in a separate repo:

## Table of Contents
[Introduction](#budget-app-api)  
[Features](#features)  
[Overview](#overview)

## Introduction
For years, my wife and I used an Excel spreadsheet to track our expenses. Over time, it became long, unwieldy, and difficult to maintain as we added new formulas and tracking rules.
I decided to build a custom application to make budgeting simpler, more flexible, and easier to maintain.  

This project aims to present financial data clearly while making it easy to calculate totals, trends, and other insights.
It also gives us the freedom to grow the tool in ways a static spreadsheet no longer allowed.

## Features
- Google Sign-in authentication
- RESTful .NET API built using EF Core
- SQL Server relational database
- Clean Architecture with separated layers

## Overview
### High Level
This repo contains teh backend services and database for the Budget App
- The **React frontend** makes JSON API calls to this server.
- The **API** handles authentication, logic and data access.
- The **SQL database** stores users, roles, permissions, and budgets.

### Components in This Repo
#### API Layer
- ASP.NET controllers that expose REST endpoints
- Request and response models
- Routing and HTTP concerns
#### Application Layer
- Business logic for managing budgets.
- Service classes
- Validation and Domain rules
#### Infrastructure Layer
- EF Core for primary data access
- Repository implementations and configuration of database connections
#### Database
- SQL scripts defining tables and seed data
- Tables for Users, Roles and Permissions
