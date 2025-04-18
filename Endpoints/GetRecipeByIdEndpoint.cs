using FastEndpoints;
using RecipeBook.API.Data;
using RecipeBook.API.Dtos;

public class GetRecipeByIdEndpoint : Endpoint<Guid, RecipeResponse>
{
    private readonly AppDbContext _db;

    public GetRecipeByIdEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/recipes/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Guid id, CancellationToken ct)
    {
        var recipe = await _db.Recipes.FindAsync(new object[] { id }, ct);

        if (recipe is null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(new RecipeResponse
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            CreatedAt = recipe.CreatedAt
        });
    }
}

