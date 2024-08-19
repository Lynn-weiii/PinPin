﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PinPinServer.Models;

public partial class PinPinContext : DbContext
{
    public PinPinContext(DbContextOptions<PinPinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatroomChat> ChatroomChats { get; set; }

    public virtual DbSet<CostCurrencyCategory> CostCurrencyCategories { get; set; }

    public virtual DbSet<FavorCategory> FavorCategories { get; set; }

    public virtual DbSet<LocationCategory> LocationCategories { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleAuthority> ScheduleAuthorities { get; set; }

    public virtual DbSet<ScheduleAuthorityCategory> ScheduleAuthorityCategories { get; set; }

    public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; }

    public virtual DbSet<ScheduleGroup> ScheduleGroups { get; set; }

    public virtual DbSet<SchedulePreview> SchedulePreviews { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<SplitCategory> SplitCategories { get; set; }

    public virtual DbSet<SplitExpense> SplitExpenses { get; set; }

    public virtual DbSet<SplitExpenseParticipant> SplitExpenseParticipants { get; set; }

    public virtual DbSet<Transportation> Transportations { get; set; }

    public virtual DbSet<TransportationCategory> TransportationCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFavor> UserFavors { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<VoteOption> VoteOptions { get; set; }

    public virtual DbSet<VoteResult> VoteResults { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    public virtual DbSet<WishlistDetail> WishlistDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatroomChat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chatroom__3213E83FADF2DB40");

            entity.ToTable("chatroom_chat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsFocus)
                .HasDefaultValue(false)
                .HasColumnName("is_focus");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasColumnName("message");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.ChatroomChats)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_chatroom_chat_schedule");

            entity.HasOne(d => d.User).WithMany(p => p.ChatroomChats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chatroom_chat_user");
        });

        modelBuilder.Entity<CostCurrencyCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cost_cur__3213E83F9B9C291A");

            entity.ToTable("cost_currency_category");

            entity.HasIndex(e => e.Code, "UQ__cost_cur__357D4CF9701A4511").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("code");
            entity.Property(e => e.Icon)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<FavorCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__favor_ca__3213E83F83E51EF2");

            entity.ToTable("favor_category");

            entity.HasIndex(e => e.Category, "UQ__favor_ca__F7F53CC2AB9F54A2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Icon)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<LocationCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__location__3213E83F7E778882");

            entity.ToTable("location_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValue("#000000")
                .HasColumnName("color");
            entity.Property(e => e.Icon)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

            entity.HasOne(d => d.Wishlist).WithMany(p => p.LocationCategories)
                .HasForeignKey(d => d.WishlistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_location_category_wishlist");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83FD5094DB5");

            entity.ToTable("schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Lat)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("lat");
            entity.Property(e => e.Lng)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("lng");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.PlaceId)
                .HasMaxLength(500)
                .HasColumnName("place_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_schedule_user");
        });

        modelBuilder.Entity<ScheduleAuthority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83F226532A2");

            entity.ToTable("schedule_authority");

            entity.HasIndex(e => new { e.ScheduleId, e.UserId, e.AuthorityCategoryId }, "unique_schedule_id_user_id_authority_category_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorityCategoryId).HasColumnName("authority_category_id");
            entity.Property(e => e.Rowversion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.AuthorityCategory).WithMany(p => p.ScheduleAuthorities)
                .HasForeignKey(d => d.AuthorityCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_schedule_authority_category");

            entity.HasOne(d => d.Schedule).WithMany(p => p.ScheduleAuthorities)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_schedule_authority_schedule");

            entity.HasOne(d => d.User).WithMany(p => p.ScheduleAuthorities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_schedule_authority_user");
        });

        modelBuilder.Entity<ScheduleAuthorityCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83FE65E9D72");

            entity.ToTable("schedule_authority_category");

            entity.HasIndex(e => e.Category, "UQ__schedule__F7F53CC2F7387FFA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("category");
        });

        modelBuilder.Entity<ScheduleDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83F7AAF1CFB");

            entity.ToTable("schedule_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Lat)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("lat");
            entity.Property(e => e.Lng)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("lng");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.LocationName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("location_name");
            entity.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_time");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.Rowversion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.ScheduleDayId).HasColumnName("schedule_day_id");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ScheduleDay).WithMany(p => p.ScheduleDetails)
                .HasForeignKey(d => d.ScheduleDayId)
                .HasConstraintName("FK_schedule_details_schedule_preview");

            entity.HasOne(d => d.User).WithMany(p => p.ScheduleDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_schedule_details_user");
        });

        modelBuilder.Entity<ScheduleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83F1E225948");

            entity.ToTable("schedule_group");

            entity.HasIndex(e => new { e.ScheduleId, e.UserId }, "unique_schedule_i_user_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsHoster).HasColumnName("is_hoster");
            entity.Property(e => e.JoinedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("joined_date");
            entity.Property(e => e.LeftDate)
                .HasColumnType("datetime")
                .HasColumnName("left_date");
            entity.Property(e => e.Rowversion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("rowversion");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.ScheduleGroups)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_schedule_group_schedule");

            entity.HasOne(d => d.User).WithMany(p => p.ScheduleGroups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_schedule_group_user");
        });

        modelBuilder.Entity<SchedulePreview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83F7E1C5E22");

            entity.ToTable("schedule_preview");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.SchedulePreviews)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_schedule_preview_schedule");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__search_h__3213E83F20E5F268");

            entity.ToTable("search_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SearchDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("search_date");
            entity.Property(e => e.SearchKey)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("search_key");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SearchHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_search_history_user");
        });

        modelBuilder.Entity<SplitCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__split_ca__3213E83FE0ABD209");

            entity.ToTable("split_category");

            entity.HasIndex(e => e.Category, "UQ__split_ca__F7F53CC2D0519866").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValue("#000000")
                .HasColumnName("color");
            entity.Property(e => e.Icon)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<SplitExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__split_ex__3213E83FB4B4B2A2");

            entity.ToTable("split_expense");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PayerId).HasColumnName("payer_id");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.SplitCategoryId).HasColumnName("split_category_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.SplitExpenses)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_split_expense_cost_currency_category");

            entity.HasOne(d => d.Payer).WithMany(p => p.SplitExpenses)
                .HasForeignKey(d => d.PayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_split_expense_user_payer");

            entity.HasOne(d => d.Schedule).WithMany(p => p.SplitExpenses)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_split_expense_schedule");

            entity.HasOne(d => d.SplitCategory).WithMany(p => p.SplitExpenses)
                .HasForeignKey(d => d.SplitCategoryId)
                .HasConstraintName("FK_split_expense_split_category");
        });

        modelBuilder.Entity<SplitExpenseParticipant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__split_ex__3213E83FB7E48401");

            entity.ToTable("split_expense_participant");

            entity.HasIndex(e => new { e.SplitExpenseId, e.UserId }, "unique_split_expense_id_authority_user_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.IsPaid)
                .HasDefaultValue(false)
                .HasColumnName("is_paid");
            entity.Property(e => e.SplitExpenseId).HasColumnName("split_expense_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SplitExpense).WithMany(p => p.SplitExpenseParticipants)
                .HasForeignKey(d => d.SplitExpenseId)
                .HasConstraintName("FK_split_expense_participant_split_expense");

            entity.HasOne(d => d.User).WithMany(p => p.SplitExpenseParticipants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_split_expense_participant_user");
        });

        modelBuilder.Entity<Transportation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transpor__3213E83FA24AE647");

            entity.ToTable("transportation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.ScheduleDetailsId).HasColumnName("schedule_Details_id");
            entity.Property(e => e.TicketImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ticket_image_url");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.TransportationCategoryId).HasColumnName("transportation_category_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Transportations)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_transportation_cost_currency_category");

            entity.HasOne(d => d.ScheduleDetails).WithMany(p => p.Transportations)
                .HasForeignKey(d => d.ScheduleDetailsId)
                .HasConstraintName("FK_transportation_schedule_details");

            entity.HasOne(d => d.TransportationCategory).WithMany(p => p.Transportations)
                .HasForeignKey(d => d.TransportationCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transportation_transportation_category");
        });

        modelBuilder.Entity<TransportationCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transpor__3213E83FADD53877");

            entity.ToTable("transportation_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Icon)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TransportationCategories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transportation_category_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F72FB7389");

            entity.ToTable("user");

            entity.HasIndex(e => e.Phone, "IX_user_phone")
                .IsUnique()
                .HasFilter("([phone] IS NOT NULL)");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E6164FE56D53A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasDefaultValue(0)
                .HasColumnName("gender");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .HasColumnName("google_id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Photo)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Role)
                .HasDefaultValue(1)
                .HasColumnName("role");
        });

        modelBuilder.Entity<UserFavor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_fav__3213E83F64183ED0");

            entity.ToTable("user_favor");

            entity.HasIndex(e => new { e.UserId, e.FavorCategoryId }, "unique_user_id_favor_category_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FavorCategoryId).HasColumnName("favor_category_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.FavorCategory).WithMany(p => p.UserFavors)
                .HasForeignKey(d => d.FavorCategoryId)
                .HasConstraintName("FK_user_favor_favor_category");

            entity.HasOne(d => d.User).WithMany(p => p.UserFavors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_favor_user");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__votes__3213E83F19B52F7F");

            entity.ToTable("votes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Votes)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_votes_schedule");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_votes_user");
        });

        modelBuilder.Entity<VoteOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vote_opt__3213E83F9D95B816");

            entity.ToTable("vote_options");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.VoteId).HasColumnName("vote_id");
            entity.Property(e => e.WishlistDetailId).HasColumnName("wishlist_detail_id");

            entity.HasOne(d => d.Vote).WithMany(p => p.VoteOptions)
                .HasForeignKey(d => d.VoteId)
                .HasConstraintName("FK_vote_options_votes");

            entity.HasOne(d => d.WishlistDetail).WithMany(p => p.VoteOptions)
                .HasForeignKey(d => d.WishlistDetailId)
                .HasConstraintName("FK_vote_options_wishlist_detail");
        });

        modelBuilder.Entity<VoteResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vote_res__3213E83F91308F73");

            entity.ToTable("vote_result");

            entity.HasIndex(e => new { e.VoteId, e.VoteOptionId, e.UserId }, "unique_vote_id_vote_option_id_user_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VoteId).HasColumnName("vote_id");
            entity.Property(e => e.VoteOptionId).HasColumnName("vote_option_id");
            entity.Property(e => e.VotedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("voted_at");

            entity.HasOne(d => d.User).WithMany(p => p.VoteResults)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vote_result_user");

            entity.HasOne(d => d.Vote).WithMany(p => p.VoteResults)
                .HasForeignKey(d => d.VoteId)
                .HasConstraintName("FK_vote_result_votes");

            entity.HasOne(d => d.VoteOption).WithMany(p => p.VoteResults)
                .HasForeignKey(d => d.VoteOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vote_result_vote_options");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__wishlist__3213E83FC5862F3F");

            entity.ToTable("wishlist");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wishlists_user");
        });

        modelBuilder.Entity<WishlistDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__wishlist__3213E83F3B25E9BB");

            entity.ToTable("wishlist_detail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.GooglePlaceId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("google_place_id");
            entity.Property(e => e.LocationCategoryId).HasColumnName("location_category_id");
            entity.Property(e => e.LocationLat)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("location_lat");
            entity.Property(e => e.LocationLng)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("location_lng");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.WishlistId).HasColumnName("wishlist_id");

            entity.HasOne(d => d.LocationCategory).WithMany(p => p.WishlistDetails)
                .HasForeignKey(d => d.LocationCategoryId)
                .HasConstraintName("FK_wishlist_detail_location_category");

            entity.HasOne(d => d.Wishlist).WithMany(p => p.WishlistDetails)
                .HasForeignKey(d => d.WishlistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wishlist_detail_wishlists");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}