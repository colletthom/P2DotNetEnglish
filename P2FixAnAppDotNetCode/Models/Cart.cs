using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> cartLines;
        private List<CartLine> GetCartLineList()
        {

            //return new List<CartLine>();
            /* jai modifié cette méthode car elle réinitialiser toujours à 0 la commande*/
            if (cartLines == null)
            {
                cartLines = new List<CartLine>();
            }
            return cartLines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // DONE implement the method

            int nombreDeProduitsDejaCommande = cartLines.Count;
            bool produitPresent = false;
            int indexProduitPesent=0;

            for (int i = 0; i < nombreDeProduitsDejaCommande; i++)
            {
                if (cartLines[i].Product.Id == product.Id)
                {
                    produitPresent = true;
                    indexProduitPesent = i;
                }
            }

            if (produitPresent == true)
            {
                cartLines[indexProduitPesent].Quantity += 1;
            }
            else
            {
                CartLine line = new CartLine
                {
                    OrderLineId = nombreDeProduitsDejaCommande,
                    Product = product,
                    Quantity = quantity
                };
                cartLines.Add(line);
            }   
        }


        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // DONE implement the method
            double totalCommande = 0;
            for (int i = 0; i<cartLines.Count; i++)
                totalCommande += (cartLines[i].Product.Price * cartLines[i].Quantity);
            return totalCommande;
            //return 0.0;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // DONE implement the method
            double prixMoyenChaqueArticle = 0.00;
            int nombreArticles = 0;
            for (int i = 0; i < cartLines.Count; i++)
                nombreArticles += cartLines[i].Quantity;
            prixMoyenChaqueArticle = GetTotalValue() / nombreArticles;
            return prixMoyenChaqueArticle;
            //return 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // DONE implement the method
            Product produitDontIdTrouve = null;
            for (int i = 0; i < cartLines.Count; i++)
            {
                if (cartLines[i].Product.Id == productId)
                {
                    produitDontIdTrouve = cartLines[i].Product;
                    break;
                }
            }
            return produitDontIdTrouve;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
