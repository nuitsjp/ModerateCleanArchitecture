using System;

namespace AdventureWorks
{
    public class SalesOrderDetail
    {
        public SalesOrderDetail(
            SalesOrderDetailKey key,
            string carrierTrackingNumber, 
            short orderQty, 
            int productId, 
            int specialOfferId,
            decimal unitPrice,
            decimal unitPriceDiscount,
            decimal lineTotal, 
            DateTime modifiedDate)
        {
            Key = key;
            CarrierTrackingNumber = carrierTrackingNumber;
            OrderQty = orderQty;
            ProductId = productId;
            SpecialOfferId = specialOfferId;
            UnitPrice = unitPrice;
            UnitPriceDiscount = unitPriceDiscount;
            LineTotal = lineTotal;
            ModifiedDate = modifiedDate;
        }

        public SalesOrderDetailKey Key { get; }
        public string CarrierTrackingNumber { get; }
        public short OrderQty { get; }
        public int ProductId { get; }
        public int SpecialOfferId { get; }
        public decimal UnitPrice { get; }
        public decimal UnitPriceDiscount { get; }
        public decimal LineTotal { get; }
        public DateTime ModifiedDate { get; }
    }
}
