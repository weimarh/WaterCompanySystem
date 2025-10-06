namespace Domain.Payments
{
    public interface IPaymentRepository
    {
        Task<IReadOnlyList<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(PaymentId id);
        Task<bool> ExistsAsync(PaymentId id);
        Task Add(Payment payment);
        void Update(Payment payment);
        void Delete(Payment payment);
    }
}
