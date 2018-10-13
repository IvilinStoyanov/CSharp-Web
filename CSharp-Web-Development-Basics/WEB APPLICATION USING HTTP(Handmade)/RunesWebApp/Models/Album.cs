using System.Collections.Generic;

namespace RunesWebApp.Models
{
    public class Album : BaseModel<string>
    {
        public Album()
        {
            this.Albums = new HashSet<TrackAlbum>();
        }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<TrackAlbum> Albums { get; set; }
    }
}
