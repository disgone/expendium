using Expendium.Api.Infrastructure;
using Expendium.Data;
using Expendium.Data.Entities;

namespace Expendium.Api.Endpoints.Categories;

public class Categories : EndpointGroup
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this);

        group.MapGet("/", (ExpendiumDbContext dbContext) =>
        {
            return dbContext.Categories.Select(n => new Category
            {
                Id = n.ExpenseCategoryId, Name = n.Name
            }).ToList();
        });

        group.MapPost("/", (ExpendiumDbContext dbContext, CreateCategory category) =>
        {
            var newCategory = new ExpenseCategory { Name = category.Name };

            dbContext.Categories.Add(newCategory);
            dbContext.SaveChanges();

            return Results.Created($"/api/categories/{newCategory.ExpenseCategoryId}",
                newCategory);
        }).Produces<int>(StatusCodes.Status201Created, "application/json");

        group.MapDelete("/{id}", (ExpendiumDbContext dbContext, int id) =>
        {
            var category = dbContext.Categories.Find(id);
            if (category is null)
            {
                return Results.NotFound();
            }

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();

            return Results.NoContent();
        });
    }

    private record Category
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    private record CreateCategory
    {
        public string Name { get; }
    }
}