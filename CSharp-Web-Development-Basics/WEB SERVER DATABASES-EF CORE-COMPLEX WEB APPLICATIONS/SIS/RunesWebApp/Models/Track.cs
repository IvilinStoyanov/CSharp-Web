using System.Collections.Generic;

namespace RunesWebApp.Models
{
    public class Track : BaseModel<string>
    {
        public Track()
        {
            this.Tracks = new HashSet<TrackAlbum>();
        }

        public string Name { get; set; }

        public string Link { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<TrackAlbum> Tracks { get; set; }
    }
}
