# Lab 3 Requirements

Podcasts have become one of the most popular platforms for sharing information. One of my favorites is Moonshot by Peter Diamandis, available at https://www.diamandis.com/podcast. You are asked to develop a web application using ASP.NET Core to mimic a basic podcast management system. 

The application should allow podcasters to manage episodes, users (i.e., viewers) to subscribe and interact (e.g., add comments, search episode(s), likes episodes, etc.), and administrators to  oversee operations.  

## To handle data efficiently,  
- **Structured data** (e.g., episode metadata, user accounts, subscriptions) will be stored in a 
Relational Database Management System (RDBMS) (e.g., Microsoft SQL Server) for 
strong consistency, ACID compliance, and complex querying. 
- **Unstructured data** (e.g., listener comments) will be stored in Amazon DynamoDB for 
flexibility, scalability, and handling variable schemas. 
The application will be built as an ASP.NET Core MVC web app, and deployed on AWS Elastic 
Beanstalk. 

## System Architecture 
The application follows a layered architecture: 
1. **Presentation Layer**: ASP.NET MVC views for user interfaces (e.g., dashboard, episode 
upload form, etc.). 
2. **Business Logic Layer**: Controllers handling requests, services for logic (e.g., 
EpisodeService for CRUD).
3. Data Access Layer: Repositories for database interactions:  
- RDBMS repository using EF Core for structured queries 
- DynamoDB repository using AWS SDK 
- S3 for video/audio files

## Database Design
1. **Relational Database (Structured Data)**
Use Microsoft SQL Server for data with fixed schemas and relationships: 
- **Podcasts Table:**
	- PodcastID (PK, int, auto-increment) 
	- Title  
	- Description  
	- CreatorID  
	- CreatedDate  
- **Episodes Table:**  
	- EpisodeID (PK, int, auto-increment) 
	- PodcastID  
	- Title
	- ReleaseDate 
	- Duration (in minutes) 
	- PlayCount (i.e., number of viewers) 
	- AudioFileURL  // Link to S3 object 
	- Number of views 
- **Users Table** (via ASP.NET Identity):  
	- UserID (PK, Guid) 
	- Username  
	- Email  
	- Role (enum: Podcaster, Listener/ viewer, Admin) 
- **Subscriptions Table: ** 
	- SubscriptionID
	- UserID
	- PodcastID
	- SubscribedDate
2. **DynamoDB (Unstructured Data)**
- **Comments Table:**
	- EpisodeID
	- PodcastID 
	- CommentID (e.g., GUID) 
	- UserID  
	- Text  
	- Timestamp 

## Features to Implement
1. **User Authentication:** Register/login as podcaster/listener/admin. 
2. **Podcast Management** (Podcaster Role):  
	- Create/edit/delete podcasts and episodes (store metadata in RDBMS). 
	- Upload audio/video files S3 
3. **Episode Viewing/Interaction** (Listener/viewer Role):  
	- Browse/search episodes (SQL queries on RDBMS). 
	- Add comments (store in DynamoDB) 
	- Edit comments  
	- Subscribe to podcasts (RDBMS) 
4. **Analytics Dashboard** (Admin/Podcaster):  
	- View episode stats (aggregate from DynamoDB). 
	- Basic reports (e.g., top episodes by the number of views). 
5. **Admin Panel:** Manage users, approve episodes