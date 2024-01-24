# ProgressPacer
This is a time tracking web application for college students; allowing them to sign up, to register,  to add an unlimited number of modules (with all of their information, the  web application will then calculate the module's total weekly self study hours), log study hours for each of those modules, and view information regarding a module and viewing their weekly study hours in a graph format. It is formatted as an ASP.NET MVC (Model-View-Controller) application, written in C# (pronounced “C-Sharp”) and was created using Visual Studio 2022 (which would also be required to run this application) it uses ADO.NET and the Entity Framework Core; and the database management application used for to create the database is SQL Server Management Studio 2019. 


## TABLE OF CONTENTS
- [Change Log](#change-log)
- [Key Features](#key-features)
- [Video Tutorial Link](#video-tutorial-link)
- [Functionality of Progress Pacer](#functionality-of-progress-pacer)
- [Structure of Progress Pacer](#structure-of-progress-pacer)
- [Comparison of POE Part 1 Class Diagram POE Part 2 Class Diagram and POE Part 3 Class Diagram](#Comparison-of-POE-Part-1-Class-Diagram-POE-Part-2-Class-Diagram-and-POE-Part-3-Class-Diagram)
- [Implementation of Feedback](#implementation-of-feedback)
- [Installation and Running of the Application](#installation-and-running-of-the-application)
   + [Requirements to install the project](#requirements-to-install-the-project)
   + [How to install and run the application](#how-to-install-and-run-the-application)
- [License](#license)
- [References](#references)


## Change Log
- Progress Pacer is no longer a Windows Presentation Foundation application, it is now a ASP.NET MVC web application.
- The user interface (UI) of the application has been updated, according to the MVC archictecture, it now has a cleaner, more minimal and modern user interface.
- The application is no longer a desktop application, it can be accessed from a browser. 
- The module information is now depicted to the user in a tabular format, and can view the information of multiple modules at the same time.
- The tabs to select which operation the user would like to perform are on the top of the screen; not the left of the screen anymore.
- The user can now view a module, it's ideal weekly self study hours and how many hours the user has studied for, in a line graph format.
- The user can log out of the application.


## Key Features
- Register using an email address and a password.
- Sign in using the same email address and password.
- Add modules to be stored in the database and viewed under the Modules tab.
- Log Study Hours for a module to be stored in the database and viewed under the Study Tab.
- View a module's ideal study hours and actual study hours in a line graph format.

## Video Tutorial Link
https://youtu.be/LbkrZh2GmVY

## Functionality of Progress Pacer
  - Signing Up: The student can enter their  email address to be used as their username, and a password. For this example, the data that will be added is as follows:

- Signing In: The student can enter their email address and password that the user has used to sign up. 

- Adding a module: The student can click on the 'Module' Tab and click 'Create New' and can then add the module code, the module name, the number of credits that the module is worth, the number of weekly class hours, the duration of the module in weeks and the start date of the module. After the student has clicked 'Add Module', the application it will then calculate the number of weekly self study using the equation of

 *Weekly Self Study Hours = ((Number of credits * 10)/ Number of weeks) - Weekly class hours*

and the data will be added into the database, thereafter, a message will be displayed to the user, showing them the module that they have just saved and it's weekly self-study hours.

- Logging self-study hours: The student can click on the 'Log Study Hours' tab and click 'Create New' and thereafter select a module name (from a combo box) and a date  that they have studied on and they can enter the number of self study hours for that date. After the student has clicked 'Create', the application will do a calculation of how many study hours will be left for that specific week. The application will then save this information in the database. The user will be directed to the Index page where all the logged hours can be viewed.


 - Viewing of the module in line graph format: The student can click on 'View Graph Data' and the application will retrieve the data from the database and display a module's ideal number of weekly self study hours and the actual number of weekly self-study hours.




## Structure of Progress Pacer
This application follows the MVC architecture which consists of 3 main components: 

#### Models
There are 5 models in this web application, those being the following:
1. DataPoint.cs - This is the model that forms part of the line component of the web application.
2. ErrorViewModel.cs - This is an autogenerated model for handling errors.
3. Module.cs - This is the model that represents the 'Module' table in the database, it contains all the fields that the 'Module' table in the database contains.
4. Prog6212PoeSt10033475Context - This is an autogenerated model that handles the relations and the queries of the Entity Framework component of the web application.
5. Study.cs - This is the model that represents the 'Study' table in the database, it contains all the fields that the 'Study' table in the database contains.

#### Controllers
There are 4 controllers in this web application, those being the following:
1. HomeController.cs - This controller returns a view, that being either the 'Index' view.
2. GraphController.cs - This controller handles the logic (such as SQL select statements) for the graph and returns the 'Index' view.
3. ModuleController.cs - This controller handles all the logic for the creation and retrieval of modules from the database and returns the view 'Index' and 'Create'.
4. Study Controller.cs - This controller handles all the logic for the creation and retrieval of study hours from the database and returns the view 'Index' and 'Create'.

#### Views
The following views are retrieved for this application:
- Index = This is used by the HomeController (to display a welcome screen), the ModuleController (to display all of the modules), the StudyController (to display all of the logged study hours) and the GraphController (to display the graph).
- Create = This is used by the ModuleController (to display the user interface to create new modules) and by the StudyController (to display the user interface to log new study hours).

## Comparison of POE Part 1 Class Diagram, POE Part 2 Class Diagram and POE Part 3 Class Diagram]
### POE Part 1 Class Diagram:
![image](https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/420865fb-25bd-490d-83fe-69c3cd19a4d4)
### POE Part 2 Class Diagram:
<img width="728" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/2e622670-3dec-4852-a5a3-6f7deebea3fb">
### POE Part 3 Class Diagram:
![POE PART 3 CLASS DIAGRAM drawio](https://github.com/VCDBN/prog6212-poe-ST10033475/assets/104624074/b1b94cd7-2258-47a9-88a5-e5ac8edcc2e0)


I removed the 'MainWindow', 'AddModule',  'SelfStudyHours' and the 'DisplayModules' methods and added the method called 'CalcHrs'. I also added Models, Views and Controllers to the application. 

## Implementation of Feedback
For the previous part of this portfolio of evidence, I had obtained 100%, therefore there was no updates or changes that needed to be implemented based on feedback.

## Installation and running of the Application
### Requirements to install the project
1. Microsoft Visual Studio
2. A computer device with at least an Intel Celeron Processor, 4GB of RAM and an Internet connection.
3. A GitHub account (to clone the repository).
4. SQL Server Management Studio 19
5. The database script

### How to install and run the application:
1. Go to https://visualstudio.microsoft.com/vs/ and click 'Download'.
2. Click on 'Community 2022'.
3. Double click the 'VisualStudioSetup.exe' file.
4. Follow the prompts to download Microsoft Visual Studio.
5. Once you have Microsoft Visual Studio set up, go to https://github.com/
6. Sign up or sign into GitHub.
7. Go to https://github.com/VCDBN/prog6212-poe-ST10033475/tree/master
8. Copy the following link: https://github.com/VCDBN/prog6212-poe-ST10033475/tree/master (by pressing ‘CTRL’ and ‘C’ at the same time on your keyboard).
9. Go back to Visual Studio, and click on ‘Clone a Repository’.
10. Paste the link where you are prompted to paste link (by pressing ‘CTRL’ and ‘V’ at the same time on your keyboard).
11. Press ‘ENTER’ to clone the repository.
12. Run the database script in SSMS 19 and replace the current connection string with your own connection string
13. To run the application, click on the green triangle or Play button that is located at the middle top of the screen to start the application.

## License
MIT License - [LICENSE](LICENSE)

## References
Atiris. 2014. How do I determine if a date lies between current week dates?, 6 February 2014. [Online]. Available at: https://stackoverflow.com/questions/21598365/how-do-i-determine-if-a-date-lies-between-current-week-dates [Accessed 19 September 2023].

B.S. 2021. How can I insert the current logged in user into a database using ASP.NET Core?, 21 January 2021. [Online]. Available at: https://stackoverflow.com/questions/65824768/how-can-i-insert-the-current-logged-in-user-in-a-database-using-asp-net-core [27 November 2023].

CanvasJS. 2023. Line Charts, 2023. [Online]. Avaiable at: https://canvasjs.com/asp-net-mvc-charts/line-chart/ [Accessed 29 November 2023].

Get Current Logged In UserId of User in ASP.NET CORE Identity. 2017. YouTube Video, added by ASP.NET MVC.  [Online]. Available at: https://youtu.be/OP_KDWlAgCQ [27 November 2023].

Heidi, Erika. 2022. Documentation 101: creating a good README for your software project, 14 December 2022. [Online]. Available at: https://dev.to/erikaheidi/documentation-101-creating-a-good-readme-for-your-software-project-cf8 [Accessed 22 September 2023].

How to use Identity Pages in ASP.NET MVC Applications - Edit the register and login page. 2022. YouTube Video, added by tutorialsEU - C#. [Online]. Available at:https://youtu.be/fCS5LLOs3Qg [20 June 2022]. 

Leshaba, Isaac. 11 June 2023. ASP.NET Core MVC Web app Using Entity Framework. [Online]. Available at: https://drive.google.com/file/d/1JdVWfbfYmFYh5xd7TcG6djbUBRNo_Y3F/view [Accessed 20 June 2023].

Microsoft. 2023. System.string.TrimEnd, 2023. [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.string.trimend?view=net-8.0 [Accessed 30 November 2023].

Satzinger, JW., Jackson, RB. and Burd, SD. 2016. System Analysis and Design in a Changing World. 7th ed. Boston: Cengage Learning.
