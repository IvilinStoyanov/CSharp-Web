﻿using SIS.Framework.Attributes.Properties;

namespace RunesWebApp
{
    public class IndexViewModel
    {
        [NumberRange(5, 12)]
        public double Id { get; set; }

        [Regex(@"^[a-zA-Z]+$")]
        public string Username { get; set; }
    }
}
