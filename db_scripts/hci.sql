create database if not exists hci;

create table if not exists hci.softwares(
	softwareId varchar(15) not null primary key,
	name varchar(60) not null,
	operatingSys varchar(15) not null,
	developer varchar(30) not null,
	site varchar(50) not null,
	year int(5) not null,
	price double not null,
	description varchar(50) not null
);

create table if not exists hci.classrooms(
	classroomId varchar(15) not null primary key,
	description varchar(60) not null,
	size int(5) not null,
	haveProjector boolean not null,
	haveBoard boolean not null,
	haveSmartBoard boolean not null,
	operatingSys varchar(15) not null
);

create table if not exists hci.softwareInClassroom(
	classroomId varchar(15) not null,
	softwareId varchar(15) not null,
	CONSTRAINT `classroomId_in_softclass`
	FOREIGN KEY (classroomId) REFERENCES hci.classrooms (classroomId),
	CONSTRAINT `softId_in_softclass`
	FOREIGN KEY (softwareId) REFERENCES hci.softwares (softwareId)
);

create table if not exists hci.courses(
	courseId varchar(15) not null primary key,
	name varchar(60) not null,
	date_ varchar(15) not null,
	description varchar(50) not null
);

create table if not exists hci.subjects(
	subjectId varchar(15) not null primary key,
	name varchar(60) not null,
	description varchar(50) not null,
	size int(5) not null,
	minLength int(5) not null,
	noOfClasses int(5) not null,
	needProjector boolean not null,
	needBoard boolean not null,
	needSmartBoard boolean not null,
	needOperatingSys varchar(15) not null,
	needSoftware varchar(15) not null,
	courseId varchar(15) not null,
	CONSTRAINT `courseId_in_subjects`
	FOREIGN KEY (courseId) REFERENCES hci.courses (courseId),
	CONSTRAINT `softId_in_subjects`
	FOREIGN KEY (needSoftware) REFERENCES hci.softwares (softwareId)
);

Insert into hci.softwares (softwareId, name, operatingSys, developer, site, year, price, description) values
("VS17","Visual Studio 2017", "Windows 10", "Microsoft", "www.microsoft.com", 2017, 150.50, "IDE");