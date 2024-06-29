# Pquyquy.Persistence.SQL

Pquyquy.Persistence.SQL is a .NET library that forms part of the Pquyquy project group. This project is responsible for managing the basic configuration of Entity Framework, such as initializing the dbContext, handling UnitOfWorks, or the DbContextFactory.

## Table of Contents

- [Installation](#installation)
- [Dependencies](#dependencies)
- [Usage](#usage)
- [License](#license)

## Installation

Currently, the `Pquyquy.Persistence.SQL` NuGet package is not available in the NuGet Gallery. You will need to download the project source code and generate the NuGet package locally. Follow these steps to get started:

   ```bash
   #1. Clone the repository or download the source code:
   git clone [URL]
   #2. Navigate to the project directory:
   cd Pquyquy.Persistence.SQL
   #3. Restore dependencies and build the project
   dotnet restore
   dotnet build
   #4. Generate the NuGet package
   dotnet pack -c Release
   #5. The NuGet package (Pquyquy.Persistence.SQL.[version].nupkg) will be generated in the bin/Release directory of the project. You can then reference this local package in your other projects as needed.
   ```

## Dependencies
- Pquyquy.Domain
- Pquyquy.Logging
- Pquyquy.Specification.EntityFrameworkCore

## Usage

Configure dependency injection

   ```csharp
   services.AddPquyquyPersistenceSQL(connectionString);
   ```

Adding models to context

   ```csharp
   services.AddTransient(typeof(IEntityTypeConfigurationProvider), typeof([ModelClass]));
   ```

## License

This project is licensed under the MIT License. 