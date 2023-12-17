# ITU-Connect4
 VUT-FIT-ITU project
 
 This document lists the third-party libraries used in the Connect4 application, including their versions, purposes, and licensing information.

## System.Data.SQLite

- **Version:** 1.0.118
- **Purpose:** This library is used for SQLite database interactions, providing an ADO.NET interface to work with SQLite databases within the application.
- **Homepage:** [System.Data.SQLite](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki)
- **License:** Public Domain (for more details, visit the link provided)
- **Installation:** This package is included in the project via NuGet and should restore automatically during the build process. If manual installation is needed, use the NuGet package manager command: `Install-Package System.Data.SQLite -Version 1.0.118`.

## Microsoft.Xaml.Behaviors.Wpf

- **Version:** 1.1.77
- **Purpose:** Provides support for creating XAML-based user interface behaviors for WPF applications.
- **Homepage:** [Microsoft.Xaml.Behaviors.Wpf on GitHub](https://github.com/microsoft/XamlBehaviorsWpf)
- **License:** MIT License
- **Installation:** Available via NuGet: `Install-Package Microsoft.Xaml.Behaviors.Wpf -Version 1.1.77`.

## EntityFramework

- **Version:** 6.4.4
- **Purpose:** Entity Framework (EF6) is an object-relational mapper (ORM) for .NET, which eliminates the need for most of the data-access code that developers usually need to write.
- **Homepage:** [EntityFramework on GitHub](https://github.com/dotnet/ef6)
- **License:** Apache License 2.0
- **Installation:** Can be installed using NuGet: `Install-Package EntityFramework -Version 6.4.4`.

## Stub.System.Data.SQLite.Core.NetFramework

- **Version:** 1.0.118
- **Purpose:** This package provides a stub of SQLite for the .NET Framework. It serves as a placeholder for the official SQLite package to ensure the correct functioning of the SQLite ADO.NET provider.
- **Homepage:** [Stub.System.Data.SQLite.Core.NetFramework on NuGet](https://www.nuget.org/packages/Stub.System.Data.SQLite.Core.NetFramework/)
- **License:** Apache License 2.0 (assumed, verify on the official site)
- **Installation:** Installed via NuGet, typically not directly added by users.

## System.Data.SQLite.Core

- **Version:** 1.0.118
- **Purpose:** This package contains the core functionality of the SQLite ADO.NET provider. It is the main component used to interact with SQLite databases in a .NET environment.
- **Homepage:** [System.Data.SQLite](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki)
- **License:** Public Domain
- **Installation:** Install via NuGet using the command: `Install-Package System.Data.SQLite.Core -Version 1.0.118`.

## System.Data.SQLite.EF6

- **Version:** 1.0.118
- **Purpose:** Provides support for using SQLite with the Entity Framework 6 ORM. It includes the necessary classes to integrate SQLite as an Entity Framework database provider.
- **Homepage:** [System.Data.SQLite.EF6 on NuGet](https://www.nuget.org/packages/System.Data.SQLite.EF6/)
- **License:** Public Domain
- **Installation:** Install via NuGet: `Install-Package System.Data.SQLite.EF6 -Version 1.0.118`.

## System.Data.SQLite.Linq

- **Version:** 1.0.118
- **Purpose:** Adds Language Integrated Query (LINQ) support for SQLite, allowing the use of LINQ queries with SQLite databases in .NET applications.
- **Homepage:** [System.Data.SQLite.Linq on NuGet](https://www.nuget.org/packages/System.Data.SQLite.Linq/)
- **License:** Public Domain
- **Installation:** Install via NuGet: `Install-Package System.Data.SQLite.Linq -Version 1.0.118`.

