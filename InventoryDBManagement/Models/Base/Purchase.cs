using InventoryDBManagement.DAL;
using InventoryManagement.Models.Out;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Models.Base
{
    public class PurchaseBase
    {
        public PurchaseBase() { }
        public PurchaseBase(PurchaseBase rhs)
        {
            ID = rhs.ID;
            VendorID = rhs.VendorID;
            TotalBuyingPrice = rhs.TotalBuyingPrice;
        }

        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [JsonProperty]
        public int VendorID { get; set; }

        [JsonProperty]
        [Required]
        public double TotalBuyingPrice { get; set; }

    }

    public class PurchaseDTO : PurchaseBase
    {

        public PurchaseDTO() { }
        public PurchaseDTO(PurchaseIn In)
            : base(In)
        {
            ProductIDs = In.ProductIDs;
            ProductQuantities = In.ProductQuantities;
            ProductBuyingPrices = In.ProductBuyingPrices;
        }

        [JsonProperty]
        public string ProductIDs { get; set; }

        [JsonProperty]
        public string ProductQuantities { get; set; }

        [JsonProperty]
        public string ProductBuyingPrices { get; set; }
    }

    public class PurchaseIn : PurchaseBase
    {
        [JsonProperty]
        public string ProductIDs { get; set; }

        [JsonProperty]
        public string ProductQuantities { get; set; }

        [JsonProperty]
        public string ProductBuyingPrices { get; set; }
    }

    public class ProductPurchaseDetails
    {
        public ProductPurchaseDetails(ProductOut productOut, int quantity, double buyingPrice)
        {
            Product = productOut;
            Quantity = quantity;
            BuyingPrice = buyingPrice;
        }

        [JsonProperty]
        public ProductOut Product;

        [JsonProperty]
        public int Quantity;

        [JsonProperty]
        public double BuyingPrice;
    }

    public class PurchaseOut : PurchaseBase
    {

        public PurchaseOut(InventoryDBContext context, PurchaseDTO dto)
            : base(dto)
        {
            this.ProductDetails = new List<ProductPurchaseDetails>();

            // products
            string[] productIDs = dto.ProductIDs.Split(',');
            string[] productQuantities = dto.ProductQuantities.Split(',');
            string[] productBuyingPrices = dto.ProductBuyingPrices.Split(',');
            int numProducts = productIDs.Length;
            for (int i = 0; i < numProducts; ++i)
            {
                int id = int.Parse(productIDs[i]);
                ProductOut productOut = new ProductOut(context, context.GetProduct(id));
                int Quantity = int.Parse(productQuantities[i]);
                double BuyingPrice = double.Parse(productBuyingPrices[i]);

                this.ProductDetails.Add(new ProductPurchaseDetails(productOut, Quantity, BuyingPrice));
            }

        }

        [JsonProperty]
        public List<ProductPurchaseDetails> ProductDetails { get; set; }

    }

}
