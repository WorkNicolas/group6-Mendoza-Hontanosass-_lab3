# Initial Phase
## Essential Packages
- `Controllers/` - Business logic and request handling
	- HTTP request handlers
	- Authorization attributes
	- Service/repository coordination
	- Return views or redirects
- `Models/` - Data structures and entities
	- Entity classes (matching database tables)
	- View models
	- Enums for constants
	- Data annotation for validation
- `Views/` - UI templates
	- Razor templates (.cshtml files)
	- Organized by controller name
	- Shared layouts and partials
- `Data/` - Database contexts and repositories
	- `ApplicationDbContext` for EF core
	- Repository interfaces and implementations
	- Database migrations (Created via EF Core tools)
- `wwwroot/` - Static files (CSS, JS, images)

## Supporting folders:
- `Properties/` - Configuration files (launchSettings.json)
- `Areas/` - If using areas for Admin/Podcaster/Listener separation

## Misc
- `bin/, obj/` - Compiled output (exclude from context)
- `docs/` - Documentation only

## Priority
1. Models
2. Data
3. Controllers
4. Views
5. AWS Integration