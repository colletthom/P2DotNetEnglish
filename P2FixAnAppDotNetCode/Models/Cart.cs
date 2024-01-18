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
        /// <returns>Return the actual cartline list, type: List</returns>
        public List<CartLine> cartLines;
        private List<CartLine> GetCartLineList()
        {

            //return new List<CartLine>();
            /* I modified this method because it always resets the command to 0*/
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

            int numberProductAlreadyOrdered = cartLines.Count;
            bool productPresent = false;
            int productIndexPesent=0;

            for (int i = 0; i < numberProductAlreadyOrdered; i++)
            {
                if (cartLines[i].Product.Id == product.Id)
                {
                    productPresent = true;
                    productIndexPesent = i;
                }
            }

            if (productPresent == true)
            {
                cartLines[productIndexPesent].Quantity += 1;
            }
            else
            {
                CartLine line = new CartLine
                {
                    OrderLineId = numberProductAlreadyOrdered,
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
            double totalOrder = 0;
            for (int i = 0; i<cartLines.Count; i++)
                totalOrder += (cartLines[i].Product.Price * cartLines[i].Quantity);
            return totalOrder;
            //return 0.0;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // DONE implement the method
            double averagePriceEachItem = 0.00;
            int numberItems = 0;
            for (int i = 0; i < cartLines.Count; i++)
                numberItems += cartLines[i].Quantity;

            if (numberItems > 0)
                averagePriceEachItem = GetTotalValue() / numberItems;
            return averagePriceEachItem;
            //return 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // DONE implement the method
            Product productWhoseIdFound = null;
            for (int i = 0; i < cartLines.Count; i++)
            {
                if (cartLines[i].Product.Id == productId)
                {
                    productWhoseIdFound = cartLines[i].Product;
                    break;
                }
            }
            return productWhoseIdFound;
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
