# Progress Pacer 
This is a time tracking application (with an intuitive graphic user interface) for college students; allowing them to sign up, to sign in,  to add an unlimited number of modules (with all of their information, the application will then calculate the module's total weekly self study hours), log study hours for each of those modules, and view information regarding a module; while saving the user information and module information . It is written in C# (pronounced “C-Sharp”) and was created using Visual Studio 2022 (which would also be required to run this application) it uses the ADO.NET connection string format; and the database management application used for to createh the database is SQL Server Management Studio 2019. 


## TABLE OF CONTENTS
- [Change Log](#change-log)
- [Key Features](#key-features)
- [Video Tutorial Link](#video-tutorial-link)
- [Functionality of Progress Pacer](#functionality-of-progress-pacer)
- [Structure of Progress Pacer](#structure-of-progress-pacer)
- [Comparison of POE Part 1 Class Diagram and POE Part 2 Class Diagram](#Comparison-of-POE-Part-1-Class-Diagram-and-POE-Part-2-Class-Diagram)
- [License](#license)
- [References](#references)


## Change Log
- The user interface (UI) of the application has been updated, the user can now select which operation they would like to perform from the side bar, instead of all function being displayed to the user all at once.
- Instead of storing the module information in memory; the module information is now stored in a database. 
- The user can sign up and they can log in, due to this, only the modules that a specific user has entered will be displayed to that user.
- The user's personal details (username and password); are stored in the database (only the hash of the password is stored in the database).
- The application no longer has 4 classes, it now has 3 classes.
- Multithreading is used to delay operation by a few hundred milliseconds to ensure that the user interface does not glitch.

## Key Features
- Register (sign up) using a student email address and a password.
- Sign in using the same student email address and password.
- Add modules to be stored in the database
- Log Study Hours for a module, to be stored in the database.
- View a selected module and it's information from the database.

## Video Tutorial Link
https://youtu.be/aDrPRpTS1Ec

## Functionality of Progress Pacer
  - Signing Up: 
     + The student can enter their student email address to be used as their email address, and a password. For this example, the data that will be added is as follows:
	+ Student Email Address: ST10000000@vcconnect.edu.za 
	+ Password: Password123!
<img width="586" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/c5c4c912-a01b-4217-870e-5f03411aacc1">
<img width="585" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/f0545dba-a36c-4646-9368-61e7b5b9dd5a">

- Signing In: 
     + The student can enter their student email address and password that the user has used to sign up. For this example, the data that will be the same as when signing up:
	+ Student Email Address: ST10000000@vcconnect.edu.za 
	+ Password: Password123!
<img width="582" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/0d44f3ae-ef89-484e-8e67-1b94c75f9759">
<img width="584" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/5dedf42b-8254-48b7-999a-ec1149266e61">

- Adding a module: 
     + The student can add the module code, the module name, the number of credits that the module is worth, the number of weekly class hours, the duration of the module in weeks and the start date of the module (from a date picker). For this example, the data that will be added is as follows:
        + Module Code: PROG6212
        + Module Name: PROGRAMMING 2B
        + Number of Credits: 15
        + Class Hours per Week: 6
        + Number of weeks: 10
        + Start Date: 2023/11/03
<img width="587" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/464e49d1-0eb8-4ddf-a682-0f5b8c5caa10">
<img width="583" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/be234d2a-e513-46f2-b220-d5b00bf64b6b">

    + After the student has clicked 'Add Module', the application it will then calculate the number of weekly self study using the equation of
       
       *Weekly Self Study Hours = ((Number of credits * 10)/ Number of weeks) - Weekly class hours*

       and the data will be added into the database, thereafter, a message will be displayed to the user, showing them the module that they have just saved and it's weekly self-study hours.
<img width="589" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/9670ed90-a06d-4fba-9e70-8b2ab37c1d76">

- Logging self-study hours:
    + The student can select a module name (from a combo box) and a date (from a date picker) that they have studied on and they can enter the number of self study hours for that date. For this example, the data will be:
        + Module Name: Programming 2B
        + Study Date: 2023/11/03
        + Study Hours: 1
    + After the student has clicked 'Log Study Hours', the application will do a calculation of how many study hours will be left for that specific week.
    + The application will then save this information in the database.
    + The application will display a message to the student with the remaining hours for the week that the user has just logged hours for.

<img width="585" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/1cf93e48-2d29-409a-b011-94028db9c848">
<img width="588" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/239a47a7-c693-4443-9c1a-3ec7e0749fb5">
<img width="586" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/d7764c68-a36d-4e67-9f3a-57d12b753ad5">




 - Viewing of the module:
    + The student can select a module to view by selecting a module from the combo box, and clicking on 'View Module Stats'
    + The application will retrieve the data from the database and display the selected module's code, name, number of credits, start date, number of weeks, total number of weekly self-study hours and the remaining hours for the _current_ week.

<img width="586" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/3263dbdd-4352-471a-8cda-ff7796976d45">
<img width="583" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/abc06b4b-b9f9-442c-a17c-dce3a4d2663c">



## Error Handling
- Sign Up:
    + If the user has entered no email address (or an email address that doesn't begin with 'ST' and end with '@vcconnect.edu.za') then an error message will be displayed to the user requesting them to enter a valid email address.
    + If the user has entered no password (or a password that is less than 8 characters in length) then an error message will be displayed to the user requesting them to enter a valid password.
 
- Sign In:
    + If the user has entered no email address (or an email address that has not been registered) then an error message will be displayed to the user requesting them to either re-enter their email address and password or sign up.
    + If the user has entered no password (or the incorrect password) then an error message will be displayed to the user requesting them to either re-enter their email address and password or sign up.

- Add Module:
    + If the user has entered nothing for the field ‘Module Code’ then an error message will be displayed to the user requesting them to enter a valid module code. 
    + If the user has entered nothing for the field ‘Module Name’ then an error message will be displayed to the user requesting them to enter a valid module name.
    + If the user has entered nothing (or text other than a positive number) for the field ‘Number of Credits’ then an error message will be displayed to the user requesting them to enter a valid number of credits.
    + If the user has entered nothing (or text other than a positive number) for the field ‘Class Hours per Week’ then an error message will be displayed to the user requesting them to enter a valid number of class hours per week.
    + If the user has entered nothing (or text other than a positive number) for the field ‘Number of Weeks’ then an error message will be displayed to the user requesting them to enter a valid number of weeks.
    + If the user has not selected a date for the field ‘Start Date’ then an error message will be displayed to the user requesting them to enter a valid start date.

- Log Hours:
    + If the user has not selected a module code to log hours from; from the combobox, then an error message will be displayed to the user requesting them to select a module code.
    + If the user has not selected a date (or has selected a date that is before the start of the semester) for the field ‘Study Date’ then an error message will be displayed to the user requesting them to enter a valid start date.
    + If the user has entered nothing (or text other than a positive number) for the field ‘Study Hours’ then an error message will be displayed to the user requesting them to enter a valid number of study hours.

- View Module:
    + If the user has not selected a module code to view; from the combobox, then an error message will be displayed to the user requesting them to select a module code.



## Structure of Progress Pacer
This application has 3 classes as showin the diagram below:

1. The first class is the Connection class, which consists of the string constant called 'conn' which contains the connection string of the database.

2. The next class is called 'Methods', which is where all the data and logic is handled. It contains the following variables:
   - moduleCode (string variable )
   - moduleName (string variable)
   - numCredits (double variable)
   - numWeeks  (double variable)
   - ClassHours (double variable)
   - SelfStudy  (double variable)
   - StudyHours  (double variable)
   - date (DateTime variable)
   - remainingHours (double variable)
   - display  (string variable)
   - displayRemaining (double variable)

3. The last class is the class called 'MainWindow' which handles the logic for the user interface. It contains a string list which contains the codes of the modules that the student has entered, string variables called 'mCode' and 'mName', double variables 'numWeeks', 'classHours',  'numCredits', 'selfStudy' and a DateTime variable called 'date'. It also has a constructor for the MainWindow. 



## Comparison of POE Part 1 Class Diagram and POE Part 2 Class Diagram
### POE Part 1 Class Diagram:
![image](https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/420865fb-25bd-490d-83fe-69c3cd19a4d4)
### POE Part 2 Class Diagram:
<img width="728" alt="image" src="https://github.com/VCDBN/prog6212-part-2-ST10033475/assets/104624074/2e622670-3dec-4852-a5a3-6f7deebea3fb">

I removed the 'Module' and the 'WeeklySelfStudyHours' class and added the 'Connection' class and the database tables.


## License
MIT License - [LICENSE](LICENSE)

## References
Atiris. 2014. How do I determine if a date lies between current week dates?, 6 February 2014. [Online]. Available at: https://stackoverflow.com/questions/21598365/how-do-i-determine-if-a-date-lies-between-current-week-dates [Accessed 19 September 2023].


Govender, D. 2023. PROG-6212--PROGRAMMING-2B, 27 October 2023 2023. [Online]. Available at: https://github.com/VCDBN/PROG-6212--PROGRAMMING-2B [Accessed 27 October 2023].


Heidi, Erika. 2022. Documentation 101: creating a good README for your software project, 14 December 2022. [Online]. Available at: https://dev.to/erikaheidi/documentation-101-creating-a-good-readme-for-your-software-project-cf8 [Accessed 22 September 2023].


Satzinger, JW., Jackson, RB. and Burd, SD. 2016. System Analysis and Design in a 
Changing World. 7th ed. Boston: Cengage Learning.
