# Smart Recipe Book

A Windows desktop application (C# WinForms, .NET 8) backed by a MySQL database.
Users register and log in, write recipes, submit them for review, and browse published recipes.
An admin approves or rejects submitted recipes.

## Features

- Register / log in (passwords are hashed with BCrypt). The **first account created becomes the admin**.
- Create, edit and delete recipes (title, ingredients, instructions, optional photo).
- Save a recipe as a draft, or submit it for admin review.
- Admin: approve or reject pending recipes; the author gets a notification.
- Browse published recipes, search by title or ingredient, mark favorites.

All data is created, read, updated and deleted through the GUI and stored in MySQL.

## Architecture (MVC)

| Layer | Folder | Responsibility |
|---|---|---|
| Model | `Models/` | `User`, `Recipe`, `Ingredient` - plain data classes |
| View | `Views/` | WinForms screens built with the Visual Studio designer (`*.Designer.cs`): `LoginForm`, `RegisterForm`, `MainForm`, `RecipeEditForm` |
| Controller | `Controllers/` | `AuthController`, `RecipeController` - validation, business rules and SQL. Views only call controllers. |
| Data access | `Data/Db.cs` | Opens the MySQL connection and builds parameterized commands |

## Database schema

Tables: `users`, `recipes`, `recipe_ingredients`, `favorites`, `notifications` (see `database/schema.sql`).

## Setup

**Requirements:** Windows, Visual Studio 2022 (".NET desktop development" workload), MySQL Server 8 + MySQL Workbench.

1. Open MySQL Workbench, connect as `root`, open `database/schema.sql` and execute it (lightning-bolt button).
   This creates the `smart_recipe_book` database, the app user `recipe` / `Recipe123`, and all tables.
2. Open `SmartRecipeBook.csproj` in Visual Studio. NuGet packages (`MySqlConnector`, `BCrypt.Net-Next`) restore automatically.
3. This project is set to MySQL port **3307**. If your MySQL uses a different port or credentials, edit the connection string in `Data/Db.cs`.
4. Press **F5** to run.

## Using the app

1. Click **Create account** and register. The first account is the admin.
2. Log in and use **Add recipe** to create a recipe (ingredients: one per line as `name, quantity`).
3. **Submit for review**, then log in as the admin, open **Pending review**, and **Approve**.
4. Check **Notifications**, and try search and favorites.

Uploaded photos are copied to an `Images` folder next to the executable and are not stored in the repository.
