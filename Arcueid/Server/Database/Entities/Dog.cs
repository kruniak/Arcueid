﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcueid.Server.Database.Entities;

public class Dog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    [ForeignKey("DogBreedId")]
    public virtual DogBreed Breed { get; set; }
}

