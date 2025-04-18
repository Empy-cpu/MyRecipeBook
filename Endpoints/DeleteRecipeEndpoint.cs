using FastEndpoints;
using RecipeBook.API.Data;

public class DeleteRecipeEndpoint : Endpoint<Guid>
{
    private readonly AppDbContext _db;

    public DeleteRecipeEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Delete("/recipes/{id}");
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

        _db.Recipes.Remove(recipe);
        await _db.SaveChangesAsync(ct);

        await SendNoContentAsync();
    }
}

