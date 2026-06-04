# Task Management Assignment

o Setup instructions & How to run the project :-
run project on visual studio and also you need to run redis server at the same time . it will create database automatically and you can use API From swagger.
  
o Seeded admin credentials
Email: admin@example.com  
Password: Admin@123  

o Any assumptions you made  
I supposed that 
 Priority values are
    High = 1,
    Low = 2

Status
{
    Pending = 1,
    InProgress = 2,
    Done = 3
}

The project also applies Global exception handling  & Soft delete for users & task Processing using hangfire Background jobs & and caching task into redis after first getTaskById request
