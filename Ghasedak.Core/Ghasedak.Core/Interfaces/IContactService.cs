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
        Task<GroupResult> AddGroupAsync(string name, int parent, string dep = null);
        Task<ApiResult> RemoveGroupAsync(int groupid, string dep = null);
        Task<ApiResult> EditGroupAsync(int groupid, string name, string dep = null);
        Task<ApiResult> AddNumberToGroupAsync(int groupid, string[] number, string[] firstname = null, string[] lastname = null, string[] email=null, string dep = null);
        Task<GroupListResult> GroupListAsync(int parent = 0, string dep = null);
        Task<GroupNumbersResult> GroupNumbersListAsync(int groupid, int page = 1, int offset = 100, string dep = null);
    }
}
