using DemoApi.Postgresql.Features;
using DemoApi.Postgresql.Features.Services;

namespace DemoApi.PostgreSQL.Features
{
    public static class TodoFeature
    {
        private const string BasePath = "/todos";

        public static void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(BasePath, async (ITodoService todoService, CancellationToken cancellationToken) =>
            {
                return await todoService.GetTodoListAsync();
            });

            endpoints.MapGet(BasePath + "/{id:Guid}", async (ITodoService todoService, Guid id, CancellationToken cancellationToken) =>
            {
                return await todoService.FindAsync(id);
            });

            endpoints.MapPost(BasePath, async (ITodoService todoService, CreateTodoDto createTodoDto, CancellationToken cancellationToken) =>
            {
                TodoDto todoDto = await todoService.CreateTodoAsync(createTodoDto);

                return Results.Created($"{BasePath}/{todoDto.Id}", todoDto);
            });

            endpoints.MapPut(BasePath + "/{id:Guid}", async (ITodoService todoService, Guid id, UpdateTodoDto todoToUpdate, CancellationToken cancellationToken) =>
            {
                await todoService.UpdateTodoAsync(id, todoToUpdate);

                return Results.NoContent();
            });

            endpoints.MapDelete(BasePath + "/{id:Guid}", async (ITodoService todoService, Guid id, CancellationToken cancellationToken) =>
            {
                await todoService.DeleteTodoAsync(id);

                return Results.Ok();
            });
        }
    }
}
