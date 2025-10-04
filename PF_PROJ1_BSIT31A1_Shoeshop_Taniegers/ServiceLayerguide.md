1. Build & run locally (validation steps)
•	Restore: dotnet restore
•	Build: dotnet build
•	Run console tool: dotnet run --project ShoeShop.Repository.Console
•	Run unit tests (if any): dotnet test
2. Database / migrations
•	List migrations added (e.g., Migrations/20250924161515_InitialCreate)
•	Apply locally: dotnet ef database update --project ShoeShop.Repository --startup-project ShoeShop.Repository.Console
•	Note seed data consequences and expected tables/rows.
3. Configuration & packages
•	Note package updates (versions), config file changes (e.g., appsettings.json), and required secrets/connection strings.
4.	API / public surface changes
•	Describe any changed service interfaces, DTOs, or public methods that require consumers to update.
5.	Tests & QA checklist
•	Manual scenarios to validate (list a few)
•	Automated tests updated / added
•	Edge cases to verify
6.	Deployment notes
•	Any migrations or config to run in staging/production
•	Order of operations (migrate DB first, then deploy)
7.	Rollback plan
•	Which commits to revert or which migration to roll back and how