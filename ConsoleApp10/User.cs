using System;
using System.Collections.Generic;

namespace ConsoleApp10;

public partial class User
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PatronomicName { get; set; }

    public long Age { get; set; }

    public string? Role { get; set; }

    public string Account { get; set; } = null!;

    public decimal Balance { get; set; }
}
