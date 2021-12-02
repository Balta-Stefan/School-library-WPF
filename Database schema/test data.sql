insert into genres(genreName) values('prvi zanr');
insert into genres(genreName) values('drugi zanr');

insert into publishers(publisherName) values('prvi izdavac');
insert into publishers(publisherName) values('drugi izdavac');

insert into authors(firstName, lastName) values('prvi', 'prvic');
insert into authors(firstName, lastName) values('drugi', 'drugic');

insert into books(`isbn13`, `isbn10`, bookTitle, edition, authorID, publisherID, genre) values('1111', '1111', 'prva knjiga', 1, 1, 1, 1);
insert into books(`isbn13`, `isbn10`, bookTitle, edition, authorID, publisherID, genre) values('2222', '2222', 'Druga knjiga', 2, 2, 2, 2);

insert into book_conditions(`condition`) values('new');
insert into book_conditions(`condition`) values('mildly damaged');
insert into book_conditions(`condition`) values('unusable');

insert into book_copies(conditionID, deliveredAt, bookID) values(1, '2019-06-14 12:30:15', 1);
insert into book_copies(conditionID, deliveredAt, bookID) values(1, '2019-06-14 12:30:15', 1);
insert into book_copies(conditionID, deliveredAt, bookID) values(1, '2019-06-14 12:30:15', 2);

insert into users(firstName, lastName, username, password, userType) values('Marko', 'Markovic', 'Marki', '1234', 'MEMBER');
insert into members(userID) values(1);
insert into users(firstName, lastName, username, password, userType) values('Stefan', 'Stefanovic', 'Stefi', '1234', 'MEMBER');
insert into members(userID) values(2);
insert into users(firstName, lastName, username, password, userType) values('Stefan', 'Stefkovic', 'Stefic', '1234', 'MEMBER');
insert into members(userID) values(3);
insert into users(firstName, lastName, username, password, userType) values('Petar', 'Petrovic', 'Petri', '1234', 'ACCOUNTANT');

insert into users(firstName, lastName, username, password, userType) values('prvi', 'bibliotekar', 'pb', '1234', 'LIBRARIAN');
insert into librarians(userID) values(4);