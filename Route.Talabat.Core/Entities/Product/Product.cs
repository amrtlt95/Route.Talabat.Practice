using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int BrandId{ get; set; } // foreign Key
        public virtual ProductBrand Brand { get; set; } = null!;//navigational property
        public int CategoryId { get; set; } // foreign key
        public virtual ProductCategory Category { get; set; } = null!; // navigational property
    }
}
