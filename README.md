# Announcement Management

ASP.NET Core MVC 公告管理維護功能測驗實作。

## Features

- 公告查詢
  - 依上架日期區間查詢
  - 依公告標題查詢
- 新增公告
- 編輯公告
- 表單驗證
- 公告置頂與排序
- 基本例外處理與 Logging

## Technology

- ASP.NET Core MVC
- .NET 10
- Entity Framework Core
- SQL Server

## Project Structure

Controllers/
- AnnouncementController.cs

Data/
- ApplicationDbContext.cs

Models/
- Announcement.cs

Services/
- IAnnouncementService.cs
- AnnouncementService.cs

ViewModels/
- AnnouncementEditViewModel.cs
- AnnouncementSearchViewModel.cs

Views/Announcement/
- Index.cshtml
- Create.cshtml
- Edit.cshtml
- _AnnouncementForm.cshtml

## Database Setup

The project uses Entity Framework Core (EF Core) Code First migrations.

Configure the connection string in `appsettings.json` according to your
local SQL Server environment.

Eg.

`Server=localhost;Database=AnnouncementManagementDb;Trusted_Connection=True;TrustServerCertificate=True;`

Then run the following command in Visual Studio Package Manager Console:

`Update-Database`

EF Core will create/update the database using the included migrations.

## How to Run

1. Clone or download the repository.
2. Open the solution in Visual Studio.
3. Configure the SQL Server connection string in `appsettings.json`.
4. Run `Update-Database` in Package Manager Console.
5. Build and run the project.
6. The default page will open the announcement management query screen.

## Validation

The project includes validation for:

- 公告標題必填
- 上架開始及結束時間必填
- 排序不可小於 0
- 上架結束時間不可早於開始時間

## Notes

- 公告內容使用 multiline TextArea。
- Controller 與 EF Core DbContext 之間使用 Service layer。
- Read-only query 使用 `AsNoTracking()`。

## Future Work

- Add xUnit。
