using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.Infrastructure.Data;
public sealed partial class YomikazeDbInitializer
{
    private static partial class DefaultData
    {
        public static readonly Comic[] Comics = new Comic[] {
            new() {
                Name = "Naruto",
                Authors = "Masashi Kishimoto",
                Description = " It tells the story of Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the leader of his village.",
                Cover = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=Naruto&color=fffBCDA&font=raleway",
            },
            new ()
            {
                Name = "One Piece",
                Authors = "Eiichiro Oda",
                Description = "The story follows the adventures of Monkey D. Luffy, a boy whose body gained the properties of rubber after unintentionally eating a Devil Fruit. With his pirate crew, the Straw Hat Pirates, Luffy explores the Grand Line in search of the deceased King of the Pirates Gol D. Roger's ultimate treasure known as the \"One Piece\" in order to become the next King of the Pirates.",
                Cover = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=OnePiece&color=fffBCDA&font=raleway",
            }
        };

        public static readonly Chapter[] Chapters = new Chapter[]
        {
            new()
            {
                Comic = Comics[0],
                Title = "Chapter 1",
                Index = 0,
            },
            new()
            {
                Comic = Comics[0],
                Title = "Chapter 2",
                Index = 1,
            },
            new()
            {
                Comic = Comics[0],
                Title = "Chapter 3",
                Index = 2,
            },
            new()
            {
                Comic = Comics[0],
                Title = "Chapter 4",
                Index = 3,
            },
            new()
            {
                Comic = Comics[0],
                Title = "Chapter 5",
                Index = 4,
            }
        };

        public static readonly Page[] Pages = new Page[]
        {
            new()
            {
                Index = 0,
                Server = 0,
                Image = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=Page%201&color=fffBCDA&font=raleway",
                Chapter = Chapters[0],
            },
            new()
            {
                Index = 1,
                Server = 0,
                Image = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=Page%202&color=fffBCDA&font=raleway",
                Chapter = Chapters[0],
            },
            new()
            {
                Index = 2,
                Server = 0,
                Image = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=Page%203&color=fffBCDA&font=raleway",
                Chapter = Chapters[0],
            },
            new()
            {
                Index = 3,
                Server = 0,
                Image = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=Page%204&color=fffBCDA&font=raleway",
                Chapter = Chapters[0],
            },
            new()
            {
                Index = 4,
                Server = 0,
                Image = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=Page%205&color=fffBCDA&font=raleway",
                Chapter = Chapters[0],
            },
            new()
            {
                Index = 5,
                Server = 0,
                Image = "https://placehold.co/800x1200/FFFFFF/000000/png/?text=Page%206&color=fffBCDA&font=raleway",
                Chapter = Chapters[0],
            },
        };
    }
}
