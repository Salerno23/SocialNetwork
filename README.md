Første kørsel af SocialNetwork solution:
1. Byg solution
2. Kør projekt med Ctrl + F5
3. Åbn Postman program
4. Seed databasen ved at lave en GET request på: https://localhost:<port>/api/seed

------------------------------------------------------------------------------------------

Tilgå de forskellige collections i SocialNetwork databasen:
1. Kør projekt med Ctrl + F5
2. Åbn Postman program
3. Tilgå collection ved at lave en GET request på:
3a. https://localhost:<port>/api/blocked
3b. https://localhost:<port>/api/circle
3c. https://localhost:<port>/api/comment
3d. https://localhost:<port>/api/follows
3e. https://localhost:<port>/api/post
3f. https://localhost:<port>/api/user

------------------------------------------------------------------------------------------

Querying Feed for user eller User Wall hvor Guest har adgangsrettigheder:

Tilgå feed for user:
1. Kør projekt med Ctrl + F5
2. Åbn Postman program
3. Tilgå feed ved lave en GET request på:
https://localhost:<port>/api/feeduser/User<number>
fx:
https://localhost:<port>/api/feeduser/User1
https://localhost:<port>/api/feeduser/User2 osv..



Tilgå wall for user, som guest har adgangsrettigheder til:
1. Kør projekt med Ctrl + F5
2. Åbn Postman program
3. Tilgå wall ved at lave en GET request på:
https://localhost:<port>/api/walluserguest/User<number>/User<number>
fx:
https://localhost:<port>/api/walluserguest/User2/User1 hvor User2 er User og User1 er Guest

------------------------------------------------------------------------------------------

Post data til databasen:

Opret en post lavet af en user ved at:
1. Kør projekt med Ctrl + F5
2. Åbn Postman program
3. Lav en POST request til:
https://localhost:44315/api/createpost/User<number>
fx: 
https://localhost:44315/api/createpost/User1

hvor body består af en raw JSON med følgende indhold:

{
	"postId": "PostId<number>",
	"post_": "<post indhold>",
	"date":"<yyyy-mm-dd>T<hh:mm:ss>Z,
	"contentType": <"text" || "image">,
	"isPublic": <true || false>,
	"comments": [],
	"circleRef": []
}

fx:

{
	"postId": "PostId7",
	"post_": "this is post 7",
	"date": "2020-05-05T22:00:00Z",
	"contentType": "text",
	"isPublic": true,
	"comments": [],
	"circleRef": []
}

JSON indhold:
<post indhold>: Består af enten tekst for en post eller imaginær URL/string for beliggenhed af det uploadede image
"contentType": "text" hvis det er en tekst post, "image" hvis det er en image post
"isPublic": er false hvis post er til en circle
"circleRef": CircleId'er hvorpå denne post er postet til.

Lav en comment for PostId:
1. Kør projekt med Ctrl + F5
2. Åbn Postman program
3. Lav en POST request til:
https://localhost:<port>/api/createcomment/PostId<number>

hvor body består af en raw JSON med følgende indhold:

{
	"commentId": "CommentId<number>",
	"userId": "User<number>",
	"text": "<comment text>",
	"date": "<yyyy-mm-dd>T<hh:mm:ss>Z"
}

fx:

{
	"commentId": "CommentId5",
	"userId": "User1",
	"text": "Post comment",
	"date": "2020-05-05T22:00:00Z"
}

JSON indhold:
"userId" : User der har oprettet selve kommentaren

------------------------------------------------------------------------------------------
