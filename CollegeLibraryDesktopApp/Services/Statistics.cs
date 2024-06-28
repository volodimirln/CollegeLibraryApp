using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace CollegeLibraryDesktopApp.Services
{
    public class Statistics
    {
        public int[]  GetAllIssuedStatistics(int allbooks, int issued)
        {
            int remainder = allbooks - issued;
            int[] statistics = new int[] { allbooks, issued, remainder};
            return statistics;
        }

        public int[] GetAllRemainderStatistics(int allbooks, int remainder)
        {
            int issued = allbooks - remainder;
            int[] statistics = new int[] { allbooks, issued, remainder };
            return statistics;
        }
        public int[] GetIssuedRemainderStatistics(int issued, int remainder)
        {
            int allbooks = issued + remainder;
            int[] statistics = new int[] { allbooks, issued, remainder };
            return statistics;
        }
        public string GetAllStatistics(int allbooks, int issued)
        {
            int remainder = allbooks - issued;
            return $"Выдано книг - {issued}, всего книг - {allbooks}, остаток - {remainder}";
        }
        public bool CheckAllIssuedRemainderStatistics(int allbooks, int issued, int remainder)
        {
            bool result = false;
            if ((issued + remainder) == allbooks)
            {
                result = true;
            }
            return result;
        }
    }
}
