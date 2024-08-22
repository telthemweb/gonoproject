var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.KeryNhava>("kerynhava");

builder.Build().Run();
