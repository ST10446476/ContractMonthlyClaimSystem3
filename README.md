# Contract Monthly Claim System

A comprehensive WPF application for managing monthly claims submitted by lecturers. This system streamlines the claim submission, approval, and tracking process for educational institutions.

## 📋 Table of Contents

- [Features](#features)
- [System Requirements](#system-requirements)
- [Installation](#installation)
- [Project Structure](#project-structure)
- [Database Setup](#database-setup)
- [Usage](#usage)
- [User Roles](#user-roles)
- [Technologies Used](#technologies-used)
- [Configuration](#configuration)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)

## ✨ Features

### For Lecturers
- Submit monthly claims with supporting documents
- Automatic payment calculation based on hours and hourly rate
- View claim history and status
- Track pending, approved, and rejected claims
- Upload supporting documents (PDF, Word, Excel)

### For Coordinators
- Review and approve/reject lecturer claims
- View all pending claims
- Access claim details and supporting documents

### For HR Department
- Process approved claims
- Generate payment reports
- Manage lecturer profiles
- View audit logs and system activity

### General Features
- Secure login with password hashing (SHA-256)
- Role-based access control
- Audit logging for all activities
- Document management system
- Real-time status updates
- Modern, intuitive user interface

## 💻 System Requirements

- **Operating System:** Windows 10 or later
- **.NET Framework:** 4.7.2 or higher
- **Database:** SQL Server 2016 or later (or SQL Server Express)
- **RAM:** Minimum 4GB
- **Storage:** 100MB minimum (plus space for claim documents)

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/ContractMonthlyClaimSystem.git
cd ContractMonthlyClaimSystem
```

### 2. Restore NuGet Packages

Open the solution in Visual Studio and restore NuGet packages:
- Right-click on the solution in Solution Explorer
- Select "Restore NuGet Packages"

Required packages:
- EntityFramework 6.x
- System.Data.Entity

### 3. Configure Database Connection

Update the connection string in `App.config`:

```xml
<connectionStrings>
  <add name="ClaimSystemDB" 
       connectionString="Data Source=YOUR_SERVER;Initial Catalog=ClaimSystemDB;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 4. Initialize Database

The application will automatically create the database on first run. Alternatively, run migrations manually:

```bash
Enable-Migrations
Update-Database
```

### 5. Build and Run

- Press F5 in Visual Studio to build and run the application
- Or use: Build → Build Solution, then Debug → Start Debugging

## 📁 Project Structure

```
ContractMonthlyClaimSystem/
│
├── Data/
│   └── ClaimDbContext.cs          # Entity Framework database context
│
├── Models/
│   ├── Claim.cs                   # Claim entity model
│   ├── Lecturer.cs                # Lecturer entity model
│   ├── User.cs                    # User entity model
│   └── AuditLog.cs                # Audit log entity model
│
├── Views/
│   ├── LoginWindow.xaml           # Login screen
│   ├── LecturerView.xaml          # Lecturer dashboard
│   ├── CoordinatorView.xaml       # Coordinator dashboard
│   └── HRView.xaml                # HR dashboard
│
├── ViewModels/
│   ├── LoginViewModel.cs          # Login logic
│   ├── LecturerViewModel.cs       # Lecturer dashboard logic
│   └── ViewModelBase.cs           # Base class for ViewModels
│
├── App.xaml                       # Application entry point
├── App.config                     # Configuration file
└── README.md                      # This file
```

## 🗄️ Database Setup

### Initial Data Seeding

Create initial users and test data by running these SQL scripts after database creation:

```sql
-- Create default admin user (username: admin, password: admin123)
INSERT INTO [User] (Username, Email, PasswordHash, Role, IsActive, CreatedDate)
VALUES ('admin', 'admin@institution.edu', 
        'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 
        'HR', 1, GETDATE());

-- Create sample lecturer
INSERT INTO Lecturer (FirstName, LastName, Email, Department, DefaultHourlyRate)
VALUES ('John', 'Doe', 'john.doe@institution.edu', 'Computer Science', 350.00);

-- Link user to lecturer
INSERT INTO [User] (Username, Email, PasswordHash, Role, LecturerId, IsActive, CreatedDate)
VALUES ('john.doe', 'john.doe@institution.edu', 
        'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 
        'Lecturer', 1, 1, GETDATE());
```

### Database Schema

**Tables:**
- `User` - System users and authentication
- `Lecturer` - Lecturer information and default rates
- `Claim` - Monthly claim submissions
- `AuditLog` - System activity tracking

## 📖 Usage

### Logging In

1. Launch the application
2. Enter your username and password
3. Press Enter or click "Login"
4. You'll be redirected to your role-specific dashboard

### Default Credentials

- **Username:** admin
- **Password:** admin123
- **Role:** HR

### Submitting a Claim (Lecturer)

1. Navigate to the "Submit New Claim" section
2. Enter hours worked (1-200 hours)
3. Set hourly rate (R20-R500)
4. Click "Browse..." to attach supporting document
5. Add optional notes
6. Click "Submit Claim"
7. View confirmation with Claim ID

### Reviewing Claims (Coordinator)

1. View list of pending claims
2. Click on a claim to view details
3. Review supporting documents
4. Approve or reject with comments
5. Claims move to HR for processing

### Processing Claims (HR)

1. View all approved claims
2. Generate payment reports
3. Mark claims as paid
4. View audit logs for compliance

## 👥 User Roles

### Lecturer
- Submit claims
- View own claim history
- Upload documents
- Track claim status

### Coordinator
- Review all claims
- Approve/reject submissions
- View lecturer profiles
- Access reports

### HR
- Process approved claims
- Manage users
- Generate reports
- System administration

## 🛠️ Technologies Used

- **Framework:** WPF (.NET Framework 4.7.2+)
- **Architecture:** MVVM (Model-View-ViewModel)
- **ORM:** Entity Framework 6
- **Database:** SQL Server
- **Security:** SHA-256 password hashing
- **UI:** XAML with modern styling
- **Design Patterns:** Repository, Command, Observer

## ⚙️ Configuration

### App.config Settings

```xml
<configuration>
  <connectionStrings>
    <add name="ClaimSystemDB" 
         connectionString="YOUR_CONNECTION_STRING" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>
  
  <appSettings>
    <add key="MaxDocumentSize" value="10485760" /> <!-- 10MB -->
    <add key="DocumentsFolder" value="ClaimDocuments" />
  </appSettings>
</configuration>
```

### Customization Options

- **Maximum document size:** Modify `MaxDocumentSize` in App.config
- **Hourly rate limits:** Update validation in `LecturerViewModel.cs`
- **UI theme:** Modify styles in `App.xaml`

## 🔧 Troubleshooting

### Common Issues

**Database Connection Error**
- Verify SQL Server is running
- Check connection string in App.config
- Ensure database user has proper permissions

**Login Failed**
- Verify user exists in database
- Check password hash matches
- Ensure IsActive flag is set to true

**Document Upload Error**
- Check file size (max 10MB)
- Verify ClaimDocuments folder exists
- Ensure application has write permissions

**Build Errors**
- Restore NuGet packages
- Clean and rebuild solution
- Check .NET Framework version

### Error Logs

Application logs are stored in:
```
%APPDATA%\ContractMonthlyClaimSystem\Logs\
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards

- Follow C# naming conventions
- Use MVVM pattern for new features
- Add XML documentation for public methods
- Write unit tests for business logic
- Keep ViewModels testable and UI-independent

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For support and questions:
- **Email:** support@institution.edu
- **Documentation:** [Wiki](https://github.com/st10446476/ContractMonthlyClaimSystem/wiki)
- **Issues:** [GitHub Issues](https://github.com/st10446476/ContractMonthlyClaimSystem/issues)

## 🙏 Acknowledgments

- Icons from Material Design Icons
- UI inspiration from modern WPF applications
- Entity Framework team for excellent ORM support
