using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ghasedak.Core.Models.Results;

namespace Ghasedak.Core.Interfaces
{
    public interface IContactService
    {
        Task<GroupResult> AddGroupAsync(string name, int parent);
        Task<ApiResult> RemoveGroupAsync(int groupid);
        Task<ApiResult> EditGroupAsync(int groupid, string name);
        Task<ApiResult> AddNumberToGroupAsync(int groupid, string[] number, string[] firstname = null, string[] lastname = null, string[] email=null);
        Task<GroupListResult> GroupListAsync(int parent = 0);
        Task<GroupNumbersResult> GroupNumbersListAsync(int groupid, int page = 1, int offset = 100);
    }
}
