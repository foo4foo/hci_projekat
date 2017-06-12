create database if not exists hci;

create table if not exists hci.softwares(
	ID int(15) not null auto_increment primary key,
	softwareId varchar(15) unique not null,
	name varchar(60) not null,
	operatingSys varchar(15) not null,
	developer varchar(30) not null,
	site varchar(50) not null,
	year int(5) not null,
	price double not null,
	description varchar(50) not null
);

create table if not exists hci.classrooms(
	ID int(15) not null auto_increment primary key,
	classroomId varchar(15) not null,
	description varchar(60) not null,
	size int(5) not null,
	haveProjector boolean not null,
	haveBoard boolean not null,
	haveSmartBoard boolean not null,
	operatingSys varchar(15) not null
);

create table if not exists hci.softwareInClassroom(
	classroomId int(15) not null,
	softwareId int(15) not null,
	CONSTRAINT `classroomId_in_softclass`
	FOREIGN KEY (classroomId) REFERENCES hci.classrooms (ID),
	CONSTRAINT `softId_in_softclass`
	FOREIGN KEY (softwareId) REFERENCES hci.softwares (ID)
);

create table if not exists hci.courses(
	ID int(15) not null auto_increment primary key,
	courseId varchar(15) unique not null,
	name varchar(60) not null,
	date_ varchar(15) not null,
	description varchar(50) not null
);

create table if not exists hci.subjects(
	ID int(15) not null auto_increment primary key,
	subjectId varchar(15) unique not null,
	name varchar(60) not null,
	description varchar(50) not null,
	size int(5) not null,
	minLength int(5) not null,
	noOfClasses int(5) not null,
	needProjector boolean not null,
	needBoard boolean not null,
	needSmartBoard boolean not null,
	needOperatingSys varchar(15) not null,
	courseId int(15) not null,
	CONSTRAINT `courseId_in_subjects`
	FOREIGN KEY (courseId) REFERENCES hci.courses (ID)
	
);

create table if not exists hci.softwareInSubject(
	subjectId int(15) not null,
	softwareId int(15) not null,
	CONSTRAINT `subjectId_in_softsubject`
	FOREIGN KEY (subjectId) REFERENCES hci.subjects (ID),
	CONSTRAINT `softId_in_softsubject`
	FOREIGN KEY (softwareId) REFERENCES hci.softwares (ID)
);


Insert into hci.softwares (softwareId, name, operatingSys, developer, site, year, price, description) values
("VS17","Visual Studio 2017", "Windows 10", "Microsoft", "www.microsoft.com", 2017, 150.50, "IDE");