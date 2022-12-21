using BikeStoresAPI.Models;

namespace BikeStoresAPI.Interfaces
{
    public interface Istaffs_repository
    {
        Task<IEnumerable<staffs>> GetStaffs();
        Task<staffs> GetStaff(int staff_id);
        Task<staffs> AddStaff(staffs staff);
        Task<staffs> UpdateStaff(staffs staff);
        Task<staffs> DeleteStaff(int staff_id);
    }
}
