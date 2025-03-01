using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.RequestService.EditStatus
{
    public interface IEditRequestStatusService
    {
        Task<Request?> EditStatus(int id, RequestStatus status);
    }
}