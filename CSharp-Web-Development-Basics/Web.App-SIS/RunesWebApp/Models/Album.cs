using System.Collections.Generic;

namespace RunesWebApp.Models
{
    public class Album : BaseModel<int>
    {
        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
