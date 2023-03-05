namespace Ecommerce.Common;

using Microsoft.AspNetCore.Mvc.ActionConstraints;

// Add action constrains thus allows multiple Query for the same route: 
// Link: https://stackoverflow.com/questions/46477959/is-it-possible-to-have-multiple-gets-that-vary-only-by-parameters-in-asp-net-cor
public class QueryParamAttribute : Attribute, IActionConstraint
{
    private readonly string[] keys;

    public QueryParamAttribute(params string[] keys) =>
        this.keys = keys;

    public int Order => 0;

    // True: if query satisfies the constrains, otherwise false
    public bool Accept(ActionConstraintContext context)
    {
        var query = context.RouteContext.HttpContext.Request.Query;
        return query.Count == keys.Length && keys.All(key => query.ContainsKey(key));
    }
}