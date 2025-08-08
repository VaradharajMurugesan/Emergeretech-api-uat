---create table tbl_Job_career(JobTitle nvarchar(50), JobDescription nvarchar(100), NoOfVacancies nvarchar(100),
--ExpectedExperience nvarchar(50), JobLocation nvarchar(100), PostedOn nvarchar(100))

--create table tbl_resume_upload(JobTitle nvarchar(50), candidate_name varchar(50), DOB date, resume_link nvarchar(500)) 

select * from tbl_resume_upload

--insert into	tbl_Job_career(JobTitle,JobDescription,NoOfVacancies,ExpectedExperience, JobLocation,PostedOn) values('Developer', 'App Developer','5', '4','Bangalore', '09-09-2022')

select * from  tbl_Job_career

--INSERT INTO tbl_resume_upload (JobTitle,candidate_name,DOB,resume_link) VALUES ('Testing','Testing','2022-10-14','C:\Users\intel\source\repos\EmergereJobCareerWebApi\EmergereJobCareerWebApi\wwwroot\sample.wav')