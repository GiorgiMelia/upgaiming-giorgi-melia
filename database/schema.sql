CREATE TABLE Authors (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);


CREATE TABLE Books (ID INT IDENTITY(1,1) PRIMARY KEY, Title VARCHAR(150) NOT NULL, AuthorID INT NOT NULL, PublicationYear INT NOT NULL,
    CONSTRAINT FK_Books_Authors FOREIGN KEY (AuthorID) REFERENCES Authors(ID)
);

INSERT INTO Authors (Name)
VALUES 
    ('Robert C. Martin'),
    ('Jeffrey Richter');

INSERT INTO Books (Title, AuthorID, PublicationYear)
VALUES
    ('Clean Code', 1, 2008),
    ('The Clean Coder', 1, 2011),
    ('CLR via C#', 2, 2012),
    ('Clean Architecture', 1, 2017);

UPDATE Books
SET PublicationYear = 2013 WHERE ID = 2;


SELECT ID, Title, PublicationYear FROM Books
WHERE ID = 2;


DELETE FROM Books
WHERE ID = 3;


SELECT b.Title AS BookTitle,a.Name AS AuthorName, b.PublicationYear AS publ_year
FROM Books  b INNER JOIN Authors  a
ON b.AuthorID = a.ID
WHERE b.PublicationYear > 2010;