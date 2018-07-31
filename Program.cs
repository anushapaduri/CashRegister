using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<item> listItem = new List<item>();

            CashRegister cashRegister = new CashRegister();

            //for each scan prepare the item object and read the input values
            //call AddItem methond to add to teh list 
            item itemAdd = new item();
            itemAdd.id = 1;
            itemAdd.name = "Item1";
            itemAdd.quantity = 1;
            itemAdd.weight = 1;
            itemAdd.DiscType = null;
            cashRegister.AddItem(listItem, itemAdd);

            //call RemoveItem methond if a scanned item has to be removed from the list
            item itmRemove = new item();
            itmRemove.id = 1;
            itmRemove.name = "Item1";
            itmRemove.quantity = 1;
            itmRemove.weight = 1;
            itmRemove.DiscType = null;
            cashRegister.AddItem(listItem, itmRemove);

            if (listItem.Count > 0)
            {
                Console.WriteLine("Item         Price");
                foreach (item itm in listItem)
                {
                    Console.WriteLine(itm.name + "          " + itm.price);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Total: " + "          " + cashRegister.GetTotal(listItem));
        }
    }

    class CashRegister
    {
        public List<item> AddItem(List<item> listItem, item itm)
        {
            listItem.Add(itm);
            return listItem;
        }

        public List<item> RemoveItem(List<item> listItem, item itm)
        {
            listItem.Remove(itm);
            return listItem;
        }

        public decimal GetTotal(List<item> listItem)
        {
            decimal total = 0m;

            foreach(item itm in listItem)
            {
                if (itm.PricType == PriceType.TotalItemPrice)
                {
                    if (itm.DiscType == null)
                    {
                        total += itm.price;
                    }
                    else
                    {
                        Discount disc = itm.Disc;
                        if (itm.DiscType == DiscountType.DiscPercent)
                        {
                            total += Convert.ToDecimal(disc.BuyN * (itm.price - (itm.price * (disc.Percentage / 100))));
                        }
                        else
                        {
                            total += Convert.ToDecimal(itm.quantity * itm.price);
                            //'GetN' number of items would have to be removed from the inventory
                            //conditional though, based on weather GetN is the number of items getting free or total number of items including BuyN
                        }
                    }
                }
                else
                {
                    if (itm.DiscType == null)
                    {
                        if (itm.weight == null)
                        {
                            total += Convert.ToDecimal(itm.quantity * itm.price);
                        }
                        if (itm.quantity == null)
                        {
                            total += Convert.ToDecimal(itm.weight * itm.price);
                        }
                    }
                    else
                    {
                        Discount disc = itm.Disc;
                        if (itm.DiscType == DiscountType.DiscPercent)
                        {
                            if (itm.weight == null)
                            {
                                total += Convert.ToDecimal(itm.quantity * disc.BuyN * (itm.price - (itm.price * (disc.Percentage / 100))));
                            }
                            if (itm.quantity == null)
                            {
                                total += Convert.ToDecimal(itm.weight * disc.BuyN * (itm.price - (itm.price * (disc.Percentage / 100))));
                            }                            
                        }
                        else
                        {
                            if (itm.weight == null)
                            {
                                total += Convert.ToDecimal(itm.quantity * itm.price);
                            }
                            if (itm.quantity == null)
                            {
                                total += Convert.ToDecimal(itm.weight * itm.price);
                            }
                            //'GetN' number of items would have to be removed from the inventory
                            //conditional though, based on weather GetN is the number of items getting free or total number of items including BuyN
                        }
                    }
                }
            }

            return total;
        }
    }

    public class item
    {
        public int id
        {
            get;
            set;
        }
        
        public string name
        {
            get;
            set;
        }

        public decimal? quantity
        {
            get;
            set;
        }

        public decimal? weight
        {
            get;
            set;
        }

        public PriceType? PricType
        {
            get;
            set;
        }

        public decimal price
        {
            get;
            set;
        }

        public DiscountType? DiscType
        {
            get;
            set;
        }

        public Discount Disc
        {
            get;
            set;
        }
    }

    enum PriceType
    {
        TotalItemPrice,
        PerUnitPrice
    }

    enum DiscountType
    {
        DiscBuyNGetN,
        DiscPercent
    }

    public class Discount
    {
        public int BuyN
        {
            get;
            set;
        }

        public int GetN
        {
            get;
            set;
        }

        public decimal? Percentage
        {
            get;
            set;
        }
    }

    public class DiscBuyNGetN
    {
        public int BuyN
        {
            get;
            set;
        }
    }

    public class DiscPercent
    {
        public double Percentage
        {
            get;
            set;
        }
    }
}
