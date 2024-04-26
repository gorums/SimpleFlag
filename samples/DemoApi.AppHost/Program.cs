var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DemoApi_Postgresql>("demoapi-postgresql");

builder.Build().Run();
