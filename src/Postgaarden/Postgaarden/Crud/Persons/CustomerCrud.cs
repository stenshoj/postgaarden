﻿using Postgaarden.Model.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgaarden.Crud.Persons
{
    //Created by Jens Kloster
    public abstract class CustomerCrud: Crud <Customer, string>
    {
        public abstract Customer Read(Booking booking);
    }
}