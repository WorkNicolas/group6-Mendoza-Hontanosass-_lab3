# AccountController
- **Register (GET/POST)** - User registration with role selection
- **Login (GET/POST)** - Login with email or username
- **Logout (POST)** - Sign out user
- **AccessDenied** - Access denied page

# PodcastController
- **Index** - List all approved podcasts (public)
- **Details** - View podcast details (public)
- **Create (GET/POST)** - Create new podcast (Podcaster/Admin)
- **Edit (GET/POST)** - Edit podcast (Podcaster/Admin)
- **Delete (GET/POST)** - Delete podcast (Podcaster/Admin)
- **MyPodcasts** - View user's own podcasts (Podcaster)

# EpisodeController
- **Index** - Browse/search episodes (public)
- **Search (POST)** - Search with filters (public)
- **Details** - View episode with comments and player (public)
- **Create (GET/POST)** - Create episode (Podcaster/Admin)
- **Edit (GET/POST)** - Edit episode (Podcaster/Admin)
- **Delete (GET/POST)** - Delete episode (Podcaster/Admin)
- **IncrementPlayCount (POST)** - Track plays (AJAX)

# CommentController
- **Create (POST)** - Add comment (authenticated users)
- **Edit (GET/POST)** - Edit own comment
- **Delete (POST)** - Delete own comment (or admin)

# SubscriptionController
- **MySubscriptions** - View user's subscriptions
- **Subscribe (POST)** - Subscribe to podcast
- **Unsubscribe (POST)** - Unsubscribe from podcast

# AnalyticsController
- **Dashboard** - View analytics (Podcaster sees own, Admin sees all)

# AdminController
- **Index** - Admin panel home
- **ManageUsers** - List all users with management options
- **LockUser / UnlockUser / DeleteUser** - User management
- **ApprovePodcasts** - List unapproved podcasts
- **ApprovePodcast** - Approve a podcast
- **ApproveEpisodes** - List unapproved episodes
- **ApproveEpisode / RejectEpisode** - Episode approval/rejection