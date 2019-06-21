using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoanMgntAPI.Models
{
    public partial class LoanManagementContext : DbContext
    {
        public LoanManagementContext()
        {
        }

        public LoanManagementContext(DbContextOptions<LoanManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgentLoan> AgentLoan { get; set; }
        public virtual DbSet<AgentLoanTxn> AgentLoanTxn { get; set; }
        public virtual DbSet<AreaMaster> AreaMaster { get; set; }
        public virtual DbSet<CityMaster> CityMaster { get; set; }
        public virtual DbSet<CustomerLoan> CustomerLoan { get; set; }
        public virtual DbSet<CustomerLoanDocumentDtl> CustomerLoanDocumentDtl { get; set; }
        public virtual DbSet<CustomerLoanTxn> CustomerLoanTxn { get; set; }
        public virtual DbSet<DocumentTypeMaster> DocumentTypeMaster { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplate { get; set; }
        public virtual DbSet<Link> Link { get; set; }
        public virtual DbSet<LoanTypeMaster> LoanTypeMaster { get; set; }
        public virtual DbSet<LoginCredentials> LoginCredentials { get; set; }
        public virtual DbSet<ModuleMaster> ModuleMaster { get; set; }
        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<RoleRight> RoleRight { get; set; }
        public virtual DbSet<StateMaster> StateMaster { get; set; }
        public virtual DbSet<TicketDetail> TicketDetail { get; set; }
        public virtual DbSet<TicketMaster> TicketMaster { get; set; }
        public virtual DbSet<WalletMaster> WalletMaster { get; set; }
        public virtual DbSet<WalletTransaction> WalletTransaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=WS-SRV-NET;Database=LoanManagement;User ID=sa;Password=Admin@net;persist security info=True;Connect Timeout=200;Max Pool Size=200;Min Pool Size=5;Pooling=true;Connection Lifetime=300");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentLoan>(entity =>
            {
                entity.Property(e => e.AgentLoanId).HasColumnName("AgentLoanID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.InitiateDate).HasColumnType("date");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateInt).HasComputedColumnSql("((datepart(year,[UpdatedDate])*(10000)+datepart(month,[UpdatedDate])*(100))+datepart(day,[UpdatedDate]))");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentLoan)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentLoan_LoginCredentials");
            });

            modelBuilder.Entity<AgentLoanTxn>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.LoanId).HasColumnName("LoanID");

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.Property(e => e.ReceiptDate).HasColumnType("date");

                entity.Property(e => e.ReceiptNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.AgentLoanTxn)
                    .HasForeignKey(d => d.LoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentLoanTxn_AgentLoan");
            });

            modelBuilder.Entity<AreaMaster>(entity =>
            {
                entity.HasKey(e => e.AreaId);

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");
            });

            modelBuilder.Entity<CityMaster>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityId)
                    .HasColumnName("CityID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CityName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.City)
                    .WithOne(p => p.InverseCity)
                    .HasForeignKey<CityMaster>(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_MST_City_MST");
            });

            modelBuilder.Entity<CustomerLoan>(entity =>
            {
                entity.Property(e => e.CustomerLoanId).HasColumnName("CustomerLoanID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.InstallmentAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Interest).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LoanTypeId).HasColumnName("LoanTypeID");

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PenaltyAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateInt).HasComputedColumnSql("((datepart(year,[UpdatedDate])*(10000)+datepart(month,[UpdatedDate])*(100))+datepart(day,[UpdatedDate]))");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CustomerLoan)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLoan_AgentLoginCredentials");

                entity.HasOne(d => d.LoanType)
                    .WithMany(p => p.CustomerLoan)
                    .HasForeignKey(d => d.LoanTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLoan_LoanTypeMaster");
            });

            modelBuilder.Entity<CustomerLoanDocumentDtl>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.CustomerLoanId).HasColumnName("CustomerLoanID");

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UploadedDate).HasColumnType("datetime");

                entity.Property(e => e.UploadedDateInt).HasComputedColumnSql("((datepart(year,[UploadedDate])*(10000)+datepart(month,[UploadedDate])*(100))+datepart(day,[UploadedDate]))");

                entity.HasOne(d => d.CustomerLoan)
                    .WithMany(p => p.CustomerLoanDocumentDtl)
                    .HasForeignKey(d => d.CustomerLoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLoanDocumentDtl_CustomerLoan");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.CustomerLoanDocumentDtl)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLoanDocumentDtl_DocumentTypeMaster");
            });

            modelBuilder.Entity<CustomerLoanTxn>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.BankName).IsUnicode(false);

                entity.Property(e => e.ChequeNo).IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.CustomerLoanId).HasColumnName("CustomerLoanID");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.InstallmentAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.InstallmentDate).HasColumnType("date");

                entity.Property(e => e.InstallmentDateInt).HasComputedColumnSql("((datepart(year,[InstallmentDate])*(10000)+datepart(month,[InstallmentDate])*(100))+datepart(day,[InstallmentDate]))");

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.PaidDateInt).HasComputedColumnSql("((datepart(year,[PaidDate])*(10000)+datepart(month,[PaidDate])*(100))+datepart(day,[PaidDate]))");

                entity.Property(e => e.PaneltyAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.Property(e => e.ReceiptDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo).IsUnicode(false);

                entity.HasOne(d => d.CustomerLoan)
                    .WithMany(p => p.CustomerLoanTxn)
                    .HasForeignKey(d => d.CustomerLoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerLoanTxn_CustomerLoan");
            });

            modelBuilder.Entity<DocumentTypeMaster>(entity =>
            {
                entity.HasKey(e => e.DocumentTypeId);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateInt).HasComputedColumnSql("((datepart(year,[UpdatedDate])*(10000)+datepart(month,[UpdatedDate])*(100))+datepart(day,[UpdatedDate]))");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.Property(e => e.LinkId).HasColumnName("LinkID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RouteLink)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateInt).HasComputedColumnSql("((datepart(year,[UpdatedDate])*(10000)+datepart(month,[UpdatedDate])*(100))+datepart(day,[UpdatedDate]))");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Link)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Link_ModuleMaster");
            });

            modelBuilder.Entity<LoanTypeMaster>(entity =>
            {
                entity.HasKey(e => e.LoanTypeId);

                entity.Property(e => e.LoanTypeId).HasColumnName("LoanTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateInt).HasComputedColumnSql("((datepart(year,[UpdatedDate])*(10000)+datepart(month,[UpdatedDate])*(100))+datepart(day,[UpdatedDate]))");
            });

            modelBuilder.Entity<LoginCredentials>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasColumnName("EmailID")
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Profession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage).HasColumnType("text");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateInt).HasComputedColumnSql("((datepart(year,[UpdatedDate])*(10000)+datepart(month,[UpdatedDate])*(100))+datepart(day,[UpdatedDate]))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.LoginCredentials)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_LoginCredentials_CityMaster");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.LoginCredentials)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_LoginCredentials_StateMaster");
            });

            modelBuilder.Entity<ModuleMaster>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.IconName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleRight>(entity =>
            {
                entity.Property(e => e.RoleRightId).HasColumnName("RoleRightID");

                entity.Property(e => e.LinkId).HasColumnName("LinkID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Link)
                    .WithMany(p => p.RoleRight)
                    .HasForeignKey(d => d.LinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRIght_Link");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleRight)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRIght_RoleMaster");
            });

            modelBuilder.Entity<StateMaster>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StateName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TicketDetail>(entity =>
            {
                entity.HasKey(e => e.TicketDtlId);

                entity.Property(e => e.TicketDtlId).HasColumnName("Ticket_Dtl_ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketDetail)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketDetail_TicketMaster");
            });

            modelBuilder.Entity<TicketMaster>(entity =>
            {
                entity.HasKey(e => e.TicketId);

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Priority)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TicketNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WalletMaster>(entity =>
            {
                entity.HasKey(e => e.WalletId);

                entity.Property(e => e.WalletId).HasColumnName("WalletID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<WalletTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactinId);

                entity.Property(e => e.TransactinId).HasColumnName("TransactinID");

                entity.Property(e => e.Amount).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDateInt).HasComputedColumnSql("((datepart(year,[CreatedDate])*(10000)+datepart(month,[CreatedDate])*(100))+datepart(day,[CreatedDate]))");

                entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDateInt).HasComputedColumnSql("((datepart(year,[UpdatedDate])*(10000)+datepart(month,[UpdatedDate])*(100))+datepart(day,[UpdatedDate]))");

                entity.Property(e => e.WalletId).HasColumnName("WalletID");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.WalletTransaction)
                    .HasForeignKey(d => d.WalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WalletTransaction_WalletMaster");
            });
        }
    }
}
