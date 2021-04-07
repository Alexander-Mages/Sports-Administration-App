# Sports-Administration-App
The primary purpose of this app is for security testing purposes as a part of an apprenticeship with SecurityPs. The app has account manipulation, administration functions, separate teams, and it holds sports data for a team.
It is not a thoroughly tested app, and by nature, will most likely have vulnerabilities.
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
### appsettings.json
You must rename appsettinggit.json to appsettings.json, and add your RECaptcha Keys. If you do not have ReCaptcha Keys, visit here: https://www.google.com/recaptcha/admin/create
# Prerequisites
to run the application without docker, you must install .NET Core.

# Running the Application
### Without Docker
To run the application from the command line, use the following commands:
```
dotnet restore
dotnet run
```
The application can also be run through Visual Studio or IIS. For instructions visit 
<a href="https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/run-program?view=vs-2019#:~:text=To%20start%20the%20program%2C%20press,If%20that%20succeeds%2C%20great!">here</a>

### With Docker
To run the application using docker, use these commands inside of the project:
```
docker build -t SportsAdministrationApp -t .
docker run -d -p 5001:80 --name app SportsAdministrationApp
```
# Accessing the Application
To access the running application, visit localhost:5001 in your browser.

# Application Usage
### Account Manipulation
###### To register a new user, visit:
```
localhost:port/Account/Register
```
###### To Log in, visit:
```
localhost:port/Account/login
```
###### To access account settings, visit:
```
localhost:port/Account/AccountSettings
```
###### To reset your password, visit:
```
localhost:port/Account/ForgotPassword
```
### Administration Functionality
###### To invite a coach, visit:
```
localhost:port/Administration/InviteCoach
```
###### To view and edit users, visit:
```
localhost:port/Administration/Index
```
###### To manage roles, visit:
```
localhost:port/Administration/ListRoles
```

### Testing Credentials
Privilege  | Username     | Email                 | Password | Team
---------- | -----------  | --------------------  | -------- | ------
Admin      | Admin        | Admin@Admin.com       | Test123! | N/A
Athlete    | TestAthlete1 | Athlete1@Athlete.com  | Test123! | Swim
Coach      | TestCoach2   | Coac1h@Coach.com      | Test123! | Swim
Athlete    | TestAthlete1 | Athlete2@Athlete.com  | Test123! | Swim
Coach      | TestCoach2   | Coach2@Coach.com      | Test123! | Swim