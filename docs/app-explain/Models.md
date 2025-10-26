# Entity Models (DB Table)
1. User Role
	- Enum for podcaster, listener, admin
2. User
	- Extend IdentityUser with role and relationships
3. Podcast
	- Podcast metadata with creator relationship
4. Episode
	- Episode metadata with audio file URL
5. Subscription
	- user-podcast subscription relationship
6. Comment
	- DynamoDB model for episode comments

# ViewModels
1. RegisterViewModel
	- user registration
2. LoginViewModel
	- user login
3. PodcastViewModel
	- create/edit podcasts
4. EpisodeViewModel
	- create/edit episodes
5. EpisodeDetailsViewModel
	- display episode with comments
6. CommentsViewModel
	- create/edit comments
7. AnalyticsViewModel
	- dashboard statistics
8. EpisodesSearchViewModel
	- search and filter episodes
9. UserManagementViewModel
	- admin user management
10. EpisodeApprovalViewModel
	- admin episode approval