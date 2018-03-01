﻿namespace LearningSystem.Data.Models
{
    using LearningSystem.Web.Models;

    public class StudentCourse
    {
        public string StudentId { get; set; }

        public User Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
