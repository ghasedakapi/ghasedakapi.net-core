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
        Task<GroupResult> AddGroup(string name, int parent);
        Task<ApiResult> RemoveGroup(int groupid);
        Task<ApiResult> EditGroup(int groupid, string name);
        Task<ApiResult> AddNumberToGroup(int groupid, string[] number, string[] firstname = null, string[] lastname = null, string[] email=null);
        Task<GroupListResult> GroupList(int parent = 0);
        Task<GroupNumbersResult> GroupNumbersList(int groupid, int page = 1, int offset = 100);
    }
}
