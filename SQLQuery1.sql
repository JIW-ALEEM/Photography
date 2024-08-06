
CREATE DATABASE Photography;
USE Photography;


CREATE TABLE Role(
RoleId INT PRIMARY KEY IDENTITY(1,1),
RoleName VARCHAR(255) NOT NULL
);
INSERT INTO ROLE (RoleName) VALUES ('Admin');
INSERT INTO ROLE (RoleName) VALUES ('User');

-- Creating Users table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(150) NOT NULL,
    UserEmail VARCHAR(100) UNIQUE NOT NULL,
    UserPassword VARCHAR(255) NOT NULL,
    UserPhone BIGINT,
	UserImg VARCHAR(255),
	UserPet VARCHAR(110) ,
	UserRoleId INT
FOREIGN KEY (UserRoleId) REFERENCES ROLE (RoleId)
);
INSERT INTO Users (UserName, UserEmail, UserImg, UserPassword, UserRoleId) VALUES ('Admin','admin@gmail.com', 'img/User/default.png','admin123',1);

-- Creating Plans table
CREATE TABLE Plans (
    PlanID INT PRIMARY KEY IDENTITY,
    PlanName VARCHAR(50) NOT NULL,
    list1 VARCHAR(MAX) ,
	list2 VARCHAR(MAX),
	list3 VARCHAR(MAX) ,
	list4 VARCHAR(MAX),
	list5 VARCHAR(MAX) ,
	list6 VARCHAR(MAX) ,
    Price bigint NOT NULL
);

-- Creating Bookings table
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    BookingDate DATE NOT NULL,
    BookingTime TIME NOT NULL,
    BookingStatus VARCHAR(20) NOT NULL DEFAULT 'Pending',
    BookingAddress VARCHAR(255) NOT NULL,
    BookingContact VARCHAR(50) NOT NULL,
    BookingPlanID INT NOT NULL FOREIGN KEY REFERENCES Plans(PlanID),
    BookingCreatedAt DATETIME DEFAULT GETDATE(),
    BookingUpdatedAt DATETIME DEFAULT GETDATE(),
    BookingUserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID)
);

-- Creating Blogs table
CREATE TABLE Blogs (
    BlogID INT PRIMARY KEY IDENTITY,
    BlogTitle VARCHAR(100) NOT NULL,
    BlogContent VARCHAR(MAX) NOT NULL,
    AuthorName VARCHAR(100) NOT NULL DEFAULT 'Admin',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Creating Comments table
CREATE TABLE Comments (
    CommentID INT PRIMARY KEY IDENTITY,
    BlogID INT NOT NULL FOREIGN KEY REFERENCES Blogs(BlogID),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    CommentText VARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Creating Likes table
CREATE TABLE Likes (
    LikeID INT PRIMARY KEY IDENTITY,
    BlogID INT NOT NULL FOREIGN KEY REFERENCES Blogs(BlogID),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Creating Notifications table
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    NotificationMessage VARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0
);

-- Creating Testimonials table
CREATE TABLE Testimonials (
    TestimonialID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Content VARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    StarRating INT CHECK (StarRating >= 1 AND StarRating <= 5)
);


-- Creating PhotoCategories table
CREATE TABLE PhotoCategories (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName VARCHAR(50) NOT NULL,
	CategoryPhoto VARCHAR(255) NOT NULL,
);

-- Creating Photos table
CREATE TABLE Photos (
    PhotoID INT PRIMARY KEY IDENTITY,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES PhotoCategories(CategoryId) ,  
    PhotoURL VARCHAR(255) NOT NULL,
	PhotoTitle VARCHAR(255) NOT NULL,
	PhotoDesc VARCHAR(255) NOT NULL,
    
);
