﻿using P2FixAnAppDotNetCode.Models.Repositories;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using System.Linq;
using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        //public Product[] GetAllProducts()
        public List<Product> GetAllProducts()
        {
            // DONE change the return type from array to List<T> and propagate the change
            // thoughout the application
            return _productRepository.GetAllProducts().ToList();
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            // DONE implement the method
            var listeDesProduits = GetAllProducts();
            Product result = null;
            for (int i = 0; i < listeDesProduits.Count; i++)
            {
                if (listeDesProduits[i].Id == id)
                    result = listeDesProduits[i];
            }
            return result;
            //return null;
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            // DONE implement the method
            
            for (int i = 0; i< cart.cartLines.Count(); i++)
            {
                _productRepository.UpdateProductStocks(cart.cartLines[i].Product.Id, cart.cartLines[i].Quantity);
            }
            //var a = _productRepository; //test pour vérifier que le stock baisse
            //var b = _productRepository;
            // update product inventory by using _productRepository.UpdateProductStocks() method.
        }
    }
}
