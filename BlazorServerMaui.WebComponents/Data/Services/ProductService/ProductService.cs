using BlazorServerMaui.WebComponents.Data.Entities;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace BlazorServerMaui.WebComponents.Data.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpContextAccessor _httpContext;
    private readonly string? _connectionString;

    public List<Product> Products { get; set; } = new List<Product>();

    public ProductService(IConfiguration config, HttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
        _connectionString = config.GetConnectionString("Default");
        if (_connectionString == null)
            _connectionString =
                "server=localhost\\sqlexpress;database=blazortest;trusted_connection=true;TrustServerCertificate=True;";
    }

    public async Task<List<Product>> GetProducts()
    {
        using var connection = new SqlConnection(_connectionString);

        Console.WriteLine("Entered product service");

        var result =  await connection.QueryAsync<Product>("dbo.GetProducts",
            commandType: CommandType.StoredProcedure);

        if(result != null) return result.ToList();

        //Products = result.ToList();

        throw new Exception();
    }

    public Task<Product> GetProductById(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetProductByCategory(string categoryUrl)
    {
        throw new NotImplementedException();
    }

    public Task<Product> CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    //public void Login(string cp)
    //{
    //    var user = new GenericPrincipal(new ClaimsIdentity("test Pier"), new []{"user"});
    //    _httpContext.HttpContext.User = user;
    //    Console.WriteLine(_httpContext.HttpContext.User.Identity.Name);
    
    //}
}