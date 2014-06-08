using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Tests
{
    public static partial class AssertEx
    {
        public static void AssertThrows<T>(Action action, string message)
            where T : Exception
        {
            try
            {
                action();
                Assert.Fail("Should have thrown exception!" + Environment.NewLine + message);
            }
            catch (T)
            {
                // ignore - it is expected
            }
            catch
            {
                throw;
            }
        }
    }
}
