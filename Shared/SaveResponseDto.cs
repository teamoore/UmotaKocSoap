﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmotaKocSoap.Shared
{
    public class SaveResponseDto
    {
        public string externalCode { get; set; }
        public int Status { get; set; }
        public string ErrorText { get; set; }
    }
}
