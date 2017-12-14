Budjit
============
[![GitHub issues](https://img.shields.io/github/issues/reecebedding/budjit.svg?style=plastic)](https://github.com/reecebedding/budjit/issues) [![Build status](https://ci.appveyor.com/api/projects/status/f592dnl3x468in6s?svg=true)](https://ci.appveyor.com/project/reecebedding/budjit) 

This is a .NET core web application, integrated with Electron.NET to provide a cross platform desktop finance budgeting solution.

---

## Features
- Importing from Santander bank exports
- Grouping individual transactions

---

## Setup
#### Requirements
- .NET Core SDK 2.0.0+ [Download Page](https://www.microsoft.com/net/download/)
- NodeJS 8.6.0+ [Download Page](https://nodejs.org/en/download/)

Clone this repo to your machine and run `dotnet build ` to install and compile project dependencies.

Run `dotnet restore src/budjit.ui` to install UI related dependencies including the dotnet cli electron tool, [Electron.Net](https://github.com/ElectronNET/Electron.NET)

---

## Usage

#### Web app 
Navigate to `src/budjit.ui/` and run `dotnet run`. You will then be able to access it at localhost:5000
#### Electron desktop app
>Note: If you are running this on Linux/Mac, you will need to run this command first 
`sudo npm install electron-packager --global`

Navigate to `src/budjit.ui/` and run `dotnet electronize start`. This will publish and execute the application using the Electron.Net ui.

Under src/budjit.core.runner/Examples/CSV there are sample .csv files you can use for importing.

---

## Publishing
Navigate to `src/budjit.ui` and run `dotnet electronize build`. The end result should be an electron app under your /bin/desktop folder.

---

## License
> You can check out the full license [here](https://github.com/reecebedding/budjit/blob/master/LICENSE)

This project is licensed under the terms of the **GNU General Public License v3.0** license.
