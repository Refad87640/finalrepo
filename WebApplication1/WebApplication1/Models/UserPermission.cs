﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class UserPermission
    {
        public UserPermission()
        {
               }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }

        }
}
