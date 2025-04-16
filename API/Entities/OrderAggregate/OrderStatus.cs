namespace API.Entities.OrderAggregate;

public enum OrderStatus
{
  Pending, // Order has been created but not yet processed
           // AwaitingPayment, // Order is awaiting payment confirmation
  PaymentReceived, // Payment has been received for the order
  PaymentFailed, // Payment has failed for the order
  PaymentMismatch, // Payment amount does not match the order total
                   // AwaitingShipping, // Order is awaiting shipping
                   // Shipped, // Order has been shipped to the customer
                   // Delivered, // Order has been delivered to the customer
                   // Cancelled // Order has been cancelled
}
