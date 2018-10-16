using RunesWebApp.Data;
using RunesWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace RunesWebApp.Services
{
    public class TrackService
    {
        public void AddTrack(string name, string link, decimal price, int albumId)
        {
            using (var context = new RunesDbContext())
            {
                var track = new Track()
                {
                    Name = name,
                    Link = link,
                    Price = price,
                    AlbumId = albumId
                };

                context.Tracks.Add(track);
                context.SaveChanges();
            }
        }

        public bool ContainsTrack(string name)
        {
            using (var context = new RunesDbContext())
            {
                var isExist = context
                    .Tracks
                    .Any(a => a.Name == name);

                return isExist;
            }
        }

        public Track GetTrack(int trackId)
        {
            using (var context = new RunesDbContext())
            {
                var track = context
                    .Tracks
                    .Find(trackId);

                return track;
            }
        }

        public List<Track> GetAllTracks(int albumId)
        {
            using (var context = new RunesDbContext())
            {
                var allTracks = context
                    .Tracks
                    .Where(t => t.AlbumId == albumId)
                    .ToList();

                return allTracks;
            }
        }
    }
}

