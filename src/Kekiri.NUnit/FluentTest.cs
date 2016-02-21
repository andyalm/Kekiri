using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Kekiri.Exceptions;
using Kekiri.Impl;
using Kekiri.Reporting;
using NUnit.Framework;

namespace Kekiri
{
    [TestFixture]
    public abstract class FluentTest : FluentBase
    {
        [Test]
        public override void RunScenario()
        {
            base.RunScenario();
        }
    }

    public abstract class FluentTest<TContext> : FluentTest where TContext : new()
    {
        protected override object CreateContextObject()
        {
            return new TContext();
        }

        protected internal new TContext Context
        {
            get { return base.Context; }
        }
    }
}