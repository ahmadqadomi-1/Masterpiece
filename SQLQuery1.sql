USE Masterpiece;
GO
------------------------------------------------------Break------------------------------------------------------!
-- 1. Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),  
    UserName NVARCHAR(100),
    Email VARCHAR(100) UNIQUE,
    Password NVARCHAR(255),
    PasswordHash VARBINARY(MAX),
    PasswordSalt VARBINARY(MAX),
    PhoneNumber NVARCHAR(15),
    ProfileImage NVARCHAR(MAX),
    Points INT DEFAULT 0,
    UserType NVARCHAR(50), -- Admin, Customer
);
------------------------------------------------------Break------------------------------------------------------!
-- 2. UserAddresses Table
CREATE TABLE UserAddresses (
    AddressID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,  -- Foreign key to Users table
    Street NVARCHAR(255),  -- Street name
    City NVARCHAR(100),    -- City name
    HomeNumberCode NVARCHAR(50),  -- Home number or code
    CONSTRAINT FK_UserAddresses_Users FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);
------------------------------------------------------Break------------------------------------------------------!
-- 3. Categories Table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100),
    CategoryRate DECIMAL(2,1) DEFAULT 0 CHECK (CategoryRate BETWEEN 0 AND 5),
    CategoryImage NVARCHAR(MAX)
);
INSERT INTO Categories (CategoryName, CategoryRate, CategoryImage)
VALUES 
( N'سيراميك حجاري' , 4.7, 'img/Stone ceramics3.jpg'),
( N' جدران سيراميك' , 5, 'img/Wall Tiles14.jpg'),
( N'مغاسل سيراميك' , 4.2, 'img/Washbasin5.jpg'),
( N'سيراميك أرضيات' , 4, 'img/Floor tiles3.jpg'),
( N'مرايا' , 3.9, 'img/Mirrors12.jpg');
Select * from Categories;
------------------------------------------------------Break------------------------------------------------------!
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100),
    ProductDescription NVARCHAR(MAX),
    ProductDescriptionList1 NVARCHAR(MAX),
    ProductDescriptionList2 NVARCHAR(MAX),
    ProductDescriptionList3 NVARCHAR(MAX),
    Price DECIMAL(10, 2),
    Stock INT,
    CategoryID INT,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    ProductImage NVARCHAR(MAX),
    ProductRate DECIMAL(2,1) DEFAULT 0 CHECK (ProductRate BETWEEN 0 AND 5)
);
INSERT INTO Products (ProductName, ProductDescription, ProductDescriptionList1, ProductDescriptionList2, ProductDescriptionList3, Price, Stock, CategoryID, ProductImage)
VALUES 
    (N'سيراميك جدران حجاري', 
     N'تم تصميم سيراميك الجدران الحجاري بأحدث الألوان العصرية، ليضفي لمسة جمالية على كل زاوية من زوايا منزلك، حيث يتميز بجودة عالية تناسب جميع الأذواق.', 
     N'حجاري بألوان دافئة', 
     N'سهل التركيب', 
     N'مثالي للمساحات الداخلية', 
     7.50, 
     500, 
     1, 
     'img/Stone ceramics3.jpg'),

    (N'سيراميك حجاري', 
     N'يتميز سيراميك الجدران الحجاري بأحدث الألوان المتاحة في المملكة الأردنية، وهو مثالي لإضفاء لمسة أنيقة وعصرية على منزلك.', 
     N'حجاري بألوان متألقة', 
     N'يضيف لمسة جمالية', 
     N'متوفر بأحجام متنوعة', 
     10.00, 
     500, 
     1, 
     'img/Stone ceramics1.jpg'),

    (N'جدران حجاري', 
     N'سيراميك الجدران الحجاري بأحدث الألوان المتوفرة في أربد، مما يجعله خيارًا مثاليًا للمساحات العامة، حيث يجمع بين الجمال والمتانة.', 
     N'أنيق ومتين', 
     N'تصميم عصري', 
     N'مناسب للأماكن العامة', 
     10.00, 
     500, 
     1, 
     'img/Stone ceramics2.jpg');

INSERT INTO Products (ProductName, ProductDescription, ProductDescriptionList1, ProductDescriptionList2, ProductDescriptionList3, Price, Stock, CategoryID, ProductImage)
VALUES 
    (N'سيراميك جدران', 
     N'تم تصميم سيراميك الجدران بأحدث الألوان المتاحة، مما يمنح منزلك مظهراً عصريًا وجذابًا. يتميز بتنوع الأشكال والألوان التي تناسب جميع الأذواق.',
     N'ألوان متألقة وجذابة', 
     N'سهلة التنظيف والصيانة', 
     N'مناسبة لكل أنواع الجدران', 
     7.50, 
     500, 
     2, 
     'img/Wall Tiles13.jpg'),

    (N'سيراميك جدران حمامات', 
     N'يتميز سيراميك الجدران الخاص بالحمامات بأحدث الألوان المتوفرة في المملكة الأردنية، وهو مصمم خصيصًا لمقاومة الرطوبة والعوامل الجوية، مما يجعله الخيار الأمثل للحمامات الحديثة.',
     N'تصميم عصري ومبتكر', 
     N'مقاومة للرطوبة', 
     N'مثالية للحمامات الحديثة', 
     7.25, 
     500, 
     2, 
     'img/Wall Tiles9.jpg'),

    (N'جدران رسمات', 
     N'سيراميك الجدران المزخرف برسمات فريدة يجمع بين الأناقة والوظائف، مما يضيف لمسة فنية جذابة إلى أي مساحة. إنه الخيار المثالي لمن يحبون التميز والابتكار.',
     N'تصميمات فريدة وجذابة', 
     N'تعكس ذوقك الشخصي', 
     N'تضيف لمسة فنية للمكان', 
     85.00, 
     500, 
     2, 
     'img/Wall Tiles1.jpg');

INSERT INTO Products (ProductName, ProductDescription, ProductDescriptionList1, ProductDescriptionList2, ProductDescriptionList3, Price, Stock, CategoryID, ProductImage)
VALUES 
    (N'مغاسل سيراميك', 
     N'تتميز مغاسل السيراميك بتصميمها العصري وألوانها الجذابة، مما يضيف لمسة من الأناقة إلى حمامك. إنها مصنوعة من مواد عالية الجودة لضمان المتانة.', 
     N'تصميم أنيق وعصري', 
     N'سهلة التركيب والتنظيف', 
     N'مناسبة لكل أنواع الحمامات', 
     70.00, 
     500, 
     3, 
     'img/Washbasin5.jpg'),

    (N'مغاسل سيراميك حديثة', 
     N'تقدم مغاسل السيراميك الحديثة بألوان عصرية ومتنوعة، مثالية لمختلف المساحات في المملكة الأردنية، حيث تجمع بين الجمال والوظيفة.', 
     N'ألوان متألقة وجذابة', 
     N'تساعد على توفير المساحة', 
     N'تتميز بالمتانة والقوة', 
     65.00, 
     500, 
     3, 
     'img/Washbasin1.jpg'),

    (N'قطعة واحدة', 
     N'تتميز المغاسل السيراميك من نوع القطعة الواحدة بأحدث الألوان المتاحة في أربد، حيث توفر تصميمًا مدمجًا يلبي احتياجاتك اليومية.', 
     N'تصميم مدمج', 
     N'سهل الاستخدام والتنظيف', 
     N'يتناسب مع جميع الديكورات', 
     75.00, 
     500, 
     3, 
     'img/Washbasin4.jpg');

INSERT INTO Products (ProductName, ProductDescription, ProductDescriptionList1, ProductDescriptionList2, ProductDescriptionList3, Price, Stock, CategoryID, ProductImage)
VALUES 
    (N'سيراميك أرضيات سجاد', 
     N'سجاد سيراميك بأحدث الألوان العصرية، يوفر مظهرًا جذابًا ومريحًا للمساحات الداخلية، مما يجعل منزلك أكثر أناقة.', 
     N'ألوان دافئة وجذابة', 
     N'سهل التركيب والتنظيف', 
     N'مناسب للمساحات الداخلية', 
     65.00, 
     500, 
     4, 
     'img/Floor tiles1.jpg'),

    (N'سيراميك أرضيات باركيه', 
     N'باركيه سيراميك بأحدث الألوان المتاحة في المملكة الأردنية، يجمع بين الجمال والمتانة، مما يجعله خيارًا مثاليًا للأرضيات.', 
     N'تصميم عصري', 
     N'يوفر الدفء والراحة', 
     N'يتناسب مع جميع الأنماط', 
     4.00, 
     500, 
     4, 
     'img/Floor tiles4.jpg'),

    (N'سيراميك أرضيات خارجي', 
     N'أرضيات سيراميك خارجي بأحدث الألوان المتاحة في أربد، مقاومة للعوامل الجوية، مما يجعلها مثالية للمساحات الخارجية.', 
     N'مناسبة للأماكن الخارجية', 
     N'تتميز بالمتانة والصلابة', 
     N'تحمل الظروف المناخية', 
     5.00, 
     500, 
     4, 
     'img/Floor tiles5.jpg');

INSERT INTO Products (ProductName, ProductDescription, ProductDescriptionList1, ProductDescriptionList2, ProductDescriptionList3, Price, Stock, CategoryID, ProductImage)
VALUES 
    (N'مرايا مضيئة مربعة', 
     N'تقدم مرايا مضيئة مربعة الشكل الأحدث في المملكة، والتي تضيف لمسة عصرية وجمالية إلى أي غرفة، كما توفر إضاءة مريحة تساعد في رؤية واضحة.', 
     N'تصميم عصري ومبتكر', 
     N'تزيد من جمالية المساحة', 
     N'توفير إضاءة مريحة', 
     15.00, 
     500, 
     5, 
     'img/Mirrors11.jpg'),

    (N'مرايا مضيئة دائرية', 
     N'مرايا مضيئة دائرية الشكل الأحدث في المملكة، تخلق جوًا مميزًا في أي مكان وتضيف لمسة من الأناقة، مما يجعلها خيارًا مثاليًا للمساحات الصغيرة.', 
     N'جمالية ودقة في التصميم', 
     N'توفير إضاءة مريحة وجميلة', 
     N'مناسبة لكل أنواع الديكور', 
     15.00, 
     500, 
     5, 
     'img/Mirrors1.jpg'),

    (N'مرايا مضيئة مستطيلة الشكل', 
     N'تتميز مرايا مضيئة مستطيلة الشكل الأحدث في المملكة بجودة عالية وتصميم جذاب، مما يجعلها مثالية للاستخدام في الحمامات وغرف النوم.', 
     N'تصميم أنيق وعصري', 
     N'توفير إضاءة مريحة', 
     N'مثالية للاستخدام اليومي', 
     25.00, 
     500, 
     5, 
     'img/Mirrors13.jpg');

Select * from Products;
------------------------------------------------------Break------------------------------------------------------!
DROP TABLE IF EXISTS UserRoles;

CREATE TABLE UserRoles (
    UserID INT,
    Role NVARCHAR(50),
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserID, Role),
    CONSTRAINT FK_UserRole_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
INSERT INTO UserRoles (UserID, Role)
VALUES
(1, 'Main Admin'),
(2, 'Admin'),
(3, 'Client');
SELECT * FROM UserRoles;

------------------------------------------------------Break------------------------------------------------------!
-- . Feedback Table
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(255),
    Message NVARCHAR(1000),
    SentDate DATETIME DEFAULT GETDATE(),
);
------------------------------------------------------Break------------------------------------------------------!
CREATE TABLE Review (
	ReviewID INT PRIMARY KEY IDENTITY(1,1),
	ProductID INT,
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	UserID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
	CommentText NVARCHAR (MAX),
	Name NVARCHAR (100),
	Email NVARCHAR (100),
	RatingValue INT NOT NULL CHECK (RatingValue BETWEEN 1 AND 5),
	ReviewTime DATETIME DEFAULT GETDATE()
	);
------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!

------------------------------------------------------Break------------------------------------------------------!