# Student Exam Management System

A web application for managing students, professors, subjects, exam periods, exams, exam registrations, and exam records.
The system enables student exam registration, professor grading, and statistical analysis of exam results.

This project was developed as a university assignment using Vue 3 and ASP.NET Core Web API with Entity Framework Core and SQL Server.

---

## Features

- Student management (view, edit, delete)
- Professor management
- Subject management with assigned professors
- Exam periods (opening and closing of exam sessions)
- Exam creation per subject and exam period
- Student exam registration
- Exam records (zapisnik) with grades and points
- Exam statistics and grade distribution
- Calculation of passed exams and average grades
- RESTful API backend
- Clean and reactive frontend using Vue 3 Composition API

---

## Project Structure

 /frontend → Vue 3 application /backend → ASP.NET Core Web API 

---

## Technologies Used

### Frontend
- Vue 3 (Composition API)
- Vite
- Axios
- JavaScript
- HTML / CSS

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- C#
- LINQ

---

## Database

The application uses a SQL Server database named STUDENTSKA with the following main tables:

- student
- profesor
- predmet
- ispitni_rok
- ispit
- student_predmet
- zapisnik

The database schema follows relational design principles and avoids data redundancy.

---

## Installation and Setup

### Backend

1. Open the backend project in Visual Studio
2. Make sure SQL Server is running and the STUDENTSKA database exists
3. Update the connection string in appsettings.json if necessary
4. Run the project

The backend runs on:

 https://localhost:7043 

---

### Frontend

bash cd frontend 
npm install 
npm run dev 

The frontend runs on:

 http://localhost:5173 

---

## API Endpoints (Examples)

 GET /api/Students GET /api/Students/{id} POST /api/Students PUT /api/Students/{id} DELETE /api/Students/{id}  GET /api/Profesors GET /api/Predmets GET /api/IspitniRoks GET /api/Ispits GET /api/Zapisniks 

---

## Notes

- The project is intended for educational and academic use

---

## Author

Damjan Jovanović

---

## License

This project is intended for educational use only.