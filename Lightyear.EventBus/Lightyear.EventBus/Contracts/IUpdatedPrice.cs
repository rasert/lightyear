using System;
namespace Lightyear.EventBus.Contracts
{
    public interface IUpdatedPrice
    {
        int ProductID { get; set; }
        decimal NewValue { get; set; }
    }
}
