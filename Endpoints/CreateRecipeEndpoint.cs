using FastEndpoints;
using RecipeBook.API.Data;
using RecipeBook.API.Models;
using Microsoft.EntityFrameworkCore;
using RecipeBook.API.Dtos;

namespace RecipeBook.API.Endpoints.Recipes
{

    public class CreateRecipeEndpoint : Endpoint<CreateRecipeRequest, RecipeResponse>
    {
        private readonly AppDbContext _db;

        public CreateRecipeEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Post("/recipes");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateRecipeRequest req, CancellationToken ct)
        {
            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = req.Title,
                Description = req.Description,
                Ingredients = req.Ingredients,
                Steps = req.Steps,
                CreatedAt = DateTime.UtcNow
            };

            await _db.Recipes.AddAsync(recipe, ct);
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

}


