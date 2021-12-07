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

INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

--create accounts
CREATE TABLE accounts (
	account_id int IDENTITY(1, 1) NOT NULL,
	user_id int NOT NULL,
	email varchar(50),
	profile_image varchar(100) NOT NULL,

	CONSTRAINT PK_account PRIMARY KEY (account_id),
	CONSTRAINT FK_account_to_user FOREIGN KEY(user_id) REFERENCES users(user_id),
)

INSERT INTO accounts (user_id, email, profile_image) VALUES (1, 'user@gmail.com', 'https://i.stack.imgur.com/l60Hf.png');
INSERT INTO accounts (user_id, email, profile_image) VALUES (2, 'admin@gmail.com', 'https://i.stack.imgur.com/l60Hf.png');

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

INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (1, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (2, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (2, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');
INSERT INTO posts (account_id, media_link, caption, timestamp) VALUES (2, 'https://picsum.photos/200/300', 'lorem picsum', '12/03/2021');

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

INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (1, 1, '12/03/2021', 'this is a sample comment');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (1, 2, '12/03/2021', 'this is a sample comment');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (1, 3, '12/03/2021', 'this is a sample comment');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (2, 1, '12/03/2021', 'this is a sample comment');
INSERT INTO comments (account_id, post_id, timestamp, text) VALUES (2, 2, '12/03/2021', 'this is a sample comment');

--create liked_posts
CREATE TABLE liked_posts (
	account_id int NOT NULL,
	post_id int NOT NULL,

	CONSTRAINT PK_liked_post PRIMARY KEY (account_id, post_id)
)

INSERT INTO liked_posts (account_id, post_id) VALUES (1, 6);

--create liked_posts
CREATE TABLE favorited_posts (
	account_id int NOT NULL,
	post_id int NOT NULL,

	CONSTRAINT PK_favorited_post PRIMARY KEY (account_id, post_id)
)

INSERT INTO favorited_posts (account_id, post_id) VALUES (1, 8);