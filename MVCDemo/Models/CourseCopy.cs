﻿#nullable disable
using System;
using System.Collections.Generic;

namespace MVCDemo.Models;

public partial class CourseCopy
{
    public int CourseId { get; set; }

    public string Title { get; set; }

    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    public string Description { get; set; }

    public string Slug { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime StartDate { get; set; }

    public virtual Department Department { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<PersonList> Instructors { get; set; } = new List<PersonList>();
}