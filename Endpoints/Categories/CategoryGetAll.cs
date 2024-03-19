﻿using IWantAPP.Domain.Products;
using IWantAPP.Infra.Data;

namespace IWantAPP.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    
    public static IResult Action(ApplicationDbContext context)
    {
        var categories = context.Categories.ToList();
        var response = categories.Select(c => new CategoyResponse { Id = c.Id, Name = c.Name, Active = c.Active });    

        return Results.Ok(response);
    }
}
