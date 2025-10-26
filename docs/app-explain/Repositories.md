# ApplicationDBContext
- inherits from IdentityDbContext<User> for ASP.NET Identity integration
- Dbset for Podcasts, Episodes, and Subscriptions
- Configured relationships with proper delete behaviours
- Unique index on subscription (UserID, PodcastID) to prevent duplicate subscriptions

# Repository Interfaces
1. IPodcastRepository
	- Podcast CRUD + search ops
2. IEpisodeRepository
	- Episodee CRUD + views/play count tracking
3. ISubscriptionRepository
	- Subscription management
4. ICommentRepository
	- DynamoDB comment ops
5. IUserRepository
	- user queries

# Repository Implementations
1. PodcastRepository
	- EF Core implementation with loading
2. Episode Repository
	- search functionalities with filters
3. Subscription Repository
	- subscription logic
4. Comment Repository
	- DynamoDB ops using AWS SDK
5. User Repository
	- user queries with role filtering