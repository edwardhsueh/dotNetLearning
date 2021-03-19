using System;
using Edward.Shared;
namespace Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // using is important

            using (var db = new EdwardDb()){
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

    }
}
