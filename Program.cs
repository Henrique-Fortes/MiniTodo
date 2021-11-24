using MiniTodo.Data;
using MiniTodo.ViewModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(); //Gerencia toda conexao do banco de dados com a aplicacao (AppDbContext -> garante que so tenha uma conexao, ou fecha se ja tiver uma aberta)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/todos", (AppDbContext context) =>
{
    var todos = context.Todos.ToList();
    return Results.Ok(todos);
}).Produces<Todo>();

app.MapPost("v1/todos", (
    AppDbContext context,
    CreateTodoViewModel model) => {

    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo); //Salva as informacoes no banco
    context.SaveChanges();

        return Results.Created($"/v1/todos/{todo.Id}", todo); //resulta no httpost 201 -> created
});

app.Run();
