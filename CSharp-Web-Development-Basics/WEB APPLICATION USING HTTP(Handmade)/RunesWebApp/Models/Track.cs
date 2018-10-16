﻿using System.Collections.Generic;

namespace RunesWebApp.Models
{
    public class Track : BaseModel<int>
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public decimal Price { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
