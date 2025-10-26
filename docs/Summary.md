# Podcast App Documentation
C# WPF Project presented by Carl Nicolas Mendoza and Neil Hontanosass for Lab 3.

## Lab Requirements Summary
Summary of the lab requirements for the Podcast Management System web application.

### Pages Required
- Login/Register Page
- Podcast Management Page
	- Podcaster Role
	- Create, edit, delete podcasts and episodes.
	- Upload audio/video files to S3.
- Episode Listing/Search Page
	- Listener Role
	- Browse/search episodes by topic, host, and release date.
- Episode Details Page
	- Display episode metadata.
	- Allows adding/viewing comments (from DynamoDB).
	- Supports likes and subscriptions.
	- Could include comment management page.
- Comment Management Page
	- Listener Role
	- Modify/delete own comments.
	- Could be integrated into the Episode Details page.
- Analytics Dashboard
	- Admin Role/Podcaster Role
	- View stats (e.g. top episodes by views)
	- Aggregate data from RDS + DynamoDB
- Admin Panel Page
	- Manage users.
	- Approve episodes.

## Required AWS Services
- AWS Elastic Beanstalk
	- Deploy and host the ASP.NET Core MVC web application.
- Amazon DynamoDB
	- Stores unstructured data: listener comments.
	- Supports flexible schema and fast lookups.
- Amazon S3
	- Store audio/video files for episodes.
	- Metadata in RDS points to S3 object URLs.
- AWS System Manager Parameter Store
	- Securely stores RDS credentials.
- AWS Identity and Access Management (IAM)
	- Manage access to AWS resources.
	- Define roles for the application to access S3, DynamoDB, and RDS.