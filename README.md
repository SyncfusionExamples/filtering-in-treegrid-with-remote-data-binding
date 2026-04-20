# filtering-in-treegrid-with-remote-data-binding
## Repository Description
This repository demonstrates remote data binding for ASP.NET Core TreeGrid using Url adaptor. It showcases fetching data dynamically and enabling server-side filtering with different hierarchy modes.

## Overview
Remote data binding with URL API efficiently loads large datasets without requiring all data in memory, enabling real-time updates.

## Features
- URL API: connect to URL endpoints
- Dynamic Binding: fetch from remote sources
- Server-Side Filtering: filter data
- Real-Time Updates: dynamic content

## Prerequisites
- .NET 6.0 or higher
- Visual Studio or VS Code
- ASP.NET Core SDK
- C# knowledge
- REST endpoint knowledge

## Installation
1. Clone repository
2. Navigate to project directory
3. Run `dotnet restore`
4. Execute `dotnet build`
5. Run `dotnet run`

## Usage
Implement remote data binding:
1. Create URL API endpoint returning JSON
2. Configure treegrid with API URL
3. Set filtering

## Configuration
- API Endpoint: `https://api.example.com/data` (replace with your API endpoint)
- Response Format: JSON format with standard data structure
- Filter Parameters: Pass filter criteria as query string parameters

## Documentation
For detailed information and configuration options:

https://ej2.syncfusion.com/aspnetcore/documentation/tree-grid/data-binding/remote-data#loadchildondemand
