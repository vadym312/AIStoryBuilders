﻿
#nullable disable
using System;
using System.Collections.Generic;

namespace AIStoryBuilders.Models;

public partial class Story
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Style { get; set; }

    public string Theme { get; set; }

    public string Synopsis { get; set; }

    public string ChapterCount { get; set; }

    public virtual ICollection<Chapter> Chapter { get; set; } = new List<Chapter>();

    public virtual ICollection<Character> Character { get; set; } = new List<Character>();

    public virtual ICollection<Location> Location { get; set; } = new List<Location>();

    public virtual ICollection<Timeline> Timeline { get; set; } = new List<Timeline>();
}