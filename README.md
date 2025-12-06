# Budget App API
Backend API and database schema for the budget app.
>This repo contains the backend and database code.  
>The React frontend lives in a separate repo:

## Table of Contents
[Introduction](#budget-app-api)  
[Features](#features)  
[Authentication](#authentication-layer)

## Introduction
For years, my wife and I used an Excel spreadsheet to track our expenses. Over time, it became long, unwieldy, and difficult to maintain as we added new formulas and tracking rules.
I decided to build a custom application to make budgeting simpler, more flexible, and easier to maintain.  

This project aims to present financial data clearly while making it easy to calculate totals, trends, and other insights.
It also gives us the freedom to grow the tool in ways a static spreadsheet no longer allowed.

## Features
- Authentication Layer using Custom Auth Library 
- RESTful .NET API
- SQL Server relational database

## Authentication Layer
[Link to Authentication Repo](https://github.com/johncookmartin/AuthLibrary)

This application uses a shared authentication library that I maintain across multiple projects.
It is built on ASP.NET Core Identity with Entity Framework and JWT bearer authentication.

Authentication is token based. Short-lived JWT access tokens are issued for API access, while refresh tokens are stored in 
HTTP-only cookies and used to securely re-issue access tokens without requiring the user to reauthenticate.

The system supports both:
- Custom email and password sign-in
- Google OAuth sign-in

