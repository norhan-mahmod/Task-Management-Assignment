# Task Management Assignment

o Setup instructions & How to run the project :-
run project on visual studio and also you need to run redis server at the same time by open redis-server.exe file from this folder after download folder from here  https://drive.google.com/drive/folders/1eLJN6kKvp1h45I_wY6uQGsfdUbpjH49S?usp=sharing  .
database automatically created after running the project and you can use API From swagger.
  
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
