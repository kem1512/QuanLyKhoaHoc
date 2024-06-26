﻿namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class LearningProgressMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RegisterStudyId { get; set; }

        public int CurrentSubjectId { get; set; }
    }

    public class LearningProgressQuery : QueryModel
    {
        public int? RegisterStudyId { get; set; }
    }

    public class LearningProgressCreate
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RegisterStudyId { get; set; }

        public int CurrentSubjectId { get; set; }
    }

    public class LearningProgressUpdate
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RegisterStudyId { get; set; }

        public int CurrentSubjectId { get; set; }
    }
}
