﻿namespace QuanLyKhoaHoc.Infrastructure.Data.Configurations
{
    public class RunTestCaseConfiguration : IEntityTypeConfiguration<RunTestCase>
    {
        public void Configure(EntityTypeBuilder<RunTestCase> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.RunTime).HasDefaultValue(0);

            builder.HasOne(c => c.DoHomework).WithMany(c => c.RunTestCases).HasForeignKey(c => c.DoHomeworkId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.TestCase).WithMany(c => c.RunTestCases).HasForeignKey(c => c.TestCaseId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
