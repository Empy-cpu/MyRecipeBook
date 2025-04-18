using FastEndpoints;
using RecipeBook.API.Data;
using RecipeBook.API.Dtos;

public class UpdateRecipeEndpoint : Endpoint<UpdateRecipeRequest, RecipeResponse>
{
    private readonly AppDbContext _db;

    public UpdateRecipeEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Put("/recipes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateRecipeRequest req, CancellationToken ct)
    {
        var recipe = await _db.Recipes.FindAsync(new object[] { req.Id }, ct);

        if (recipe is null)
        {
            await SendNotFoundAsync();
            return;
        }

        recipe.Title = req.Title;
        recipe.Description = req.Description;
        recipe.Ingredients = req.Ingredients;
        recipe.Steps = req.Steps;

        await _db.SaveChangesAsync(ct);

        await SendAsync(new RecipeResponse
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            CreatedAt = recipe.CreatedAt
        });
    }
}

