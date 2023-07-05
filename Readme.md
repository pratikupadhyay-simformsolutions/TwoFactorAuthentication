ASP.NET MVC web application with two-factor authentication using Google Authenticator


### Short description of the project
This is a two factor authentication technique integrated with MVC. I have used google authenticator to authenticate user.

First, you need to run this project, and then you can see the login page, so you need to add your username and password on the login screen.
Once details are filled out, you need to click on the login button. So in the backend, I have verified the user details, so this is a PoC, 
so I have checked value with a static value, so once it is verified, 
After clicking the login button, a QR code and the setup key for two-factor authentication were generated, which will be used later on the mobile app for code generation. 
on your mobile device, open google authenticator app; in app you have two options: scan a QR code or enter a setup key.
you can scan the QR code, so you only have one Authenticator Code, which will change after a while. Enter code which show on you app on your website.
if you have added code correctly, you will be redirected to your index page after clicking the submit button in screen. 

You need to add one key ins web.config file=> <add key="GoogleAuthKey" value="" />

### Prerequisites  
 - Visual Studio (2022)  
 - Andorid Device
	- Need to download and install google authenticator from google play store

### Dependancies
- Android Device and Install google authenticator app
	- https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&hl=en_IN&gl=US

### Project Architecture
This project follows a single-layered architecture.

Content (Contains CSS like Bootstrap, Font-awesome, etc..).
Controller (Contains Business Logic).
Models (Conatins Database Model)
Scripts (Contains Javascript files).
Views (Contains all the file which directly interact with the user).
Web.config (Contains app settings, Package Config details).

## Running the tests
Not used any tools for bulid Unit test.

## Versioning
Specify the version history
|Version        |Descrption					 |Other Description			   |
|---------------|----------------------------|-----------------------------|
|1.0.0.0		|First Version				 |Other Description            |

## License

This project is licensed under the Simform Solutions Pvt. Ltd
