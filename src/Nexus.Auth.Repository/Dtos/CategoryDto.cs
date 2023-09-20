﻿using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos;

public class CategoryDto
{
    [Required]
    public string Name { get; set; }
}

public class CategoryPutDto : CategoryDto
{
    [Required]
    public int Id { get; set; }
}