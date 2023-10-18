using HHPIZZA_BACKEND.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Linq;
using System.Dynamic;
using System.Runtime.CompilerServices;
using HHPIZZA_BACKEND;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HhpizzaDbContext>(builder.Configuration["HhpizzaDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:5169")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();
//Add for Cors 
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//Check if User exist in the system
app.MapGet("/checkuser/{uid}", (HhpizzaDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.Uid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});

//Create a User
app.MapPost("/api/user", (HhpizzaDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/user/{user.Id}", user);
});

//Get user by id
app.MapGet("/api/user/{id}", (HhpizzaDbContext db, int id) =>
{
    var user = db.Users.Single(u => u.Id == id);
    return user;
});

//Items
//Delete an Item
app.MapDelete("/api/item/{id}", (HhpizzaDbContext db, int id) =>
{
    Item item = db.Items.SingleOrDefault(p => p.Id == id);
    if (item == null)
    {
        return Results.NotFound();
    }
    db.Items.Remove(item);
    db.SaveChanges();
    return Results.NoContent();

});
//Update an Item
app.MapPut("/api/items/{id}", (HhpizzaDbContext db, int id, Item item) =>
{
    Item itemToUpdate = db.Items.SingleOrDefault(product => product.Id == id);
    if (itemToUpdate == null)
    {
        return Results.NotFound();
    }
    itemToUpdate.Name = item.Name;
    itemToUpdate.Price = item.Price;

    db.SaveChanges();
    return Results.NoContent();
});

//Add an item
app.MapPost("/api/items", (HhpizzaDbContext db, Item item) =>
{
    db.Items.Add(item);
    db.SaveChanges();
    return Results.Created($"/api/products/{item.Id}", item);
});

//Add an item to an order
app.MapPost("/api/items/{id}", (HhpizzaDbContext db, int id, Item item) =>
{
    var order = db.Orders.Where(o => o.Id == id).Include(I => I.items).FirstOrDefault();
    if (order == null)
    {
        return Results.NotFound("not found");
    }

    order.items.Add(item);
    db.SaveChanges();
    return Results.Created($"/api/items/{order.Id}", item);
});


//Get all items
app.MapGet("/api/allItems", (HhpizzaDbContext db) =>
{
    
    return db.Items;
}
);

//Orders 
//View user's orders
app.MapGet("api/getUserOrders/{userId}", (HhpizzaDbContext db, int userId) =>
{
    var userOrders = db.Orders.Where(o => o.UserId == userId);

    return userOrders.ToList();
});
//Delete an Order
app.MapDelete("/api/order/{id}", (HhpizzaDbContext db, int id) =>
{
    Order order = db.Orders.SingleOrDefault(o => o.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();

});

//Create an Order 
app.MapPost("/api/CreateOrder", (HhpizzaDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"/api/products/{order.Id}", order);


});
//Update an Order
app.MapPut("/api/Order/{id}", (HhpizzaDbContext db, int id, Order order) =>
{
    Order OrderToUpdate = db.Orders.SingleOrDefault(order => order.Id == id);
    if (OrderToUpdate == null)
    {
        return Results.NotFound();
    }
    OrderToUpdate.Name = order.Name;
    OrderToUpdate.Phone = order.Phone;
    OrderToUpdate.Email = order.Email;
    OrderToUpdate.OrderType = order.OrderType;
    OrderToUpdate.Status = order.Status;
    OrderToUpdate.PaymentId = order.PaymentId;
    OrderToUpdate.Tip = order.Tip;
    OrderToUpdate.ReviewId = order.ReviewId;

    db.SaveChanges();
    return Results.NoContent();
});

app.MapGet("/api/allOrders", (HhpizzaDbContext db) =>
{

    return db.Orders;
}
);

//View Order Items
app.MapGet("/api/OrderDetails/{id}", (HhpizzaDbContext db, int id) =>
{
    var getOrder = db.Orders.Where(o => o.Id == id).Include(x => x.items).FirstOrDefault();
    if (getOrder != null)
    {
        var items = getOrder.items.ToList();
        return Results.Ok(items);
    }
    else
    {
        return Results.NotFound();
    }
   
}
);

app.MapPost("/api/addReview/{id}", (HhpizzaDbContext db, int id, Review review) =>
{
    var getOrder = db.Orders.Where(o => o.Id == id).FirstOrDefault();
    db.Reviews.Add(review);
    db.SaveChanges();
    getOrder.ReviewId = review.Id;
    db.SaveChanges();
    return Results.Ok(getOrder);
});

//get Order's Review
app.MapGet("/api/getReview/{id}", (HhpizzaDbContext db, int id) =>
{
    var getOrder = db.Orders.Where(o => o.Id == id).Include(x => x.Review).FirstOrDefault();
    if (getOrder.ReviewId != null)
    {
        var review = db.Reviews.Where(r => r.Id == getOrder.ReviewId).ToList();
        return Results.Ok(review);
    }
    else
    {
        return Results.NotFound();
    }

}
);
app.Run();
