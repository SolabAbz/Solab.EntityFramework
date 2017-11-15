using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solab.EntityFramework.Tests.Helpers
{
    public static class TextWriterHelpers
    {
        public static IndentedTextWriter Create()
        {
            return new IndentedTextWriter(new StringWriter());
        }
    }
}
