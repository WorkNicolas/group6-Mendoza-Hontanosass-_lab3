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
In AWS Console, type the following:
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

