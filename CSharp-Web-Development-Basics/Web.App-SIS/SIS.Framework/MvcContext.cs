using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Framework
{
    public class MvcContext
    {
        private static MvcContext Instance;

        private MvcContext() { }

        public static MvcContext Get => Instance ?? (Instance = new MvcContext());

        public string AssemblyName { get; set; }

        public string ControllerFolder { get; set; } = "Controllers";

        public string ControllerSuffix { get; set; } = "Controller";

        public string ViewsFolder { get; set; } = "Views";

        public string ModelFolder { get; set; } = "Models";
    }
}
