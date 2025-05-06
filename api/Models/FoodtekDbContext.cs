using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public partial class FoodtekDbContext : DbContext
{
    public FoodtekDbContext()
    {
    }

    public FoodtekDbContext(DbContextOptions<FoodtekDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Authentication> Authentications { get; set; }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemBadge> ItemBadges { get; set; }

    public virtual DbSet<ItemOption> ItemOptions { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<OptionCategory> OptionCategories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<ProblemTicket> ProblemTickets { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<SuggestionTicket> SuggestionTickets { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MRAYYAN\\LOCALHOST;Initial Catalog=foodtek;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Addresse__3214EC073C816C52");

            entity.Property(e => e.AddressName).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasColumnName("CLientID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Hint).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Province).HasMaxLength(255);
            entity.Property(e => e.Region).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Addresses__CLien__7B5B524B");
        });

        modelBuilder.Entity<Authentication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authenti__3214EC07F0AAFCCF");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProviderLoginId).HasMaxLength(255);
            entity.Property(e => e.ProviderName).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.Authentications)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Authentic__Clien__0E391C95");
        });

        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookmark__3214EC072597C46E");

            entity.HasIndex(e => new { e.ClientId, e.ItemId }, "UQ__Bookmark__4159F21DB22B5FB8").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Client).WithMany(p => p.Bookmarks)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Bookmarks__Clien__4A8310C6");

            entity.HasOne(d => d.Item).WithMany(p => p.Bookmarks)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Bookmarks__ItemI__4B7734FF");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3214EC07789764FC");

            entity.ToTable("CartItem");

            entity.HasOne(d => d.Item).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__ItemId__7EC1CEDB");

            entity.HasOne(d => d.User).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__UserId__7FB5F314");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07C4419C58");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameAr)
                .HasMaxLength(255)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Offer).WithMany(p => p.Categories)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Categorie__Offer__2739D489");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC07BEA5272F");

            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.ClientStatus).HasMaxLength(20);

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Clients__UserId__6A30C649");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conversa__3214EC075B86CD1C");

            entity.HasIndex(e => new { e.DriverId, e.ClientId }, "UQ__Conversa__AFD62C852DAAD1F4").IsUnique();

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");

            entity.HasOne(d => d.Client).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Conversat__Clien__6FE99F9F");

            entity.HasOne(d => d.Driver).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Conversat__Drive__6EF57B66");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3214EC07438759E3");

            entity.HasOne(d => d.User).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Drivers__UserId__656C112C");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0750EBA077");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employees__RoleI__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Employees__UserI__628FA481");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Items__3214EC07CC2CF56E");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemDescriptionAr)
                .HasMaxLength(255)
                .HasColumnName("ItemDescriptionAR");
            entity.Property(e => e.ItemDescriptionEn)
                .HasMaxLength(255)
                .HasColumnName("ItemDescriptionEN");
            entity.Property(e => e.ItemNameAr)
                .HasMaxLength(255)
                .HasColumnName("ItemNameAR");
            entity.Property(e => e.ItemNameEn)
                .HasMaxLength(255)
                .HasColumnName("ItemNameEN");
            entity.Property(e => e.Price).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Items__CategoryI__395884C4");

            entity.HasOne(d => d.ItemBadge).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemBadgeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Items__ItemBadge__3A4CA8FD");

            entity.HasOne(d => d.Offer).WithMany(p => p.Items)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Items__OfferId__3864608B");
        });

        modelBuilder.Entity<ItemBadge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemBadg__3214EC0735927FB7");

            entity.Property(e => e.BadgeDescription).HasMaxLength(255);
            entity.Property(e => e.BadgeName).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ItemOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemOpti__3214EC075BF84FAC");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemOptions)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__ItemOptio__ItemI__634EBE90");

            entity.HasOne(d => d.Option).WithMany(p => p.ItemOptions)
                .HasForeignKey(d => d.OptionId)
                .HasConstraintName("FK__ItemOptio__Optio__625A9A57");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Messages__3214EC076E8CB2B6");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MessageText).HasColumnType("text");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("FK__Messages__Conver__75A278F5");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Messages__Sender__74AE54BC");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC077A08FC85");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionAr)
                .HasMaxLength(255)
                .HasColumnName("DescriptionAR");
            entity.Property(e => e.DescriptionEn)
                .HasMaxLength(255)
                .HasColumnName("DescriptionEN");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.NotificationType).HasMaxLength(255);
            entity.Property(e => e.TitleAr)
                .HasMaxLength(255)
                .HasColumnName("TitleAR");
            entity.Property(e => e.TitleEn)
                .HasMaxLength(255)
                .HasColumnName("TitleEN");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__078C1F06");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Offers__3214EC074E72F563");

            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionAr)
                .HasMaxLength(255)
                .HasColumnName("DescriptionAR");
            entity.Property(e => e.DescriptionEn)
                .HasMaxLength(255)
                .HasColumnName("DescriptionEN");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LimitAmount).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.OfferStatus).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TitleAr)
                .HasMaxLength(255)
                .HasColumnName("TitleAR");
            entity.Property(e => e.TitleEn)
                .HasMaxLength(255)
                .HasColumnName("TitleEN");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserPersons).HasDefaultValue(0);
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Options__3214EC07162B7EA3");

            entity.HasIndex(e => new { e.NameAr, e.NameEn, e.CategoryId }, "UQ__Options__1AE41C020033EDE0").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsRequired).HasDefaultValue(false);
            entity.Property(e => e.NameAr)
                .HasMaxLength(255)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Options)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Options__Categor__5CA1C101");
        });

        modelBuilder.Entity<OptionCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OptionCa__3214EC07B3C354E2");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameAr)
                .HasMaxLength(255)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC073B0384C9");

            entity.Property(e => e.ClientRate).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.ClientReview).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverRate).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.DriverReview).HasMaxLength(255);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(20);
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__ClientId__0D7A0286");

            entity.HasOne(d => d.DeliveryAddress).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Delivery__0F624AF8");

            entity.HasOne(d => d.Driver).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Orders__DriverId__0E6E26BF");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__Orders__PaymentM__0C85DE4D");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07954B04ED");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Review).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__OrderItem__ItemI__43D61337");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__44CA3770");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OTPs__3214EC07C00BFB9F");

            entity.ToTable("OTPs");

            entity.Property(e => e.Attempt).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate)
                .HasDefaultValueSql("(dateadd(minute,(10),getdate()))")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.Otpcode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OTPCode");

            entity.HasOne(d => d.User).WithMany(p => p.Otps)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__OTPs__UserId__00DF2177");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC077AD5A015");

            entity.Property(e => e.CardHolderName).HasMaxLength(255);
            entity.Property(e => e.CardNumber).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Cvc)
                .HasMaxLength(255)
                .HasColumnName("CVC");
            entity.Property(e => e.ExpiryDate).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__PaymentMe__Clien__01142BA1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC0796064301");

            entity.HasIndex(e => e.NameEn, "UQ__Permissi__EE1C774FEB690D57").IsUnique();

            entity.HasIndex(e => e.NameAr, "UQ__Permissi__EE1CD24CD9E49906").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionAr)
                .HasMaxLength(255)
                .HasColumnName("DescriptionAR");
            entity.Property(e => e.DescriptionEn)
                .HasMaxLength(255)
                .HasColumnName("DescriptionEN");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("NameEN");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persons__3214EC073AADD1DE");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Persons__85FB4E3810F71329").IsUnique();

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProfileImageUrl).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.People)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Persons__UserId__44FF419A");
        });

        modelBuilder.Entity<ProblemTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProblemT__3214EC075C81EFF4");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RefundAmount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TicketDescription).HasMaxLength(255);
            entity.Property(e => e.TicketStatus).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.ProblemTickets)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__ProblemTi__Clien__72910220");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC075AAE4278");

            entity.HasIndex(e => e.RoleNameEn, "UQ__Roles__CB8E649092F88850").IsUnique();

            entity.HasIndex(e => e.RoleNameAr, "UQ__Roles__CB8F8509686EA8D6").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleNameAr)
                .HasMaxLength(50)
                .HasColumnName("RoleNameAR");
            entity.Property(e => e.RoleNameEn)
                .HasMaxLength(50)
                .HasColumnName("RoleNameEN");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolePerm__3214EC077C375697");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "UQ__RolePerm__6400A1A9551AD2F0").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK__RolePermi__Permi__5EBF139D");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__RolePermi__RoleI__5DCAEF64");
        });

        modelBuilder.Entity<SuggestionTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Suggesti__3214EC078BFF4F3C");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TicketDescription).HasMaxLength(255);
            entity.Property(e => e.TicketStatus).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.SuggestionTickets)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Suggestio__Clien__6AEFE058");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tokens__3214EC07F2460C62");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsExpired).HasDefaultValue(false);
            entity.Property(e => e.Jwttoken)
                .HasMaxLength(255)
                .HasColumnName("JWTToken");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Tokens__UserId__7849DB76");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07DEEB210C");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343B9B97E5").IsUnique();

            entity.HasIndex(e => e.UserNameHash, "UQ__Users__E13D9079A1687C29").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLogging).HasDefaultValue(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserNameHash).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
