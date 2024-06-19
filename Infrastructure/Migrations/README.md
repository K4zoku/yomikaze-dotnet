# Guide how to create a new Database Connector

## Step 1: Create a new class library project in the Infrastructure/Migrations folder
### Run the following command in the terminal
```bash
dotnet new classlib -n Yomikaze.Infrastructure.Migrations.Provider -o Infrastructure/Migrations
```
> __Note:__ _Replace `Provider` with the appropriate name for the database you are connecting to._

## Step 2: Add the necessary NuGet packages
### Run the following command in the terminal
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Provider.EntityFrameworkCore
```
> __Note:__ _Replace `Provider.EntityFrameworkCore` with the appropriate package for the database you are connecting to._

## Step 3: Add preference to the context project
### Add the following code to the `.csproj` file of the context project
```xml
<ItemGroup>
  <ProjectReference Include="..\..\Context\Yomikaze.Infrastructure.Context.csproj" />
</ItemGroup>
```
> __Note:__ _Replace `..\..\Context\Yomikaze.Infrastructure.Context.csproj` with the appropriate path to the context project._

## Step 4: Make migrations

> __Note:__ _This section currently under development._
