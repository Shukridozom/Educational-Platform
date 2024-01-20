# Educational-Platform Project

The Educational-Platform Project is a web-based application that allows authors to create courses, and allows students to browse the available courses and enroll in them, and for each enrollment the application deduct a percentage of the course price (from the author) as profit. This project was built using **ASP.NET Core** and provides a **RESTful APIs** for the frontend to interact with.

Before using the application, users have to register.
There are three types of accounts (based on their role):

- **Author Account:** Authors can create and manage courses.
- **Student Account:** Students can browse the available courses and enroll in them.
- **Admin Account:** Admins can add deposits to the students and change the profit percentage

  **Note:**

  **Admin** accounts are special accounts and can't be registered from the application, instead, they have to be directly created from the database (by either adding a new user to the users table with the role <ins>Admin</ins>, or calling the Stored Procedure **CreateAdminUser**, and the <ins>passwordHash</ins> column can be generated using this [Link](https://bcrypt.online/)).

## Features

- **Course Manager:**

  - <ins>Authors</ins> can create courses, update them, view details about the courses created by them (like the number of enrollments), and delete them (only if there are no enrollments for them).

  - <ins>Students</ins> can browse the available courses.

* **Lessons Manager:**

  - <ins>Authors</ins> can add, update, and delete lessons for their courses.

  - <ins>Students</ins> can view the lessons for courses they had enrolled in them.

* **Enrollment Manager:**

  - <ins>Students</ins> can enroll in courses, and view the courses they had enrolled in them.

* Transfer Manager:

  - <ins>Admins</ins> can add deposits for student accounts.

  - <ins>Authors</ins> can withdraw money.

* **System manager:**
  - <ins>Admins</ins> can set the percentage of profit for enrollments (this percentage can also be viewed by <ins>auhtors</ins>), and view the system balace (total deposits-total withdraws) and profit.

## Requirements

The project requires the following:

- .NET6 Runtime
- MySQL database server

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Build the solution to restore the NuGet packages.
4. Configure the connection string in **appsettings.json** file.
5. Create (or update) the database by executing **update-database** command in Package Manager Console.
6. Run the application.

## Notes

- You can test the application and review the APIs at the following URL: `/swagger/index.html`
- The project uses JWT Authentication schema.

##

I hope this helps you. Let me know if you have any other questions!
