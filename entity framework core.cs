using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace projedeneme
{

//entity  class



public class ShopContext:DbContext{

public DbSet<Product>  Products { get; set; }
public DbSet<Category>  Categorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shopdb.sqlite");
        }

     

}
public class Product{

[Key]
public int Id { get; set; }

[MaxLength(100)]// bunlar altındaki propun özellikleri veritabanındaki özellikleri belirtiliyor
[Required]
public string Name { get; set; }

public decimal Price { get; set; }

}


public class Category{
     

     public int Id { get; set; }

     public string Name { get; set; }
}




    class Program
    {
        static void Main(string[] args)
        {

// ekleme işlemi
/*
           using (var db = new ShopContext())//context bağlantısı
            {

                var p = new Product { Name = "xiami mi a3", Price = 3500 };//ürün oluşturuyoruz 


                db.Products.Add(p);//oluşturulan nesne ekleniyor 
                db.SaveChanges();
            
            }*/

//veri alma işlemi
using(var context=new ShopContext()){

var products=context.Products
.Select(prd=>new{//istenen sütunlar çekilir sadece
    prd.Name,
    prd.Price,
})
.ToList();

foreach (var item in products)
{
    Console.WriteLine($"  name:{item.Name} price: {item.Price}");
}



//  id ye göre ürün çekmek
Console.WriteLine("****************");
var id=2;
var prod=context.Products.Where(p=> p.Id==id ).FirstOrDefault();

 Console.WriteLine($"  name:{prod.Name} price: {prod.Price}");



// fiyat hsabına göre
Console.WriteLine("****************");

var pro=context.Products.Where(p=> p.Price<2000 ).ToList();
foreach (var item in pro)
{
    Console.WriteLine($"  name:{item.Name} price: {item.Price}");
}
// güncelleme işlemi
var id2=5;
var prox=context.Products.Where(p=> p.Id==id2 ).FirstOrDefault();// direk satır seçilmiş obje alınır


if (prox!=null){
    prox.Price*=1.2m;//alınan objeyi değiştirmek yaterli
    context.SaveChanges();// arkasından kaydedilmeli
     Console.WriteLine(" güncellenmiş fiyat: ");
}

//başka bir güncelleme işlemi
/*
using(var db=new ShopContext() ){

    var entity =new Product() {Id=1};
    db.products.Attach(entity);
    entity.Price=800;
    db.SaveChanges();

} 
*/


// silme işlemi
using(var db = new ShopContext() ){
int id=3;
var p=db.Products.FirstOrDefault(i=> i.Id==id);

db.Products.Remove(p);
db.SaveChanges();
}



        }
    }
}
