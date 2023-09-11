

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

#region Default Convention
// iki entity arasındaki ilişkiyi proplar coğul reflerle yapılır (ICollection List)
// cross table i efcore yapıyor 
// cross tableın primary keyi composite pm olur 
//class Kitap
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Yazar> Yazarlar { get; set; }

//}
//class Yazar
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }
//}



#endregion
#region Data Annotations 
// cross table ı manuel yazman lazım 
// composite primary key fluent apide yazılır 
// cross table dbset prop u oluşturmaya gerek yok
class Kitap
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }

}
class KitapYazar
{
    
    public int KitapId { get; set; }

    public int YazarId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}
class Yazar
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }
}



#endregion


class EKitapDbContext :DbContext
{
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-2IG9GVD\\SQLEXPRESS;Database=One2Many;Trusted_Connection=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId });
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky => new { ky.KitapId, ky.YazarId });

    //    modelBuilder.Entity<KitapYazar>()
    //        .HasOne(ky => ky.Kitap)
    //        .WithMany(k => k.Yazarlar)
    //        .HasForeignKey(ky => ky.KitapId);

    //    modelBuilder.Entity<KitapYazar>()
    //        .HasOne(ky => ky.Yazar)
    //        .WithMany(y => y.Kitaplar)
    //        .HasForeignKey(ky => ky.YazarId);
    //}
}