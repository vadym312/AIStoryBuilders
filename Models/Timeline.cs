﻿
#nullable disable
using System;
using System.Collections.Generic;

namespace AIStoryBuilders.Models;

public partial class Timeline
{
    public int Id { get; set; }

    public int StoryId { get; set; }

    public string TimelineName { get; set; }

    public string TimelineDescription { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? StopDate { get; set; }

    public virtual ICollection<Paragraph> Paragraph { get; set; } = new List<Paragraph>();

    public virtual Story Story { get; set; }
}