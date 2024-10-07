#nullable disable
using System;
using System.Collections.Generic;

namespace MVCDemo.Models;

public partial class PersonList
{
    public int Id { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime? HireDate { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string Discriminator { get; set; }

}