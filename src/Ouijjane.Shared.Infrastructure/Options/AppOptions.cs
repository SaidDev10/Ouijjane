﻿using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;

public class AppOptions : IOptionsRoot
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = "Ouijjane";
}
