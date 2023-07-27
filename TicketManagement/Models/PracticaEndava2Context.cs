using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TicketManagement.Models;

public partial class PracticaEndava2Context : DbContext
{
    public PracticaEndava2Context()
    {
    }

    public PracticaEndava2Context(DbContextOptions<PracticaEndava2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<EventU> EventUs { get; set; }

    public virtual DbSet<OrderU> OrderUs { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TotalNumberOfTicketsPerCategory> TotalNumberOfTicketsPerCategories { get; set; }

    public virtual DbSet<UserU> UserUs { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-L6120QQ;Initial Catalog=PracticaEndava2;Integrated Security=True;TrustServerCertificate=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.IdEventType).HasName("PK__EventTyp__261161616FB32E21");

            entity.ToTable("EventType");

            entity.HasIndex(e => e.EventTypeName, "UQ__EventTyp__F1C27EB158118B3E").IsUnique();

            entity.Property(e => e.IdEventType).HasColumnName("idEventType");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("eventTypeName");
        });

        modelBuilder.Entity<EventU>(entity =>
        {
            entity.HasKey(e => e.Idevent).HasName("PK__Event1__FCEAF5DBE6C8FC22");

            entity.ToTable("EventU");

            entity.Property(e => e.Idevent).HasColumnName("idevent");
            entity.Property(e => e.DescriptionEvent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descriptionEvent");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("eventName");
            entity.Property(e => e.IdEventType).HasColumnName("idEventType");
            entity.Property(e => e.IdVenue).HasColumnName("idVenue");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");

            entity.HasOne(d => d.IdEventTypeNavigation).WithMany(p => p.EventUs)
                .HasForeignKey(d => d.IdEventType)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Event1__idEventT__59FA5E80");

            entity.HasOne(d => d.IdVenueNavigation).WithMany(p => p.EventUs)
                .HasForeignKey(d => d.IdVenue)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Event1__idVenue__59063A47");
        });

        modelBuilder.Entity<OrderU>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Order1__C8AAF6FF23B87EFF");

            entity.ToTable("OrderU");

            entity.Property(e => e.IdOrder).HasColumnName("idOrder");
            entity.Property(e => e.IdTicketCategory).HasColumnName("idTicketCategory");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.NumberOfTickets).HasColumnName("numberOfTickets");
            entity.Property(e => e.OrderedAt)
                .HasColumnType("datetime")
                .HasColumnName("orderedAt");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("totalPrice");

            entity.HasOne(d => d.IdTicketCategoryNavigation).WithMany(p => p.OrderUs)
                .HasForeignKey(d => d.IdTicketCategory)
                .HasConstraintName("FK__Order1__idTicket__6B24EA82");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.OrderUs)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Order1__idUser__6A30C649");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.IdTicketCategory).HasName("PK__TicketCa__106E803D3FDEE7DC");

            entity.ToTable("TicketCategory");

            entity.Property(e => e.IdTicketCategory).HasColumnName("idTicketCategory");
            entity.Property(e => e.DescriptionEventCategory)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descriptionEventCategory");
            entity.Property(e => e.IdEvent).HasColumnName("idEvent");
            entity.Property(e => e.Price)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.IdEvent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TicketCat__idEve__5CD6CB2B");
        });

        modelBuilder.Entity<TotalNumberOfTicketsPerCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("total_number_of_tickets_per_category");

            entity.Property(e => e.IdTicketCategory).HasColumnName("idTicketCategory");
            entity.Property(e => e.TotalNumberOfTickets).HasColumnName("total_number_of_tickets");
            entity.Property(e => e.TotalSalesValue)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("total_sales_value");
        });

        modelBuilder.Entity<UserU>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__User1__3717C98292AF7BF9");

            entity.ToTable("UserU");

            entity.HasIndex(e => e.Email, "UQ__User1__AB6E616493F9E0EB").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Email)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PasswordUser)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passwordUser");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.IdVenue).HasName("PK__Venue__077D5E69B59563A4");

            entity.ToTable("Venue");

            entity.Property(e => e.IdVenue).HasColumnName("idVenue");
            entity.Property(e => e.VenueCapacity).HasColumnName("venueCapacity");
            entity.Property(e => e.VenueLocation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("venueLocation");
            entity.Property(e => e.VenueType)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("venueType");
        });
        modelBuilder.HasSequence("event_seq").IncrementsBy(50);
        modelBuilder.HasSequence("event_type_seq").IncrementsBy(50);
        modelBuilder.HasSequence("event1_seq").IncrementsBy(50);
        modelBuilder.HasSequence("order_model_seq").IncrementsBy(50);
        modelBuilder.HasSequence("order_seq").IncrementsBy(50);
        modelBuilder.HasSequence("order1_seq").IncrementsBy(50);
        modelBuilder.HasSequence("ticket_category_seq").IncrementsBy(50);
        modelBuilder.HasSequence("user_seq").IncrementsBy(50);
        modelBuilder.HasSequence("user1_seq").IncrementsBy(50);
        modelBuilder.HasSequence("venue_seq").IncrementsBy(50);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
