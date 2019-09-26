using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels
{
    interface ICrudDAO<T>
    {
        bool Add(T model);

        List<T> Load();

        T Get(int id);

        bool Change(int id, T model);

        bool Remove(int id);
    }
}
