using RunesWebApp.Data;
using RunesWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace RunesWebApp.Services
{
    public class AlbumService
    {
        public Album GetAlbum(int id)
        {
            using (var context = new RunesDbContext())
            {
                var album = context
                    .Albums
                    .Find(id);

                return album;
            }
        }

        public bool ContainsAlbum(string name, string cover)
        {
            using (var context = new RunesDbContext())
            {
                var isExist = context
                    .Albums
                    .Any(a => a.Name == name);

                return isExist;
            }
        }

        public void AddAlbum(string name, string cover)
        {
            using (var context = new RunesDbContext())
            {
                var album = new Album()
                {
                    Name = name,
                    Cover = cover
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }
        }

        public List<Album> GetAllAlbums()
        {
            using (var context = new RunesDbContext())
            {
                var allAlbums = context.Albums.ToList();

                return allAlbums;
            }
        }

        public decimal GetTotalPrice(int id)
        {
            using (var context = new RunesDbContext())
            {
                var totalPrice = context
                                   .Tracks
                                   .Where(t => t.AlbumId == id)
                                   .Sum(t => (t.Price - t.Price * 0.13m));

                return totalPrice;
            }
        }
    }
}
