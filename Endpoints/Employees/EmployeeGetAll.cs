﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantAPP.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
    {
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();

        var employees = new List<EmployeeResponse>();
        foreach (var user in users)
        {
            var claim = userManager.GetClaimsAsync(user).Result;
            var claimName = claim.FirstOrDefault(c => c.Type == "Name");
            var userName  = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(user.Email, userName));
        }


        return Results.Ok(employees);
    }
}
