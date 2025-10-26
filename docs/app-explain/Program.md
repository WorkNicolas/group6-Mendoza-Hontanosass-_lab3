# Database Config
- SQL Server with EFC
- appsettings.json connection string
- Dev exception page for DB errors

# Identity Config
- Custom user class extending Identity user
- Password req (6+ chars, uppercase, lowercase digit)
- Locked after 5 attempts
- Cookie-based authentication
- RBAC setup

# AWS Config
- AWS SDK Integration
- S3 for file storage
- DynamoDB for comments
- AWS logging provider

# Repo and Service Reg
- All 5 repo as scoped services
- Services layer interfaces
- Dependency injection configured

# Middleware Pipeline
- HTTPS redirection
- Static files
- Authentication and Authorization
- Session support
- Error handling

# DB Init
- auto migration application on startup
- seed data method that creates
	- admin, podcaster, listener roles
	- default admin user (admin@podcastapp.com / Admin@123)

# Config Files
## appsettings.json
- Production DB config
- AWS S3 Bucket config
- DynamoDB table settings
- File upload limits and allowed extensions

## appsettings.Development.json
- local dev DB
- DynamoDB local URL (localhost:8000)
- More verbose logging
- Separate S3 bucket for testing

# TAKE NOTE
**Admin**
admin@podcastapp.com
Admin@123

**DynamoDB Local Host**
localhost:8000