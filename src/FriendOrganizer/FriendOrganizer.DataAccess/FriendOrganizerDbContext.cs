using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace FriendOrganizer.DataAccess;

public class FriendOrganizerDbContext : DbContext
{
	public DbSet<Friend> Friends { get; set; }

	public FriendOrganizerDbContext(DbContextOptions options) : base(options) { }



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Remove table name pluralization that EF does by default
		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			entityType.SetTableName(entityType.DisplayName());
		}

		// Add seed data
		ConfigureSeeding(modelBuilder);

        /* Configure entities with Fluent API */
        // 01. If the configurations are small, can encapsulate the fluent api config into a private method.
        // AddFluentConfigurations(modelBuilder);

        // 02. A better approach is to have its own Fluent ApI configuration class that extends EntityTypeConfiguration
        // modelBuilder.ApplyConfiguration(new FriendConfiguration());

        // 03. Another approach is to use Data Annotations and behaviour that comes with C# nullable reference types configure on the models directly.
    }


	private void ConfigureSeeding(ModelBuilder modelBuilder)
	{
        modelBuilder.Entity<Friend>().HasData(
            new Friend { Id = 1, FirstName = "Kasun", LastName = "Kodagoda" },
            new Friend { Id = 2, FirstName = "Oreliya", LastName = "Fernando" },
            new Friend { Id = 3, FirstName = "John", LastName = "Doe" },
            new Friend { Id = 4, FirstName = "Jane", LastName = "Smith" });
    }

	private void AddFluentConfigurations(ModelBuilder modelBuilder)
	{
        modelBuilder.Entity<Friend>()
            .Property(f => f.FirstName)
            .IsRequired()
            .HasMaxLength(50);
    }
}


public class FriendConfiguration : IEntityTypeConfiguration<Friend>
{
	public void Configure(EntityTypeBuilder<Friend> builder)
	{
		builder.HasKey(f => f.Id);
		builder.Property(f => f.FirstName)
			.IsRequired()
			.HasMaxLength(50);
	}
}

public class FriendOrganizerDbContextFactory : IDesignTimeDbContextFactory<FriendOrganizerDbContext>
{
	public FriendOrganizerDbContext CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FriendOrganizer.UI"))
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

		var connectionString = configuration.GetConnectionString(nameof(FriendOrganizerDbContext));

		var dbContextOptionsBuilder = new DbContextOptionsBuilder<FriendOrganizerDbContext>();
		dbContextOptionsBuilder.UseSqlServer(connectionString);

		return new FriendOrganizerDbContext(dbContextOptionsBuilder.Options);
	}
}