# Sports-Administration-App
# Downloading
to clone the repository, and download the application in its current state, use git clone:
```
git clone https://github.com/Alexander-Mages/Sports-Administration-App.git
```
# Configuration
To configure the application, you must add ReCaptcha keys to appsecrets.json, and set Dotnet AppSecrets for email credentials.
```
dotnet user-secrets set "Email:Password" "PasswordHere"
dotnet user-secrets set "Email:Username" "UsernameHere"
dotnet user-secrets set "Email:Host" "EmailHostHere"
dotnet user-secrets set "Email:From" "EmailHere"
```
You also must rename appsettinggit.json to appsettings.json, as well as adding your RECaptcha Keys. If you do not have one, visit here: https://www.google.com/recaptcha/admin/create
# Prerequisites
to run the application without docker, you must install .NET Core.

# Running the Application
To run the application from the command line, use the following commands:
```
dotnet restore
dotnet run
```
The application can also be run through Visual Studio or IIS. For instructions visit 
<a href="https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/run-program?view=vs-2019#:~:text=To%20start%20the%20program%2C%20press,If%20that%20succeeds%2C%20great!">here</a>

# Application Usage
To register a new user, visit localhost:port/Account/Register
To Log in, visit localhost:port/Account/login
### User Management
To delete/edit users, visit localhost:port/Home/Index
to see details on particular users, and perform actions on such, visist localhost:port/Home/Details/useridhere
