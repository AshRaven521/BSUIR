using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_953506_KONDRASHOV.Entities;

namespace WEB_953506_KONDRASHOV.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }

        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }

        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }

        public int Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Engine.Price);
            }
        }

        public virtual void AddToCart(Engine engine)
        {
            if (Items.ContainsKey(engine.EngineId))
            {
                Items[engine.EngineId].Quantity++;
            }
                
            else
            {
                Items.Add(engine.EngineId, new CartItem
                {
                    Engine = engine,
                    Quantity = 1
                });
            }
                

        }

        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }

    public class CartItem
    {
        public Engine Engine { get; set; }
        public int Quantity { get; set; }
    }
}
