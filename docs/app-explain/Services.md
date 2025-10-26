# S3 Service
- **UploadFileAsync** - Upload files to S3 with unique names
- **DeleteFileAsync** - Delete files from S3
- **GetPresignedUrlAsync** - Generate temporary signed URLs
- **GetFileKeyFromUrl** - Extract S3 key from URL

# DynamoDB Service
- **CreateTableIfNotExistsAsync** - Create DynamoDB table
- **TableExistsAsync** - Check if table exists

# Podcast Service
- CRUD operations for podcasts
- Thumbnail upload/delete via S3
- Approval workflow
- Creator filtering

# Episode Service
- CRUD operations for episodes
- Audio file and thumbnail handling
- Episode details with comments
- View/play count tracking
- Search functionality
- Subscription status checking

# Analytics Service
- Dashboard aggregation
- Top episodes by views
- Podcast statistics
- Comment count aggregation