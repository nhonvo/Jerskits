using ecommerce.Application.Common.Interfaces;

namespace ecommerce.Application.Services;

public class CurrentTime : ICurrentTime
{
    public DateTime GetCurrentTime() => DateTime.UtcNow;
}
