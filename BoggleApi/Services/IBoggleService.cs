using BoggleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleApi.Services
{
    public interface IBoggleService
    {
        BoggleBox GetBoggleBox();
        BoggleBox GetBoggleBox(Guid guid);
        bool CheckWordPresent(BoggleBox boggleBox, string word);


        //bool CheckWordPresent(BoggleBox boggleBox, string word);
    }
}
