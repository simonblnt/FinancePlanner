using FinancePlanner.ViewModels;

namespace FinancePlanner.Database
{
    public class EventRepository
    {
        public EventViewModel CreateEventViewModel()
        {
            return new EventViewModel();
        }
    }
}