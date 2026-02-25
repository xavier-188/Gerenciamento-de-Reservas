using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddCors(options =>
  options.AddPolicy("Acesso Total",
    configs => configs
      .AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod())
);

var app = builder.Build();
app.UseCors("AllowAll");

app.MapGet("/", () => "Restaurante");

app.MapGet("/api/restaurante/listar", ([FromServices] AppDbContext ctx) =>
{
    if (ctx.Clientes.Any())
    {
        return Results.Ok(ctx.Clientes.ToList());
    }
    return Results.BadRequest("A lista de clientes está vazia!");

});

app.MapPost("/api/restaurante/cadastrar", ([FromBody] Cliente cliente, [FromServices] AppDbContext ctx) =>
{
    var existente = ctx.Clientes.FirstOrDefault(c => c.Email == cliente.Email);
    if (existente != null)
        return Results.Conflict("Cliente já cadastrado!");

    ctx.Clientes.Add(cliente);
    ctx.SaveChanges();
    return Results.Created("", cliente);

});

app.MapDelete("/api/restaurante/remover/{id}", ([FromRoute] int id, [FromServices] AppDbContext ctx) =>
{

    Cliente clienteBuscado = ctx.Clientes.Find(id);
    if (clienteBuscado == null)
    {
        return Results.NotFound("Cliente não encontrado!");
    }

    ctx.Clientes.Remove(clienteBuscado);
    ctx.SaveChanges();
    return Results.Ok("Cliente Removido!");

});

app.MapPatch("/api/restaurante/alterar/{id}", ([FromRoute] int id, [FromServices]AppDbContext ctx, [FromBody] Cliente clienteAlterado) =>
{
    Cliente clienteBuscado = ctx.Clientes.Find(id);
    if (clienteBuscado == null)
    {
        return Results.NotFound("Cliente não encontrado!");
    }
    clienteBuscado.Nome = clienteAlterado.Nome;
    clienteBuscado.Email = clienteAlterado.Email;
    clienteBuscado.Telefone = clienteAlterado.Telefone;

    ctx.Clientes.Update(clienteBuscado);
    ctx.SaveChanges();
    return Results.Ok("Cliente alterado com sucesso!");
    
});

app.Run();
