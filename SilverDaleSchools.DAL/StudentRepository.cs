﻿using SilverDaleSchools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverDaleSchools.DAL
{
    public class StudentRepository : GenericRepository<Student>
    {
       public StudentRepository(sdContext context)
            : base(context)
        {

        }
    }
}
