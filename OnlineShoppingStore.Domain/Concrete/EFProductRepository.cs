using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public Product DeleteProduct(int productId)
        {
            Product DbEntry = context.Products.Find(productId);
            if (DbEntry != null)
            {
                context.Products.Remove(DbEntry);
                context.SaveChanges();
            }
            return DbEntry;
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product Dbentry = context.Products.Find(product.ProductID);
                if (Dbentry != null)
                {
                    Dbentry.Name = product.Name;
                    Dbentry.Description = product.Description;
                    Dbentry.Price = product.Price;
                    Dbentry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
    }
}

