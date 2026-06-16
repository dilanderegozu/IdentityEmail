using IdentityEmail.Context;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmailContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<EmailContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EmailContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    var allUsers = userManager.Users.ToList();
    Console.WriteLine($"Toplam kullanıcı sayısı: {allUsers.Count}");

    if (allUsers.Any())
    {
        var firstUser = allUsers.First();
        Console.WriteLine($"Seçilen kullanıcı: {firstUser.UserName}, Id: {firstUser.Id}");

        context.MessageCategories.AddRange(
            new MessageCategory { Name = "İş", Icon = "work", Color = "secondary", UserId = firstUser.Id },
            new MessageCategory { Name = "Okul", Icon = "school", Color = "primary", UserId = firstUser.Id },
            new MessageCategory { Name = "Kişisel", Icon = "person", Color = "tertiary", UserId = firstUser.Id },
            new MessageCategory { Name = "Finans", Icon = "payments", Color = "error", UserId = firstUser.Id },
            new MessageCategory { Name = "Seyahat", Icon = "flight", Color = "[#1D9E75]", UserId = firstUser.Id }
        );

        await context.SaveChangesAsync();
        Console.WriteLine("Kategoriler başarıyla eklendi.");
    }
    else
    {
        Console.WriteLine("Hiç kullanıcı bulunamadı, kategori eklenemedi.");
    }
}

app.Run();

