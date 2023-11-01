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
            },
            new ()
            {
                Name = "One Piece",
                Authors = "Eiichiro Oda",
                Description = "The story follows the adventures of Monkey D. Luffy, a boy whose body gained the properties of rubber after unintentionally eating a Devil Fruit. With his pirate crew, the Straw Hat Pirates, Luffy explores the Grand Line in search of the deceased King of the Pirates Gol D. Roger's ultimate treasure known as the \"One Piece\" in order to become the next King of the Pirates.",
            }
        };
    }
}
