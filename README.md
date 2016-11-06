# skills
Angular 2, Typescript, Rxjs, ASP.NET Core, Web Api tech demo.

##Getting started:

###Api Project

To run the Api you require the ASP.NET Core sdk, it works on unix/windows/mac

navigate to src/Api

run: dotnet restore; $env:ASPNETCORE_URLS="https://*:52752"; dotnet run (on windows)

You can verify api at http://localhost:52752/api/skills

###Web client

navigate to ui

npm install

ng serve (or npm node_modules\angular-cli\bin ng serve if angular-cli is not installed globally)

You can verify frontend at http://localhost:4200/


