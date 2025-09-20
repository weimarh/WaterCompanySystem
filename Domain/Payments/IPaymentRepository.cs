namespace Domain.Payments
{
    public interface IPaymentRepository
    {
        Task<IReadOnlyList<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(PaymentId id);
        Task<bool> ExistsAsync(PaymentId id);
        Task Add(Payment customer);
        Task Update(Payment customer);
        Task Delete(Payment customer);
    }
}
