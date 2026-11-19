using System;
using System.Collections;
using System.Collections.Generic;

namespace Client.Models;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string ContactPerson { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Email { get; set; } = "";

    public ICollection<Ingredients> Ingredients { get; set; }
        = new List<Ingredients>();
}
