-- get all posts for a user account with ID of 1
SELECT posts.media_link, posts.caption, posts.timestamp FROM accounts JOIN posts ON posts.account_id = accounts.account_id WHERE accounts.account_id = 1;

-- get all comments for a post with ID of 1
SELECT comments.account_id, comments.text from posts JOIN comments ON comments.post_id = posts.post_id WHERE posts.post_id = 1;

-- get likes for a post with ID of 6
SELECT account_id FROM liked_posts WHERE post_id = 6;

-- get favorited posts for account with ID of 1

SELECT post_id from favorited_posts WHERE account_id = 1;