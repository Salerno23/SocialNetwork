using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Data
{
    public class DataSeeder
    {
        private readonly IMongoCollection<Blocked> _blocked;
        private readonly IMongoCollection<Circle> _circle;
        private readonly IMongoCollection<Comment> _comment;
        private readonly IMongoCollection<Follows> _follows;
        private readonly IMongoCollection<Post> _post;
        private readonly IMongoCollection<User> _user;

        public DataSeeder(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _blocked    = database.GetCollection<Blocked>(settings.BlockedCollectionName);
            _circle     = database.GetCollection<Circle>(settings.CircleCollectionName);
            _comment    = database.GetCollection<Comment>(settings.CommentCollectionName);
            _follows    = database.GetCollection<Follows>(settings.FollowsCollectionName);
            _user       = database.GetCollection<User>(settings.UserCollectionName);
            _post       = database.GetCollection<Post>(settings.PostCollectionName);
        }

        public void SeedBlocked()
        {
            Blocked blockeda = new Blocked
            {
                UserId = "User1",
                BlockedUserIds = new List<string>
                {
                    "User5"
                }
            };

            Blocked blockedb = new Blocked
            {
                UserId = "User2",
                BlockedUserIds = new List<string>()
            };

            Blocked blockedc = new Blocked
            {
                UserId = "User3",
                BlockedUserIds = new List<string>()
            };

            Blocked blockedd = new Blocked
            {
                UserId = "User4",
                BlockedUserIds = new List<string>()
            };

            Blocked blockede = new Blocked
            {
                UserId = "User5",
                BlockedUserIds = new List<string>()
            };

            List<Blocked> blocks = new List<Blocked>
            {
                blockeda, blockedb, blockedc, blockedd, blockede
            };

            _blocked.InsertMany(blocks);
        }

        public void SeedCircle()
        {
            Circle circleA = new Circle
            {
                CircleId = "CircleA",
                CircleOwner = "User1",
                MemberIds = new List<string>
                {
                    "User3", "User4"
                }
            };

            Circle circleB = new Circle
            {
                CircleId = "CircleB",
                CircleOwner = "User2",
                MemberIds = new List<string>
                {
                    "User1"
                }
            };

            Circle circleC = new Circle
            {
                CircleId = "CircleC",
                CircleOwner = "User3",
                MemberIds = new List<string>
                {
                    "User5"
                }
            };

            List<Circle> circles = new List<Circle>
            {
                circleA,
                circleB,
                circleC
            };

            _circle.InsertMany(circles);
        }

        public void SeedComment()
        {
            Comment comment1 = new Comment
            {
                CommentId = "CommentId1",
                UserId = "User1",
                Text = "This is comment 1",
                Date = new DateTime(2020, 5, 3)
            };

            Comment comment2 = new Comment
            {
                CommentId = "CommentId2",
                UserId = "User2",
                Text = "This is comment 2",
                Date = new DateTime(2020, 5, 4)
            };

            Comment comment3 = new Comment
            {
                CommentId = "CommentId3",
                UserId = "User3",
                Text = "This is comment 3",
                Date = new DateTime(2020, 5, 4)
            };

            Comment comment4 = new Comment
            {
                CommentId = "CommentId4",
                UserId = "User4",
                Text = "This is comment 4",
                Date = new DateTime(2020, 5, 4)
            };

            List<Comment> comments = new List<Comment>
            {
                comment1, comment2, comment3, comment4
            };

            _comment.InsertMany(comments);
        }

        public void SeedFollows()
        {
            Follows follows1 = new Follows
            {
                UserId = "User1",
                FollowedUserIds = new List<string>
                {
                    "User2"
                }
            };

            Follows follows2 = new Follows
            {
                UserId = "User2",
                FollowedUserIds = new List<string>
                {
                    "User4", "User5"
                }
            };

            Follows follows3 = new Follows
            {
                UserId = "User3",
                FollowedUserIds = new List<string>()
            };

            Follows follows4 = new Follows
            {
                UserId = "User4",
                FollowedUserIds = new List<string>()
            };

            Follows follows5 = new Follows
            {
                UserId = "User5",
                FollowedUserIds = new List<string>()
            };

            List<Follows> followses = new List<Follows>
            {
                follows1, follows2, follows3, follows4, follows5
            };

            _follows.InsertMany(followses);
        }

        public void SeedPost()
        {
            Post post1 = new Post
            {
                PostId = "PostId1",
                Post_ = "This is post1",
                Date = new DateTime(2020, 5, 1),
                ContentType = "text",
                IsPublic = true,
                Comments = new List<string>
                {
                    "CommentId1", "CommendId2"
                },
                CircleRef = new List<string>()
            };

            Post post2 = new Post
            {
                PostId = "PostId2",
                Post_ = "This is post2",
                Date = new DateTime(2020, 4, 30),
                ContentType = "text",
                IsPublic = false,
                Comments = new List<string>(),
                CircleRef = new List<string>()
            };

            Post post3 = new Post
            {
                PostId = "PostId3",
                Post_ = "This is post3",
                Date = new DateTime(2020, 5, 2),
                ContentType = "text",
                IsPublic = false,
                Comments = new List<string>(),
                CircleRef = new List<string>()
            };

            Post post4 = new Post
            {
                PostId = "PostId4",
                Post_ = "/image/image.jpg",
                Date = new DateTime(2020, 5, 3),
                ContentType = "image",
                IsPublic = true,
                Comments = new List<string>(),
                CircleRef = new List<string>()
            };

            Post post5 = new Post
            {
                PostId = "PostId5",
                Post_ = "This is post to circle b",
                Date = new DateTime(2020, 5, 5),
                ContentType = "text",
                IsPublic = false,
                Comments = new List<string>(),
                CircleRef = new List<string>
                {
                    "CircleB"
                }
            };

            List<Post> posts = new List<Post>
            {
                post1, post2, post3, post4, post5
            };

            _post.InsertMany(posts);
        }

        public void SeedUser()
        {
            User user1 = new User
            {
                UserId = "User1",
                Name = "Rene",
                Gender = "M",
                Age = 30,
                CreatedCircles = new List<string>
                {
                    "CircleA"
                },
                Posts = new List<string>
                {
                    "PostId1"
                },
                Blocked = "BlockedId1",
                Follows = "FollowsId1"
            };

            User user2 = new User
            {
                UserId = "User2",
                Name = "Stine",
                Gender = "F",
                Age = 23,
                CreatedCircles = new List<string>
                {
                    "CircleB"
                },
                Posts = new List<string>
                {
                    "PostId2"
                },
                Blocked = "BlockedId2",
                Follows = "FollowsId2"
            };

            User user3 = new User
            {
                UserId = "User3",
                Name = "Dorte",
                Gender = "F",
                Age = 44,
                CreatedCircles = new List<string>
                {
                    "CircleC"
                },
                Posts = new List<string>
                {
                    "PostId3", "PostId4"
                },
                Blocked = "BlockedId3",
                Follows = "FollowsId3"
            };


            User user4 = new User
            {
                UserId = "User4",
                Name = "Jason",
                Gender = "M",
                Age = 17,
                CreatedCircles = new List<string>(),
                Posts = new List<string>(),
                Blocked = "BlockedId4",
                Follows = "FollowsId4"
            };

            User user5 = new User
            {
                UserId = "User5",
                Name = "Jens",
                Gender = "M",
                Age = 45,
                CreatedCircles = new List<string>(),
                Posts = new List<string>(),
                Blocked = "BlockedId5",
                Follows = "FollowsId5"
            };

            List<User> users = new List<User>
            {
                user1, user2, user3, user4, user5
            };

            _user.InsertMany(users);
        }

        public void SeedAll()
        {
            SeedBlocked();
            SeedCircle();
            SeedComment();
            SeedFollows();
            SeedPost();
            SeedUser();
        }
    }
}
