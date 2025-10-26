# group6-Mendoza-Hontanosass-_lab3
Instructions for setting up the project.
## Setup Guide for Podcast App
### Step 1: Download aws client from AWS.
### Step 2: Type these environment variables:
```bash
set AWS_ACCESS_KEY_ID=your_access_key
set AWS_SECRET_ACCESS_KEY=your_secret_key
set AWS_DEFAULT_REGION=us-east-1
```

Alternative:
Create ~/.aws/credentials:
```bash
[default]
aws_access_key_id = your_access_key
aws_secret_access_key = your_secret_key
```

### Step 3: Create S3 Bucket
IAM User requires S3FullAccess policy and this inline policy:
```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                "s3:PutBucketPublicAccessBlock",
                "s3:GetBucketPublicAccessBlock"
            ],
            "Resource": "arn:aws:s3:::podcast-bucket-group6-mendoza-hontanosas"
        }
    ]
}
```

Then in AWS Console, execute the following:
```bash
aws s3 mb s3://your-podcast-bucket --region us-east-1
aws s3api put-public-access-block --bucket your-podcast-bucket --public-access-block-configuration "BlockPublicAcls=false,IgnorePublicAcls=false,BlockPublicPolicy=false,RestrictPublicBuckets=false"
```

### Step 4: Create DynamoDB Table
```bash
aws dynamodb create-table \
    --table-name Comments \
    --attribute-definitions \
        AttributeName=EpisodeID,AttributeType=N \
        AttributeName=CommentID,AttributeType=S \
    --key-schema \
        AttributeName=EpisodeID,KeyType=HASH \
        AttributeName=CommentID,KeyType=RANGE \
    --billing-mode PAY_PER_REQUEST \
    --region us-east-1
```

Output:
```json
    "TableDescription": {
        "AttributeDefinitions": [
            {
                "AttributeName": "CommentID",
                "AttributeType": "S"
            },
            {
                "AttributeName": "EpisodeID",
                "AttributeType": "N"
            }
        ],
        "TableName": "Comments",
        "KeySchema": [
            {
                "AttributeName": "EpisodeID",
                "KeyType": "HASH"
            },
                "AttributeName": "CommentID",
{
    "TableDescription": {
        "AttributeDefinitions": [
            {
                "AttributeName": "CommentID",
                "AttributeType": "S"
            },
            {
                "AttributeName": "EpisodeID",
                "AttributeType": "N"
            }
        ],
        "TableName": "Comments",
        "KeySchema": [
            {
                "AttributeName": "EpisodeID",
                "KeyType": "HASH"
            },
```

### Step 5: Database Migrations
Execute these commands anytime you change anything related to DB.
```bash
dotnet ef migrations add InitialCreate
```

```bash
dotnet ef database update
```

### Step 6: Run the app
You can run the app through VS2022 (hotkey: F5) or by executing this command.
```bash
dotnet run
```

Run the command through VS2022 first, because it will ask you to trust the SSL certificate autogenned by VS2022.

### Step 7: Default Admin Login
Email me for admin login. I do not want to risk putting any auth online.

### Step 8: Test the app
Here's what you can do.
- Register user
- Create podcast w/ thumbnail
- Add episode (audio)
- Test comments (register as Listener)
- Test subscription
- Login as admin (email me for admin credentials)

## AWS Elastic Bean Deployment
### Step 1: Install AWS Elastic Beanstalk CLI
pip, that means you need python
```bash
pip install awsebcli
```

### Step 2: Initialize EB in your project
```bash
eb init -p "64bit Amazon Linux 2 v2.5.0 running .NET Core" -r us-east-1 podcast-app
```

**OS:** 64-bit Amazon Linux 2 v2.5.0
**Region:** us-east-1

### Step 3: Create EB env
```bash
   eb create podcast-app-env
```

### Step 4: Configure environment variables in EB console:
- ConnectionStrings__DefaultConnection
- AWS__S3__BucketName
- AWS__DynamoDB__TableName

### Step 5: Deploy
```bash
dotnet publish -c Release
eb deploy
```