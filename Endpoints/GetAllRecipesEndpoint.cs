using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using RecipeBook.API.Data;
using RecipeBook.API.Dtos;

public class GetAllRecipesEndpoint : EndpointWithoutRequest<List<RecipeResponse>>
{
    private readonly AppDbContext _db;

    public GetAllRecipesEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/recipes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var recipes = await _db.Recipes
            .Select(r => new RecipeResponse
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync(ct);

        await SendAsync(recipes);
    }
}
