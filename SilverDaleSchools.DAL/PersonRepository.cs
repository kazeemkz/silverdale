﻿using SilverDaleSchools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverDaleSchools.DAL
{
    public class PersonRepository : GenericRepository<Person>
    {
        public PersonRepository(sdContext context)
            : base(context)
        {

        }
    }
}
