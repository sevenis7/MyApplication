using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.RequestService.EditStatus
{
    public interface IEditStatusService
    {
        Task<Request?> EditStatus(int id, RequestStatus status);
    }
}