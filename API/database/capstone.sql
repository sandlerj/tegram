USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create users
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,

	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

INSERT INTO users (username, password_hash, salt, user_role) VALUES ('stephen','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('walt','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

--create accounts
CREATE TABLE accounts (
	account_id int IDENTITY(1, 1) NOT NULL,
	user_id int NOT NULL,
	email varchar(50),
	profile_image varchar(100) NOT NULL,

	CONSTRAINT PK_account PRIMARY KEY (account_id),
	CONSTRAINT FK_account_to_user FOREIGN KEY(user_id) REFERENCES users(user_id),
)

INSERT INTO accounts (user_id, email, profile_image) VALUES (1, 'bigsteve420@gmail.com', 'https://i.stack.imgur.com/l60Hf.png');
INSERT INTO accounts (user_id, email, profile_image) VALUES (2, 'mrwalter@gmail.com', 'https://i.stack.imgur.com/l60Hf.png');

--create posts
CREATE TABLE posts (
	post_id int IDENTITY(1, 1) NOT NULL,
	account_id int NOT NULL,
	media_link varchar(200) NOT NULL,
	caption varchar(100),
	timestamp datetime,

	CONSTRAINT PK_post PRIMARY KEY (post_id),
	CONSTRAINT FK_post_to_account FOREIGN KEY (account_id) REFERENCES accounts(account_id)
)

-- create posts for account stephen
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/id/101/1024/768', 'abandoned building', '10/02/2021 10:49:21:000');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/id/102/1024/768', 'summer raspberries', '08/27/2021 09:02:13:000');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/id/103/1280/1024', 'relaxing at the park', '12/04/2021 13:18:45:000');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/id/104/720/576', 'dreamcatcher', '12/13/2021 12:24:00:000');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/id/106/1280/1024', 'tree in bloom', '05/16/2021 14:51:43:000');

-- create posts for account walt
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (2, 'https://picsum.photos/id/107/1280/720', 'midwest prairie', '07/21/2021 13:24:11:000');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (2, 'https://picsum.photos/id/108/1024/768', 'lazy beach days', '05/03/2021 16:48:02:000');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (2, 'https://picsum.photos/id/109/1280/1024', 'sunrise in the woods', '09/15/2021 07:59:02:000');

--create comments
CREATE TABLE comments (
	comment_id int IDENTITY(1, 1) NOT NULL,
	account_id int NOT NULL,
	post_id int NOT NULL,
	timestamp datetime,
	text varchar(1000) NOT NULL,

	CONSTRAINT PK_comment PRIMARY KEY (comment_id),
	CONSTRAINT FK_comment_to_account FOREIGN KEY (account_id) REFERENCES accounts(account_id),
	CONSTRAINT FK_comment_to_post FOREIGN KEY (post_id) REFERENCES posts(post_Id)
)

INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (1, 6, '07/22/2021 11:41:21:000', 'Love me some grass.');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (1, 7, '06/08/2021 13:35:58:000', 'take me baaaaack');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (1, 8, '09/15/2021 10:30:12:000', 'But is there wifi out there?');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (2, 1, '10/05/2021 20:55:07:000', 'Looks like a great investment opportunity!');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (2, 3, '12/05/2021 22:02:30:000', 'got nothing on my velcros boi');

--create liked_posts
CREATE TABLE liked_posts (
	account_id int NOT NULL,
	post_id int NOT NULL,

	CONSTRAINT FK_liked_post_post_id FOREIGN KEY (post_id) REFERENCES posts(post_id),
	CONSTRAINT FK_liked_post_account_id FOREIGN KEY (account_id) REFERENCES accounts(account_id)
)

INSERT INTO liked_posts (account_id, post_id) VALUES (1, 6);

--create liked_posts
CREATE TABLE favorited_posts (
	account_id int NOT NULL,
	post_id int NOT NULL,

	CONSTRAINT FK_favorited_post_post_id FOREIGN KEY (post_id) REFERENCES posts(post_id),
	CONSTRAINT FK_favorited_post_account_id FOREIGN KEY (account_id) REFERENCES accounts(account_id)
)

INSERT INTO favorited_posts (account_id, post_id) VALUES (1, 8);