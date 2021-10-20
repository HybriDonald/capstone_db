﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbLogin
{
    interface IDatabase
    {
        void Execute(string query);
    }

    interface IInformation
    {
        void Print();
    }
}
