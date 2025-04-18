namespace RecipeBook.API.Dtos
{
    public class CreateRecipeRequest
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<string> Ingredients { get; set; } = new();
        public List<string> Steps { get; set; } = new();
    }

    public class RecipeResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }

}
