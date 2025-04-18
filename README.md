# Carvana

**Carvana** is a car rental application developed as a university project. It allows users to browse, book, and manage car rentals efficiently through a modern and responsive interface.

---

## 🚗 Features

- **Car Browsing**: Explore a wide range of available rental cars.
- **Booking Management**: Book cars and manage reservations with ease.
- **Filtering Options**: Filter cars by model, brand, price, and features.
- **Responsive Design**: Seamless experience across all device sizes.

---

## 🛠 Tech Stack

- **C# (.NET)**: Backend API development.
- **JavaScript (React)**: Frontend logic and user interface.
- **CSS**: Custom styling and responsive layout.
- **HTML**: Page structure and semantic markup.

---

## 📁 Folder Structure
```bash
Carvana/
├── Controllers/             # API services (C#)
├── Data/                    # DB and Data related code and services (C#)
├── Services/                # Backend functions and class creation (C#)
├── wwwroot/                 # Static Files for .NET web app
├── ReactApp/                # Developer version of frontend application(React, JS, CSS)
├── Standup Meetings/        # Meeting documentation
├── Program.cs               # Main .NET web app file
└── README.md
```

---

## ⚙️ Getting Started

Follow the steps below to set up and run the project locally.

### 1. Clone the Repository

```bash
git clone https://github.com/p0ndu/Carvana.git
cd Carvana
```

### 2. Install dependencies and libraries and run

### Front-End (React)
Make sure you have **Node.js** and **npm** installed
```bash
cd ReactApp
npm install
```
This installs the required dependencies. If you want to add changes, then add build version into the back-end to run, do this after changes:
```bash
cd ReactApp
npm run build
```
Copy the files in the build folder and then replace the files in wwwroot.
If you want to start development version of site, along with backend, run backend in seperate terminal, then:
```bash
npm run dev
```

### Back-End (C#)
Make sure you have **.NET 6.0 SDK*** or later installed
```bash
dotnet Restore
```
If you want to run it along with the build version of the site:
```bash
dotnet build
dotnet run
```
